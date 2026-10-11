# Sincronização das Tasks com o GitHub Project

## Objetivo

Projetar o backlog canônico do OBS-AI-Live-Assistant em GitHub Issues e no GitHub Project V2 #22, sem transformar o board em fonte de verdade e sem executar Tasks de produto. A sincronização é unidirecional: Markdown canônico para GitHub.

## Repositório e Project

- Repositório: `gfmaurila/OBS-AI-Live-Assistant`.
- Project pessoal: `gfmaurila`, número `22`.
- Board: <https://github.com/users/gfmaurila/projects/22/views/1>.
- Branch administrativa: `feature/github-project-22-task-sync` para integração em `develop`.

## Fonte da verdade

Os arquivos abaixo prevalecem sobre Issues, campos do Project e automações:

- `tasks/BACKLOG.md`;
- `tasks/README.md`;
- `tasks/DEPENDENCY_GRAPH.md`;
- `tasks/IMPLEMENTATION_ORDER.md`;
- `tasks/TRACEABILITY_MATRIX.md`;
- `tasks/DEFINITION_OF_READY.md`;
- `tasks/DEFINITION_OF_DONE.md`;
- arquivos individuais em `tasks/backlog/`, `tasks/ready/`, `tasks/in-progress/`, `tasks/review/`, `tasks/blocked/` e `tasks/done/`.

O sincronizador valida IDs únicos, correspondência dos metadados com o backlog, dependências existentes e ausência de ciclos antes de consultar ou alterar o GitHub.

## Formato das Issues

Cada Task possui uma Issue canônica intitulada `[TASK-XXX] Título oficial` e identificada por um marcador estável:

```html
<!-- obs-ai-task-id:TASK-XXX -->
```

O conteúdo derivado fica entre `<!-- obs-ai-managed:start -->` e `<!-- obs-ai-managed:end -->`. Reexecuções substituem apenas esse bloco; texto humano fora dele é preservado. Uma Issue pode ser localizada pelo marcador ou pelo ID no título. Duas candidatas para a mesma Task geram conflito e nunca provocam a criação de uma terceira Issue.

O bloco gerenciado registra identificação, objetivo, escopo, critérios de aceite, Requirements, ADRs, dependências navegáveis, testes, DoR, DoD, arquivo canônico, backlog, grafo e PRs explicitamente citados na Task.

## Campos do Project

O sincronizador reutiliza campos com nome e tipo compatíveis e cria somente os ausentes:

| Campo | Tipo | Valores gerenciados |
|---|---|---|
| `Status` | Single select | Backlog, Ready, In Progress, Code Review, Validation, Done, Blocked |
| `Priority` | Single select | P0, P1 |
| `Epic` | Text | Epic canônico |
| `Wave` | Number | Wave canônica |
| `Complexity` | Single select | S, M, L, XL |
| `Risk` | Single select | LOW, MEDIUM, HIGH |
| `Task ID` | Text | `TASK-XXX` |
| `Dependencies` | Text | IDs predecessores ou `Nenhuma` |

Ao adicionar opções a um campo single select, a atualização reenvia todas as opções existentes com seus IDs, cores e descrições. Isso preserva identidade e valores já atribuídos. Tipo incompatível, nome duplicado ou outra ambiguidade bloqueia a escrita; nenhum campo ou item é excluído, arquivado ou renomeado.

## Semântica dos status

- `Backlog`: Task registrada, ainda não autorizada ou elegível.
- `Ready`: DoR formalmente aprovada.
- `In Progress`: execução autorizada e iniciada.
- `Code Review`: implementação submetida para revisão.
- `Validation`: gates, PR ou validação pós-merge em andamento.
- `Done`: DoD e finalização administrativa concluídas.
- `Blocked`: impedimento formal registrado.

Dependências satisfeitas não promovem uma Task para `Ready`, e merge de PR não promove uma Task para `Done`. O status vem exclusivamente do arquivo canônico.

## Dependências

O campo `Dependencies` mantém os IDs oficiais. No corpo da Issue, predecessoras com Issue conhecida usam referências navegáveis, por exemplo `TASK-009 — #9`. O sincronizador não inverte relações nem cria dependências com base em inferência.

## Comandos

O modo padrão é dry-run; `-Apply` precisa ser informado explicitamente.

```powershell
.\tooling\github-project-sync.ps1 `
  -Owner gfmaurila `
  -Repository OBS-AI-Live-Assistant `
  -ProjectNumber 22 `
  -DryRun
```

```powershell
.\tooling\github-project-sync.ps1 `
  -Owner gfmaurila `
  -Repository OBS-AI-Live-Assistant `
  -ProjectNumber 22 `
  -Apply `
  -VerboseReport
```

```powershell
.\tooling\github-project-sync.ps1 `
  -Owner gfmaurila `
  -Repository OBS-AI-Live-Assistant `
  -ProjectNumber 22 `
  -TaskId TASK-013 `
  -Apply
```

Testes administrativos sem rede:

```powershell
powershell -ExecutionPolicy Bypass -File tooling\tests\github-project-sync.tests.ps1
```

## Permissões GitHub

A execução manual aceita, nesta ordem, `GH_TOKEN`, `GITHUB_TOKEN`, credencial da GitHub CLI ou credencial do helper seguro do Git. O token não é exibido, gravado em checkpoint ou incluído em mensagens de erro. Para um token classic, são necessários `repo` para Issues/PRs e `project` para leitura e escrita do Project V2. Tokens fine-grained ou GitHub Apps devem conceder acesso equivalente apenas ao repositório e Project necessários.

Para autorizar a GitHub CLI por fluxo web, sem copiar tokens:

```powershell
gh auth login --hostname github.com --git-protocol https --web --scopes repo,workflow,project
gh auth status
```

## GitHub Actions

O workflow `.github/workflows/github-project-sync.yml` executa em alterações de `tasks/**` integradas em `develop` e por acionamento manual. Ele não cria commits, branches, releases ou deploys e não executa código de produto.

O `GITHUB_TOKEN` padrão não deve ser considerado suficiente para um Project pessoal. A automação fica segura como `PENDING_CONFIGURATION` enquanto o secret `PROJECT_SYNC_TOKEN` não existir. O secret deve conter uma credencial dedicada com acesso mínimo a Issues do repositório e ao Project #22. Na ausência do secret, o job registra a pendência e encerra sem escrita.

## Idempotência

Antes de escrever, o sincronizador pagina e reconcilia Issues e itens. Uma operação só é emitida quando título, bloco gerenciado, estado da Issue, vínculo ou valor de campo diverge. Após `-Apply`, ele consulta tudo novamente e exige plano vazio. Uma segunda execução em dry-run deve informar zero operações.

Não são usados timestamps de sincronização no conteúdo gerenciado, pois causariam mutações em toda execução.

## Checkpoints e recuperação

Com `-VerboseReport`, a aplicação final emite, para cada Task, ID, número e URL da Issue, ID do item, status esperado, status observado e resultado da verificação. O checkpoint é derivado do GitHub e não contém credenciais.

Após interrupção:

1. execute dry-run novamente;
2. corrija autenticação ou conflito informado;
3. execute `-Apply`;
4. aguarde a reconciliação pós-aplicação;
5. execute um segundo dry-run e confirme zero operações.

Issues e itens já criados são reutilizados. O sincronizador não apaga, arquiva ou recria registros para recuperar uma execução parcial.

## Tratamento de falhas

- 401 e 403 comuns falham imediatamente para correção de autenticação ou permissão;
- 429, 500, 502, 503 e 504 usam retry exponencial limitado;
- rate limits explícitos respeitam `Retry-After` ou `X-RateLimit-Reset`, com espera limitada;
- erros GraphQL são sanitizados;
- falta do escopo `project` interrompe antes de qualquer escrita;
- duplicidades, tipos incompatíveis e valores ambíguos são conflitos, não oportunidades para exclusão.

## Segurança de credenciais

- nunca informe tokens por argumento de linha de comando;
- nunca cole tokens em Issues, logs, prompts, Markdown ou arquivos de configuração;
- use o armazenamento seguro da GitHub CLI, o helper do Git ou Actions Secrets;
- não habilite debug HTTP com headers de autorização;
- respostas e mensagens passam por redaction de formatos conhecidos de token;
- fixtures e testes não usam rede nem credenciais reais.

## Inconsistências

O Markdown canônico prevalece para os campos gerenciados. Conteúdo humano fora do bloco gerenciado e campos não gerenciados do Project são preservados. Se duas Issues representarem a mesma Task, se um campo tiver tipo incompatível ou se a contagem canônica divergir, registre e resolva o conflito sem excluir dados.

## Adicionar novas Tasks

1. adicione a Task pelos documentos e diretório de estado canônicos;
2. valide ID, metadados e dependências;
3. integre a alteração em `develop` pelos gates normais;
4. execute dry-run ou aguarde o workflow configurado;
5. aplique e confirme plano final vazio.

Nenhuma Task é automaticamente promovida para `Ready`.

## Finalizar Tasks

Somente após a DoD e a finalização administrativa, mova o arquivo para `tasks/done/` e registre `DONE` nas fontes canônicas. A sincronização então define `Status = Done` e fecha a Issue. PR mesclado isoladamente não é evidência suficiente.

## Verificar integridade

Uma execução íntegra possui:

- uma Issue por ID canônico;
- nenhum conflito de duplicidade;
- um item de Project por Issue canônica;
- estado aberto/fechado coerente;
- campos iguais aos documentos;
- dry-run final com zero operações;
- testes administrativos e Quality Gates aprovados.

## Limitações conhecidas

- Projects V2 exige escopo separado `project`; acesso administrativo ao repositório não o substitui.
- A credencial do workflow é configuração externa e não pode ser criada pelo repositório.
- O sincronizador não altera layouts ou views do Project.
- Relações nativas de dependência não são criadas: os vínculos navegáveis no corpo e o campo textual são o baseline compatível.
- A sincronização é unidirecional; alterações manuais em campos gerenciados podem ser revertidas para o estado canônico.
