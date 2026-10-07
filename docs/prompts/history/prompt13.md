AUTORIZAÇÃO — EXECUTAR SECURITY REQUIREMENTS COMPLETO

PROJETO:
OBS-AI-Live-Assistant

REPOSITÓRIO:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

KIT IA DEV:
D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev

TASK:
security-requirements

BRANCH:
feature/task-security-requirements

============================================================
1. EXECUÇÃO INTEGRAL — REGRA OBRIGATÓRIA
============================================================

ESTA TASK ESTÁ AUTORIZADA PARA EXECUÇÃO COMPLETA.

Executar TODO o setup e TODAS as etapas pertencentes ao
escopo desta task até o fim.

NÃO solicitar confirmações intermediárias para:

- leitura da documentação;
- análise dos Requirements;
- modelagem de segurança;
- criação/edição de documentação;
- correções;
- Security Review;
- Code Review;
- Quality Gates;
- Secret Scan;
- arquivamento do prompt;
- commit;
- push;
- criação do PR;
- validação do PR;
- merge feature/task-* → develop;
- sincronização de develop;
- cleanup da feature.

STOP significa:

FIM COMPLETO DA TASK.

Somente interromper diante de:

- bloqueio técnico real;
- risco destrutivo;
- segredo/credencial necessário;
- conflito Git não resolvível com segurança;
- decisão obrigatória do cliente que bloqueie a task;
- Critical/High Finding impossível de corrigir;
- necessidade de alteração fora do escopo autorizado.

============================================================
2. IDIOMA
============================================================

Toda documentação humana deve ser:

PORTUGUÊS DO BRASIL — pt-BR.

Preservar nomes e classificações técnicas quando apropriado:

BYOK
OAuth
API Key
Access Token
Refresh Token
Prompt Injection
Rate Limiting
Threat Model
Trust Boundary
STRIDE
CURRENT DIRECTION
REQUIRES_RESEARCH
REQUIRES_ADR
REQUIRES_CLIENT_DECISION
PASSED
BLOCKED

============================================================
3. ESTADO ATUAL
============================================================

Estado conhecido:

Knowledge Quality Gate:
PASSED

Requirements:
CONCLUÍDO

Requirements Quality Gate:
PASSED

Architecture Readiness:
READY

RF:
36

RNF:
28

Blocking Questions:
0

Architecture:
NOT STARTED

Implementation:
NOT STARTED

Product Source Code:
NOT CREATED

OBS:
NOT MODIFIED

Confirmar tudo pelo repositório.

Não confiar apenas neste prompt.

============================================================
4. RECUPERAR ESTADO GIT
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

feature/task-security-requirements

Toda alteração deverá ocorrer nessa branch.

============================================================
5. AUDITAR PROMPT HISTORY
============================================================

ANTES de criar qualquer novo prompt histórico:

inspecionar:

docs/prompts/history/

Listar:

prompt*.md

Determinar:

- convenção atual;
- maior número existente;
- existência de duplicações;
- lacunas;
- possível problema relacionado ao prompt3.md reportado
  pela task Requirements.

IMPORTANTE:

NÃO renumerar arquivos históricos automaticamente.

NÃO sobrescrever arquivos.

NÃO apagar histórico.

Se houver inconsistência:

documentar e corrigir somente se houver evidência suficiente
de erro e a correção preservar rastreabilidade.

Caso contrário:

registrar a inconsistência para tratamento posterior.

Calcular o próximo prompt pelo estado REAL do repositório.

============================================================
6. CARREGAR FONTES CANÔNICAS
============================================================

Ler integralmente os documentos relevantes.

Incluir:

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

Ler especialmente:

REQUIREMENTS.md
FUNCTIONAL_REQUIREMENTS.md
NON_FUNCTIONAL_REQUIREMENTS.md
CONSTRAINTS.md
ASSUMPTIONS.md
OPEN_QUESTIONS.md
RISKS.md
RESEARCH_DEPENDENCIES.md
ADR_CANDIDATES.md
REQUIREMENTS_TRACEABILITY.md
REQUIREMENTS_REPORT.md

============================================================
7. OBJETIVO DE SECURITY REQUIREMENTS
============================================================

Transformar os requisitos e riscos relacionados à segurança
em uma especificação formal e rastreável.

SECURITY REQUIREMENTS define:

O QUE deve ser protegido.

Não decidir prematuramente:

COMO a arquitetura implementará cada proteção.

Exemplo:

CORRETO:

"As credenciais dos providers não devem ser armazenadas em
texto puro."

Não transformar automaticamente isso em:

"O sistema obrigatoriamente utilizará DPAPI."

A escolha entre:

DPAPI
Windows Credential Manager
ou outra solução

pertence a:

RESEARCH / ADR.

============================================================
8. ESTRUTURA DOCUMENTAL
============================================================

Organizar:

docs/security/

Estrutura sugerida:

docs/security/
├── README.md
├── SECURITY_REQUIREMENTS.md
├── THREAT_MODEL.md
├── TRUST_BOUNDARIES.md
├── ASSET_CLASSIFICATION.md
├── SECRETS_AND_BYOK.md
├── INPUT_OUTPUT_SECURITY.md
├── OBS_SECURITY.md
├── LOGGING_AND_PRIVACY.md
├── SECURITY_RISKS.md
├── SECURITY_RESEARCH_DEPENDENCIES.md
└── SECURITY_TRACEABILITY.md

Não criar duplicações.

Se documentos equivalentes já existirem:

consolidar.

============================================================
9. SECURITY REQUIREMENTS
============================================================

Criar requisitos numerados:

SEC-001
SEC-002
SEC-003
...

Cada requisito deve conter:

ID
Título
Descrição
Motivação
Prioridade
Status
Origem
Ativo protegido
Ameaça relacionada
Dependências
Critérios de aceite
Research/ADR relacionado
Observações

Prioridades:

MUST
SHOULD
COULD
WON'T V1

============================================================
10. ATIVOS A PROTEGER
============================================================

Identificar e classificar pelo menos:

- API Keys;
- OAuth tokens;
- refresh tokens;
- provider credentials;
- configurações;
- dados das sessões;
- mensagens do chat;
- respostas da IA;
- memória;
- histórico;
- logs;
- banco local;
- configurações do OBS;
- processo OBS;
- Assistant Core;
- integração OBS;
- dados do streamer;
- dados de viewers quando persistidos.

Classificar sensibilidade quando aplicável.

Não inventar dados que o sistema não armazena.

============================================================
11. THREAT MODEL
============================================================

Criar threat model inicial.

Usar STRIDE quando apropriado:

Spoofing
Tampering
Repudiation
Information Disclosure
Denial of Service
Elevation of Privilege

Analisar pelo menos:

Viewer
Chat Provider
AI Provider
TTS Provider
Assistant Core
OBS integration
local machine
local storage
network communication
installer/updater

Para cada ameaça registrar:

ID
Origem
Alvo
Descrição
Impacto
Probabilidade qualitativa
Mitigação requerida
Status
Requirement relacionado
Research/ADR relacionado

Não inventar controles técnicos finais.

============================================================
12. TRUST BOUNDARIES
============================================================

Documentar boundaries conceituais entre:

Viewer
↓
Chat Platform
↓
Chat Provider
↓
Assistant

Assistant
↓
AI Provider

Assistant
↓
TTS Provider

Assistant Core
↔
OBS Integration

Assistant
↔
Local Storage

Installer
↔
Operating System / OBS

Considerar como NÃO CONFIÁVEL por padrão:

- mensagens do chat;
- conteúdo fornecido por viewers;
- respostas externas;
- AI output;
- dados vindos de APIs externas.

============================================================
13. BYOK / SECRETS
============================================================

BYOK é requisito fundamental.

Documentar requisitos para:

- inclusão da credencial;
- validação;
- atualização;
- substituição;
- remoção;
- mascaramento;
- armazenamento seguro;
- acesso mínimo;
- não exposição em UI;
- não exposição em logs;
- não exposição em exceptions;
- não exposição em telemetry;
- não exposição em prompts;
- não commit no Git.

PROIBIDO:

plaintext secrets em:

SQLite
appsettings
JSON
.env versionado
logs
prompt history
reports

Não escolher mecanismo final nesta task.

Criar Research/ADR quando necessário.

============================================================
14. OAUTH
============================================================

Avaliar requisitos para integrações que utilizem OAuth.

Considerar:

authorization code
access token
refresh token
expiration
revocation
logout/disconnect
token refresh
least privilege

Não inventar provider OAuth quando não aplicável.

============================================================
15. CHAT COMO ENTRADA NÃO CONFIÁVEL
============================================================

Formalizar:

CHAT INPUT = UNTRUSTED INPUT

Avaliar:

- tamanho máximo;
- normalização;
- validação;
- sanitização quando pertinente;
- flood;
- spam;
- repeated messages;
- malicious instructions;
- prompt injection;
- encoding;
- malformed content.

============================================================
16. PROMPT INJECTION
============================================================

Criar requisitos específicos.

Viewer não deve conseguir:

- substituir System Instructions;
- solicitar secrets;
- modificar configuração;
- executar comandos locais;
- executar shell;
- acessar arquivos arbitrários;
- alterar OBS diretamente;
- acessar credenciais;
- elevar privilégios.

Separar:

prompt injection mitigation

de:

architecture implementation.

============================================================
17. AI OUTPUT = UNTRUSTED
============================================================

Formalizar:

AI OUTPUT = UNTRUSTED OUTPUT

Antes de qualquer ação sensível:

validar
autorizar
limitar
registrar quando apropriado.

AI não deve possuir autoridade implícita para:

- executar comandos;
- modificar sistema operacional;
- alterar configurações críticas;
- manipular OBS sem autorização;
- acessar secrets;
- instalar software.

============================================================
18. PROTEÇÃO DO OBS
============================================================

Requisito crítico:

O assistente NÃO pode comprometer a estabilidade do OBS.

Formalizar requisitos para:

- isolamento de falhas;
- timeout;
- cancelamento;
- rate limiting;
- circuit breaking conceitual quando aplicável;
- falha segura;
- tratamento de exceptions;
- indisponibilidade de providers.

Chat não pode executar diretamente comandos sensíveis do OBS.

Qualquer controle futuro deve possuir:

authorization layer
allowlist
validation
rate limiting
auditability

quando aplicável.

============================================================
19. DENIAL OF SERVICE
============================================================

Avaliar ameaças de:

- spam de chat;
- fila ilimitada;
- respostas muito longas;
- TTS excessivo;
- requests simultâneos;
- provider timeout;
- retry infinito;
- memória crescente;
- logs excessivos;
- banco crescendo indefinidamente.

Gerar requisitos.

Não inventar thresholds sem evidência.

Usar:

CONFIGURABLE

ou:

REQUIRES_CLIENT_DECISION

quando necessário.

============================================================
20. RATE LIMITING
============================================================

Formalizar necessidade de:

- cooldown por viewer;
- cooldown global;
- queue limits;
- provider limits;
- cancellation;
- timeout.

Valores específicos podem permanecer configuráveis.

============================================================
21. LOGGING SEGURO
============================================================

Criar requisitos para:

- structured logging quando aplicável;
- redaction;
- secrets filtering;
- token masking;
- PII minimization;
- log retention;
- diagnostic usefulness;
- correlation quando apropriado.

Logs nunca devem conter credenciais em texto puro.

============================================================
22. PRIVACIDADE
============================================================

Avaliar:

- mensagens de viewers;
- usernames;
- IDs;
- sessões;
- histórico;
- memória;
- logs;
- analytics.

Aplicar:

data minimization.

Quando retenção ainda não estiver definida:

REQUIRES_CLIENT_DECISION

ou:

REQUIRES_RESEARCH.

Não inventar prazo.

============================================================
23. LOCAL STORAGE
============================================================

Formalizar requisitos de segurança para dados locais.

Avaliar:

- database;
- configuration;
- cache;
- logs;
- temporary files.

Não escolher tecnologia criptográfica final sem Research/ADR.

============================================================
24. INSTALLER / UPDATE
============================================================

Definir requisitos de segurança para:

installer
upgrade
repair
uninstall

Avaliar:

- origem confiável;
- integridade;
- permissões;
- arquivos instalados;
- rollback;
- preservação segura de configuração;
- remoção de dados/secrets quando aplicável.

Code signing pode ser:

REQUIRES_RESEARCH

se ainda não decidido.

============================================================
25. DEPENDÊNCIAS / SUPPLY CHAIN
============================================================

Criar requisitos conceituais para:

- dependency review;
- known vulnerabilities;
- package integrity;
- dependency minimization;
- update strategy.

Não instalar ferramentas nesta task.

============================================================
26. SECURITY RISKS
============================================================

Consolidar riscos específicos em:

docs/security/SECURITY_RISKS.md

IDs:

SRISK-001
SRISK-002
...

Avaliar pelo menos:

secret leakage
prompt injection
chat abuse
provider compromise
malicious output
DoS
OBS disruption
local data exposure
dependency compromise
installer tampering
OAuth token leakage
excessive logging
privacy leakage

============================================================
27. SECURITY RESEARCH DEPENDENCIES
============================================================

Criar:

docs/security/SECURITY_RESEARCH_DEPENDENCIES.md

Mapear pelo menos:

Credential Manager vs DPAPI

OAuth provider requirements

secret storage

local database protection

log redaction

installer signing

update integrity

OBS integration trust boundary

IPC authentication/authorization quando aplicável

============================================================
28. SECURITY TRACEABILITY
============================================================

Criar:

docs/security/SECURITY_TRACEABILITY.md

Mapear:

Requirement RF/RNF
→ Security Requirement
→ Threat
→ Asset
→ Risk
→ Research
→ ADR Candidate
→ Acceptance Criteria

Garantir rastreabilidade com os Requirements existentes.

============================================================
29. SECURITY QUALITY GATE
============================================================

Criar ou atualizar gate apropriado.

Validar:

[ ] assets identificados
[ ] trust boundaries identificadas
[ ] threat model criado
[ ] BYOK formalizado
[ ] secrets formalizados
[ ] OAuth considerado
[ ] chat classificado como untrusted
[ ] AI output classificado como untrusted
[ ] prompt injection tratado
[ ] OBS protection tratada
[ ] DoS tratado
[ ] rate limiting tratado
[ ] logging seguro tratado
[ ] privacy tratada
[ ] local storage tratado
[ ] installer/update tratado
[ ] supply chain tratada
[ ] security risks registrados
[ ] research dependencies registradas
[ ] traceability estabelecida
[ ] nenhum HOW arquitetural crítico foi decidido prematuramente
[ ] Architecture pode prosseguir com segurança

Resultado:

PASSED

ou:

BLOCKED.

Não marcar PASSED artificialmente.

============================================================
30. RELATÓRIO
============================================================

Criar:

docs/reports/SECURITY_REQUIREMENTS_REPORT.md

Incluir:

- fontes analisadas;
- quantidade de Security Requirements;
- assets;
- threats;
- trust boundaries;
- security risks;
- research dependencies;
- decisões pendentes;
- Security Quality Gate;
- Architecture Security Readiness;
- blockers;
- recomendação.

============================================================
31. ATUALIZAR DOCUMENTAÇÃO
============================================================

Atualizar somente quando necessário:

README.md
docs/README.md
docs/security/README.md
docs/reports/README.md
docs/requirements/REQUIREMENTS_TRACEABILITY.md
docs/knowledge/PROJECT_KNOWLEDGE_MAP.md
docs/research/RESEARCH_BACKLOG.md

Não duplicar informações.

============================================================
32. CODE REVIEW + SECURITY REVIEW
============================================================

Executar:

Security Review
+
Code Review documental

Usar Skills disponíveis quando aplicável:

security-audit
code-review
doc-writer
feature-planner
pr-writer

Classificar findings:

CRITICAL
HIGH
MEDIUM
LOW
INFO

Corrigir:

CRITICAL
HIGH

Corrigir MEDIUM quando dentro do escopo e seguro.

Revalidar.

============================================================
33. SECRET SCAN
============================================================

Executar Secret Scan real sobre alterações e arquivos
relevantes.

Procurar:

API Keys
OAuth secrets
tokens
passwords
private keys
connection strings
refresh tokens
.env
credenciais

Se encontrar segredo real:

NÃO COMMITAR.

BLOCK.

============================================================
34. PROMPT TRACEABILITY
============================================================

Inspecionar o estado REAL de:

docs/prompts/history/

Não assumir que prompt13 ou qualquer outro seja o próximo.

Auditar especialmente a situação em que a task Requirements
reportou:

docs/prompts/history/prompt3.md

Determinar se isso está correto segundo a estrutura real.

NÃO:

- sobrescrever;
- renumerar cegamente;
- excluir histórico válido.

Arquivar ESTE prompt integralmente no próximo identificador
válido segundo a convenção real.

Registrar qualquer inconsistência detectada.

============================================================
35. VALIDAÇÃO PRÉ-COMMIT
============================================================

Executar:

git status
git diff
git diff --check

Validar:

Security Requirements = concluído

Requirements existentes = preservados

Architecture = não iniciada

Research externo = não executado

Product Code = não criado

OBS = não modificado

Secrets = NONE

Prompt Traceability = OK

============================================================
36. COMMIT
============================================================

Conventional Commit sugerido:

docs: define security requirements

Incluir no mesmo commit:

- documentação;
- relatório;
- correções;
- prompt history desta task.

============================================================
37. PUSH
============================================================

Push:

origin/feature/task-security-requirements

Validar:

local HEAD = remote HEAD

============================================================
38. PULL REQUEST
============================================================

Criar PR:

feature/task-security-requirements
→
develop

Descrição:

PORTUGUÊS DO BRASIL.

Incluir:

- objetivo;
- Security Requirements;
- Threat Model;
- Trust Boundaries;
- assets;
- risks;
- Research Dependencies;
- Security Quality Gate;
- Architecture Security Readiness;
- validações.

============================================================
39. PR VALIDATION
============================================================

Obrigatório:

Critical Findings = 0
High Findings = 0
Secrets = NONE
Security Quality Gate = PASSED ou diagnóstico válido
Prompt Traceability = PASSED
GitFlow = PASSED

============================================================
40. MERGE AUTOMÁTICO → DEVELOP
============================================================

Se a TASK documental estiver aprovada:

feature/task-security-requirements
→
develop

MERGE AUTOMÁTICO.

Não pedir autorização intermediária.

Se Security Quality Gate = BLOCKED mas o diagnóstico estiver
correto:

a documentação pode ser mergeada,

porém:

Architecture Security Readiness = NOT READY.

============================================================
41. PÓS-MERGE
============================================================

Após merge:

git switch develop
git pull --ff-only origin develop

Validar:

develop local = origin/develop

Excluir feature local e remota quando seguro.

Working Tree:

CLEAN

============================================================
42. NÃO PROMOVER
============================================================

Esta é uma Task individual.

Termina em:

develop.

NÃO executar:

develop → hml

NÃO criar:

release/1.0.0XXXX

NÃO modificar:

main

NÃO criar:

tag
GitHub Release

============================================================
43. NÃO EXECUTAR
============================================================

NÃO executar Architecture.

NÃO executar Research externo.

NÃO criar ADR final.

NÃO criar código.

NÃO criar src/.

NÃO criar solution .NET.

NÃO criar C++.

NÃO criar SQLite.

NÃO criar installer.

NÃO modificar OBS.

NÃO modificar:

C:\Users\gfmau\AppData\Roaming\obs-studio

============================================================
44. RESULTADO FINAL
============================================================

Retornar em português:

OBS-AI-Live-Assistant

TASK:
security-requirements

TASK STATUS:
CONCLUÍDA / BLOQUEADA

Execução integral:
CONCLUÍDA / BLOQUEADA

Requirements Quality Gate:
PASSED / BLOCKED

Security Requirements:
CONCLUÍDO / BLOQUEADO

Security Requirements:
<quantidade>

Assets:
<quantidade>

Threats:
<quantidade>

Trust Boundaries:
<quantidade>

Security Risks:
<quantidade>

Security Research Dependencies:
<quantidade>

Security Traceability:
PASSED / BLOCKED

Security Quality Gate:
PASSED / BLOCKED

Architecture Security Readiness:
READY / NOT READY

Prompt History Audit:
PASSED / FINDINGS

Prompt anterior inconsistente:
SIM / NÃO

Prompt arquivado:
SIM / NÃO

Prompt:
docs/prompts/history/<arquivo>

Relatório:
docs/reports/SECURITY_REQUIREMENTS_REPORT.md

Security Review:
APROVADO / BLOQUEADO

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
feature/task-security-requirements

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

Product Source Code:
NÃO CRIADO

Implementation:
NÃO INICIADA

OBS:
NÃO MODIFICADO

PRÓXIMO:

Se:

Security Quality Gate = PASSED
e
Architecture Security Readiness = READY

então:

RESEARCH PREPARATION / ARCHITECTURE PREPARATION

Caso contrário:

RESOLVER SECURITY BLOCKERS

============================================================
STOP
============================================================

STOP SOMENTE DEPOIS DE CONCLUIR INTEGRALMENTE:

setup
→ Security Requirements
→ Threat Model
→ Trust Boundaries
→ Security Traceability
→ Security Quality Gate
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

NÃO iniciar Research ou Architecture automaticamente.
