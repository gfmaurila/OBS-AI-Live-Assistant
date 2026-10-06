AUTORIZAÇÃO — ORGANIZAR E PADRONIZAR HISTÓRICO DE PROMPTS

Projeto:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

==================================================
REGRA PERMANENTE DO PROJETO
==================================================

A partir desta tarefa, TODO prompt operacional executado
para alterar, configurar, documentar ou evoluir este projeto
deverá ser arquivado ao final da própria execução.

Local oficial:

docs/prompts/history/

O histórico deverá permanecer versionado no Git.

Toda alteração autorizada no projeto deverá obrigatoriamente
terminar com:

1. validação das alterações;
2. atualização do histórico do prompt;
3. git add;
4. commit;
5. push;

sempre na branch correspondente à própria tarefa.

Não deixar alteração autorizada sem commit e push.

Exceção:

se a execução falhar antes de produzir qualquer alteração
válida, não criar commit artificial apenas para registrar
falha.

==================================================
1. GITFLOW
==================================================

A branch atualmente reportada é:

hml

NÃO alterar hml diretamente.

Execute fetch e valide o repositório.

Mude para:

develop

Garanta que:

develop = origin/develop

Crie a partir de develop:

feature/task-organize-prompt-history

Toda alteração desta tarefa deverá ocorrer exclusivamente
nessa branch.

==================================================
2. HISTÓRICO EXISTENTE
==================================================

Atualmente existe a pasta:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant\OBS-AI-Live-Assistant

contendo o histórico:

prompt.md
prompt2.md
prompt3.md
prompt4.md
prompt5.md
prompt6.md
prompt7.md

Esses arquivos foram previamente analisados e:

Secrets Detected: NO

Eles pertencem ao histórico operacional do projeto.

==================================================
3. DESTINO OFICIAL
==================================================

Criar:

docs/prompts/history/

Mover integralmente:

prompt.md
prompt2.md
prompt3.md
prompt4.md
prompt5.md
prompt6.md
prompt7.md

para:

docs/prompts/history/

Não alterar o conteúdo histórico desses arquivos.

==================================================
4. PADRÃO PARA NOVOS PROMPTS
==================================================

A partir desta tarefa, novos prompts deverão ser registrados
diretamente em:

docs/prompts/history/

Não utilizar novamente:

OBS-AI-Live-Assistant/OBS-AI-Live-Assistant/

como destino intencional do histórico.

Para novos arquivos, utilizar numeração sequencial.

Como prompt7.md já existe, este prompt deverá ser registrado
como:

docs/prompts/history/prompt8.md

Os próximos deverão seguir:

prompt9.md
prompt10.md
prompt11.md
...

Nunca sobrescrever um prompt histórico existente.

Antes de escolher o próximo número:

1. listar os prompt*.md existentes;
2. identificar o maior número;
3. utilizar o próximo número disponível.

==================================================
5. REGISTRAR ESTE PRÓPRIO PROMPT
==================================================

IMPORTANTE:

Ao final desta execução, salvar uma cópia integral deste
prompt operacional como:

docs/prompts/history/prompt8.md

Esse arquivo passa a fazer parte desta mesma alteração.

Não criar uma segunda task apenas para registrar o prompt.

Ele deverá entrar no mesmo commit e no mesmo push.

==================================================
6. README DO HISTÓRICO
==================================================

Criar:

docs/prompts/README.md

Em inglês técnico, documentar que:

- docs/prompts/history contains the chronological history
  of operational prompts used during the project;

- prompts are maintained for traceability and auditing;

- prompts are NOT automatically approved requirements;

- prompts are NOT automatically architectural decisions;

- canonical Requirements are produced and approved through
  the Requirements process;

- canonical architectural decisions are maintained through
  Architecture documentation and ADRs;

- newer canonical documentation takes precedence over
  historical prompts;

- prompts must never contain secrets, credentials,
  passwords, tokens or API keys;

- each project-changing operational prompt must be archived
  before the task commit;

- the archived prompt must be included in the same commit
  and push as the changes produced by that prompt.

==================================================
7. GOVERNANCE
==================================================

Atualize a documentação de governança apropriada já
existente no projeto para registrar a nova regra:

PROMPT TRACEABILITY RULE

Para toda tarefa que produza alteração no repositório:

Prompt
  ↓
Execution
  ↓
Validation
  ↓
Archive Prompt
  ↓
Secret Check
  ↓
git add
  ↓
Commit
  ↓
Push

O prompt utilizado deve fazer parte do mesmo commit da
alteração correspondente.

Não duplicar regras desnecessariamente em vários documentos.

Escolha o documento de governança canônico apropriado.

==================================================
8. COMMIT/PUSH RULE
==================================================

Registrar também na governança:

Toda alteração autorizada no repositório deve ser
comitada e enviada ao remoto na branch da própria tarefa.

Não terminar uma tarefa modificadora deixando arquivos
intencionalmente sem commit.

Não terminar uma tarefa modificadora sem push, salvo:

- falha de autenticação;
- indisponibilidade do remoto;
- conflito;
- erro técnico que impeça o push.

Nesses casos:

reportar explicitamente:

COMMIT: OK/BLOCKED
PUSH: BLOCKED
REASON: <motivo>

Nunca fazer push para outra branch para contornar o erro.

==================================================
9. PASTA DUPLICADA
==================================================

Após mover os prompts existentes, verifique:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant\OBS-AI-Live-Assistant

Se estiver completamente vazia:

remova a pasta.

Se algum processo externo criar novo prompt durante esta
execução:

NÃO apagar o novo arquivo.

Identifique-o.

Mova-o também para:

docs/prompts/history/

preservando a ordem cronológica.

Evite colisão de nomes.

Reporte que o comportamento externo continua ocorrendo.

==================================================
10. SEGURANÇA
==================================================

Antes do commit:

verifique TODO conteúdo novo/modificado que será versionado.

Não permitir:

API keys
passwords
access tokens
refresh tokens
credentials
private keys
connection strings com secrets
.env com secrets

O histórico de prompts também está sujeito a essa regra.

Se um futuro prompt contiver secret:

NÃO arquivar o valor do secret no Git.

Interromper e reportar o problema para tratamento seguro.

==================================================
11. VALIDAÇÃO
==================================================

Antes do commit, executar:

git status
git diff
git diff --cached

Validar:

- prompts históricos preservados;
- prompt desta tarefa arquivado;
- README criado;
- governança atualizada;
- pasta duplicada tratada;
- nenhum código funcional criado;
- nenhum secret presente;
- OBS não modificado.

==================================================
12. COMMIT
==================================================

Adicionar somente as alterações pertencentes a esta tarefa.

Commit obrigatório.

Utilizar Conventional Commits:

docs: establish prompt history and traceability

O commit deverá incluir:

- docs/prompts/history/prompt*.md;
- docs/prompts/README.md;
- atualização de governança;
- demais alterações documentais estritamente necessárias
  para esta tarefa.

==================================================
13. PUSH
==================================================

Push obrigatório para:

origin/feature/task-organize-prompt-history

Configurar upstream se necessário.

Validar que o commit local está presente no remoto.

==================================================
14. NÃO FAZER MERGE
==================================================

Commit + push são obrigatórios.

Merge NÃO está autorizado nesta tarefa.

NÃO fazer merge em develop.

NÃO fazer merge em hml.

NÃO fazer merge em main.

NÃO criar release.

NÃO criar código funcional.

NÃO executar Knowledge Quality Gate.

NÃO executar Requirements.

NÃO executar Architecture.

==================================================
15. RESULTADO
==================================================

Retorne:

OBS-AI-Live-Assistant

PROMPT HISTORY ORGANIZATION:
COMPLETED / BLOCKED

Prompt Traceability Rule:
OK / BLOCKED

Commit/Push Rule:
OK / BLOCKED

Historical Prompts Migrated:
<quantidade>

Current Prompt Archived:
YES / NO

Current Prompt File:
docs/prompts/history/prompt8.md
ou número efetivamente utilizado

README:
OK / BLOCKED

Governance:
UPDATED / BLOCKED

Secrets:
NONE / DETECTED

Duplicate Directory:
REMOVED / RETAINED / RECREATED_EXTERNALLY

Branch:
feature/task-organize-prompt-history

Commit:
<HASH / BLOCKED>

Push:
OK / BLOCKED

Remote Validation:
OK / BLOCKED

Working Tree:
CLEAN / DIRTY

Advanced Skills:
BLOCKED

Product Source Code:
NOT CREATED

Implementation:
NOT STARTED

OBS Environment:
NOT MODIFIED

NEXT:
REVIEW
+
PR TO DEVELOP
+
RESOLVE ADVANCED SKILLS

==================================================
STOP
==================================================

Após COMMIT + PUSH:

STOP.

Não fazer merge.
Não executar Requirements.
Não executar Architecture.

Aguardar autorização explícita.
