# Pesquisa — YouTube Live Chat

## Fluxo necessário no V1

1. OAuth para aplicativo desktop usando browser do sistema, loopback e PKCE; aplicativo instalado é public client e não deve depender de client secret embarcado.
2. Escopos mínimos, consentimento, refresh protegido, revogação e disconnect.
3. Descobrir broadcast ativo e `liveChatId` com Live Streaming API.
4. Consumir `liveChatMessages.streamList` quando adequado ou `list`, respeitando `pollingIntervalMillis` e `nextPageToken`.
5. Tratar chat encerrado/desabilitado, `forbidden`, `notFound`, `rateLimitExceeded`, expiração e reconexão com backoff limitado.

A quota default documentada é 10.000 unidades/dia; `list` custa 1 e operações de escrita frequentemente 50, mas valores devem ser consultados e observados, não hardcoded como garantia. Políticas do YouTube exigem transparência, controles de exclusão e limites de retenção aplicáveis.

Fontes: SRC-024..030. Confidence: **HIGH**. Status: **RESOLVED**.
