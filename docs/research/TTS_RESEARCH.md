# Pesquisa — TTS Providers

| Provider | Auth/localidade | Streaming/formato | Dependência | Uso recomendado |
|---|---|---|---|---|
| Windows SpeechSynthesizer | Local; vozes Microsoft instaladas | Stream local; seleção de voz | Qualidade/vozes do SO | Fallback/offline candidato |
| Azure Speech | Secret/token cloud | SSML e múltiplos formatos streaming | Custo, região e rede | Provider cloud candidato |
| ElevenLabs | API key cloud | HTTP/WebSocket streaming e formatos configuráveis | Custo, retenção e rede | Provider opcional |

O port deve oferecer voz/locale, texto limitado, formato negociado, streaming opcional, cancellation, timeout, custo/usage quando disponível e erro normalizado. Conteúdo da IA deve ser validado antes do TTS; fila limitada e interrupção manual/global são obrigatórias. Provider V1 ainda depende de decisão.

Fontes: SRC-040..043. Confidence: **HIGH** sobre capacidades, **MEDIUM** sobre seleção. Status: **PARTIALLY_RESOLVED**.
