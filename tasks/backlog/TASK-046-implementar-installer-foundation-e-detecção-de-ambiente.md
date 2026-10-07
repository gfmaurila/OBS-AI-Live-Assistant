# TASK-046 — Implementar installer foundation e detecção de ambiente

## Objetivo

Implementar installer foundation e detecção de ambiente como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-12

## Contexto

Esta Task entrega uma unidade independente do EPIC-12 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **7**.

## Requirements

RF-032; RNF-020, RNF-024, RNF-025; SEC-007, SEC-027, SEC-029.

## ADRs

ADR-001, ADR-007, ADR-010.

## Dependências

TASK-003, TASK-045.

## Bloqueia

TASK-047, TASK-048.

## Prioridade

P0

## Complexidade

XL

## Risco

HIGH

## Arquivos/áreas esperadas

installer project; component manifest; OBS/Windows detection.

## Implementação esperada

Implementar installer aprovado para detectar arquitetura/versão/layout/prerequisites, exigir OBS fechado e instalar Core/plugin como componentes distintos.

## Segurança

Cobrir SEC-007, SEC-027, SEC-029. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

INSTALLER; OBS COMPATIBILITY; SECURITY; FAILURE.

## Critérios de aceite

- Incompatibilidade é detectada antes de mudança irreversível; instalação parcial reverte; configuração externa do OBS não é alterada.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-046-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Installer Gate; OBS Compatibility Gate; Security Gate; Failure Gate; Acceptance Gate; Code Review Gate.

## Status

BACKLOG
