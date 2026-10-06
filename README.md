# OBS-AI-Live-Assistant

OBS-AI-Live-Assistant is a planned, independent Windows application for AI-assisted interaction during OBS Studio live streams. It is intended to accept viewer or streamer requests and produce configurable text and voice responses while keeping OBS stability independent from chat, AI, TTS, storage, and network failures.

## Current Status

- Documentation baseline: established.
- Kit IA Dev base: installed.
- Advanced Skills: **BLOCKED** because the official package is unavailable.
- Knowledge Quality Gate: **NOT PASSED**.
- Requirements, research, and architecture approval: **NOT STARTED**.
- Product source code and implementation: **NOT CREATED / NOT STARTED**.

## Stack Direction

The current direction is C# / .NET 10 on Windows 10/11 x64, with C/C++ only when a validated OBS-native capability requires it. OBS Studio 32.x, SQLite for local V1 persistence, a modular monolith, Ports and Adapters, provider boundaries, BYOK AI, multi-provider TTS, and YouTube Live Chat priority are planning inputs—not a substitute for Requirements, research, or ADR approval.

## Documentation

Start at the [project documentation index](docs/README.md). It separates current context, governance, knowledge, research backlog, security, testing, reports, prompt history, and planned architecture artifacts.

## GitFlow

Work starts from `develop` on `feature/task-*` branches. A successful task is reviewed, validated, pushed, submitted by pull request, and merged into `develop` when all gates pass. Promotion to `hml` or `main` is not automatic. See [GitFlow](docs/governance/GITFLOW.md).

## Current Blockers

- The official Advanced Skills package has not been located.
- The Knowledge Quality Gate cannot pass until its prerequisites are validated.

No product files or OBS runtime data are created by this documentation baseline.
