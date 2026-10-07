# TASK-047 — Implementar upgrade, repair, uninstall e rollback

## Objetivo

Implementar upgrade, repair, uninstall e rollback como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-12

## Contexto

Esta Task entrega uma unidade independente do EPIC-12 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **7**.

## Requirements

RF-033 a RF-035; RNF-003, RNF-012, RNF-020, RNF-021; SEC-009, SEC-013, SEC-023, SEC-028.

## ADRs

ADR-004, ADR-005, ADR-007.

## Dependências

TASK-021, TASK-046.

## Bloqueia

TASK-049, TASK-050.

## Prioridade

P0

## Complexidade

XL

## Risco

HIGH

## Arquivos/áreas esperadas

installer lifecycle; migrations; rollback; removal policy.

## Implementação esperada

Implementar upgrade version-aware, repair por manifesto, uninstall por categorias e rollback, após registrar a decisão explícita sobre dados/logs/secrets.

## Segurança

Cobrir SEC-009, SEC-013, SEC-023, SEC-028. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INSTALLER; DATABASE; SECURITY; FAILURE; OBS COMPATIBILITY.

## Critérios de aceite

- Falha não vira sucesso; dados/secrets seguem escolha explícita; outra integração/OBS Truck Live Optimizer nunca é removida; rollback restaura estado válido.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-047-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Installer Gate; Database Gate; Security Gate; Failure Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
