AUTORIZAÇÃO — EXECUTAR KNOWLEDGE QUALITY GATE COMPLETO

PROJETO:
OBS-AI-Live-Assistant

REPOSITÓRIO LOCAL:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

KIT IA DEV:
D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev

KNOWLEDGE DICTIONARY:
D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev\dicionario

Se o diretório acima não existir, verificar também:

D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\dicionario

============================================================
TASK
============================================================

knowledge-quality-gate

Branch esperada:

feature/task-knowledge-quality-gate

============================================================
REGRA PRINCIPAL — EXECUÇÃO INTEGRAL
============================================================

ESTA TASK ESTÁ AUTORIZADA PARA EXECUÇÃO COMPLETA.

Depois de iniciada, executar TODO o setup e TODAS as etapas
pertencentes ao escopo desta task até sua conclusão.

NÃO parar para solicitar confirmações intermediárias.

NÃO perguntar se pode:

- analisar;
- editar documentação;
- corrigir documentação;
- executar validações;
- executar Code Review;
- executar Security/Secret Scan;
- arquivar prompt;
- fazer commit;
- fazer push;
- criar PR;
- validar PR;
- mergear feature/task-* para develop;
- sincronizar develop;
- remover a feature concluída.

Tudo isso já está autorizado dentro desta task.

Parar antecipadamente SOMENTE diante de:

1. bloqueio técnico real;
2. risco de perda/destruição de dados;
3. necessidade de segredo/credencial não disponível;
4. conflito Git que não possa ser resolvido com segurança;
5. decisão de produto/cliente que não possa ser inferida;
6. Critical/High Finding que não possa ser corrigido;
7. alteração necessária fora do escopo autorizado.

Se nenhum desses casos ocorrer:

EXECUTAR ATÉ O FIM.

O STOP significa:

FIM DA TASK COMPLETA

e não uma solicitação de aprovação intermediária.

============================================================
IDIOMA OFICIAL
============================================================

Documentação humana:

PORTUGUÊS DO BRASIL — pt-BR

Incluindo:

- README;
- documentação;
- descrição de Tasks;
- relatórios;
- Governance;
- Knowledge;
- Research;
- Requirements futuros;
- Architecture futura;
- descrição humana de Pull Requests.

Preservar termos técnicos e estados formais quando apropriado:

CURRENT DIRECTION
REQUIRES_RESEARCH
REQUIRES_ADR
REQUIRES_CLIENT_DECISION
NOT STARTED
NOT APPLICABLE
PASSED
BLOCKED

Também preservar:

C#
.NET
C++
OBS
OBS WebSocket
IPC
SQLite
TTS
BYOK
API
GitFlow
Quality Gate
Code Review

Não traduzir nomes técnicos artificialmente.

============================================================
ESTADO CONHECIDO DO PROJETO
============================================================

Documentation Baseline:
COMPLETED

Documentação humana:
pt-BR

GitFlow:
CONFIGURADO

Fluxo de Task:

feature/task-*
→ develop

Fluxo de homologação:

develop
→ hml

Fluxo de release:

hml
→ release/1.0.0XXXX
→ main

Advanced Skills:
BLOCKED

Knowledge Quality Gate:
NOT EXECUTED / NOT PASSED

Requirements:
NOT STARTED

Architecture:
NOT STARTED

Product Source Code:
NOT CREATED

Implementation:
NOT STARTED

OBS:
NOT MODIFIED

============================================================
FASE 1 — RECUPERAR ESTADO REAL
============================================================

Antes de alterar qualquer arquivo:

git fetch origin
git branch --show-current
git status
git status --short
git log --oneline --decorate -10

Validar:

Working Tree = CLEAN

Confirmar:

develop local
origin/develop
main
hml

Não alterar main ou hml.

Se estiver em branch residual já concluída, validar antes de
qualquer mudança.

Depois:

git switch develop
git pull --ff-only origin develop

Validar:

develop local = origin/develop

Criar:

feature/task-knowledge-quality-gate

Toda alteração desta task deve ocorrer nessa branch.

============================================================
FASE 2 — CARREGAR GOVERNANÇA
============================================================

Antes de executar o Knowledge Gate, ler integralmente os
documentos canônicos aplicáveis.

Incluir, quando existentes:

README.md
docs/README.md

docs/project/
docs/governance/
docs/knowledge/
docs/research/
docs/security/
docs/testing/
docs/reports/

AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md

.ai/
agent_docs/

tasks/

Ler as regras atuais antes de modificar documentos.

Não substituir a governança recém-aprovada por regras antigas.

============================================================
FASE 3 — KNOWLEDGE DICTIONARY
============================================================

Localizar o Knowledge Dictionary oficial.

Prioridade:

D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev\dicionario

Fallback:

D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\dicionario

Listar os arquivos encontrados.

Ler INTEGRALMENTE o dicionário.

Não analisar apenas títulos.

Não selecionar somente documentos convenientes.

Não copiar o conteúdo cegamente.

Para cada conhecimento relevante determinar sua relação com:

OBS-AI-Live-Assistant

============================================================
FASE 4 — CLASSIFICAÇÃO DO CONHECIMENTO
============================================================

Classificar conhecimento relevante usando:

ADOPT
ADAPT
REFERENCE
FUTURE
OUT_OF_SCOPE
NOT_APPLICABLE
REQUIRES_RESEARCH
REQUIRES_ADR
REQUIRES_CLIENT_DECISION
CONFLICT
BLOCKED

Não transformar conhecimento genérico do Kit IA Dev em
requisito do produto automaticamente.

============================================================
CONTEXTO ATUAL DO PRODUTO
============================================================

Considerar como direção atual conhecida, não necessariamente
como arquitetura final:

Produto:

OBS-AI-Live-Assistant

Objetivo:

Assistente de IA para lives integrado ao OBS Studio.

Direções atuais:

- Windows 10/11 x64;
- OBS Studio 32.x x64;
- .NET 10 / C#;
- C/C++ somente quando integração nativa OBS exigir;
- integração com OBS;
- arquitetura local;
- Modular Monolith como direção atual;
- Provider Architecture;
- BYOK;
- AI Providers;
- TTS Providers;
- Chat Providers;
- YouTube Live Chat como prioridade V1;
- Twitch futuro;
- SQLite local como candidato/direção V1;
- isolamento de processo;
- falha da IA não pode derrubar OBS;
- falha de TTS não pode derrubar OBS;
- falha do chat não pode derrubar OBS;
- credenciais seguras;
- moderação;
- rate limiting;
- filas internas;
- LiveContext;
- Assistant Profiles;
- memória;
- logs;
- installer;
- upgrade;
- repair;
- uninstall.

Não transformar automaticamente:

direção
→ decisão arquitetural final.

Quando necessário usar:

REQUIRES_RESEARCH

ou:

REQUIRES_ADR

============================================================
FASE 5 — ADVANCED SKILLS
============================================================

O estado conhecido é:

Advanced Skills:
BLOCKED

O pacote oficial não foi encontrado anteriormente.

NÃO:

- inventar Skills;
- recriar Skills;
- gerar conteúdo substituto;
- inventar nomes;
- inventar patches;
- alterar as 10 Skills base para simular o pacote.

Revalidar apenas o necessário para confirmar o estado.

Depois determinar:

A ausência das Advanced Skills bloqueia Requirements?

Classificar como:

BLOCKING

ou:

NON-BLOCKING FOR REQUIREMENTS

Justificar documentalmente.

IMPORTANTE:

A ausência de Advanced Skills NÃO deve causar automaticamente
falha do Knowledge Quality Gate.

O Gate avalia se existe conhecimento suficiente e controlado
para iniciar Requirements.

============================================================
FASE 6 — PROJECT KNOWLEDGE MAP
============================================================

Atualizar:

docs/knowledge/PROJECT_KNOWLEDGE_MAP.md

Mapear:

FONTE
→ CONCEITO
→ RELEVÂNCIA PARA O PROJETO
→ CLASSIFICAÇÃO
→ CANDIDATO A REQUISITO
→ RESEARCH
→ ADR
→ FUTURE / OUT OF SCOPE

Cobrir pelo menos:

OBS Studio
OBS Native Plugin
OBS WebSocket
OBS Dock
OBS Audio
C/C++
.NET 10
C#
IPC
Process Isolation
SQLite
Database Lifecycle
BYOK
Credential Storage
Windows Credential Manager
DPAPI
AI Providers
OpenAI
Anthropic
Gemini
Ollama
OpenRouter
Custom Providers
TTS
Windows TTS
Azure Speech
ElevenLabs
Chat Providers
YouTube Live Chat
Twitch
Moderation
Prompt Injection
Rate Limiting
Queues
Timeout
Cancellation
LiveContext
Assistant Profiles
Short-Term Memory
Persistent Memory
Retention
Logging
Observability
Installer
Upgrade
Repair
Uninstall
Testing
OBS Compatibility
Security
GitFlow
Quality Gates
Prompt Traceability

Não assumir que todos serão V1.

============================================================
FASE 7 — KNOWLEDGE DECISIONS
============================================================

Atualizar:

docs/knowledge/KNOWLEDGE_DECISIONS.md

Para cada item relevante registrar:

- assunto;
- fonte;
- contexto;
- classificação;
- estado;
- justificativa;
- impacto;
- próxima ação.

Estados permitidos:

ADOPT
ADAPT
REFERENCE
FUTURE
OUT_OF_SCOPE
REQUIRES_RESEARCH
REQUIRES_ADR
REQUIRES_CLIENT_DECISION
BLOCKED

Evitar decisões prematuras.

Exemplo:

SQLite

não deve automaticamente virar:

FINAL ARCHITECTURE DECISION

Pode ser:

CURRENT DIRECTION
+
REQUIREMENTS CANDIDATE
+
REQUIRES_ADR

se isso representar melhor o estado real.

============================================================
FASE 8 — KNOWLEDGE CONFLICTS
============================================================

Atualizar:

docs/knowledge/KNOWLEDGE_CONFLICTS.md

Pesquisar conflitos e ambiguidades.

Avaliar pelo menos:

Native Plugin
vs
OBS WebSocket

Same Process
vs
Process Isolation

Native Audio
vs
OBS Source
vs
Virtual Audio Device

Credential Manager
vs
DPAPI
vs
outras estratégias

SQLite lifecycle

Cloud AI
vs
Local AI

Persistent Memory
vs
Privacy/Retention

Automatic Narrator
vs
V1 manual/chat trigger

Provider scope V1

Installer technologies

Upgrade strategy

OBS compatibility

Knowledge Dictionary patterns inadequados para aplicação
desktop integrada ao OBS.

Para cada conflito:

CONFLITO
FONTES
IMPACTO
RISCO
ESTADO
CAMINHO DE RESOLUÇÃO

Caminhos:

RESEARCH
ADR
REQUIREMENTS
CLIENT DECISION
OUT OF SCOPE

============================================================
FASE 9 — RESEARCH BACKLOG
============================================================

Atualizar:

docs/research/RESEARCH_BACKLOG.md

Somente adicionar pesquisas realmente necessárias.

Garantir cobertura de:

- OBS 32.x Plugin SDK;
- headers/libs/build;
- compatibilidade;
- caminhos oficiais de instalação de plugins;
- OBS Dock;
- native audio;
- OBS WebSocket;
- divisão de responsabilidades;
- C++ ↔ .NET IPC;
- process isolation;
- SQLite lifecycle;
- Credential Manager;
- DPAPI;
- YouTube Live Chat API;
- AI provider abstraction;
- local AI viability;
- TTS abstraction;
- installer;
- upgrade;
- repair;
- uninstall;
- OBS compatibility strategy.

NÃO executar essas pesquisas nesta task.

Esta task identifica e organiza conhecimento.

============================================================
FASE 10 — KNOWLEDGE QUALITY GATE
============================================================

Atualizar:

docs/governance/KNOWLEDGE_QUALITY_GATE.md

Verificar explicitamente:

[ ] Kit IA Dev base analisado
[ ] Knowledge Dictionary localizado
[ ] Knowledge Dictionary lido integralmente
[ ] Documentação baseline analisada
[ ] Governança analisada
[ ] Prompt History tratado como fonte não canônica
[ ] Project Knowledge Map atualizado
[ ] Knowledge Decisions atualizado
[ ] Knowledge Conflicts atualizado
[ ] Research Backlog atualizado
[ ] Advanced Skills classificado
[ ] Nenhum conflito crítico não controlado impede Requirements
[ ] Unknowns estão classificados
[ ] Research está separado de Requirements
[ ] ADR está separado de Requirements
[ ] Client Decisions estão identificadas
[ ] Requirements pode começar sem inventar conhecimento

Resultado:

PASSED

ou:

BLOCKED

NÃO marcar PASSED artificialmente.

============================================================
REGRA DO GATE
============================================================

O Knowledge Quality Gate PODE passar mesmo existindo:

REQUIRES_RESEARCH
REQUIRES_ADR
REQUIRES_CLIENT_DECISION
FUTURE
OUT_OF_SCOPE

desde que esses itens estejam:

- identificados;
- classificados;
- rastreáveis;
- controlados;
- não impeçam a elaboração segura de Requirements.

O objetivo do Gate não é resolver Architecture.

O objetivo é verificar se Requirements pode começar com uma
base confiável.

============================================================
FASE 11 — RELATÓRIO
============================================================

Criar ou atualizar:

docs/reports/KNOWLEDGE_QUALITY_GATE_REPORT.md

Em português do Brasil.

Incluir:

- data;
- task;
- fontes analisadas;
- Knowledge Dictionary;
- quantidade de documentos analisados;
- conceitos relevantes;
- itens ADOPT;
- itens ADAPT;
- itens REFERENCE;
- itens FUTURE;
- itens OUT_OF_SCOPE;
- itens REQUIRES_RESEARCH;
- itens REQUIRES_ADR;
- itens REQUIRES_CLIENT_DECISION;
- conflitos;
- blockers;
- Advanced Skills status;
- resultado do Gate;
- Requirements Readiness;
- recomendação.

============================================================
FASE 12 — CONSISTÊNCIA DOCUMENTAL
============================================================

Verificar todos os documentos alterados.

Garantir:

- pt-BR;
- UTF-8;
- sem caracteres corrompidos;
- sem links quebrados;
- sem contradições;
- sem decisões arquiteturais inventadas;
- sem implementação fictícia;
- sem afirmar que código existe;
- sem afirmar que testes inexistentes passaram;
- sem afirmar que pesquisa não executada foi concluída.

Preservar:

Product Source Code:
NOT CREATED

Implementation:
NOT STARTED

Architecture:
NOT STARTED

Requirements:
NOT STARTED

até o fim desta task.

============================================================
FASE 13 — CODE REVIEW
============================================================

Executar Code Review completo da task.

Usar as Skills disponíveis quando aplicável.

Classificar:

CRITICAL
HIGH
MEDIUM
LOW
INFO

Corrigir:

CRITICAL
HIGH

Corrigir MEDIUM quando estiver dentro do escopo e a correção
for segura.

Reexecutar validações após correções.

============================================================
FASE 14 — SECURITY / SECRET SCAN
============================================================

Executar Security Audit / Secret Scan.

Verificar pelo menos:

API keys
tokens
passwords
credentials
private keys
connection strings
OAuth secrets
refresh tokens
.env
dados sensíveis

Resultado esperado:

Secrets:
NONE

Se segredo real for encontrado:

NÃO COMMITAR.

Tratar como BLOCKER.

============================================================
FASE 15 — PROMPT TRACEABILITY
============================================================

Aplicar a regra oficial.

Inspecionar:

docs/prompts/history/prompt*.md

Identificar o maior número existente.

O último conhecido é:

prompt11.md

Portanto, se confirmado, este prompt deverá ser:

docs/prompts/history/prompt12.md

NÃO assumir cegamente.

Calcular o próximo número real.

Arquivar ESTE PROMPT integralmente.

Não sobrescrever arquivos existentes.

O prompt deve entrar no mesmo commit da task.

============================================================
FASE 16 — VALIDAÇÃO PRÉ-COMMIT
============================================================

Executar:

git status
git diff
git diff --check

Validar:

- somente arquivos autorizados;
- nenhuma alteração no OBS;
- nenhum código de produto;
- nenhuma alteração indevida em main/hml;
- prompt arquivado;
- documentação válida;
- Gate calculado;
- relatório criado;
- secrets = NONE.

============================================================
FASE 17 — COMMIT
============================================================

Se a documentação da task estiver válida:

git add ...

Criar commit seguindo Conventional Commits.

Sugestão:

docs: complete knowledge quality gate

O conteúdo humano dos documentos continua em pt-BR.

============================================================
FASE 18 — PUSH
============================================================

Push:

origin/feature/task-knowledge-quality-gate

Validar:

local HEAD = remote HEAD

============================================================
FASE 19 — PULL REQUEST
============================================================

Criar PR:

feature/task-knowledge-quality-gate
→
develop

Título pode seguir Conventional Commit.

Descrição humana:

PORTUGUÊS DO BRASIL.

Incluir:

- objetivo;
- fontes analisadas;
- Knowledge Dictionary;
- principais classificações;
- conflitos;
- Advanced Skills;
- resultado do Gate;
- Requirements Readiness;
- validações;
- escopo não executado.

============================================================
FASE 20 — PR VALIDATION
============================================================

Validar PR integralmente.

Confirmar:

Critical Findings = 0
High Findings = 0
Secrets = NONE
Documentation Validation = PASSED
Prompt Traceability = PASSED
GitFlow = PASSED

============================================================
FASE 21 — MERGE AUTOMÁTICO PARA DEVELOP
============================================================

REGRA IMPORTANTE:

O merge documental desta task para develop NÃO depende de o
Knowledge Quality Gate resultar PASSED.

Existem dois gates distintos:

1. QUALITY GATE DA TASK
2. KNOWLEDGE QUALITY GATE DO PRODUTO

Se a documentação/diagnóstico estiver correta, ela pode ser
mergeada mesmo se o resultado documentado for:

Knowledge Quality Gate:
BLOCKED

Portanto:

se a TASK estiver aprovada:

MERGE AUTOMÁTICO
feature/task-knowledge-quality-gate
→
develop

Não pedir confirmação intermediária.

============================================================
FASE 22 — PÓS-MERGE
============================================================

Depois do merge:

git switch develop
git pull --ff-only origin develop

Validar:

develop local SHA
=
origin/develop SHA

Confirmar PR:

MERGED

Excluir:

feature local
feature remota

quando seguro.

Validar:

git status

Resultado:

Working Tree:
CLEAN

============================================================
IMPORTANTE — NÃO PROMOVER NESTA TASK
============================================================

Esta é uma Task normal.

Portanto o fluxo termina em:

develop

NÃO executar:

develop → hml

NÃO criar:

release/1.0.0XXXX

NÃO executar:

release → main

NÃO criar tag.

NÃO criar GitHub Release.

Esses passos pertencem ao fluxo de entrega/release, não ao
fluxo individual de Task.

============================================================
NÃO EXECUTAR
============================================================

NÃO executar Requirements.

NÃO executar Architecture.

NÃO executar pesquisas técnicas externas.

NÃO criar ADR final.

NÃO criar backlog funcional de implementação.

NÃO criar Tasks de implementação.

NÃO criar código.

NÃO criar src/.

NÃO criar solution .NET.

NÃO criar projeto C++.

NÃO criar SQLite.

NÃO criar installer.

NÃO modificar OBS.

NÃO modificar:

C:\Users\gfmau\AppData\Roaming\obs-studio

NÃO criar Advanced Skills falsas.

NÃO promover develop para hml.

NÃO modificar main.

============================================================
RESULTADO FINAL OBRIGATÓRIO
============================================================

Retornar em PORTUGUÊS DO BRASIL:

OBS-AI-Live-Assistant

TASK:
knowledge-quality-gate

TASK STATUS:
CONCLUÍDA / BLOQUEADA

Execução integral:
CONCLUÍDA / BLOQUEADA

Knowledge Dictionary:
LIDO / BLOQUEADO

Caminho do Knowledge Dictionary:
<caminho>

Documentos analisados:
<quantidade>

Project Knowledge Map:
ATUALIZADO / BLOQUEADO

Knowledge Decisions:
ATUALIZADO / BLOQUEADO

Knowledge Conflicts:
ATUALIZADO / BLOQUEADO

Research Backlog:
ATUALIZADO / BLOQUEADO

Advanced Skills:
BLOCKED

Advanced Skills para Requirements:
NON-BLOCKING / BLOCKING

Knowledge Quality Gate:
PASSED / BLOCKED

Requirements Readiness:
READY / NOT READY

Relatório:
docs/reports/KNOWLEDGE_QUALITY_GATE_REPORT.md

Prompt arquivado:
SIM / NÃO

Prompt:
docs/prompts/history/promptN.md

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
feature/task-knowledge-quality-gate

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

Requirements:
NÃO INICIADO

Architecture:
NÃO INICIADA

Product Source Code:
NÃO CRIADO

Implementation:
NÃO INICIADA

OBS Environment:
NÃO MODIFICADO

PRÓXIMO:

Se Knowledge Quality Gate = PASSED:

REQUIREMENTS

Se Knowledge Quality Gate = BLOCKED:

RESOLVER BLOCKERS DOCUMENTADOS

============================================================
STOP
============================================================

O STOP SOMENTE DEVE OCORRER DEPOIS DE:

- análise concluída;
- documentação concluída;
- correções concluídas;
- Code Review concluído;
- Security Scan concluído;
- prompt arquivado;
- commit realizado;
- push realizado;
- PR criado;
- PR validado;
- merge para develop realizado, se a task estiver aprovada;
- develop sincronizada;
- feature limpa;
- Working Tree validado;
- relatório final emitido.

NÃO iniciar Requirements automaticamente.

Aguardar nova autorização após a conclusão integral desta task.
