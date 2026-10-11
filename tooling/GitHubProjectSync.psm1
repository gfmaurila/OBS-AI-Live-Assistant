Set-StrictMode -Version Latest

$script:ManagedStart = '<!-- obs-ai-managed:start -->'
$script:ManagedEnd = '<!-- obs-ai-managed:end -->'
$script:SupportedStatuses = @(
    'BACKLOG',
    'READY',
    'IN_PROGRESS',
    'CODE_REVIEW',
    'VALIDATION',
    'DONE',
    'BLOCKED'
)

function Get-MarkdownSections {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string]$Content)

    $sections = [ordered]@{}
    $matches = [regex]::Matches($Content, '(?m)^## (?<name>[^\r\n]+)\r?\n')
    for ($index = 0; $index -lt $matches.Count; $index++) {
        $name = $matches[$index].Groups['name'].Value.Trim()
        if ($sections.Contains($name)) {
            continue
        }

        $start = $matches[$index].Index + $matches[$index].Length
        $end = if ($index + 1 -lt $matches.Count) { $matches[$index + 1].Index } else { $Content.Length }
        $sections[$name] = $Content.Substring($start, $end - $start).Trim()
    }

    $sections
}

function ConvertFrom-BacklogTable {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string]$Path)

    $rows = @{}
    foreach ($line in Get-Content -LiteralPath $Path -Encoding UTF8) {
        if ($line -notmatch '^\|\s*(TASK-\d{3})\s*\|') {
            continue
        }

        $cells = @($line.Trim().Trim('|').Split('|') | ForEach-Object Trim)
        if ($cells.Count -lt 10) {
            throw "Linha invalida no backlog para $($Matches[1]): $line"
        }

        $id = $cells[0].ToUpperInvariant()
        if ($rows.ContainsKey($id)) {
            throw "ID duplicado em tasks/BACKLOG.md: $id"
        }

        $rows[$id] = [pscustomobject]@{
            Id = $id
            Epic = $cells[1]
            Title = $cells[2]
            Priority = $cells[3]
            Complexity = $cells[4]
            Risk = $cells[5]
            Dependencies = @([regex]::Matches($cells[6], 'TASK-\d{3}') | ForEach-Object Value)
            Status = $cells[7].ToUpperInvariant().Replace(' ', '_')
            Wave = [int]$cells[8]
            Parallelism = $cells[9]
        }
    }

    $rows
}

function Get-CanonicalTaskInventory {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][string]$RepositoryRoot,
        [string]$TaskId
    )

    $tasksRoot = Join-Path $RepositoryRoot 'tasks'
    $backlogPath = Join-Path $tasksRoot 'BACKLOG.md'
    if (-not (Test-Path -LiteralPath $backlogPath -PathType Leaf)) {
        throw "Backlog canonico nao encontrado: $backlogPath"
    }

    $backlogRows = ConvertFrom-BacklogTable -Path $backlogPath
    $stateDirectories = @('backlog', 'ready', 'in-progress', 'review', 'blocked', 'done')
    $files = foreach ($directory in $stateDirectories) {
        $path = Join-Path $tasksRoot $directory
        if (Test-Path -LiteralPath $path -PathType Container) {
            Get-ChildItem -LiteralPath $path -File -Filter 'TASK-*.md'
        }
    }

    $tasks = [System.Collections.Generic.List[object]]::new()
    foreach ($file in @($files | Sort-Object Name)) {
        if ($file.Name -notmatch '^(TASK-\d{3})') {
            continue
        }

        $id = $Matches[1].ToUpperInvariant()
        if ($TaskId -and $id -ne $TaskId.ToUpperInvariant()) {
            continue
        }

        if (-not $backlogRows.ContainsKey($id)) {
            throw "$id possui arquivo individual, mas nao existe em tasks/BACKLOG.md."
        }

        $content = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8
        $heading = [regex]::Match($content, '(?m)^#\s+(TASK-\d{3})\s+(?:\u2014|-)\s+(.+?)\s*$')
        if (-not $heading.Success -or $heading.Groups[1].Value.ToUpperInvariant() -ne $id) {
            throw "Cabecalho canonico invalido em $($file.FullName)."
        }

        $sections = Get-MarkdownSections -Content $content
        foreach ($required in @('Objetivo', 'Epic', 'Dependências', 'Prioridade', 'Complexidade', 'Risco', 'Critérios de aceite', 'Definition of Ready', 'Definition of Done', 'Status')) {
            if (-not $sections.Contains($required) -or [string]::IsNullOrWhiteSpace([string]$sections[$required])) {
                throw "$id nao possui a secao obrigatoria '$required'."
            }
        }

        $statusText = ([string]$sections['Status']).ToUpperInvariant().Replace(' ', '_')
        $statusMatch = [regex]::Match($statusText, '(?m)^(BACKLOG|READY|IN_PROGRESS|CODE_REVIEW|VALIDATION|DONE|BLOCKED)(?:_|\b|$)')
        if (-not $statusMatch.Success) { throw "Status canonico nao suportado em ${id}: $statusText" }
        $status = $statusMatch.Groups[1].Value
        $allowedStatusByDirectory = @{
            'backlog' = @('BACKLOG')
            'ready' = @('READY')
            'in-progress' = @('IN_PROGRESS')
            'review' = @('CODE_REVIEW', 'VALIDATION')
            'blocked' = @('BLOCKED')
            'done' = @('DONE')
        }
        if ($status -notin $allowedStatusByDirectory[$file.Directory.Name]) {
            throw "$id possui status $status incompatível com tasks/$($file.Directory.Name)/."
        }

        $row = $backlogRows[$id]
        $dependencies = @([regex]::Matches([string]$sections['Dependências'], 'TASK-\d{3}') | ForEach-Object { $_.Value.ToUpperInvariant() })
        $relativePath = $file.FullName.Substring($RepositoryRoot.TrimEnd('\', '/').Length + 1).Replace('\', '/')
        $pullRequests = @([regex]::Matches($content, 'https://github\.com/[^\s\)]+/pull/\d+') | ForEach-Object Value | Sort-Object -Unique)

        $tasks.Add([pscustomobject]@{
            Id = $id
            Title = $heading.Groups[2].Value.Trim()
            Epic = ([string]$sections['Epic']).Trim()
            Priority = ([string]$sections['Prioridade']).Trim()
            Complexity = ([string]$sections['Complexidade']).Trim()
            Risk = ([string]$sections['Risco']).Trim()
            Wave = $row.Wave
            Status = $status
            Dependencies = $dependencies
            Objective = [string]$sections['Objetivo']
            ExpectedAreas = if ($sections.Contains('Arquivos/áreas esperadas')) { [string]$sections['Arquivos/áreas esperadas'] } else { '' }
            ExpectedImplementation = if ($sections.Contains('Implementação esperada')) { [string]$sections['Implementação esperada'] } else { '' }
            AcceptanceCriteria = [string]$sections['Critérios de aceite']
            Requirements = if ($sections.Contains('Requirements')) { [string]$sections['Requirements'] } else { '' }
            Adrs = if ($sections.Contains('ADRs')) { [string]$sections['ADRs'] } else { '' }
            RequiredTests = if ($sections.Contains('Testes obrigatórios')) { [string]$sections['Testes obrigatórios'] } else { '' }
            QualityGates = if ($sections.Contains('Quality Gates')) { [string]$sections['Quality Gates'] } else { '' }
            DefinitionOfReady = [string]$sections['Definition of Ready']
            DefinitionOfDone = [string]$sections['Definition of Done']
            PullRequests = $pullRequests
            CanonicalPath = $relativePath
            SourceDirectory = $file.Directory.Name
            ContentHash = (Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash
        })
    }

    Test-CanonicalTaskInventory -Tasks $tasks -BacklogRows $backlogRows -Filtered:([bool]$TaskId)
    @($tasks)
}

function Test-CanonicalTaskInventory {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][object[]]$Tasks,
        [Parameter(Mandatory)][hashtable]$BacklogRows,
        [switch]$Filtered
    )

    $duplicates = @($Tasks | Group-Object Id | Where-Object Count -gt 1)
    if ($duplicates.Count -gt 0) {
        throw "IDs duplicados nos arquivos canonicos: $(@($duplicates.Name) -join ', ')"
    }

    if (-not $Filtered -and $Tasks.Count -ne $BacklogRows.Count) {
        throw "Divergencia entre backlog ($($BacklogRows.Count)) e arquivos individuais ($($Tasks.Count))."
    }

    $knownIds = @($BacklogRows.Keys)
    foreach ($task in $Tasks) {
        $row = $BacklogRows[$task.Id]
        foreach ($property in @('Epic', 'Priority', 'Complexity', 'Risk', 'Wave', 'Status')) {
            if ([string]$task.$property -cne [string]$row.$property) {
                throw "$($task.Id) diverge do backlog no campo ${property}: '$($task.$property)' != '$($row.$property)'."
            }
        }

        $taskDependencies = @($task.Dependencies | Sort-Object -Unique)
        $rowDependencies = @($row.Dependencies | Sort-Object -Unique)
        if (($taskDependencies -join ',') -cne ($rowDependencies -join ',')) {
            throw "$($task.Id) diverge do backlog nas dependencias."
        }

        foreach ($dependency in $taskDependencies) {
            if ($dependency -notin $knownIds) {
                throw "$($task.Id) referencia dependencia inexistente: $dependency"
            }
            if ($dependency -eq $task.Id) {
                throw "$($task.Id) depende de si mesma."
            }
        }
    }

    Test-DependencyCycles -Tasks $Tasks -KnownIds $knownIds
}

function Test-DependencyCycles {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][object[]]$Tasks,
        [Parameter(Mandatory)][string[]]$KnownIds
    )

    $dependenciesById = @{}
    foreach ($task in $Tasks) { $dependenciesById[$task.Id] = @($task.Dependencies) }
    $visiting = [System.Collections.Generic.HashSet[string]]::new()
    $visited = [System.Collections.Generic.HashSet[string]]::new()

    function Visit-TaskDependency {
        param([string]$Id, [string[]]$Trail)
        if ($visited.Contains($Id) -or -not $dependenciesById.ContainsKey($Id)) { return }
        if ($visiting.Contains($Id)) {
            throw "Ciclo de dependencias detectado: $(@($Trail + $Id) -join ' -> ')"
        }
        $null = $visiting.Add($Id)
        foreach ($dependency in $dependenciesById[$Id]) {
            Visit-TaskDependency -Id $dependency -Trail @($Trail + $Id)
        }
        $null = $visiting.Remove($Id)
        $null = $visited.Add($Id)
    }

    foreach ($id in $KnownIds) { Visit-TaskDependency -Id $id -Trail @() }
}

function ConvertTo-ProjectStatus {
    [CmdletBinding()]
    param([Parameter(Mandatory)][string]$CanonicalStatus)

    switch ($CanonicalStatus.ToUpperInvariant().Replace(' ', '_')) {
        'BACKLOG' { 'Backlog' }
        'READY' { 'Ready' }
        'IN_PROGRESS' { 'In Progress' }
        'CODE_REVIEW' { 'Code Review' }
        'VALIDATION' { 'Validation' }
        'DONE' { 'Done' }
        'BLOCKED' { 'Blocked' }
        default { throw "Status canonico nao suportado: $CanonicalStatus" }
    }
}

function ConvertTo-GitHubPathUrl {
    param([Parameter(Mandatory)][string]$Path)
    (@($Path.Replace('\', '/').Split('/') | ForEach-Object { [uri]::EscapeDataString($_) }) -join '/')
}

function New-ManagedIssueBody {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][object]$Task,
        [Parameter(Mandatory)][string]$Owner,
        [Parameter(Mandatory)][string]$Repository,
        [hashtable]$IssueNumberByTaskId = @{}
    )

    $canonicalUrl = "https://github.com/$Owner/$Repository/blob/develop/$(ConvertTo-GitHubPathUrl -Path $Task.CanonicalPath)"
    $backlogUrl = "https://github.com/$Owner/$Repository/blob/develop/tasks/BACKLOG.md"
    $graphUrl = "https://github.com/$Owner/$Repository/blob/develop/tasks/DEPENDENCY_GRAPH.md"
    $dorUrl = "https://github.com/$Owner/$Repository/blob/develop/tasks/DEFINITION_OF_READY.md"
    $dodUrl = "https://github.com/$Owner/$Repository/blob/develop/tasks/DEFINITION_OF_DONE.md"
    $lines = [System.Collections.Generic.List[string]]::new()
    $dash = [char]0x2014
    $lines.Add($script:ManagedStart)
    $lines.Add("# $($Task.Id) $dash $($Task.Title)")
    $lines.Add('')
    $lines.Add('## Identificação')
    $lines.Add('')
    $lines.Add("- Epic: $($Task.Epic)")
    $lines.Add("- Prioridade: $($Task.Priority)")
    $lines.Add("- Complexidade: $($Task.Complexity)")
    $lines.Add("- Risco: $($Task.Risk)")
    $lines.Add("- Wave: $($Task.Wave)")
    $lines.Add("- Status canônico: $($Task.Status)")
    $lines.Add('')
    $lines.Add('## Objetivo')
    $lines.Add('')
    $lines.Add($Task.Objective.Trim())
    $lines.Add('')
    $lines.Add('## Escopo')
    $lines.Add('')
    if (-not [string]::IsNullOrWhiteSpace($Task.ExpectedAreas)) {
        $lines.Add("Áreas previstas: $($Task.ExpectedAreas.Trim())")
        $lines.Add('')
    }
    if (-not [string]::IsNullOrWhiteSpace($Task.ExpectedImplementation)) {
        $lines.Add($Task.ExpectedImplementation.Trim())
    }
    else {
        $lines.Add('Não há detalhamento adicional de implementação no arquivo canônico.')
    }
    $lines.Add('')
    $lines.Add('## Critérios de aceite')
    $lines.Add('')
    $lines.Add($Task.AcceptanceCriteria.Trim())
    $lines.Add('')
    $lines.Add('## Requisitos e ADRs')
    $lines.Add('')
    $lines.Add("- Requirements: $(if ([string]::IsNullOrWhiteSpace($Task.Requirements)) { 'não definidos no arquivo canônico' } else { $Task.Requirements.Trim() })")
    $lines.Add("- ADRs: $(if ([string]::IsNullOrWhiteSpace($Task.Adrs)) { 'não definidos no arquivo canônico' } else { $Task.Adrs.Trim() })")
    $lines.Add('')
    $lines.Add('## Dependências')
    $lines.Add('')
    if ($Task.Dependencies.Count -eq 0) {
        $lines.Add('- Nenhuma.')
    }
    else {
        foreach ($dependency in $Task.Dependencies) {
            $reference = if ($IssueNumberByTaskId.ContainsKey($dependency)) { "#$($IssueNumberByTaskId[$dependency])" } else { 'Issue ainda não vinculada' }
            $lines.Add("- $dependency $dash $reference")
        }
    }
    $lines.Add('')
    $lines.Add('## Testes e validação')
    $lines.Add('')
    $lines.Add("- Testes previstos: $(if ([string]::IsNullOrWhiteSpace($Task.RequiredTests)) { 'não definidos no arquivo canônico' } else { $Task.RequiredTests.Trim() })")
    $lines.Add("- Quality Gates: $(if ([string]::IsNullOrWhiteSpace($Task.QualityGates)) { 'não definidos no arquivo canônico' } else { $Task.QualityGates.Trim() })")
    $lines.Add('')
    $lines.Add('## Definition of Ready')
    $lines.Add('')
    $lines.Add($Task.DefinitionOfReady.Trim())
    $lines.Add('')
    $lines.Add("Política oficial: [DEFINITION_OF_READY.md]($dorUrl).")
    $lines.Add('')
    $lines.Add('## Definition of Done')
    $lines.Add('')
    $lines.Add($Task.DefinitionOfDone.Trim())
    $lines.Add('')
    $lines.Add("Política oficial: [DEFINITION_OF_DONE.md]($dodUrl).")
    $lines.Add('')
    $lines.Add('## Documentação')
    $lines.Add('')
    $lines.Add("- [Arquivo canônico da $($Task.Id)]($canonicalUrl)")
    $lines.Add('')
    $lines.Add('## Referências')
    $lines.Add('')
    $lines.Add("- [Backlog]($backlogUrl)")
    $lines.Add("- [Dependency Graph]($graphUrl)")
    if ($Task.PullRequests.Count -gt 0) {
        $lines.Add('')
        $lines.Add('## Pull Requests')
        $lines.Add('')
        foreach ($pullRequest in $Task.PullRequests) { $lines.Add("- $pullRequest") }
    }
    $lines.Add('')
    $lines.Add("<!-- obs-ai-task-id:$($Task.Id) -->")
    $lines.Add($script:ManagedEnd)
    ($lines -join "`n").Trim()
}

function Merge-ManagedIssueBody {
    [CmdletBinding()]
    param(
        [AllowEmptyString()][string]$ExistingBody,
        [Parameter(Mandatory)][string]$ManagedBody
    )

    if ([string]::IsNullOrWhiteSpace($ExistingBody)) { return $ManagedBody.Trim() }
    $escapedStart = [regex]::Escape($script:ManagedStart)
    $escapedEnd = [regex]::Escape($script:ManagedEnd)
    $pattern = "(?s)$escapedStart.*?$escapedEnd"
    if ([regex]::IsMatch($ExistingBody, $pattern)) {
        return ([regex]::Replace($ExistingBody, $pattern, [System.Text.RegularExpressions.MatchEvaluator]{ param($match) $ManagedBody.Trim() }, 1)).Trim()
    }

    @(
        $ManagedBody.Trim(),
        '',
        '---',
        '',
        '## Conteúdo preexistente preservado',
        '',
        $ExistingBody.Trim()
    ) -join "`n"
}

function Resolve-TaskIssueMap {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][object[]]$Tasks,
        [Parameter(Mandatory)][AllowEmptyCollection()][object[]]$Issues
    )

    $taskIds = @($Tasks.Id)
    $candidates = @{}
    $ambiguousCandidates = @{}
    foreach ($taskId in $taskIds) { $candidates[$taskId] = [System.Collections.Generic.List[object]]::new() }
    foreach ($issue in $Issues) {
        $matchedIds = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
        foreach ($match in [regex]::Matches([string]$issue.body, '<!--\s*obs-ai-task-id:(TASK-\d{3})\s*-->')) {
            $null = $matchedIds.Add($match.Groups[1].Value.ToUpperInvariant())
        }
        if ([string]$issue.title -match '(?i)\b(TASK-\d{3})\b') {
            $null = $matchedIds.Add($Matches[1].ToUpperInvariant())
        }
        $knownMatchedIds = @($matchedIds | Where-Object { $candidates.ContainsKey($_) })
        if ($knownMatchedIds.Count -gt 1) {
            foreach ($matchedId in $knownMatchedIds) {
                if (-not $ambiguousCandidates.ContainsKey($matchedId)) { $ambiguousCandidates[$matchedId] = [System.Collections.Generic.List[object]]::new() }
                $ambiguousCandidates[$matchedId].Add($issue)
            }
            continue
        }
        foreach ($matchedId in $knownMatchedIds) {
            if ($candidates.ContainsKey($matchedId)) { $candidates[$matchedId].Add($issue) }
        }
    }

    $map = @{}
    $conflicts = [System.Collections.Generic.List[object]]::new()
    foreach ($task in $Tasks) {
        $matches = @($candidates[$task.Id] | Sort-Object number -Unique)
        if ($ambiguousCandidates.ContainsKey($task.Id)) {
            $conflicts.Add([pscustomobject]@{ TaskId = $task.Id; IssueNumbers = @($ambiguousCandidates[$task.Id].number); Reason = 'Uma Issue referencia mais de um Task ID.' })
        }
        elseif ($matches.Count -eq 1) {
            $map[$task.Id] = $matches[0]
        }
        elseif ($matches.Count -gt 1) {
            $conflicts.Add([pscustomobject]@{ TaskId = $task.Id; IssueNumbers = @($matches.number); Reason = 'Mais de uma Issue candidata.' })
        }
    }

    [pscustomobject]@{ Map = $map; Conflicts = @($conflicts) }
}

function Protect-LogMessage {
    [CmdletBinding()]
    param([AllowEmptyString()][string]$Message)

    if ($null -eq $Message) { return '' }
    $safe = [regex]::Replace($Message, '(?i)(authorization\s*[:=]\s*(?:bearer|token)\s+)[^\s,;]+', '$1[REDACTED]')
    $safe = [regex]::Replace($safe, '(?i)\b(gh[pousr]_[A-Za-z0-9_]{20,}|github_pat_[A-Za-z0-9_]{20,})\b', '[REDACTED]')
    $safe
}

function Add-PullRequestEvidence {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][object[]]$Tasks,
        [Parameter(Mandatory)][AllowEmptyCollection()][object[]]$PullRequests
    )

    foreach ($task in $Tasks) {
        $pattern = "(?i)(?<![A-Z0-9-])$([regex]::Escape($task.Id))(?![A-Z0-9-])"
        $related = foreach ($pullRequest in $PullRequests) {
            $headRef = if ($pullRequest.head) { [string]$pullRequest.head.ref } else { '' }
            $searchable = @([string]$pullRequest.title, [string]$pullRequest.body, $headRef) -join "`n"
            if ($searchable -match $pattern -and -not [string]::IsNullOrWhiteSpace([string]$pullRequest.html_url)) {
                [string]$pullRequest.html_url
            }
        }
        $task.PullRequests = @(@($task.PullRequests) + @($related) | Sort-Object -Unique)
    }
    $Tasks
}

function Test-RetryableStatusCode {
    [CmdletBinding()]
    param([Parameter(Mandatory)][int]$StatusCode)
    $StatusCode -in @(429, 500, 502, 503, 504)
}

function Invoke-PaginatedCollection {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][scriptblock]$FetchPage,
        [ValidateRange(1, 100)][int]$PageSize = 100
    )

    $results = [System.Collections.Generic.List[object]]::new()
    for ($page = 1; ; $page++) {
        $batch = @(& $FetchPage $page $PageSize)
        foreach ($entry in $batch) { $results.Add($entry) }
        if ($batch.Count -lt $PageSize) { break }
    }
    @($results)
}

function Get-SyncPlan {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)][object[]]$Tasks,
        [Parameter(Mandatory)][AllowEmptyCollection()][object[]]$Issues,
        [object]$ProjectSnapshot
    )

    $resolved = Resolve-TaskIssueMap -Tasks $Tasks -Issues $Issues
    $operations = [System.Collections.Generic.List[object]]::new()
    foreach ($task in $Tasks) {
        if (@($resolved.Conflicts | ForEach-Object TaskId) -contains $task.Id) {
            $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='CONFLICT'; Detail='Mais de uma Issue candidata.' })
            continue
        }
        if (-not $resolved.Map.ContainsKey($task.Id)) {
            $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='CREATE_ISSUE'; Detail="[$($task.Id)] $($task.Title)" })
            if ($task.Status -eq 'DONE') {
                $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='SET_ISSUE_CLOSED'; Detail='Após criar a Issue.' })
            }
            $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='ADD_PROJECT_ITEM'; Detail='Após criar a Issue.' })
            continue
        }
        $issue = $resolved.Map[$task.Id]
        $desiredState = if ($task.Status -eq 'DONE') { 'closed' } else { 'open' }
        if ([string]$issue.title -cne "[$($task.Id)] $($task.Title)") {
            $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='UPDATE_ISSUE_TITLE'; Detail="Issue #$($issue.number)" })
        }
        if ([string]$issue.state -cne $desiredState) {
            $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind="SET_ISSUE_$($desiredState.ToUpperInvariant())"; Detail="Issue #$($issue.number)" })
        }
        if ($ProjectSnapshot) {
            $item = @($ProjectSnapshot.Items | Where-Object { $_.content.__typename -eq 'Issue' -and $_.content.number -eq $issue.number -and $_.content.repository.nameWithOwner -eq $ProjectSnapshot.Repository })
            if ($item.Count -eq 0) {
                $operations.Add([pscustomobject]@{ TaskId=$task.Id; Kind='ADD_PROJECT_ITEM'; Detail="Issue #$($issue.number)" })
            }
        }
    }

    [pscustomobject]@{
        TaskCount = $Tasks.Count
        ExistingIssueCount = $resolved.Map.Count
        ConflictCount = $resolved.Conflicts.Count
        Conflicts = $resolved.Conflicts
        Operations = @($operations)
    }
}

Export-ModuleMember -Function @(
    'ConvertFrom-BacklogTable',
    'ConvertTo-ProjectStatus',
    'Add-PullRequestEvidence',
    'Get-CanonicalTaskInventory',
    'Get-MarkdownSections',
    'Get-SyncPlan',
    'Invoke-PaginatedCollection',
    'Merge-ManagedIssueBody',
    'New-ManagedIssueBody',
    'Protect-LogMessage',
    'Resolve-TaskIssueMap',
    'Test-CanonicalTaskInventory',
    'Test-DependencyCycles',
    'Test-RetryableStatusCode'
)
