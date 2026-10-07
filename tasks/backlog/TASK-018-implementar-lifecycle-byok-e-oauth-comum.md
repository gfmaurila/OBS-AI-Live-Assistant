# TASK-018 — Implementar lifecycle BYOK e OAuth comum

## Objetivo

Implementar lifecycle BYOK e OAuth comum como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-03

## Contexto

Esta Task entrega uma unidade independente do EPIC-03 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL após TASK-017**. Wave: **3**.

## Requirements

RF-005, RF-017, RF-018; RNF-003, RNF-006, RNF-011; SEC-008 a SEC-013, SEC-033.

## ADRs

ADR-004, ADR-005.

## Dependências

TASK-014, TASK-017.

## Bloqueia

TASK-030, TASK-033, TASK-036, TASK-041.

## Prioridade

P0

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

credential lifecycle; OAuth state; masking; revocation guidance.

## Implementação esperada

Implementar inclusão, validação, substituição, remoção e estado de OAuth/credenciais, com scopes mínimos e sem client secret embarcado.

## Segurança

Cobrir SEC-008 a SEC-013, SEC-033. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; INTEGRATION; SECURITY; CONTRACT.

## Critérios de aceite

- Credencial removida não autentica nova operação; token expirado/revogado não é válido; UI recebe apenas metadata mascarada.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-018-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Integration Test Gate; Contract Gate; Security Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
