# Proteção de segurança do OBS

## Regra crítica

O assistente não pode tornar a estabilidade do OBS Studio dependente de AI Provider, TTS Provider, Chat Provider, banco, Assistant Core ou rede. Falha, hang, payload inválido, indisponibilidade ou saturação nesses elementos deve resultar em degradação controlada, nunca em encerramento do OBS.

**CHAT NÃO DEVE EXECUTAR DIRETAMENTE COMANDOS SENSÍVEIS DO OBS.** Trigger, identidade de viewer, conteúdo de chat ou AI output são insuficientes como autorização.

## Resultados obrigatórios

- chamadas e mensagens que cruzem a integração devem ter contrato validado, limites, timeout e cancellation quando aplicável;
- exceções não podem atravessar boundaries de modo a comprometer o processo OBS;
- filas e concorrência não podem crescer sem limite nem bloquear threads críticas do OBS;
- indisponibilidade externa deve preservar controle do streamer e oferecer estado degradado observável;
- retries devem ser limitados e interrompíveis; suspensão temporária de chamadas após falhas repetidas deve ser possível, sem fixar aqui o mecanismo;
- configuração externa do OBS só pode ser alterada após autorização explícita e dentro de escopo conhecido;
- qualquer capacidade futura de controle sensível requer authorization layer, allowlist, validação, rate limiting e evidência auditável.

Processos, IPC, plugin nativo, OBS WebSocket e responsabilidades finais são `REQUIRES_RESEARCH + REQUIRES_ADR`.
