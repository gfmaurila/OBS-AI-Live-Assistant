AUTORIZAÇÃO — CRIAR BACKLOG + DEPENDENCY GRAPH + IMPLEMENTATION TASKS

PROJETO:
OBS-AI-Live-Assistant

REPOSITÓRIO:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

KIT IA DEV:
D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev

TASK:
backlog-dependency-graph-implementation-tasks

BRANCH:
feature/task-backlog-dependency-graph

============================================================
1. OBJETIVO
============================================================

Transformar toda a documentação aprovada do projeto em um
BACKLOG EXECUTÁVEL, rastreável e ordenado.

Usar como cadeia obrigatória:

Knowledge
→ Requirements
→ Security Requirements
→ Technical Research
→ Architecture
→ ADRs
→ Epics
→ Tasks
→ Dependency Graph
→ Implementation

NÃO implementar código nesta task.

O resultado deve permitir que uma IA posteriormente escolha
a próxima task READY e execute o desenvolvimento sem precisar
reinterpretar toda a arquitetura.

============================================================
2. EXECUÇÃO INTEGRAL
============================================================

ESTA TASK ESTÁ AUTORIZADA PARA EXECUÇÃO COMPLETA.

Executar:

setup
→ leitura das fontes
→ inventário de entregáveis
→ definição dos Epics
→ decomposição em Tasks
→ Dependency Graph
→ rastreabilidade
→ ordem de implementação
→ Definition of Ready
→ Definition of Done
→ Quality Gates
→ estratégia de releases
→ Backlog Quality Gate
→ Security Review
→ Code Review
→ correções
→ Secret Scan
→ Prompt Archive
→ commit
→ push
→ PR
→ PR Validation
→ merge automático para develop
→ sincronização
→ cleanup
→ Working Tree CLEAN
→ relatório final
→ STOP

NÃO solicitar confirmações intermediárias.

Somente parar antecipadamente diante de:

- bloqueio técnico real;
- risco destrutivo;
- segredo/credencial necessário;
- conflito Git não resolvível;
- Critical/High Finding impossível de corrigir;
- decisão obrigatória do cliente que impeça a decomposição.

============================================================
3. IDIOMA
============================================================

Toda documentação humana:

PORTUGUÊS DO BRASIL — pt-BR.

Isso inclui obrigatoriamente:

- descrição dos Epics;
- descrição das Tasks;
- critérios de aceite;
- README;
- Dependency Graph;
- relatórios;
- PR;
- documentação de planejamento.

Preservar nomes técnicos oficiais em inglês quando necessário.

============================================================
4. ESTADO ATUAL
============================================================

Confirmar pelo repositório:

Knowledge Quality Gate:
PASSED

Requirements Quality Gate:
PASSED

Security Quality Gate:
PASSED

Research Quality Gate:
PASSED

Architecture Quality Gate:
PASSED

Security Architecture Review:
PASSED

Backlog Readiness:
READY

RF:
36

RNF:
28

Security Requirements:
34

Research Items:
27

ADRs:
11

ADRs Accepted:
7

ADRs Proposed:
3

ADRs Deferred:
1

Architecture:
CONCLUÍDA

Product Source Code:
NÃO CRIADO

Implementation:
NÃO INICIADA

OBS:
NÃO MODIFICADO

Não confiar apenas neste prompt.

O repositório é a fonte da verdade.

============================================================
5. PREPARAÇÃO GIT
============================================================

Executar:

git fetch origin
git branch --show-current
git status
git status --short
git log --oneline --decorate -15

Confirmar:

Working Tree = CLEAN

Depois:

git switch develop
git pull --ff-only origin develop

Confirmar:

develop local = origin/develop

Criar:

feature/task-backlog-dependency-graph

Toda alteração desta task ocorre nessa branch.

============================================================
6. FONTES CANÔNICAS
============================================================

Ler integralmente o conteúdo relevante de:

README.md
docs/README.md

docs/project/
docs/governance/
docs/knowledge/
docs/requirements/
docs/security/
docs/research/
docs/architecture/
docs/reports/

AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md

.ai/
agent_docs/

Especialmente:

FUNCTIONAL_REQUIREMENTS.md
NON_FUNCTIONAL_REQUIREMENTS.md
SECURITY_REQUIREMENTS.md
REQUIREMENTS_TRACEABILITY.md
SECURITY_TRACEABILITY.md

ARCHITECTURE.md
PROJECT_STRUCTURE.md
ARCHITECTURE_DECISION_MAP.md

todos os ADRs

todos os diagramas arquiteturais

TECHNICAL_RESEARCH_REPORT.md
ARCHITECTURE_REPORT.md

============================================================
7. NÃO REABRIR ARCHITECTURE
============================================================

Esta task NÃO deve redesenhar a arquitetura.

Usar Architecture e ADRs como baseline.

Se detectar contradição realmente impeditiva:

registrar:

ARCHITECTURE_BLOCKER

Não inventar nova arquitetura dentro do Backlog.

============================================================
8. ESTRUTURA DO BACKLOG
============================================================

Organizar:

tasks/

Estrutura recomendada:

tasks/
├── README.md
├── BACKLOG.md
├── DEPENDENCY_GRAPH.md
├── IMPLEMENTATION_ORDER.md
├── TRACEABILITY_MATRIX.md
├── DEFINITION_OF_READY.md
├── DEFINITION_OF_DONE.md
├── RELEASE_PLAN.md
├── backlog/
├── ready/
├── in-progress/
├── review/
├── blocked/
└── done/

Se estrutura equivalente já existir:

CONSOLIDAR.

Não duplicar.

============================================================
9. IDENTIFICAÇÃO DAS TASKS
============================================================

Usar IDs estáveis:

TASK-001
TASK-002
TASK-003
...

Não utilizar apenas nomes soltos.

Cada Task deve ser independente o suficiente para possuir:

branch própria
commit próprio
PR próprio
review próprio
quality gate próprio

Evitar:

Tasks gigantes.

Evitar também:

microtasks artificiais sem valor independente.

============================================================
10. EPICS
============================================================

Criar agrupamentos conceituais.

Avaliar pelo menos:

EPIC-01 — Foundation

EPIC-02 — Domain / Application Core

EPIC-03 — Security Foundation

EPIC-04 — Persistence / Configuration

EPIC-05 — IPC

EPIC-06 — OBS Native Integration

EPIC-07 — Chat Integration

EPIC-08 — AI Providers

EPIC-09 — TTS / Audio

EPIC-10 — OBS Dock / UI

EPIC-11 — Observability / Diagnostics

EPIC-12 — Installer / Update

EPIC-13 — Testing / Quality

EPIC-14 — Integration / Hardening

EPIC-15 — Release Preparation

Esses nomes são referência.

Adaptar à arquitetura REAL.

============================================================
11. ORDEM MACRO DE IMPLEMENTAÇÃO
============================================================

A ordem deve priorizar:

1. FOUNDATION LOCAL
2. SOLUTION / PROJECT STRUCTURE
3. DOMAIN / APPLICATION CORE
4. CONTRACTS / PORTS
5. CONFIGURATION
6. SECURITY FOUNDATION
7. PERSISTENCE
8. IPC
9. OBS INTEGRATION FOUNDATION
10. CHAT
11. AI PROVIDER
12. TTS
13. OBS UI / DOCK
14. OBS AUDIO
15. OBSERVABILITY
16. INSTALLER
17. INTEGRATION
18. HARDENING
19. RELEASE PREPARATION

Porém:

Dependency Graph é a autoridade final.

Não criar ordem artificial que contradiga dependências reais.

============================================================
12. FOUNDATION
============================================================

Planejar Tasks para criação futura da fundação.

Avaliar:

solution
Directory.Build.props
Directory.Packages.props quando apropriado
.editorconfig
global.json quando apropriado
src/
tests/
build configuration
common conventions
dependency rules
architecture tests foundation

NÃO criar esses arquivos nesta task.

Somente planejar.

============================================================
13. PROJECT STRUCTURE
============================================================

Usar a estrutura aprovada em:

PROJECT_STRUCTURE.md

Cada futuro projeto deve possuir propósito claro.

Não inventar projetos sem responsabilidade arquitetural.

Mapear:

Project
→ Architecture Component
→ Requirements
→ ADR
→ Tasks

============================================================
14. CORE
============================================================

Planejar Tasks para módulos do Assistant Core.

Avaliar:

Chat
AI
TTS
Moderation
Context
Session
Configuration
Persistence
Security
Observability
Provider Management

Respeitar Modular Monolith + Ports and Adapters.

============================================================
15. PROVIDER PORTS
============================================================

Planejar abstrações futuras para:

IChatProvider
IAIProvider
ITtsProvider

ou nomes aprovados pela Architecture.

Separar:

contrato

de:

implementação concreta.

============================================================
16. CHAT
============================================================

Planejar Tasks para:

YouTube Live Chat

incluindo quando aplicável:

OAuth
connection
live chat discovery
message reception
normalization
trigger detection
@assistente
!ia
manual activation
reconnect
error handling
rate limiting

Twitch permanece FUTURE quando assim definido.

============================================================
17. AI
============================================================

Planejar:

AI abstraction

antes de provider concreto.

Selecionar somente providers previstos para V1 pela
documentação.

Não transformar todos os candidatos pesquisados em V1.

Planejar:

streaming
timeout
cancellation
usage
errors
response validation

quando aplicável.

============================================================
18. TTS
============================================================

TTS / Audio possui ADR PROPOSED.

Criar explicitamente Task de:

PROTOTYPE / VALIDATION

ANTES da implementação definitiva dependente dessa decisão.

Fluxo:

prototype
→ evidence
→ ADR validation
→ implementation

Não fingir que ADR PROPOSED já está totalmente resolvido.

============================================================
19. COMPATIBILITY
============================================================

Compatibility Strategy está PROPOSED.

Criar Tasks específicas para validar:

OBS 32.x matrix

Windows versions/editions

.NET 10 compatibility

Não deixar isso apenas para o fim do projeto.

============================================================
20. INSTALLER
============================================================

Installer / Update está PROPOSED.

Planejar:

prototype/validation
→ installer foundation
→ OBS detection
→ component deployment
→ upgrade
→ repair
→ uninstall
→ rollback
→ signing readiness

Não implementar nesta task.

============================================================
21. PERSISTENCE
============================================================

Com SQLite ACCEPTED, planejar Tasks para:

database foundation
schema
migrations
repositories/adapters
single-writer strategy
backup
recovery
retention
integration tests

Secrets permanecem fora do SQLite plaintext.

============================================================
22. SECURITY
============================================================

Os 34 Security Requirements devem possuir cobertura no
Backlog.

Planejar Tasks específicas ou critérios transversais para:

BYOK
Credential Manager
DPAPI
secret redaction
input validation
output validation
prompt injection mitigation
authorization
rate limiting
least privilege
IPC security
OBS protection
secure logging

Não criar uma única task genérica:

"implementar segurança".

============================================================
23. IPC
============================================================

Named Pipes está ACCEPTED.

Planejar Tasks para:

protocol contracts
versioning
message envelope
DACL
authentication/authorization quando aplicável
request/response
events
correlation
timeout
cancellation
reconnection
backpressure
C++ side
.NET side
contract tests
failure tests

============================================================
24. OBS NATIVE
============================================================

Planejar componente nativo mínimo.

Separar Tasks quando apropriado para:

plugin skeleton
OBS lifecycle
frontend events
IPC bridge
Dock integration
audio integration
health
shutdown
failure isolation

Evitar transformar native plugin em Core de negócio.

============================================================
25. FAILURE ISOLATION
============================================================

Criar cobertura explícita para:

Assistant Core crash
AI Provider failure
Chat failure
TTS failure
IPC disconnect
database failure
OBS shutdown
Assistant shutdown

Requirement fundamental:

Assistant failure != OBS failure.

============================================================
26. OBSERVABILITY
============================================================

Planejar:

structured logging
correlation IDs
redaction
rotation
diagnostics
health
provider failures
IPC failures
queue metrics quando necessário

Sem stack cloud desnecessária.

============================================================
27. TEST STRATEGY
============================================================

Cada Task de implementação deve indicar tipos de testes
necessários.

Usar quando aplicável:

UNIT
INTEGRATION
ARCHITECTURE
CONTRACT
SECURITY
IPC
FAILURE
OBS COMPATIBILITY
INSTALLER
END-TO-END

Não criar testes agora.

============================================================
28. FORMATO DE CADA TASK
============================================================

Cada arquivo TASK deve conter:

# TASK-XXX — Nome

## Objetivo

## Epic

## Contexto

## Requirements

RF-...
RNF-...
SEC-...

## ADRs

ADR-...

## Dependências

TASK-...

## Bloqueia

TASK-...

## Prioridade

P0
P1
P2
P3

## Complexidade

XS
S
M
L
XL

## Risco

LOW
MEDIUM
HIGH

## Arquivos/áreas esperadas

## Implementação esperada

Em nível suficiente para execução,
sem escrever o código.

## Segurança

## Testes obrigatórios

## Critérios de aceite

## Definition of Ready

## Definition of Done

## Quality Gates

## Status

BACKLOG
READY
IN_PROGRESS
REVIEW
BLOCKED
DONE

============================================================
29. DEFINITION OF READY
============================================================

Criar:

tasks/DEFINITION_OF_READY.md

Uma Task somente pode virar READY se:

- objetivo claro;
- Requirements mapeados;
- ADRs aplicáveis conhecidos;
- dependências concluídas;
- critérios de aceite definidos;
- testes esperados definidos;
- Security impact identificado;
- nenhum blocker aberto;
- escopo suficientemente pequeno;
- branch name determinável.

============================================================
30. DEFINITION OF DONE
============================================================

Criar:

tasks/DEFINITION_OF_DONE.md

Incluir quando aplicável:

implementation complete
build passed
unit tests passed
integration tests passed
architecture tests passed
security validation passed
acceptance criteria passed
code review passed
Critical = 0
High = 0
secret scan passed
documentation updated
prompt archived
commit
push
PR
PR validation
merge develop
post-merge validation
cleanup
Working Tree CLEAN

============================================================
31. QUALITY GATES POR TASK
============================================================

Cada Task deve indicar Gates aplicáveis.

Exemplo:

Build Gate
Unit Test Gate
Integration Test Gate
Architecture Gate
Security Gate
Acceptance Gate
Documentation Gate
Code Review Gate
Secret Scan
Prompt Traceability
GitFlow Gate

Não exigir Gate irrelevante.

============================================================
32. DEPENDENCY GRAPH
============================================================

Criar/atualizar:

tasks/DEPENDENCY_GRAPH.md

Representar:

TASK
→ depends on
→ unlocks

Criar também diagrama Mermaid quando útil.

O grafo deve ser:

ACYCLIC.

Validar ausência de dependências circulares.

============================================================
33. IMPLEMENTATION ORDER
============================================================

Criar:

tasks/IMPLEMENTATION_ORDER.md

Separar por Waves.

Exemplo:

WAVE 0 — Validation / Prototypes

WAVE 1 — Foundation

WAVE 2 — Core

WAVE 3 — Infrastructure

WAVE 4 — OBS / IPC

WAVE 5 — Providers

WAVE 6 — UI / Audio

WAVE 7 — Installer

WAVE 8 — Integration / Hardening

WAVE 9 — Release

Adaptar ao Dependency Graph real.

============================================================
34. PARALELISMO
============================================================

Identificar Tasks que podem ser executadas em paralelo.

Documentar:

PARALLEL SAFE

ou:

SEQUENTIAL

Evitar paralelismo quando Tasks alterarem as mesmas áreas
centrais ou dependerem de contratos ainda instáveis.

============================================================
35. TRACEABILITY MATRIX
============================================================

Criar:

tasks/TRACEABILITY_MATRIX.md

Mapear:

RF
→ Task(s)

RNF
→ Task(s)

SEC
→ Task(s)

ADR
→ Task(s)

Research validation
→ Task(s)

Nenhum requisito MUST deve ficar sem cobertura.

Nenhum Security Requirement MUST deve ficar sem cobertura.

============================================================
36. BACKLOG
============================================================

Criar:

tasks/BACKLOG.md

Tabela consolidada:

ID
Epic
Título
Priority
Complexity
Risk
Dependencies
Status
Wave

============================================================
37. READY
============================================================

Após construir o grafo:

determinar quais Tasks estão realmente READY.

Mover/registrar somente essas em:

tasks/ready/

ou usar o mecanismo canônico já existente.

Não marcar tudo como READY.

Normalmente apenas Tasks raiz do grafo estarão READY.

============================================================
38. RELEASE PLAN
============================================================

Criar:

tasks/RELEASE_PLAN.md

Planejar conceitualmente:

V1 / 1.0.0

Não criar release real.

Relacionar:

MVP/V1 scope
required Tasks
required Gates
required ADR validations
compatibility validation
installer validation
OBS validation

============================================================
39. GITFLOW DE IMPLEMENTAÇÃO
============================================================

Formalizar para futuras Tasks:

develop
↓
feature/task-TASK-XXX-description
↓
implementation
↓
tests
↓
review
↓
prompt archive
↓
commit
↓
push
↓
PR
↓
validation
↓
automatic merge to develop

Cada Task futura deve usar sua própria branch.

============================================================
40. FLUXO DE HOMOLOGAÇÃO E RELEASE
============================================================

Preservar a governança aprovada:

Tasks individuais:

feature/task-*
→ develop

Quando conjunto autorizado estiver pronto para homologação:

develop
→ hml

Após homologação aprovada:

hml
→ release/1.0.0XXXX

Depois da Release Quality Gate:

release/1.0.0XXXX
→ main

Depois:

tag / GitHub Release quando autorizado.

NÃO executar nenhuma dessas promoções nesta task.

============================================================
41. RELEASE VERSION
============================================================

Documentar convenção:

release/1.0.0XXXX

O sufixo deve seguir a governança real existente.

Não inventar uma release agora.

============================================================
42. TASK EXECUTION PROTOCOL
============================================================

Atualizar EXECUTION_PLAN quando necessário para estabelecer:

1. selecionar primeira Task READY pela prioridade;
2. validar dependencies;
3. criar feature/task-*;
4. executar implementação;
5. executar testes;
6. executar quality gates;
7. Code Review;
8. Security Review quando aplicável;
9. corrigir findings;
10. Secret Scan;
11. arquivar prompt;
12. commit;
13. push;
14. PR;
15. PR Validation;
16. merge automático para develop;
17. validar develop;
18. excluir feature;
19. atualizar estado da Task;
20. selecionar próxima Task READY.

IMPORTANTE:

uma autorização futura para execução de uma Task deve
executar o setup dessa Task ATÉ O FIM.

============================================================
43. NÃO EXECUTAR IMPLEMENTAÇÃO
============================================================

PROIBIDO nesta task:

criar src/
criar solution
criar projeto .NET
criar projeto C++
criar database
criar migration
criar installer
criar testes executáveis
instalar plugin
alterar OBS
implementar provider
implementar IPC

É planejamento executável, não implementação.

============================================================
44. BACKLOG QUALITY GATE
============================================================

Validar:

[ ] Epics definidos
[ ] Tasks identificadas
[ ] IDs estáveis
[ ] prioridades
[ ] complexidade
[ ] riscos
[ ] dependencies
[ ] dependency graph acíclico
[ ] implementation order
[ ] waves
[ ] parallelism
[ ] Definition of Ready
[ ] Definition of Done
[ ] Quality Gates
[ ] RF traceability
[ ] RNF traceability
[ ] SEC traceability
[ ] ADR traceability
[ ] MUST requirements cobertos
[ ] MUST security requirements cobertos
[ ] PROPOSED ADR validations planejadas
[ ] release plan
[ ] primeira Task READY identificada
[ ] nenhuma implementação criada

Resultado:

PASSED
ou
BLOCKED.

============================================================
45. IMPLEMENTATION READINESS
============================================================

Calcular:

READY
ou
NOT READY.

Só marcar READY se existir pelo menos uma Task executável
sem decisão bloqueante pendente.

============================================================
46. RELATÓRIO
============================================================

Criar:

docs/reports/BACKLOG_REPORT.md

Incluir:

Epics
Tasks
P0/P1/P2/P3
XS/S/M/L/XL
risks
waves
parallelizable tasks
dependency graph
READY tasks
BLOCKED tasks
coverage RF
coverage RNF
coverage SEC
coverage ADR
Backlog Quality Gate
Implementation Readiness
primeira Task recomendada

============================================================
47. CODE REVIEW DOCUMENTAL
============================================================

Executar Code Review.

Usar Skills disponíveis quando aplicável:

feature-planner
doc-writer
security-audit
code-review
pr-writer

Validar:

granularidade
dependências
ciclos
ordem
rastreabilidade
segurança
Architecture compliance
SOLID
testability
release flow
pt-BR
UTF-8

Classificar:

CRITICAL
HIGH
MEDIUM
LOW
INFO

Corrigir Critical/High.

Corrigir Medium quando seguro.

============================================================
48. SECURITY REVIEW
============================================================

Verificar se o Backlog cobre adequadamente:

34 Security Requirements
Threat Model
Security Risks
Trust Boundaries
BYOK
IPC Security
OBS Isolation
Secrets
Prompt Injection
Input/Output Validation
Installer Security

Nenhum SEC MUST pode desaparecer na decomposição.

============================================================
49. SECRET SCAN
============================================================

Executar Secret Scan.

Resultado necessário:

Secrets:
NONE

============================================================
50. PROMPT TRACEABILITY
============================================================

Inspecionar:

docs/prompts/history/

Último conhecido:

prompt15.md

NÃO assumir automaticamente que o próximo é prompt16.

Calcular pelo estado real.

Arquivar ESTE PROMPT integralmente no próximo identificador
válido.

Não sobrescrever histórico.

============================================================
51. VALIDAÇÃO PRÉ-COMMIT
============================================================

Executar:

git status
git diff
git diff --check

Confirmar:

Backlog = criado
Dependency Graph = criado
Tasks = criadas
Traceability = concluída
Implementation = NÃO INICIADA
Product Code = NÃO CRIADO
OBS = NÃO MODIFICADO
Secrets = NONE

============================================================
52. COMMIT
============================================================

Conventional Commit sugerido:

docs: define implementation backlog

Incluir:

backlog
tasks
dependency graph
traceability
release plan
report
prompt history

============================================================
53. PUSH
============================================================

Push:

origin/feature/task-backlog-dependency-graph

Confirmar:

local HEAD = remote HEAD

============================================================
54. PULL REQUEST
============================================================

Criar PR:

feature/task-backlog-dependency-graph
→
develop

Descrição:

PORTUGUÊS DO BRASIL.

Incluir:

Epics
Tasks
Dependency Graph
Waves
Traceability
Security Coverage
Release Plan
Backlog Quality Gate
Implementation Readiness
primeira Task READY

============================================================
55. PR VALIDATION
============================================================

Obrigatório:

Critical Findings = 0
High Findings = 0
Secrets = NONE
Backlog Quality Gate = PASSED
Security Review = PASSED
Traceability = PASSED
Prompt Traceability = PASSED
GitFlow = PASSED

============================================================
56. MERGE AUTOMÁTICO → DEVELOP
============================================================

Se todos os gates obrigatórios passarem:

feature/task-backlog-dependency-graph
→
develop

MERGE AUTOMÁTICO.

Não solicitar confirmação intermediária.

============================================================
57. PÓS-MERGE
============================================================

Executar:

git switch develop
git pull --ff-only origin develop

Confirmar:

develop local = origin/develop

Excluir feature local e remota.

Confirmar:

Working Tree = CLEAN

============================================================
58. NÃO PROMOVER
============================================================

Esta task termina em:

develop.

NÃO executar:

develop → hml

NÃO criar:

release/1.0.0XXXX

NÃO alterar:

main

NÃO criar:

tag
GitHub Release

============================================================
59. RESULTADO FINAL
============================================================

Retornar:

OBS-AI-Live-Assistant

TASK:
backlog-dependency-graph-implementation-tasks

TASK STATUS:
CONCLUÍDA / BLOQUEADA

Execução integral:
CONCLUÍDA / BLOQUEADA

Epics:
<quantidade>

Tasks:
<quantidade>

P0:
<quantidade>

P1:
<quantidade>

P2:
<quantidade>

P3:
<quantidade>

XS:
<quantidade>

S:
<quantidade>

M:
<quantidade>

L:
<quantidade>

XL:
<quantidade>

Waves:
<quantidade>

Tasks READY:
<quantidade>

Tasks BLOCKED:
<quantidade>

Parallel Safe:
<quantidade>

Dependency Graph:
PASSED / BLOCKED

Dependency Cycles:
<quantidade>

RF Coverage:
<percentual>

RNF Coverage:
<percentual>

SEC Coverage:
<percentual>

ADR Coverage:
<percentual>

Proposed ADR Validation Tasks:
<quantidade>

Definition of Ready:
PASSED / BLOCKED

Definition of Done:
PASSED / BLOCKED

Release Plan:
CONCLUÍDO / BLOQUEADO

Backlog Quality Gate:
PASSED / BLOCKED

Security Review:
PASSED / BLOCKED

Implementation Readiness:
READY / NOT READY

Primeira Task READY:
<TASK-ID — nome>

Relatório:
docs/reports/BACKLOG_REPORT.md

Prompt arquivado:
SIM / NÃO

Prompt:
docs/prompts/history/<arquivo>

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

Branch:
feature/task-backlog-dependency-graph

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

hml:
NÃO MODIFICADA

Release:
NÃO CRIADA

main:
NÃO MODIFICADA

Product Source Code:
NÃO CRIADO

Implementation:
NÃO INICIADA

OBS:
NÃO MODIFICADO

PRÓXIMO:

Se:

Backlog Quality Gate = PASSED
Security Review = PASSED
Implementation Readiness = READY

então retornar:

EXECUTAR PRIMEIRA TASK READY:
<TASK-ID — nome>

Caso contrário:

RESOLVER BACKLOG BLOCKERS

============================================================
STOP
============================================================

STOP SOMENTE DEPOIS DE:

Backlog
→ Epics
→ Tasks
→ Dependency Graph
→ Implementation Order
→ Waves
→ Traceability
→ Definition of Ready
→ Definition of Done
→ Release Plan
→ Backlog Quality Gate
→ Security Review
→ Code Review
→ correções
→ Secret Scan
→ Prompt Archive
→ commit
→ push
→ PR
→ PR Validation
→ merge develop
→ sincronização
→ cleanup
→ Working Tree CLEAN
→ relatório final.

NÃO iniciar a primeira Task de implementação automaticamente.

Aguardar nova autorização.
