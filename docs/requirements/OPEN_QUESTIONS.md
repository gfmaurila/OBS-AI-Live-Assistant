# Open Questions

Nenhuma pergunta abaixo bloqueia o fechamento de Requirements. Algumas bloqueiam escolhas posteriores e estão vinculadas a Research, ADR ou Product Decision.

| ID | Pergunta | Tipo | Prioridade para Requirements | Bloqueia posteriormente | Encaminhamento |
|---|---|---|---|---|---|
| OQ-001 | Quais capacidades exigem plugin nativo e quais podem usar OBS WebSocket? | RESEARCH / ADR | NON-BLOCKING | Architecture | RES-001, RES-007, RES-008; ADR-001 |
| OQ-002 | A UI será externa, Dock do OBS ou combinação controlada? | RESEARCH / ADR | NON-BLOCKING | UI/Integration Design | RES-004; ADR-001 |
| OQ-003 | Como o áudio TTS será entregue e monitorado no OBS? | RESEARCH / ADR | NON-BLOCKING | Architecture | RES-005, RES-006; ADR-006 |
| OQ-004 | Qual mecanismo protegerá secrets locais e qual será seu lifecycle? | RESEARCH / ADR | NON-BLOCKING | Security/Architecture | RES-012, RES-013; ADR-005 |
| OQ-005 | Quais AI e TTS Providers específicos compõem a V1? | PRODUCT DECISION | NON-BLOCKING | Backlog/Implementation | RES-015, RES-017 |
| OQ-006 | IA local fará parte da V1 ou permanecerá opção futura? | RESEARCH / CLIENT DECISION | NON-BLOCKING | Scope/Architecture | RES-016 |
| OQ-007 | Persistent Memory terá finalidade aprovada na V1? | CLIENT DECISION | NON-BLOCKING | Data/Security Design | RES-023; ADR-009 |
| OQ-008 | Quais categorias de interação, logs e estatísticas serão retidas e por quanto tempo? | PRODUCT DECISION | NON-BLOCKING | Security/Data Design | RES-023, RES-024 |
| OQ-009 | O suporte ao OBS 32.x será por versão exata ou faixa de versões? | RESEARCH / CLIENT DECISION | NON-BLOCKING | Compatibility policy | RES-022; ADR-010 |
| OQ-010 | Quais limites padrão e faixas configuráveis serão adotados para entrada, resposta, fila, cooldown, timeout e TTS? | RESEARCH / PRODUCT DECISION | NON-BLOCKING | Security/Testing | Threat model e testes futuros |
| OQ-011 | Qual tecnologia e modelo de elevação/assinatura serão usados pelo installer? | RESEARCH / ADR | NON-BLOCKING | Installation Design | RES-018; ADR-007 |
| OQ-012 | O uninstall removerá dados, logs e secrets por padrão ou oferecerá escolhas explícitas? | PRODUCT DECISION | NON-BLOCKING | Installation Design | RES-021 |
| OQ-013 | Qual experiência de autenticação OAuth do YouTube será adotada e quais scopes mínimos serão usados? | RESEARCH / ADR | NON-BLOCKING | Chat/Security Design | RES-014 |
| OQ-014 | Quais ações OBS, se alguma, serão autorizadas na V1 além de exibir/reproduzir respostas? | CLIENT DECISION | NON-BLOCKING | Scope/Security | Security Requirements e ADR-001 |
| OQ-015 | Quais requisitos de acessibilidade adicionais se aplicam à UI além de operação por teclado e comunicação textual de estado? | PRODUCT DECISION | NON-BLOCKING | UI Design | Refinamento de UX |

**Blocking Questions de Requirements:** 0.
