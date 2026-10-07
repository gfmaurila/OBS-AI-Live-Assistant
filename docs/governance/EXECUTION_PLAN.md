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

A sequência obrigatória mais detalhada em `AGENTS.md` permanece autoritativa. Esta visão destaca a progressão imediata do projeto. Knowledge, Requirements e Security Requirements estão `CONCLUÍDOS`, com seus Quality Gates `PASSED`. A próxima atividade permitida é a preparação de Research e Architecture conforme dependências e autorizações próprias. Advanced Skills continua `BLOCKED`, classificada como não bloqueadora para as fases concluídas, e não é inserida como gate obrigatório.

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
