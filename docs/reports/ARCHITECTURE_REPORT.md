# Relatório — Architecture + ADRs

## Identificação

- Projeto: OBS-AI-Live-Assistant
- Task: `architecture-adrs`
- Data: 2026-10-07
- Architecture: **CONCLUÍDA**
- Implementation: **NOT STARTED**

## Fontes

A baseline deriva de Knowledge Quality Gate `PASSED`, 36 RF, 28 RNF, 34 Security Requirements, 27 Research Items, Technical Research Report e 10 ADR Candidates. Foram usados os artefatos canônicos de `docs/project`, `governance`, `knowledge`, `requirements`, `security`, `research`, `reports`, `.ai`, `agent_docs`, `AGENTS.md`, `CLAUDE.md` e `PROJECT_SKILLS.md`.

## Arquitetura escolhida

Assistant Core externo em C#/.NET 10 como Modular Monolith com Ports and Adapters, SOLID e provider boundaries. Integração Hybrid usa plugin C++ mínimo para Dock/lifecycle/capability nativa inevitável, obs-websocket para RPC/events suportados e Named Pipes protegidos/versionados para IPC. SQLite é a persistência local V1; Windows Credential Manager protege secrets discretos e DPAPI user-scope é complementar.

Não foram introduzidos microservices, broker, cloud infrastructure, RAG ou runtime multi-agent.

## Componentes

Chat, AI, TTS, Context/Session/Short-Term Memory, Moderation/Security, Persistence, Configuration, Observability, Provider Management, OBS Integration e Host. Native OBS Component permanece fora da lógica de negócio.

## ADRs

| Estado | Quantidade | ADRs |
|---|---:|---|
| ACCEPTED | 7 | ADR-001, 002, 003, 004, 005, 008, 011 |
| PROPOSED | 3 | ADR-006, 007, 010 |
| DEFERRED | 1 | ADR-009 |
| REJECTED | 0 | — |
| Total | 11 | 10 candidatos reais + ADR de estilo arquitetural |

ADRs `PROPOSED` têm direção suficiente para backlog, mas requerem spike/política antes da implementação correspondente. ADR-009 mantém Persistent Memory fora da baseline V1.

## Diagramas

Oito diagramas textuais foram criados: C4 Context, Container, Component, OBS Integration Flow, Chat/AI/TTS Flow, Security Boundaries, Deployment View e Failure Isolation.

## Trade-offs principais

- Hybrid e processo separado adicionam IPC/lifecycle, mas reduzem o blast radius no OBS.
- Named Pipes exigem protocolo próprio, mas evitam rede e dependências desnecessárias.
- Ports/capabilities exigem contract tests, mas contêm vendor coupling.
- SQLite single-writer limita escrita paralela, adequada à escala local V1.
- Credential Manager reduz portabilidade automática de secrets, preservando proteção por usuário.
- áudio capturado externamente é a preferência de protótipo; source nativa oferece mixer melhor com maior risco in-process.

## Client Decisions e open decisions

Cinco grupos `REQUIRES_CLIENT_DECISION` permanecem: providers concretos/IA local; Persistent Memory/retenção; defaults de uninstall; faixa comercial do OBS; edições Windows 10 versus target .NET. Somados aos spikes técnicos de áudio/installer e valores quantitativos, há **7 grupos de open architecture decisions**. Todos são não bloqueantes para backlog; capabilities opcionais ficam desabilitadas ou parametrizáveis até decisão.

## Security Architecture Review

Resultado: **PASSED**.

Cobertura revalidada: 34/34 SEC, 20/20 assets, 22/22 threats, 8/8 trust boundaries e 15/15 security risks. BYOK permanece fora de SQLite/config/log/prompt; chat e AI output são não confiáveis; IPC exige DACL/auth/version/limits; autorização OBS é deny-by-default e allowlisted; providers são isolados; logging é redacted; installer exige integridade/signing/rollback. Não existe runtime nesta fase, portanto riscos continuam `OPEN / CONTROLLED`, não “eliminados”.

## Architecture Quality Gate

Resultado: **PASSED**.

- [x] Architecture derivada dos Requirements
- [x] Security e Research incorporadas
- [x] 11 ADRs rastreáveis criados
- [x] System Context, Container, Component e Deployment Views criados
- [x] OBS boundary, process isolation, IPC e providers definidos
- [x] persistence, secrets, TTS/audio, installer e compatibility tratados
- [x] observability, failure isolation, SOLID e dependency rules definidos
- [x] test architecture e open decisions explícitas
- [x] nenhuma implementação ou artefato de produto criado
- [x] baseline suficiente para Backlog

## Code Review documental

Resultado: **APROVADO**.

- Critical: 0
- High: 0
- Medium: 0
- Low: 0
- INFO: os três ADRs `PROPOSED` e o `DEFERRED` são limites deliberados, não defeitos.

A revisão verificou consistência arquitetural, coesão, acoplamento, dependency direction, SOLID, segurança, rastreabilidade, evidência, consistência de ADR/diagrama, idioma, links e escopo. A revisão é documental: build/test são `NOT APPLICABLE` porque não existe solução e esta Task proíbe código.

## Secret Scan

Resultado da varredura por formatos de private key, tokens conhecidos e atribuições comuns de credenciais: **Secrets: NONE**. Nenhuma credencial real é necessária; exemplos usam apenas nomes de mecanismos e identificadores.

## Backlog Readiness

**READY.** Nenhuma decisão estrutural obrigatória impede decompor componentes e dependências. Os itens propostos são Tasks de validação delimitadas; decisões do cliente antecedem apenas a habilitação ou release da capacidade afetada.

## Blockers

0.

## Recomendação

Após merge desta Task em `develop`, prosseguir somente mediante autorização para **BACKLOG + DEPENDENCY GRAPH + IMPLEMENTATION TASKS**. Não iniciar implementação automaticamente.
