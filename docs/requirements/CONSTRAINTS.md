# Constraints

| ID | Restrição | Origem | Classificação |
|---|---|---|---|
| CON-001 | A V1 deve operar em Windows 10 e Windows 11 x64. | `AGENTS.md`; `PROJECT_SCOPE.md` | CONFIRMED |
| CON-002 | O alvo atual é OBS Studio 32.x x64; suporte além dessa faixa não é prometido. | `AGENTS.md`; Knowledge Map | CONFIRMED |
| CON-003 | O produto deve ser uma aplicação independente e local. | `AGENTS.md`; regras de negócio | CONFIRMED |
| CON-004 | Falhas do assistente e de suas dependências não podem derrubar ou bloquear o OBS Studio. | `AGENTS.md`; regras de negócio | CONFIRMED |
| CON-005 | Chat, providers, arquivos, APIs, persistência e integração são entradas não confiáveis. | `agent_docs/security.md` | CONFIRMED |
| CON-006 | BYOK deve ser suportado sem provider obrigatório. | `AGENTS.md`; Knowledge Decisions | CONFIRMED |
| CON-007 | Secrets não podem ser persistidos ou registrados em plaintext. | `agent_docs/security.md` | CONFIRMED |
| CON-008 | Mensagens de chat não podem autorizar diretamente ações sensíveis do OBS. | `AGENTS.md`; `agent_docs/security.md` | CONFIRMED |
| CON-009 | Configurações externas do OBS não podem ser modificadas sem autorização explícita. | contrato do repositório | CONFIRMED |
| CON-010 | O projeto não pode absorver ou modificar arquivos do OBS Truck Live Optimizer. | `AGENTS.md` | CONFIRMED |
| CON-011 | C# / .NET 10 é a direção principal; C/C++ só pode ser usado se uma capacidade nativa validada exigir. | `AGENTS.md` | CURRENT DIRECTION / REQUIRES_ADR |
| CON-012 | SQLite é direção relacional local da V1, não decisão final de lifecycle ou schema. | `AGENTS.md`; Knowledge Decisions | CURRENT DIRECTION / REQUIRES_ADR |
| CON-013 | Documentos humanos e descrições de PR usam pt-BR; identificadores técnicos podem permanecer em inglês. | `AGENTS.md`; governança | CONFIRMED |
| CON-014 | Mudanças seguem o GitFlow e os Quality Gates vigentes; esta fase não autoriza código de produto. | governança | CONFIRMED |
| CON-015 | O produto deve coexistir com outras integrações e não assumir exclusividade sobre recursos compartilhados do OBS. | contrato do repositório; segurança | CONFIRMED |
