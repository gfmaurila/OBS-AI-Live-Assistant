# Knowledge Conflicts

| Sources | Potential conflict | Current handling | Status |
|---|---|---|---|
| CMS/Azure reference vs. OBS-AI-Live-Assistant | Reference contains Azure, CMS, multi-tenant, React, cloud databases, messaging, RAG, workers, and platform-specific architecture. | Use organization, navigation, governance, gates, reports, and task-state patterns only. | RESOLVED FOR BASELINE |
| Product direction vs. unapproved design | Current context names .NET, SQLite, provider boundaries, and possible OBS mechanisms. | Label direction and assumptions; require research and ADRs for unresolved choices. | OPEN / CONTROLLED |
| Expected runtime-data path vs. installation uncertainty | `%APPDATA%\obs-studio\obs-ai-live-assistant` is anticipated, but official plugin and data paths require research. | Treat the runtime-data path as an assumption and do not create it. | REQUIRES_RESEARCH |
| Base Skills vs. Advanced Skills | Ten base Skills exist; the official package for eight Advanced Skills is absent. | Keep base Skills unchanged and report Advanced Skills as blocked. | BLOCKED |

No conflict was resolved by copying functional architecture from the reference project.
