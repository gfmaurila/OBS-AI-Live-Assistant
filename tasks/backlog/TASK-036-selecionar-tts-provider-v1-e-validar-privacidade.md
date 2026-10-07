# TASK-036 — Selecionar TTS Provider V1 e validar privacidade

## Objetivo

Selecionar TTS Provider V1 e validar privacidade como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-09

## Contexto

Esta Task entrega uma unidade independente do EPIC-09 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-033**. Wave: **5**.

## Requirements

RF-023 a RF-025; RNF-002, RNF-005, RNF-011, RNF-017; SEC-005, SEC-007, SEC-024, SEC-033, SEC-034.

## ADRs

ADR-004, ADR-006.

## Dependências

TASK-003, TASK-005, TASK-018.

## Bloqueia

TASK-037.

## Prioridade

P1

## Complexidade

M

## Risco

HIGH

## Arquivos/áreas esperadas

matriz TTS; vozes/formatos; termos; decisão de produto.

## Implementação esperada

Comparar Windows TTS e providers cloud candidatos, revalidar termos/capabilities e registrar a seleção mínima compatível com o protótipo de áudio.

## Segurança

Cobrir SEC-005, SEC-007, SEC-024, SEC-033, SEC-034. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

CONTRACT design review; SECURITY review; evidência documental.

## Critérios de aceite

- Provider selecionado possui matriz de dados, formatos, cancellation, custo/usage e riscos; texto continua independente de TTS.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-036-<descricao>` determinável.

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
