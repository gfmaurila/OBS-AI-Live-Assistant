# Project Structure

## Status

This document represents the current documentation structure and a conceptual product layout only. It is not a final architecture specification.

```text
OBS-AI-Live-Assistant/
|-- .ai/                 # AI roles, knowledge pointers, and enforceable governance
|-- .claude/             # Claude rules, hooks documentation, and project Skills
|-- .codex/              # Codex project Skills
|-- .github/             # Tool guidance and future CI configuration
|-- agent_docs/          # Specialized engineering constraints
|-- docs/                # Canonical project documentation
|-- tasks/               # Task lifecycle and future dependency graph
|-- AGENTS.md             # Multi-tool engineering contract
|-- PROJECT_SKILLS.md     # Installed Skills registry
`-- future product structure
    |-- assistant-core/   # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
    |-- obs-integration/  # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
    |-- tests/            # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
    |-- installer/        # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
    `-- tooling/          # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
```

## Current Physical Boundary

Only governance, agent, Skill, prompt-history, task-state, and documentation artifacts exist. There is no `src/`, solution, native project, database, installer, or OBS Dock.

## Planning Constraints

- `assistant-core/` is a conceptual label for the external application responsibilities; its final name and physical layout require Architecture approval.
- `obs-integration/` does not imply that a native plugin has been selected. The responsibility split among a native plugin, OBS WebSocket, and IPC is `REQUIRES_RESEARCH`, `REQUIRES_ADR`, and `REQUIRES_CLIENT_DECISION` where applicable.
- Product directories must not be created until the mandated workflow reaches the relevant approved task.
- No structure from the CMS/Azure reference project is a product requirement for this repository.
