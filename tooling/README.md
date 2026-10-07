# tooling — Quality Gates runner

Scripts de validação determinísticos do projeto. Estabelecidos na `TASK-004` e referenciados pela [Arquitetura de Testes](../docs/testing/TEST_ARCHITECTURE.md).

## quality-gates.ps1

Executa os comandos canônicos da solução em sequência e reporta status por gate. Requer `dotnet` 10 (`global.json`) e PowerShell 5.1+. Não instala ferramentas (`SEC-029`). Encerra com código de saída `1` se algum gate falhar.

```powershell
powershell -ExecutionPolicy Bypass -File tooling\quality-gates.ps1
```

Gates executados:

| # | Gate | Comando |
|---:|---|---|
| 1 | Restore | `dotnet restore` |
| 2 | Build | `dotnet build --no-restore` |
| 3 | Testes determinísticos | `dotnet test --no-build` |
| 4 | Format (solucao) | `dotnet format --verify-no-changes --no-restore` |

O gate de Format do protótipo (`CompatibilityProbe.csproj`) e o smoke de compatibilidade são executados pelas tarefas que tocam o protótipo (ver `docs/testing/TEST_ARCHITECTURE.md`).

O script usa `MSBUILDDISABLENODEREUSE=1` para evitar locks de MSBuild em pastas sincronizadas (ex.: Google Drive). Não contém secrets e nunca autentica; interaja via API/CLI pelo fluxo de cada Task.