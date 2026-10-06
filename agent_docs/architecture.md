# Architecture Direction

## Approved Direction

- Modular monolith for the Assistant Core.
- Ports and Adapters for external integrations.
- Provider architecture for AI, TTS, and chat capabilities.
- C# / .NET 10 for the Assistant Core.
- C/C++ only for OBS-native capabilities that cannot be satisfied safely by supported external integration.
- SQLite as the local relational database direction for V1.
- Windows 10 and Windows 11 x64; OBS Studio 32.x x64.
- Process isolation must protect OBS from Assistant Core failures.

## Boundary Intent

The native OBS component, if an ADR proves it necessary, should remain focused on OBS lifecycle, UI/Dock, events, audio, and controlled communication. Chat, AI, TTS orchestration, moderation, memory, context, providers, persistence, security, configuration, and application logging belong outside the OBS process by default.

## Decisions Not Yet Made

The following must not be resolved during bootstrap:

- Native OBS plugin versus OBS WebSocket responsibility split.
- Dock implementation mechanism and compatibility details.
- C++ to .NET IPC transport and protocol.
- TTS audio routing into OBS.
- Credential protection mechanism.
- SQLite library, migration mechanism, concurrency policy, and recovery strategy.
- Concrete AI, TTS, and YouTube client libraries.
- Local AI provider viability.
- Installer technology, packaging layout, upgrade, repair, and uninstall mechanics.
- Exact OBS plugin binary and data installation paths.

Each material decision requires official technical research and an ADR during an authorized phase.

## Explicit Non-Goals for V1

Microservices, Kafka, RabbitMQ, Redis, Kubernetes, cloud infrastructure, RAG, multi-agent runtime, ATS/ETS2 telemetry, TruckHub integration, Twitch, Kick, automatic narration, and advanced analytics are out of V1 unless separately approved.
