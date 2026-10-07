# Pesquisa — OBS WebSocket

obs-websocket integra o OBS desde a versão 28. O protocolo 5.x possui `Hello`, `Identify`, challenge-response de autenticação, requests, batches e subscriptions de eventos. `GetVersion` expõe versão RPC e requests disponíveis; portanto o adapter deve negociar capabilities e falhar fechado.

É adequado a operações já expostas — consultar estado, cenas/sources e eventos — em especial fora do processo. Não substitui Frontend API para Dock nem libobs para source de áudio customizada. Mesmo em localhost, autenticação deve permanecer habilitada e o segredo não pode estar em logs/configuração plana.

Fontes: SRC-012/013. Confidence: **HIGH**. Status: **RESOLVED**.
