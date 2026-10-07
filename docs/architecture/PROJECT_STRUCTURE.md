# Estrutura física planejada

Status: **PROJECTS CREATED / INTERNAL DIRECTORIES PARTIAL**. A `TASK-001` criou a solution, as convenções comuns (`global.json`, `Directory.Build.props`, `Directory.Packages.props`, `.editorconfig`) e os diretórios raiz `src/` e `tests/`. A `TASK-002` criou os seis projetos `ObsAi.*` em `src/`, as referências mínimas e o projeto determinístico `ObsAi.Architecture.Tests` em `tests/Architecture/`. Os demais agrupamentos de teste (`Unit`, `Integration`, `Contracts`, `Security`, `FailureIsolation`, `ObsCompatibility`, `Installer`) permanecem `PLANNED / NOT CREATED` até a `TASK-004`, e o plugin nativo (`native/ObsAi.ObsPlugin`) até a `TASK-024`. Esta estrutura orienta backlog e Tasks; não autoriza ampliar escopo sem nova Task.

```text
OBS-AI-Live-Assistant/
├── src/                               # CREATED (TASK-001); subprojetos CREATED (TASK-002)
│   ├── ObsAi.Domain/                  # CREATED (TASK-002) — zero dependências
│   ├── ObsAi.Application/             # CREATED (TASK-002) — depende de Domain
│   ├── ObsAi.Infrastructure/          # CREATED (TASK-002) — depende de Application
│   ├── ObsAi.Providers/               # CREATED (TASK-002) — depende de Application
│   ├── ObsAi.ObsIntegration/          # CREATED (TASK-002) — depende de Application
│   ├── ObsAi.Host/                    # CREATED (TASK-002) — composition root
│   └── native/
│       └── ObsAi.ObsPlugin/           # PLANNED / NOT CREATED (TASK-024)
├── tests/                             # CREATED (TASK-001); subprojetos PARCIAL (TASK-002/TASK-004)
│   ├── Unit/                          # PLANNED / NOT CREATED (TASK-004)
│   ├── Integration/                   # PLANNED / NOT CREATED (TASK-004)
│   ├── Architecture/
│   │   └── ObsAi.Architecture.Tests/  # CREATED (TASK-002) — regras de dependência e convenções
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

Os nomes podem ser refinados na Execution Plan se o refinamento preservar boundaries e ADRs. A `TASK-001` criou a solution `OBS-AI-Live-Assistant.slnx`, as convenções comuns e as raízes `src/` e `tests/`. A `TASK-002` criou os seis projetos `ObsAi.*`, as referências mínimas do grafo acima, o projeto `ObsAi.Architecture.Tests` e centralizou os packages de teste em `Directory.Packages.props`. Referências adicionadas (ou removidas) como `ProjectReference` entre projetos de `src/` fazem o gate de arquitetura falhar até serem refletidas na tabela `AllowedProjectReferences` do projeto de testes. Nenhum banco, migration, installer ou teste executável de integração foi criado.
