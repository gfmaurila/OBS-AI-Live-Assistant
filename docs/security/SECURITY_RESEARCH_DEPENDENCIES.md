# Dependências de Research de segurança

Nenhuma pesquisa externa foi executada nesta Task. Os itens abaixo especializam o backlog canônico em [`docs/research/RESEARCH_BACKLOG.md`](../research/RESEARCH_BACKLOG.md).

| ID | Research de segurança | Backlog canônico | SEC | Blocking? | Impacto em Architecture |
|---|---|---|---|---|---|
| SRES-001 | Credential Manager versus DPAPI e alternativas | RES-012, RES-013 | SEC-008 a SEC-013 | SIM — secret design | Define escopo, proteção, recuperação e migração. |
| SRES-002 | Requisitos OAuth do YouTube/provider | RES-014 | SEC-012, SEC-033 | SIM — integração YouTube | Define fluxo, scopes, refresh, revogação e quotas. |
| SRES-003 | Lifecycle de secrets em install/upgrade/repair/uninstall | RES-012, RES-013, RES-018 a RES-021 | SEC-009, SEC-013, SEC-028 | SIM — lifecycle | Define preservação, remoção e recovery. |
| SRES-004 | Proteção do banco e arquivos locais | RES-011 | SEC-022, SEC-023 | SIM — Data Design | Define permissões, integridade, backup e recovery. |
| SRES-005 | Redaction e validação de logging na stack escolhida | Refinamento futuro do backlog | SEC-011, SEC-026 | SIM — observabilidade | Define filtros, campos permitidos e testes sentinela. |
| SRES-006 | Assinatura, origem e privilégio do installer | RES-003, RES-018 | SEC-027 | SIM — Installation Design | Define trust, signing e escopo de instalação. |
| SRES-007 | Integridade e rollback de update | RES-019 | SEC-028 | SIM — Upgrade Design | Define verificação, versionamento e recuperação. |
| SRES-008 | Trust boundary da integração OBS | RES-007, RES-008 | SEC-006, SEC-020 | SIM — Architecture | Delimita capacidades e autoridade dentro/fora do OBS. |
| SRES-009 | Autenticação/autorização de IPC, se aplicável | RES-009 | SEC-030 | SIM — se houver IPC | Define identidade, contrato e proteção contra replay/spoofing. |
| SRES-010 | Isolamento, falha segura e contenção | RES-010 | SEC-015 a SEC-021 | SIM — Architecture | Define limites de processo, restart e backpressure. |
| SRES-011 | Privacidade, retenção e termos de providers | RES-023, RES-024 | SEC-024, SEC-025, SEC-034 | SIM — seleção/provider | Define dados permitidos e lifecycle por provider. |
| SRES-012 | Integridade e vulnerabilidades da supply chain | RES-018, RES-019; refinamento futuro | SEC-029, SEC-032 | SIM — tooling/release | Define inventário, verificação e resposta a findings. |

Os itens bloqueiam a decisão/implementação correspondente, não o fechamento documental desta fase.
