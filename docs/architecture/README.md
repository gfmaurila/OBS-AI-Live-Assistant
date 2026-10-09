# Arquitetura

A Architecture Baseline do OBS-AI-Live-Assistant foi concluída em 2026-10-07. Ela define como o produto será decomposto, sem criar código, banco, installer ou integração no OBS.

- [Baseline principal](ARCHITECTURE.md)
- [Mapa de decisões](ARCHITECTURE_DECISION_MAP.md)
- [Estrutura física planejada](PROJECT_STRUCTURE.md)
- [Contracts e ports da aplicação](APPLICATION_CONTRACTS.md) — fronteiras vendor-neutral materializadas pela TASK-005
- [Modelo de domínio](DOMAIN_MODEL.md) — sessão, perfil e contexto efêmero materializados pela TASK-006
- [Lifecycle de sessão](SESSION_LIFECYCLE.md) — estados, leases e shutdown ordenado materializados pela TASK-007
- [Filas bounded e backpressure](QUEUES.md) — buffers limitados e políticas de saturação materializados pela TASK-008
- [Validação e normalização de entradas](INPUT_VALIDATION.md) — fronteira fail-closed de entrada materializada pela TASK-009
- [Autorização e políticas de segurança](AUTHORIZATION_POLICIES.md) — deny-by-default, allowlist e autoridade com escopo por sessão materializados pela TASK-015
- [Compatibilidade](compatibility/README.md) — matriz de compatibilidade (ADR-010, validada na TASK-003)
- [ADRs](decisions/README.md)
- [Diagramas](diagrams/README.md)
- [Relatório e Quality Gates](../reports/ARCHITECTURE_REPORT.md)

Status: **CONCLUÍDA / APPLICATION CONTRACTS, DOMAIN MODEL, SESSION LIFECYCLE, BOUNDED QUEUES, INPUT VALIDATION AND AUTHORIZATION POLICIES IMPLEMENTED**. Architecture Quality Gate: **PASSED**. Security Architecture Review: **PASSED**. Backlog Readiness: **READY**. Estratégia de compatibilidade (ADR-010): **ACCEPTED** desde 2026-10-07 (TASK-003). Os contracts e ports da aplicação foram materializados pela TASK-005; o modelo de sessão, perfil e contexto efêmero foi materializado pela TASK-006; o lifecycle e a orquestração de sessão foram materializados pela TASK-007; as filas bounded e o backpressure do pipeline foram materializados pela TASK-008; a validação e normalização de entradas foi materializada pela TASK-009; a fronteira local de autorização e políticas deny-by-default foi materializada pela TASK-015, com integração ainda pendente; sem alterar as decisões da baseline.

Os ADRs `PROPOSED` ou `DEFERRED` delimitam escolhas que dependem de protótipo ou decisão do cliente; não impedem decompor o sistema em backlog e Tasks. Nenhum artefato desta pasta autoriza implementação.
