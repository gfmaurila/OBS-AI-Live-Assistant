# PROMPT OPERACIONAL — CONTINUAÇÃO LOCAL DA TASK-014

**Projeto:** OBS-AI-Live-Assistant
**Task autorizada:** TASK-014
**Modo:** 100% local, sem GitHub remoto
**Data:** 2026-10-09

> Texto verbatim do prompt de continuação recebido pelo agente, arquivado para cumprir o Gate de Prompt Traceability.

---

Continue exclusivamente a TASK-014 do projeto OBS-AI-Live-Assistant, exatamente do ponto em que a execução foi interrompida pelo limite de uso.

**Modo obrigatório: 100% local, sem GitHub remoto.**

### 1. Recuperação segura

Antes de alterar qualquer arquivo:

- Leia AGENTS.md, CLAUDE.md, PROJECT_SKILLS.md e o arquivo canônico da TASK-014.
- Inspecione `git status`, branches, commits e worktrees.
- Localize e preserve o trabalho existente em `temp\task014`, verificando seu caminho absoluto e vínculo com o repositório.
- Preserve integralmente a implementação já realizada em `ObsAi.Application.Configuration`.
- Preserve os testes, documentos e alterações administrativas existentes.
- Não recrie a branch ou o worktree se já existirem.
- Não descartar arquivos não rastreados ou alterações não commitadas.
- Não executar `git reset --hard`, `git clean -fd` ou limpeza destrutiva.

### 2. Estado anterior conhecido

O último log confirmou:

- Build: 14 projetos, 0 erros, 0 avisos.
- Testes Unit: 150 aprovados.
- Documentação de arquitetura e segurança criada.
- Relatório TASK-014_REPORT.md iniciado.
- Backlog e rastreabilidade atualizados.
- TASK-014 movida para `tasks/in-progress/`.

Esses resultados são parciais e precisam ser confirmados.

### 3. Continuar a execução

Finalize apenas o que estiver pendente:

1. Revisar a implementação e os critérios de aceite canônicos da TASK-014.
2. Completar os testes necessários.
3. Verificar a consistência de `ConfigurationPolicy`, snapshots e validações.
4. Executar o conjunto completo de Quality Gates usando `.\tooling\quality-gates.ps1`.
5. Executar Architecture Gate.
6. Executar Security Audit.
7. Realizar Code Review e corrigir findings bloqueantes.
8. Executar Secret Scan.
9. Finalizar documentação, `TRACEABILITY_MATRIX.md`, backlog e relatório técnico.
10. Verificar a consistência das alterações administrativas, preservando a distinção entre implementação concluída e integração pendente.
11. Arquivar integralmente o prompt original da TASK-014 no próximo arquivo histórico disponível, sem sobrescrever outros prompts.
12. Executar `git diff --check` e inspecionar todos os arquivos staged e untracked.
13. Criar Conventional Commit exclusivamente local, incluindo somente os arquivos da TASK-014.
14. Confirmar que o worktree da TASK-014 esteja limpo.

### 4. Restrições obrigatórias

- Não executar TASK-010, TASK-011, TASK-012, TASK-013, TASK-015 ou qualquer outra Task.
- Preservar a branch e o worktree da TASK-013, incluindo seu commit `91de8d93ff50a26480317a574178a4d8c1b1fcc8`.
- Não acessar GitHub Issues ou Project #22.
- Não executar `gh`, `git fetch`, `git pull` ou `git push`.
- Não criar PR nem realizar merge.
- Não modificar `main`, `hml` ou releases.
- Não modificar a instalação ou as configurações do OBS Studio.
- Não mover TASK-014 para `tasks/done/` se a Definition of Done exigir integração e validação pós-merge.

### 5. Relatório final

Apresente:

- Definition of Ready.
- Critérios de aceite.
- Implementação concluída.
- Testes totais, aprovados e falhos.
- Restore, Build e Format.
- Architecture Gate.
- Security Audit.
- Code Review.
- Secret Scan.
- Documentação.
- Arquivo do prompt history.
- Branch e worktree locais.
- Hash completo do commit local.
- Estado final da TASK-014.
- Confirmação de preservação da TASK-013.
- Confirmação de que nenhuma outra Task foi executada.

Se todos os critérios técnicos forem aprovados, registrar:

`TASK-014: IMPLEMENTATION COMPLETE — PENDING INTEGRATION`

Entregar relatório e **STOP**.
