[CmdletBinding()]
param()

# Deterministic quality gates for OBS-AI-Live-Assistant (TASK-004).
# Runs the canonical commands in sequence and reports PASSED/FAILED per gate.
# No tool installs, no secrets, no external writes (SEC-029).
$ErrorActionPreference = 'Stop'

$root = Split-Path -Parent $PSScriptRoot
if (-not (Test-Path -LiteralPath "$root\OBS-AI-Live-Assistant.slnx")) {
    throw "Solution not found under repository root: $root"
}

Set-Location -LiteralPath $root
$env:MSBUILDDISABLENODEREUSE = '1'

$script:results = New-Object System.Collections.Generic.List[object]

function Invoke-Gate {
    param(
        [Parameter(Mandatory = $true)][string]$Name,
        [Parameter(Mandatory = $true)][string[]]$Arguments
    )

    Write-Host "`n==> GATE: $Name" -ForegroundColor Cyan
    & dotnet @Arguments
    $exit = $LASTEXITCODE
    $status = if ($exit -eq 0) { 'PASSED' } else { 'FAILED' }
    Write-Host "==> $Name : $status (exit $exit)" -ForegroundColor $(if ($exit -eq 0) { 'Green' } else { 'Red' })
    $script:results.Add([pscustomobject]@{ Gate = $Name; ExitCode = $exit; Status = $status })
}

Invoke-Gate 'Restore' @('restore')
Invoke-Gate 'Build (sem restore)' @('build', '--no-restore')
Invoke-Gate 'Testes determinísticos' @('test', '--no-build')
Invoke-Gate 'Format (verify-no-changes)' @('format', '--verify-no-changes', '--no-restore')

Write-Host "`n=== RESUMO DOS QUALITY GATES ===" -ForegroundColor Yellow
$script:results | Format-Table -AutoSize | Out-String | Write-Host

$failed = @($script:results | Where-Object { $_.ExitCode -ne 0 })
if ($failed.Count -gt 0) {
    Write-Host "QUALITY GATES: $($failed.Count) FALHOU(RAM)." -ForegroundColor Red
    exit 1
}

Write-Host 'QUALITY GATES: TODOS PASSED.' -ForegroundColor Green
exit 0