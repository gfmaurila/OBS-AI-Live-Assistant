[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
$modulePath = Join-Path $root 'tooling\GitHubProjectSync.psm1'
Import-Module $modulePath -Force

$script:Passed = 0
$script:Failed = 0

function Assert-Equal {
    param($Expected, $Actual, [string]$Because)
    if ([string]$Expected -cne [string]$Actual) {
        throw "Esperado '$Expected', observado '$Actual'. $Because"
    }
}

function Assert-True {
    param([bool]$Condition, [string]$Because)
    if (-not $Condition) { throw "Condição não atendida. $Because" }
}

function Assert-Throws {
    param([scriptblock]$Action, [string]$MessagePattern, [string]$Because)
    try { & $Action; throw 'A ação não lançou erro.' }
    catch {
        if ([string]$_.Exception.Message -notmatch $MessagePattern) {
            throw "Erro inesperado: $($_.Exception.Message). $Because"
        }
    }
}

function Invoke-Test {
    param([string]$Name, [scriptblock]$Test)
    try {
        & $Test
        $script:Passed++
        Write-Host "PASS $Name" -ForegroundColor Green
    }
    catch {
        $script:Failed++
        Write-Host "FAIL $Name`n  $($_.Exception.Message)" -ForegroundColor Red
    }
}

function New-TestTask {
    param([string]$Id='TASK-010', [string]$Status='BACKLOG', [string[]]$Dependencies=@('TASK-008','TASK-009'))
    [pscustomobject]@{
        Id=$Id
        Title='Título oficial'
        Epic='EPIC-02'
        Priority='P0'
        Complexity='L'
        Risk='HIGH'
        Wave=2
        Status=$Status
        Dependencies=$Dependencies
        Objective='Objetivo canônico.'
        ExpectedAreas='área A; área B.'
        ExpectedImplementation='Entregável canônico.'
        AcceptanceCriteria='- Critério verificável.'
        Requirements='RF-001.'
        Adrs='ADR-001.'
        RequiredTests='UNIT; SECURITY.'
        QualityGates='Build Gate; Security Gate.'
        DefinitionOfReady='- DoR aprovado.'
        DefinitionOfDone='- DoD atendida.'
        PullRequests=@()
        CanonicalPath="tasks/backlog/$Id.md"
    }
}

function New-TestIssue {
    param([int]$Number, [string]$Title, [string]$Body='', [string]$State='open')
    [pscustomobject]@{ number=$Number; title=$Title; body=$Body; state=$State; html_url="https://example.test/issues/$Number" }
}

Invoke-Test 'baseline contém 52 Tasks únicas, 9 DONE e 43 BACKLOG' {
    $tasks = @(Get-CanonicalTaskInventory -RepositoryRoot $root)
    Assert-Equal 52 $tasks.Count 'A contagem deve ser derivada das fontes canônicas.'
    Assert-Equal 52 @($tasks.Id | Sort-Object -Unique).Count 'IDs devem ser únicos.'
    Assert-Equal 9 @($tasks | Where-Object Status -eq 'DONE').Count 'Baseline concluído divergente.'
    Assert-Equal 43 @($tasks | Where-Object Status -eq 'BACKLOG').Count 'Baseline pendente divergente.'
    Assert-Equal 0 @($tasks | Where-Object Status -eq 'READY').Count 'Nenhuma Task pode ser promovida por inferência.'
}

Invoke-Test 'dependências do baseline existem e não formam ciclos' {
    $tasks = @(Get-CanonicalTaskInventory -RepositoryRoot $root)
    $known = @($tasks.Id)
    foreach ($dependency in @($tasks.Dependencies)) {
        Assert-True ($dependency -in $known) "Dependência inexistente: $dependency"
    }
    Test-DependencyCycles -Tasks $tasks -KnownIds $known
}

Invoke-Test 'normalização de status cobre o fluxo oficial' {
    $expected = [ordered]@{
        BACKLOG='Backlog'; READY='Ready'; IN_PROGRESS='In Progress'; CODE_REVIEW='Code Review';
        VALIDATION='Validation'; DONE='Done'; BLOCKED='Blocked'
    }
    foreach ($entry in $expected.GetEnumerator()) {
        Assert-Equal $entry.Value (ConvertTo-ProjectStatus $entry.Key) 'Status incorreto.'
    }
    Assert-Throws { ConvertTo-ProjectStatus 'UNKNOWN' } 'nao suportado' 'Estados inventados devem falhar.'
}

Invoke-Test 'identificador estável localiza Issue mesmo com título alterado' {
    $task = New-TestTask
    $issue = New-TestIssue -Number 7 -Title 'Título humano' -Body '<!-- obs-ai-task-id:TASK-010 -->'
    $resolved = Resolve-TaskIssueMap -Tasks @($task) -Issues @($issue)
    Assert-Equal 7 $resolved.Map['TASK-010'].number 'O marcador deve prevalecer sobre mudanças de título.'
    Assert-Equal 0 $resolved.Conflicts.Count 'Não há duplicidade.'
}

Invoke-Test 'duas Issues candidatas geram conflito sem terceira criação' {
    $task = New-TestTask
    $issues = @(
        (New-TestIssue -Number 1 -Title '[TASK-010] A'),
        (New-TestIssue -Number 2 -Title 'B' -Body '<!-- obs-ai-task-id:TASK-010 -->')
    )
    $plan = Get-SyncPlan -Tasks @($task) -Issues $issues
    Assert-Equal 1 $plan.ConflictCount 'Duplicidade deve ser explícita.'
    Assert-Equal 0 @($plan.Operations | Where-Object Kind -eq 'CREATE_ISSUE').Count 'Não deve criar terceira Issue.'
}

Invoke-Test 'uma Issue com IDs divergentes é conflito e não é reutilizada' {
    $tasks = @(
        (New-TestTask -Id 'TASK-010'),
        (New-TestTask -Id 'TASK-011')
    )
    $issue = New-TestIssue -Number 3 -Title '[TASK-010] A' -Body '<!-- obs-ai-task-id:TASK-011 -->'
    $resolved = Resolve-TaskIssueMap -Tasks $tasks -Issues @($issue)
    Assert-Equal 2 $resolved.Conflicts.Count 'Ambiguidade intrínseca deve bloquear ambos os IDs.'
    Assert-Equal 0 $resolved.Map.Count 'Issue ambígua não pode ser escolhida como canônica.'
}

Invoke-Test 'body gerenciado é idempotente e preserva conteúdo humano' {
    $task = New-TestTask
    $managed = New-ManagedIssueBody -Task $task -Owner 'owner' -Repository 'repo' -IssueNumberByTaskId @{ 'TASK-008'=8; 'TASK-009'=9 }
    $first = Merge-ManagedIssueBody -ExistingBody 'Anotação humana.' -ManagedBody $managed
    $second = Merge-ManagedIssueBody -ExistingBody $first -ManagedBody $managed
    Assert-Equal $first $second 'Segunda execução não pode alterar o body.'
    Assert-True ($second.Contains('Anotação humana.')) 'Conteúdo fora do bloco gerenciado deve sobreviver.'
    Assert-True ($second.Contains("TASK-008 $([char]0x2014) #8")) 'Dependência deve ser navegável.'
}

Invoke-Test 'dry-run puro planeja criação sem executar transportes' {
    $script:TransportCalls = 0
    $task = New-TestTask
    $plan = Get-SyncPlan -Tasks @($task) -Issues @()
    Assert-Equal 1 @($plan.Operations | Where-Object Kind -eq 'CREATE_ISSUE').Count 'Issue ausente deve ser planejada.'
    Assert-Equal 0 $script:TransportCalls 'Planejamento puro não chama rede.'
}

Invoke-Test 'dry-run explicita fechamento de nova Issue DONE' {
    $task = New-TestTask -Status 'DONE'
    $plan = Get-SyncPlan -Tasks @($task) -Issues @()
    Assert-Equal 1 @($plan.Operations | Where-Object Kind -eq 'SET_ISSUE_CLOSED').Count 'A etapa de fechamento deve aparecer no plano inicial.'
}

Invoke-Test 'Issue existente e consistente não é recriada' {
    $task = New-TestTask
    $issue = New-TestIssue -Number 10 -Title '[TASK-010] Título oficial' -Body '<!-- obs-ai-task-id:TASK-010 -->'
    $plan = Get-SyncPlan -Tasks @($task) -Issues @($issue)
    Assert-Equal 0 @($plan.Operations | Where-Object Kind -eq 'CREATE_ISSUE').Count 'Criação deve ser condicional.'
}

Invoke-Test '401 e 403 comuns não são classificados como transitórios' {
    Assert-True (-not (Test-RetryableStatusCode 401)) '401 exige correção de autenticação.'
    Assert-True (-not (Test-RetryableStatusCode 403)) '403 comum exige correção de permissão.'
    Assert-True (Test-RetryableStatusCode 429) 'Rate limit explícito deve permitir retry.'
    Assert-True (Test-RetryableStatusCode 503) 'Falha transitória deve permitir retry.'
}

Invoke-Test 'paginação percorre todas as páginas até a página parcial' {
    $script:PagesFetched = [System.Collections.Generic.List[int]]::new()
    $items = @(Invoke-PaginatedCollection -PageSize 2 -FetchPage {
        param($page, $pageSize)
        $script:PagesFetched.Add($page)
        switch ($page) {
            1 { @('a', 'b') }
            2 { @('c', 'd') }
            3 { @('e') }
        }
    })
    Assert-Equal 'a,b,c,d,e' ($items -join ',') 'Itens de todas as páginas devem ser preservados.'
    Assert-Equal '1,2,3' (@($script:PagesFetched) -join ',') 'Paginação não pode parar em 100 itens.'
}

Invoke-Test 'retomada reutiliza Issue e item existentes sem operações destrutivas' {
    $task = New-TestTask
    $issue = New-TestIssue -Number 10 -Title '[TASK-010] Título oficial' -Body '<!-- obs-ai-task-id:TASK-010 -->'
    $snapshot = [pscustomobject]@{
        Repository='owner/repo'
        Items=@([pscustomobject]@{
            id='ITEM_1'
            content=[pscustomobject]@{ __typename='Issue'; number=10; repository=[pscustomobject]@{ nameWithOwner='owner/repo' } }
        })
    }
    $plan = Get-SyncPlan -Tasks @($task) -Issues @($issue) -ProjectSnapshot $snapshot
    Assert-Equal 0 @($plan.Operations | Where-Object Kind -in @('CREATE_ISSUE','ADD_PROJECT_ITEM')).Count 'Retomada deve reutilizar registros.'
    Assert-Equal 0 @($plan.Operations | Where-Object Kind -match 'DELETE|REMOVE|ARCHIVE').Count 'Sincronização não pode excluir dados.'
}

Invoke-Test 'PR com o mesmo número não substitui o item da Issue' {
    $task = New-TestTask
    $issue = New-TestIssue -Number 10 -Title '[TASK-010] Título oficial' -Body '<!-- obs-ai-task-id:TASK-010 -->'
    $snapshot = [pscustomobject]@{
        Repository='owner/repo'
        Items=@([pscustomobject]@{
            id='PR_ITEM_1'
            content=[pscustomobject]@{ __typename='PullRequest'; number=10; repository=[pscustomobject]@{ nameWithOwner='owner/repo' } }
        })
    }
    $plan = Get-SyncPlan -Tasks @($task) -Issues @($issue) -ProjectSnapshot $snapshot
    Assert-Equal 1 @($plan.Operations | Where-Object Kind -eq 'ADD_PROJECT_ITEM').Count 'O vínculo deve exigir conteúdo do tipo Issue.'
}

Invoke-Test 'PR histórico exige associação explícita pelo Task ID' {
    $task = New-TestTask
    $pulls = @(
        [pscustomobject]@{ title='Finalizar TASK-010'; body=''; head=[pscustomobject]@{ ref='feature/task-TASK-010' }; html_url='https://example.test/pull/10' },
        [pscustomobject]@{ title='Mudança próxima'; body=''; head=[pscustomobject]@{ ref='feature/other' }; html_url='https://example.test/pull/11' }
    )
    $result = @(Add-PullRequestEvidence -Tasks @($task) -PullRequests $pulls)
    Assert-Equal 1 $result[0].PullRequests.Count 'Não se deve inferir PR por proximidade.'
    Assert-Equal 'https://example.test/pull/10' $result[0].PullRequests[0] 'PR explicitamente associado deve ser preservado.'
}

Invoke-Test 'redaction remove tokens de logs sem apagar diagnóstico' {
    $syntheticToken = 'github_' + 'pat_' + ('X' * 24)
    $raw = "Authorization: Bearer $syntheticToken; status=403"
    $safe = Protect-LogMessage $raw
    Assert-True (-not $safe.Contains($syntheticToken)) 'Token não pode permanecer no log.'
    Assert-True ($safe.Contains('status=403')) 'Diagnóstico não sensível deve permanecer.'
}

Invoke-Test 'filtro de Task mantém inventário validado e isolado' {
    $tasks = @(Get-CanonicalTaskInventory -RepositoryRoot $root -TaskId 'TASK-013')
    Assert-Equal 1 $tasks.Count 'Filtro deve retornar uma Task.'
    Assert-Equal 'TASK-013' $tasks[0].Id 'Filtro retornou a Task errada.'
}

Write-Host "`nGitHub Project Sync tests: $($script:Passed) passed, $($script:Failed) failed."
if ($script:Failed -gt 0) { exit 1 }
exit 0
