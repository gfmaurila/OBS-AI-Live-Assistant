# Definition of Ready

Uma Task somente pode mudar de `BACKLOG` para `READY` quando todos os itens abaixo estiverem comprovados:

- objetivo, resultado observável e limites estão claros em pt-BR;
- Requirements `RF-*`, `RNF-*` e `SEC-*` aplicáveis estão mapeados;
- ADRs aplicáveis e seus estados são conhecidos;
- todas as dependências do Dependency Graph estão `DONE` e integradas em `develop`;
- critérios de aceite são verificáveis;
- tipos de testes obrigatórios estão definidos;
- impacto de segurança, trust boundaries e dados afetados estão identificados;
- não existe `ARCHITECTURE_BLOCKER`, decisão obrigatória ou finding Critical/High aberto;
- o escopo cabe em branch, commit, PR, review e quality gate próprios;
- arquivos/áreas esperadas e riscos de concorrência com outras Tasks são conhecidos;
- o nome `feature/task-TASK-XXX-<descricao>` pode ser determinado;
- documentação oficial instável necessária será revalidada na execução.

## Aplicação atual

`TASK-001` a `TASK-004` passaram para `DONE`. A `TASK-005` teve DoR formalmente aprovada em 2026-10-07, recebeu autorização explícita, foi implementada e está em `review/`; ainda não está `DONE` porque merge e validação pós-merge são obrigatórios. Nenhuma Task está formalmente `READY`. `TASK-006` e `TASK-024` possuem dependências concluídas e permanecem candidatas a READY, sem execução. As demais ainda dependem direta ou transitivamente de trabalho não concluído.
