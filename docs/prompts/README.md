# Histórico de prompts operacionais

`docs/prompts/history/` contém o histórico cronológico dos prompts operacionais usados para configurar, documentar e evoluir este projeto. O arquivo é versionado para rastreabilidade e auditoria.

Prompts históricos são entradas e registros de execução. Eles não são Requirements ou decisões de Architecture automaticamente aprovados. Requirements canônicos são produzidos e aprovados pelo processo de Requirements. Decisões arquiteturais canônicas são mantidas na documentação de Architecture e em Architecture Decision Records (ADRs). Documentação canônica mais recente prevalece sobre prompts históricos.

Prompts nunca devem conter secrets, credenciais, passwords, access tokens, refresh tokens, private keys, connection strings com secrets ou API keys. Se um prompt contiver dado sensível, interrompa e providencie tratamento ou redação segura antes do arquivamento no Git.

Para cada prompt operacional que produzir alteração no repositório:

1. concluir e validar a alteração autorizada;
2. arquivar o prompt em `docs/prompts/history/`, usando o próximo nome sequencial `prompt<number>.md` disponível;
3. executar o Secret Check no prompt e em todos os arquivos alterados;
4. incluir o prompt arquivado no mesmo commit das alterações produzidas;
5. enviar esse commit para a branch da própria Task.

Nunca sobrescreva um prompt histórico existente.
