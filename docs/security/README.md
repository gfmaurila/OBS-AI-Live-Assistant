# Documentação de Security

O baseline atual de segurança permanece em [`agent_docs/security.md`](../../agent_docs/security.md). Esta página fornece navegação sem duplicar aquela fonte.

As restrições atuais incluem tratar dados de chat e providers como não confiáveis, impedir que o chat autorize diretamente ações sensíveis do OBS, aplicar least privilege, limitar trabalho e nunca armazenar secrets no controle de versão, prompts, documentação, logs, configuração em texto puro ou SQLite.

Security Requirements e artefatos de threat modeling estão **NOT STARTED**. Proteção de credenciais no Windows, OAuth do YouTube, autenticação de IPC, endpoints locais, segurança do instalador e ciclo de vida dos dados exigem Research e decisões de segurança autorizadas.
