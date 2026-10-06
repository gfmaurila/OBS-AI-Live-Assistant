RETOMAR GIT SETUP — BLOQUEIO RESOLVIDO

O Git for Windows foi reinstalado e está funcionando corretamente.

Validação realizada:

git --version
git version 2.55.0.windows.5

Configuração global:

user.name = Guilherme Maurila
user.email = gfmaurila@gmail.com

==================================================
PROJETO
==================================================

Projeto local:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

Repositório GitHub:

https://github.com/gfmaurila/OBS-AI-Live-Assistant.git

Repository:

gfmaurila/OBS-AI-Live-Assistant

Default branch:

main

==================================================
IMPORTANTE
==================================================

RETOME exatamente a etapa GIT SETUP anteriormente bloqueada.

NÃO repita o bootstrap documental.

NÃO reinstale Agents.
NÃO reinstale Skills básicas.
NÃO regenere AGENTS.md.
NÃO regenere CLAUDE.md.
NÃO altere os documentos já validados desnecessariamente.

Skills Avançadas continuam sendo uma dependência separada.

==================================================
1. VALIDAR GIT
==================================================

Execute:

git --version

Confirme que o Git está operacional.

==================================================
2. VALIDAR .GITIGNORE
==================================================

Crie ou ajuste o .gitignore para o projeto.

Considere:

.NET 10
C#
C/C++
Visual Studio
VS Code
Windows
OBS plugin development
SQLite
logs
cache
temporários
secrets

Não versionar:

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

artefatos compilados
artefatos de instalador

Não ignore os arquivos de engenharia:

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

quando existentes.

==================================================
3. SECURITY CHECK
==================================================

ANTES do primeiro commit, examine os arquivos que serão
versionados.

Garanta que não existam:

- API keys
- passwords
- access tokens
- refresh tokens
- credentials
- connection strings com secrets
- chaves privadas
- certificados privados
- arquivos .env contendo secrets
- dados sensíveis locais

Se encontrar algum secret:

PARE.

Não faça commit.

Reporte o problema.

==================================================
4. INICIALIZAR REPOSITÓRIO
==================================================

Se .git ainda não existir:

git init -b main

Garanta que a branch principal seja:

main

==================================================
5. CONFIGURAR REMOTE
==================================================

Configure:

origin

para:

https://github.com/gfmaurila/OBS-AI-Live-Assistant.git

Valide:

git remote -v

Não crie outro remote desnecessariamente.

==================================================
6. PRIMEIRO COMMIT
==================================================

Adicione somente os arquivos válidos do bootstrap.

Revise:

git status

antes do commit.

Crie:

chore: bootstrap AI development environment

Esse será o primeiro commit documental/de engenharia.

Nenhum código funcional do produto deve ser criado.

==================================================
7. PUSH MAIN
==================================================

Publique:

main → origin/main

Configure upstream tracking.

Valide:

local main
=
origin/main

==================================================
8. DEVELOP
==================================================

A partir de main, crie:

develop

Publique:

develop → origin/develop

Configure upstream tracking.

==================================================
9. HML
==================================================

A partir de develop, crie:

hml

Publique:

hml → origin/hml

Configure upstream tracking.

==================================================
10. GITFLOW
==================================================

O fluxo oficial deverá ser:

feature/task-*
      ↓
    develop
      ↓
      hml
      ↓
 release/x.y.z
      ↓
     main

Regras:

- main = produção
- develop = integração/desenvolvimento
- hml = homologação
- feature/task-* nasce de develop
- release/* somente quando houver release aprovada

NÃO criar feature/task-* agora.

NÃO criar release/* agora.

==================================================
11. NÃO EXECUTAR
==================================================

NÃO criar código.

NÃO criar src/.

NÃO criar solution .NET.

NÃO criar projeto C++.

NÃO criar banco SQLite.

NÃO modificar OBS.

NÃO modificar:

C:\Users\gfmau\AppData\Roaming\obs-studio

NÃO executar Requirements.

NÃO executar Architecture.

NÃO executar Knowledge Quality Gate.

NÃO criar Pull Request.

NÃO criar GitHub Release.

NÃO configurar CI/CD ainda.

==================================================
12. VALIDAÇÃO
==================================================

Execute e valide:

git status
git branch
git branch -r
git remote -v
git log --oneline --decorate --all

Confirme:

- main local existe
- origin/main existe
- develop local existe
- origin/develop existe
- hml local existe
- origin/hml existe
- origin aponta para o repositório correto
- upstream está configurado
- primeiro commit existe
- working tree está limpa
- nenhum secret foi versionado

==================================================
13. RESULTADO
==================================================

Retorne exatamente o estado:

OBS-AI-Live-Assistant

GIT SETUP:
COMPLETED / BLOCKED

Git Version:
2.55.0.windows.5 / OUTRO / BLOCKED

Repository:
gfmaurila/OBS-AI-Live-Assistant

Remote:
OK / BLOCKED

Initial Commit:
OK / BLOCKED

main:
OK / BLOCKED

develop:
OK / BLOCKED

hml:
OK / BLOCKED

Upstream Tracking:
OK / BLOCKED

Secret Validation:
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
STOP
==================================================

Ao concluir o Git Setup:

STOP.

Não avance para Requirements.
Não avance para Architecture.
Não implemente o produto.

Aguarde autorização explícita.