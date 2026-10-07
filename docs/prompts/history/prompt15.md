AUTORIZAÇÃO — EXECUTAR ARCHITECTURE + ADRs COMPLETO

PROJETO:
OBS-AI-Live-Assistant

REPOSITÓRIO:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

KIT IA DEV:
D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev

TASK:
architecture-adrs

BRANCH:
feature/task-architecture-adrs

============================================================
1. OBJETIVO
============================================================

Criar a Architecture Baseline oficial do
OBS-AI-Live-Assistant e transformar os ADR Candidates
preparados durante Requirements, Security e Technical Research
em decisões arquiteturais formais, rastreáveis e justificadas.

A arquitetura deve ser derivada de:

Knowledge
→ Requirements
→ Security Requirements
→ Technical Research
→ ADR Candidates
→ Architecture

NÃO iniciar implementação.

============================================================
2. EXECUÇÃO INTEGRAL
============================================================

ESTA TASK ESTÁ AUTORIZADA PARA EXECUÇÃO COMPLETA.

Executar até o fim:

setup
→ leitura completa do contexto
→ consolidação das decisões pendentes
→ Architecture Baseline
→ diagramas
→ ADRs
→ rastreabilidade
→ Architecture Quality Gate
→ Security Architecture Review
→ Code Review documental
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

============================================================
3. REGRA PARA INTERRUPÇÃO POR LIMITE
============================================================

O ambiente pode atingir limite de uso durante esta task.

Portanto:

- trabalhar incrementalmente;
- salvar documentos conforme forem concluídos;
- nunca descartar trabalho válido;
- nunca executar reset destrutivo;
- nunca recomeçar a task do zero sem necessidade.

Se ocorrer interrupção externa por:

usage limit
timeout
sessão encerrada
quota

o estado atual deve permanecer recuperável.

Na retomada:

INSPECIONAR O ESTADO REAL
E CONTINUAR DO PONTO EM QUE PAROU.

PROIBIDO usar para "resolver" interrupção:

git reset --hard
git restore .
git clean -fd
git checkout -- .

Não criar commit artificial apenas para contornar limite.

============================================================
4. IDIOMA
============================================================

Toda documentação humana:

PORTUGUÊS DO BRASIL — pt-BR.

Incluindo:

README
Architecture
ADRs
diagramas textuais
relatórios
PR
Task descriptions

Preservar nomes técnicos:

OBS Studio
libobs
OBS Frontend API
obs-websocket
C++
C#
.NET
IPC
Named Pipes
SQLite
BYOK
TTS
OAuth
DPAPI
Windows Credential Manager
ADR
C4
etc.

============================================================
5. ESTADO ESPERADO
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

Architecture Readiness:
READY

Architecture Security Readiness:
READY

RF:
36

RNF:
28

Security Requirements:
34

Research Items:
27

Research Blocked:
0

ADR Candidates:
aproximadamente 10 — confirmar estado real.

Architecture:
NOT STARTED

Implementation:
NOT STARTED

OBS:
NOT MODIFIED

O repositório é a fonte da verdade.

============================================================
6. PREPARAÇÃO GIT
============================================================

Executar:

git fetch origin
git branch --show-current
git status
git status --short
git log --oneline --decorate -15

Working Tree deve estar CLEAN antes da nova task.

Depois:

git switch develop
git pull --ff-only origin develop

Confirmar:

develop local = origin/develop

Criar:

feature/task-architecture-adrs

Toda alteração deve ocorrer nessa branch.

============================================================
7. FONTES CANÔNICAS
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
docs/reports/

AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md

.ai/
agent_docs/

Prioridade especial para:

REQUIREMENTS.md
FUNCTIONAL_REQUIREMENTS.md
NON_FUNCTIONAL_REQUIREMENTS.md
CONSTRAINTS.md
RISKS.md
OPEN_QUESTIONS.md
ADR_CANDIDATES.md
REQUIREMENTS_TRACEABILITY.md

SECURITY_REQUIREMENTS.md
THREAT_MODEL.md
TRUST_BOUNDARIES.md
SECURITY_RISKS.md
SECURITY_TRACEABILITY.md

RESEARCH_MATRIX.md
RESEARCH_BACKLOG.md
TECHNICAL_RESEARCH_REPORT.md

e todos os arquivos:

docs/research/*.md

============================================================
8. PRINCÍPIO ARQUITETURAL
============================================================

Architecture agora pode decidir COMO.

Porém toda decisão importante deve possuir:

Requirement
+
Security Requirement quando aplicável
+
Research Evidence
+
Trade-offs
+
ADR

Não decidir tecnologia importante apenas por preferência.

============================================================
9. CLIENT DECISIONS
============================================================

A pesquisa possui itens classificados como CLIENT_DECISION.

Antes de bloquear Architecture:

verificar se são realmente necessários para Architecture
Baseline.

Se forem NON-BLOCKING:

documentar alternativas e manter configurável/extensível.

Se uma decisão for realmente necessária agora:

classificar:

REQUIRES_CLIENT_DECISION

e explicar impacto.

Não inventar decisão do cliente.

============================================================
10. ARQUITETURA DE ALTO NÍVEL
============================================================

Criar arquitetura conceitual incluindo:

OBS Studio
↓
OBS Integration Layer
↓
IPC Boundary
↓
Assistant Core

Assistant Core
├── Chat
├── AI
├── TTS
├── Context
├── Moderation
├── Security
├── Persistence
├── Configuration
├── Observability
└── Provider Abstractions

External:
├── YouTube
├── AI Providers
└── TTS Providers

Local:
├── Secure Credentials
├── SQLite
├── Logs
└── Configuration

============================================================
11. ESTILO ARQUITETURAL
============================================================

Avaliar formalmente e decidir através de ADR:

Modular Monolith
+
Plugin/Provider Architecture
+
Process Isolation

Não introduzir microservices sem Requirement.

Não introduzir:

Kafka
RabbitMQ
Redis
Kubernetes
cloud infrastructure

somente por preferência arquitetural.

============================================================
12. HYBRID OBS ARCHITECTURE
============================================================

A Technical Research recomendou como input:

Hybrid Architecture

com:

minimal native OBS integration
+
Assistant Core separado.

Validar evidências.

Se confirmadas:

formalizar via ADR.

Definir claramente responsabilidades de:

Native OBS Component

Assistant Core

OBS WebSocket

IPC

Evitar sobreposição desnecessária.

============================================================
13. NATIVE OBS COMPONENT
============================================================

Definir responsabilidade mínima necessária.

Avaliar:

- OBS lifecycle;
- Dock/UI integration;
- frontend events;
- native audio integration quando necessário;
- bridge para Core;
- health/status;
- graceful degradation.

Regra:

quanto menor o código dentro do processo OBS,
menor o blast radius.

============================================================
14. ASSISTANT CORE
============================================================

Definir responsabilidade do Core .NET.

Avaliar módulos:

Chat
AI
TTS
Moderation
Context
Memory
Persistence
Configuration
Security
Observability
Provider Management
Session Management

Core deve permanecer desacoplado do OBS quando possível.

============================================================
15. PROCESS ISOLATION
============================================================

Formalizar ADR.

Requisito central:

Assistant failure != OBS failure.

Definir:

process boundary
lifecycle
startup
shutdown
reconnection
health
crash behavior
recovery behavior

============================================================
16. IPC
============================================================

Usar Technical Research como evidência.

A recomendação existente indica:

Named Pipes
+
DACL explícita
+
protocolo versionado.

Validar e, se adequado, formalizar ADR.

Definir:

transport
message envelope
version
request/response
events
correlation ID
timeout
cancellation
reconnect
authentication/authorization
backpressure
failure behavior

Não implementar protocolo nesta task.

============================================================
17. OBS WEBSOCKET
============================================================

Definir papel arquitetural.

Não utilizar WebSocket para algo que exija necessariamente
plugin nativo.

Não colocar código nativo onde WebSocket seja suficiente.

Documentar matriz de responsabilidades.

============================================================
18. PROVIDER ARCHITECTURE
============================================================

Definir abstrações arquiteturais equivalentes a:

IAIProvider
ITtsProvider
IChatProvider

Os nomes finais podem ser refinados.

AI candidates:

OpenAI
Anthropic
Gemini
Ollama
OpenRouter
Custom/OpenAI-compatible

TTS:

Windows TTS
Azure Speech
ElevenLabs

Chat:

YouTube V1
Twitch FUTURE

Arquitetura deve permitir extensão sem modificar Core
desnecessariamente.

Aplicar SOLID.

============================================================
19. SOLID
============================================================

SOLID é obrigatório.

Documentar como a arquitetura atende:

SRP
OCP
LSP
ISP
DIP

Especialmente:

Provider abstractions
OBS integration
Persistence
Security
TTS
Chat
AI

Evitar abstração sem necessidade.

============================================================
20. PIPELINE
============================================================

Formalizar arquitetura do pipeline:

Live Chat
→ Message Listener
→ Normalization
→ Trigger Detection
→ Moderation
→ Rate Limiter
→ Request Queue
→ Context Builder
→ AI Provider
→ Response Validation
→ Response Queue
→ Text/TTS
→ Output

Definir boundaries e responsabilidades.

============================================================
21. CONCURRENCY / QUEUES
============================================================

Definir arquitetura conceitual para:

bounded queues
backpressure
cancellation
timeouts
provider concurrency
TTS queue
response queue
shutdown

Não adicionar broker externo sem necessidade.

Preferir mecanismos locais adequados ao V1.

============================================================
22. LIVE CONTEXT
============================================================

Definir modelo conceitual:

LiveContext

Pode incluir:

streamer
live title
platform
game
OBS scene
assistant profile
recent interactions
session
custom context

Não definir schema final ainda se desnecessário.

============================================================
23. MEMORY
============================================================

Separar:

Short-Term Memory

Persistent Memory

Se persistent memory continuar CLIENT_DECISION:

arquitetura deve suportar extensão futura sem torná-la
obrigatória no V1.

============================================================
24. PERSISTENCE
============================================================

Com base na pesquisa, decidir via ADR se SQLite será a
persistência local V1.

Se aprovado:

definir responsabilidades conceituais:

Settings
Provider Metadata
Profiles
Sessions
Interactions
Moderation
Statistics
Commands quando aplicável

Secrets:

NÃO ficam em plaintext no SQLite.

Definir:

migration strategy
backup strategy
lifecycle
recovery
retention boundary

sem implementar.

============================================================
25. SECURE CREDENTIAL STORAGE
============================================================

Usar evidência da pesquisa:

Windows Credential Manager
DPAPI

Formalizar ADR apropriado.

Definir:

qual tipo de segredo vai para qual mecanismo,
se aplicável.

BYOK deve permanecer isolado de:

SQLite
logs
prompt history
config plaintext.

============================================================
26. SECURITY ARCHITECTURE
============================================================

Transformar Security Requirements em arquitetura.

Documentar:

Trust Boundaries
Input Validation
Output Validation
Authorization
Secrets
Rate Limiting
Prompt Injection Defense
Logging Redaction
Provider Isolation
IPC Security
OBS Protection
Least Privilege
Failure Isolation

Não alegar segurança absoluta.

============================================================
27. CHAT SECURITY
============================================================

Arquitetura deve assumir:

CHAT INPUT = UNTRUSTED.

Nenhuma mensagem de viewer pode diretamente:

executar shell
ler arquivo
ler segredo
modificar configuração crítica
executar comando sensível do OBS

sem camada explícita autorizada.

============================================================
28. AI OUTPUT SECURITY
============================================================

Arquitetura deve assumir:

AI OUTPUT = UNTRUSTED.

AI não recebe autoridade implícita.

Qualquer ação sensível futura precisa de:

policy
validation
authorization
allowlist quando apropriado
auditability

============================================================
29. TTS / AUDIO ARCHITECTURE
============================================================

Usar Technical Research.

Definir arquitetura recomendada para:

TTS generation
queue
audio lifecycle
OBS integration
volume
mute
cancel
failure
routing

Se escolha final ainda depender de protótipo:

criar ADR com status:

PROPOSED

ou:

ACCEPTED WITH VALIDATION

conforme governança.

Não fingir certeza inexistente.

============================================================
30. OBS DOCK / UI
============================================================

Definir papel arquitetural da UI.

Documentar:

configuration
provider status
connection status
assistant enable/disable
profile
TTS
diagnostics
security-sensitive settings

Não implementar UI.

============================================================
31. OBSERVABILITY
============================================================

Definir arquitetura local para:

structured logging
correlation ID
health
diagnostics
log rotation
redaction
provider failures
IPC failures
queue metrics quando aplicável.

Não introduzir infraestrutura cloud.

============================================================
32. ERROR HANDLING / RESILIENCE
============================================================

Definir políticas arquiteturais para:

timeout
cancellation
retry
backoff
provider failure
IPC disconnect
database error
TTS failure
chat disconnect
shutdown

Evitar retry infinito.

============================================================
33. INSTALLER ARCHITECTURE
============================================================

Usar Technical Research.

Definir arquitetura conceitual para:

OBS-AI-Live-Assistant-Setup.exe

Considerar:

OBS detection
native plugin deployment
Core deployment
AppData
prerequisites
upgrade
repair
uninstall
rollback
version detection
code signing

Não criar installer.

============================================================
34. UPDATE ARCHITECTURE
============================================================

Definir direção arquitetural para:

manual update
installer-based update
ou alternativa recomendada pela pesquisa.

Não implementar updater.

============================================================
35. COMPATIBILITY STRATEGY
============================================================

Definir política arquitetural para:

OBS 32.x x64

e evolução futura.

Também tratar explicitamente a descoberta da pesquisa sobre:

.NET 10
+
Windows 10 editions/support.

Não inventar suporte universal ao Windows 10.

Registrar eventual CLIENT_DECISION.

============================================================
36. DEPLOYMENT VIEW
============================================================

Criar visão conceitual:

Windows Host
│
├── OBS Studio Process
│   └── OBS-AI Native Integration
│
├── OBS-AI Assistant Core Process
│   ├── Chat
│   ├── AI
│   ├── TTS
│   ├── Persistence
│   └── Security
│
├── Secure Credential Store
├── SQLite
├── Logs
│
└── External Providers

============================================================
37. C4
============================================================

Criar diagramas equivalentes a:

C4 Context
C4 Container
C4 Component

Pode usar:

Mermaid

quando adequado ao repositório.

Os diagramas devem ser versionáveis como texto.

Não depender apenas de PNG.

============================================================
38. DIAGRAMAS OBRIGATÓRIOS
============================================================

Criar em:

docs/architecture/diagrams/

pelo menos:

SYSTEM_CONTEXT.md
CONTAINER_VIEW.md
COMPONENT_VIEW.md
OBS_INTEGRATION_FLOW.md
CHAT_AI_TTS_FLOW.md
SECURITY_BOUNDARIES.md
DEPLOYMENT_VIEW.md
FAILURE_ISOLATION.md

Se já existirem equivalentes:

atualizar/consolidar.

============================================================
39. PROJECT STRUCTURE
============================================================

Atualizar:

docs/architecture/PROJECT_STRUCTURE.md

Agora Architecture pode definir estrutura física planejada.

Avaliar estrutura futura semelhante a:

src/
├── ObsAi.Core/
├── ObsAi.Application/
├── ObsAi.Domain/
├── ObsAi.Infrastructure/
├── ObsAi.Providers/
├── ObsAi.ObsBridge/
└── native/
    └── ObsAi.ObsPlugin/

tests/
├── Unit/
├── Integration/
└── Architecture/

installer/

tooling/

IMPORTANTE:

isso é apenas exemplo conceitual.

A Architecture deve definir a estrutura baseada nas decisões
reais.

NÃO criar essas pastas de código nesta task.

============================================================
40. DEPENDENCY RULES
============================================================

Documentar dependências permitidas.

Exemplo conceitual:

Domain
↑
Application
↑
Infrastructure/Adapters
↑
Host

Mas não copiar Clean Architecture mecanicamente.

Definir conforme Architecture real.

Evitar dependência do Domain em:

OBS
SQLite
OpenAI
YouTube
TTS vendor
Windows APIs

quando não necessária.

============================================================
41. ADRs
============================================================

Criar em:

docs/architecture/decisions/

Convenção sugerida:

ADR-001-architecture-style.md
ADR-002-obs-integration-strategy.md
ADR-003-process-isolation.md
ADR-004-ipc-strategy.md
ADR-005-persistence.md
ADR-006-secret-storage.md
ADR-007-provider-architecture.md
ADR-008-tts-audio.md
ADR-009-installer-update.md
ADR-010-compatibility-strategy.md

NÃO assumir exatamente 10.

Usar os ADR Candidates reais.

============================================================
42. TEMPLATE ADR
============================================================

Cada ADR deve conter:

ID
Título
Status
Data
Contexto
Requirements relacionados
Security Requirements relacionados
Research Evidence
Opções consideradas
Decisão
Justificativa
Trade-offs
Consequências positivas
Consequências negativas
Security Impact
OBS Impact
Implementation Impact
Validation Required
Supersedes
Superseded By

Status permitido conforme maturidade:

ACCEPTED
PROPOSED
DEFERRED
REJECTED

Não marcar ACCEPTED sem evidência suficiente.

============================================================
43. ARCHITECTURE DECISION MAP
============================================================

Criar:

docs/architecture/ARCHITECTURE_DECISION_MAP.md

Mapear:

Requirement
→ Security Requirement
→ Research
→ ADR
→ Component
→ Future Task

============================================================
44. ARCHITECTURE BASELINE
============================================================

Criar documento principal:

docs/architecture/ARCHITECTURE.md

Deve conter:

Visão Geral
Goals
Non-Goals
Principles
System Context
Containers
Components
Processes
Boundaries
Data Flow
Security
Persistence
Providers
OBS Integration
IPC
TTS
Observability
Deployment
Installer
Compatibility
Failure Modes
ADR Summary
Traceability
Known Limitations
Open Decisions
Implementation Guidance

============================================================
45. ARCHITECTURE README
============================================================

Atualizar:

docs/architecture/README.md

para funcionar como índice.

============================================================
46. IMPLEMENTATION GUIDANCE
============================================================

Architecture pode preparar diretrizes para implementação.

Porém NÃO criar backlog detalhado ainda.

Registrar:

component boundaries
dependency rules
coding constraints
security constraints
test boundaries

Backlog será task posterior.

============================================================
47. TEST ARCHITECTURE
============================================================

Definir estratégia arquitetural de testes:

Unit
Integration
Architecture Tests
IPC Tests
Provider Contract Tests
Failure Isolation Tests
Security Tests
Installer Tests
OBS Compatibility Tests

Não criar testes nesta task.

============================================================
48. ARCHITECTURE QUALITY GATE
============================================================

Executar gate completo.

Validar:

[ ] Architecture derivada dos Requirements
[ ] Security incorporada
[ ] Research incorporada
[ ] ADRs criados
[ ] decisões rastreáveis
[ ] System Context criado
[ ] Container View criado
[ ] Component View criado
[ ] Deployment View criado
[ ] OBS boundary definido
[ ] process isolation definido
[ ] IPC definido
[ ] provider architecture definida
[ ] persistence definida
[ ] secrets definida
[ ] TTS/audio tratada
[ ] installer tratada
[ ] compatibility tratada
[ ] observability definida
[ ] failure isolation definida
[ ] SOLID aplicado
[ ] dependency rules definidas
[ ] test architecture definida
[ ] open decisions explícitas
[ ] nenhuma implementação criada
[ ] Architecture pronta para Backlog

Resultado:

PASSED
ou
BLOCKED.

============================================================
49. SECURITY ARCHITECTURE REVIEW
============================================================

Revalidar contra:

34 Security Requirements
20 Assets
22 Threats
8 Trust Boundaries
15 Security Risks

Verificar especialmente:

BYOK
secrets
IPC
OBS isolation
prompt injection
AI output
chat input
logging
installer
provider boundaries

============================================================
50. ARCHITECTURE REPORT
============================================================

Criar:

docs/reports/ARCHITECTURE_REPORT.md

Incluir:

- fontes;
- arquitetura escolhida;
- componentes;
- ADRs;
- ADRs ACCEPTED;
- ADRs PROPOSED;
- ADRs DEFERRED;
- diagrams;
- security review;
- client decisions;
- open architecture decisions;
- Architecture Quality Gate;
- Backlog Readiness;
- blockers;
- recomendação.

============================================================
51. BACKLOG READINESS
============================================================

Calcular:

READY
ou
NOT READY.

Pode ser READY com ADRs PROPOSED não bloqueantes.

Não pode ser READY se existir decisão estrutural obrigatória
sem solução suficiente para decompor o sistema em Tasks.

============================================================
52. DOCUMENTAÇÃO
============================================================

Atualizar quando necessário:

README.md
docs/README.md
docs/architecture/README.md
docs/architecture/PROJECT_STRUCTURE.md
docs/knowledge/PROJECT_KNOWLEDGE_MAP.md
docs/requirements/REQUIREMENTS_TRACEABILITY.md
docs/security/SECURITY_TRACEABILITY.md
docs/reports/README.md

Evitar duplicação.

============================================================
53. CODE REVIEW DOCUMENTAL
============================================================

Usar Skills disponíveis quando aplicável:

feature-planner
doc-writer
security-audit
code-review
pr-writer

Validar:

architecture consistency
SOLID
coupling
cohesion
dependency direction
security
traceability
research evidence
ADR consistency
diagram consistency
pt-BR
UTF-8
links
scope

Classificar:

CRITICAL
HIGH
MEDIUM
LOW
INFO

Corrigir Critical/High.

Corrigir Medium quando seguro.

============================================================
54. SECRET SCAN
============================================================

Executar Secret Scan.

Nenhuma credencial real é necessária.

Resultado obrigatório para aprovação:

Secrets:
NONE

============================================================
55. PROMPT TRACEABILITY
============================================================

Inspecionar:

docs/prompts/history/

Último conhecido:

prompt14.md

NÃO assumir que o próximo é prompt15.

Calcular pelo estado real.

Arquivar ESTE PROMPT integralmente no próximo identificador
válido.

Não sobrescrever histórico.

============================================================
56. VALIDAÇÃO PRÉ-COMMIT
============================================================

Executar:

git status
git diff
git diff --check

Confirmar:

Architecture = concluída

ADRs = criados

Implementation = não iniciada

Product Source Code = não criado

OBS = não modificado

Secrets = NONE

Prompt Traceability = OK

============================================================
57. COMMIT
============================================================

Conventional Commit sugerido:

docs: define architecture and ADRs

Incluir no commit:

Architecture
ADRs
diagrams
traceability
report
prompt history
correções

============================================================
58. PUSH
============================================================

Push:

origin/feature/task-architecture-adrs

Confirmar:

local HEAD = remote HEAD

============================================================
59. PULL REQUEST
============================================================

Criar PR:

feature/task-architecture-adrs
→
develop

Descrição em pt-BR.

Incluir:

Architecture summary
ADRs
diagrams
security
research evidence
trade-offs
client decisions
Architecture Quality Gate
Backlog Readiness

============================================================
60. PR VALIDATION
============================================================

Obrigatório:

Critical Findings = 0
High Findings = 0
Secrets = NONE
Architecture Quality Gate = PASSED
Security Architecture Review = PASSED
Prompt Traceability = PASSED
GitFlow = PASSED

============================================================
61. MERGE AUTOMÁTICO → DEVELOP
============================================================

Se gates obrigatórios passarem:

feature/task-architecture-adrs
→
develop

MERGE AUTOMÁTICO.

Não solicitar confirmação intermediária.

============================================================
62. PÓS-MERGE
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
63. NÃO PROMOVER
============================================================

Esta é uma Task individual.

Termina em:

develop.

NÃO executar:

develop → hml

NÃO criar ainda:

release/1.0.0XXXX

NÃO modificar:

main

NÃO criar:

tag
GitHub Release

O fluxo:

develop
→ hml
→ release/1.0.0XXXX
→ main

será executado posteriormente no momento de promoção/release
autorizado pela governança.

============================================================
64. NÃO IMPLEMENTAR
============================================================

NÃO criar:

src/
solution .NET
projetos C#
projeto C++
database
migrations
installer
tests executáveis
plugin OBS

NÃO modificar:

C:\Program Files\obs-studio

NÃO modificar:

C:\Users\gfmau\AppData\Roaming\obs-studio

NÃO instalar nada no OBS.

============================================================
65. RESULTADO FINAL
============================================================

Retornar:

OBS-AI-Live-Assistant

TASK:
architecture-adrs

TASK STATUS:
CONCLUÍDA / BLOQUEADA

Execução integral:
CONCLUÍDA / BLOQUEADA

Architecture:
CONCLUÍDA / BLOQUEADA

Architecture Style:
<resultado>

OBS Integration Strategy:
<resultado>

Process Isolation:
<resultado>

IPC Strategy:
<resultado>

Persistence:
<resultado>

Secret Storage:
<resultado>

Provider Architecture:
<resultado>

TTS / Audio:
<resultado>

Installer / Update:
<resultado>

Compatibility Strategy:
<resultado>

SOLID:
VALIDADO / BLOQUEADO

C4 Context:
CONCLUÍDO / BLOQUEADO

C4 Container:
CONCLUÍDO / BLOQUEADO

C4 Component:
CONCLUÍDO / BLOQUEADO

Deployment View:
CONCLUÍDO / BLOQUEADO

Security Boundaries:
CONCLUÍDO / BLOQUEADO

Failure Isolation:
CONCLUÍDO / BLOQUEADO

ADRs:
<quantidade>

ADRs Accepted:
<quantidade>

ADRs Proposed:
<quantidade>

ADRs Deferred:
<quantidade>

Client Decisions:
<quantidade>

Open Architecture Decisions:
<quantidade>

Architecture Traceability:
PASSED / BLOCKED

Architecture Quality Gate:
PASSED / BLOCKED

Security Architecture Review:
PASSED / BLOCKED

Backlog Readiness:
READY / NOT READY

Relatório:
docs/reports/ARCHITECTURE_REPORT.md

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
feature/task-architecture-adrs

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

Architecture Quality Gate = PASSED
Security Architecture Review = PASSED
Backlog Readiness = READY

então:

BACKLOG + DEPENDENCY GRAPH + IMPLEMENTATION TASKS

Caso contrário:

RESOLVER ARCHITECTURE BLOCKERS

============================================================
STOP
============================================================

STOP SOMENTE DEPOIS DE CONCLUIR:

Architecture
→ ADRs
→ diagramas
→ rastreabilidade
→ Architecture Quality Gate
→ Security Architecture Review
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

NÃO iniciar Backlog ou implementação automaticamente.
