# Ordem de implementação

O Dependency Graph prevalece sobre a ordem visual. Dentro de uma Wave, Tasks marcadas `PARALLEL SAFE` podem ocorrer em paralelo somente após suas dependências e desde que não editem as mesmas áreas centrais.

| Wave | Objetivo | Tasks | Observações |
|---:|---|---|---|
| 1 | Foundation e validação inicial | TASK-001 a TASK-004 | TASK-001 é a única READY; compatibilidade e quality tooling seguem a estrutura. |
| 2 | Core, contracts, configuração e security policies | TASK-005 a TASK-016 | Contracts precedem adapters; Domain pode avançar em paralelo com ports. |
| 3 | Infrastructure local | TASK-017 a TASK-023, TASK-043 | Secrets, SQLite, observability e contrato/.NET IPC. |
| 4 | OBS e IPC nativo | TASK-024 a TASK-029 | Código in-process mínimo; integração sequencial nos pontos de alto risco. |
| 5 | Providers e chat | TASK-030 a TASK-036, TASK-044 | Seleções documentadas precedem adapters concretos. |
| 6 | TTS, áudio e UI | TASK-037 a TASK-042 | TASK-038 valida ADR-006 antes da implementação definitiva. |
| 7 | Installer e supply chain | TASK-045 a TASK-048 | TASK-045 valida ADR-007 antes do installer definitivo. |
| 8 | Integração e hardening | TASK-049 a TASK-051 | Regressão, E2E, segurança, falhas, desempenho e matriz final. |
| 9 | Release preparation | TASK-052 | Prepara evidência; não promove branches nem cria release. |

## Regras de paralelismo

- `PARALLEL SAFE` significa dependências estabilizadas e áreas predominantemente distintas; não elimina integração/rebase.
- `SEQUENTIAL` é obrigatório para contratos ainda instáveis, decisão → adapter, protocolo → peers, protótipo → implementação definitiva e integração/hardening.
- Tasks que alteram `ObsAi.Application`, contratos IPC, schema/migrations, plugin lifecycle ou installer manifest não devem executar concorrentemente com outra mudança na mesma área sem coordenação explícita.
- O backlog possui **24** Tasks marcadas `PARALLEL SAFE` e **28** sequenciais.

## Primeira Task

**TASK-001 — Criar a fundação da solution e do tooling.** Ela não depende de decisão de provider, ADR proposto ou client decision. A execução só começa após nova autorização.
