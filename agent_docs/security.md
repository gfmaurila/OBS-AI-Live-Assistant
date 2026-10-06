# Security Considerations

## Security Baseline

- Treat live chat, AI output, provider responses, IPC messages, OBS events, configuration imports, and database contents as untrusted input.
- Validate and bound all externally controlled text before it enters prompts, logs, chat output, TTS, persistence, or OBS-facing commands.
- Apply input moderation before AI processing and output validation/moderation before publishing or narration.
- Enforce global and per-user limits, bounded queues, request timeouts, cancellation, response limits, and anti-spam controls.
- Never execute sensitive OBS actions directly from chat content. Authorization policy and allowlisted commands must mediate every OBS-facing action.
- Use least privilege across processes, providers, files, credentials, and OBS capabilities.

## Secrets

API keys, OAuth access tokens, refresh tokens, provider secrets, passwords, and OBS credentials are secrets.

Secrets must never be stored in plaintext in SQLite, `appsettings.json`, `.env` files, source code, prompts, documentation, crash reports, telemetry, or logs. Logs must redact sensitive headers, query values, payload fields, and exception details.

The final Windows secret-storage mechanism requires security research and an ADR. Windows Credential Manager and DPAPI are candidates, not approved implementations.

## Required Future Research

- Windows Credential Manager and DPAPI threat models, user/machine scope, backup, repair, and uninstall behavior.
- YouTube OAuth flows, token lifecycle, scopes, revocation, and quota exposure.
- OBS plugin, WebSocket, IPC, and local endpoint authentication boundaries.
- Prompt injection, output abuse, moderation policy, and safe degradation.
- SQLite file permissions, corruption handling, backup, retention, and privacy.
- Installer elevation, signing, supply-chain verification, and secure upgrade behavior.
