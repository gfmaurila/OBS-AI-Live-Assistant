# Knowledge Decisions

No architectural decision is approved by this baseline. This register records only how current sources will be treated.

| Source or topic | Classification | Rationale |
|---|---|---|
| Client-provided current product context | ADOPT / ADAPT | Use as bounded planning input and preserve explicit approval markers. |
| `AGENTS.md` and `agent_docs/` | ADOPT | Current repository constraints and workflow rules. |
| Kit IA Dev base | ADAPT | Base agents and Skills are installed; relevant knowledge still requires validation. |
| CMS/Azure reference repository | REFERENCE | Reuse documentation organization and traceability patterns only. |
| Advanced Skills | BLOCKED | Official package is unavailable; no substitute may be invented. |
| OBS integration allocation | REQUIRES_RESEARCH / REQUIRES_ADR | Native plugin, WebSocket, IPC, Dock, and audio responsibilities remain unresolved. |
| Windows secret storage | REQUIRES_RESEARCH / REQUIRES_ADR | Credential Manager and DPAPI are candidates, not decisions. |
| Twitch and additional chat providers | FUTURE | Not part of V1 unless separately approved. |

Approved Requirements and ADRs will be added only during their authorized processes.
