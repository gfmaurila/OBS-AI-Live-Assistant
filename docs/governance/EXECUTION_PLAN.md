# Plano de execução

## Ordem das fases do projeto

```text
KIT IA DEV
-> DOCUMENTATION BASELINE
-> KNOWLEDGE QUALITY GATE
-> REQUIREMENTS
-> RESEARCH
-> ARCHITECTURE
-> ADR
-> BACKLOG
-> TASKS
-> IMPLEMENTATION
```

A sequência obrigatória mais detalhada em `AGENTS.md` permanece autoritativa. Knowledge, Requirements, Security Requirements, Research, Architecture, ADRs, Backlog, Dependency Graph e Implementation Tasks estão concluídos com seus gates documentais `PASSED`. Implementation Readiness está `READY`, mas nenhuma Task de implementação começa sem autorização explícita do cliente. Advanced Skills continua `BLOCKED`, classificada como não bloqueadora.

## Protocolo de execução das Implementation Tasks

1. selecionar a primeira Task `READY` por prioridade e ordem topológica;
2. confirmar que todas as dependências estão `DONE` em `develop`;
3. sincronizar `develop` e criar `feature/task-TASK-XXX-<descricao>`;
4. executar a implementação completa daquela Task;
5. executar somente os testes e Quality Gates aplicáveis registrados no arquivo da Task;
6. realizar Code Review e Security Review quando aplicável;
7. corrigir findings obrigatórios;
8. executar Secret Scan e arquivar o prompt;
9. commit, push, PR e PR Validation;
10. fazer merge automático somente `feature/task-* -> develop` quando todos os gates passarem;
11. validar `develop`, excluir a feature local/remota e confirmar Working Tree `CLEAN`;
12. atualizar a Task para `DONE`, recalcular dependências e promover as novas Tasks elegíveis a `READY`;
13. selecionar a próxima Task somente sob a autorização aplicável.

Uma autorização futura para uma Task individual cobre o setup dessa Task até o fim do fluxo acima, mas não autoriza automaticamente a Task seguinte nem promoções para `hml`, `release/*` ou `main`.

## Fluxo de Task

```text
develop sincronizada
-> feature/task-<descricao>
-> execução
-> validação
-> Code Review
-> correções obrigatórias
-> arquivamento do prompt
-> verificação de secrets
-> commit
-> push
-> pull request
-> validação do PR
-> merge para develop
-> validação pós-merge
-> limpeza da feature
```

O merge automático termina em `develop` e se aplica somente quando todos os gates obrigatórios estiverem aprovados.

## Fluxo de entrega

```text
develop
-> Quality Gate de homologação
-> pull request
-> hml
-> aprovação da versão homologada
-> release/1.0.0XXXX
-> Final Quality Gate
-> pull request
-> main
-> tag
-> GitHub Release
-> sincronização pós-release
```

O fluxo de Task e o fluxo de entrega são níveis diferentes. Uma Task individual não promove automaticamente `develop` para `hml`, não cria uma branch de release e não altera `main`. Tags e GitHub Releases só são criadas para releases reais explicitamente aprovadas.
