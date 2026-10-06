# Project Scope

This baseline classifies known context without creating new requirements.

## V1 Current Direction

- An independent local Windows application supporting OBS Studio 32.x x64.
- Viewer interaction prioritized through YouTube Live Chat and manual streamer interaction.
- Configurable AI text responses and TTS voice responses.
- BYOK and provider boundaries for AI and TTS.
- Local relational persistence with SQLite as the current direction.
- Strong secret handling and process isolation from OBS.
- A modular monolith with Ports and Adapters as the current Assistant Core direction.

Each item remains subject to the applicable Requirements, research, security, ADR, and client approval gates.

## Future

- Twitch and other chat providers.
- Additional AI and TTS providers validated against approved provider contracts.
- Local AI provider support if technical research establishes viability.

Future items are not V1 commitments.

## Out of Scope V1

- Microservices and distributed messaging infrastructure.
- Kafka, RabbitMQ, Redis, Kubernetes, and cloud platform infrastructure.
- RAG and a multi-agent product runtime.
- Twitch, Kick, automatic narration, and advanced analytics unless separately approved.
- Functionality from the CMS/Azure reference project, including CMS, multi-tenancy, React applications, and cloud-specific services.
