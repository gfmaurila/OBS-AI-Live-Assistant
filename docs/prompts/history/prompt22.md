# PROMPT OPERACIONAL — CODEX — TASK-006

**Projeto:** OBS-AI-Live-Assistant
**Repositório:** `D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant`
**Kit IA Dev:** `D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev`
**IA executora:** Codex
**Task autorizada:** TASK-006
**Idioma obrigatório:** Português do Brasil
**Destino autorizado:** `develop`
**Modalidade:** Execução integral, com merge automático condicionado aos Quality Gates.

---

## 1. MISSÃO

Você é o agente de engenharia responsável pela execução integral da TASK-006.

Execute exclusivamente a TASK-006, desde a preparação do ambiente até a validação pós-merge e a limpeza final.

O fluxo obrigatório é:

**Setup → Project Discovery → Knowledge Discovery → Definition of Ready → Planejamento → Feature Branch → Implementação → Testes → Quality Gates → Code Review → Correções → Secret Scan → Documentação → Prompt History → Commit → Push → PR → Validação do PR → Merge em develop → Validação pós-merge → Definition of Done → Rastreabilidade → Limpeza → Relatório Final → STOP.**

A execução deve ser autônoma dentro dos limites desta autorização.

Não solicitar aprovação intermediária para ações já autorizadas e permitidas pela governança.

Não executar qualquer outra Task.

---

## 2. REGRAS ABSOLUTAS

É obrigatório:

- Executar somente a TASK-006.
- Ler o arquivo canônico antes de planejar ou implementar.
- Respeitar a Definition of Ready.
- Seguir os ADRs e requisitos aprovados.
- Preservar o histórico Git.
- Criar feature branch baseada em `develop`.
- Implementar somente os critérios de aceite da TASK-006.
- Executar todos os Quality Gates aplicáveis.
- Corrigir findings antes do merge.
- Arquivar este prompt integralmente.
- Criar PR exclusivamente para `develop`.
- Fazer merge somente após aprovação de todos os gates.
- Executar validação pós-merge.
- Preservar qualquer trabalho preexistente.
- Emitir relatório final com evidências reais.

É proibido:

- Executar TASK-007, TASK-024 ou qualquer outra Task.
- Antecipar funcionalidades de Tasks futuras.
- Modificar `hml` ou `main`.
- Criar branches de release ou tags.
- Executar deploy.
- Modificar a instalação ou as configurações do OBS.
- Desabilitar testes para obter aprovação.
- Contornar branch protection.
- Forçar merge.
- Declarar testes não executados como PASSED.
- Declarar DONE antes de cumprir a governança.
- Apagar arquivos desconhecidos ou não rastreados sem análise.

---

## 3. LEITURA COMPLETA DO SETUP

Antes de editar qualquer arquivo, inspecionar a estrutura do repositório e ler integralmente todos os documentos aplicáveis.

### Documentos principais

```text
AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md
README.md
docs/README.md
```

### Diretórios de agentes e Skills

```text
.ai/
agent_docs/
.claude/
.codex/
```

Ler os arquivos existentes, respeitando as instruções de cada ferramenta.

Não presumir que todas as pastas existam.

### Governança e conhecimento

```text
docs/governance/
docs/knowledge/
docs/requirements/
docs/security/
docs/research/
docs/architecture/
docs/testing/
docs/reports/
```

### Gestão das Tasks

```text
tasks/README.md
tasks/BACKLOG.md
tasks/DEPENDENCY_GRAPH.md
tasks/IMPLEMENTATION_ORDER.md
tasks/TRACEABILITY_MATRIX.md
tasks/DEFINITION_OF_READY.md
tasks/DEFINITION_OF_DONE.md
tasks/RELEASE_PLAN.md
```

Ler o arquivo canônico da TASK-006 por completo.

Identificar todos os documentos, Requirements, ADRs, decisões e critérios referenciados pela Task.

Consultar o Kit IA Dev quando a governança exigir.

O repositório é a fonte da verdade.

---

## 4. ESTADO INICIAL CONHECIDO

O último relatório da TASK-005 informou:

```text
TASK-001: DONE
TASK-002: DONE
TASK-003: DONE
TASK-004: DONE
TASK-005: DONE

TASK-006: CANDIDATA A READY

Branch: develop

HEAD:
4127e483d8d48b855ae40815bea2f0a8b30ef852

origin/develop:
4127e483d8d48b855ae40815bea2f0a8b30ef852

Working Tree: CLEAN

.NET SDK: 10.0.401

Restore: PASSED
Build: PASSED
Warnings: 0
Errors: 0
Tests: 65/65 PASSED
Format: PASSED

OBS: NÃO MODIFICADO
```

Essas informações constituem apenas o baseline conhecido.

Confirmar o estado real antes de qualquer alteração.

Não presumir que a TASK-006 já esteja formalmente READY.

---

## 5. INSPEÇÃO E SINCRONIZAÇÃO DO GIT

Executar:

```powershell
git fetch origin
git branch --show-current
git status
git status --short
git log -10 --oneline --decorate
git branch -vv
git rev-parse HEAD
git rev-parse origin/develop
```

Verificar se existem:

- Alterações rastreadas.
- Arquivos não rastreados.
- Arquivos staged.
- Commits locais não publicados.
- Branches anteriores da TASK-006.
- PRs existentes.
- Trabalhos interrompidos.

Se houver alterações, analisar e preservar.

Não descartar trabalho válido.

Se a árvore estiver limpa e não houver execução anterior em andamento:

```powershell
git switch develop
git pull --ff-only origin develop
```

Confirmar:

`develop local = origin/develop`

Se houver divergência que não possa ser resolvida sem risco, registrar BLOCKED e interromper.

---

## 6. LOCALIZAR A TASK-006 CANÔNICA

Pesquisar nos diretórios de Tasks:

```text
tasks/
tasks/backlog/
tasks/ready/
tasks/in-progress/
tasks/done/
```

Usar apenas os diretórios efetivamente existentes.

Localizar o arquivo que contém o identificador TASK-006.

Ler integralmente:

- Título oficial.
- Objetivo.
- Escopo.
- Fora de escopo.
- Dependências.
- Requirements.
- ADRs.
- Critérios de aceite.
- Definition of Ready.
- Definition of Done.
- Testes obrigatórios.
- Segurança.
- Riscos.
- Entregáveis.

Não inventar o título ou o conteúdo da TASK.

Se houver mais de um arquivo canônico conflitante, interromper e registrar a inconsistência.

---

## 7. DEFINITION OF READY

Validar formalmente a TASK-006 conforme:

```text
tasks/DEFINITION_OF_READY.md
tasks/DEPENDENCY_GRAPH.md
tasks/BACKLOG.md
```

Confirmar:

1. Todas as dependências estão DONE.
2. O escopo está claro.
3. Os critérios de aceite são verificáveis.
4. Os Requirements estão identificados.
5. Os ADRs aplicáveis foram consultados.
6. Os riscos estão mapeados.
7. A estratégia de testes está definida.
8. Os requisitos de segurança estão identificados.
9. Não existem blockers.
10. A Task está autorizada para execução.

Se todos os critérios forem atendidos:

`Definition of Ready: PASSED`

Efetuar a transição para READY ou IN_PROGRESS conforme o workflow oficial.

Se qualquer requisito obrigatório não for atendido:

`Definition of Ready: BLOCKED`

Registrar o motivo e STOP.

Não implementar uma Task com DoR bloqueado.

---

## 8. CRIAÇÃO DA FEATURE BRANCH

Após a aprovação da DoR, criar uma branch a partir da `develop` atualizada:

```text
feature/task-TASK-006-<descricao-curta-baseada-no-titulo-canonico>
```

Exemplo de procedimento:

```powershell
git switch develop
git pull --ff-only origin develop
git switch -c feature/task-TASK-006-<slug>
```

Substituir `<slug>` pelo identificador real.

Não usar literalmente os placeholders.

Não criar branch a partir de `main` ou `hml`.

Não sobrescrever uma branch preexistente.

---

## 9. PLANEJAMENTO DA IMPLEMENTAÇÃO

Antes de alterar o código, elaborar um plano técnico com:

- Objetivo da TASK-006.
- Critérios de aceite.
- Projetos impactados.
- Arquivos previstos.
- Interfaces e contratos envolvidos.
- Dependências permitidas.
- Testes necessários.
- Requisitos de segurança.
- Riscos de regressão.
- Evidências esperadas.

Criar uma matriz de rastreabilidade entre critérios de aceite e testes.

Respeitar os artefatos de planejamento previstos pela governança.

Não criar documentação redundante.

Não ampliar o escopo sem autorização.

---

## 10. ARQUITETURA OBRIGATÓRIA

Preservar:

**Modular Monolith + Ports & Adapters + Provider Architecture.**

Aplicar:

- SOLID.
- Clean Code.
- Clean Architecture conforme os ADRs.
- Dependency Inversion.
- Separation of Concerns.
- Baixo acoplamento.
- Alta coesão.
- Testabilidade.
- Fail-closed quando exigido.

Projetos existentes:

```text
src/
├── ObsAi.Domain/
├── ObsAi.Application/
├── ObsAi.Infrastructure/
├── ObsAi.Providers/
├── ObsAi.ObsIntegration/
└── ObsAi.Host/
```

Preservar as regras de dependência aprovadas.

Não introduzir referências circulares.

Não adicionar `ProjectReference` prematuro.

Não criar integrações externas fora do escopo.

Não modificar contratos estabelecidos na TASK-005 sem necessidade explícita da TASK-006.

Caso seja indispensável alterar um contrato existente, comprovar compatibilidade e cobertura de regressão.

---

## 11. IMPLEMENTAÇÃO DA TASK-006

Implementar exclusivamente os entregáveis definidos no arquivo canônico.

Seguir a ordem:

1. Compreender os contratos existentes.
2. Identificar os componentes necessários.
3. Implementar o menor conjunto de alterações suficiente.
4. Aplicar os padrões arquiteturais.
5. Tratar falhas e entradas inválidas.
6. Adicionar testes.
7. Validar critérios de aceite.
8. Revisar segurança e regressões.

Não antecipar funcionalidades de outras Tasks.

Não criar mocks ou abstrações sem necessidade.

Não adicionar bibliotecas apenas por conveniência.

Não alterar o comportamento do OBS Studio.

---

## 12. FUNDAÇÃO .NET

Utilizar a configuração existente:

```text
global.json
Directory.Build.props
Directory.Packages.props
.editorconfig
.gitattributes
OBS-AI-Live-Assistant.slnx
```

Preservar:

- .NET SDK 10.0.401.
- Central Package Management, quando aplicável.
- Regras de compilação.
- Nullable.
- Convenções de código.
- Normalização LF.
- Arquitetura de testes.

Não recriar a solution.

Não modificar configurações globais sem justificativa técnica.

---

## 13. TESTES AUTOMATIZADOS

Baseline conhecido:

**65 testes aprovados após a TASK-005.**

Utilizar a arquitetura estabelecida em:

```text
docs/testing/TEST_ARCHITECTURE.md
```

Suítes existentes:

```text
tests/Architecture/
tests/Unit/
tests/Integration/
tests/Contracts/
tests/Security/
tests/FailureIsolation/
tests/Installer/
```

Inspecionar os caminhos reais antes de executar.

Criar testes apropriados ao escopo da TASK-006.

Cobrir:

- Caminhos de sucesso.
- Entradas inválidas.
- Falhas esperadas.
- Casos-limite.
- Regressões relevantes.
- Contratos aplicáveis.
- Isolamento de falhas quando necessário.

Não confundir âncoras de scaffold com testes funcionais.

Não aumentar artificialmente a contagem de testes.

Não desabilitar testes existentes.

Registrar:

```text
Testes baseline: 65
Testes adicionados: <quantidade>
Testes executados: <quantidade>
Passed: <quantidade>
Failed: <quantidade>
Skipped: <quantidade>
Not Applicable: <quantidade>
```

Se algum teste não for executável, explicar a razão.

---

## 14. QUALITY GATES OFICIAIS

Utilizar o executor canônico:

```powershell
.\tooling\quality-gates.ps1
```

O último relatório informou que o script não recebe parâmetros.

Confirmar seu conteúdo atual.

Executar o pipeline completo.

Resultado esperado:

```text
Restore: PASSED
Build: PASSED
Tests: PASSED
Format: PASSED
```

Registrar exit codes reais.

Não mascarar falhas.

Se falhar:

Investigar → Corrigir → Reexecutar.

Não editar o Quality Gate apenas para contornar uma falha.

---

## 15. ARCHITECTURE GATE

Validar:

- Dependências entre projetos.
- Ausência de ciclos.
- Limites entre camadas.
- Ports & Adapters.
- SOLID.
- ADR compliance.
- Compatibilidade com contratos.
- Ausência de dependências prematuras.
- Escopo exclusivo da TASK-006.

Executar os testes de arquitetura existentes.

Resultado obrigatório:

`Architecture Gate: PASSED`

---

## 16. SECURITY GATE

Aplicar os requisitos de segurança relevantes à TASK-006.

Verificar:

- Entradas não confiáveis.
- Tratamento de exceções.
- Exposição de informações sensíveis.
- Segredos.
- Configurações inseguras.
- Logging.
- Fronteiras de confiança.
- Dependências externas.
- Falhas de isolamento.
- Compatibilidade com as políticas de segurança.

Executar os testes e auditorias aplicáveis.

Resultado obrigatório:

`Security Gate: PASSED`

---

## 17. CODE REVIEW OBRIGATÓRIO

Utilizar as Skills de revisão disponíveis.

Realizar revisão contextual do diff.

Analisar:

- Correção funcional.
- Critérios de aceite.
- SOLID.
- Arquitetura.
- Segurança.
- Testabilidade.
- Regressões.
- Tratamento de erros.
- Nomenclatura.
- Manutenibilidade.
- Complexidade.
- Dependências.
- Documentação.
- Escopo.

Classificar os findings:

```text
CRITICAL
HIGH
MEDIUM
LOW
INFO
```

Corrigir todos os findings bloqueantes conforme a governança.

Não realizar merge com findings críticos ou altos pendentes.

Reexecutar os gates impactados após qualquer correção.

Resultado obrigatório:

`Code Review: APPROVED`

---

## 18. SECRET SCAN

Executar varredura de segredos no diff, nos arquivos alterados e no conteúdo versionado relevante.

Procurar:

- API keys.
- Tokens.
- Senhas.
- Credenciais.
- Chaves privadas.
- Connection strings com segredos.
- Arquivos `.env` indevidamente versionados.
- Logs sensíveis.

Não exibir valores sensíveis.

Se encontrar segredo real, bloquear o merge e registrar o incidente conforme a governança.

Resultado obrigatório:

`Secrets: NONE`

---

## 19. DOCUMENTAÇÃO E RASTREABILIDADE

Atualizar apenas os documentos impactados pela TASK-006.

Incluir, quando exigido:

- Documentação técnica.
- Decisões relevantes.
- Evidências dos testes.
- Relatório de implementação.
- Critérios de aceite.
- Rastreabilidade Requirements → Implementação → Testes.
- Estado da Task.
- Dependency Graph.

Preservar documentação histórica.

Todos os textos destinados a pessoas devem estar em português do Brasil.

Não modificar documentação de outras Tasks sem necessidade de consistência do grafo ou índice.

---

## 20. ARQUIVAMENTO DO PROMPT

Inspecionar:

```text
docs/prompts/history/
```

O último prompt conhecido é:

`prompt21.md`

Determinar o próximo número disponível.

Provavelmente será `prompt22.md`, mas confirmar.

Arquivar **integralmente este prompt**, sem resumir ou substituir por um relato de execução.

Não sobrescrever arquivos existentes.

Não inserir credenciais.

Incluir o arquivo no commit da TASK-006.

---

## 21. DEFINITION OF DONE

Validar os critérios em:

```text
tasks/DEFINITION_OF_DONE.md
```

Antes de publicar:

```text
Implementation: COMPLETE
Acceptance Criteria: PASSED
Restore: PASSED
Build: PASSED
Tests: PASSED
Format: PASSED
Architecture Gate: PASSED
Security Gate: PASSED
Code Review: APPROVED
Critical Findings: 0
High Findings: 0
Secret Scan: PASSED
Documentation: UPDATED
Prompt History: ARCHIVED
```

Os critérios que dependem do merge e da validação pós-merge serão confirmados posteriormente.

Não marcar a Task como DONE prematuramente.

---

## 22. VERIFICAÇÃO PRÉ-COMMIT

Executar:

```powershell
git status
git status --short
git diff
git diff --check
```

Inspecionar arquivos não rastreados.

Não adicionar ao commit:

- `bin/`.
- `obj/`.
- Logs.
- Arquivos temporários.
- Caches.
- Credenciais.
- Artefatos de teste indevidos.
- Arquivos duplicados com sufixo `(1)`.

Não executar limpeza indiscriminada.

Confirmar que o diff contém somente alterações justificadas pela TASK-006.

---

## 23. COMMIT E PUSH

Criar Conventional Commit coerente com a implementação.

Exemplo de formato:

```text
feat: implement TASK-006 canonical scope
```

Escolher o tipo e a descrição apropriados ao conteúdo real.

Incluir:

- Código.
- Testes.
- Documentação.
- Rastreabilidade.
- Prompt histórico.

Publicar a feature branch.

Confirmar que o HEAD remoto corresponde ao local.

Não fazer push diretamente para `develop`.

---

## 24. PULL REQUEST

Antes de criar PR, verificar se já existe um PR válido para a feature.

Se existir, reutilizar.

Caso contrário, criar:

```text
BASE: develop
HEAD: feature/task-TASK-006-<slug>
```

O PR deve conter:

- Título oficial da TASK-006.
- Objetivo.
- Escopo.
- Critérios de aceite.
- Requirements.
- ADRs.
- Arquivos alterados.
- Testes.
- Quality Gates.
- Code Review.
- Security Review.
- Secret Scan.
- Riscos e limitações.

Descrição em português do Brasil.

Não criar PR de teste.

---

## 25. VALIDAÇÃO DO PR

Antes do merge, verificar:

- Base `develop`.
- Head correto.
- Ausência de conflitos.
- Diff dentro do escopo.
- Gates locais aprovados.
- Checks obrigatórios.
- Reviews obrigatórios.
- Branch protection.
- Nenhum blocker.
- Nenhum segredo.

Se não existir CI configurado:

`CI: NOT CONFIGURED`

Não confundir ausência de checks com aprovação de CI.

Não contornar regras de proteção.

---

## 26. MERGE AUTOMÁTICO PARA DEVELOP

A execução está autorizada a realizar merge automático **somente quando todos os requisitos forem atendidos**.

Condições:

```text
DoR: PASSED
Acceptance Criteria: PASSED
Tests: PASSED
Build: PASSED
Format: PASSED
Architecture Gate: PASSED
Security Gate: PASSED
Code Review: APPROVED
Secret Scan: PASSED
PR Validation: PASSED
Required Checks: SATISFIED
Required Reviews: SATISFIED
```

Se todas as condições forem atendidas, realizar merge para `develop`.

Não usar force merge.

Não ignorar regras de proteção.

Se houver blocker, preservar branch e PR e interromper.

---

## 27. VALIDAÇÃO PÓS-MERGE

Após o merge:

```powershell
git switch develop
git pull --ff-only origin develop
git rev-parse HEAD
git rev-parse origin/develop
git status --short
```

Confirmar:

`develop local = origin/develop`

Executar novamente:

```powershell
.\tooling\quality-gates.ps1
```

Validar:

- Restore.
- Build.
- Testes.
- Format.
- Architecture Gate.
- Security Gate.
- Critérios de aceite.
- Estado da Task.
- Rastreabilidade.
- Dependency Graph.

Se houver falha pós-merge, não declarar DONE.

Corrigir em nova feature e PR, se necessário.

Não fazer commit direto em `develop`.

---

## 28. FINALIZAÇÃO ADMINISTRATIVA

Se a governança exigir que a Task seja marcada como DONE somente após o merge:

1. Confirmar o merge de implementação.
2. Confirmar os gates pós-merge.
3. Criar branch administrativa específica da TASK-006 a partir de `develop`.
4. Atualizar o estado da Task.
5. Mover o arquivo canônico para `tasks/done/`.
6. Atualizar backlog, índices, rastreabilidade e Dependency Graph.
7. Criar commit e PR de finalização para `develop`.
8. Validar e realizar merge se todos os gates permitirem.
9. Sincronizar novamente `develop`.
10. Confirmar a documentação final.
11. Reexecutar as verificações aplicáveis.

Esse PR administrativo pertence exclusivamente à TASK-006.

Não criar PR administrativo se o workflow não exigir.

Não promover artificialmente outras Tasks para READY.

---

## 29. LIMPEZA FINAL

Após validação:

- Confirmar que todos os PRs da TASK-006 estão mesclados.
- Confirmar que as branches temporárias podem ser removidas.
- Excluir as branches da TASK-006 localmente e remotamente.
- Remover somente arquivos temporários criados nesta execução.
- Preservar arquivos preexistentes.
- Confirmar `develop` sincronizada.
- Confirmar Working Tree CLEAN.

Não executar:

```powershell
git reset --hard
git clean -fd
git restore .
git checkout -- .
```

Se houver arquivos desconhecidos, analisá-los antes de qualquer exclusão.

Não sacrificar trabalho legítimo para obter um status CLEAN.

---

## 30. SEGURANÇA DAS CREDENCIAIS GITHUB

Utilizar os mecanismos de autenticação já disponíveis.

Não imprimir credenciais.

Não registrar tokens em scripts versionados.

Se for necessário criar scripts temporários para integração com a API GitHub:

- Utilizar apenas os mecanismos aprovados.
- Não persistir valores de tokens.
- Não imprimir respostas sensíveis.
- Não incluir credenciais nos logs.
- Remover os scripts temporários criados pela execução quando seguro.
- Confirmar que nenhum segredo foi versionado.

Não solicitar credenciais em texto aberto.

---

## 31. INTERRUPÇÕES E RETOMADA SEGURA

Se ocorrer:

- Rate limit.
- Usage limit.
- Timeout.
- Falha de ferramenta.
- Falha de autenticação.
- Interrupção da sessão.

Preservar todo o trabalho válido.

Na retomada:

1. Inspecionar Git.
2. Identificar a branch.
3. Verificar commits.
4. Verificar PRs.
5. Verificar arquivos temporários.
6. Identificar o último gate concluído.
7. Continuar do ponto correto.

Não reiniciar a Task automaticamente.

Não duplicar PRs.

Não descartar arquivos preexistentes.

Se não for possível continuar, emitir relatório parcial com o estado real e STOP.

---

## 32. RELATÓRIO FINAL OBRIGATÓRIO

Entregar:

```text
OBS-AI-Live-Assistant

TASK:
TASK-006 — <título canônico>

TASK STATUS:
DONE / BLOCKED

IA:
Codex

Definition of Ready:
PASSED / BLOCKED

Execução integral:
CONCLUÍDA / BLOQUEADA

Escopo implementado:
<resumo>

Projetos alterados:
<lista>

.NET SDK:
<versão>

Restore:
PASSED / BLOCKED

Build:
PASSED / BLOCKED

Build Warnings:
<quantidade>

Build Errors:
<quantidade>

Format:
PASSED / BLOCKED

Testes baseline:
65

Testes adicionados:
<quantidade>

Testes totais:
<quantidade>

Tests:
PASSED / BLOCKED

Architecture Gate:
PASSED / BLOCKED

Security Gate:
PASSED / BLOCKED

Acceptance Criteria:
PASSED / BLOCKED

Definition of Done:
PASSED / BLOCKED

Code Review:
APPROVED / BLOCKED

Critical Findings:
<quantidade>

High Findings:
<quantidade>

Medium Findings:
<quantidade>

Low Findings:
<quantidade>

Secrets:
NONE / DETECTED

Prompt arquivado:
<arquivo>

Branch:
<nome>

Commits:
<hashes>

Push:
OK / BLOCKED

PR de implementação:
<número e URL>

PR de finalização:
<número e URL ou NOT APPLICABLE>

PR Validation:
PASSED / BLOCKED

CI:
PASSED / BLOCKED / NOT CONFIGURED

Merge para develop:
CONCLUÍDO / BLOQUEADO

Merge Commit:
<hash>

Develop local:
<hash>

Develop remoto:
<hash>

Develop sincronizada:
SIM / NÃO

Features locais:
EXCLUÍDAS / RETIDAS

Features remotas:
EXCLUÍDAS / RETIDAS

Working Tree:
CLEAN / DIRTY

TASK-006:
DONE / BLOCKED

Dependency Graph:
CONSISTENTE / BLOCKED

Novas Tasks READY:
<lista>

Tasks candidatas a READY:
<lista>

Tasks BLOCKED:
<lista>

hml:
NÃO MODIFICADA

Release:
NÃO CRIADA

main:
NÃO MODIFICADA

OBS:
NÃO MODIFICADO

PRÓXIMO:
<próxima Task candidata conforme o grafo>

STOP
```

---

## 33. CONDIÇÃO DE ENCERRAMENTO

A TASK-006 somente poderá ser declarada DONE após:

1. Todos os critérios de aceite aprovados.
2. Todos os Quality Gates aplicáveis aprovados.
3. Code Review aprovado.
4. Secret Scan aprovado.
5. Documentação atualizada.
6. Prompt histórico arquivado.
7. Commit e push realizados.
8. PR de implementação mesclado em `develop`.
9. Pipeline pós-merge aprovado.
10. Finalização administrativa concluída, quando exigida.
11. Dependency Graph consistente.
12. Branches temporárias removidas quando seguro.
13. Working Tree limpo ou exceções preexistentes justificadas.
14. Relatório final emitido.

**EXECUTAR EXCLUSIVAMENTE TASK-006.**

**NÃO EXECUTAR OUTRAS TASKS.**

**NÃO MODIFICAR HML, RELEASE OU MAIN.**

**NÃO MODIFICAR OBS STUDIO.**

**STOP.**
