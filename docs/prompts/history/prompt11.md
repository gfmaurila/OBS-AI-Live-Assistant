AUTORIZAÇÃO — AJUSTAR IDIOMA DA DOCUMENTAÇÃO E FLUXO DE RELEASE

Projeto:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

==================================================
OBJETIVO
==================================================

Antes de executar o Knowledge Quality Gate, atualizar a
governança oficial do projeto com duas novas regras:

1. documentação funcional, README e descrição das Tasks
   devem ser escritos em português do Brasil;

2. o fluxo oficial de entrega deve contemplar:

feature/task-*
→ develop
→ hml
→ release/1.0.0XXXX
→ main

Esta task é exclusivamente documental e de governança.

NÃO executar Knowledge Quality Gate.
NÃO executar Requirements.
NÃO executar Architecture.
NÃO implementar código.

==================================================
1. GITFLOW DA PRÓPRIA TASK
==================================================

Executar:

git fetch origin
git switch develop
git pull --ff-only origin develop

Validar:

Working Tree = CLEAN

Criar:

feature/task-ajustar-governanca-release-idioma

Toda alteração desta task deve ocorrer nessa branch.

==================================================
2. REGRA DE IDIOMA
==================================================

Registrar na documentação canônica do projeto:

IDIOMA OFICIAL DA DOCUMENTAÇÃO:
Português do Brasil (pt-BR)

Devem ser escritos prioritariamente em português:

- README.md da raiz;
- docs/README.md;
- documentação de projeto;
- descrição das Tasks;
- documentação de Requirements;
- documentação de Architecture;
- documentação de Research;
- documentação de Security;
- documentação de Testing;
- documentação de Reports;
- documentação de Governance;
- documentação de Knowledge;
- descrições de Pull Requests;
- relatórios de execução;
- documentação destinada à leitura humana.

==================================================
3. EXCEÇÕES DE IDIOMA
==================================================

Não traduzir artificialmente elementos técnicos que devem
permanecer em inglês.

Podem permanecer em inglês:

- código-fonte;
- nomes de classes;
- nomes de interfaces;
- nomes de métodos;
- nomes de propriedades;
- namespaces;
- nomes de projetos;
- nomes de arquivos técnicos quando fizer sentido;
- comandos;
- nomes de branches;
- Conventional Commits;
- identificadores;
- APIs;
- protocolos;
- bibliotecas;
- nomes oficiais de tecnologias;
- termos técnicos consagrados quando a tradução prejudicar
  clareza;
- palavras-chave estruturais exigidas por ferramentas.

Exemplos:

feature/task-*
release/*
main
develop
hml
git
commit
push
pull request
Code Review
Quality Gate
OBS WebSocket
SQLite
TTS
BYOK
IPC
.NET
C#
C++

podem permanecer com seus nomes técnicos.

==================================================
4. AGENTS / SKILLS
==================================================

Não traduzir automaticamente:

AGENTS.md
CLAUDE.md
SKILL.md

quando sua estrutura ou instruções técnicas dependam do
formato atual para Claude, Codex, Copilot ou outras
ferramentas.

A regra de pt-BR aplica-se principalmente à documentação
humana do projeto.

Não quebrar instruções de ferramentas apenas para cumprir
tradução.

==================================================
5. DOCUMENTAÇÃO EXISTENTE
==================================================

Revisar a documentação criada na task:

project-documentation-baseline

Identificar documentos humanos que estejam integralmente
em inglês.

Converter para português do Brasil quando apropriado.

Preservar:

- significado;
- estados;
- classificações;
- referências;
- links;
- estrutura;
- termos técnicos;
- rastreabilidade.

Não alterar decisões apenas por causa da tradução.

==================================================
6. TASKS EM PORTUGUÊS
==================================================

Registrar como regra:

Toda Task deve possuir descrição humana em português do
Brasil.

Exemplo:

TASK-001

Título:
Pesquisar estratégia de integração com OBS Studio

Descrição:
Avaliar as opções de integração nativa, OBS WebSocket e IPC
para determinar quais capacidades deverão ser utilizadas
pelo OBS-AI-Live-Assistant.

Critérios de aceite:
- opções documentadas;
- riscos identificados;
- recomendação registrada;
- necessidade de ADR identificada.

Os identificadores técnicos podem permanecer:

TASK-001
TASK-002
TASK-003

==================================================
7. GITFLOW OFICIAL
==================================================

Atualizar:

docs/governance/GITFLOW.md

e demais documentos canônicos relacionados.

Fluxo oficial:

feature/task-*
      ↓
    develop
      ↓
      hml
      ↓
release/1.0.0XXXX
      ↓
     main

==================================================
8. FEATURE → DEVELOP
==================================================

Cada Task deve nascer de:

develop

Formato:

feature/task-<descricao>

Ao concluir:

Task
↓
Validation
↓
Code Review
↓
Prompt Archive
↓
Secret Check
↓
Commit
↓
Push
↓
PR
↓
Merge → develop

Se todos os gates estiverem verdes, o merge da feature para
develop pode ocorrer automaticamente.

==================================================
9. DEVELOP → HML
==================================================

develop representa integração contínua das Tasks aprovadas.

A promoção para:

hml

não deve acontecer após cada Task individual.

A promoção deve ocorrer quando existir um conjunto coerente
de alterações pronto para homologação.

Fluxo:

develop
↓
Quality Gate
↓
PR
↓
hml

Registrar que hml representa:

HOMOLOGAÇÃO

==================================================
10. VERSIONAMENTO DE RELEASE
==================================================

Adotar branches:

release/1.0.0XXXX

Onde XXXX representa sequência incremental da release.

Exemplos:

release/1.0.00001
release/1.0.00002
release/1.0.00003
release/1.0.00004

IMPORTANTE:

Antes de consolidar esse formato, validar a convenção já
existente na governança do projeto.

Se houver convenção anterior compatível, adaptar sem criar
duplicação.

Não criar uma release real nesta task.

Apenas documentar o fluxo.

==================================================
11. HML → RELEASE
==================================================

Quando a versão homologada estiver aprovada:

hml
↓
release/1.0.0XXXX

A release deve ser criada a partir do estado aprovado de hml.

A branch release deve permitir somente:

- ajustes finais;
- correções de release;
- documentação;
- versionamento;
- metadados;
- correções bloqueadoras aprovadas.

Não desenvolver feature nova dentro de release/*.

==================================================
12. RELEASE → MAIN
==================================================

Após validação final:

release/1.0.0XXXX
↓
Final Quality Gate
↓
PR
↓
main

main representa:

PRODUÇÃO / VERSÃO ESTÁVEL

Nenhuma feature deve ser mergeada diretamente em main.

==================================================
13. PÓS-MERGE EM MAIN
==================================================

Documentar o fluxo esperado:

release/1.0.0XXXX
↓
PR
↓
main
↓
merge
↓
tag
↓
GitHub Release
↓
sincronização das branches necessárias

A criação efetiva de tag/GitHub Release somente ocorrerá
quando existir uma release real aprovada.

Não criar tag nesta task.

Não criar GitHub Release nesta task.

==================================================
14. SINCRONIZAÇÃO PÓS-RELEASE
==================================================

Documentar que alterações realizadas exclusivamente durante
release/* não podem ficar apenas em main.

Após release, avaliar sincronização de:

main
→ develop

e quando aplicável:

main
→ hml

para evitar divergência de correções/versionamento.

A estratégia exata deve preservar histórico e evitar perda
de alterações.

==================================================
15. QUALITY GATES DE RELEASE
==================================================

Preparar na governança gates conceituais para:

DEVELOPMENT GATE
HML GATE
RELEASE GATE
PRODUCTION GATE

Enquanto não houver implementação, itens como:

Build
Unit Tests
Integration Tests
Installer Tests
OBS Compatibility Tests
Regression Tests

devem permanecer:

NOT APPLICABLE UNTIL IMPLEMENTATION

Não marcar testes inexistentes como PASSED.

==================================================
16. EXECUTION PLAN
==================================================

Atualizar:

docs/governance/EXECUTION_PLAN.md

para refletir dois níveis distintos.

FLUXO DE TASK:

develop
↓
feature/task-*
↓
Review
↓
PR
↓
develop

FLUXO DE ENTREGA:

develop
↓
hml
↓
release/1.0.0XXXX
↓
main
↓
tag / GitHub Release

Não misturar Task com Release.

==================================================
17. PROMPT TRACEABILITY
==================================================

Arquivar este prompt integralmente em:

docs/prompts/history/promptN.md

Identificar o próximo número disponível.

Nunca sobrescrever histórico.

O prompt deve entrar no mesmo commit desta task.

==================================================
18. CODE REVIEW
==================================================

Executar Code Review documental.

Validar:

- documentação em pt-BR;
- consistência do GitFlow;
- consistência do release flow;
- links;
- ausência de contradições;
- ausência de duplicações;
- Prompt Traceability;
- secrets;
- escopo da task.

Critical e High findings devem ser corrigidos.

==================================================
19. COMMIT
==================================================

Toda alteração deve ser comitada.

Utilizar Conventional Commit.

Sugestão:

docs: atualiza governanca de idioma e release

Se a política atual exigir Conventional Commits em inglês,
utilizar:

docs: update language and release governance

==================================================
20. PUSH
==================================================

Push obrigatório:

origin/feature/task-ajustar-governanca-release-idioma

Validar:

local HEAD = remote HEAD

==================================================
21. PR → DEVELOP
==================================================

Criar Pull Request:

feature/task-ajustar-governanca-release-idioma
→
develop

Título e descrição humana do PR:

PORTUGUÊS DO BRASIL

Se todos os Quality Gates estiverem verdes:

MERGE AUTOMÁTICO PARA DEVELOP

==================================================
22. CLEANUP
==================================================

Após merge confirmado:

git switch develop
git pull --ff-only origin develop

Validar:

develop local = origin/develop

Excluir feature local.

Excluir feature remota.

Working Tree:

CLEAN

==================================================
23. IMPORTANTE — NÃO CRIAR RELEASE AGORA
==================================================

Esta task apenas DEFINE o fluxo de release.

NÃO criar:

release/1.0.00001

agora.

A primeira release real deverá ocorrer quando houver um
conjunto de alterações explicitamente aprovado para
homologação/entrega.

==================================================
24. NÃO EXECUTAR
==================================================

NÃO executar Knowledge Quality Gate.

NÃO executar Requirements.

NÃO executar Architecture.

NÃO executar Research.

NÃO criar código.

NÃO criar src/.

NÃO modificar OBS.

NÃO promover develop para hml nesta task.

NÃO criar release real.

NÃO alterar main.

==================================================
25. RESULTADO FINAL
==================================================

Retorne em português:

OBS-AI-Live-Assistant

TASK:
ajustar-governanca-release-idioma

STATUS:
CONCLUÍDA / BLOQUEADA

Idioma da documentação:
PT-BR / BLOQUEADO

README:
PT-BR / BLOQUEADO

Descrição das Tasks:
PT-BR / BLOQUEADO

GitFlow:
ATUALIZADO / BLOQUEADO

Fluxo de Task:
feature/task-* → develop

Fluxo de Homologação:
develop → hml

Fluxo de Release:
hml → release/1.0.0XXXX → main

Versionamento:
DOCUMENTADO / BLOQUEADO

Quality Gates:
ATUALIZADOS / BLOQUEADOS

Prompt arquivado:
SIM / NÃO

Arquivo do Prompt:
docs/prompts/history/promptN.md

Code Review:
APROVADO / BLOQUEADO

Critical Findings:
<quantidade>

High Findings:
<quantidade>

Secrets:
NENHUM / DETECTADO

Branch:
feature/task-ajustar-governanca-release-idioma

Commit:
<hash>

Push:
OK / BLOQUEADO

Pull Request:
CRIADO / BLOQUEADO

PR:
<número>

Merge para develop:
CONCLUÍDO / BLOQUEADO

Develop sincronizada:
SIM / NÃO

Feature local:
EXCLUÍDA / RETIDA

Feature remota:
EXCLUÍDA / RETIDA

Working Tree:
CLEAN / DIRTY

Knowledge Quality Gate:
NÃO EXECUTADO

Requirements:
NÃO INICIADO

Architecture:
NÃO INICIADA

Implementation:
NÃO INICIADA

OBS:
NÃO MODIFICADO

PRÓXIMO:
KNOWLEDGE QUALITY GATE

==================================================
STOP
==================================================

Após merge em develop:

STOP.

Não iniciar o Knowledge Quality Gate automaticamente.
