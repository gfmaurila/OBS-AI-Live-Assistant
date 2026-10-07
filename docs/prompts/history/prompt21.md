# PROMPT OPERACIONAL — CODEX — TASK-005

**Projeto:** OBS-AI-Live-Assistant  
**Repositório:** `D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant`  
**Kit IA Dev:** `D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev`  
**IA executora:** Codex  
**Task autorizada:** TASK-005  
**Idioma:** Português do Brasil  
**Modalidade:** Execução integral, com merge automático condicionado à aprovação dos Quality Gates.

---

## 1. MISSÃO PRINCIPAL

Você é o agente de engenharia responsável pela execução integral da TASK-005 do projeto OBS-AI-Live-Assistant.

**Execute exclusivamente a TASK-005, do setup até o fim.**

O objetivo é:

1. Ler todo o setup e a documentação aplicável.
2. Localizar e interpretar o arquivo canônico da TASK-005.
3. Validar formalmente sua Definition of Ready.
4. Criar uma feature branch a partir de `develop`.
5. Implementar todos os critérios de aceite.
6. Criar ou atualizar testes automatizados.
7. Executar os Quality Gates.
8. Realizar Code Review e corrigir findings.
9. Executar Security Review e Secret Scan.
10. Atualizar documentação e rastreabilidade.
11. Arquivar integralmente este prompt.
12. Validar a Definition of Done.
13. Criar commit e push.
14. Criar Pull Request para `develop`.
15. Validar PR, checks e regras de proteção.
16. Realizar merge automático quando todos os gates passarem.
17. Validar novamente o código em `develop`.
18. Confirmar a conclusão da TASK-005.
19. Recalcular o Dependency Graph.
20. Excluir a feature branch quando seguro.
21. Emitir relatório final.
22. STOP.

Não solicitar confirmações intermediárias para ações expressamente autorizadas e compatíveis com a governança.

**Não executar TASK-006, TASK-024 ou qualquer outra Task.**

---

## 2. FONTE DA VERDADE

O repositório é a fonte da verdade.

Não presumir o título, os arquivos a modificar, os componentes ou os critérios de aceite da TASK-005.

Localizar o arquivo canônico e utilizá-lo como contrato de execução.

A documentação do projeto prevalece sobre suposições técnicas.

Se houver conflito entre documentos, aplicar a política de resolução de conflitos estabelecida pela governança.

Se o conflito impedir uma decisão segura, registrar o blocker e interromper.

Não reabrir decisões arquiteturais aprovadas sem necessidade formal.

---

## 3. LEITURA INTEGRAL DO SETUP

Antes de modificar qualquer arquivo, ler integralmente os documentos existentes e aplicáveis:

```text
AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md
README.md
docs/README.md

.ai/
agent_docs/
.claude/
.codex/

docs/governance/
docs/knowledge/
docs/requirements/
docs/security/
docs/research/
docs/architecture/
docs/testing/

tasks/README.md
tasks/BACKLOG.md
tasks/DEPENDENCY_GRAPH.md
tasks/IMPLEMENTATION_ORDER.md
tasks/TRACEABILITY_MATRIX.md
tasks/DEFINITION_OF_READY.md
tasks/DEFINITION_OF_DONE.md
tasks/RELEASE_PLAN.md
```

Localizar e ler integralmente o arquivo da TASK-005.

Ler todos os Requirements, Security Requirements, ADRs, decisões, riscos e documentos de arquitetura referenciados pela Task.

Consultar os relatórios anteriores relevantes:

```text
docs/reports/
```

Inspecionar as Skills disponíveis e aplicar as pertinentes:

- Requirements.
- Architecture.
- Developer.
- Test Generator.
- Security Audit.
- Code Review.
- Documentation.
- GitFlow e PR.

Utilizar somente Skills efetivamente instaladas.

Não criar Skills fictícias.

---

## 4. ESTADO INICIAL CONHECIDO

A última execução informou:

| Item | Estado |
|---|---|
| TASK-001 | DONE |
| TASK-002 | DONE |
| TASK-003 | DONE |
| TASK-004 | DONE |
| TASK-005 | Candidata a READY |
| TASK-006 | Candidata a READY |
| TASK-024 | Candidata a READY |
| .NET SDK | 10.0.401 |
| Solution | OBS-AI-Live-Assistant.slnx |
| Testes anteriores | 43/43 |
| Último merge conhecido | `93a4aee7db71cba1ec1cce0887d463f31399295b` |
| Working Tree | CLEAN |

Esses dados são apenas o baseline informado.

**Confirmar o estado real do Git e dos documentos canônicos.**

Não assumir que a TASK-005 já passou na Definition of Ready.

---

## 5. INSPEÇÃO INICIAL DO GIT

Executar:

```powershell
git fetch origin
git branch --show-current
git status
git status --short
git log --oneline --decorate -15
git branch -vv
```

Verificar:

- Branch atual.
- Arquivos modificados.
- Arquivos não rastreados.
- Commits locais.
- Sincronização com o remoto.
- Possíveis branches de execução anterior.
- PR existente para a TASK-005.

Se houver trabalho anterior válido, não sobrescrever.

Se o repositório estiver limpo e não houver execução em andamento:

```powershell
git switch develop
git pull --ff-only origin develop
```

Confirmar:

`develop local = origin/develop`

Não executar operações destrutivas para forçar o estado limpo.

---

## 6. DEFINITION OF READY

Localizar a TASK-005 no workflow de Tasks.

Validar formalmente:

```text
tasks/DEFINITION_OF_READY.md
```

Verificar:

- Objetivo.
- Escopo.
- Dependências.
- Requirements.
- ADRs.
- Critérios de aceite.
- Riscos.
- Segurança.
- Testabilidade.
- Estimativa.
- Responsáveis, quando exigidos.
- Ausência de blockers.

Confirmar que todas as dependências da TASK-005 estão DONE.

Se a TASK-005 estiver apenas como candidata a READY, efetuar a transição prevista pela governança somente após comprovar todos os critérios.

**Condição de execução: DoR = PASSED.**

Se BLOCKED, registrar o motivo e STOP.

Não iniciar implementação de uma Task sem DoR aprovado.

---

## 7. CRIAR FEATURE BRANCH

Após aprovação da DoR:

```text
feature/task-TASK-005-<descricao-canônica-curta>
```

Criar a partir de `develop` atualizado.

Não utilizar `main` ou `hml` como base.

Não reutilizar branch antiga sem inspecionar seu histórico e estado.

Preservar a rastreabilidade da Task.

---

## 8. PLANO DE IMPLEMENTAÇÃO

Antes de editar código:

1. Identificar os critérios de aceite.
2. Mapear arquivos e projetos impactados.
3. Identificar testes necessários.
4. Identificar Security Requirements.
5. Identificar ADRs aplicáveis.
6. Verificar referências e dependências.
7. Determinar o menor conjunto de alterações suficiente.
8. Definir verificações objetivas para cada critério.

Registrar o plano conforme a governança.

Executar automaticamente se não houver bloqueio.

Não expandir o escopo para funcionalidades futuras.

---

## 9. ARQUITETURA OBRIGATÓRIA

Respeitar a arquitetura aprovada:

**Modular Monolith + Ports and Adapters + Provider Architecture.**

Aplicar:

- SOLID.
- Clean Code.
- Separação de responsabilidades.
- Dependency Inversion.
- Encapsulamento.
- Testabilidade.
- Baixo acoplamento.
- Alta coesão.

Preservar os limites dos projetos:

```text
src/
├── ObsAi.Domain/
├── ObsAi.Application/
├── ObsAi.Infrastructure/
├── ObsAi.Providers/
├── ObsAi.ObsIntegration/
└── ObsAi.Host/
```

Não introduzir referências proibidas.

Não criar dependências circulares.

Não acoplar Domain e Application a implementações concretas de OBS, provedores externos, banco de dados ou APIs do Windows.

Respeitar os ADRs ACCEPTED.

Não converter ADRs PROPOSED em ACCEPTED sem evidências e escopo apropriados.

---

## 10. IMPLEMENTAÇÃO EXCLUSIVA

Implementar exatamente o que a TASK-005 exigir.

Não antecipar funcionalidades de:

- TASK-006.
- TASK-024.
- Outras Tasks do backlog.

Não implementar recursos externos apenas porque serão necessários futuramente.

Se algum requisito depender de uma Task ainda não concluída, registrar a dependência.

Não executar a Task dependente sem autorização.

Não modificar OBS Studio nem suas configurações.

---

## 11. PRESERVAR A FUNDAÇÃO EXISTENTE

Utilizar:

```text
global.json
Directory.Build.props
Directory.Packages.props
.editorconfig
.gitattributes
OBS-AI-Live-Assistant.slnx
tooling/quality-gates.ps1
docs/testing/TEST_ARCHITECTURE.md
```

Não recriar a solution.

Não reinstalar o SDK sem necessidade e autorização.

Não substituir o Quality Gate existente por uma implementação paralela.

Não adicionar pacotes externos desnecessários.

Preservar LF nos arquivos C# conforme `.gitattributes`.

---

## 12. TESTES AUTOMATIZADOS

Baseline conhecido:

**43 testes aprovados após TASK-004.**

Executar todos os testes existentes e criar os novos testes exigidos pela TASK-005.

Utilizar a arquitetura de testes estabelecida:

- Architecture.
- Unit.
- Integration.
- Contracts.
- Security.
- FailureIsolation.
- Installer.

Escolher somente as suítes aplicáveis ao escopo.

Não criar testes artificiais para aumentar a contagem.

Não confundir testes de scaffold com validação de funcionalidades de produto.

Cada critério de aceite testável deve possuir evidência apropriada.

Registrar:

```text
Testes anteriores:
Testes novos:
Testes executados:
Passed:
Failed:
Skipped:
Not Applicable:
```

Não declarar testes não executados como PASSED.

---

## 13. RESTORE, BUILD E FORMAT

Executar os comandos canônicos definidos no repositório.

Como referência:

```powershell
dotnet --info
dotnet restore
dotnet build --no-restore
dotnet test
dotnet format --verify-no-changes
```

Utilizar também o executor oficial:

```powershell
.\tooling\quality-gates.ps1
```

Inspecionar seus parâmetros e instruções antes de executar.

Não presumir flags inexistentes.

Não alterar o script apenas para fazer os gates passarem.

Se houver falha:

Investigar → Corrigir → Reexecutar.

Registrar exit codes.

Resultados obrigatórios:

```text
Restore: PASSED
Build: PASSED
Tests: PASSED
Format: PASSED
```

---

## 14. ARCHITECTURE QUALITY GATE

Executar os testes arquiteturais.

Verificar:

- Dependências permitidas.
- Dependências proibidas.
- Ausência de ciclos.
- Limites entre camadas.
- SOLID.
- ADR compliance.
- Referências da solution.
- Ausência de funcionalidades fora do escopo.
- Preservação dos contratos existentes.

Resultado obrigatório:

`Architecture Gate: PASSED`

---

## 15. SECURITY QUALITY GATE

Executar Security Review com base nos requisitos aplicáveis e no diff real.

Verificar:

- Segredos.
- Credenciais.
- API keys.
- Tokens.
- Logging sensível.
- Validação de entradas.
- Permissões.
- Fronteiras de confiança.
- Dependências externas.
- Configurações inseguras.
- Falhas de isolamento.
- Regressões de segurança.

Não criar mecanismos de segurança não previstos sem necessidade.

Resultado obrigatório:

`Security Gate: PASSED`

---

## 16. CODE REVIEW

Executar Code Review integral usando as Skills disponíveis.

Avaliar:

- Correção funcional.
- Critérios de aceite.
- Arquitetura.
- SOLID.
- Segurança.
- Testes.
- Tratamento de erros.
- Manutenibilidade.
- Convenções.
- Dependências.
- Documentação.
- Escopo da TASK-005.

Classificar findings:

```text
CRITICAL
HIGH
MEDIUM
LOW
INFO
```

Antes do merge:

- Critical = 0.
- High = 0.
- Medium corrigidos quando possível e apropriado.
- Demais findings documentados e tratados conforme governança.

Reexecutar os gates afetados após as correções.

Não aprovar o próprio código apenas com base no build.

---

## 17. SECRET SCAN

Executar varredura nos arquivos alterados e no conteúdo versionado relevante.

Não exibir valores sensíveis encontrados.

Se houver segredo real, bloquear o merge e seguir o procedimento de segurança.

Resultado obrigatório:

`Secrets: NONE`

---

## 18. DOCUMENTAÇÃO

Toda documentação humana deve permanecer em português do Brasil.

Atualizar apenas os documentos impactados.

Incluir, quando exigido:

- Documentação técnica.
- Evidências dos testes.
- Relatório da TASK-005.
- Critérios de aceite.
- Rastreabilidade.
- Estado do backlog.
- Dependency Graph.

Não reescrever documentação histórica sem necessidade.

Preservar identificadores técnicos oficiais.

---

## 19. PROMPT HISTORY

Inspecionar:

```text
docs/prompts/history/
```

Último prompt conhecido:

`prompt20.md`

Não presumir que o próximo arquivo seja `prompt21.md`.

Calcular o próximo número disponível pelo estado real do repositório.

Arquivar **integralmente este prompt operacional**.

Não sobrescrever histórico.

Não incluir segredos.

O prompt deve fazer parte do commit da TASK-005.

---

## 20. DEFINITION OF DONE

Validar integralmente:

```text
tasks/DEFINITION_OF_DONE.md
```

Antes do merge, todos os gates técnicos aplicáveis devem estar aprovados:

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
Secrets: NONE
Documentation: UPDATED
Prompt History: ARCHIVED
```

Os itens de Git, PR, merge e validação pós-merge serão confirmados nas respectivas etapas.

Não declarar DONE final antes de satisfazer a governança de conclusão.

---

## 21. STATUS E DEPENDENCY GRAPH

Após implementação e gates técnicos:

- Atualizar o estado da TASK-005 conforme workflow canônico.
- Preparar sua transição para DONE.
- Atualizar rastreabilidade.
- Recalcular o Dependency Graph.
- Identificar Tasks desbloqueadas.
- Atualizar os índices afetados.

Se a governança exigir evidência de merge antes de DONE, realizar a finalização administrativa por PR separado, como ocorreu na TASK-004.

Não criar commit direto em `develop`.

Não promover automaticamente Tasks candidatas a READY sem verificar a DoR.

Não executar Tasks desbloqueadas.

---

## 22. COMMIT

Antes de commitar:

```powershell
git status
git diff
git diff --check
```

Inspecionar arquivos novos e não rastreados.

Não versionar:

- `bin/`
- `obj/`
- Logs.
- Arquivos temporários.
- Cache de IDE.
- Secrets.
- Credenciais.
- Artefatos de execução não previstos.

Criar Conventional Commit coerente com o escopo.

Incluir implementação, testes, documentação, atualização de estado permitida e prompt histórico.

---

## 23. PUSH

Publicar exclusivamente a feature branch da TASK-005.

Confirmar:

`local HEAD = remote HEAD`

Não expor credenciais.

Não criar arquivos persistentes com tokens.

Se o push falhar, investigar sem descartar alterações.

---

## 24. PULL REQUEST

Criar PR:

```text
feature/task-TASK-005-* → develop
```

Antes, verificar se já existe PR para a mesma branch.

Não criar PR de teste ou probe.

Se existir PR válido, reutilizar.

Título e descrição em português do Brasil.

O PR deve conter:

- Objetivo.
- Escopo.
- Requisitos atendidos.
- ADRs aplicáveis.
- Critérios de aceite.
- Arquivos modificados.
- Testes.
- Build.
- Architecture Gate.
- Security Gate.
- Code Review.
- Secret Scan.
- Riscos.
- Limitações.

---

## 25. PR VALIDATION

Confirmar:

- Base correta.
- Head correto.
- PR mergeable.
- Diff dentro do escopo.
- Nenhum conflito.
- Gates aprovados.
- Checks obrigatórios concluídos.
- Reviews obrigatórios satisfeitos.
- Nenhum blocker.
- Nenhum segredo.

Não considerar `mergeable_state=clean` como substituto dos Quality Gates.

Se não existir CI, registrar:

`CI: NOT CONFIGURED`

Não afirmar que CI passou quando não há workflow.

---

## 26. MERGE AUTOMÁTICO

Se todos os Quality Gates obrigatórios passarem e a governança permitir:

**Executar merge automático da feature para `develop`.**

Não solicitar autorização intermediária.

Não forçar merge.

Não contornar branch protection.

Não ignorar checks obrigatórios.

Se algum gate falhar:

- Não realizar merge.
- Preservar branch e PR.
- Registrar blocker.
- STOP.

---

## 27. VALIDAÇÃO PÓS-MERGE

Após o merge:

```powershell
git switch develop
git pull --ff-only origin develop
git status
git status --short
```

Confirmar:

`develop local = origin/develop`

Executar novamente o pipeline oficial:

```powershell
.\tooling\quality-gates.ps1
```

Utilizar parâmetros reais do script.

Validar:

- Restore.
- Build.
- Testes.
- Format.
- Architecture.
- Security.
- Estado da TASK-005.
- Rastreabilidade.
- Dependency Graph.

Se houver falha pós-merge, não declarar DONE.

Corrigir por nova feature branch e PR, respeitando a governança.

Após validação final, remover as branches temporárias local e remota quando seguro.

Resultado esperado:

`Working Tree: CLEAN`

---

## 28. NÃO PROMOVER PARA OUTROS AMBIENTES

A autorização termina em `develop`.

Não executar:

```text
develop → hml
hml → release
release → main
```

Não criar:

- `release/1.0.0XXXX`.
- Tag.
- GitHub Release.
- Deploy.

Não modificar `main`.

Não modificar `hml`.

---

## 29. NÃO MODIFICAR OBS

Não alterar:

```text
C:\Program Files\obs-studio
C:\Users\gfmau\AppData\Roaming\obs-studio
```

Não instalar plugins.

Não alterar cenas, perfis, áudio ou configurações.

Não iniciar ações que modifiquem o ambiente OBS.

Resultado obrigatório:

`OBS: NÃO MODIFICADO`

---

## 30. INTERRUPÇÃO E RETOMADA

Se houver:

- Rate limit.
- Usage limit.
- Timeout.
- Falha de ferramenta.
- Falha de autenticação.
- Interrupção de sessão.

Preservar trabalho válido.

Na retomada:

1. Inspecionar Git.
2. Verificar branch atual.
3. Identificar alterações.
4. Identificar último gate concluído.
5. Retomar do ponto exato.
6. Revalidar o que for necessário.

Não reiniciar a Task do zero.

Não executar operações destrutivas:

```powershell
git reset --hard
git clean -fd
git restore .
git checkout -- .
```

Não criar commits artificiais apenas para registrar falhas.

---

## 31. RELATÓRIO FINAL OBRIGATÓRIO

Retornar exatamente estes campos, preenchidos com evidências reais:

```text
OBS-AI-Live-Assistant

TASK:
TASK-005 — <título canônico>

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

Testes anteriores:
<quantidade>

Testes novos:
<quantidade>

Total Tests:
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
APROVADO / BLOQUEADO

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
SIM / NÃO

Prompt:
docs/prompts/history/<arquivo>

Branch:
<nome>

Commit:
<hash>

Push:
OK / BLOQUEADO

Pull Request:
CRIADO / BLOQUEADO

PR Number:
<número>

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

Feature local:
EXCLUÍDA / RETIDA

Feature remota:
EXCLUÍDA / RETIDA

Working Tree:
CLEAN / DIRTY

TASK-005:
DONE / BLOCKED

Novas Tasks READY:
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
<próxima Task READY conforme grafo>

STOP
```

---

## 32. CONDIÇÃO FINAL DE EXECUÇÃO

**EXECUTE A TASK-005 DO SETUP ATÉ O FIM.**

Não parar após implementação, build ou testes.

Prosseguir, quando todos os gates permitirem, por todo o fluxo:

**Setup → DoR → Feature Branch → Implementação → Testes → Quality Gates → Code Review → Correções → Secret Scan → Documentação → Prompt History → Commit → Push → PR → PR Validation → Merge develop → Pós-merge → DoD final → Dependency Graph → Cleanup → Relatório Final → STOP.**

**PROIBIDO executar TASK-006, TASK-024 ou qualquer outra Task.**

**PROIBIDO promover para hml, release ou main.**

**PROIBIDO modificar OBS.**

Ao concluir, emitir relatório final e STOP.
