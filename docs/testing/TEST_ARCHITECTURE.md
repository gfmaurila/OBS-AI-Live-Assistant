# Arquitetura de Testes e Quality Gates

Status: **APPROVED BY TASK-004**. Fonte canônica da estratégia de testes e dos qualidade gates da V1.

## Objetivo

Estabelecer a fundação determinística de testes e quality gates: categorias, projetos/harness, comandos canônicos e gates por estágio, proporcionais ao risco (RNF-019), rastreáveis (RNF-028), com dependências mínimas e sem secrets (SEC-029) e com controles de segurança testáveis de forma determinística (SEC-032).

## Requisitos e decisões relacionados

- `RNF-019` — Testes e critérios determinísticos, proporcionais ao risco das regras.
- `RNF-028` — Governança e rastreabilidade rígida de decisões e artefatos.
- `SEC-029` — Dependências mínimas e rastreáveis; `SEC-032` — Controles de segurança testáveis.
- `ADR-010` — Estratégia de compatibilidade OBS. `ADR-011` — Modular Monolith, Ports and Adapters e arquitetura de testes obrigatória.
- `TASK-004` — fundação e tooling (este documento); `TASK-049` — consolidação das suites.

## Categoria -> Projeto ou harness

| Categoria | Projeto ou harness | Local canônico | Framework/harness | O que valida (surgirá com o produto) | Gate atual | Aplicável a partir de |
|---|---|---:|---|---|---|---|
| Unit | Projeto | `tests/Unit/ObsAi.Unit.Tests` | xunit (net10.0) | regras de domínio e convenções puras de Domain/Application | EXECUTED para contracts da TASK-005 e domínio da TASK-006 | TASK-005+ |
| Integration | Projeto | `tests/Integration/ObsAi.Integration.Tests` | xunit (net10.0) | pipelines, adapters e fluxos entre boundaries | NOT EXECUTED para comportamento de produto; âncora do scaffold EXECUTED | TASK-007+ |
| Architecture | Projeto existente | `tests/Architecture/ObsAi.Architecture.Tests` | xunit determinístico | dependências, convenções, matriz de compatibilidade, arquitetura de testes, contracts e domínio | EXECUTED (TASK-002/003/004/005/006) | desde TASK-002 |
| Contracts | Projeto | `tests/Contracts/ObsAi.Contract.Tests` | xunit (net10.0) | ports, contratos IPC e envelope versionado | EXECUTED para ports da TASK-005; IPC NOT EXECUTED | TASK-005 (ports) / TASK-022+ (IPC) |
| Security | Projeto | `tests/Security/ObsAi.Security.Tests` | xunit (net10.0) | autorização, redaction, minimização, isolamento de sessão, secrets e políticas | EXECUTED para minimização e isolamento do domínio da TASK-006; demais controles NOT EXECUTED | TASK-006+ conforme requisito da Task |
| FailureIsolation | Projeto | `tests/FailureIsolation/ObsAi.FailureIsolation.Tests` | xunit (net10.0) | falhas, timeout, retry, filas bounded, IPC e encerramento | NOT EXECUTED para comportamento de produto; âncora do scaffold EXECUTED | TASK-008/013/026+ |
| ObsCompatibility | Harness (sem projeto novo) | `prototypes/compat-sniff` + `CompatibilityMatrixTests` + `docs/architecture/compatibility/` | sonda read-only + xunit determinístico | matriz OBS 32.x x64, Windows 10/11 x64, .NET 10, fail-closed | PARTIAL — smoke (TASK-003); suite formal NOT CREATED (TASK-051) | desde TASK-003; formal TASK-051 |
| Installer | Projeto | `tests/Installer/ObsAi.Installer.Tests` | xunit (net10.0) | detecção de ambiente, upgrade/repair/uninstall/rollback | NOT EXECUTED para comportamento de produto; âncora do scaffold EXECUTED | TASK-046+ |
| Regression/E2E | Consolidação | suites existentes + `TASK-049`/`TASK-050` | xunit | regressão e integração end-to-end | NOT CREATED | TASK-049/TASK-050 |

## Princípios

1. Testes determinísticos e proporcionais ao risco (`RNF-019`); nada de suites vazias sem função e nada de testes fictícios que apenas "passam".
2. Sem teste fictício: uma suíte é criada junto com o comportamento testável que a justifica, referenciando esta carta.
3. Entrada de chat, saída de providers, arquivos e respostas externas são não confiáveis; testes usam dados sintéticos e nenhum secret real (`SEC-029`, `SEC-032`).
4. Testes de produção nunca dependem de produção; produção nunca referencia testes.
5. Gates não aplicáveis permanecem identificados (`NOT EXECUTED` / `NOT CREATED` / `NOT APPLICABLE`) e nunca são reportados como aprovados sem comandos e artefatos reais.
6. Nenhuma suíte modifica OBS; acesso quando aplicável é somente leitura, fora do `%APPDATA%\obs-studio`.

## Decisões

- **ObsCompatibility** usa o harness existente (sonda read-only `prototypes\compat-sniff` + testes determinísticos de matriz) em vez de um projeto vazio; a suite formal executável permanece em `TASK-051`.
- **Scaffolds da TASK-004** não recebem `ProjectReference` até a Task correspondente autorizar. A TASK-005 ativou Unit e Contracts para `ObsAi.Application`; a TASK-006 adiciona `ObsAi.Domain` à suite Unit e ativa Security exclusivamente para `ObsAi.Domain`. Integration, FailureIsolation e Installer continuam sem referência. Um teste determinístico valida esses estados.
- **Comandos canônicos** são os do `AGENTS.md` e do `tooling\quality-gates.ps1`; nenhum gate é aprovado sem execução real com evidência.

## Comandos canônicos para gates

| Comando | Gate | Estado ao final da TASK-004 |
|---|---|---:|
| `dotnet restore` | Restore | EXECUTADO |
| `dotnet build --no-restore` | Build | EXECUTADO |
| `dotnet test --no-build` | Testes determinísticos | EXECUTADO (ver QUALITY_GATES) |
| `dotnet format --verify-no-changes --no-restore` | Format (solucao) | EXECUTADO |
| `dotnet format --verify-no-changes --no-restore prototypes\compat-sniff\CompatibilityProbe.csproj` | Format (prototipo) | EXECUTADO |
| `dotnet run --project prototypes\compat-sniff\CompatibilityProbe.csproj` | ObsCompatibility smoke | EXECUTADO (TASK-003) |
| `powershell -ExecutionPolicy Bypass -File tooling\quality-gates.ps1` | Runner de conveniência | DOCUMENTADO |

## Gate de arquitetura de testes

`TestingFoundationTests` (em `ObsAi.Architecture.Tests`) valida esta carta e o grafo de projetos de teste de forma determinística: categorias declaradas, localizações, gate status válidos, solução registrada, referências somente nas suites ativadas, ausência de análises triviais, comandos e tooling documentados.

## Rastreabilidade

`RNF-019`, `RNF-028`, `SEC-029`, `SEC-032`, `ADR-010`, `ADR-011`; `TASK-004` (fundação), `TASK-049` (consolidação). Este documento é a fonte canônica da "arquitetura de testes e quality gates" e prevalece sobre documentação anterior que o contradiga.
