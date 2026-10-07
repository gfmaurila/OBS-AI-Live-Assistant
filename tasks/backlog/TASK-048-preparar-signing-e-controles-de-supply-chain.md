# TASK-048 — Preparar signing e controles de supply chain

## Objetivo

Preparar signing e controles de supply chain como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-12

## Contexto

Esta Task entrega uma unidade independente do EPIC-12 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-047**. Wave: **7**.

## Requirements

RNF-003, RNF-019, RNF-020, RNF-028; SEC-008, SEC-027 a SEC-029, SEC-032.

## ADRs

ADR-007, ADR-010.

## Dependências

TASK-004, TASK-045, TASK-046.

## Bloqueia

TASK-052.

## Prioridade

P1

## Complexidade

L

## Risco

HIGH

## Arquivos/áreas esperadas

dependency inventory; SBOM; vulnerability scan; Authenticode verification.

## Implementação esperada

Criar gates reproduzíveis de dependências e artefatos, SBOM quando aplicável, assinatura/timestamp e verificação de hash/origem sem armazenar chave privada no repositório.

## Segurança

Cobrir SEC-008, SEC-027 a SEC-029, SEC-032. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

SECURITY; INSTALLER; tamper tests.

## Critérios de aceite

- Dependências têm origem/versão; pacote adulterado é recusado; Critical/High bloqueiam release; credencial de signing não reside em repo/dev comum.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-048-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Security Gate; Supply Chain Gate; Installer Gate; Secret Scan; Documentation Gate; Code Review Gate.

## Status

BACKLOG
