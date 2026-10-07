# Riscos de Requirements

Escala qualitativa: probabilidade e impacto são `BAIXA`, `MÉDIA` ou `ALTA`. As avaliações orientam fases posteriores e não substituem threat model, Research ou plano de testes.

| ID | Risco | Probabilidade | Impacto | Requisitos afetados | Tratamento requerido | Estado |
|---|---|---|---|---|---|---|
| RSK-001 | Falha do Assistant Core, integração ou código nativo causar crash, hang ou corrupção no OBS. | MÉDIA | ALTA | RNF-001, RNF-015, RNF-026 | Isolamento verificável, limites de responsabilidade e failure tests. | OPEN / CONTROLLED |
| RSK-002 | Indisponibilidade de AI Provider impedir respostas ou provocar retries/custos indevidos. | ALTA | MÉDIA | RF-019, RF-027, RNF-002, RNF-011 | Falhas classificadas, timeout, cancelamento e retry seguro. | OPEN / CONTROLLED |
| RSK-003 | Quota ou custo de APIs crescer por abuso, rajadas ou contexto excessivo. | ALTA | ALTA | RF-011, RF-013, RF-014, RNF-008 | Rate limiting, filas limitadas, minimização e observabilidade de consumo. | OPEN / CONTROLLED |
| RSK-004 | Latência de IA ou TTS prejudicar ritmo da live e acumular filas. | ALTA | MÉDIA | RF-025 a RF-027, RNF-008 a RNF-010, RNF-015 | Metas após baseline, limites, cancelamento e degradação independente. | OPEN / CONTROLLED |
| RSK-005 | Abuso do chat, spam ou conteúdo impróprio alcançar providers ou saídas. | ALTA | ALTA | RF-008 a RF-013, RF-020, RNF-004 | Trigger, moderação, blocklist, anti-spam e limites por usuário/global. | OPEN / CONTROLLED |
| RSK-006 | Prompt injection induzir vazamento, contornar moderação ou solicitar ação sensível do OBS. | ALTA | ALTA | RF-010, RF-014, RF-020, RF-036, RNF-007 | Separação de autoridade, output validation, allowlist e autorização do streamer. | OPEN / CONTROLLED |
| RSK-007 | API key, token OAuth ou refresh token vazar em arquivo, banco, prompt, log ou diagnóstico. | MÉDIA | ALTA | RF-018, RF-031, RNF-003, RNF-013 | Secret storage pesquisado, redação, scans e testes de lifecycle. | OPEN / CONTROLLED |
| RSK-008 | Mudança de API/ABI ou comportamento do OBS 32.x quebrar integração. | MÉDIA | ALTA | RF-032, RNF-024, RNF-025 | Matriz de compatibilidade, detecção e regressão por versão suportada. | OPEN / CONTROLLED |
| RSK-009 | Installer, upgrade, repair ou uninstall afetar OBS, dados, credenciais ou outra integração. | MÉDIA | ALTA | RF-032 a RF-035, RNF-020, RNF-021 | Research de lifecycle, rollback, escopo explícito e testes em ambiente controlado. | OPEN / CONTROLLED |
| RSK-010 | Roteamento de áudio causar eco, dispositivo incorreto, sobreposição ou instabilidade no OBS. | MÉDIA | ALTA | RF-023 a RF-025, RNF-002, RNF-025 | Research de áudio, controles de monitoramento e ADR antes da implementação. | OPEN / CONTROLLED |
| RSK-011 | Retenção excessiva ou envio indevido de conteúdo violar privacidade do streamer/viewer. | MÉDIA | ALTA | RF-004, RF-006, RF-014, RF-030, RNF-005, RNF-023 | Minimização, finalidade, política por categoria, exclusão e avaliação de provider. | OPEN / CONTROLLED |
| RSK-012 | Corrupção, lock, migration ou falha de disco causar perda silenciosa de configuração e histórico autorizado. | MÉDIA | ALTA | RF-028 a RF-030, RNF-012, RNF-022 | Integridade, recovery, backup e migrations pesquisados e testados. | OPEN / CONTROLLED |
| RSK-013 | Fila sobrecarregada consumir recursos e competir com OBS. | ALTA | ALTA | RF-013, RF-021, RF-025, RNF-008, RNF-016 | Backpressure, capacidade limitada, descarte controlado e observabilidade. | OPEN / CONTROLLED |
| RSK-014 | Escopo multi-provider inflar a V1 ou contratos refletirem um vendor específico. | MÉDIA | MÉDIA | RF-016, RF-017, RF-024, RNF-017 | Contratos mínimos baseados em requisitos; seleção posterior de poucos providers. | OPEN / CONTROLLED |

Nenhum risco está aceito como ausência de controle. As mitigações técnicas dependem das fases indicadas e devem preservar o princípio WHAT × HOW.
