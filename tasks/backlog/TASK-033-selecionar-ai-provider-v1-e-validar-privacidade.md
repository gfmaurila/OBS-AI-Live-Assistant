# TASK-033 — Selecionar AI Provider V1 e validar privacidade

## Objetivo

Selecionar AI Provider V1 e validar privacidade como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-08

## Contexto

Esta Task entrega uma unidade independente do EPIC-08 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com chat foundation**. Wave: **5**.

## Requirements

RF-016 a RF-018; RNF-005, RNF-011, RNF-017, RNF-028; SEC-007, SEC-009, SEC-024, SEC-033, SEC-034.

## ADRs

ADR-004, ADR-005.

## Dependências

TASK-003, TASK-005, TASK-018.

## Bloqueia

TASK-034.

## Prioridade

P0

## Complexidade

M

## Risco

HIGH

## Arquivos/áreas esperadas

matriz de providers; termos; capacidades; decisão de produto.

## Implementação esperada

Revalidar documentação oficial vigente, comparar providers candidatos e registrar a seleção mínima do V1, dados enviados, retenção, auth, quotas e resposta a comprometimento.

## Segurança

Cobrir SEC-007, SEC-009, SEC-024, SEC-033, SEC-034. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

CONTRACT design review; SECURITY review; evidência documental.

## Critérios de aceite

- Ao menos um provider é selecionado sem tornar vendor obrigatório na arquitetura; riscos/dados/capabilities ficam explícitos; Ollama permanece fora salvo decisão.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-033-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Security Gate; Architecture Gate; Acceptance Gate; Documentation Gate; Code Review Gate.

## Status

BACKLOG
