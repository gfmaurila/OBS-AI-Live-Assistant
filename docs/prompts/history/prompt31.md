# PROMPT OPERACIONAL — CONSOLIDAÇÃO LOCAL DAS TASK-013, TASK-014 E TASK-015

**Projeto:** OBS-AI-Live-Assistant
**Tasks autorizadas:** TASK-013, TASK-014 e TASK-015 (consolidação local)
**Modo:** 100% local, sem GitHub remoto
**Data:** 2026-10-09

> Texto verbatim do prompt de consolidação recebido pelo agente, arquivado para cumprir o Gate de Prompt Traceability.

---

Continue exclusivamente a **consolidação local das TASK-013, TASK-014 e TASK-015** do projeto OBS-AI-Live-Assistant, exatamente do ponto em que a execução foi interrompida pelo limite de uso.

**Não reinicie o processo e não descarte nenhum trabalho existente.**

### 1. Recuperação segura

Antes de qualquer alteração:

- Leia `AGENTS.md`, `CLAUDE.md` e as regras de governança.
- Execute `git status`, `git branch -vv`, `git log` e `git worktree list --porcelain`.
- Identifique a branch e o worktree de integração existentes.
- Verifique quais commits já foram integrados e se existe algum merge em andamento.
- Preserve todos os arquivos modificados e não rastreados.
- Preserve integralmente as branches e worktrees originais das três Tasks.
- Não recrie a branch de integração se ela já existir.

Commits originais:

- TASK-013: `91de8d93ff50a26480317a574178a4d8c1b1fcc8`
- TASK-014: `78da119434965b49f3846b48e633d5df068a0e0f`
- TASK-015: `ce9688b32159b7e534a14d636e07945ee772c82a`

### 2. Continuar a integração

Retome o plano original:

1. Concluir a inspeção dos contratos e das alterações.
2. Integrar localmente os três incrementos, sem duplicar commits.
3. Resolver conflitos preservando todas as funcionalidades e evidências.
4. Reconciliar `BACKLOG.md`, `DEPENDENCY_GRAPH.md`, `IMPLEMENTATION_ORDER.md`, `TRACEABILITY_MATRIX.md`, relatórios e índices.
5. Preservar integralmente os históricos de prompts.
6. Executar testes consolidados.
7. Executar `.\tooling\quality-gates.ps1`.
8. Executar Architecture Gate, Security Audit, Code Review e Secret Scan.
9. Criar o relatório de integração local.
10. Criar commit local das correções e reconciliações, se necessário.
11. Confirmar worktree limpo e as três branches originais preservadas.

### 3. Restrições

- Não executar nenhuma nova Task.
- Não modificar `develop`, `main` ou `hml`.
- Não executar `git fetch`, `git pull`, `git push` ou comandos `gh`.
- Não acessar GitHub Issues ou Project #22.
- Não criar Pull Requests.
- Não executar merge remoto.
- Não modificar releases ou OBS Studio.
- Não executar `git reset --hard`, `git clean -fd` ou limpeza destrutiva.
- Não marcar TASK-013, TASK-014 ou TASK-015 como DONE definitivo enquanto a DoD exigir integração remota.

### 4. Relatório final

Informar:

- Branch e worktree de integração.
- Commits integrados.
- Conflitos encontrados e resolvidos.
- Total de testes consolidados.
- Resultado dos Quality Gates.
- Architecture Gate.
- Security Audit.
- Code Review.
- Secret Scan.
- Arquivos de documentação reconciliados.
- Hash do commit final local.
- Estado do working tree.
- Confirmação da preservação das branches originais.

Estado esperado, se todas as validações passarem:

**LOCAL INTEGRATION COMPLETE — PENDING REMOTE INTEGRATION**

Após o relatório, **STOP**.
