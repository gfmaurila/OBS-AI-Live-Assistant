# Project Knowledge Map

This initial map identifies knowledge areas and their treatment. It does not convert them into approved requirements or architecture.

| Knowledge area | Current source | Classification | Next validation |
|---|---|---|---|
| Kit IA Dev | Installed repository bootstrap artifacts | ADAPT | Validate relevant dictionary entries at the Knowledge Quality Gate. |
| Current product concept | Client context and `agent_docs/business-rules.md` | ADOPT | Formalize through Requirements. |
| OBS integration | `agent_docs/architecture.md` | REQUIRES_RESEARCH / REQUIRES_ADR | Evaluate native plugin, WebSocket, Dock, audio, and IPC boundaries. |
| BYOK | Client context and security constraints | ADAPT | Define provider and credential requirements; approve secret-storage ADR. |
| TTS | Product context | REQUIRES_RESEARCH | Define provider, routing, moderation, and failure behavior. |
| YouTube Live Chat | Product context | REQUIRES_RESEARCH | Validate official API, OAuth, quotas, and lifecycle. |
| SQLite | Architecture direction | REQUIRES_ADR | Validate library, migrations, concurrency, recovery, retention, and file security. |
| Windows | Target direction | ADOPT | Validate compatibility and security mechanisms. |
| Installer | Product context | REQUIRES_RESEARCH / REQUIRES_ADR | Select technology and lifecycle only after research. |
| Security | `agent_docs/security.md` | ADOPT | Produce Security Requirements and validate threat boundaries. |
| Prompt history | `docs/prompts/` | ADOPT | Continue same-commit traceability. |
| GitFlow | Governance documents | ADOPT | Enforce task gates and protected promotions. |
| Advanced Skills | Official package not found | BLOCKED | Install only from the validated official package. |
| Twitch and other chat providers | Product context | FUTURE | Exclude from V1 unless approved. |
