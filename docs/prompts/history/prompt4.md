O repositório GitHub oficial do projeto já foi criado.

Projeto local:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

Repositório remoto:

https://github.com/gfmaurila/OBS-AI-Live-Assistant.git

Repository:
gfmaurila/OBS-AI-Live-Assistant

Default branch:
main

O repositório remoto está vazio.

==================================================
OBJETIVO DESTA ETAPA
==================================================

Concluir SOMENTE:

1. Git setup
2. vínculo com GitHub
3. primeiro commit do bootstrap
4. criação das branches develop e hml
5. push das branches
6. validação final do GitFlow

NÃO execute Requirements.
NÃO execute Architecture.
NÃO implemente código funcional.

==================================================
1. VALIDAR GIT
==================================================

Primeiro execute:

git --version

Se Git continuar indisponível:

PARE.

Informe:

GIT_SETUP: BLOCKED

Não tente simular Git.

==================================================
2. VALIDAR PROJETO LOCAL
==================================================

Acesse:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

Confirme que os artefatos do Kit IA Dev já existentes continuam presentes.

Antes de inicializar Git, valide:

AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md
agent_docs/
.ai/
.claude/
.codex/
.github/

Não remova nem regenere desnecessariamente esses arquivos.

==================================================
3. .GITIGNORE
==================================================

Antes do primeiro commit, crie ou valide um .gitignore adequado para:

.NET
C#
C++
Visual Studio
VS Code
OBS plugin development
SQLite
Windows

O .gitignore deve impedir versionamento acidental de:

bin/
obj/
.vs/
build/
out/
dist/
logs/
temp/
cache/
*.user
*.suo
*.db
*.db-shm
*.db-wal
.env
.env.*
secrets
credentials
API keys
tokens
generated binaries
installer artifacts

IMPORTANTE:

Não ignorar:

AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md
.ai/
.claude/
.codex/
.github/
agent_docs/
docs/
tasks/

quando estes fizerem parte da governança do projeto.

==================================================
4. INICIALIZAR GIT
==================================================

Se ainda não existir .git:

git init -b main

Depois configure:

origin

para:

https://github.com/gfmaurila/OBS-AI-Live-Assistant.git

Valide:

git remote -v

O resultado deverá apontar para:

origin https://github.com/gfmaurila/OBS-AI-Live-Assistant.git

==================================================
5. PRIMEIRO COMMIT
==================================================

Revise todos os arquivos antes do commit.

Confirme que NÃO existem:

secrets
tokens
API keys
credenciais
arquivos temporários
artefatos de build
bancos locais

Depois faça o primeiro commit contendo somente o bootstrap de engenharia.

Utilize Conventional Commits.

Mensagem:

chore: bootstrap AI development environment

==================================================
6. PUSH MAIN
==================================================

Faça push:

main → origin/main

Utilize upstream tracking.

Valide que:

local main
=
remote main

==================================================
7. CRIAR DEVELOP
==================================================

A partir de main:

crie:

develop

Faça push para:

origin/develop

Configure upstream tracking.

==================================================
8. CRIAR HML
==================================================

A branch hml deverá inicialmente partir de develop.

Crie:

hml

Faça push para:

origin/hml

Configure upstream tracking.

==================================================
9. GITFLOW
==================================================

Estrutura esperada:

main
 ↑
release/*
 ↑
hml
 ↑
develop
 ↑
feature/task-*

Regras:

feature/task-* deve partir de develop.

Cada Task aprovada deverá possuir sua própria branch quando aplicável.

Fluxo futuro:

feature/task-*
    ↓
PR
    ↓
develop
    ↓
hml
    ↓
release/x.y.z
    ↓
main

NÃO crie:

feature/*
release/*

agora, pois ainda não existem Tasks ou release aprovadas.

==================================================
10. NÃO ALTERAR GITHUB ALÉM DO NECESSÁRIO
==================================================

Nesta etapa:

NÃO criar Issues.
NÃO criar Pull Requests.
NÃO criar Releases.
NÃO configurar Actions.
NÃO configurar branch protection.
NÃO alterar visibilidade do repositório.

Esses itens serão tratados nas fases apropriadas.

==================================================
11. SKILLS AVANÇADAS
==================================================

Skills Avançadas continuam sendo um pré-requisito separado.

Se o pacote ainda não estiver disponível:

Advanced Skills:
BLOCKED

Isso NÃO deve impedir a configuração do Git.

Não invente Skills ausentes.

==================================================
12. VALIDAÇÃO FINAL
==================================================

Valide:

git status
git branch
git branch -r
git remote -v
git log

Confirme:

main existe local e remoto
develop existe local e remoto
hml existe local e remoto
origin está correto
working tree está limpa
primeiro commit existe
nenhum secret foi versionado

==================================================
13. RESULTADO ESPERADO
==================================================

Apresente:

OBS-AI-Live-Assistant

GIT SETUP:
COMPLETED / BLOCKED

Repository:
gfmaurila/OBS-AI-Live-Assistant

Remote:
origin

main:
OK / BLOCKED

develop:
OK / BLOCKED

hml:
OK / BLOCKED

Initial Commit:
OK / BLOCKED

Push:
OK / BLOCKED

Working Tree:
CLEAN / DIRTY

Advanced Skills:
OK / BLOCKED

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

CLIENT_APPROVAL_GATE:
PENDING_APPROVAL

==================================================
STOP CONDITION
==================================================

Ao terminar:

STOP.

NÃO execute Requirements.
NÃO execute Architecture.
NÃO implemente código.

Aguarde minha autorização explícita.