AUTORIZAÇÃO — REVIEW + PR + MERGE AUTOMÁTICO PARA DEVELOP

Projeto:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

==================================================
NOVA REGRA PERMANENTE DE GITFLOW
==================================================

A partir desta tarefa:

Toda task executada em:

feature/task-*

deve, quando concluída com sucesso:

1. executar validações;
2. executar Code Review;
3. corrigir findings obrigatórios;
4. arquivar o prompt;
5. executar secret/security check;
6. commit;
7. push da própria feature;
8. criar PR para develop;
9. validar o PR;
10. fazer merge para develop;
11. validar develop após o merge;
12. remover a feature local e remota quando seguro;
13. terminar com develop sincronizada e limpa.

NÃO aguardar nova autorização entre:

TASK COMPLETED
→ REVIEW
→ PR
→ MERGE TO DEVELOP

desde que todos os Quality Gates estejam aprovados.

==================================================
REGRA DE BLOQUEIO
==================================================

O merge NÃO poderá ocorrer se existir:

- CRITICAL finding;
- HIGH finding não resolvido;
- teste obrigatório falhando;
- secret detectado;
- conflito Git;
- PR inválido;
- alteração fora do escopo;
- Quality Gate obrigatório reprovado;
- push incompleto;
- diferença inesperada entre local e remoto.

Nesses casos:

STOP.

Reportar:

MERGE:
BLOCKED

REASON:
<motivo>

==================================================
HML E MAIN
==================================================

Esta autorização automática vale SOMENTE para:

feature/task-* → develop

NÃO autoriza automaticamente:

develop → hml
hml → main
release/* → main

Promoções para homologação e produção continuam sujeitas
aos respectivos gates definidos pelo projeto.

==================================================
TASK ATUAL
==================================================

Branch:

feature/task-organize-prompt-history

Commit anteriormente confirmado:

2040d2db539926ee419701a6f6e3bdac2508e101

Objetivo atual:

finalizar completamente esta task através de:

REVIEW
→
PR
→
MERGE TO DEVELOP

==================================================
1. ARQUIVAR ESTE PROMPT
==================================================

Aplicar Prompt Traceability Rule.

Local:

docs/prompts/history/

Identificar automaticamente o próximo:

promptN.md

Nunca sobrescrever histórico.

O esperado, considerando prompt8.md existente, é:

prompt9.md

Salvar integralmente este prompt.

==================================================
2. ATUALIZAR GOVERNANÇA
==================================================

Atualizar a documentação canônica de governança para
registrar a nova regra permanente:

FEATURE COMPLETION RULE

feature/task-*
    ↓
Implementation / Documentation
    ↓
Validation
    ↓
Code Review
    ↓
Required Fixes
    ↓
Prompt Archive
    ↓
Security Check
    ↓
Commit
    ↓
Push
    ↓
Pull Request
    ↓
PR Validation
    ↓
Merge to develop
    ↓
Post-Merge Validation
    ↓
Feature Cleanup

Quando todos os gates estiverem aprovados, não é necessária
nova autorização do cliente especificamente entre PR e
merge para develop.

Registrar também que essa regra NÃO concede autorização
automática para promoção a hml ou main.

==================================================
3. CODE REVIEW
==================================================

Revisar:

develop...feature/task-organize-prompt-history

Classificar findings:

CRITICAL
HIGH
MEDIUM
LOW
INFO

CRITICAL:
bloqueia merge.

HIGH:
bloqueia merge até correção.

MEDIUM:
corrigir quando pertencente ao escopo da task.

LOW/INFO:
podem ser documentados sem bloquear merge.

==================================================
4. CORREÇÕES
==================================================

Se forem necessárias correções:

realizar exclusivamente em:

feature/task-organize-prompt-history

Validar novamente.

Arquivar este prompt.

Executar secret check.

Commit + push obrigatórios.

==================================================
5. COMMIT
==================================================

Toda alteração desta etapa deverá ser comitada.

Se houver somente:

- prompt9.md;
- atualização da governança;

utilizar:

docs: update feature completion workflow

Se existirem correções adicionais, utilizar Conventional
Commit apropriado ao conjunto real das alterações.

==================================================
6. PUSH
==================================================

Push obrigatório para:

origin/feature/task-organize-prompt-history

Confirmar:

local HEAD = remote HEAD

Working Tree:
CLEAN

==================================================
7. PULL REQUEST
==================================================

Criar PR:

FROM:

feature/task-organize-prompt-history

TO:

develop

Título:

docs: establish prompt history and traceability

Descrição contendo:

## Summary

- organizes operational prompt history;
- establishes prompt traceability;
- documents automatic feature completion workflow;
- preserves historical prompts;
- separates historical prompts from canonical documentation.

## Validation

- Code Review completed;
- historical prompts preserved;
- governance validated;
- secret scan completed;
- feature synchronized with remote;
- working tree clean.

## Scope

Documentation and governance only.

No product source code created.

No OBS environment modified.

## Known Blocker

Advanced Skills package remains unavailable.

==================================================
8. VALIDAR PR
==================================================

Antes do merge confirmar:

PR Base:
develop

PR Head:
feature/task-organize-prompt-history

PR Status:
OPEN

Conflicts:
NONE

Critical Findings:
0

High Findings:
0

Secret Validation:
OK

Required Validation:
PASSED

Remote:
SYNCHRONIZED

Somente se tudo estiver aprovado:

MERGE AUTORIZADO.

==================================================
9. MERGE
==================================================

Fazer merge do PR para:

develop

Preferir a estratégia de merge compatível com a política
atual do repositório.

Não utilizar force push.

Não alterar main.

Não alterar hml.

==================================================
10. PÓS-MERGE
==================================================

Após o merge:

mudar para:

develop

Atualizar:

origin/develop

Validar:

git status
git log
git branch -vv

Confirmar que o merge está presente em develop.

==================================================
11. CLEANUP
==================================================

Somente depois de confirmar o merge:

remover:

feature/task-organize-prompt-history

localmente.

Remover também:

origin/feature/task-organize-prompt-history

quando seguro e quando o PR estiver efetivamente merged.

Não excluir branch antes da confirmação do merge.

==================================================
12. REGRA PARA TODAS AS PRÓXIMAS TASKS
==================================================

A partir de agora toda task modificadora deve:

partir de develop;

usar:

feature/task-<nome>

e terminar automaticamente em:

commit
+
push
+
review
+
PR
+
merge para develop

quando todos os gates estiverem aprovados.

Não deixar feature concluída sem PR/merge apenas esperando
nova autorização, salvo quando houver bloqueio real.

==================================================
13. NÃO EXECUTAR
==================================================

NÃO fazer:

develop → hml

NÃO fazer:

hml → main

NÃO criar release.

NÃO executar Requirements ainda.

NÃO executar Architecture.

NÃO criar código funcional.

NÃO modificar OBS.

Advanced Skills permanece:

BLOCKED

==================================================
14. RESULTADO FINAL
==================================================

Retorne:

OBS-AI-Live-Assistant

TASK:
organize-prompt-history

TASK STATUS:
COMPLETED / BLOCKED

CODE REVIEW:
APPROVED / BLOCKED

Critical Findings:
<quantidade>

High Findings:
<quantidade>

Medium Findings:
<quantidade>

Low Findings:
<quantidade>

Prompt Archived:
YES / NO

Prompt File:
docs/prompts/history/promptN.md

Governance:
UPDATED / BLOCKED

Feature Commit:
<hash>

Feature Push:
OK / BLOCKED

Pull Request:
CREATED / BLOCKED

PR Number:
<número>

PR URL:
<url>

PR Validation:
PASSED / BLOCKED

Merge to Develop:
COMPLETED / BLOCKED

Develop Local:
<hash>

Develop Remote:
<hash>

Develop Synchronized:
YES / NO

Feature Local:
DELETED / RETAINED

Feature Remote:
DELETED / RETAINED

Working Tree:
CLEAN / DIRTY

main:
UNCHANGED

hml:
UNCHANGED

Advanced Skills:
BLOCKED

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

Depois de concluir o merge e validar develop:

STOP.

Não promover para hml.
Não promover para main.
Não executar Requirements nesta tarefa.
