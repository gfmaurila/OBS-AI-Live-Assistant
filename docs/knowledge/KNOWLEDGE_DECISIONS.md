# Decisões sobre o conhecimento

Nenhuma decisão arquitetural é aprovada por esta baseline. Este registro documenta somente como as fontes atuais serão tratadas.

| Fonte ou tópico | Classificação | Justificativa |
|---|---|---|
| Contexto atual fornecido pelo cliente | ADOPT / ADAPT | Usar como insumo delimitado de planejamento e preservar marcadores explícitos de aprovação. |
| `AGENTS.md` e `agent_docs/` | ADOPT | Restrições e regras de fluxo vigentes no repositório. |
| Base do Kit IA Dev | ADAPT | Agentes e Skills básicas estão instalados; o conhecimento relevante ainda requer validação. |
| Repositório de referência CMS/Azure | REFERENCE | Reutilizar somente padrões de organização documental e rastreabilidade. |
| Advanced Skills | BLOCKED | O pacote oficial não está disponível; nenhum substituto pode ser inventado. |
| Distribuição da integração com OBS | REQUIRES_RESEARCH / REQUIRES_ADR | Responsabilidades de plugin nativo, WebSocket, IPC, Dock e áudio permanecem abertas. |
| Armazenamento de secrets no Windows | REQUIRES_RESEARCH / REQUIRES_ADR | Credential Manager e DPAPI são candidatos, não decisões. |
| Twitch e providers adicionais de chat | FUTURE | Não fazem parte da V1 sem aprovação específica. |

Requirements e ADRs aprovados serão adicionados somente durante seus processos autorizados.
