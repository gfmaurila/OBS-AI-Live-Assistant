# Rastreabilidade de segurança

Os critérios completos estão em [SECURITY_REQUIREMENTS.md](SECURITY_REQUIREMENTS.md). Prompt History é evidência operacional, não fonte canônica.

| RF/RNF de origem | SEC | Threat | Asset | Risk | Research | ADR | Critério rastreável |
|---|---|---|---|---|---|---|---|
| RF-002, RF-007; RNF-004 | SEC-001, SEC-002 | THT-001, THT-004 | AST-005, AST-007, AST-015 | SRISK-004, SRISK-007 | RES-014 | ADR-004, ADR-008 | Entrada inválida/oversize não alcança provider nem altera estado. |
| RF-010 a RF-012 | SEC-003 | THT-002, THT-003 | AST-007, AST-014 | SRISK-003, SRISK-004 | — | ADR-008 | Solicitação reprovada não alcança provider. |
| RF-010, RF-014, RF-036; RNF-007 | SEC-004 | THT-003 | AST-001 a AST-017 | SRISK-003 | — | ADR-001, ADR-008 | Injeção não revela secret nem aciona operação local/OBS. |
| RF-020; RNF-007 | SEC-005 | THT-006, THT-007 | AST-008, AST-017 | SRISK-005 | RES-015, RES-024 | ADR-004, ADR-008 | Output reprovado não é publicado, narrado, persistido ou executado. |
| RF-036; RNF-006, RNF-025 | SEC-006, SEC-007 | THT-008, THT-009 | AST-013, AST-014, AST-016 | SRISK-008, SRISK-013 | RES-007 a RES-010 | ADR-001, ADR-002 | Chat/IA não autorizam ação; ação não allowlisted é negada. |
| RF-017, RF-018; RNF-003 | SEC-008 a SEC-013 | THT-010, THT-012, THT-013 | AST-001 a AST-004 | SRISK-001, SRISK-002 | RES-012 a RES-014, RES-021 | ADR-005 | Secret não aparece em plaintext; remoção/revogação impede novo uso. |
| RF-011, RF-013, RF-021, RF-025; RNF-008 | SEC-014, SEC-015 | THT-002, THT-004 | AST-014, AST-015 | SRISK-004, SRISK-007 | RES-010 | ADR-003, ADR-008 | Limite excedido não chama provider nem amplia fila. |
| RF-019, RF-026, RF-027; RNF-009 a RNF-011 | SEC-016 a SEC-018 | THT-005, THT-015 | AST-014 a AST-016 | SRISK-007, SRISK-008 | RES-009, RES-010, RES-014 a RES-017 | ADR-002 a ADR-004 | Timeout/cancelamento libera capacidade; retry não é infinito. |
| RNF-008, RNF-016, RNF-023 | SEC-019 | THT-004, THT-016, THT-017 | AST-011, AST-012, AST-014, AST-015 | SRISK-007, SRISK-011 | RES-010, RES-011 | ADR-003, ADR-009 | Sobrecarga ativa recusa/backpressure sem crescimento ilimitado. |
| RF-001, RF-027; RNF-001, RNF-002 | SEC-020, SEC-021 | THT-015 | AST-014 a AST-016 | SRISK-008 | RES-007 a RES-010 | ADR-001 a ADR-003 | Falha de provider/banco/Core não encerra OBS nem remove controles. |
| RF-028, RF-033; RNF-012, RNF-022 | SEC-022, SEC-023 | THT-014, THT-016 | AST-005, AST-006, AST-012 | SRISK-009, SRISK-010 | RES-011, RES-019 | ADR-004, ADR-007 | Adulteração/falha não é sucesso silencioso e recovery é controlado. |
| RF-006, RF-014, RF-015, RF-030; RNF-005, RNF-023 | SEC-024, SEC-025 | THT-007, THT-018 | AST-006 a AST-010, AST-017, AST-018 | SRISK-012 | RES-023, RES-024 | ADR-009 | Só dados necessários são usados; expirados/excluídos deixam de ser usados. |
| RF-031; RNF-013, RNF-014 | SEC-011, SEC-026 | THT-010, THT-017, THT-019 | AST-011 | SRISK-001, SRISK-011 | SRES-005 | ADR-008 | Diagnóstico distingue estado sem secret/payload sensível integral. |
| RF-032 a RF-035; RNF-020, RNF-021 | SEC-027, SEC-028 | THT-020, THT-021 | AST-001 a AST-005, AST-019 | SRISK-015 | RES-003, RES-018 a RES-021 | ADR-007 | Pacote adulterado é recusado; falha/remoção não afeta itens alheios. |
| RNF-020, RNF-028 | SEC-029, SEC-032 | THT-021, THT-022 | AST-019, AST-020 | SRISK-014, SRISK-015 | SRES-012, RES-022 | ADR-007, ADR-010 | Dependências são rastreáveis; Critical/High ou secret bloqueiam progressão. |
| RNF-004, RNF-006 | SEC-030 | THT-008, THT-009, THT-019 | AST-015, AST-016 | SRISK-013 | RES-009, RES-010 | ADR-002, ADR-003 | Cliente/mensagem não autorizados ou incompatíveis não produzem ação. |
| RF-004, RF-014, RF-015, RF-029; RNF-026 | SEC-031 | THT-007, THT-019 | AST-006, AST-008, AST-009 | SRISK-005, SRISK-012 | RES-023 | ADR-009 | Resposta tardia/cancelada não cruza sessão; contexto encerrado não é reutilizado. |
| RF-005, RF-016 a RF-019; RNF-011, RNF-017 | SEC-033, SEC-034 | THT-006, THT-011, THT-013, THT-018 | AST-001 a AST-010, AST-017, AST-018 | SRISK-002, SRISK-006, SRISK-012 | RES-014 a RES-017, RES-024 | ADR-004, ADR-005 | Destino/resposta incompatível é recusado; provider possui matriz de risco/dados. |

## Cobertura

- Security Requirements: **34 de 34**.
- Assets: **20 de 20**.
- Threats: **22 de 22**.
- Security Risks: **15 de 15**.
- Security Research Dependencies: **12 de 12**.
- Trust Boundaries: **8 de 8**.

Resultado: **PASSED**.

## Extensão Technical Research — 2026-10-06

As 12 dependências `SRES-*` foram preservadas e mapeadas aos `RES-*`, incluindo redaction/observabilidade e supply chain. Evidência, assets, requirements, ameaças, riscos, ADR e impacto arquitetural estão consolidados em [`RESEARCH_MATRIX.md`](../research/RESEARCH_MATRIX.md). Resultado: **PASSED**.
