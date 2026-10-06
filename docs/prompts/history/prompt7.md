EXECUTAR SOMENTE DIAGNÓSTICO DO WORKING TREE.

Projeto:

D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

Foi identificado:

Working Tree: DIRTY

Existe aparentemente um diretório não rastreado:

OBS-AI-Live-Assistant/

contendo:

prompt.md
prompt2.md
prompt3.md
prompt4.md
prompt5.md
prompt6.md

A branch atualmente ativa também foi reportada como:

hml

==================================================
OBJETIVO
==================================================

Descobrir exatamente:

1. onde essa pasta está fisicamente;
2. por que existe uma pasta OBS-AI-Live-Assistant dentro
   da própria raiz OBS-AI-Live-Assistant;
3. quais arquivos ela contém;
4. se contém somente prompt.md...prompt6.md;
5. tamanho e data dos arquivos;
6. se esses arquivos são documentação útil;
7. se algum deles contém secrets ou credenciais;
8. por que estão untracked;
9. confirmar a branch ativa;
10. confirmar se main/develop/hml continuam sincronizadas
    com o bootstrap já publicado.

==================================================
IMPORTANTE
==================================================

SOMENTE LEITURA.

NÃO apagar a pasta.

NÃO mover arquivos.

NÃO adicionar ao Git.

NÃO executar git add.

NÃO executar commit.

NÃO executar push.

NÃO executar checkout/switch.

NÃO alterar branch.

NÃO editar .gitignore.

NÃO modificar nenhum prompt.

NÃO executar Requirements.

NÃO executar Architecture.

NÃO instalar Skills.

==================================================
COMANDOS/VALIDAÇÕES
==================================================

Inspecione:

git status --short
git status
git branch --show-current
git branch -vv
git log --oneline --decorate --all -10

Identifique também a raiz real:

git rev-parse --show-toplevel

Inspecione recursivamente SOMENTE a pasta não rastreada.

Liste:

- caminho absoluto;
- estrutura;
- arquivos;
- tamanho;
- datas;
- status Git.

Faça uma análise somente leitura do conteúdo dos arquivos:

prompt.md
prompt2.md
prompt3.md
prompt4.md
prompt5.md
prompt6.md

Para cada um informe apenas:

- finalidade aparente;
- assunto;
- se pertence ao OBS-AI-Live-Assistant;
- se parece documentação histórica, prompt operacional,
  configuração ou outro tipo;
- se contém possível secret/credencial;
- recomendação:
  KEEP / MOVE / IGNORE / DELETE / REQUIRES_CLIENT_DECISION.

NÃO execute a recomendação.

==================================================
ADVANCED SKILLS
==================================================

Manter:

Advanced Skills: BLOCKED

O pacote oficial:

Upsell1-Kit-IA-Dev

ou:

Kit-IA-Dev-Skills-Avancadas.zip

não foi localizado.

Não tentar recriá-lo.

==================================================
RESULTADO
==================================================

Retorne:

REPOSITORY DIAGNOSTIC

Git Root:
<caminho>

Current Branch:
<branch>

Working Tree:
CLEAN / DIRTY

Untracked Directory:
<caminho absoluto>

Nested Project Directory:
YES / NO

Files Found:
<quantidade>

prompt.md:
<análise>

prompt2.md:
<análise>

prompt3.md:
<análise>

prompt4.md:
<análise>

prompt5.md:
<análise>

prompt6.md:
<análise>

Secrets Detected:
YES / NO

main:
<hash>

develop:
<hash>

hml:
<hash>

origin/main:
<hash>

origin/develop:
<hash>

origin/hml:
<hash>

Branches Synchronized:
YES / NO

Advanced Skills:
BLOCKED

RECOMMENDED ACTION:
<sem executar>

==================================================
STOP
==================================================

STOP após o diagnóstico.

Não faça nenhuma alteração.
Aguarde autorização explícita.