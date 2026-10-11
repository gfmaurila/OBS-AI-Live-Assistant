# MASTER PROMPT — GITHUB PROJECTS V2 SYNC
## Sincronização completa das 52 Tasks — Codex

**Projeto:** OBS-AI-Live-Assistant  
**Repositório:** `gfmaurila/OBS-AI-Live-Assistant`  
**Diretório local:** `D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant`  
**Kit IA Dev:** `D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev`  
**GitHub Project:** `https://github.com/users/gfmaurila/projects/22/views/1?layout_template=board`  
**Project owner:** `gfmaurila`  
**Project number:** `22`  
**Branch de destino:** `develop`  
**Idioma:** Português do Brasil  
**Executor:** Codex  
**Operação:** Gestão, sincronização e automação do backlog, SEM implementação do produto.

---

# 1. MISSÃO

Você é o agente responsável por integrar a gestão de desenvolvimento do projeto OBS-AI-Live-Assistant ao GitHub Issues e GitHub Projects V2.

Sua missão é:

1. Ler integralmente o setup e a governança do projeto.
2. Identificar as 52 Tasks canônicas.
3. Inspecionar as Issues e os itens existentes no GitHub Project #22.
4. Validar autenticação e permissões.
5. Criar ou atualizar uma Issue por Task, sem duplicações.
6. Vincular as 52 Issues ao Project #22.
7. Sincronizar prioridades, Epics, dependências, Waves e status.
8. Marcar as nove Tasks já concluídas como Done.
9. Preservar as outras 43 como pendentes.
10. Preparar um mecanismo de sincronização automatizada e idempotente.
11. Documentar o funcionamento e os procedimentos de recuperação.
12. Validar o resultado no GitHub.
13. Integrar os arquivos administrativos ao repositório usando feature branch e PR para `develop`.
14. Apresentar relatório completo.
15. Executar STOP.

**Esta não é uma Task de implementação do produto.**

É uma operação de preparação da governança e de integração com GitHub.

Não iniciar qualquer Task de desenvolvimento após concluir.

---

# 2. AUTORIZAÇÃO E LIMITES

O cliente autoriza expressamente:

- Criar até 52 Issues canônicas, uma por Task.
- Atualizar Issues que representem exatamente essas Tasks.
- Fechar as Issues das TASK-001 até TASK-009.
- Manter abertas as Issues das demais Tasks.
- Vincular Issues ao GitHub Project #22.
- Atualizar os campos dessas 52 Tasks no Project #22.
- Configurar campos e opções necessários, desde que isso não destrua configurações existentes.
- Criar scripts administrativos de sincronização.
- Preparar uma automação de sincronização futura.
- Criar feature branch administrativa.
- Fazer commit e push dessa branch.
- Criar PR para `develop`.
- Validar e mesclar o PR, desde que os gates e a governança permitam.
- Executar validação após o merge.

**Não está autorizado:**

- Implementar TASK-010 a TASK-052.
- Iniciar qualquer outra Task do produto.
- Alterar lógica de negócio, APIs, providers, filas, validações, testes do produto ou integrações OBS.
- Modificar `main` ou `hml`.
- Criar releases ou tags.
- Executar deploy.
- Modificar instalações ou configurações do OBS Studio.
- Alterar permissões globais do usuário GitHub sem necessidade.
- Excluir Issues, projetos, campos ou dados preexistentes.
- Criar tokens, divulgar tokens ou imprimir credenciais.
- Ignorar regras de proteção de branch.
- Promover automaticamente qualquer Task para READY.
- Executar as Tasks de desenvolvimento em sequência.
- Habilitar execução autônoma de código de produto nesta operação.

---

# 3. ESTADO INICIAL CONHECIDO

Última conclusão confirmada:

```text
TASK-009: DONE

PR implementação:
#25

Merge implementação:
4c3acaa2c6784958ad6ea5f51a43e861a2c8aa03

PR finalização:
#26

Merge final:
4e5b8c6dc09dd629984311ef0ee87c61af0e3146

Branch:
develop

Tasks planejadas:
52

Tasks DONE:
TASK-001 até TASK-009

Tasks pendentes:
TASK-010 até TASK-052

Tasks formalmente READY:
0

Candidatas a READY:
TASK-013
TASK-014
TASK-024

Baseline de testes informado:
228/228 PASSED

Último prompt histórico conhecido:
docs/prompts/history/prompt26.md
```

O estado acima é apenas referência.

Não presumir que continue atual.

Consultar o repositório e o GitHub antes de realizar alterações.

---

# 4. SETUP OBRIGATÓRIO

Ler as instruções disponíveis:

```text
AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md
README.md
docs/README.md
```

Inspecionar:

```text
.ai/
agent_docs/
docs/governance/
docs/knowledge/
docs/requirements/
docs/architecture/
docs/security/
docs/testing/
```

Consultar o Kit IA Dev:

```text
D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev
```

Aplicar os padrões existentes de:

- Requirements.
- GitFlow.
- Quality Gates.
- Documentation.
- Security.
- Code Review.
- Traceability.
- Prompt History.
- Definition of Ready.
- Definition of Done.

Não instalar novas dependências sem necessidade.

---

# 5. FONTE DA VERDADE

Utilizar como fontes canônicas:

```text
tasks/BACKLOG.md
tasks/README.md
tasks/DEPENDENCY_GRAPH.md
tasks/IMPLEMENTATION_ORDER.md
tasks/TRACEABILITY_MATRIX.md
tasks/DEFINITION_OF_READY.md
tasks/DEFINITION_OF_DONE.md
```

Ler os arquivos individuais:

```text
tasks/backlog/
tasks/ready/
tasks/in-progress/
tasks/done/
```

Cada Task possui informações que devem orientar sua Issue:

- ID.
- Título oficial.
- Epic.
- Prioridade.
- Complexidade.
- Risco.
- Wave.
- Dependências.
- Objetivo.
- Escopo.
- Critérios de aceite.
- Requisitos relacionados.
- ADRs aplicáveis.
- Testes previstos.
- Status canônico.

Não inventar requisitos ou critérios de aceite.

**O Markdown canônico continua sendo a fonte de verdade; GitHub Issues e Projects são projeções de acompanhamento.**

Caso existam divergências, registrar o conflito e priorizar a fonte canônica aprovada, sem apagar informações humanas não relacionadas.

---

# 6. DISCOVERY INICIAL

Antes de criar qualquer Issue, executar inspeção completa.

```powershell
git fetch origin
git branch --show-current
git status --short
git log -5 --oneline
git rev-parse HEAD
git rev-parse origin/develop
git worktree list --porcelain
```

Verificar:

- Alterações locais.
- Branches existentes.
- PRs abertos.
- Execução anterior de sincronização.
- Scripts já implementados.
- Issues existentes.
- Itens existentes no Project #22.

Se houver execução anterior parcialmente concluída, retomar de forma idempotente.

Não reiniciar indiscriminadamente.

---

# 7. AUTENTICAÇÃO GITHUB

Verificar GitHub CLI:

```powershell
gh --version
gh auth status
```

Confirmar acesso ao repositório:

```powershell
gh repo view gfmaurila/OBS-AI-Live-Assistant
```

Confirmar acesso ao Project:

```powershell
gh project view 22 --owner gfmaurila --format json
gh project field-list 22 --owner gfmaurila --format json
gh project item-list 22 --owner gfmaurila --limit 100 --format json
```

Validar permissões de:

- Leitura e escrita de Issues.
- Leitura e escrita do Project V2.
- Consulta e edição dos campos do Project.
- Criação de branches.
- Push.
- Pull Requests.

Se a autenticação não tiver permissões para Projects V2, investigar se é possível autorizar a GitHub CLI de maneira segura.

O acesso ao Project pessoal pode exigir permissão de Projects específica, como o escopo `project` para credenciais classic, ou permissões equivalentes de fine-grained token/GitHub App.

Não presumir que acesso ao repositório implica acesso ao Project.

Não imprimir tokens.

Não pedir ao usuário que cole credenciais no terminal compartilhado ou na conversa.

Se for necessária uma autenticação interativa do usuário, apresentar a instrução segura e parar somente a etapa que depender dela.

Preservar o progresso já validado.

---

# 8. INVENTÁRIO DAS 52 TASKS

Construir um inventário a partir do backlog e dos arquivos individuais.

Esperado:

```text
TASK-001 ... TASK-052

TOTAL: 52

DONE: 9
PENDENTES: 43
```

Não fixar esses valores no código como verdade permanente.

A contagem deve ser derivada dinamicamente.

O inventário deve validar:

- IDs únicos.
- Títulos.
- Arquivos canônicos.
- Estados.
- Dependências.
- Epics.
- Prioridades.
- Waves.
- Ausência de duplicatas.
- Ausência de referências inexistentes.

Se a contagem atual diferir da referência, investigar antes de criar registros.

---

# 9. INVENTÁRIO DAS ISSUES EXISTENTES

Consultar todas as Issues, abertas e fechadas, com paginação completa.

Não limitar a busca às primeiras 100 se existirem mais resultados.

Para cada Task:

1. Procurar Issue com ID canônico no título.
2. Procurar identificador estável no corpo.
3. Verificar associação ao repositório correto.
4. Comparar com arquivo canônico.
5. Inspecionar possíveis duplicatas.
6. Escolher a Issue canônica, se já existir.

Identificador recomendado:

```text
<!-- obs-ai-task-id:TASK-009 -->
```

Não depender exclusivamente do título, pois ele pode mudar.

Se encontrar duas Issues candidatas à mesma Task:

- Não criar terceira.
- Não fechar ou excluir automaticamente as duplicadas.
- Registrar o conflito.
- Reconciliar apenas quando a correspondência for inequívoca.

A criação deve ser **idempotente**.

Executar duas vezes não deve produzir Issues duplicadas.

---

# 10. CRIAÇÃO DAS ISSUES

Criar somente as Tasks que ainda não possuírem Issue canônica.

Padrão de título:

```text
[TASK-013] Implementar timeout, cancellation, retry e erros normalizados
```

Utilizar o título real da Task.

Estrutura obrigatória do corpo:

```markdown
# TASK-XXX — Título oficial

## Identificação

- Epic:
- Prioridade:
- Complexidade:
- Risco:
- Wave:
- Status canônico:

## Objetivo

Texto baseado no arquivo oficial.

## Escopo

Descrição dos entregáveis aprovados.

## Critérios de aceite

Reproduzir ou referenciar fielmente os critérios canônicos.

## Requisitos e ADRs

Referências existentes e válidas.

## Dependências

Tasks predecessoras.

## Testes e validação

Critérios previstos na Task.

## Definition of Ready

Estado e referência à política oficial.

## Definition of Done

Checklist conforme governança.

## Documentação

Link para o Markdown canônico.

## Referências

Link para o backlog e Dependency Graph.

## Pull Requests

Referências existentes, quando aplicável.

<!-- obs-ai-task-id:TASK-XXX -->
```

Não adicionar requisitos imaginados.

Se uma seção não existir no arquivo canônico, omitir ou registrar que não há informação definida.

Não criar Issues vazias.

Não usar Markdown redundante que dificulte a manutenção.

---

# 11. TASKS JÁ CONCLUÍDAS

As Tasks concluídas no estado inicial conhecido são:

```text
TASK-001
TASK-002
TASK-003
TASK-004
TASK-005
TASK-006
TASK-007
TASK-008
TASK-009
```

Para cada uma:

1. Verificar o estado canônico atual.
2. Localizar ou criar Issue.
3. Adicionar ao Project #22.
4. Definir campo Status como Done.
5. Fechar a Issue.
6. Associar PRs de implementação e finalização conhecidos, quando existirem.
7. Preservar o histórico real de commits.
8. Não criar datas de conclusão fictícias.
9. Não afirmar que checks remotos passaram sem evidência.

Se o arquivo canônico não confirmar DONE, não forçar o fechamento.

---

# 12. TASKS PENDENTES

Para as Tasks ainda não concluídas:

- Manter Issue aberta.
- Utilizar Backlog como estado padrão quando o arquivo canônico indicar BACKLOG.
- Não marcar READY por inferência.
- Não criar feature branches de implementação.
- Não iniciar testes ou execução funcional.
- Não marcar DONE.
- Não alterar critérios de aceite.

O último relatório identificou como candidatas:

```text
TASK-013
TASK-014
TASK-024
```

Essas Tasks são somente **candidatas a READY**.

Não confundir dependências satisfeitas com Definition of Ready aprovada.

As Tasks dependentes devem permanecer no estado canônico até que seus requisitos sejam atendidos.

---

# 13. CONFIGURAÇÃO DO GITHUB PROJECT #22

Project:

```text
Owner: gfmaurila
Number: 22
Type: GitHub Projects V2
```

Inspecionar a estrutura existente antes de editar.

Campos desejados:

| Campo | Tipo preferencial |
|---|---|
| Status | Single select |
| Priority | Single select |
| Epic | Single select ou texto |
| Wave | Number ou texto |
| Complexity | Single select |
| Risk | Single select |
| Task ID | Texto |
| Dependencies | Texto ou referência |

Não criar campos redundantes.

Reutilizar os campos existentes quando semanticamente compatíveis.

Não renomear ou excluir campos utilizados por outros projetos ou fluxos sem garantia de compatibilidade.

Campos adicionais são desejáveis, mas não devem bloquear o inventário básico se as permissões ou limites do GitHub impedirem sua criação.

---

# 14. STATUS DO BOARD

Fluxo desejado:

```text
Backlog
   ↓
Ready
   ↓
In Progress
   ↓
Code Review
   ↓
Validation
   ↓
Done
```

Estado alternativo:

```text
Blocked
```

Semântica obrigatória:

### Backlog

Task registrada, ainda não autorizada ou elegível.

### Ready

Definition of Ready formalmente aprovada.

### In Progress

Execução autorizada, feature branch criada e implementação iniciada.

### Code Review

Implementação submetida para revisão.

### Validation

Gates, PR ou validação pós-merge em andamento.

### Done

Definition of Done atendida e finalização administrativa concluída.

### Blocked

Impedimento formal de dependência, segurança, requisitos ou execução.

Se já existir campo `Status`, verificar suas opções.

Criar opções ausentes somente por operação segura e compatível.

Se a API exigir substituir o conjunto inteiro de opções, preservar todos os IDs, valores e configurações preexistentes e validar os efeitos antes de executar.

Não reconstruir o campo de maneira destrutiva.

A ausência de uma opção desejada deve ser registrada como bloqueio de configuração, não resolvida com perda de dados.

---

# 15. VINCULAR AS ISSUES AO PROJECT

Para cada Issue canônica:

1. Consultar se já está vinculada ao Project #22.
2. Reutilizar item existente, se presente.
3. Adicionar ao Project somente se necessário.
4. Obter o ID real do item.
5. Obter os IDs reais de campo e opções.
6. Atualizar Status.
7. Atualizar demais campos compatíveis.
8. Verificar persistência.
9. Registrar o vínculo.

Exemplo de comando para consulta:

```powershell
gh project item-list 22 --owner gfmaurila --limit 100 --format json
```

Exemplo de adição:

```powershell
gh project item-add 22 --owner gfmaurila --url <URL_DA_ISSUE>
```

Substituir placeholders por valores reais.

Usar `gh project item-edit` ou GraphQL com IDs obtidos dinamicamente.

Não inventar IDs de campos, opções ou itens.

Não criar draft items como substitutos das Issues canônicas.

A Issue deve existir no repositório e estar vinculada ao Project.

---

# 16. STATUS DAS 52 TASKS

Depois da sincronização, validar:

```text
TOTAL DE TASKS: 52

ISSUES CANÔNICAS: 52

DUPLICATAS NOVAS: 0

DONE: 9

PENDENTES: 43

READY: 0
```

Esses valores são a expectativa para o baseline informado.

Se o backlog tiver mudado legitimamente, registrar a diferença e reconciliar com o estado real.

As nove DONE devem estar:

```text
Issue: CLOSED
Project Status: Done
```

As 43 restantes devem estar:

```text
Issue: OPEN
Project Status: conforme estado canônico
```

Não marcar todas as pendentes como Blocked apenas porque possuem dependências futuras.

---

# 17. RELACIONAMENTOS ENTRE TASKS

Preservar as dependências oficiais.

Exemplo:

```text
TASK-010
depends on:
TASK-008
TASK-009
TASK-014
TASK-015
```

Criar referências navegáveis às Issues correspondentes.

Preferir relacionamentos nativos de dependência do GitHub quando disponíveis, permitidos e seguros.

Caso contrário, registrar no corpo:

```markdown
## Dependências

- TASK-008 — #ISSUE
- TASK-009 — #ISSUE
- TASK-014 — #ISSUE
- TASK-015 — #ISSUE
```

Não criar relacionamentos circulares.

Não inverter predecessoras e sucessoras.

Validar contra `tasks/DEPENDENCY_GRAPH.md`.

Não usar menções que gerem alterações não autorizadas em outros repositórios.

---

# 18. EPICS, WAVES E PRIORIDADES

As 52 Tasks pertencem a Epics e Waves já definidos.

Sincronizar:

- Epic oficial.
- Wave.
- Priority P0/P1.
- Complexity.
- Risk.

As informações devem ser derivadas do backlog.

Não recalcular prioridades por preferência própria.

Não reordenar a execução fora da política atual.

Se o Project não suportar todos os campos, preservar os metadados nas Issues e registrar a limitação.

---

# 19. PRS E EVIDÊNCIAS HISTÓRICAS

Para Tasks concluídas:

- Consultar PRs já mesclados.
- Relacionar PRs à Issue correspondente.
- Registrar merge commits reais quando necessário.
- Usar o arquivo canônico e os relatórios existentes para identificar evidências.

Não inferir relacionamento apenas por proximidade numérica de PR.

Não criar comentários repetitivos.

Não reabrir Tasks concluídas apenas para adicionar histórico.

Não modificar PRs mesclados sem necessidade.

---

# 20. AUTOMAÇÃO FUTURA

Preparar mecanismo de sincronização para que o GitHub Project acompanhe o workflow das próximas Tasks.

A automação deve executar somente operações de gestão de Issues e Projects.

**Não deve disparar implementação de software.**

Comportamento desejado:

| Evento confirmado | Ação |
|---|---|
| Nova Task canônica criada | Criar Issue e vincular ao Project |
| Task marcada READY após DoR | Atualizar Status para Ready |
| Task oficialmente IN_PROGRESS | Atualizar para In Progress |
| PR de implementação validamente associado e em revisão | Atualizar para Code Review |
| Validação formal registrada | Atualizar para Validation |
| Task marcada DONE após pós-merge | Atualizar para Done e fechar Issue |
| Task formalmente BLOCKED | Atualizar para Blocked |
| Task reaberta no repositório | Reconciliar Issue e Project com o estado canônico |

Não inferir DONE apenas porque um PR foi mesclado.

A conclusão exige a política de Definition of Done.

Não inferir READY apenas porque as dependências estão concluídas.

---

# 21. SCRIPT DE SINCRONIZAÇÃO

Criar ou adaptar, conforme a organização existente:

```text
tooling/github-project-sync.ps1
```

Objetivo:

Sincronizar Tasks canônicas com GitHub Issues e Project #22.

Requisitos:

- PowerShell compatível com o ambiente do projeto.
- Idempotência.
- Paginação.
- Retry controlado para falhas transitórias.
- Respeito a rate limits.
- Tratamento de erros HTTP.
- Sem tokens em logs.
- Dry-run.
- Checkpoints.
- Validação dos resultados.
- Preservação de dados humanos.
- Zero exclusões destrutivas.

Parâmetros desejados:

```powershell
-Owner
-Repository
-ProjectNumber
-DryRun
-Apply
-TaskId
-VerboseReport
```

Exemplos de uso desejado:

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
  -Apply
```

```powershell
.\tooling\github-project-sync.ps1 `
  -Owner gfmaurila `
  -Repository OBS-AI-Live-Assistant `
  -ProjectNumber 22 `
  -TaskId TASK-013 `
  -Apply
```

Usar nomes de parâmetros efetivamente implementados.

Não aceitar operação de escrita por omissão: a execução padrão deve ser dry-run.

Falhas não podem ser ocultadas.

---

# 22. PLANO DE EXECUÇÃO SEGURA

Organizar a sincronização em etapas.

## Etapa A — Discovery

- Ler setup.
- Inspecionar Tasks.
- Inspecionar Issues.
- Inspecionar Project.
- Validar permissões.
- Gerar inventário.

## Etapa B — Dry-run

Calcular:

- Issues a criar.
- Issues a atualizar.
- Issues a fechar.
- Itens a vincular.
- Campos a configurar.
- Conflitos.
- Duplicatas.
- Operações impedidas por permissões.

Não executar escrita nesta etapa.

## Etapa C — Aplicação

Após validar o plano internamente:

- Criar Issues faltantes.
- Atualizar Issues existentes.
- Vincular Project.
- Atualizar campos.
- Fechar as DONE.
- Preservar abertas as pendentes.

O cliente já autorizou essas operações neste prompt.

Não solicitar autorização adicional se a operação estiver dentro do escopo e as permissões estiverem corretas.

## Etapa D — Reconciliação

Consultar novamente o GitHub.

Verificar o estado persistido de todos os registros.

Não considerar sucesso apenas porque uma chamada de API retornou HTTP 200 ou 201.

## Etapa E — Reexecução

Executar um segundo dry-run.

Resultado esperado:

```text
Issues adicionais: 0
Vínculos adicionais: 0
Alterações de status: 0
Duplicatas: 0
```

Diferenças justificadas por comentários, timestamps ou metadados não devem causar mutações repetitivas.

---

# 23. CHECKPOINTS E RECUPERAÇÃO

Registrar progresso sem credenciais.

Para cada Task:

```text
Task ID
Issue Number
Issue URL
Project Item ID
Expected Status
Observed Status
Last Synchronization Result
```

Preferir inventário verificável derivado do GitHub.

Se existir checkpoint local, não versionar dados sensíveis.

Se houver interrupção:

1. Consultar Issues novamente.
2. Consultar itens do Project.
3. Identificar operações já realizadas.
4. Retomar as faltantes.
5. Não duplicar registros.
6. Não reabrir Issues concluídas indevidamente.
7. Não apagar dados para corrigir inconsistências.

Se a autenticação expirar, preservar progresso e interromper apenas as operações autenticadas.

---

# 24. AUTOMAÇÃO GITHUB ACTIONS

Avaliar uma automação administrativa como:

```text
.github/workflows/github-project-sync.yml
```

Se existir mecanismo equivalente, reutilizá-lo.

A automação deve:

- Ler `tasks/` após alterações integradas em `develop`.
- Executar sincronização de Issues e Projects.
- Utilizar permissões mínimas necessárias.
- Não executar o desenvolvimento das Tasks.
- Não gerar commits automaticamente.
- Não promover branches.
- Não criar releases.
- Não executar deploy.
- Não modificar OBS.

Considerar triggers como alterações em:

```text
tasks/**
```

O workflow deve evitar loops e execuções redundantes.

Não utilizar eventos de Projects como gatilho de escrita no backlog sem uma política explícita de sincronização bidirecional.

A sincronização inicial deve ser **unidirecional: Markdown canônico → GitHub Issues/Project**.

### Autenticação da automação

Verificar se o `GITHUB_TOKEN` do workflow tem as permissões necessárias.

Não assumir que ele pode editar Projects pessoais.

Se for necessário GitHub App ou token específico:

- Definir a configuração necessária.
- Documentar a permissão mínima.
- Usar GitHub Actions Secrets.
- Não versionar credenciais.
- Não habilitar um workflow de escrita que falharia ou teria permissões insuficientes.

Se a credencial apropriada não estiver disponível, deixar a automação em modo seguro, documentada e pendente de configuração, sem afirmar que está operacional.

A sincronização manual via Codex deve continuar utilizável.

---

# 25. ATUALIZAR O WORKFLOW DOS AGENTES

Inspecionar:

```text
AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md
docs/governance/
```

Inserir, de forma mínima e compatível com as instruções existentes, uma regra de sincronização administrativa.

Comportamento desejado:

**Quando uma Task for autorizada e executada futuramente:**

1. Consultar Issue correspondente.
2. Confirmar arquivo canônico.
3. Validar DoR.
4. Atualizar status quando a transição for formal.
5. Vincular PR.
6. Após merge, validar gates.
7. Após finalização administrativa, sincronizar DONE.
8. Confirmar consistência com o Project #22.

Não tornar a disponibilidade do Project uma justificativa para ignorar gates do código.

Se a API de Projects estiver temporariamente indisponível, registrar pendência de sincronização recuperável.

Não modificar a governança para permitir deploy, releases ou merges sem os gates existentes.

---

# 26. DOCUMENTAÇÃO ADMINISTRATIVA

Criar ou atualizar documento de referência:

```text
docs/governance/GITHUB_PROJECT_SYNC.md
```

Conteúdo obrigatório:

1. Objetivo.
2. Repositório e Project.
3. Fonte da verdade.
4. Formato das Issues.
5. Campos do Project.
6. Semântica dos status.
7. Mapeamento de dependências.
8. Comandos de dry-run.
9. Comandos de sincronização.
10. Permissões GitHub.
11. Configuração do workflow.
12. Estratégia de idempotência.
13. Recuperação de falhas.
14. Segurança de credenciais.
15. Tratamento de inconsistências.
16. Como adicionar novas Tasks.
17. Como finalizar Tasks.
18. Como verificar integridade.
19. Limitações conhecidas.

Documentar a configuração realmente implementada.

Não declarar automação ativa se depender de credencial ainda não configurada.

---

# 27. TESTES DO SINCRONIZADOR

Criar testes administrativos determinísticos, se compatível com o tooling.

Validar:

- Parsing do backlog.
- Identificação de 52 IDs únicos no baseline.
- Detecção de duplicatas.
- Normalização de status.
- Mapeamento de dependências.
- Idempotência.
- Dry-run sem escrita.
- Criação condicional.
- Atualização condicional.
- Paginação.
- Erros 401/403.
- Rate limits.
- Retomada.
- Ausência de impressão de secrets.
- Preservação de campos não gerenciados.

Usar fixtures e mocks quando possível.

Não usar tokens reais nos testes.

Não executar testes destrutivos contra o Project.

---

# 28. QUALITY GATES

Para alterações versionadas:

```powershell
.\tooling\quality-gates.ps1
```

Executar os gates oficiais aplicáveis.

Confirmar:

- Restore.
- Build.
- Tests.
- Format.

A sincronização administrativa não pode quebrar os 228 testes conhecidos do produto.

Se houver scripts adicionais, validar sintaxe, testes e dry-run.

Registrar resultados reais.

Não declarar PASSED sem execução.

---

# 29. SECURITY AUDIT E CODE REVIEW

Revisar:

- Uso da API GitHub.
- Permissões.
- Autenticação.
- Secret leakage.
- Command injection.
- Manipulação de JSON.
- Escape correto de argumentos.
- Paginação.
- Retry.
- Rate limits.
- Idempotência.
- Duplicatas.
- Alteração indevida de Issues.
- Fechamento incorreto.
- Regressões de governança.
- Segurança dos workflows.

Executar Secret Scan.

Corrigir findings bloqueantes.

Não imprimir respostas completas de comandos de credenciais.

Não incorporar tokens em logs, relatórios ou PRs.

---

# 30. PROMPT HISTORY

Inspecionar:

```text
docs/prompts/history/
```

Último prompt conhecido:

```text
prompt26.md
```

Identificar o próximo nome livre, possivelmente:

```text
prompt27.md
```

Arquivar integralmente este prompt.

Não sobrescrever arquivos existentes.

Não incluir credenciais.

O arquivo deve ser incluído no PR administrativo.

---

# 31. GITFLOW ADMINISTRATIVO

Não trabalhar diretamente em `develop`.

Criar branch administrativa a partir da `develop` atualizada.

Nome sugerido:

```text
feature/github-project-22-task-sync
```

Antes de criar:

- Verificar se já existe.
- Verificar se existe PR aberto.
- Reutilizar execução anterior quando apropriado.

Aplicar apenas alterações de tooling, governança, workflow e documentação administrativa.

Commit sugerido:

```text
chore: integrate task backlog with GitHub Project 22
```

Push da feature branch.

Criar PR para:

```text
BASE: develop
HEAD: feature/github-project-22-task-sync
```

Validar:

- Diff administrativo.
- Quality Gates.
- Security Review.
- Code Review.
- Secret Scan.
- Branch protection.
- Reviews/checks obrigatórios.

Se tudo estiver aprovado e permitido, fazer merge em `develop`.

Não forçar merge.

Não modificar `main` ou `hml`.

---

# 32. VALIDAÇÃO PÓS-MERGE

Após o merge:

```powershell
git switch develop
git pull --ff-only origin develop
git rev-parse HEAD
git rev-parse origin/develop
git status --short
```

Executar Quality Gates aplicáveis.

Verificar:

- Documentação integrada.
- Scripts versionados.
- Nenhuma regressão.
- Workflow seguro.
- Sincronização consistente.
- Issues existentes.
- Itens vinculados ao Project.
- Status persistidos.

Executar dry-run final.

Se houver divergências, corrigir pelo fluxo GitFlow quando envolver código versionado, ou reconciliar pela API quando envolver apenas registros administrativos autorizados.

---

# 33. CRITÉRIOS DE SUCESSO

A sincronização integral estará concluída quando:

```text
52 Tasks canônicas identificadas.

52 Issues canônicas existentes.

52 Issues vinculadas ao Project #22.

0 novas duplicatas.

9 Issues DONE e CLOSED.

43 Issues pendentes e OPEN.

0 Tasks promovidas indevidamente a READY.

Epics, prioridades, Waves e dependências registrados.

Status do Project consistentes.

Script de sincronização disponível.

Dry-run idempotente aprovado.

Documentação atualizada.

Automação futura configurada e validada
OU declarada PENDING_CONFIGURATION se faltar credencial.

Quality Gates aplicáveis aprovados.

Security Audit aprovado.

Code Review aprovado.

Secret Scan aprovado.

PR administrativo integrado a develop.

develop sincronizada.

Working Tree limpo.
```

Separar os resultados:

- **Sincronização das Issues**
- **Vínculo com Project**
- **Configuração do Board**
- **Automação futura**
- **Integração do tooling**

Não declarar sucesso integral se apenas algumas partes tiverem sido concluídas.

---

# 34. RELATÓRIO FINAL

Apresentar:

```text
OBS-AI-LIVE-ASSISTANT
GITHUB PROJECT #22 — TASK SYNC REPORT

EXECUTOR:
Codex

REPOSITÓRIO:
gfmaurila/OBS-AI-Live-Assistant

PROJECT:
https://github.com/users/gfmaurila/projects/22/views/1

TOTAL DE TASKS:
<valor>

ISSUES EXISTENTES REUTILIZADAS:
<valor>

ISSUES CRIADAS:
<valor>

ISSUES ATUALIZADAS:
<valor>

ISSUES CANÔNICAS TOTAIS:
<valor>

ISSUES CLOSED / DONE:
<valor>

ISSUES OPEN:
<valor>

DUPLICATAS:
<valor>

PROJECT ITEMS VINCULADOS:
<valor>

PROJECT ITEMS JÁ EXISTENTES:
<valor>

PROJECT ITEMS ADICIONADOS:
<valor>

STATUS BACKLOG:
<valor>

STATUS READY:
<valor>

STATUS IN PROGRESS:
<valor>

STATUS CODE REVIEW:
<valor>

STATUS VALIDATION:
<valor>

STATUS DONE:
<valor>

STATUS BLOCKED:
<valor>

EPICS:
<resultado>

PRIORIDADES:
<resultado>

WAVES:
<resultado>

DEPENDÊNCIAS:
<resultado>

DRY-RUN INICIAL:
PASSED / BLOCKED

SYNC APPLY:
PASSED / PARTIAL / BLOCKED

DRY-RUN FINAL:
PASSED / BLOCKED

IDEMPOTÊNCIA:
PASSED / BLOCKED

AUTOMAÇÃO:
ACTIVE / PENDING_CONFIGURATION / BLOCKED

CREDENCIAIS:
SAFE / ATTENTION_REQUIRED

DOCUMENTAÇÃO:
UPDATED / BLOCKED

PROMPT HISTORY:
<arquivo>

SCRIPT DE SYNC:
<caminho>

GITHUB ACTION:
<caminho ou NOT CONFIGURED>

RESTORE:
PASSED / BLOCKED

BUILD:
PASSED / BLOCKED

TESTS:
PASSED / BLOCKED

FORMAT:
PASSED / BLOCKED

ARCHITECTURE:
PASSED / BLOCKED

SECURITY:
PASSED / BLOCKED

CODE REVIEW:
APPROVED / BLOCKED

SECRET SCAN:
PASSED / BLOCKED

BRANCH ADMINISTRATIVA:
<nome>

COMMIT:
<sha>

PR:
<número e URL>

MERGE:
<sha ou BLOCKED>

DEVELOP LOCAL:
<sha>

DEVELOP REMOTO:
<sha>

WORKTREE:
CLEAN / DIRTY

MAIN:
NÃO MODIFICADA

HML:
NÃO MODIFICADA

RELEASE:
NÃO CRIADA

OBS STUDIO:
NÃO MODIFICADO

TASKS DO PRODUTO IMPLEMENTADAS:
0

PRÓXIMA TASK:
NÃO INICIADA

STATUS FINAL:
COMPLETE / PARTIAL / BLOCKED

STOP
```

---

# 35. CONDIÇÃO FINAL DE PARADA

Depois de configurar e validar o Project #22:

- Não iniciar TASK-013.
- Não iniciar TASK-014.
- Não iniciar TASK-024.
- Não executar nenhuma Task pendente.
- Não criar execução autônoma de desenvolvimento nesta operação.
- Não modificar `main`, `hml` ou releases.
- Não modificar OBS Studio.
- Não executar limpeza destrutiva.

A execução autônoma das Tasks restantes será configurada separadamente, em uma etapa posterior.

**OBJETIVO DESTA EXECUÇÃO: ORGANIZAR E AUTOMATIZAR A GESTÃO DAS 52 TASKS NO GITHUB PROJECT #22.**

**EXECUTAR A SINCRONIZAÇÃO COMPLETA, VALIDAR E PARAR.**

**STOP.**
