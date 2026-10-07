AUTORIZAÇÃO — EXECUTAR TECHNICAL RESEARCH COMPLETO

PROJETO:
OBS-AI-Live-Assistant

REPOSITÓRIO:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

KIT IA DEV:
D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev

TASK:
technical-research

BRANCH:
feature/task-technical-research

============================================================
1. EXECUÇÃO INTEGRAL
============================================================

ESTA TASK ESTÁ AUTORIZADA PARA EXECUÇÃO COMPLETA.

Executar integralmente:

setup
→ leitura das fontes internas
→ levantamento das Research Dependencies
→ pesquisa técnica externa
→ coleta de evidências
→ análise de alternativas
→ recomendações
→ atualização do Research Backlog
→ preparação dos ADR Candidates
→ Research Quality Gate
→ Architecture Readiness
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

Somente interromper antecipadamente diante de:

- bloqueio técnico real;
- risco destrutivo;
- credencial/segredo necessário;
- conflito Git não resolvível com segurança;
- decisão obrigatória do cliente que bloqueie a pesquisa;
- Critical/High Finding impossível de corrigir;
- necessidade de alteração fora do escopo autorizado.

============================================================
2. IDIOMA
============================================================

Documentação humana:

PORTUGUÊS DO BRASIL — pt-BR.

Preservar nomes técnicos oficiais:

OBS Studio
libobs
obs-websocket
OBS Frontend API
Dock
C++
C#
.NET
IPC
Named Pipes
gRPC
SQLite
DPAPI
Windows Credential Manager
OAuth
YouTube Live Streaming API
TTS
BYOK
SDK
API
ABI
SemVer
etc.

============================================================
3. ESTADO ATUAL
============================================================

Estado esperado:

Knowledge Quality Gate:
PASSED

Requirements Quality Gate:
PASSED

Security Quality Gate:
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

Research Dependencies:
24

Security Research Dependencies:
12

ADR Candidates:
10

Architecture:
NOT STARTED

Implementation:
NOT STARTED

Product Source Code:
NOT CREATED

OBS:
NOT MODIFIED

Confirmar tudo pelo repositório.

O repositório é a fonte da verdade.

============================================================
4. PREPARAÇÃO GIT
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

feature/task-technical-research

Toda alteração deve ocorrer nessa branch.

============================================================
5. FONTES INTERNAS
============================================================

Ler integralmente o conteúdo relevante de:

README.md
docs/README.md

docs/project/
docs/governance/
docs/knowledge/
docs/research/
docs/requirements/
docs/security/
docs/reports/

AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md

.ai/
agent_docs/

Dar atenção especial a:

RESEARCH_BACKLOG.md
RESEARCH_DEPENDENCIES.md
ADR_CANDIDATES.md
OPEN_QUESTIONS.md
RISKS.md
SECURITY_RESEARCH_DEPENDENCIES.md
SECURITY_RISKS.md
THREAT_MODEL.md
TRUST_BOUNDARIES.md
REQUIREMENTS_TRACEABILITY.md
SECURITY_TRACEABILITY.md

============================================================
6. INVENTÁRIO DA PESQUISA
============================================================

Antes de pesquisar externamente, construir inventário único
das dependências.

Consolidar:

Requirements Research Dependencies
+
Security Research Dependencies
+
Open Questions classificadas como RESEARCH
+
ADR Candidates que dependem de evidência técnica.

Eliminar duplicações conceituais.

Não perder rastreabilidade dos IDs originais.

Cada Research Item deve possuir ID:

RES-001
RES-002
RES-003
...

Para cada item registrar:

ID
Título
Pergunta
Origem
Requirements relacionados
Security Requirements relacionados
ADR relacionado
Prioridade
Blocking?
Fontes necessárias
Status

============================================================
7. POLÍTICA DE FONTES
============================================================

Pesquisa externa ESTÁ AUTORIZADA nesta task.

Prioridade obrigatória:

1. documentação oficial;
2. repositório oficial;
3. documentação oficial de SDK/API;
4. documentação oficial Microsoft/.NET;
5. documentação oficial Google/YouTube;
6. documentação oficial dos providers;
7. specifications/standards;
8. fontes técnicas secundárias somente quando necessário.

Evitar basear decisão importante em:

- blogs genéricos;
- conteúdo SEO;
- snippets sem contexto;
- posts antigos;
- respostas de fórum não verificadas.

Para toda conclusão importante registrar:

- fonte;
- URL;
- data de acesso quando apropriado;
- versão relevante;
- evidência;
- conclusão.

============================================================
8. ATUALIDADE DAS FONTES
============================================================

Estamos trabalhando com tecnologias que mudam.

Verificar documentação atual.

Não assumir que conhecimento antigo ainda vale.

Registrar explicitamente versões relevantes.

Especialmente:

OBS Studio 32.x

.NET 10

YouTube APIs

OAuth

AI Provider APIs

TTS APIs

installer/update technologies.

============================================================
9. PESQUISA — OBS 32.x
============================================================

Pesquisar oficialmente:

OBS Studio 32.x

Investigar:

- plugin development;
- plugin SDK;
- libobs;
- OBS Frontend API;
- headers;
- libraries;
- build requirements;
- supported compiler/toolchain;
- CMake;
- Windows x64;
- ABI/API compatibility;
- plugin lifecycle;
- frontend events;
- module loading;
- compatibility strategy.

Responder:

O que é necessário para desenvolver e distribuir uma
integração compatível com OBS Studio 32.x no Windows?

============================================================
10. PESQUISA — INSTALAÇÃO DE PLUGIN OBS
============================================================

Determinar oficialmente:

- estrutura de plugin;
- diretórios de binaries;
- diretórios de data;
- caminhos de instalação;
- instalação system-wide;
- instalação per-user quando suportada;
- diferenças entre instalação manual e installer.

IMPORTANTE:

Não modificar:

C:\Program Files\obs-studio

nem:

C:\Users\gfmau\AppData\Roaming\obs-studio

Nesta task.

Somente pesquisar/documentar.

============================================================
11. PESQUISA — OBS DOCK
============================================================

Pesquisar:

- APIs disponíveis;
- frontend Dock;
- integração UI;
- lifecycle;
- persistência;
- limitações;
- compatibilidade;
- alternativas suportadas.

Responder:

Qual é a abordagem oficialmente suportada para adicionar a
interface do OBS-AI-Live-Assistant ao OBS?

Não implementar.

============================================================
12. PESQUISA — OBS WEBSOCKET
============================================================

Pesquisar versão atual integrada ao OBS 32.x.

Avaliar:

- capabilities;
- authentication;
- events;
- requests;
- localhost usage;
- limitations;
- security;
- lifecycle;
- compatibility.

Determinar quais responsabilidades podem razoavelmente ser
executadas via OBS WebSocket.

Não decidir ainda a arquitetura final.

============================================================
13. NATIVE PLUGIN vs OBS WEBSOCKET
============================================================

Comparar tecnicamente:

Native Plugin

vs

OBS WebSocket

vs

Hybrid

Criar matriz:

Capability
Native Plugin
WebSocket
Hybrid
Security
Complexity
OBS coupling
Failure impact
Distribution
Maintenance
Recommendation

Responder:

Quais capacidades realmente exigem componente nativo?

Quais podem permanecer fora do processo OBS?

============================================================
14. PROCESS ISOLATION
============================================================

Pesquisar e avaliar:

Same Process

vs

Separate Process

para o Assistant Core.

Considerar requisito:

Falha do Assistant não pode derrubar OBS.

Avaliar:

reliability
security
lifecycle
performance
debugging
installer
communication overhead

Produzir recomendação técnica para futura ADR.

============================================================
15. C++ ↔ .NET IPC
============================================================

Pesquisar alternativas adequadas para Windows local:

Named Pipes
gRPC
local sockets
shared memory
COM quando pertinente
ou outras alternativas justificáveis.

Comparar:

latency
complexity
security
authentication
serialization
versioning
streaming/events
failure isolation
deployment
.NET support
C++ support

Não implementar.

Produzir matriz comparativa e recomendação para ADR.

============================================================
16. OBS AUDIO / TTS
============================================================

Pesquisar alternativas para levar áudio TTS ao OBS.

Avaliar:

- native OBS audio source;
- plugin-managed source;
- virtual audio device;
- application audio capture quando pertinente;
- outras APIs oficiais suportadas.

Considerar:

latency
routing
volume control
mute
monitoring
stream track
recording track
failure isolation
installation complexity
user experience

Não assumir previamente que uma solução é correta.

============================================================
17. SQLITE
============================================================

Pesquisar adequação do SQLite para V1 local.

Avaliar:

- lifecycle;
- migrations;
- locking/concurrency;
- backup;
- corruption/recovery;
- WAL;
- storage location;
- upgrade;
- uninstall;
- data retention;
- encryption limitations;
- interaction with secrets policy.

IMPORTANTE:

Secrets não devem ser armazenados em plaintext no SQLite.

Produzir recomendação para Architecture/ADR.

============================================================
18. SECRETS — WINDOWS
============================================================

Pesquisar oficialmente:

Windows Credential Manager

vs

DPAPI

e outras APIs nativas justificáveis.

Comparar:

security model
user scope
machine scope
developer complexity
backup/restore implications
installer implications
uninstall
credential rotation
.NET support
C++ interoperability

Produzir recomendação.

Não armazenar segredo real.

============================================================
19. YOUTUBE LIVE CHAT
============================================================

Pesquisar documentação oficial Google/YouTube.

Avaliar:

- API necessária;
- authentication;
- OAuth scopes;
- live chat discovery;
- message polling/streaming;
- quotas;
- rate limits;
- token refresh;
- errors;
- reconnect;
- moderation-related capabilities;
- terms/limitations relevantes.

Responder:

O que é necessário para suportar YouTube Live Chat no V1?

============================================================
20. AI PROVIDER ABSTRACTION
============================================================

Pesquisar apenas o necessário para definir contrato comum.

Avaliar diferenças relevantes entre:

OpenAI
Anthropic
Gemini
Ollama
OpenRouter
Custom/OpenAI-compatible quando aplicável.

Investigar:

- authentication;
- chat/message model;
- streaming;
- cancellation;
- timeout;
- token/usage information;
- error handling;
- rate limits;
- local vs cloud;
- provider-specific capabilities.

Não implementar providers.

Não exigir que todos façam parte do V1.

============================================================
21. LOCAL AI
============================================================

Avaliar viabilidade de:

Ollama

e outras opções locais somente se já constarem das fontes
internas.

Considerar:

hardware
RAM
VRAM
latency
model size
installation
support burden
streaming
Windows compatibility

Resultado possível:

V1
OPTIONAL
FUTURE
OUT_OF_SCOPE_V1

Não promover para V1 sem evidência.

============================================================
22. TTS PROVIDERS
============================================================

Pesquisar necessidades comuns para abstração TTS.

Avaliar:

Windows TTS
Azure Speech
ElevenLabs

quando aplicável.

Comparar:

authentication
local/cloud
latency
streaming
voice selection
output format
cost dependency
failure behavior
offline capability

Não implementar.

============================================================
23. INSTALLER
============================================================

Pesquisar alternativas adequadas ao produto Windows.

Avaliar tecnologias atuais e suportadas para:

- installer EXE;
- detection do OBS;
- plugin deployment;
- Assistant Core deployment;
- AppData;
- upgrade;
- repair;
- uninstall;
- rollback;
- prerequisites;
- signing;
- versioning.

Comparar opções com evidências.

Não criar installer nesta task.

============================================================
24. CODE SIGNING
============================================================

Pesquisar requisitos/opções para assinatura de:

installer
executables
DLLs

no Windows.

Avaliar:

necessidade
SmartScreen implications
certificate options
CI/CD implications
release process

Não comprar nem configurar certificado.

============================================================
25. COMPATIBILIDADE OBS
============================================================

Pesquisar estratégia de compatibilidade.

Avaliar:

OBS 32.x

e impacto de futuras versões.

Investigar:

API compatibility
ABI compatibility
plugin rebuild requirements
minimum supported version
version detection
compatibility testing

Produzir recomendação.

============================================================
26. LOGGING / OBSERVABILITY LOCAL
============================================================

Pesquisar apenas o necessário para Architecture.

Avaliar abordagem adequada para aplicação desktop/local:

structured logs
correlation IDs
log rotation
redaction
diagnostics
health status
local troubleshooting

Evitar trazer stack cloud desnecessária.

============================================================
27. UPDATE STRATEGY
============================================================

Pesquisar alternativas para atualização do produto.

Avaliar:

manual update
installer-based update
in-app update

Considerar:

security
signing
rollback
OBS compatibility
user experience

Não implementar.

============================================================
28. PRIVACIDADE / RETENÇÃO
============================================================

Pesquisar somente aspectos técnicos necessários.

Não fornecer aconselhamento jurídico.

Avaliar implicações técnicas de armazenar:

chat messages
usernames
viewer IDs
AI interactions
memory
logs

Separar:

technical recommendation

de:

policy/client decision.

============================================================
29. MATRIZ DE DECISÃO
============================================================

Para temas com alternativas relevantes, utilizar matriz.

Formato:

Opção
Vantagens
Desvantagens
Segurança
Complexidade
Manutenção
Compatibilidade
Impacto no OBS
Recomendação
Confiança

Não escolher por preferência da IA.

Basear recomendação nas evidências.

============================================================
30. NÍVEL DE CONFIANÇA
============================================================

Para conclusões importantes registrar:

HIGH
MEDIUM
LOW

HIGH:
fonte oficial e evidência direta.

MEDIUM:
evidência suficiente, mas existem detalhes ainda dependentes
de implementação/versão.

LOW:
evidência insuficiente ou fontes conflitantes.

LOW deve gerar:

follow-up research
ou
ADR uncertainty.

============================================================
31. DOCUMENTAÇÃO DE RESEARCH
============================================================

Organizar:

docs/research/

Estrutura sugerida:

docs/research/
├── README.md
├── RESEARCH_BACKLOG.md
├── RESEARCH_MATRIX.md
├── OBS_PLUGIN_RESEARCH.md
├── OBS_WEBSOCKET_RESEARCH.md
├── OBS_DOCK_RESEARCH.md
├── OBS_AUDIO_RESEARCH.md
├── PROCESS_ISOLATION_RESEARCH.md
├── IPC_RESEARCH.md
├── SQLITE_RESEARCH.md
├── WINDOWS_SECRETS_RESEARCH.md
├── YOUTUBE_LIVE_CHAT_RESEARCH.md
├── AI_PROVIDERS_RESEARCH.md
├── LOCAL_AI_RESEARCH.md
├── TTS_RESEARCH.md
├── INSTALLER_RESEARCH.md
├── CODE_SIGNING_RESEARCH.md
├── COMPATIBILITY_RESEARCH.md
├── OBSERVABILITY_RESEARCH.md
└── UPDATE_STRATEGY_RESEARCH.md

Não criar arquivos redundantes.

Consolidar quando fizer mais sentido.

============================================================
32. RESEARCH MATRIX
============================================================

Criar:

docs/research/RESEARCH_MATRIX.md

Para cada RES-*:

ID
Pergunta
Requirement
Security Requirement
ADR Candidate
Status
Conclusão
Confidence
Fontes
Architecture Impact

Status:

RESOLVED
PARTIALLY_RESOLVED
BLOCKED
CLIENT_DECISION
NOT_APPLICABLE

============================================================
33. ATUALIZAR RESEARCH BACKLOG
============================================================

Atualizar:

docs/research/RESEARCH_BACKLOG.md

Itens resolvidos:

RESOLVED

Itens parcialmente resolvidos:

PARTIALLY_RESOLVED

Itens dependentes do cliente:

CLIENT_DECISION

Não apagar histórico.

============================================================
34. ADR INPUTS
============================================================

Atualizar:

docs/requirements/ADR_CANDIDATES.md

ou documento equivalente canônico.

Para cada ADR Candidate registrar:

- Research Evidence;
- opções;
- recomendação;
- trade-offs;
- Security impact;
- Requirements impact;
- confidence.

IMPORTANTE:

NÃO criar decisão ADR final nesta task.

Research recomenda.

Architecture/ADR decide.

============================================================
35. OPEN QUESTIONS
============================================================

Atualizar Open Questions somente quando Research produzir
evidência suficiente.

Não fechar:

CLIENT DECISION

sem decisão explícita do cliente.

Pode fechar:

RESEARCH

quando evidência suficiente existir.

Registrar justificativa.

============================================================
36. RASTREABILIDADE
============================================================

Atualizar:

Requirements Traceability
Security Traceability
Knowledge Map

quando necessário.

Fluxo esperado:

Knowledge
→ Requirement
→ Security Requirement
→ Research
→ ADR Candidate
→ Architecture futura.

============================================================
37. RESEARCH QUALITY GATE
============================================================

Criar ou atualizar gate apropriado.

Validar:

[ ] inventário de Research consolidado
[ ] fontes oficiais priorizadas
[ ] versões registradas
[ ] OBS plugin pesquisado
[ ] plugin paths pesquisados
[ ] Dock pesquisado
[ ] OBS WebSocket pesquisado
[ ] Native vs WebSocket comparado
[ ] process isolation pesquisado
[ ] IPC pesquisado
[ ] OBS audio pesquisado
[ ] SQLite pesquisado
[ ] Windows secrets pesquisado
[ ] YouTube Live Chat pesquisado
[ ] AI provider abstraction pesquisada
[ ] local AI avaliada
[ ] TTS pesquisado
[ ] installer pesquisado
[ ] code signing pesquisado
[ ] OBS compatibility pesquisada
[ ] observability local pesquisada
[ ] update strategy pesquisada
[ ] security research tratada
[ ] ADR inputs preparados
[ ] rastreabilidade atualizada
[ ] fontes registradas
[ ] confidence registrada
[ ] blockers explícitos
[ ] Architecture pode começar sem inventar evidência

Resultado:

PASSED
ou
BLOCKED

Não marcar PASSED artificialmente.

============================================================
38. ARCHITECTURE READINESS
============================================================

Avaliar separadamente:

Architecture Readiness

e:

Architecture Security Readiness.

Resultado:

READY
ou
NOT READY

Open Questions NON-BLOCKING não impedem READY.

CLIENT DECISION somente bloqueia se a decisão for necessária
antes de Architecture.

============================================================
39. RELATÓRIO
============================================================

Criar:

docs/reports/TECHNICAL_RESEARCH_REPORT.md

Em pt-BR.

Incluir:

- fontes internas;
- fontes externas;
- quantidade de Research Items;
- resolved;
- partially resolved;
- blocked;
- client decisions;
- principais conclusões;
- principais recomendações;
- confidence;
- ADR inputs;
- riscos;
- Security impact;
- Architecture impact;
- Research Quality Gate;
- Architecture Readiness;
- blockers;
- recomendação da próxima fase.

============================================================
40. NÃO INVENTAR PESQUISA
============================================================

Regra crítica:

Não afirmar:

"documentação oficial diz"

sem ter consultado a fonte.

Não inventar:

URLs
versões
APIs
paths
capabilities
compatibility
quotas
limits

Se acesso externo não estiver disponível:

marcar:

BLOCKED

ou:

PARTIALLY_RESOLVED

conforme aplicável.

============================================================
41. CODE REVIEW
============================================================

Executar Code Review documental.

Validar:

- qualidade das fontes;
- links;
- versões;
- evidências;
- conclusões;
- matrizes;
- rastreabilidade;
- WHAT/RESEARCH/ADR separation;
- pt-BR;
- UTF-8;
- ausência de implementação;
- ausência de decisões inventadas.

Classificar:

CRITICAL
HIGH
MEDIUM
LOW
INFO

Corrigir Critical/High.

Corrigir Medium quando seguro e no escopo.

============================================================
42. SECURITY REVIEW
============================================================

Executar Security Review das recomendações.

Garantir que nenhuma recomendação:

- enfraqueça BYOK;
- exponha secrets;
- dê autoridade indevida ao chat;
- dê autoridade indevida à IA;
- comprometa OBS;
- introduza trust boundary ignorada;
- contradiga Security Requirements.

============================================================
43. SECRET SCAN
============================================================

Executar Secret Scan.

Não armazenar:

API keys
tokens
OAuth secrets
credentials
private keys
refresh tokens
passwords

Nenhuma credencial real é necessária para esta pesquisa.

Resultado esperado:

Secrets:
NONE

============================================================
44. PROMPT TRACEABILITY
============================================================

Inspecionar:

docs/prompts/history/

O último conhecido após a task anterior é:

prompt13.md

NÃO assumir automaticamente que o próximo é prompt14.

Calcular pelo estado real.

Arquivar ESTE PROMPT integralmente no próximo identificador
válido.

Nunca sobrescrever histórico.

============================================================
45. VALIDAÇÃO PRÉ-COMMIT
============================================================

Executar:

git status
git diff
git diff --check

Validar:

Technical Research = concluído

Research Quality Gate = calculado

Architecture = não iniciada

ADR final = não criado

Implementation = não iniciada

Product Code = não criado

OBS = não modificado

Secrets = NONE

============================================================
46. COMMIT
============================================================

Conventional Commit sugerido:

docs: complete technical research

Incluir:

research
reports
traceability
prompt history
correções

no mesmo commit da task.

============================================================
47. PUSH
============================================================

Push:

origin/feature/task-technical-research

Validar:

local HEAD = remote HEAD

============================================================
48. PULL REQUEST
============================================================

Criar PR:

feature/task-technical-research
→
develop

Descrição humana:

PORTUGUÊS DO BRASIL.

Incluir:

- objetivo;
- Research Items;
- fontes oficiais;
- principais conclusões;
- recomendações;
- ADR inputs;
- Security impact;
- Research Quality Gate;
- Architecture Readiness;
- itens ainda abertos.

============================================================
49. PR VALIDATION
============================================================

Validar:

Critical Findings = 0
High Findings = 0
Secrets = NONE
Research Validation = PASSED
Prompt Traceability = PASSED
GitFlow = PASSED

============================================================
50. MERGE AUTOMÁTICO
============================================================

Se a TASK estiver documentalmente aprovada:

feature/task-technical-research
→
develop

MERGE AUTOMÁTICO.

Não pedir autorização intermediária.

Mesmo que um Research Item permaneça BLOCKED, a documentação
pode ser mergeada se o diagnóstico estiver correto.

Porém:

Architecture Readiness deve refletir honestamente o impacto.

============================================================
51. PÓS-MERGE
============================================================

Após merge:

git switch develop
git pull --ff-only origin develop

Confirmar:

develop local = origin/develop

Excluir feature local e remota quando seguro.

Working Tree:

CLEAN

============================================================
52. NÃO PROMOVER
============================================================

Esta é uma Task individual.

Termina em:

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
53. NÃO EXECUTAR
============================================================

NÃO executar Architecture final.

NÃO criar ADR final.

NÃO implementar código.

NÃO criar src/.

NÃO criar solution .NET.

NÃO criar projeto C++.

NÃO criar database.

NÃO criar migrations.

NÃO criar installer.

NÃO instalar plugin no OBS.

NÃO modificar OBS.

NÃO modificar:

C:\Users\gfmau\AppData\Roaming\obs-studio

NÃO modificar:

C:\Program Files\obs-studio

NÃO utilizar credenciais reais.

============================================================
54. RESULTADO FINAL
============================================================

Retornar em português:

OBS-AI-Live-Assistant

TASK:
technical-research

TASK STATUS:
CONCLUÍDA / BLOQUEADA

Execução integral:
CONCLUÍDA / BLOQUEADA

Research Items:
<quantidade>

Resolved:
<quantidade>

Partially Resolved:
<quantidade>

Blocked:
<quantidade>

Client Decisions:
<quantidade>

Official Sources:
<quantidade>

Research Matrix:
CONCLUÍDA / BLOQUEADA

OBS Plugin Research:
CONCLUÍDO / BLOQUEADO

OBS WebSocket Research:
CONCLUÍDO / BLOQUEADO

OBS Dock Research:
CONCLUÍDO / BLOQUEADO

OBS Audio Research:
CONCLUÍDO / BLOQUEADO

Process Isolation Research:
CONCLUÍDO / BLOQUEADO

IPC Research:
CONCLUÍDO / BLOQUEADO

SQLite Research:
CONCLUÍDO / BLOQUEADO

Windows Secrets Research:
CONCLUÍDO / BLOQUEADO

YouTube Live Chat Research:
CONCLUÍDO / BLOQUEADO

AI Providers Research:
CONCLUÍDO / BLOQUEADO

Local AI Research:
CONCLUÍDO / BLOQUEADO

TTS Research:
CONCLUÍDO / BLOQUEADO

Installer Research:
CONCLUÍDO / BLOQUEADO

Code Signing Research:
CONCLUÍDO / BLOQUEADO

Compatibility Research:
CONCLUÍDO / BLOQUEADO

Observability Research:
CONCLUÍDO / BLOQUEADO

Update Strategy Research:
CONCLUÍDO / BLOQUEADO

ADR Inputs:
PREPARADOS / BLOQUEADOS

Research Quality Gate:
PASSED / BLOCKED

Architecture Readiness:
READY / NOT READY

Architecture Security Readiness:
READY / NOT READY

Relatório:
docs/reports/TECHNICAL_RESEARCH_REPORT.md

Prompt arquivado:
SIM / NÃO

Prompt:
docs/prompts/history/<arquivo>

Code Review:
APROVADO / BLOQUEADO

Security Review:
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
feature/task-technical-research

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

Architecture:
NÃO INICIADA

ADR Final:
NÃO CRIADO

Product Source Code:
NÃO CRIADO

Implementation:
NÃO INICIADA

OBS:
NÃO MODIFICADO

PRÓXIMO:

Se:

Research Quality Gate = PASSED
Architecture Readiness = READY
Architecture Security Readiness = READY

então:

ARCHITECTURE + ADRs

Caso contrário:

RESOLVER RESEARCH BLOCKERS

============================================================
STOP
============================================================

STOP SOMENTE DEPOIS DE:

Research completo
→ documentação
→ matrizes
→ rastreabilidade
→ Research Quality Gate
→ Architecture Readiness
→ Code Review
→ Security Review
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

NÃO iniciar Architecture automaticamente.
