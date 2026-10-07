# Estrutura física planejada

Status: **APPROVED FOR PLANNING / NOT CREATED**. Esta estrutura orienta backlog e Tasks; não autoriza criar diretórios de produto antes do Client Approval Gate.

```text
OBS-AI-Live-Assistant/
├── src/                               # PLANNED / NOT CREATED
│   ├── ObsAi.Domain/                  # políticas e modelos sem dependências externas
│   ├── ObsAi.Application/             # casos de uso, ports e pipeline
│   ├── ObsAi.Infrastructure/          # SQLite, secret store, logging e OS adapters
│   ├── ObsAi.Providers/               # adapters Chat, AI e TTS
│   ├── ObsAi.ObsIntegration/          # obs-websocket adapter e boundary do bridge
│   ├── ObsAi.Host/                    # processo Assistant Core e composition root
│   └── native/
│       └── ObsAi.ObsPlugin/           # C++ mínimo: Dock, lifecycle, IPC e áudio validado
├── tests/                             # PLANNED / NOT CREATED
│   ├── Unit/
│   ├── Integration/
│   ├── Architecture/
│   ├── Contracts/
│   ├── Security/
│   ├── FailureIsolation/
│   ├── ObsCompatibility/
│   └── Installer/
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

Os nomes podem ser refinados na Execution Plan se o refinamento preservar boundaries e ADRs. Nenhuma pasta acima, solution, projeto C#/C++, banco, migration, installer ou teste executável foi criada nesta Task.
