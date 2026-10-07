# OBS-AI-Live-Assistant Engineering Contract

## Project Context

OBS-AI-Live-Assistant is a new, independent Windows application that enables viewers to interact with an AI assistant during OBS Studio live streams. The product will receive chat or manual requests and return configurable text and voice responses without making OBS stability depend on AI, chat, TTS, database, or network availability.

This repository must never modify or absorb files from the separate OBS Truck Live Optimizer project under `%APPDATA%\obs-studio`. Existing OBS documents may be read only as architectural references.

## Approved Direction

- Primary runtime and language: C# / .NET 10.
- Native integration: C/C++ only when a validated OBS capability requires it.
- Data: relational SQL with SQLite as the V1 direction.
- Architecture direction: modular monolith with Ports and Adapters and provider boundaries.
- Target: OBS Studio 32.x x64 on Windows 10 and Windows 11 x64.
- AI and TTS: multi-provider, BYOK, with no mandatory vendor coupling.
- Chat V1: YouTube Live Chat.
- Future chat providers: Twitch and other providers; not part of V1 unless approved.

These are constraints and directions, not permission to resolve open ADRs or begin implementation.

## Mandatory Workflow

Follow this sequence and stop at every client approval gate:

```text
Bootstrap
-> Project Discovery
-> Knowledge Discovery
-> Knowledge Quality Gate
-> Requirements
-> Security Requirements
-> Architecture
-> Database Design
-> Integration Designs
-> Installation Design
-> Execution Plan
-> Dependency Graph
-> Backlog
-> Tasks
-> Client Approval Gate
-> Implementation
```

Do not create product source code before the explicit implementation approval gate.

## Commands

Commands executados com sucesso na solução `OBS-AI-Live-Assistant.slnx` (.NET 10, SDK fixado em `global.json`). A solução contém os projetos `ObsAi.Domain`, `ObsAi.Application`, `ObsAi.Infrastructure`, `ObsAi.Providers`, `ObsAi.ObsIntegration`, `ObsAi.Host` (criados em `TASK-002`) e os projetos de teste `ObsAi.Architecture.Tests`, `ObsAi.Unit.Tests`, `ObsAi.Integration.Tests`, `ObsAi.Contract.Tests`, `ObsAi.Security.Tests`, `ObsAi.FailureIsolation.Tests` e `ObsAi.Installer.Tests`. `dotnet test --no-build` executa 43 testes determinísticos no momento (37 no projeto de arquitetura e 6 âncoras de scaffold).

```text
Restore: dotnet restore
Build: dotnet build --no-restore
Test: dotnet test --no-build
Lint / format validation: dotnet format --verify-no-changes --no-restore
Quality gates runner: powershell -ExecutionPolicy Bypass -File tooling\quality-gates.ps1
Prototype (evidência, fora da solution): dotnet run --project prototypes\compat-sniff\CompatibilityProbe.csproj
Native build: PENDING_OBS_ARCHITECTURE_AND_TOOLCHAIN_DESIGN
```

Do not invent commands. Update this section when the corresponding project files and tools are approved.

## Repository Structure

```text
.ai/                 AI roles, knowledge pointers, and governance
.claude/             Claude Code rules, hooks documentation, and project skills
.codex/              Codex project skills
.github/             GitHub Copilot bridge and future CI configuration
agent_docs/          Detailed project rules loaded when relevant
docs/                Canonical project documentation and prompt history
tasks/               Task lifecycle directories and dependency graph
src/                 ObsAi.* projects (Domain, Application, Infrastructure, Providers, ObsIntegration, Host)
tests/               Test projects by category (Architecture, Unit, Integration, Contracts, Security, FailureIsolation, Installer)
prototypes/          Evidence prototypes outside the solution (compat-sniff desde TASK-003)
tooling/             Deterministic quality-gate runner
AGENTS.md             Multi-tool source of truth
CLAUDE.md             Thin Claude Code wrapper
PROJECT_SKILLS.md     Installed skill registry
```

Solution and build configuration files (`OBS-AI-Live-Assistant.slnx`, `global.json`, `Directory.Build.props`, `Directory.Packages.props`, `.editorconfig`) exist as the foundation. Product source code is scaffolded (empty `ObsAi.*` projects with minimal references); the deterministic architecture project and category test scaffolds exist. Functional product code, executable product-behavior tests, packaging, and installer implementation remain intentionally absent until their approved tasks complete.

## Workflow Rules

- Speak with the client in Brazilian Portuguese. Human-facing project documentation, root and documentation READMEs, Task descriptions, Requirements, Architecture, Research, Security, Testing, Reports, Governance, Knowledge, execution reports, and pull request descriptions use Brazilian Portuguese (`pt-BR`). Source code and technical identifiers remain in English; established technical terms may remain in English when translation would reduce clarity. Tool-facing files such as `AGENTS.md`, `CLAUDE.md`, and `SKILL.md` may retain the language and structure required by their tools.
- Read relevant requirements, architecture decisions, tasks, and agent documentation before changing files.
- Use the smallest sufficient design; do not introduce microservices, distributed messaging, cloud infrastructure, RAG, or multi-agent runtime without approved requirements and ADRs.
- Treat chat input, provider output, files, and external API responses as untrusted.
- Never place secrets in source control, SQLite, configuration files, prompts, documentation, or logs.
- Never let chat messages directly execute sensitive OBS actions; an explicit authorization policy is required.
- Keep native OBS responsibilities narrow and keep business logic in the external Assistant Core.
- Preserve process isolation so Assistant Core failures cannot crash OBS whenever technically possible.
- Use deterministic quality gates in addition to AI review.
- Never claim a command, test, integration, or deployment succeeded without evidence.
- Do not commit, push, open a PR, modify OBS, or perform external writes unless the current approved phase authorizes it.

## Git Status

`GIT_SETUP: COMPLETED`

Git and the permanent `main`, `develop`, and `hml` branches are operational. Task work uses `feature/task-*` branches created from synchronized `develop`, with the governed flow `feature/task-* -> develop -> hml -> release/1.0.0XXXX -> main`. Only a successful task PR may merge automatically into `develop`; later promotions require their own gates.

## Skills and Agents

- Project agents: `.ai/agents/`.
- Skill registry: `PROJECT_SKILLS.md`.
- Claude project skills: `.claude/skills/`.
- Codex project skills: `.codex/skills/`.
- Load only roles and skills relevant to the current task.

## Deeper Context

- Business rules: `agent_docs/business-rules.md`
- Security constraints: `agent_docs/security.md`
- Engineering standards: `agent_docs/engineering-standards.md`
- Architecture direction and unresolved decisions: `agent_docs/architecture.md`
- Productivity and tool status: `agent_docs/productivity.md`
- Governance: `.ai/governance/README.md`
- Knowledge sources: `.ai/knowledge/README.md`

## Keeping This Contract Current

Update the relevant specialized document when a requirement, architectural decision, security constraint, command, or recurring lesson becomes authoritative. Keep `AGENTS.md` concise and avoid duplicating detailed rules already maintained in `agent_docs/`.
