# GitFlow

## Branches

- Permanentes: `main`, `develop` e `hml`.
- Trabalho de Task: `feature/task-<descricao>`, criada a partir de `develop` sincronizada.
- Preparação de release: `release/1.0.0XXXX`, criada a partir do estado homologado e aprovado de `hml`.

Nenhuma Task trabalha diretamente em `main`, `develop` ou `hml`.

## Fluxo oficial de entrega

```text
feature/task-*
      |
      v
   develop
      |
      v
     hml
      |
      v
release/1.0.0XXXX
      |
      v
    main
```

## Feature para develop

Cada Task nasce de `develop` sincronizada e segue:

```text
Task
-> Validation
-> Code Review
-> Prompt Archive
-> Secret Check
-> Commit
-> Push
-> Pull Request
-> Merge para develop
```

O merge de `feature/task-*` para `develop` pode ocorrer automaticamente quando validações, review, segurança, sincronização remota e todos os Quality Gates obrigatórios estiverem aprovados. Findings Critical ou High não resolvidos bloqueiam o merge.

## Develop para hml

`develop` representa a integração contínua das Tasks aprovadas. A promoção não ocorre após cada Task individual; ela acontece quando existe um conjunto coerente de alterações pronto para homologação.

```text
develop
-> HML GATE
-> pull request
-> hml
```

`hml` representa **HOMOLOGAÇÃO**. A promoção exige autorização e gates próprios.

## Convenção e criação da release

O formato oficial é `release/1.0.0XXXX`, em que `XXXX` representa a sequência incremental da release. Exemplos válidos:

- `release/1.0.00001`
- `release/1.0.00002`
- `release/1.0.00003`
- `release/1.0.00004`

Esta convenção torna específica a regra genérica anterior de branches de release; não cria um fluxo concorrente. A branch é criada somente quando a versão homologada em `hml` estiver aprovada:

```text
hml
-> aprovação da versão homologada
-> release/1.0.0XXXX
```

Uma branch `release/*` aceita apenas:

- ajustes finais;
- correções de release;
- documentação;
- versionamento;
- metadados;
- correções bloqueadoras aprovadas.

Features novas são proibidas em `release/*`.

## Release para main

```text
release/1.0.0XXXX
-> PRODUCTION GATE / Final Quality Gate
-> pull request
-> main
```

`main` representa **PRODUÇÃO / VERSÃO ESTÁVEL**. Nenhuma feature é mergeada diretamente em `main`.

Após o merge aprovado:

```text
main
-> tag
-> GitHub Release
-> sincronização das branches necessárias
```

A criação de tag e GitHub Release só ocorre para uma release real aprovada.

## Sincronização pós-release

Alterações realizadas exclusivamente em `release/*` não podem permanecer apenas em `main`. Depois da release, deve-se avaliar e executar, por pull request e com preservação do histórico:

- `main -> develop`;
- `main -> hml`, quando aplicável.

A sincronização deve evitar perda de correções, versionamento ou metadados. Force push e push para branch alternativa não podem ser usados para contornar conflitos ou gates.

## Limites de automação

A autorização automática aplica-se somente a `feature/task-* -> develop`. Promoções `develop -> hml`, criação de `release/1.0.0XXXX`, merge `release/* -> main`, tag e GitHub Release exigem gates e autorização próprios. A limpeza de uma feature ocorre somente depois de o merge ter sido confirmado.
