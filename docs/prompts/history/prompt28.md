# PROMPT OPERACIONAL — TASK-013
## OBS-AI-Live-Assistant | Codex | Execução local

**Projeto:** OBS-AI-Live-Assistant  
**Diretório:** `D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant`  
**Kit IA Dev:** `D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev`  
**Executor:** Codex  
**Idioma obrigatório:** Português do Brasil  
**Task exclusiva:** TASK-013 — Implementar timeout, cancellation, retry e erros normalizados  
**Modalidade:** Desenvolvimento local, sem operações remotas no GitHub.

---

## 1. OBJETIVO

Executar integralmente a implementação técnica da TASK-013, respeitando o arquivo canônico, a arquitetura e a governança do projeto.

Fluxo obrigatório:

**Setup → Discovery → Definition of Ready → Planejamento → Branch local → Implementação → Testes → Quality Gates → Architecture Gate → Security Audit → Code Review → Correções → Secret Scan → Documentação → Prompt History → Commit local → Relatório → STOP.**

Não executar outras Tasks.

Não utilizar GitHub Issues ou GitHub Projects.

Não solicitar autenticação GitHub.

Não executar push, criar PR ou realizar merge.

---

## 2. REGRAS ABSOLUTAS

É obrigatório:

1. Ler o setup e as instruções dos agentes.
2. Ler integralmente o arquivo canônico da TASK-013.
3. Validar a Definition of Ready.
4. Preservar o trabalho local existente.
5. Criar ou reutilizar uma branch local exclusiva.
6. Respeitar Requirements e ADRs aprovados.
7. Implementar somente os critérios de aceite.
8. Criar testes reais e determinísticos.
9. Executar Quality Gates.
10. Executar Architecture Gate.
11. Executar Security Audit.
12. Realizar Code Review.
13. Corrigir findings bloqueantes.
14. Executar Secret Scan.
15. Atualizar documentação e rastreabilidade.
16. Arquivar este prompt integralmente.
17. Criar Conventional Commit local.
18. Apresentar evidências e relatório final.
19. Parar após a conclusão.

É proibido:

- Executar TASK-010, TASK-011, TASK-012, TASK-014 ou qualquer outra Task.
- Antecipar funcionalidades de Tasks futuras.
- Alterar `main`, `hml` ou releases.
- Realizar deploy.
- Modificar a instalação, plugins ou configurações do OBS Studio.
- Desabilitar testes ou enfraquecer Quality Gates.
- Descartar trabalho existente.
- Executar `git reset --hard` ou `git clean -fd`.
- Fazer push ou qualquer operação de escrita remota.
- Declarar DONE sem cumprir a Definition of Done canônica.

---

## 3. INSPEÇÃO INICIAL

Execute:

```powershell
git branch --show-current
git status --short
git log -5 --oneline
git branch -vv
git worktree list --porcelain
```

Verifique se já existe trabalho da TASK-013, branch local ou execução interrompida.

Há uma execução administrativa anterior de sincronização com GitHub Project #22 que pode ter deixado arquivos não rastreados:

```text
tooling/GitHubProjectSync.psm1
tooling/github-project-sync.ps1
tooling/tests/github-project-sync.tests.ps1
.github/workflows/
docs/governance/GITHUB_PROJECT_SYNC.md
```

**Preserve integralmente esses arquivos, caso existam.**

Não os inclua no commit da TASK-013.

Se for necessário isolar a execução, use um Git worktree local separado a partir da referência local válida de `develop`.

Não apagar, sobrescrever ou mover arquivos preexistentes sem necessidade.

Não executar `git fetch`, `git pull` ou comandos `gh`.

---

## 4. LEITURA DO SETUP

Leia os arquivos existentes:

```text
AGENTS.md
CLAUDE.md
PROJECT_SKILLS.md
README.md
docs/README.md
```

Inspecione:

```text
.ai/
agent_docs/
docs/governance/
docs/knowledge/
docs/requirements/
docs/architecture/
docs/security/
docs/testing/
```

Consulte o Kit IA Dev no caminho oficial.

Leia também:

```text
tasks/README.md
tasks/BACKLOG.md
tasks/DEPENDENCY_GRAPH.md
tasks/IMPLEMENTATION_ORDER.md
tasks/TRACEABILITY_MATRIX.md
tasks/DEFINITION_OF_READY.md
tasks/DEFINITION_OF_DONE.md
```

Localize e leia integralmente:

```text
tasks/backlog/TASK-013-implementar-timeout-cancellation-retry-e-erros-normalizados.md
```

Se o arquivo estiver em outra pasta, localizar sua versão canônica, preservando a organização atual.

Não inventar requisitos ausentes.

---

## 5. BASELINE CONHECIDO

Último estado confirmado pelo relatório da TASK-009:

```text
TASK-001 até TASK-009: DONE

TASK-010: BACKLOG
TASK-011: BACKLOG
TASK-012: BACKLOG
TASK-013: BACKLOG
TASK-014: BACKLOG

Tasks formalmente READY: 0

Baseline de testes: 228/228 PASSED

.NET SDK: 10.0.401
```

Conferir o estado atual pelos arquivos locais.

Não presumir que TASK-013 esteja READY.

---

## 6. DEFINITION OF READY

Validar formalmente a TASK-013.

Dependências conhecidas:

```text
TASK-005
TASK-007
TASK-008
```

Confirmar:

- Todas as dependências obrigatórias concluídas.
- Requirements identificados.
- ADRs aplicáveis consultados.
- Critérios de aceite claros.
- Escopo e exclusões definidos.
- Estratégia de testes aprovada.
- Riscos identificados.
- Ausência de blockers.
- Condições da Definition of Ready atendidas.

Se aprovado:

`Definition of Ready: PASSED`

Caso contrário:

`Definition of Ready: BLOCKED`

Se bloqueado, registrar evidências e interromper.

---

## 7. BRANCH LOCAL

Criar ou reutilizar:

```text
feature/task-TASK-013-timeout-cancellation-retry
```

Utilizar uma referência local íntegra e apropriada de `develop`.

Não utilizar estado remoto como prova de atualização.

Se houver alterações locais incompatíveis, não forçar troca de branch.

Preferir isolamento em worktree local quando necessário.

Não fazer push.

---

## 8. PLANEJAMENTO TÉCNICO

Antes de implementar, produzir plano contendo:

1. Objetivo oficial.
2. Requisitos e critérios de aceite.
3. Contratos existentes.
4. Componentes afetados.
5. Regras de timeout.
6. Regras de cancellation.
7. Política de retry.
8. Classificação de erros.
9. Tratamento de resultados tardios.
10. Idempotência.
11. Integração com lifecycle.
12. Integração com bounded queues, quando exigida.
13. Estratégia de testes.
14. Segurança e riscos.
15. Evidências esperadas.

O arquivo canônico tem precedência sobre as sugestões técnicas deste prompt.

---

## 9. ARQUITETURA

Preservar:

**Modular Monolith + Ports & Adapters + SOLID + Provider Architecture.**

Reutilizar os componentes existentes quando apropriado:

```text
ObsAi.Domain
ObsAi.Application
ObsAi.Application.Lifecycle
ObsAi.Application.Queues
ObsAi.Application.Validation
```

Regras:

- Não criar dependências circulares.
- Não acoplar Domain/Application a providers concretos.
- Não implementar infraestrutura externa.
- Não introduzir bibliotecas desnecessárias.
- Não alterar contratos públicos sem justificativa.
- Não antecipar funcionalidades de outras Tasks.

---

## 10. IMPLEMENTAÇÃO — TIMEOUT

Implementar os requisitos canônicos relativos a timeout.

Considerar:

- Limites por operação.
- Cancelamento por expiração.
- Timeout total, quando previsto.
- Resultado explícito de timeout.
- Tratamento de conclusão tardia.
- Propagação de `CancellationToken`.
- Ausência de operações órfãs.

Não presumir que cancelar um token encerra imediatamente uma operação externa.

Impedir aceitação indevida de resultados expirados.

Não definir limites arbitrários não aprovados.

---

## 11. IMPLEMENTAÇÃO — CANCELLATION

Implementar o comportamento aprovado para:

- Cancelamento antes da execução.
- Cancelamento durante a execução.
- Cancelamento após conclusão.
- Encerramento de sessão.
- Timeout.
- Cancelamento concorrente.
- Resultados tardios.

Preservar as invariantes introduzidas pela TASK-007.

Evitar race conditions e estados inválidos.

Respeitar a semântica dos contratos existentes.

---

## 12. IMPLEMENTAÇÃO — RETRY

Implementar somente retries permitidos.

Avaliar:

- Falhas transitórias.
- Falhas permanentes.
- Idempotência.
- Número máximo de tentativas.
- Backoff.
- Cancelamento durante espera.
- Timeout total.
- Efeitos colaterais.
- Tratamento de falhas repetidas.

Requisitos de segurança:

- Não criar retries infinitos.
- Não repetir operações não idempotentes sem autorização.
- Não utilizar busy waiting.
- Não mascarar falhas permanentes.
- Não ignorar cancelamento.
- Não introduzir provider concreto nesta Task.

---

## 13. IMPLEMENTAÇÃO — ERROS NORMALIZADOS

Definir ou reutilizar os modelos de erro aprovados.

Categorias possíveis, somente quando previstas pelos contratos:

```text
Timeout
Cancelled
TransientFailure
PermanentFailure
InvalidInput
InternalFailure
```

Não inventar categorias incompatíveis com o domínio.

Os erros devem:

- Ter representação consistente.
- Ser testáveis.
- Evitar exposição de dados sensíveis.
- Preservar a distinção entre falhas relevantes.
- Permitir tratamento determinístico.

Não expor credenciais, conteúdos confidenciais ou stack traces em respostas públicas.

---

## 14. TESTES AUTOMATIZADOS

Baseline conhecido: **228 testes aprovados**.

Criar testes para os critérios de aceite reais.

Cenários sugeridos:

1. Operação dentro do timeout.
2. Operação expirada.
3. Cancelamento antes de iniciar.
4. Cancelamento durante execução.
5. Cancelamento após conclusão.
6. Retry transitório com sucesso.
7. Retry esgotado.
8. Falha permanente sem retry.
9. Cancelamento durante backoff.
10. Limite de tentativas respeitado.
11. Operações concorrentes.
12. Isolamento entre sessões.
13. Resultado tardio rejeitado.
14. Erro normalizado corretamente.
15. Dados sensíveis não expostos.
16. Regressão do lifecycle da TASK-007.
17. Compatibilidade com bounded queues da TASK-008.
18. Compatibilidade com validação da TASK-009.

Utilizar dados sintéticos.

Preferir controle determinístico de tempo e concorrência.

Não utilizar rede real nem OBS Studio.

---

## 15. QUALITY GATES

Executar:

```powershell
.\tooling\quality-gates.ps1
```

Validar:

```text
Restore: PASSED
Build: PASSED
Tests: PASSED
Format: PASSED
```

Registrar resultados e exit codes.

Corrigir falhas antes de prosseguir.

Não enfraquecer o runner.

---

## 16. ARCHITECTURE GATE

Verificar:

- SOLID.
- Regras de dependência.
- Ports & Adapters.
- Ausência de ciclos.
- Separação de responsabilidades.
- Compatibilidade com TASK-005 a TASK-009.
- Isolamento de providers.
- Invariantes de sessão.
- Limites da TASK-013.

Executar testes arquiteturais.

Registrar aprovação ou bloqueio real.

---

## 17. SECURITY AUDIT

Auditar:

- Retries ilimitados.
- Exaustão de recursos.
- Retry de operações não idempotentes.
- Falhas de cancelamento.
- Resultados tardios.
- Vazamento entre sessões.
- Dados sensíveis em erros.
- Tratamento inseguro de exceções.
- Race conditions.
- Timeout incorreto.
- Falhas permanentes mascaradas.

Corrigir findings bloqueantes.

---

## 18. CODE REVIEW

Realizar revisão formal do diff completo.

Avaliar:

- Correção funcional.
- Critérios de aceite.
- Arquitetura.
- SOLID.
- Concorrência.
- Cancelamento.
- Retry.
- Segurança.
- Testabilidade.
- Regressões.
- Tratamento de falhas.
- Documentação.
- Escopo.

Classificar findings:

```text
CRITICAL
HIGH
MEDIUM
LOW
INFO
```

Corrigir findings bloqueantes e reexecutar os gates necessários.

---

## 19. SECRET SCAN

Executar varredura das alterações relevantes.

Verificar:

- Tokens.
- Senhas.
- API keys.
- Chaves privadas.
- Connection strings sensíveis.
- Logs.
- Credenciais.

Não imprimir segredos.

Não solicitar login GitHub.

Não acessar o GitHub Project #22.

---

## 20. DOCUMENTAÇÃO

Atualizar somente documentos relacionados à TASK-013.

Registrar:

- Objetivo.
- Escopo.
- Requirements.
- ADRs.
- Políticas de timeout.
- Cancellation.
- Retry.
- Erros normalizados.
- Evidências de testes.
- Quality Gates.
- Security Audit.
- Code Review.
- Riscos residuais.
- Rastreabilidade.
- Estado local da Task.

Atualizar os registros administrativos locais necessários, sem declarar integração remota inexistente.

**Não mover para `tasks/done/` se a Definition of Done exigir merge e validação pós-merge.**

Nesse caso, manter o estado apropriado em `tasks/in-progress/`, indicando que a implementação está pronta e a integração permanece pendente.

---

## 21. PROMPT HISTORY

Inspecionar:

```text
docs/prompts/history/
```

Último histórico conhecido:

```text
prompt26.md
```

Arquivar integralmente este prompt no próximo nome livre.

Não presumir automaticamente que será `prompt27.md` caso esse arquivo já exista.

Não sobrescrever histórico anterior.

Não incluir credenciais.

---

## 22. VALIDAÇÃO PRÉ-COMMIT

Executar:

```powershell
git status --short
git diff
git diff --check
```

Inspecionar arquivos staged e não rastreados.

Incluir somente arquivos relacionados à TASK-013.

Não incluir scripts administrativos da sincronização GitHub.

Confirmar:

```text
Implementation: COMPLETE
Acceptance Criteria: PASSED
Restore: PASSED
Build: PASSED
Tests: PASSED
Format: PASSED
Architecture Gate: PASSED
Security Audit: PASSED
Code Review: APPROVED
Secret Scan: PASSED
Documentation: UPDATED
Prompt History: ARCHIVED
```

Nenhum resultado deve ser declarado aprovado sem execução real.

---

## 23. COMMIT LOCAL

Criar Conventional Commit, por exemplo:

```text
feat: implement TASK-013 timeout cancellation and retry
```

Executar somente commit local.

Não realizar:

```text
git push
git pull
git fetch
gh
```

Não criar Pull Request.

Não realizar merge.

Não excluir a branch da TASK-013.

Manter o trabalho disponível para integração posterior.

---

## 24. RELATÓRIO FINAL

Produzir:

```text
OBS-AI-LIVE-ASSISTANT
TASK-013 — RELATÓRIO FINAL LOCAL

Executor:
Codex

Definition of Ready:
PASSED / BLOCKED

Implementation:
COMPLETE / BLOCKED

Acceptance Criteria:
PASSED / BLOCKED

Restore:
PASSED / BLOCKED

Build:
PASSED / BLOCKED

Warnings:
<quantidade>

Errors:
<quantidade>

Baseline Tests:
228

Tests Added:
<quantidade>

Total Tests:
<quantidade>

Tests Passed:
<quantidade>

Tests Failed:
<quantidade>

Format:
PASSED / BLOCKED

Architecture Gate:
PASSED / BLOCKED

Security Audit:
PASSED / BLOCKED

Code Review:
APPROVED / BLOCKED

Secret Scan:
PASSED / BLOCKED

Documentation:
UPDATED / BLOCKED

Prompt History:
<arquivo>

Local Branch:
<nome>

Local Commit:
<hash>

Working Tree:
CLEAN / DIRTY

GitHub Issues:
NOT USED

GitHub Projects:
NOT USED

Push:
NOT EXECUTED

Pull Request:
NOT CREATED

Remote Merge:
NOT EXECUTED

TASK-013 State:
<estado conforme DoD>

TASK-011:
NOT EXECUTED

TASK-012:
NOT EXECUTED

Next Task:
NOT STARTED

Main:
NOT MODIFIED

Hml:
NOT MODIFIED

Release:
NOT CREATED

OBS Studio:
NOT MODIFIED

STOP
```

---

## 25. CONDIÇÃO DE PARADA

Concluir exclusivamente a implementação local da TASK-013.

Preservar as Tasks anteriores.

Não iniciar a TASK-011 ou TASK-012.

Não executar outras Tasks.

Não atualizar GitHub Issues ou Project #22.

Não utilizar autenticação GitHub.

Não executar push, PR ou merge remoto.

Não modificar `main`, `hml`, releases ou OBS Studio.

Após testes, revisão, documentação e commit local, entregar relatório e **STOP**.
