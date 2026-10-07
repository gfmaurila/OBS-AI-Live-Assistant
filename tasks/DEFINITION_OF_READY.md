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

`TASK-001` satisfaz os critérios e está em `ready/`. Todas as demais Tasks dependem direta ou transitivamente de trabalho ainda não concluído e permanecem em `backlog/`. `READY` não autoriza execução por si só; a implementação ainda exige autorização do cliente.
