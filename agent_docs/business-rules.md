# Business Rules and Domain Knowledge

- The product is an independent assistant for OBS Studio live streams.
- V1 prioritizes YouTube Live Chat, `@assistente`, `!ia`, and manual streamer interaction.
- Text and voice responses are independently configurable.
- Multiple viewers must not cause simultaneous or unbounded AI or TTS processing.
- Input and output pass through validation, moderation, rate limiting, and bounded queues.
- Chat content must never directly authorize sensitive OBS operations.
- The streamer controls assistant activation, provider selection, model/configuration, profile, voice behavior, and moderation settings.
- Assistant personality and response style are configuration, not hardcoded behavior.
- Live context must be limited to what is necessary; unlimited conversation history is prohibited.
- Short-term live context and persistent configuration/statistics are distinct concepts.
- Conversation data must not be retained indiscriminately; retention and cleanup require explicit rules.
- BYOK credentials belong to the user and must never be persisted or logged as plaintext.
- AI, TTS, chat, database, network, or Assistant Core failure must not terminate or compromise the OBS broadcast whenever technically possible.
- User application data may eventually live under `%APPDATA%\obs-studio\obs-ai-live-assistant`, but that directory must not be created before an approved implementation or installation task.
- Plugin binaries and application data are separate concerns; no installation path may be assumed without research.
