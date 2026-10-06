# Mapa de conhecimento do projeto

Este mapa inicial identifica áreas de conhecimento e seu tratamento. Ele não converte conteúdo em Requirements ou Architecture aprovados.

| Área de conhecimento | Fonte atual | Classificação | Próxima validação |
|---|---|---|---|
| Kit IA Dev | Artefatos instalados no bootstrap do repositório | ADAPT | Validar entradas relevantes do dicionário no Knowledge Quality Gate. |
| Conceito atual do produto | Contexto do cliente e `agent_docs/business-rules.md` | ADOPT | Formalizar por meio de Requirements. |
| Integração com OBS | `agent_docs/architecture.md` | REQUIRES_RESEARCH / REQUIRES_ADR | Avaliar limites de plugin nativo, WebSocket, Dock, áudio e IPC. |
| BYOK | Contexto do cliente e restrições de segurança | ADAPT | Definir requisitos de providers e credenciais; aprovar ADR de armazenamento de secrets. |
| TTS | Contexto do produto | REQUIRES_RESEARCH | Definir provider, roteamento, moderação e comportamento de falha. |
| YouTube Live Chat | Contexto do produto | REQUIRES_RESEARCH | Validar API oficial, OAuth, quotas e ciclo de vida. |
| SQLite | Direção de Architecture | REQUIRES_ADR | Validar biblioteca, migrations, concorrência, recuperação, retenção e segurança do arquivo. |
| Windows | Direção de plataforma | ADOPT | Validar compatibilidade e mecanismos de segurança. |
| Installer | Contexto do produto | REQUIRES_RESEARCH / REQUIRES_ADR | Selecionar tecnologia e ciclo de vida somente após Research. |
| Security | `agent_docs/security.md` | ADOPT | Produzir Security Requirements e validar limites de ameaça. |
| Histórico de prompts | `docs/prompts/` | ADOPT | Manter rastreabilidade no mesmo commit da Task. |
| GitFlow | Documentos de Governance | ADOPT | Aplicar gates da Task e promoções protegidas. |
| Advanced Skills | Pacote oficial não localizado | BLOCKED | Instalar somente a partir do pacote oficial validado. |
| Twitch e outros providers de chat | Contexto do produto | FUTURE | Excluir da V1 salvo aprovação. |
