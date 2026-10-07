# Estrutura física planejada

Status: **FOUNDATION CREATED / INTERNAL DIRECTORIES PENDING**. A `TASK-001` criou a solution, as convenções comuns (`global.json`, `Directory.Build.props`, `Directory.Packages.props`, `.editorconfig`) e os diretórios raiz `src/` e `tests/`. Os diretórios internos abaixo permanecem `PLANNED / NOT CREATED` até as Tasks correspondentes (`TASK-002` para projetos, `TASK-004` para a arquitetura de testes). Esta estrutura orienta backlog e Tasks; não autoriza criar diretórios de produto antes do Client Approval Gate.

```text
OBS-AI-Live-Assistant/
├── src/                               # CREATED (raiz); subprojetos PLANNED / NOT CREATED
│   ├── ObsAi.Domain/                  # PLANNED / NOT CREATED (TASK-002)
│   ├── ObsAi.Application/             # PLANNED / NOT CREATED (TASK-002)
│   ├── ObsAi.Infrastructure/          # PLANNED / NOT CREATED (TASK-002)
│   ├── ObsAi.Providers/               # PLANNED / NOT CREATED (TASK-002)
│   ├── ObsAi.ObsIntegration/          # PLANNED / NOT CREATED (TASK-002)
│   ├── ObsAi.Host/                    # PLANNED / NOT CREATED (TASK-002)
│   └── native/
│       └── ObsAi.ObsPlugin/           # PLANNED / NOT CREATED (TASK-024)
├── tests/                             # CREATED (raiz); subprojetos PLANNED / NOT CREATED
│   ├── Unit/                          # PLANNED / NOT CREATED (TASK-004)
│   ├── Integration/                   # PLANNED / NOT CREATED (TASK-004)
│   ├── Architecture/                  # PLANNED / NOT CREATED (TASK-004)
│   ├── Contracts/                     # PLANNED / NOT CREATED (TASK-004)
│   ├── Security/                      # PLANNED / NOT CREATED (TASK-004)
│   ├── FailureIsolation/              # PLANNED / NOT CREATED (TASK-004)
│   ├── ObsCompatibility/              # PLANNED / NOT CREATED (TASK-004)
│   └── Installer/                     # PLANNED / NOT CREATED (TASK-004)
├── installer/                         # PLANNED / NOT CREATED
├── tooling/                           # PLANNED / NOT CREATED
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

Os nomes podem ser refinados na Execution Plan se o refinamento preservar boundaries e ADRs. Nesta Task foram criados apenas a solution `OBS-AI-Live-Assistant.slnx`, as convenções comuns e as raízes `src/` e `tests/`; nenhum projeto C#/C++, banco, migration, installer ou teste executável foi criado.
