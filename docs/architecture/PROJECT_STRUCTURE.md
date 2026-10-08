# Estrutura física planejada

Status: **PROJECTS CREATED / APPLICATION CONTRACTS, DOMAIN MODEL AND SESSION LIFECYCLE CREATED / INTERNAL DIRECTORIES PARTIAL**. A `TASK-001` criou a solution e convenções; a `TASK-002`, os seis projetos `ObsAi.*` e regras de referência; a `TASK-003`, o protótipo/matriz de compatibilidade; e a `TASK-004`, os projetos de teste por categoria e o tooling. A `TASK-005` materializou contracts e ports vendor-neutral em `ObsAi.Application`. A `TASK-006` materializou sessão, perfil e contexto efêmero em `ObsAi.Domain`. A `TASK-007` materializou o lifecycle e a orquestração de sessão em `ObsAi.Application.Lifecycle`, manteve Contracts ligado apenas a Application, ampliou Unit para Application e Domain e ativou Integration, FailureIsolation e Security com Application e Domain. Installer permanece scaffold sem `ProjectReference`. ObsCompatibility usa o harness existente. O plugin nativo permanece `PLANNED / NOT CREATED` até a `TASK-024`.

```text
OBS-AI-Live-Assistant/
├── src/                               # CREATED (TASK-001); subprojetos CREATED (TASK-002)
│   ├── ObsAi.Domain/                  # DOMAIN MODEL CREATED (TASK-006) — zero dependências
│   ├── ObsAi.Application/             # CONTRACTS/PORTS AND LIFECYCLE CREATED (TASK-005/TASK-007) — depende de Domain
│   ├── ObsAi.Infrastructure/          # CREATED (TASK-002) — depende de Application
│   ├── ObsAi.Providers/               # CREATED (TASK-002) — depende de Application
│   ├── ObsAi.ObsIntegration/          # CREATED (TASK-002) — depende de Application
│   ├── ObsAi.Host/                    # CREATED (TASK-002) — composition root
│   └── native/
│       └── ObsAi.ObsPlugin/           # PLANNED / NOT CREATED (TASK-024)
├── tests/                             # CREATED (TASK-001); subprojetos PARCIAL (TASK-002/TASK-004)
│   ├── Unit/                          # ACTIVE (TASK-005/006/007) — contracts, domínio e lifecycle
│   ├── Integration/                   # ACTIVE (TASK-007) — fluxos de lifecycle de sessão
│   ├── Architecture/
│   │   └── ObsAi.Architecture.Tests/  # CREATED (TASK-002) — regras de dependência, convenções e arquitetura de testes (TASK-004)
│   ├── Contracts/                     # ACTIVE (TASK-005) — ports; IPC permanece futuro
│   ├── Security/                      # ACTIVE (TASK-006/007) — minimização, descarte de contexto e autoridade de lease
│   ├── FailureIsolation/              # ACTIVE (TASK-007) — shutdown ordenado e falhas de publicação
│   └── Installer/                     # SCAFFOLD CREATED (TASK-004) — ObsAi.Installer.Tests
├── installer/                         # PLANNED / NOT CREATED
├── tooling/                           # CREATED (TASK-004) — quality-gates.ps1 + README.md
├── prototypes/
│   └── compat-sniff/                  # CREATED (TASK-003) — evidência de compatibilidade, fora da solution
├── docs/                              # documentação canônica existente
├── tasks/                             # lifecycle e futuro dependency graph
├── .ai/ .claude/ .codex/ .github/    # governança e tooling existente
└── agent_docs/                        # regras especializadas existentes
```

## Dependency rules

```text
ObsAi.Domain
      ↑
ObsAi.Application
      ↑
Adapters: Infrastructure / Providers / ObsIntegration
      ↑
ObsAi.Host (composition root)

ObsAi.ObsPlugin <-> contrato IPC versionado <-> ObsAi.ObsIntegration
```

- `Domain` não depende de Application, OBS, SQLite, YouTube, AI/TTS vendors, Windows APIs ou rede.
- `Application` depende de Domain e de seus próprios ports; não referencia implementações.
- Adapters implementam ports e encapsulam SDKs, formatos, autenticação e erros externos.
- `Host` compõe dependências e governa lifecycle; não recebe regras de domínio exclusivas.
- O plugin nativo não carrega .NET nem acessa providers, banco, secrets ou configuração de negócio.
- Contratos IPC são mínimos, versionados e livres de tipos de SDK do OBS/vendor.
- Test projects podem depender do alvo necessário; produção nunca depende de testes/tooling.

## Ownership conceitual

| Projeto planejado | Ownership |
|---|---|
| `ObsAi.Domain` | Session, LiveContext, Assistant Profile, políticas, estados e valores normalizados |
| `ObsAi.Application` | pipeline, queues, orchestration, authorization, provider/persistence/OBS ports |
| `ObsAi.Infrastructure` | SQLite, Windows Credential Manager/DPAPI, arquivos, logging e clock |
| `ObsAi.Providers` | YouTube, AI e TTS adapters e contract mapping |
| `ObsAi.ObsIntegration` | obs-websocket, bridge IPC e policies de capability OBS |
| `ObsAi.Host` | processo, startup/shutdown, DI/composition, health e configuração runtime |
| `ObsAi.ObsPlugin` | módulo C++ x64, Dock, frontend events e capability nativa validada |

## Regras de criação

Os nomes podem ser refinados na Execution Plan se o refinamento preservar boundaries e ADRs. A `TASK-001` criou a solution `OBS-AI-Live-Assistant.slnx`, as convenções comuns e as raízes `src/` e `tests/`. A `TASK-002` criou os seis projetos `ObsAi.*`, as referências mínimas do grafo acima, o projeto `ObsAi.Architecture.Tests` e centralizou os packages de teste em `Directory.Packages.props`. A `TASK-003` criou `prototypes/compat-sniff` como ferramenta de evidência **fora da solution**. A `TASK-004` criou os projetos de teste por categoria e o `tooling/quality-gates.ps1`. A `TASK-005` ativou Unit e Contracts para Application. A `TASK-006` adicionou o modelo a Domain e ativou Security para o domínio. A `TASK-007` adicionou o orquestrador de lifecycle a Application, ampliou Unit, Security, Integration e FailureIsolation para Application e Domain, conforme `TestingFoundationTests`. Installer permanece sem referências. Referências entre projetos de `src/` continuam governadas por `AllowedProjectReferences`. Nenhum adapter, banco, migration ou installer foi criado.
