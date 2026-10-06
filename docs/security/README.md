# Security Documentation

The current security baseline is maintained in [`agent_docs/security.md`](../../agent_docs/security.md). This page provides navigation without duplicating that source.

Current non-negotiable constraints include treating chat and provider data as untrusted, preventing chat from directly authorizing sensitive OBS actions, applying least privilege, bounding work, and never storing secrets in source control, prompts, documentation, logs, plaintext configuration, or SQLite.

Security Requirements and threat-model artifacts are **NOT STARTED**. Windows credential protection, YouTube OAuth, IPC authentication, local endpoints, installer security, and data lifecycle require authorized research and security decisions.
