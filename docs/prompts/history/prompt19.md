# Prompt 19 — Autorização para executar TASK-003 (2026-10-07)

Autorização do cliente para executar integralmente a **TASK-003 — Validar a estratégia de compatibilidade**, do setup até o merge automático em `develop`, usando como fonte canônica `tasks/ready/TASK-003-validar-a-estratégia-de-compatibilidade.md`.

## Condições de execução

1. Executar a Task integralmente, em um único fluxo contínuo: setup → definição do escopo → implementação → testes → gates → Code Review → correções → Secret Scan → Prompt History → commit → push → PR → validação → merge → validação pós-merge → relatório final.
2. Não solicitar confirmação intermediária enquanto os gates aplicáveis estiverem verdes; retomar automaticamente do último checkpoint verificado caso a sessão seja interrompida.
3. Trabalhar somente na branch `feature/task-TASK-003-validar-compatibilidade`, criada a partir de `develop` sincronizada.
4. Não modificar `develop`, `hml` nem `main` diretamente; não executar promoções para `hml`, `release/1.0.0XXXX` ou `main`; não criar tag nem GitHub Release.
5. Não modificar diretórios do OBS (`C:\Program Files\obs-studio` e `%APPDATA%\obs-studio`); acesso somente leitura para evidência.
6. **Não executar a TASK-004** nesta autorização; apenas registrar o estado pós-Task.
7. Não adicionar pacotes, projetos ou infraestrutura sem necessidade; não ampliar o escopo além da matriz de compatibilidade, do protótipo de evidência e da ADR-010.
8. Aplicar os workarounds já validados em Tasks anteriores (lock transitório MSB4018 de `.deps.json` causado por sincronização; normalização de fim de linha CRLF/LF após checkout) e registrar apenas se reincidirem.
9. Registrar gates com evidência real (exit codes e contagens), sem alegar sucesso sem execução.
10. Arquivar este prompt em `docs/prompts/history/` com o próximo número sequencial disponível, no mesmo commit das alterações (Prompt Traceability), executar Secret Check e nunca sobrescrever prompt histórico.
11. O PR para `develop` deve ter descrição e corpo em pt-BR, com diff dentro do escopo, sem secrets e sem dependência de CI (CI inexistente); o merge deve usar `merge` (não squash nem rebase).
12. Após o merge: validar `develop` local/remoto sincronizados, working tree limpo, excluir a branch de feature local e remota e entregar o relatório final no formato obrigatório abaixo.
13. Parar completamente (STOP) após o relatório final desta Task; não iniciar nenhuma outra Task.

## Relatório final obrigatório (§28)

O relatório de TASK-003 deve conter, na ordem: TASK; TASK STATUS; IA; Execução integral; DoR; Implementação; Escopo; Projetos alterados; .NET SDK; Restore; Build; Build Warnings/Errors; Tests (anteriores/novos/total); Architecture Gate; Security Gate; Acceptance Criteria; DoD; Code Review; Critical/High/Medium/Low Findings; Secrets; Prompt arquivado; Prompt path; Branch; Commit; Push; Pull Request; PR Number; PR Validation; Merge para develop; Merge Commit; Develop local/remoto/sincronizada; Feature local/remota; Working Tree; TASK-003; TASK-004 NÃO EXECUTADA; Novas Tasks READY; Tasks BLOCKED; hml/Release/main/OBS; PRÓXIMO; STOP.

## Condição final (§29)

NÃO parar após build/testes: prosseguir até Code Review, correções, Secret Scan, Prompt History, commit, push, PR, validação, merge e validação pós-merge sem confirmação intermediária quando os gates estiverem verdes, e então entregar o relatório e STOP.