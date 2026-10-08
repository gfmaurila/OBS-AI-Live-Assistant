# TASK-009 — Implementar validação e normalização de entradas

## Objetivo

Implementar validação e normalização de entradas como incremento independente, verificável e alinhado à arquitetura aprovada.

## Epic

EPIC-02

## Contexto

Esta Task entrega uma unidade independente do EPIC-02 e segue a Architecture Baseline aprovada. Paralelismo: **PARALLEL SAFE com TASK-007**. Wave: **2**.

## Requirements

RF-006, RF-007; RNF-004, RNF-005; SEC-001, SEC-002, SEC-019, SEC-024.

## ADRs

ADR-004, ADR-008.

## Dependências

TASK-005, TASK-006.

## Bloqueia

TASK-010, TASK-011, TASK-015, TASK-031.

## Prioridade

P0

## Complexidade

M

## Risco

HIGH

## Arquivos/áreas esperadas

schemas; normalização; encoding; size limits.

## Implementação esperada

Implementar representação comum e validação de campos, encoding, tamanho, identidade operacional mínima e origem antes de qualquer sink.

## Segurança

Cobrir SEC-001, SEC-002, SEC-019, SEC-024. Tratar todos os dados externos como não confiáveis, aplicar least privilege e nunca usar secrets reais em testes ou artefatos.

## Testes obrigatórios

UNIT; CONTRACT; SECURITY.

## Critérios de aceite

- Input ausente, malformado ou oversize falha antes de provider/fila; dados normalizados preservam apenas o necessário.
- O escopo não introduz capability, provider ou infraestrutura fora da V1 aprovada.
- A documentação e a matriz de rastreabilidade são atualizadas quando o contrato mudar.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-009-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Contract Gate; Security Gate; Acceptance Gate; Code Review Gate.

## Plano de implementação

Incremento entregue em `src/ObsAi.Application/Validation` (namespace `ObsAi.Application.Validation`), session-agnostic e vendor-neutral, preservando os contratos da TASK-005, o domínio da TASK-006, o lifecycle da TASK-007 e as filas da TASK-008:

1. Limites configuráveis sem números de produto: `InputValidationLimits.Create(maximumTextLength, maximumReferenceLength)` valida e congela dimensões `>= 1` (SEC-002).
2. Diagnóstico seguro: `InputRejectionReason` classifica ausente, malformado, encoding, oversize e timestamp; `InputRejection` nunca ecoa payload e tem `ToString` redigido (RNF-004, SEC-024).
3. Representação comum: `NormalizedChatMessage` com exatamente `ProviderId`, `ChannelReference`, `SenderReference`, `MessageReference`, `Text` e `ReceivedAtUtc` (RF-007, SEC-024).
4. Fail-closed: `ChatInputValidationResult` aceita (`Message`) OU rejeita (`Rejection`), construção apenas por fábricas.
5. Validação/normalização: `ChatInputValidator.Validate(ChatMessage, InputValidationLimits)` valida schema, encoding (surrogate órfão em texto ou referência), controle de tamanho bruto antes da normalização (SEC-019) e normalizado, e timestamp; normaliza texto com NFC + trim + colapso de espaços e aparas referências; rejeições acontecem antes de qualquer provider, trigger, moderação ou fila (RF-006, RF-007, RNF-004, SEC-001, SEC-002).

## Matriz critério -> teste

| Critério | Teste determinístico |
|---|---|
| Input ausente/malformado/oversize falha antes de provider/fila | `ChatInputValidationTests`, `ChatInputValidationSecurityTests` (flood adversarial de 5.000 oversize) |
| Dados normalizados preservam apenas o necessário | `ChatInputValidationTests.PreservesOnlyMinimalOperationalIdentityAndOrigin`, `ChatInputValidationContractTests` |
| Encoding e esquema validados antes de qualquer sink | `ChatInputValidationTests` (surrogate órfão, controles, timestamp), `ChatInputValidationSecurityTests` |
| Entradas equivalentes produzem campos comuns | `ChatInputValidationTests` (espaços, NFC/NFD) |
| Limites configuráveis sem números de produto | `ChatInputValidationTests.InputValidationLimits`, `ApplicationInputValidationArchitectureTests` |
| Diagnóstico não vaza payload/identidade | `ChatInputValidationSecurityTests.RejectionDiagnostic...`, `ChatInputValidationContractTests` |
| Escopo sem capability/provider/infra fora da V1 | `ApplicationInputValidationArchitectureTests` |

Evidence executions: 228/228 testes determinísticos verdes; quality gates RESTORE/BUILD/TESTES/FORMAT `PASSED`; Architecture Gate e Security Audit `PASSED`; Code Review sem Critical/High; Secret Scan 3 varreduras com 0 hits.

## Definition of Ready

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas `DONE`;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-009-<descricao>` determinável.

## Definition of Done

- implementação, testes e documentação concluídos;
- critérios de aceite e gates aplicáveis aprovados;
- Code Review e Security Review aplicável sem Critical/High;
- Secret Scan e Prompt Traceability aprovados;
- commit, push, PR, validação, merge em `develop` e cleanup concluídos.

## Quality Gates

Build Gate; Unit Test Gate; Contract Gate; Security Gate; Acceptance Gate; Code Review Gate.

## Status

DONE

## Registro de finalização

- Implementação mergeada em `develop` via PR [#25](https://github.com/gfmaurila/OBS-AI-Live-Assistant/pull/25) (squash, commit `4c3acaa2c6784958ad6ea5f51a43e861a2c8aa03`) em 2026-10-08; branch `feature/task-TASK-009-input-validation`.
- Validação pós-merge da implementação em `develop`: Restore, Build (0 avisos/0 erros), 225/225 testes determinísticos e Format — todos `PASSED`; `tooling/quality-gates.ps1` com todos os gates `PASSED`. A finalização adicionou 3 testes de regressão (228/228) para limite bruto antes de varredura/normalização e encoding de referências.
- Code Review (0 Critical/0 High/0 Medium/0 Low), Security Audit e Architecture Gate: `PASSED`.
- Secret Scan: 3 varreduras do conteúdo staged, nenhum hit real (falsos positivos de nomes de fragmentos `Secret`/`Token`/`ApiKey` e marcador de teste `SECRET-IDENTITY-42` registrados). Prompts arquivados: `docs/prompts/history/prompt25.md` e `docs/prompts/history/prompt26.md`.
- Definition of Done integralmente satisfeito: implementação, testes, documentação, revisões, secret scan, prompt traceability, commit, push, PR, validação pós-merge, rastreabilidade, finalização e cleanup concluídos.
- Detalhamento canônico: `docs/architecture/INPUT_VALIDATION.md`; relatório: `docs/reports/TASK-009_REPORT.md`.
