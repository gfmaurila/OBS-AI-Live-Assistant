# Riscos de segurança

| ID | Risco | Prob. | Impacto | Controles / SEC | Estado |
|---|---|---:|---:|---|---|
| SRISK-001 | Vazamento de API keys ou provider credentials | MÉDIA | CRÍTICO | SEC-008 a SEC-013 | OPEN / CONTROLLED |
| SRISK-002 | Vazamento ou reutilização de OAuth tokens | MÉDIA | CRÍTICO | SEC-009, SEC-011, SEC-012, SEC-033 | OPEN / CONTROLLED |
| SRISK-003 | Prompt Injection obter autoridade ou dados | ALTA | CRÍTICO | SEC-003, SEC-004, SEC-006 | OPEN / CONTROLLED |
| SRISK-004 | Abuso de chat consumir quota/custo/recursos | ALTA | ALTO | SEC-002, SEC-003, SEC-014, SEC-015 | OPEN / CONTROLLED |
| SRISK-005 | AI output malicioso ser publicado, narrado ou executado | MÉDIA | CRÍTICO | SEC-005, SEC-006, SEC-031 | OPEN / CONTROLLED |
| SRISK-006 | Provider comprometido ou endpoint falso | MÉDIA | ALTO | SEC-001, SEC-033, SEC-034 | OPEN / CONTROLLED |
| SRISK-007 | DoS por filas, timeouts, retries, TTS ou payloads | ALTA | ALTO | SEC-014 a SEC-019 | OPEN / CONTROLLED |
| SRISK-008 | Falha do assistente interromper ou corromper OBS | MÉDIA | CRÍTICO | SEC-020, SEC-021, SEC-030 | OPEN / CONTROLLED |
| SRISK-009 | Exposição de dados no armazenamento local | MÉDIA | ALTO | SEC-022, SEC-024, SEC-025 | OPEN / CONTROLLED |
| SRISK-010 | Corrupção/adulteração de banco ou configuração | MÉDIA | ALTO | SEC-022, SEC-023 | OPEN / CONTROLLED |
| SRISK-011 | Logging excessivo vazar dados ou esgotar disco | MÉDIA | ALTO | SEC-011, SEC-019, SEC-026 | OPEN / CONTROLLED |
| SRISK-012 | Retenção/envio excessivo violar privacidade | MÉDIA | ALTO | SEC-024, SEC-025, SEC-034 | OPEN / CONTROLLED |
| SRISK-013 | Cliente local/IPC forjar ação ou cruzar sessão | MÉDIA | CRÍTICO | SEC-006, SEC-030, SEC-031 | OPEN / CONTROLLED |
| SRISK-014 | Dependência comprometida contaminar produto | MÉDIA | CRÍTICO | SEC-029, SEC-032 | OPEN / CONTROLLED |
| SRISK-015 | Installer/update adulterado ou privilegiado demais | MÉDIA | CRÍTICO | SEC-007, SEC-027, SEC-028 | OPEN / CONTROLLED |

Nenhum risco é aceito como ausência de controle. `OPEN / CONTROLLED` significa que há requisitos e encaminhamento; implementação e risco residual serão avaliados após Research/Architecture.
