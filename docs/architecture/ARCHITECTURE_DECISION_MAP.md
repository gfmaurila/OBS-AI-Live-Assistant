# Architecture Decision Map

Este mapa liga requisitos, segurança, pesquisa, decisão e ownership. Critérios completos permanecem nos documentos canônicos de Requirements e Security.

| Requirement | Security | Research | ADR | Componente | Fase/Task futura |
|---|---|---|---|---|---|
| RF-001, RF-022, RF-032; RNF-001, RNF-025 | SEC-006, SEC-007, SEC-020 | RES-001, RES-004, RES-007, RES-008 | ADR-001 | Native Plugin, OBS WebSocket Adapter | OBS Integration Design |
| RF-026, RF-027; RNF-004, RNF-009, RNF-010, RNF-026 | SEC-016, SEC-017, SEC-030 | RES-009, RES-010 | ADR-002 | IPC Bridge | IPC Integration Design |
| RF-001, RF-013, RF-021, RF-025; RNF-001, RNF-008, RNF-016 | SEC-014 a SEC-021 | RES-008, RES-010 | ADR-003 | Core Host, queues, lifecycle | Core Foundation / Failure Tests |
| RF-002, RF-005, RF-016 a RF-019, RF-024, RF-028; RNF-011, RNF-012, RNF-017, RNF-022 | SEC-001, SEC-005, SEC-022, SEC-023, SEC-033, SEC-034 | RES-011, RES-014, RES-015, RES-017, RES-024 | ADR-004 | Provider Ports, Persistence Adapter | Provider/Data Designs |
| RF-018; RNF-003, RNF-006 | SEC-008 a SEC-013 | RES-012, RES-013, RES-014, RES-021 | ADR-005 | Secret Store | Security/Data Design |
| RF-023 a RF-025; RNF-002, RNF-015, RNF-025 | SEC-005, SEC-015 a SEC-021 | RES-005, RES-006, RES-017 | ADR-006 | TTS Pipeline, Audio Output | Audio prototype + TTS Design |
| RF-032 a RF-035; RNF-020, RNF-021, RNF-024, RNF-025 | SEC-007, SEC-027 a SEC-029 | RES-002, RES-003, RES-018 a RES-022, RES-026, RES-027 | ADR-007 | Installer/Updater | Installation Design + spike |
| RF-002 a RF-004, RF-009 a RF-012, RF-031; RNF-004, RNF-013, RNF-014, RNF-027 | SEC-002 a SEC-004, SEC-010, SEC-026 | RES-004, RES-025 | ADR-008 | Dock/UI, Configuration, Observability | UI/Security Designs |
| RF-004, RF-015, RF-029, RF-030; RNF-005, RNF-023 | SEC-024, SEC-025, SEC-031 | RES-011, RES-023, RES-024 | ADR-009 | Session Context, optional Memory Port | Client decision + Data Design |
| RF-032; RNF-019, RNF-024 | SEC-027, SEC-029, SEC-032 | RES-001 a RES-003, RES-022, RES-027 | ADR-010 | Compatibility Gate | Compatibility/Release Plan; matriz validada na TASK-003 |
| RNF-017, RNF-018, RNF-019, RNF-028 | SEC-007, SEC-032 | RES-008, RES-010, RES-015, RES-017 | ADR-011 | todos os módulos | Architecture tests / Execution Plan |

## Decisões do cliente

| Decisão | Tratamento da baseline | Bloqueia backlog? |
|---|---|---|
| AI/TTS providers concretos do V1 | Ports aprovados; adapters escolhidos antes das Tasks específicas | Não |
| Ollama no V1 | `OPTIONAL/FUTURE` | Não |
| Persistent Memory e retenção | desabilitada; ADR-009 `DEFERRED` | Não |
| Dados/logs/secrets no uninstall | opções explícitas; default antes do Installation Design final | Não |
| Faixa OBS e edições Windows 10/.NET 10 | matriz de compatibilidade estabelecida na TASK-003; política comercial a fechar antes do release | Não |

Contagem de grupos `REQUIRES_CLIENT_DECISION`: **5**. Sete grupos de open decisions incluem também spikes técnicos e valores quantitativos; nenhum impede decompor o backlog.
