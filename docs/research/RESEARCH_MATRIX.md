# Matriz de Research

## Perguntas consolidadas

| ID | Pergunta |
|---|---|
| RES-001 | Quais APIs, ABI e toolchain suportam plugin OBS 32.x x64? |
| RES-002 | Como obter e compilar dependências nativas de forma reproduzível? |
| RES-003 | Quais layouts e permissões oficiais regem a instalação do plugin? |
| RES-004 | Qual API e lifecycle oficial suporta um Dock? |
| RES-005 | Como integrar áudio TTS ao mixer do OBS com segurança? |
| RES-006 | Quando usar source, captura de app ou dispositivo virtual? |
| RES-007 | Quais capabilities e limites oferece obs-websocket 5.x? |
| RES-008 | Quais responsabilidades ficam dentro e fora do OBS? |
| RES-009 | Qual IPC local atende C++/.NET, segurança e versionamento? |
| RES-010 | Como conter crash, hang e consumo do Assistant Core? |
| RES-011 | Como operar SQLite com concorrência, backup e recuperação? |
| RES-012 | Credential Manager atende o lifecycle de BYOK? |
| RES-013 | Quando DPAPI direta é adequada? |
| RES-014 | O que o V1 precisa para YouTube Live Chat? |
| RES-015 | Qual contrato mínimo comum aos AI Providers? |
| RES-016 | IA local é viável no baseline V1? |
| RES-017 | Qual contrato mínimo comum aos TTS Providers? |
| RES-018 | Qual tecnologia instala Core e plugin de forma recuperável? |
| RES-019 | Qual estratégia de update é segura e compatível? |
| RES-020 | Como reparar artefatos sem perder dados/secrets? |
| RES-021 | O que uninstall remove ou preserva? |
| RES-022 | Qual faixa OBS deve ser suportada e testada? |
| RES-023 | Memória persistente terá finalidade e retenção aprovadas? |
| RES-024 | Quais termos e políticas valem para providers escolhidos? |
| RES-025 | Como diagnosticar localmente sem expor dados? |
| RES-026 | Como proteger dependências e artefatos distribuídos? |
| RES-027 | Quais edições Windows alvo são suportadas pelo .NET 10? |

## Resultados e impacto

| ID | Requirement / SEC | ADR | Status | Conclusão | Confidence | Fontes | Impacto em Architecture |
|---|---|---|---|---|---|---|---|
| RES-001 | RF-001/032; RNF-019/025 | ADR-001/010 | RESOLVED | Plugin nativo é DLL C/C++ x64 carregada no OBS; usar template oficial e matriz por versão. | HIGH | SRC-001..005 | Boundary nativo mínimo. |
| RES-002 | RNF-019/021 | ADR-007/010 | RESOLVED | Build reproduzível via CMake/VS e dependências versionadas. | HIGH | SRC-002/004 | Pipeline nativo separado. |
| RES-003 | RF-032..035 | ADR-007 | RESOLVED | OBS 32 usa layout ProgramData legado; OBS 33 introduz layout novo. | HIGH | SRC-006/007 | Deployment version-aware. |
| RES-004 | RF-002/003 | ADR-001/008 | RESOLVED | Frontend API oferece Dock QWidget registrado/removido pelo plugin. | HIGH | SRC-003 | UI mínima nativa. |
| RES-005 | RF-023..025 | ADR-006 | PARTIALLY_RESOLVED | Source nativo integra ao mixer; precisa protótipo de threading/latência. | MEDIUM | SRC-008/009 | Port de áudio + spike. |
| RES-006 | RF-023..025 | ADR-006 | PARTIALLY_RESOLVED | Application Audio Capture isola melhor; virtual device aumenta instalação. | MEDIUM | SRC-010/011 | Routing pluggable. |
| RES-007 | RF-001/022 | ADR-001 | RESOLVED | Protocolo 5.x suporta RPC/events/auth; descobrir capacidades com `GetVersion`. | HIGH | SRC-012/013 | Adapter externo. |
| RES-008 | RNF-001/006 | ADR-001/003 | RESOLVED | Hybrid: só Dock/áudio necessários no plugin; lógica, providers e dados fora do OBS. | HIGH | RES-001/004/007 | Core separado. |
| RES-009 | RNF-004/009/010 | ADR-002 | RESOLVED | Named Pipes é baseline, com DACL explícita, rede negada e protocolo versionado. | HIGH | SRC-014..016 | IPC autenticado. |
| RES-010 | RNF-001/008 | ADR-002/003 | RESOLVED | Processo separado contém crash; Job Objects podem limitar lifecycle/recursos. | HIGH | SRC-017 | Supervisor fora do OBS. |
| RES-011 | RF-028..030; SEC-024 | ADR-004/009 | RESOLVED | SQLite atende V1: um writer, migrations, Backup API e versão WAL corrigida. | HIGH | SRC-018..020 | Repository explícito. |
| RES-012 | RF-018; SEC-001..008 | ADR-005 | RESOLVED | Credential Manager é recomendado para secrets discretos por usuário. | HIGH | SRC-021/022 | Secret store port. |
| RES-013 | RF-018; SEC-001..008 | ADR-005 | RESOLVED | DPAPI user-scope serve a blobs; machine-scope não é default seguro. | HIGH | SRC-023 | Opção de ADR. |
| RES-014 | RF-005..009; SEC-009..012 | ADR-004 | RESOLVED | OAuth desktop + PKCE, descoberta do chat, stream/list com polling e quota observada. | HIGH | SRC-024..030 | Chat adapter resiliente. |
| RES-015 | RF-016..021 | ADR-004 | PARTIALLY_RESOLVED | Normalizar streaming, usage opcional, cancellation, timeout, IDs e erros. | MEDIUM | SRC-031..037 | Ports extensíveis. |
| RES-016 | RF-016; OQ-006 | ADR-004 | CLIENT_DECISION | Ollama é opcional; hardware, modelos e suporte impedem baseline V1. | HIGH | SRC-038/039 | Adapter OPTIONAL/FUTURE. |
| RES-017 | RF-023..025 | ADR-004/006 | PARTIALLY_RESOLVED | TTS port cobre voz, formato, streaming opcional, timeout/cancelamento e custo. | HIGH | SRC-040..043 | Normalização de áudio. |
| RES-018 | RF-032..035; SEC-028..030 | ADR-007 | PARTIALLY_RESOLVED | Installer tradicional assinado favorece deployment externo; escolher por ADR/protótipo. | MEDIUM | SRC-044..049 | Bundle version-aware. |
| RES-019 | RF-033; SEC-029 | ADR-007 | RESOLVED | Update assinado, atômico, compatível e recuperável; baseline installer-based. | HIGH | SRC-047/050 | Fora da execução OBS. |
| RES-020 | RF-034 | ADR-007 | RESOLVED | Repair repõe artefatos sem resetar dados/secrets. | MEDIUM | SRC-044..049 | Manifesto de componentes. |
| RES-021 | RF-035; SEC-030 | ADR-005/007 | CLIENT_DECISION | Remover binários/plugin; dados e secrets requerem opção/política explícita. | HIGH | Requirements | Uninstall configurável. |
| RES-022 | RNF-019/024 | ADR-010 | CLIENT_DECISION | Testar mínimo e versões selecionadas; detectar incompatibilidade, sem prometer ABI ampla. Matriz implementada na TASK-003 (`docs/architecture/compatibility/`); faixa comercial a fechar antes do release. | HIGH | SRC-001/007 | Release gate. |
| RES-023 | RF-029/030; SEC-025..027 | ADR-009 | CLIENT_DECISION | Memória persistente exige finalidade, retenção, consentimento e exclusão. | HIGH | Requirements/Security | Baseline temporária. |
| RES-024 | SEC-025..027 | ADR-004/009 | PARTIALLY_RESOLVED | Revisar termos na seleção e periodicamente; YouTube impõe retenção/controle. | MEDIUM | SRC-029/030 | Policy gate. |
| RES-025 | RNF-013/014; SEC-020..023 | ADR-008 | RESOLVED | Logs estruturados, rotação, correlação e redaction central; sem payload/secrets por padrão. | HIGH | SRC-051/052 | Observability port. |
| RES-026 | RNF-020/021; SEC-031..034 | ADR-007 | RESOLVED | Dependências fixadas/auditadas e artefatos verificados/assinados. | HIGH | SRC-047/053/054 | Release gate/SBOM. |
| RES-027 | RNF-019/020; CON-001/002 | ADR-007/010 | CLIENT_DECISION | .NET 10 é LTS até novembro de 2028; no Windows 10, suporte oficial atual limita-se a LTSC/Enterprise. Evidência e política registradas na TASK-003; declarar edições suportadas ou revisar target antes do release. | HIGH | SRC-055/056 | Declarar edições suportadas ou revisar target antes do release. |

Os `SRC-*` estão em [RESEARCH_SOURCES.md](RESEARCH_SOURCES.md). Os critérios de aceite permanecem nos RF/RNF/SEC vinculados.
