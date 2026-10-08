# Backlog executável da V1

Status: **BACKLOG QUALITY GATE PASSED**. Implementation Readiness: **READY**.

| ID | Epic | Título | Prioridade | Complexidade | Risco | Dependências | Status | Wave | Paralelismo |
|---|---|---|---|---|---|---|---|---:|---|
| TASK-001 | EPIC-01 | Criar a fundação da solution e do tooling | P0 | M | MEDIUM | — | DONE | 1 | SEQUENTIAL |
| TASK-002 | EPIC-01 | Materializar projetos e regras de dependência | P0 | M | MEDIUM | TASK-001 | DONE | 1 | SEQUENTIAL |
| TASK-003 | EPIC-12 | Validar a estratégia de compatibilidade | P0 | L | HIGH | TASK-001, TASK-002 | DONE | 1 | PARALLEL SAFE após TASK-002 |
| TASK-004 | EPIC-13 | Estabelecer a arquitetura de testes e os quality gates | P0 | M | MEDIUM | TASK-001, TASK-002 | DONE | 1 | PARALLEL SAFE após TASK-002 |
| TASK-005 | EPIC-02 | Definir contracts e ports da aplicação | P0 | L | HIGH | TASK-002, TASK-004 | DONE | 2 | SEQUENTIAL |
| TASK-006 | EPIC-02 | Implementar o modelo de domínio de sessão, perfil e contexto | P0 | L | MEDIUM | TASK-002, TASK-004 | BACKLOG | 2 | PARALLEL SAFE com TASK-005 |
| TASK-007 | EPIC-02 | Implementar lifecycle e orquestração de sessão | P0 | L | HIGH | TASK-005, TASK-006 | BACKLOG | 2 | SEQUENTIAL |
| TASK-008 | EPIC-02 | Implementar filas bounded e backpressure | P0 | L | HIGH | TASK-005, TASK-007 | BACKLOG | 2 | SEQUENTIAL |
| TASK-009 | EPIC-02 | Implementar validação e normalização de entradas | P0 | M | HIGH | TASK-005, TASK-006 | BACKLOG | 2 | PARALLEL SAFE com TASK-007 |
| TASK-010 | EPIC-02 | Implementar triggers, moderação, blocklist e rate limiting | P0 | L | HIGH | TASK-008, TASK-009, TASK-014, TASK-015 | BACKLOG | 2 | SEQUENTIAL |
| TASK-011 | EPIC-02 | Implementar Context Builder e Short-Term Memory | P0 | L | HIGH | TASK-006, TASK-009, TASK-014 | BACKLOG | 2 | PARALLEL SAFE com TASK-010 após dependências |
| TASK-012 | EPIC-02 | Implementar validação e coordenação de respostas | P0 | L | HIGH | TASK-008, TASK-010, TASK-011, TASK-016 | BACKLOG | 2 | SEQUENTIAL |
| TASK-013 | EPIC-02 | Implementar timeout, cancellation, retry e erros normalizados | P0 | L | HIGH | TASK-005, TASK-007, TASK-008 | BACKLOG | 2 | PARALLEL SAFE com TASK-009 |
| TASK-014 | EPIC-04 | Implementar modelo e validação de configuração | P0 | M | MEDIUM | TASK-005, TASK-006 | BACKLOG | 2 | PARALLEL SAFE com TASK-007 |
| TASK-015 | EPIC-03 | Implementar autorização e políticas de segurança | P0 | L | HIGH | TASK-005, TASK-009 | BACKLOG | 2 | PARALLEL SAFE com TASK-014 |
| TASK-016 | EPIC-03 | Implementar redaction e erros seguros | P0 | M | HIGH | TASK-005, TASK-014 | BACKLOG | 2 | PARALLEL SAFE com TASK-015 |
| TASK-017 | EPIC-03 | Implementar Secure Credential Store do Windows | P0 | L | HIGH | TASK-005, TASK-016 | BACKLOG | 3 | PARALLEL SAFE com persistence após TASK-016 |
| TASK-018 | EPIC-03 | Implementar lifecycle BYOK e OAuth comum | P0 | L | HIGH | TASK-014, TASK-017 | BACKLOG | 3 | SEQUENTIAL após TASK-017 |
| TASK-019 | EPIC-04 | Definir schema e migrations SQLite | P0 | L | HIGH | TASK-002, TASK-004, TASK-006, TASK-014 | BACKLOG | 3 | PARALLEL SAFE com TASK-017 |
| TASK-020 | EPIC-04 | Implementar repositories e estratégia single-writer | P0 | L | HIGH | TASK-005, TASK-019 | BACKLOG | 3 | SEQUENTIAL |
| TASK-021 | EPIC-04 | Implementar backup, recovery, integridade e retenção | P1 | L | HIGH | TASK-019, TASK-020 | BACKLOG | 3 | SEQUENTIAL |
| TASK-022 | EPIC-05 | Definir protocolo e envelope IPC versionado | P0 | L | HIGH | TASK-005, TASK-013, TASK-015 | BACKLOG | 3 | PARALLEL SAFE com persistence |
| TASK-023 | EPIC-05 | Implementar o endpoint Named Pipes no .NET | P0 | L | HIGH | TASK-022 | BACKLOG | 4 | PARALLEL SAFE com TASK-024 |
| TASK-024 | EPIC-06 | Criar skeleton e lifecycle do plugin OBS | P0 | XL | HIGH | TASK-002, TASK-003, TASK-004 | BACKLOG | 4 | PARALLEL SAFE com TASK-023 |
| TASK-025 | EPIC-05 | Implementar o endpoint Named Pipes no C++ | P0 | XL | HIGH | TASK-022, TASK-024 | BACKLOG | 4 | SEQUENTIAL após TASK-024 |
| TASK-026 | EPIC-05 | Validar segurança e falhas do IPC | P0 | L | HIGH | TASK-023, TASK-025 | BACKLOG | 4 | SEQUENTIAL |
| TASK-027 | EPIC-06 | Implementar adapter obs-websocket | P1 | L | HIGH | TASK-005, TASK-013, TASK-017 | BACKLOG | 4 | PARALLEL SAFE com IPC após ports |
| TASK-028 | EPIC-06 | Implementar autorização de capabilities OBS | P0 | L | HIGH | TASK-015, TASK-026, TASK-027 | BACKLOG | 4 | SEQUENTIAL |
| TASK-029 | EPIC-06 | Validar failure isolation e lifecycle OBS/Core | P0 | XL | HIGH | TASK-007, TASK-026, TASK-027, TASK-028 | BACKLOG | 4 | SEQUENTIAL |
| TASK-030 | EPIC-07 | Implementar OAuth e descoberta do YouTube Live Chat | P0 | L | HIGH | TASK-005, TASK-018 | BACKLOG | 5 | PARALLEL SAFE com TASK-033 |
| TASK-031 | EPIC-07 | Implementar recepção, quota e reconexão do YouTube | P0 | L | HIGH | TASK-009, TASK-013, TASK-030 | BACKLOG | 5 | SEQUENTIAL |
| TASK-032 | EPIC-07 | Integrar pipeline de chat, acionamento manual e saída textual | P0 | L | HIGH | TASK-010, TASK-012, TASK-029, TASK-031 | BACKLOG | 5 | SEQUENTIAL |
| TASK-033 | EPIC-08 | Selecionar AI Provider V1 e validar privacidade | P0 | M | HIGH | TASK-003, TASK-005, TASK-018 | BACKLOG | 5 | PARALLEL SAFE com chat foundation |
| TASK-034 | EPIC-08 | Implementar o primeiro AI Provider Adapter | P0 | XL | HIGH | TASK-011, TASK-013, TASK-017, TASK-033 | BACKLOG | 5 | SEQUENTIAL após seleção |
| TASK-035 | EPIC-08 | Validar conformance, streaming e usage de AI | P1 | M | MEDIUM | TASK-012, TASK-034 | BACKLOG | 5 | SEQUENTIAL |
| TASK-036 | EPIC-09 | Selecionar TTS Provider V1 e validar privacidade | P1 | M | HIGH | TASK-003, TASK-005, TASK-018 | BACKLOG | 5 | PARALLEL SAFE com TASK-033 |
| TASK-037 | EPIC-09 | Implementar TTS Provider Adapter | P1 | XL | HIGH | TASK-008, TASK-012, TASK-013, TASK-017, TASK-036, TASK-038 | BACKLOG | 6 | PARALLEL SAFE com UI foundation |
| TASK-038 | EPIC-09 | Executar protótipo de roteamento TTS e validar ADR-006 | P0 | XL | HIGH | TASK-024, TASK-029 | BACKLOG | 6 | SEQUENTIAL |
| TASK-039 | EPIC-09 | Implementar a saída de áudio OBS aprovada | P1 | XL | HIGH | TASK-037, TASK-038 | BACKLOG | 6 | SEQUENTIAL |
| TASK-040 | EPIC-10 | Implementar skeleton do Dock e bridge de UI | P1 | XL | HIGH | TASK-024, TASK-026, TASK-029 | BACKLOG | 6 | PARALLEL SAFE com TASK-037 |
| TASK-041 | EPIC-10 | Implementar configurações, perfis e acionamento manual na UI | P1 | L | MEDIUM | TASK-014, TASK-018, TASK-020, TASK-032, TASK-040 | BACKLOG | 6 | SEQUENTIAL |
| TASK-042 | EPIC-10 | Implementar status operacional, diagnóstico e acessibilidade | P1 | M | MEDIUM | TASK-040, TASK-043, TASK-044 | BACKLOG | 6 | SEQUENTIAL |
| TASK-043 | EPIC-11 | Implementar logging estruturado, rotação e redaction | P0 | L | HIGH | TASK-005, TASK-016 | BACKLOG | 3 | PARALLEL SAFE com persistence/IPC |
| TASK-044 | EPIC-11 | Implementar health, diagnósticos e métricas locais | P1 | L | MEDIUM | TASK-008, TASK-013, TASK-029, TASK-043 | BACKLOG | 5 | PARALLEL SAFE com providers |
| TASK-045 | EPIC-12 | Executar spike de installer e validar ADR-007 | P0 | XL | HIGH | TASK-003, TASK-024, TASK-029 | BACKLOG | 7 | PARALLEL SAFE após artefatos mínimos |
| TASK-046 | EPIC-12 | Implementar installer foundation e detecção de ambiente | P0 | XL | HIGH | TASK-003, TASK-045 | BACKLOG | 7 | SEQUENTIAL |
| TASK-047 | EPIC-12 | Implementar upgrade, repair, uninstall e rollback | P0 | XL | HIGH | TASK-021, TASK-046 | BACKLOG | 7 | SEQUENTIAL |
| TASK-048 | EPIC-12 | Preparar signing e controles de supply chain | P1 | L | HIGH | TASK-004, TASK-045, TASK-046 | BACKLOG | 7 | PARALLEL SAFE com TASK-047 |
| TASK-049 | EPIC-13 | Consolidar suites de contrato, arquitetura e regressão | P1 | L | MEDIUM | TASK-026, TASK-032, TASK-035, TASK-037, TASK-039, TASK-042, TASK-047 | BACKLOG | 8 | SEQUENTIAL para consolidação |
| TASK-050 | EPIC-14 | Executar integração end-to-end e hardening de falhas | P0 | XL | HIGH | TASK-029, TASK-032, TASK-035, TASK-039, TASK-042, TASK-044, TASK-047, TASK-049 | BACKLOG | 8 | SEQUENTIAL |
| TASK-051 | EPIC-14 | Validar desempenho, recursos e matriz final | P1 | L | HIGH | TASK-003, TASK-044, TASK-050 | BACKLOG | 8 | SEQUENTIAL |
| TASK-052 | EPIC-15 | Preparar a V1 para o Release Quality Gate | P0 | L | HIGH | TASK-048, TASK-050, TASK-051 | BACKLOG | 9 | SEQUENTIAL |

Os arquivos individuais nas pastas de estado são a autoridade operacional. `TASK-001` a `TASK-005` estão `DONE`. Nenhuma Task está formalmente `READY`; `TASK-006` e `TASK-024` permanecem candidatas a READY, sem execução. As demais Tasks seguem em `BACKLOG` conforme o grafo; nenhuma Task dependente foi executada pela TASK-005.
