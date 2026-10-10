# Prompt operacional — TASK-024

Registro integral do prompt autorizador da execução documental.

```text
# OBS-AI-LIVE-ASSISTANT — TASK-024
## EXECUÇÃO DOCUMENTAL CONTROLADA VIA GITFLOW

Projeto:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

Executor: Codex
Idioma: Português do Brasil

## OBJETIVO

Aplicar exclusivamente as alterações documentais aprovadas para preparar a TASK-024 — Criar skeleton e lifecycle do plugin OBS.

Executar em branch isolada baseada em origin/develop, seguir os Quality Gates aplicáveis e preparar PR para develop.

Não implementar código do plugin nesta etapa.

## 1. PREPARAÇÃO E SEGURANÇA

Antes de modificar qualquer arquivo:

1. Ler AGENTS.md, CLAUDE.md e governança GitFlow.
2. Confirmar a revisão atual de origin/develop.
3. Identificar as alterações preexistentes no worktree principal.
4. Não tocar nos arquivos do GitHub Project Sync.
5. Preservar a quarentena TASK-013.
6. Preservar TASK-014, TASK-015 e TASK-016, especialmente seus sete arquivos C#.
7. Não executar cleanup de worktrees antigos.

Criar um ambiente de trabalho isolado, somente se permitido pelo ambiente e pela governança.

Branch sugerida:

feature/task-TASK-024-native-readiness-docs

Utilizar origin/develop como base.

Se não for possível criar o ambiente isolado sem afetar trabalho existente ou houver bloqueio de permissões Git, interromper e apresentar o motivo.

Não alterar o worktree principal.

## 2. DOCUMENTAÇÃO ARQUITETURAL

Arquivo:

agent_docs/architecture.md

Reconciliar o texto com:

- ADR-001: integração híbrida aprovada.
- ADR-003: Assistant Core em processo separado.
- ADR-010: compatibilidade versionada.

Substituir a decisão ampla pendente de divisão Native/WebSocket pelas decisões específicas ainda abertas:

- Capabilities e eventos frontend exigidos por Task.
- Toolchain e versão OBS efetivamente validadas.

Preservar as decisões pendentes sobre Dock e IPC.

Acrescentar explicitamente que Dock, áudio e IPC pertencem às Tasks correspondentes e não fazem parte automaticamente da TASK-024.

Não modificar o status de ADRs existentes.

## 3. PESQUISA TÉCNICA

Arquivo:

docs/research/OBS_PLUGIN_RESEARCH.md

Registrar:

- OBS 32.1.2 x64 como baseline mínima proposta.
- OBS 32.2.2 x64 como alvo adicional.
- Referências aos presets oficiais CMake.
- Estratégia inicial com Visual Studio 2026.
- VS 2022 como fallback condicionado a incompatibilidade demonstrada.
- Template obsproject/obs-plugintemplate.
- Commit candidato 3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc.
- Dependências oficiais, versões e hashes documentados no relatório técnico.
- Pendência de aquisição e extração dos sources no Windows.
- Divergência da identificação do Windows.

Distinguir claramente:

SOURCE VERIFIED
PROPOSED
BUILD NOT VALIDATED
COMPATIBILITY NOT VERIFIED

Não inventar hashes ou resultados de build.

## 4. TASK CANÔNICA

Arquivo:

tasks/backlog/TASK-024-criar-skeleton-e-lifecycle-do-plugin-obs.md

Refinar a implementação esperada:

- Plugin C++ x64 mínimo.
- Load/unload seguro.
- Health somente em memória.
- FINISHED_LOADING obrigatório.
- STREAMING_STARTED/STOPPED limitados a estado em memória.
- Callbacks curtos e não bloqueantes.
- Logs sem dados sensíveis.
- Nenhuma dependência de IA, providers, banco, rede ou secrets.
- Sem Qt, Dock, IPC ou áudio.

Refinar critérios de aceite:

- Configure CMake reproduzível.
- Build x64.
- Testes de lifecycle.
- Registro e remoção de callbacks.
- Tratamento seguro de falhas.
- Smoke test isolado por versão OBS.
- Compatibilidade declarada somente para versões efetivamente aprovadas nos testes.
- Instalação principal e perfil real do OBS preservados.

Manter:

Status: BACKLOG

Não marcar DONE.

Não alterar Definition of Ready ou Definition of Done.

## 5. NOVO DOCUMENTO NATIVE BUILD GATE

Criar:

docs/testing/NATIVE_BUILD_GATE.md

Incluir:

1. Objetivo e limites do Gate.
2. Pré-requisitos Windows.
3. Matriz OBS 32.1.2/32.2.2.
4. Template, revisão e dependências.
5. Verificação SHA-256.
6. Procedimento futuro de aquisição e extração.
7. Presets CMake planejados.
8. Configure/build/test.
9. Smoke test com OBS portátil isolado.
10. Critérios de segurança.
11. Evidências exigidas por versão.
12. Condições de PASSED/BLOCKED.
13. Procedimento de limpeza apenas dos artefatos temporários criados pela própria execução futura.

Não executar build.

Não instalar dependências.

Não criar binários.

Não modificar o quality runner .NET nesta etapa.

## 6. VALIDAÇÃO

Após editar os documentos:

- Conferir diffs.
- Validar Markdown.
- Verificar links e referências.
- Confirmar consistência com ADRs.
- Confirmar ausência de alterações fora dos quatro arquivos autorizados.
- Executar Quality Gates documentais aplicáveis.
- Verificar que nenhum código-fonte foi modificado.
- Verificar ausência de segredos.

Não declarar Native Build Gate PASSED.

Não promover TASK-024 para READY.

## 7. GITFLOW

Se a governança e os gates permitirem:

1. Criar commit documental na feature isolada.
2. Realizar push da feature.
3. Criar PR para develop, com descrição em português.
4. Incluir critérios, decisões, evidências e limitações no PR.
5. Aguardar revisão/aprovação exigida pela governança.

Não realizar merge automaticamente sem a aprovação aplicável.

Não modificar main, hml ou releases.

Não excluir worktrees ou branches nesta execução.

## 8. RELATÓRIO FINAL

Entregar:

OBS-AI-LIVE-ASSISTANT
TASK-024 DOCUMENTATION EXECUTION REPORT

Branch:
Base Commit:
Worktree:
Files Created:
Files Modified:
Documentation Validation:
Quality Gates:
Security Validation:
Commit:
Push:
Pull Request:
Review Status:

TASK-024 Status:
BACKLOG

Native Build Gate:
NOT EXECUTED

Definition of Ready:
PENDING FORMAL REVIEW

Protected Worktrees:
PRESERVED

Git Cleanup:
SUSPENDED

Next Action:
VALIDATE TOOLCHAIN AND SOURCE ACQUISITION

Final Status:
PR READY FOR REVIEW / BLOCKED

STOP

## REGRA FINAL

Não retomar problemas antigos do Git.

Não modificar a instalação do OBS Studio.

Não iniciar implementação C++.

Não alterar os estados canônicos das Tasks.

Concluir exclusivamente a preparação documental e apresentar o resultado.
```

## Autorização posterior

O cliente autorizou este quinto arquivo obrigatório, determinou usar o próximo número sequencial livre sem sobrescrever histórico e limitou o conjunto final aos quatro documentos solicitados mais este registro. A autorização não altera escopo técnico, estado da TASK-024 ou restrições da execução.
