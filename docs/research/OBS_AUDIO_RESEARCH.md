# Pesquisa — áudio TTS no OBS

| Opção | Vantagens | Desvantagens | Segurança/OBS | Recomendação | Confidence |
|---|---|---|---|---|---|
| Source nativa | Mixer, mute, monitoring e tracks nativos | Mais C++ e risco in-process | Validar buffers/threading | Candidata quando controle por track for obrigatório | MEDIUM |
| Application Audio Capture | Core permanece fora do OBS; API pronta | Configuração e identidade do processo | Menor crash surface | Baseline preferível para protótipo | MEDIUM |
| Virtual audio device | Routing flexível | Driver/terceiro, admin e UX complexa | Nova supply chain | Não usar por padrão | HIGH |

A decisão depende de protótipo com latência, cancelamento, mute, monitoring, gravação/stream tracks e comportamento em crash. Nunca sintetizar ou fazer I/O de rede em callback do OBS.

Fontes: SRC-008..011. Status: **PARTIALLY_RESOLVED**.
