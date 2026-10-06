# Engineering Governance

## Stage Order

```text
BOOTSTRAP
-> PROJECT DISCOVERY
-> KNOWLEDGE DISCOVERY
-> KNOWLEDGE QUALITY GATE
-> REQUIREMENTS
-> SECURITY REQUIREMENTS
-> ARCHITECTURE
-> DATABASE DESIGN
-> OBS INTEGRATION DESIGN
-> AI PROVIDER DESIGN
-> TTS PROVIDER DESIGN
-> CHAT PROVIDER DESIGN
-> INSTALLATION DESIGN
-> EXECUTION PLAN
-> DEPENDENCY GRAPH
-> BACKLOG
-> TASKS
-> CLIENT APPROVAL GATE
-> IMPLEMENTATION
```

The current bootstrap does not authorize Requirements, Architecture, or implementation.

## Decision States

- `CONFIRMED`
- `ASSUMPTION`
- `REQUIRES_RESEARCH`
- `REQUIRES_ADR`
- `REQUIRES_CLIENT_DECISION`
- `OUT_OF_SCOPE_V1`
- `BLOCKED`

Do not promote an assumption or research item to a confirmed decision without evidence and the required approval.

## Role Flow

```text
Requirements -> Architect -> Tech Lead -> Developer -> Tester -> Reviewer -> Documentation
                         \-> Security
                         \-> DevOps
```

Roles are responsibility boundaries. They do not imply autonomous execution, expanded permissions, or a multi-agent product runtime.

## Global Gates

- Requirements are complete, testable, and scoped.
- Architecture, security, database, OBS integration, provider boundaries, and installer strategy are reviewed.
- Dependencies and tasks are mapped.
- Build, tests, static analysis, security checks, and documentation gates have deterministic evidence when implementation begins.
- No unresolved critical conflict is hidden.
- No implementation begins before explicit client approval.

## Protected Boundaries

- Do not modify `%APPDATA%\obs-studio` during planning or bootstrap.
- Do not copy or alter OBS Truck Live Optimizer artifacts.
- Do not store or log secrets.
- Do not simulate Git, tests, builds, integrations, or approvals.

## Prompt Traceability Rule

Every task that produces a repository change must follow this sequence:

```text
Prompt
-> Execution
-> Validation
-> Archive Prompt
-> Secret Check
-> git add
-> Commit
-> Push
```

Archive the operational prompt under `docs/prompts/history/` before the task
commit. Use the next available sequential `prompt<number>.md` filename, never
overwrite an existing historical prompt, and include the archived prompt in
the same commit and push as the changes it authorized.

Do not archive a secret value. If a prompt contains a credential, password,
token, private key, connection string with a secret, or API key, stop and
arrange safe handling before placing the prompt in Git.

Historical prompts provide traceability; they are not automatically approved
requirements or architectural decisions. Canonical requirements,
Architecture documentation, and ADRs take precedence.

## Task Commit and Push Rule

Every authorized repository change must be committed and pushed on the branch
for its own task. Do not finish a modifying task with intentional changes left
uncommitted or without a push.

A task may finish with a blocked push only when authentication failure, remote
unavailability, a conflict, or another technical error prevents it. Report the
commit status, blocked push, and exact reason. Never push to a different branch
to bypass the failure.
