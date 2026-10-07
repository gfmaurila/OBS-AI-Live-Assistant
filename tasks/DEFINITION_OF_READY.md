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

`TASK-001` a `TASK-004` passaram para `DONE`. Nenhuma Task está formalmente `READY`. `TASK-005`, `TASK-006` e `TASK-024` possuem todas as dependências concluídas e são candidatas a READY, mas permanecem em `backlog/` até validação formal destes critérios e nova autorização do cliente. As demais Tasks ainda dependem direta ou transitivamente de trabalho não concluído. `READY` não autoriza execução por si só; a implementação sempre exige autorização do cliente.
