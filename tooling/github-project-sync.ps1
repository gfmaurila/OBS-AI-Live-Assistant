[CmdletBinding(DefaultParameterSetName = 'DryRun')]
param(
    [Parameter()][ValidatePattern('^[A-Za-z0-9_.-]+$')][string]$Owner = 'gfmaurila',
    [Parameter()][ValidatePattern('^[A-Za-z0-9_.-]+$')][string]$Repository = 'OBS-AI-Live-Assistant',
    [Parameter()][ValidateRange(1, [int]::MaxValue)][int]$ProjectNumber = 22,
    [Parameter(ParameterSetName = 'DryRun')][switch]$DryRun,
    [Parameter(Mandatory, ParameterSetName = 'Apply')][switch]$Apply,
    [Parameter()][ValidatePattern('^TASK-\d{3}$')][string]$TaskId,
    [Parameter()][switch]$VerboseReport
)

$ErrorActionPreference = 'Stop'
$ProgressPreference = 'SilentlyContinue'
$repositoryRoot = Split-Path -Parent $PSScriptRoot
$modulePath = Join-Path $PSScriptRoot 'GitHubProjectSync.psm1'
Import-Module $modulePath -Force

$script:ApiBase = 'https://api.github.com'
$script:Token = $null
$script:Headers = $null
$script:Mode = if ($Apply) { 'APPLY' } else { 'DRY_RUN' }
$script:Metrics = [ordered]@{
    IssuesExistingReused = 0
    IssuesCreated = 0
    IssuesUpdated = 0
    IssuesClosed = 0
    IssuesReopened = 0
    ProjectItemsExisting = 0
    ProjectItemsAdded = 0
    ProjectFieldsCreated = 0
    ProjectFieldsUpdated = 0
    ProjectValuesUpdated = 0
}

function Write-Operation {
    param([string]$Message, [ConsoleColor]$Color = [ConsoleColor]::Gray)
    Write-Host (Protect-LogMessage -Message $Message) -ForegroundColor $Color
}

function Get-GitHubAccessToken {
    if (-not [string]::IsNullOrWhiteSpace($env:GH_TOKEN)) { return $env:GH_TOKEN }
    if (-not [string]::IsNullOrWhiteSpace($env:GITHUB_TOKEN)) { return $env:GITHUB_TOKEN }

    $gh = Get-Command gh -ErrorAction SilentlyContinue
    $ghPath = if ($gh) { $gh.Source } else { Join-Path $env:ProgramFiles 'GitHub CLI\gh.exe' }
    if (Test-Path -LiteralPath $ghPath -PathType Leaf) {
        $ghToken = (& $ghPath auth token 2>$null | Out-String).Trim()
        if ($LASTEXITCODE -eq 0 -and -not [string]::IsNullOrWhiteSpace($ghToken)) { return $ghToken }
    }

    $credentialInput = "protocol=https`nhost=github.com`n`n"
    $credentialOutput = $credentialInput | git credential fill 2>$null
    if ($LASTEXITCODE -eq 0) {
        foreach ($line in $credentialOutput) {
            if ($line -match '^password=(.+)$') { return $Matches[1] }
        }
    }

    throw 'Credencial GitHub ausente. Use GH_TOKEN/GITHUB_TOKEN, gh auth login ou o helper seguro do Git.'
}

function Get-HttpStatusCode {
    param([Parameter(Mandatory)]$ErrorRecord)
    try { return [int]$ErrorRecord.Exception.Response.StatusCode }
    catch { return 0 }
}

function Test-RateLimitError {
    param([Parameter(Mandatory)]$ErrorRecord)
    $message = @([string]$ErrorRecord.Exception.Message, [string]$ErrorRecord.ErrorDetails.Message) -join "`n"
    if ($message -match '(?i)rate limit|secondary rate') { return $true }
    try {
        return [string]$ErrorRecord.Exception.Response.Headers['X-RateLimit-Remaining'] -eq '0'
    }
    catch { return $false }
}

function Get-RetryDelaySeconds {
    param([Parameter(Mandatory)]$ErrorRecord, [Parameter(Mandatory)][int]$Attempt)
    try {
        $retryAfter = [string]$ErrorRecord.Exception.Response.Headers['Retry-After']
        if ($retryAfter -match '^\d+$') { return [Math]::Min([int]$retryAfter, 60) }
        $reset = [string]$ErrorRecord.Exception.Response.Headers['X-RateLimit-Reset']
        if ($reset -match '^\d+$') {
            $delay = [int64]$reset - [DateTimeOffset]::UtcNow.ToUnixTimeSeconds() + 1
            return [Math]::Max(1, [Math]::Min([int]$delay, 60))
        }
    }
    catch { }
    [Math]::Min([Math]::Pow(2, $Attempt), 30)
}

function Invoke-WithRetry {
    param(
        [Parameter(Mandatory)][scriptblock]$Operation,
        [Parameter(Mandatory)][string]$Description,
        [int]$MaximumAttempts = 4
    )

    for ($attempt = 1; $attempt -le $MaximumAttempts; $attempt++) {
        try { return & $Operation }
        catch {
            $status = Get-HttpStatusCode -ErrorRecord $_
            $retryable = (Test-RetryableStatusCode -StatusCode $status) -or ($status -eq 403 -and (Test-RateLimitError -ErrorRecord $_))
            if (-not $retryable -or $attempt -eq $MaximumAttempts) {
                $safeMessage = Protect-LogMessage -Message ([string]$_.Exception.Message)
                throw "$Description falhou (HTTP $status): $safeMessage"
            }
            $delay = Get-RetryDelaySeconds -ErrorRecord $_ -Attempt $attempt
            Write-Operation "Falha transitoria em '$Description' (HTTP $status). Retry $($attempt + 1)/$MaximumAttempts em $delay s." Yellow
            Start-Sleep -Seconds $delay
        }
    }
}

function Invoke-GitHubRest {
    param(
        [Parameter(Mandatory)][ValidateSet('GET', 'POST', 'PATCH')][string]$Method,
        [Parameter(Mandatory)][string]$Path,
        [object]$Body
    )

    $uri = if ($Path.StartsWith('http', [System.StringComparison]::OrdinalIgnoreCase)) { $Path } else { "$($script:ApiBase)$Path" }
    Invoke-WithRetry -Description "$Method $Path" -Operation {
        $parameters = @{
            Method = $Method
            Uri = $uri
            Headers = $script:Headers
            ErrorAction = 'Stop'
        }
        if ($null -ne $Body) {
            $parameters.ContentType = 'application/json; charset=utf-8'
            $parameters.Body = $Body | ConvertTo-Json -Depth 30 -Compress
        }
        Invoke-RestMethod @parameters
    }
}

function Invoke-GitHubGraphQl {
    param(
        [Parameter(Mandatory)][string]$Query,
        [hashtable]$Variables = @{},
        [string]$Description = 'GraphQL'
    )

    $response = Invoke-GitHubRest -Method POST -Path '/graphql' -Body @{ query = $Query; variables = $Variables }
    if ($response.errors) {
        $messages = @($response.errors | ForEach-Object message) -join '; '
        $safe = Protect-LogMessage -Message $messages
        if ($safe -match "requires one of the following scopes: \['read:project'\]") {
            throw 'A credencial atual nao possui read:project/project. Autorize o escopo project antes de sincronizar o Project V2.'
        }
        throw "$Description retornou erro GraphQL: $safe"
    }
    $response.data
}

function Get-AllGitHubIssues {
    $entries = @(Invoke-PaginatedCollection -PageSize 100 -FetchPage {
        param($page, $pageSize)
        $batch = @(Invoke-GitHubRest -Method GET -Path "/repos/$Owner/$Repository/issues?state=all&sort=created&direction=asc&per_page=$pageSize&page=$page")
        $batch
    })
    @($entries | Where-Object { -not $_.pull_request })
}

function Get-AllGitHubPullRequests {
    @(Invoke-PaginatedCollection -PageSize 100 -FetchPage {
        param($page, $pageSize)
        @(Invoke-GitHubRest -Method GET -Path "/repos/$Owner/$Repository/pulls?state=all&sort=created&direction=asc&per_page=$pageSize&page=$page")
    })
}

function Get-ProjectSnapshot {
    $query = @'
query($login: String!, $number: Int!, $cursor: String) {
  user(login: $login) {
    projectV2(number: $number) {
      id
      title
      url
      viewerCanUpdate
      fields(first: 100) {
        nodes {
          ... on ProjectV2Field { id name dataType }
          ... on ProjectV2SingleSelectField {
            id name dataType
            options { id name color description }
          }
          ... on ProjectV2IterationField { id name dataType }
        }
      }
      items(first: 100, after: $cursor) {
        pageInfo { hasNextPage endCursor }
        nodes {
          id
          type
          content {
            __typename
            ... on Issue { id number title url state repository { nameWithOwner } }
            ... on PullRequest { id number title url state repository { nameWithOwner } }
          }
          fieldValues(first: 100) {
            nodes {
              ... on ProjectV2ItemFieldSingleSelectValue {
                name optionId
                field { ... on ProjectV2FieldCommon { id name } }
              }
              ... on ProjectV2ItemFieldTextValue {
                text
                field { ... on ProjectV2FieldCommon { id name } }
              }
              ... on ProjectV2ItemFieldNumberValue {
                number
                field { ... on ProjectV2FieldCommon { id name } }
              }
            }
          }
        }
      }
    }
  }
  rateLimit { limit remaining resetAt }
}
'@
    $items = [System.Collections.Generic.List[object]]::new()
    $cursor = $null
    $project = $null
    $rateLimit = $null
    do {
        $data = Invoke-GitHubGraphQl -Query $query -Variables @{ login=$Owner; number=$ProjectNumber; cursor=$cursor } -Description 'Consulta do Project V2'
        if (-not $data.user.projectV2) { throw "GitHub Project #$ProjectNumber nao encontrado para $Owner." }
        if (-not $project) { $project = $data.user.projectV2 }
        foreach ($item in @($data.user.projectV2.items.nodes)) { $items.Add($item) }
        $pageInfo = $data.user.projectV2.items.pageInfo
        $cursor = $pageInfo.endCursor
        $rateLimit = $data.rateLimit
    } while ($pageInfo.hasNextPage)

    if (-not $project.viewerCanUpdate -and $Apply) { throw "A credencial nao pode atualizar o Project #$ProjectNumber." }
    [pscustomobject]@{
        Id = $project.id
        Title = $project.title
        Url = $project.url
        ViewerCanUpdate = $project.viewerCanUpdate
        Fields = @($project.fields.nodes | Where-Object id)
        Items = @($items)
        Repository = "$Owner/$Repository"
        RateLimit = $rateLimit
    }
}

function Get-DesiredProjectFieldDefinitions {
    @(
        [pscustomobject]@{ Name='Status'; DataType='SINGLE_SELECT'; Options=@(
            @{ name='Backlog'; color='GRAY'; description='Task registrada, ainda não autorizada ou elegível.' },
            @{ name='Ready'; color='BLUE'; description='Definition of Ready formalmente aprovada.' },
            @{ name='In Progress'; color='YELLOW'; description='Execução autorizada e iniciada.' },
            @{ name='Code Review'; color='PURPLE'; description='Implementação submetida para revisão.' },
            @{ name='Validation'; color='ORANGE'; description='Gates, PR ou validação pós-merge em andamento.' },
            @{ name='Done'; color='GREEN'; description='Definition of Done e finalização administrativa concluídas.' },
            @{ name='Blocked'; color='RED'; description='Impedimento formal registrado.' }
        ) },
        [pscustomobject]@{ Name='Priority'; DataType='SINGLE_SELECT'; Options=@(
            @{ name='P0'; color='RED'; description='Prioridade P0 canônica.' },
            @{ name='P1'; color='YELLOW'; description='Prioridade P1 canônica.' }
        ) },
        [pscustomobject]@{ Name='Epic'; DataType='TEXT'; Options=@() },
        [pscustomobject]@{ Name='Wave'; DataType='NUMBER'; Options=@() },
        [pscustomobject]@{ Name='Complexity'; DataType='SINGLE_SELECT'; Options=@(
            @{ name='S'; color='GREEN'; description='Complexidade S.' },
            @{ name='M'; color='BLUE'; description='Complexidade M.' },
            @{ name='L'; color='YELLOW'; description='Complexidade L.' },
            @{ name='XL'; color='RED'; description='Complexidade XL.' }
        ) },
        [pscustomobject]@{ Name='Risk'; DataType='SINGLE_SELECT'; Options=@(
            @{ name='LOW'; color='GREEN'; description='Risco baixo.' },
            @{ name='MEDIUM'; color='YELLOW'; description='Risco médio.' },
            @{ name='HIGH'; color='RED'; description='Risco alto.' }
        ) },
        [pscustomobject]@{ Name='Task ID'; DataType='TEXT'; Options=@() },
        [pscustomobject]@{ Name='Dependencies'; DataType='TEXT'; Options=@() }
    )
}

function Get-ProjectFieldPlan {
    param([Parameter(Mandatory)]$Snapshot)
    $operations = [System.Collections.Generic.List[object]]::new()
    foreach ($desired in Get-DesiredProjectFieldDefinitions) {
        $matches = @($Snapshot.Fields | Where-Object { $_.name -ieq $desired.Name })
        if ($matches.Count -gt 1) {
            $operations.Add([pscustomobject]@{ Kind='FIELD_CONFLICT'; Field=$desired.Name; Detail='Mais de um campo com o mesmo nome.' })
            continue
        }
        if ($matches.Count -eq 0) {
            $operations.Add([pscustomobject]@{ Kind='CREATE_FIELD'; Field=$desired.Name; Detail=$desired.DataType })
            continue
        }
        $existing = $matches[0]
        if ([string]$existing.dataType -cne $desired.DataType) {
            $operations.Add([pscustomobject]@{ Kind='FIELD_CONFLICT'; Field=$desired.Name; Detail="Tipo existente $($existing.dataType), esperado $($desired.DataType)." })
            continue
        }
        if ($desired.DataType -eq 'SINGLE_SELECT') {
            $missing = @($desired.Options | Where-Object { $_.name -notin @($existing.options.name) })
            if ($missing.Count -gt 0) {
                $operations.Add([pscustomobject]@{ Kind='ADD_FIELD_OPTIONS'; Field=$desired.Name; Detail=@($missing.name) -join ', ' })
            }
        }
    }
    @($operations)
}

function New-ProjectField {
    param([Parameter(Mandatory)]$Snapshot, [Parameter(Mandatory)]$Definition)
    $mutation = @'
mutation($input: CreateProjectV2FieldInput!) {
  createProjectV2Field(input: $input) { projectV2Field { ... on ProjectV2FieldCommon { id name dataType } } }
}
'@
    $input = @{ projectId=$Snapshot.Id; dataType=$Definition.DataType; name=$Definition.Name }
    if ($Definition.DataType -eq 'SINGLE_SELECT') { $input.singleSelectOptions = @($Definition.Options) }
    $null = Invoke-GitHubGraphQl -Query $mutation -Variables @{ input=$input } -Description "Criação do campo $($Definition.Name)"
    $script:Metrics.ProjectFieldsCreated++
}

function Add-MissingSingleSelectOptions {
    param([Parameter(Mandatory)]$ExistingField, [Parameter(Mandatory)]$Definition)
    $missing = @($Definition.Options | Where-Object { $_.name -notin @($ExistingField.options.name) })
    if ($missing.Count -eq 0) { return }

    $options = [System.Collections.Generic.List[object]]::new()
    foreach ($option in @($ExistingField.options)) {
        $options.Add(@{
            id = $option.id
            name = $option.name
            color = [string]$option.color
            description = if ($null -eq $option.description) { '' } else { [string]$option.description }
        })
    }
    foreach ($option in $missing) { $options.Add($option) }

    $mutation = @'
mutation($input: UpdateProjectV2FieldInput!) {
  updateProjectV2Field(input: $input) { projectV2Field { ... on ProjectV2FieldCommon { id name dataType } } }
}
'@
    $null = Invoke-GitHubGraphQl -Query $mutation -Variables @{ input=@{ fieldId=$ExistingField.id; singleSelectOptions=@($options) } } -Description "Atualização segura das opções de $($Definition.Name)"
    $script:Metrics.ProjectFieldsUpdated++
}

function Ensure-ProjectFields {
    param([Parameter(Mandatory)]$Snapshot)
    $plan = @(Get-ProjectFieldPlan -Snapshot $Snapshot)
    $conflicts = @($plan | Where-Object Kind -eq 'FIELD_CONFLICT')
    if ($conflicts.Count -gt 0) {
        throw "Conflito de campos no Project: $(@($conflicts | ForEach-Object { "$($_.Field): $($_.Detail)" }) -join '; ')"
    }
    foreach ($definition in Get-DesiredProjectFieldDefinitions) {
        $existing = @($Snapshot.Fields | Where-Object { $_.name -ieq $definition.Name })
        if ($existing.Count -eq 0) {
            Write-Operation "Criando campo '$($definition.Name)' ($($definition.DataType))." Cyan
            New-ProjectField -Snapshot $Snapshot -Definition $definition
        }
        elseif ($definition.DataType -eq 'SINGLE_SELECT') {
            $missing = @($definition.Options | Where-Object { $_.name -notin @($existing[0].options.name) })
            if ($missing.Count -gt 0) {
                Write-Operation "Adicionando opções em '$($definition.Name)' sem remover as existentes: $(@($missing.name) -join ', ')." Cyan
                Add-MissingSingleSelectOptions -ExistingField $existing[0] -Definition $definition
            }
        }
    }
}

function New-Issue {
    param([Parameter(Mandatory)]$Task)
    $body = New-ManagedIssueBody -Task $Task -Owner $Owner -Repository $Repository
    $issue = Invoke-GitHubRest -Method POST -Path "/repos/$Owner/$Repository/issues" -Body @{ title="[$($Task.Id)] $($Task.Title)"; body=$body }
    $script:Metrics.IssuesCreated++
    Write-Operation "Criada Issue #$($issue.number) para $($Task.Id)." Green
    $issue
}

function Get-AllIssueNumberMap {
    param([Parameter(Mandatory)][object[]]$Issues)
    $allResolved = Resolve-TaskIssueMap -Tasks $script:AllTasks -Issues $Issues
    $map = @{}
    foreach ($entry in $allResolved.Map.GetEnumerator()) { $map[$entry.Key] = [int]$entry.Value.number }
    $map
}

function Update-Issues {
    param([Parameter(Mandatory)][object[]]$Tasks, [Parameter(Mandatory)][object[]]$Issues)
    $resolved = Resolve-TaskIssueMap -Tasks $Tasks -Issues $Issues
    if ($resolved.Conflicts.Count -gt 0) {
        throw "Issues duplicadas/ambíguas: $(@($resolved.Conflicts | ForEach-Object { "$($_.TaskId) -> $(@($_.IssueNumbers) -join ',')" }) -join '; ')"
    }
    $issueNumberByTaskId = Get-AllIssueNumberMap -Issues $Issues

    foreach ($task in $Tasks) {
        $issue = $resolved.Map[$task.Id]
        $desiredTitle = "[$($task.Id)] $($task.Title)"
        $managedBody = New-ManagedIssueBody -Task $task -Owner $Owner -Repository $Repository -IssueNumberByTaskId $issueNumberByTaskId
        $desiredBody = Merge-ManagedIssueBody -ExistingBody ([string]$issue.body) -ManagedBody $managedBody
        $desiredState = if ($task.Status -eq 'DONE') { 'closed' } else { 'open' }
        $changes = @{}
        if ([string]$issue.title -cne $desiredTitle) { $changes.title = $desiredTitle }
        if (([string]$issue.body).Replace("`r`n", "`n").Trim() -cne $desiredBody.Replace("`r`n", "`n").Trim()) { $changes.body = $desiredBody }
        if ([string]$issue.state -cne $desiredState) { $changes.state = $desiredState }
        if ($changes.Count -gt 0) {
            $null = Invoke-GitHubRest -Method PATCH -Path "/repos/$Owner/$Repository/issues/$($issue.number)" -Body $changes
            $script:Metrics.IssuesUpdated++
            if ($changes.state -eq 'closed') { $script:Metrics.IssuesClosed++ }
            if ($changes.state -eq 'open') { $script:Metrics.IssuesReopened++ }
            Write-Operation "Atualizada Issue #$($issue.number) para $($task.Id): $(@($changes.Keys) -join ', ')." Green
        }
        else { $script:Metrics.IssuesExistingReused++ }
    }
}

function Add-ProjectItem {
    param([Parameter(Mandatory)]$Snapshot, [Parameter(Mandatory)]$Issue)
    $mutation = @'
mutation($input: AddProjectV2ItemByIdInput!) {
  addProjectV2ItemById(input: $input) { item { id } }
}
'@
    $data = Invoke-GitHubGraphQl -Query $mutation -Variables @{ input=@{ projectId=$Snapshot.Id; contentId=$Issue.node_id } } -Description "Vínculo da Issue #$($Issue.number)"
    $script:Metrics.ProjectItemsAdded++
    $data.addProjectV2ItemById.item
}

function Set-ProjectItemFieldValue {
    param(
        [Parameter(Mandatory)]$Snapshot,
        [Parameter(Mandatory)]$Item,
        [Parameter(Mandatory)]$Field,
        [Parameter(Mandatory)]$Value
    )
    $mutation = @'
mutation($input: UpdateProjectV2ItemFieldValueInput!) {
  updateProjectV2ItemFieldValue(input: $input) { projectV2Item { id } }
}
'@
    $inputValue = switch ([string]$Field.dataType) {
        'SINGLE_SELECT' {
            $option = @($Field.options | Where-Object { $_.name -ieq [string]$Value })
            if ($option.Count -ne 1) { throw "Opção '$Value' ausente/ambígua no campo '$($Field.name)'." }
            @{ singleSelectOptionId=$option[0].id }
        }
        'TEXT' { @{ text=[string]$Value } }
        'NUMBER' { @{ number=[double]$Value } }
        default { throw "Tipo não gerenciado para '$($Field.name)': $($Field.dataType)" }
    }
    $null = Invoke-GitHubGraphQl -Query $mutation -Variables @{ input=@{ projectId=$Snapshot.Id; itemId=$Item.id; fieldId=$Field.id; value=$inputValue } } -Description "Campo $($Field.name) de $($Item.id)"
    $script:Metrics.ProjectValuesUpdated++
}

function Get-ObservedFieldValue {
    param([Parameter(Mandatory)]$Item, [Parameter(Mandatory)]$Field)
    $value = @($Item.fieldValues.nodes | Where-Object { $_.field.id -eq $Field.id })
    if ($value.Count -eq 0) { return $null }
    switch ([string]$Field.dataType) {
        'SINGLE_SELECT' { return [string]$value[0].name }
        'TEXT' { return [string]$value[0].text }
        'NUMBER' { return [double]$value[0].number }
    }
}

function Get-DesiredValuesForTask {
    param([Parameter(Mandatory)]$Task)
    @{
        Status = ConvertTo-ProjectStatus -CanonicalStatus $Task.Status
        Priority = $Task.Priority
        Epic = $Task.Epic
        Wave = [double]$Task.Wave
        Complexity = $Task.Complexity
        Risk = $Task.Risk
        'Task ID' = $Task.Id
        Dependencies = if ($Task.Dependencies.Count -eq 0) { 'Nenhuma' } else { @($Task.Dependencies) -join ', ' }
    }
}

function Sync-ProjectItemsAndValues {
    param([Parameter(Mandatory)][object[]]$Tasks, [Parameter(Mandatory)][object[]]$Issues, [Parameter(Mandatory)]$Snapshot)
    $resolved = Resolve-TaskIssueMap -Tasks $Tasks -Issues $Issues
    foreach ($task in $Tasks) {
        $issue = $resolved.Map[$task.Id]
        $item = @($Snapshot.Items | Where-Object { $_.content.__typename -eq 'Issue' -and $_.content.repository.nameWithOwner -eq "$Owner/$Repository" -and $_.content.number -eq $issue.number })
        if ($item.Count -gt 1) { throw "Issue #$($issue.number) possui múltiplos itens no Project." }
        if ($item.Count -eq 0) {
            Write-Operation "Vinculando Issue #$($issue.number) ao Project #$ProjectNumber." Cyan
            $newItem = Add-ProjectItem -Snapshot $Snapshot -Issue $issue
            $item = @([pscustomobject]@{ id=$newItem.id; content=[pscustomobject]@{ __typename='Issue'; number=$issue.number; repository=[pscustomobject]@{ nameWithOwner="$Owner/$Repository" } }; fieldValues=[pscustomobject]@{ nodes=@() } })
        }
        else { $script:Metrics.ProjectItemsExisting++ }

        $desiredValues = Get-DesiredValuesForTask -Task $task
        foreach ($fieldName in $desiredValues.Keys) {
            $field = @($Snapshot.Fields | Where-Object { $_.name -ieq $fieldName })
            if ($field.Count -ne 1) { throw "Campo obrigatório '$fieldName' ausente ou ambíguo no Project." }
            $observed = Get-ObservedFieldValue -Item $item[0] -Field $field[0]
            $desired = $desiredValues[$fieldName]
            $equal = if ($field[0].dataType -eq 'NUMBER') { [double]$observed -eq [double]$desired } else { [string]$observed -ceq [string]$desired }
            if (-not $equal) {
                Set-ProjectItemFieldValue -Snapshot $Snapshot -Item $item[0] -Field $field[0] -Value $desired
            }
        }
    }
}

function Get-DetailedDryRunPlan {
    param([Parameter(Mandatory)][object[]]$Tasks, [Parameter(Mandatory)][object[]]$Issues, [Parameter(Mandatory)]$Snapshot)
    $basic = Get-SyncPlan -Tasks $Tasks -Issues $Issues -ProjectSnapshot $Snapshot
    $operations = [System.Collections.Generic.List[object]]::new()
    foreach ($operation in $basic.Operations) { $operations.Add($operation) }
    foreach ($operation in Get-ProjectFieldPlan -Snapshot $Snapshot) { $operations.Add([pscustomobject]@{ TaskId='PROJECT'; Kind=$operation.Kind; Detail="$($operation.Field): $($operation.Detail)" }) }

    $resolved = Resolve-TaskIssueMap -Tasks $Tasks -Issues $Issues
    $issueNumberByTaskId = Get-AllIssueNumberMap -Issues $Issues
    foreach ($task in $Tasks) {
        if (-not $resolved.Map.ContainsKey($task.Id)) {
            foreach ($entry in (Get-DesiredValuesForTask -Task $task).GetEnumerator()) {
                $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='UPDATE_PROJECT_VALUE'; Detail="$($entry.Key) = $($entry.Value) após criar e vincular a Issue" })
            }
            continue
        }
        $issue = $resolved.Map[$task.Id]
        $desiredBody = Merge-ManagedIssueBody -ExistingBody ([string]$issue.body) -ManagedBody (New-ManagedIssueBody -Task $task -Owner $Owner -Repository $Repository -IssueNumberByTaskId $issueNumberByTaskId)
        if (([string]$issue.body).Replace("`r`n", "`n").Trim() -cne $desiredBody.Replace("`r`n", "`n").Trim()) {
            $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='UPDATE_ISSUE_BODY'; Detail="Issue #$($issue.number)" })
        }
        $item = @($Snapshot.Items | Where-Object { $_.content.__typename -eq 'Issue' -and $_.content.repository.nameWithOwner -eq "$Owner/$Repository" -and $_.content.number -eq $issue.number })
        if ($item.Count -eq 1) {
            $desiredValues = Get-DesiredValuesForTask -Task $task
            foreach ($fieldName in $desiredValues.Keys) {
                $field = @($Snapshot.Fields | Where-Object { $_.name -ieq $fieldName })
                if ($field.Count -ne 1) { continue }
                $observed = Get-ObservedFieldValue -Item $item[0] -Field $field[0]
                $desired = $desiredValues[$fieldName]
                $equal = if ($field[0].dataType -eq 'NUMBER') { $null -ne $observed -and [double]$observed -eq [double]$desired } else { [string]$observed -ceq [string]$desired }
                if (-not $equal) { $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='UPDATE_PROJECT_VALUE'; Detail="$fieldName = $desired" }) }
            }
        }
        elseif ($item.Count -eq 0) {
            foreach ($entry in (Get-DesiredValuesForTask -Task $task).GetEnumerator()) {
                $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='UPDATE_PROJECT_VALUE'; Detail="$($entry.Key) = $($entry.Value) após vincular a Issue" })
            }
        }
    }
    [pscustomobject]@{ TaskCount=$basic.TaskCount; ExistingIssueCount=$basic.ExistingIssueCount; ConflictCount=$basic.ConflictCount; Conflicts=$basic.Conflicts; Operations=@($operations) }
}

function Write-Plan {
    param([Parameter(Mandatory)]$Plan)
    Write-Operation "Modo: $($script:Mode)" Cyan
    Write-Operation "Tasks canônicas: $($Plan.TaskCount)" Cyan
    Write-Operation "Issues canônicas reutilizáveis: $($Plan.ExistingIssueCount)" Cyan
    Write-Operation "Conflitos: $($Plan.ConflictCount)" $(if ($Plan.ConflictCount) { 'Red' } else { 'Green' })
    $groups = @($Plan.Operations | Group-Object Kind | Sort-Object Name)
    if ($groups.Count -eq 0) { Write-Operation 'Operações necessárias: 0' Green }
    else {
        Write-Operation 'Operações planejadas:' Yellow
        foreach ($group in $groups) { Write-Host ("  {0,-28} {1,3}" -f $group.Name, $group.Count) }
    }
    if ($VerboseReport -and $Plan.Operations.Count -gt 0) {
        $Plan.Operations | Sort-Object TaskId,Kind | Format-Table TaskId,Kind,Detail -AutoSize | Out-String | Write-Host
    }
}

function Get-CheckpointReport {
    param([Parameter(Mandatory)][object[]]$Tasks, [Parameter(Mandatory)][object[]]$Issues, [Parameter(Mandatory)]$Snapshot)
    $resolved = Resolve-TaskIssueMap -Tasks $Tasks -Issues $Issues
    foreach ($task in $Tasks) {
        $issue = if ($resolved.Map.ContainsKey($task.Id)) { $resolved.Map[$task.Id] } else { $null }
        $item = if ($issue) { @($Snapshot.Items | Where-Object { $_.content.__typename -eq 'Issue' -and $_.content.repository.nameWithOwner -eq "$Owner/$Repository" -and $_.content.number -eq $issue.number }) | Select-Object -First 1 } else { $null }
        $statusField = @($Snapshot.Fields | Where-Object name -ieq 'Status') | Select-Object -First 1
        [pscustomobject]@{
            TaskId = $task.Id
            IssueNumber = if ($issue) { $issue.number } else { $null }
            IssueUrl = if ($issue) { $issue.html_url } else { $null }
            ProjectItemId = if ($item) { $item.id } else { $null }
            ExpectedStatus = ConvertTo-ProjectStatus -CanonicalStatus $task.Status
            ObservedStatus = if ($item -and $statusField) { Get-ObservedFieldValue -Item $item -Field $statusField } else { $null }
            LastSynchronizationResult = if ($issue -and $item) { 'VERIFIED' } else { 'MISSING' }
        }
    }
}

try {
    Write-Operation "Carregando inventário canônico ($($script:Mode))..." Cyan
    $script:AllTasks = @(Get-CanonicalTaskInventory -RepositoryRoot $repositoryRoot)
    $tasks = if ($TaskId) { @($script:AllTasks | Where-Object Id -eq $TaskId.ToUpperInvariant()) } else { @($script:AllTasks) }
    if ($tasks.Count -eq 0) { throw 'Nenhuma Task canônica corresponde ao filtro informado.' }

    $script:Token = Get-GitHubAccessToken
    $script:Headers = @{
        Accept = 'application/vnd.github+json'
        Authorization = "Bearer $($script:Token)"
        'X-GitHub-Api-Version' = '2022-11-28'
        'User-Agent' = 'OBS-AI-Live-Assistant-github-project-sync'
    }

    $repoInfo = Invoke-GitHubRest -Method GET -Path "/repos/$Owner/$Repository"
    if (-not $repoInfo.has_issues) { throw "Issues estão desabilitadas em $Owner/$Repository." }
    $pullRequests = @(Get-AllGitHubPullRequests)
    $tasks = @(Add-PullRequestEvidence -Tasks $tasks -PullRequests $pullRequests)
    $issues = @(Get-AllGitHubIssues)
    $snapshot = Get-ProjectSnapshot

    $plan = Get-DetailedDryRunPlan -Tasks $tasks -Issues $issues -Snapshot $snapshot
    Write-Plan -Plan $plan
    if ($plan.ConflictCount -gt 0 -or @($plan.Operations | Where-Object Kind -eq 'FIELD_CONFLICT').Count -gt 0) {
        throw 'O plano contém conflitos que exigem reconciliação humana; nenhuma escrita foi executada.'
    }

    if (-not $Apply) {
        Write-Operation 'DRY-RUN concluído sem escrita.' Green
        exit 0
    }

    Ensure-ProjectFields -Snapshot $snapshot
    $snapshot = Get-ProjectSnapshot

    $resolved = Resolve-TaskIssueMap -Tasks $tasks -Issues $issues
    foreach ($task in $tasks) {
        if (-not $resolved.Map.ContainsKey($task.Id)) { $null = New-Issue -Task $task }
    }
    $issues = @(Get-AllGitHubIssues)
    Update-Issues -Tasks $tasks -Issues $issues
    $issues = @(Get-AllGitHubIssues)

    Sync-ProjectItemsAndValues -Tasks $tasks -Issues $issues -Snapshot $snapshot

    $finalIssues = @(Get-AllGitHubIssues)
    $finalSnapshot = Get-ProjectSnapshot
    $finalPlan = Get-DetailedDryRunPlan -Tasks $tasks -Issues $finalIssues -Snapshot $finalSnapshot
    if ($finalPlan.ConflictCount -gt 0 -or $finalPlan.Operations.Count -gt 0) {
        Write-Plan -Plan $finalPlan
        throw "Reconciliação pós-aplicação encontrou $($finalPlan.Operations.Count) operação(ões) pendente(s)."
    }

    $checkpoint = @(Get-CheckpointReport -Tasks $tasks -Issues $finalIssues -Snapshot $finalSnapshot)
    if ($VerboseReport) { $checkpoint | Format-Table -AutoSize | Out-String | Write-Host }
    Write-Operation 'SYNC APPLY e reconciliação concluídos.' Green
    [pscustomobject]@{
        Mode = $script:Mode
        Owner = $Owner
        Repository = $Repository
        ProjectNumber = $ProjectNumber
        Tasks = $tasks.Count
        Metrics = [pscustomobject]$script:Metrics
        Checkpoints = $checkpoint
        RemainingOperations = 0
    } | ConvertTo-Json -Depth 8
}
finally {
    if ($script:Headers) { $script:Headers.Authorization = $null }
    $script:Token = $null
}
