# Segurança de entrada e saída

## Chat input = UNTRUSTED INPUT

Toda mensagem deve ser normalizada e validada antes de trigger, moderação, fila ou provider. Os controles devem rejeitar ou tratar de forma limitada conteúdo ausente, malformado, acima do limite configurado, com encoding inválido, repetido, em flood ou incompatível com o contrato do Chat Provider.

Sanitização não substitui validação contextual. O sistema deve preservar texto necessário sem interpretar conteúdo de viewer como configuração, instrução de sistema, autorização ou comando.

## Prompt Injection

Conteúdo de viewer não pode:

- substituir System Instructions ou políticas do produto;
- obter secrets, credenciais, arquivos arbitrários ou configuração protegida;
- executar shell, comandos locais ou instalar software;
- modificar configuração do produto ou do OBS;
- comandar ação sensível do OBS ou elevar privilégios;
- transformar output do modelo em autoridade.

Contexto enviado ao provider deve ser mínimo, delimitado e livre de secrets. Tentativas detectadas devem produzir resultado controlado e diagnóstico sem replicar payload sensível integral.

## AI output = UNTRUSTED OUTPUT

Resposta de IA deve ser validada, limitada e, quando aplicável, moderada antes de publicação, TTS, persistência ou qualquer ação. Ela não possui autoridade implícita para executar comandos, alterar o sistema operacional, modificar configurações críticas, manipular OBS, acessar secrets ou instalar software.

Qualquer ação sensível futura exige autorização independente, allowlist, validação dos parâmetros, rate limiting e auditabilidade. O mecanismo será definido posteriormente.

## Limites e abuso

Cooldown por viewer, cooldown global, tamanho de entrada/saída, capacidade de filas, concorrência, timeout e cancellation devem ser configuráveis dentro de limites seguros. Valores numéricos permanecem `REQUIRES_CLIENT_DECISION` ou dependem de evidência futura.
