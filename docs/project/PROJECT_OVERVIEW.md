# Project Overview

## Product

**Name:** OBS-AI-Live-Assistant

**Purpose — CURRENT DIRECTION:** provide an independent assistant for OBS Studio live streams that can receive viewer or streamer requests and return configurable text and voice responses.

**Problem — CURRENT DIRECTION:** live-stream interaction requires coordination among chat, AI, TTS, local state, and OBS without allowing failures in those dependencies to terminate or compromise the broadcast.

## Current Direction

| Area | Current understanding | State |
|---|---|---|
| Host environment | Windows 10/11 x64 with OBS Studio 32.x x64 | CURRENT DIRECTION |
| Primary runtime | C# / .NET 10 | CURRENT DIRECTION |
| Native code | C/C++ only for validated OBS-native capabilities | CURRENT DIRECTION / REQUIRES_RESEARCH |
| Product shape | Independent local application with isolated Assistant Core responsibilities | CURRENT DIRECTION / REQUIRES_ADR |
| Chat | YouTube Live Chat priority for V1 | CURRENT DIRECTION |
| AI | Multiple providers using user-owned credentials | CURRENT DIRECTION |
| TTS | Multiple providers with configurable voice responses | CURRENT DIRECTION |
| Persistence | Local relational data using SQLite for V1 | CURRENT DIRECTION / REQUIRES_ADR |
| Runtime data | `%APPDATA%\obs-studio\obs-ai-live-assistant` | ASSUMPTION / REQUIRES_RESEARCH |

BYOK means credentials belong to the user. It does not approve a provider list or a storage mechanism. Secrets must never be placed in source control, prompts, logs, plaintext configuration, or SQLite.

## OBS Integration

The product is expected to integrate with OBS, but the allocation of responsibilities among a native plugin, OBS WebSocket, and IPC is `REQUIRES_RESEARCH`, `REQUIRES_ADR`, and potentially `REQUIRES_CLIENT_DECISION`. No integration mechanism is approved merely by appearing in this overview.

## Reliability Intent

The current direction is that AI, TTS, chat, storage, and network failures must not become OBS failures whenever technically possible. Exact process, recovery, and degradation behavior remains subject to Requirements and architecture validation.
