# Documentação de Testing

A fundação de testes e quality gates foi estabelecida pela `TASK-004`. A fonte canônica é a [Arquitetura de Testes e Quality Gates](TEST_ARCHITECTURE.md), que define categorias, projeto/harness, comandos e gates por estágio.

## Estado atual

| Categoria | Suite | Gate atual |
|---|---|---|
| Architecture | `tests/Architecture/ObsAi.Architecture.Tests` | EXECUTADO (testes determinísticos; inclui `TestingFoundationTests`) |
| Unit | `tests/Unit/ObsAi.Unit.Tests` | EXECUTED — contracts, domínio, lifecycle, filas, validação, resiliência, configuração e autorização das TASK-005 a TASK-009 e TASK-013 a TASK-015 |
| Integration | `tests/Integration/ObsAi.Integration.Tests` | EXECUTED — lifecycle TASK-007, filas TASK-008 e composição queue/resilience TASK-013; adapters/providers futuros |
| Contracts | `tests/Contracts/ObsAi.Contract.Tests` | EXECUTED — ports TASK-005 e erros/políticas de resiliência TASK-013; IPC NOT EXECUTED até TASK-022+ |
| Security | `tests/Security/ObsAi.Security.Tests` | EXECUTED — controles TASK-006 a TASK-009, retry/cancellation seguro TASK-013, configuração não secreta TASK-014 e autorização deny-by-default TASK-015 |
| FailureIsolation | `tests/FailureIsolation/ObsAi.FailureIsolation.Tests` | EXECUTED — shutdown/publicação TASK-007, filas TASK-008 e retry/falhas normalizadas TASK-013; IPC TASK-026+ |
| Installer | `tests/Installer/ObsAi.Installer.Tests` (scaffold) | Âncora EXECUTED; comportamento de produto NOT EXECUTED — TASK-046+ |
| ObsCompatibility | harness `prototypes/compat-sniff` + `CompatibilityMatrixTests` | PARTIAL — smoke (TASK-003); formal NOT CREATED (TASK-051) |
| Regression/E2E | consolidação em TASK-049/TASK-050 | NOT CREATED |

Os projetos são xunit (net10.0) e não contêm testes fictícios. A TASK-005 ativou Unit e Contracts para `ObsAi.Application`; a TASK-006 ampliou Unit para `ObsAi.Domain` e ativou Security para o domínio; a TASK-007 ampliou Unit, Security, Integration e FailureIsolation; a TASK-008 cobriu filas bounded; a TASK-009 cobriu validação de entradas; a TASK-013 adicionou evidência Unit, Contract, FailureIsolation, Security, Integration e Architecture para resiliência com tempo controlado; a TASK-014 adicionou testes Unit, Security e Architecture para configuração; a TASK-015 adicionou testes Unit, Security e Architecture para autorização fail-closed, allowlist, isolamento de sessão, falhas internas e separação entre dados e autoridade. Installer permanece como scaffold sem `ProjectReference` até sua Task autorizadora (verificado por `TestingFoundationTests`).

## Comandos canônicos

| Comando | Gate |
|---|---|
| `dotnet restore` | Restore |
| `dotnet build --no-restore` | Build |
| `dotnet test --no-build` | Testes determinísticos |
| `dotnet format --verify-no-changes --no-restore` | Format (solucao) |
| `dotnet format --verify-no-changes --no-restore prototypes\compat-sniff\CompatibilityProbe.csproj` | Format (prototipo) |
| `dotnet run --project prototypes\compat-sniff\CompatibilityProbe.csproj` | ObsCompatibility smoke |
| `powershell -ExecutionPolicy Bypass -File tooling\quality-gates.ps1` | Runner determinístico de conveniência |

Gates não executados permanecem identificados (`NOT EXECUTED` / `NOT CREATED` / `NOT APPLICABLE`) e nunca são aprovados sem comandos e artefatos reais. Detalhes por categoria e rastreabilidade no `TEST_ARCHITECTURE.md`.
