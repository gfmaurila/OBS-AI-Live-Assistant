# TASK-038 — Executar protótipo de roteamento TTS e validar ADR-006

## Objetivo

Executar protótipo de roteamento TTS e validar ADR-006 como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-09

## Contexto

Esta Task entrega uma unidade independente do EPIC-09 e segue a Architecture Baseline aprovada. Paralelismo: **SEQUENTIAL**. Wave: **6**.

## Requirements

RF-023 a RF-025; RNF-001, RNF-002, RNF-015, RNF-025; SEC-005, SEC-015 a SEC-021.

## ADRs

ADR-001, ADR-003, ADR-006.

## Dependências

TASK-024, TASK-029.

## Bloqueia

TASK-037, TASK-039.

## Prioridade

P0

## Complexidade

XL

## Risco

HIGH

## Arquivos/áreas esperadas

Application Audio Capture; source nativa spike; ADR-006.

## Implementação esperada

Prototipar primeiro captura de áudio do processo com áudio sintético/controlado e, se necessário, source nativa; medir latência, cancelamento, mute/volume, monitoramento, eco, tracks, device changes e crash antes do adapter TTS definitivo.

## Segurança

Cobrir SEC-005, SEC-015 a SEC-021. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

OBS COMPATIBILITY; INTEGRATION; FAILURE; SECURITY; performance.

## Critérios de aceite

- Evidências permitem promover ADR-006 a ACCEPTED ou registrar blocker; nenhuma síntese/rede ocorre em callback OBS; rota não afeta texto.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-038-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Native Build Gate; OBS Compatibility Gate; Integration Test Gate; Failure Gate; Security Gate; Architecture Gate.

## Status

BACKLOG
