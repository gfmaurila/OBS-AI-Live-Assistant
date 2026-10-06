AUTORIZAÇÃO — CRIAR E PADRONIZAR DOCUMENTAÇÃO DO PROJETO

Projeto:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

Projeto de referência documental:

D:\Empresa\GFMaurila\projetos\gfm-template-cms-azure

GitHub de referência:

https://github.com/gfmaurila/gfm-template-cms-azure

==================================================
OBJETIVO
==================================================

Criar a estrutura documental oficial do projeto
OBS-AI-Live-Assistant utilizando como referência SOMENTE
o padrão de organização, governança e rastreabilidade do
projeto:

gfm-template-cms-azure

NÃO copiar a arquitetura funcional do CMS Azure.

NÃO copiar componentes de negócio que não pertencem ao
OBS-AI-Live-Assistant.

Adaptar o padrão documental para o contexto atual deste projeto.

==================================================
CONTEXTO ATUAL DO OBS-AI-LIVE-ASSISTANT
==================================================

Produto:

OBS-AI-Live-Assistant

Objetivo geral:

Assistente de IA para transmissões ao vivo integrado ao
OBS Studio.

Direção técnica atual:

- .NET 10 / C#
- C/C++ somente quando necessário para integração nativa OBS
- Windows 10/11 x64
- OBS Studio 32.x x64
- SQLite para persistência local V1
- Modular Monolith
- Provider Architecture / Ports & Adapters
- BYOK / multi-provider AI
- multi-provider TTS
- YouTube Live Chat como prioridade V1
- Twitch futuro
- integração OBS via combinação a decidir entre:
  - native plugin
  - OBS WebSocket
  - IPC
- segurança forte para secrets
- AI/TTS/chat failure != OBS failure
- instalação local
- runtime data previsto em:
  %APPDATA%\obs-studio\obs-ai-live-assistant

IMPORTANTE:

Essas informações são contexto atual e NÃO devem ser tratadas
como arquitetura final aprovada quando ainda dependerem de
Requirements/ADR/Research.

==================================================
REFERÊNCIA DOCUMENTAL A ADAPTAR
==================================================

Analisar no projeto:

D:\Empresa\GFMaurila\projetos\gfm-template-cms-azure

especialmente:

README.md
docs/README.md
docs/architecture/
docs/governance/
docs/knowledge/
docs/reports/
tasks/

Usar como referência de:

- organização;
- separação de responsabilidades documentais;
- navegação;
- governança;
- Quality Gates;
- Knowledge Gate;
- Execution Plan;
- reports;
- tasks;
- documentação canônica.

NÃO copiar:

- Azure;
- CMS;
- multi-tenant;
- React;
- APIs do template;
- bancos/mensageria;
- componentes de cloud;
- BYOAI por tenant;
- estruturas específicas do GFM.Template.CMS;

a menos que exista equivalência real e explicitamente válida
para o OBS-AI-Live-Assistant.

==================================================
1. GITFLOW
==================================================

A task deve partir de:

develop

Garantir primeiro:

git fetch origin
git switch develop
git pull --ff-only origin develop

Working Tree deve estar CLEAN.

Criar:

feature/task-project-documentation-baseline

Toda alteração desta task ocorre somente nessa branch.

==================================================
2. PROMPT TRACEABILITY
==================================================

Aplicar a regra permanente já definida.

Arquivar este prompt integralmente em:

docs/prompts/history/promptN.md

Identificar automaticamente o próximo número disponível.

Nunca sobrescrever prompt existente.

O prompt desta própria task deve fazer parte do mesmo
commit das alterações documentais.

==================================================
3. ESTRUTURA DOCUMENTAL
==================================================

Criar/adaptar a seguinte estrutura quando ainda não existir:

docs/
├── README.md
│
├── architecture/
│   ├── README.md
│   ├── PROJECT_STRUCTURE.md
│   ├── decisions/
│   └── diagrams/
│
├── project/
│   ├── PROJECT_OVERVIEW.md
│   ├── PROJECT_SCOPE.md
│   └── PROJECT_SKILLS.md
│
├── governance/
│   ├── README.md
│   ├── EXECUTION_PLAN.md
│   ├── GITFLOW.md
│   ├── QUALITY_GATES.md
│   └── KNOWLEDGE_QUALITY_GATE.md
│
├── knowledge/
│   ├── PROJECT_KNOWLEDGE_MAP.md
│   ├── KNOWLEDGE_DECISIONS.md
│   └── KNOWLEDGE_CONFLICTS.md
│
├── research/
│   ├── README.md
│   └── RESEARCH_BACKLOG.md
│
├── requirements/
│   └── README.md
│
├── reports/
│   ├── README.md
│   └── BOOTSTRAP_REPORT.md
│
├── security/
│   └── README.md
│
├── testing/
│   └── README.md
│
└── prompts/
    ├── README.md
    └── history/

Não criar documentos duplicados se já existir equivalente.

Preferir consolidar/ajustar a documentação existente.

==================================================
4. DOCS/README.MD
==================================================

Criar um índice central semelhante ao padrão do projeto Azure,
mas adaptado ao OBS-AI-Live-Assistant.

Deve apontar para:

Architecture
Project
Governance
Knowledge
Research
Requirements
Security
Testing
Reports
Prompt History
Tasks

O arquivo deve servir como ponto de entrada da documentação.

==================================================
5. PROJECT OVERVIEW
==================================================

Criar:

docs/project/PROJECT_OVERVIEW.md

Documentar SOMENTE o contexto atual conhecido.

Incluir:

- nome do produto;
- propósito;
- problema que pretende resolver;
- integração com OBS;
- interação via chat;
- IA;
- TTS;
- BYOK;
- operação local;
- foco Windows;
- prioridade YouTube V1;
- dependência de decisões futuras sobre integração nativa,
  WebSocket e IPC.

Marcar explicitamente informações não aprovadas como:

CURRENT DIRECTION
ASSUMPTION
REQUIRES_RESEARCH
REQUIRES_ADR
REQUIRES_CLIENT_DECISION

Não transformar hipótese em decisão.

==================================================
6. PROJECT SCOPE
==================================================

Criar:

docs/project/PROJECT_SCOPE.md

Separar claramente:

V1 CURRENT DIRECTION
FUTURE
OUT OF SCOPE V1

Usar somente o contexto já conhecido.

Não inventar requisitos novos.

==================================================
7. PROJECT STRUCTURE
==================================================

Criar/adaptar:

docs/architecture/PROJECT_STRUCTURE.md

IMPORTANTE:

Neste momento ele deve representar apenas a
ESTRUTURA DOCUMENTAL E PLANEJADA.

Não criar estrutura física de código ainda.

Não criar src/.

Não criar solution.

Não criar C++.

Não criar banco.

Não criar Dock.

Documentar conceitualmente:

OBS-AI-Live-Assistant/
├── docs/
├── .ai/
├── .claude/
├── .codex/
├── .github/
├── agent_docs/
├── tasks/
└── future product structure
    ├── assistant-core/
    ├── obs-integration/
    ├── tests/
    ├── installer/
    └── tooling/

Marcar as pastas de produto como:

PLANNED
NOT CREATED
SUBJECT TO ARCHITECTURE APPROVAL

==================================================
8. EXECUTION PLAN
==================================================

Criar/adaptar:

docs/governance/EXECUTION_PLAN.md

Fluxo atual:

KIT IA DEV
↓
DOCUMENTATION BASELINE
↓
ADVANCED SKILLS (quando pacote estiver disponível)
↓
KNOWLEDGE QUALITY GATE
↓
REQUIREMENTS
↓
RESEARCH
↓
ARCHITECTURE
↓
ADR
↓
BACKLOG
↓
TASKS
↓
IMPLEMENTATION

Registrar também o fluxo automático por task:

TASK READY
↓
feature/task-*
↓
execution
↓
validation
↓
code review
↓
required fixes
↓
archive prompt
↓
security check
↓
commit
↓
push
↓
PR
↓
PR validation
↓
merge to develop
↓
post-merge validation
↓
feature cleanup

==================================================
9. GITFLOW
==================================================

Criar/adaptar:

docs/governance/GITFLOW.md

Branches permanentes:

main
develop
hml

Branches de trabalho:

feature/task-*

Fluxo:

feature/task-*
↓
PR
↓
develop
↓
PR / promotion gate
↓
hml
↓
release/x.y.z
↓
PR
↓
main

Definir explicitamente:

- feature → develop pode fazer auto-merge se todos os gates
  estiverem verdes;
- promoção para hml NÃO é automática por task;
- main continua protegida;
- nenhuma task trabalha diretamente em main/develop/hml.

==================================================
10. QUALITY GATES
==================================================

Criar/adaptar:

docs/governance/QUALITY_GATES.md

Nesta fase documental, considerar:

- Documentation
- Scope Compliance
- Knowledge Compliance
- Security Check
- GitFlow
- Prompt Traceability
- Code Review
- Secret Scan

Preparar também gates futuros:

- Build
- Unit Tests
- Integration Tests
- Architecture Validation
- Installer Validation
- OBS Compatibility
- Regression Tests

Os gates futuros devem estar marcados:

NOT APPLICABLE UNTIL IMPLEMENTATION

==================================================
11. KNOWLEDGE QUALITY GATE
==================================================

Criar/adaptar:

docs/governance/KNOWLEDGE_QUALITY_GATE.md

Não marcar como PASSED ainda.

Estado atual deve refletir a realidade:

Kit IA Dev:
PARTIAL / BASE INSTALLED

Advanced Skills:
BLOCKED

Knowledge Dictionary:
AVAILABLE / TO BE VALIDATED

Project Documentation Baseline:
IN PROGRESS / COMPLETED após esta task

Requirements:
NOT STARTED

Architecture:
NOT STARTED

Research:
NOT STARTED

Gate:
NOT PASSED

Regra:

Se Knowledge Quality Gate falhar:
STOP antes de Requirements/Architecture implementation.

==================================================
12. KNOWLEDGE DOCUMENTS
==================================================

Criar:

docs/knowledge/PROJECT_KNOWLEDGE_MAP.md
docs/knowledge/KNOWLEDGE_DECISIONS.md
docs/knowledge/KNOWLEDGE_CONFLICTS.md

Neste momento criar apenas baseline.

Não preencher decisões arquiteturais ainda.

Exemplos de itens do mapa:

- Kit IA Dev;
- current product concept;
- OBS integration;
- BYOK;
- TTS;
- YouTube;
- SQLite;
- Windows;
- installer;
- security;
- prompt history;
- GitFlow;
- Advanced Skills blocker.

Para cada item usar estados como:

ADOPT
ADAPT
REFERENCE
REQUIRES_RESEARCH
REQUIRES_ADR
BLOCKED
FUTURE

==================================================
13. RESEARCH BACKLOG
==================================================

Criar:

docs/research/RESEARCH_BACKLOG.md

Registrar tópicos já conhecidos que exigem pesquisa técnica:

- OBS Studio 32.x plugin SDK/build strategy;
- official native plugin installation paths;
- Dock integration;
- OBS audio integration;
- OBS WebSocket capabilities;
- C++ ↔ .NET IPC strategy;
- process isolation;
- SQLite local lifecycle;
- Credential Manager / DPAPI;
- YouTube Live Chat integration;
- AI provider abstraction;
- local AI provider viability;
- TTS provider abstraction;
- installer technology;
- upgrade/repair/uninstall;
- compatibility strategy across OBS versions.

Não executar a pesquisa nesta task.

Somente registrar backlog.

==================================================
14. BOOTSTRAP REPORT
==================================================

Criar/adaptar:

docs/reports/BOOTSTRAP_REPORT.md

Registrar o estado real:

- Kit IA Dev base installed;
- AGENTS.md;
- CLAUDE.md;
- Agents;
- 10 base Skills;
- Advanced Skills BLOCKED;
- Governance installed;
- Git setup completed;
- main/develop/hml created;
- Prompt Traceability enabled;
- PR #1 completed;
- documentation baseline created;
- product source code NOT CREATED;
- implementation NOT STARTED;
- OBS environment NOT MODIFIED.

Não afirmar que Requirements ou Architecture existem.

==================================================
15. TASKS
==================================================

Se tasks/ ainda não tiver uma estrutura organizada,
preparar somente a estrutura documental:

tasks/
├── backlog/
├── ready/
├── in-progress/
├── review/
├── blocked/
├── done/
└── DEPENDENCY_GRAPH.md

Não criar backlog funcional detalhado ainda.

Não gerar tasks de implementação.

Pode registrar apenas tasks de preparação/documentação se
estritamente necessário.

==================================================
16. ROOT README
==================================================

Atualizar o README.md da raiz para servir como landing page.

Deve conter:

- nome;
- resumo curto do projeto;
- status atual;
- stack direction;
- documentação;
- GitFlow;
- current blockers;
- implementation status;
- link relativo para docs/README.md.

Não transformar o README em especificação completa.

==================================================
17. NÃO COPIAR O CMS AZURE
==================================================

Proibido copiar cegamente do projeto de referência:

- Azure Target;
- CMS;
- multi-tenant;
- React;
- frontend admin/site;
- Service Bus;
- Event Hubs;
- MySQL;
- MongoDB;
- Redis;
- Kafka;
- RabbitMQ;
- AKS;
- Bicep;
- OpenTofu;
- n8n;
- RAG;
- workers;
- APIs CMS.

Só usar o projeto como referência de padrão documental.

==================================================
18. REVIEW
==================================================

Executar Code Review documental completo.

Validar:

- estrutura;
- consistência;
- links;
- duplicações;
- escopo;
- terminology;
- canonical sources;
- contradictions;
- secrets;
- GitFlow;
- prompt traceability.

Critical/High findings devem ser corrigidos.

==================================================
19. COMMIT + PUSH + PR + MERGE
==================================================

Toda alteração deve ser:

- validada;
- prompt arquivado;
- secret checked;
- commitada;
- enviada ao remoto.

Commit sugerido:

docs: establish project documentation baseline

Push:

feature/task-project-documentation-baseline

Criar PR para:

develop

Se todos os gates estiverem verdes:

MERGE AUTOMÁTICO PARA DEVELOP

Depois:

- validar develop;
- remover feature local;
- remover feature remota;
- Working Tree CLEAN.

==================================================
20. NÃO EXECUTAR
==================================================

Não executar Requirements.

Não executar Architecture final.

Não executar Research.

Não criar código.

Não criar src/.

Não criar solution .NET.

Não criar projeto C++.

Não criar SQLite.

Não modificar OBS.

Não modificar:

C:\Users\gfmau\AppData\Roaming\obs-studio

Não tentar recriar as Advanced Skills.

==================================================
21. RESULTADO FINAL
==================================================

Retorne:

OBS-AI-Live-Assistant

TASK:
project-documentation-baseline

TASK STATUS:
COMPLETED / BLOCKED

Documentation Baseline:
OK / BLOCKED

docs/README.md:
OK / BLOCKED

PROJECT_OVERVIEW:
OK / BLOCKED

PROJECT_SCOPE:
OK / BLOCKED

PROJECT_STRUCTURE:
OK / BLOCKED

EXECUTION_PLAN:
OK / BLOCKED

GITFLOW:
OK / BLOCKED

QUALITY_GATES:
OK / BLOCKED

KNOWLEDGE_QUALITY_GATE:
OK / BLOCKED

KNOWLEDGE MAP:
OK / BLOCKED

RESEARCH BACKLOG:
OK / BLOCKED

BOOTSTRAP REPORT:
OK / BLOCKED

Root README:
OK / BLOCKED

Prompt Archived:
YES / NO

Prompt File:
docs/prompts/history/promptN.md

Code Review:
APPROVED / BLOCKED

Secrets:
NONE / DETECTED

Branch:
feature/task-project-documentation-baseline

Commit:
<hash>

Push:
OK / BLOCKED

Pull Request:
CREATED / BLOCKED

PR Number:
<número>

Merge to Develop:
COMPLETED / BLOCKED

Develop Synchronized:
YES / NO

Feature Local:
DELETED / RETAINED

Feature Remote:
DELETED / RETAINED

Working Tree:
CLEAN / DIRTY

Advanced Skills:
BLOCKED

Knowledge Quality Gate:
NOT PASSED

Requirements:
NOT STARTED

Architecture:
NOT STARTED

Product Source Code:
NOT CREATED

Implementation:
NOT STARTED

OBS Environment:
NOT MODIFIED

NEXT:
RESOLVE ADVANCED SKILLS
+
KNOWLEDGE QUALITY GATE
+
REQUIREMENTS

==================================================
STOP
==================================================

Após merge em develop e validação final:

STOP.

Não executar Requirements.
Não executar Architecture.
Não implementar código.
