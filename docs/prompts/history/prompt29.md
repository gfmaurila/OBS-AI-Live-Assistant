# PROMPT OPERACIONAL — TASK-015
## OBS-AI-Live-Assistant | Codex | Execução exclusivamente local

**Projeto:** OBS-AI-Live-Assistant
**Diretório principal:** `D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant`
**Kit IA Dev:** `D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev`
**Executor:** Codex
**Idioma obrigatório:** Português do Brasil
**Task exclusiva:** TASK-015 — Implementar autorização e políticas de segurança
**Modalidade:** Implementação local com testes, revisão, documentação e commit local.

---

## 1. MISSÃO

Execute exclusivamente a TASK-015, respeitando a arquitetura e a governança do projeto.

Fluxo obrigatório:

**Setup → Inspeção local → Project Discovery → Knowledge Discovery → Definition of Ready → Planejamento → Branch/worktree local → Implementação → Testes → Quality Gates → Architecture Gate → Security Audit → Code Review → Correções → Secret Scan → Documentação → Prompt History → Definition of Done técnica → Commit local → Relatório final → STOP.**

Não iniciar outras Tasks.

Não utilizar serviços remotos do GitHub.

Não executar push, Pull Request ou merge.

Não solicitar autenticação GitHub.

---

## 2. REGRAS ABSOLUTAS

É obrigatório:

1. Preservar todos os branches, worktrees e commits preexistentes.
2. Ler as instruções do projeto e do Kit IA Dev.
3. Ler integralmente a TASK-015 canônica.
4. Validar formalmente a Definition of Ready.
5. Implementar somente os requisitos aprovados.
6. Manter os limites da arquitetura.
7. Aplicar fail-closed às decisões de autorização exigidas.
8. Criar testes determinísticos de autorização e segurança.
9. Executar todos os Quality Gates.
10. Executar Architecture Gate.
11. Executar Security Audit.
12. Realizar Code Review.
13. Corrigir findings bloqueantes.
14. Executar Secret Scan.
15. Atualizar documentação e rastreabilidade.
16. Arquivar integralmente este prompt.
17. Criar Conventional Commit local.
18. Entregar relatório final com evidências.
19. Parar.

É proibido:

- Executar outras Tasks.
- Implementar autenticação OAuth ou armazenamento de credenciais fora do escopo.
- Introduzir permissões implícitas.
- Usar allow-all como fallback.
- Modificar `main`, `hml` ou releases.
- Modificar instalação, plugins ou configurações do OBS Studio.
- Acessar GitHub Issues ou Projects.
- Executar `gh`, `git fetch`, `git pull` ou `git push`.
- Criar PR ou realizar merge.
- Descartar trabalho preexistente.
- Enfraquecer Quality Gates.
- Declarar DONE sem a Definition of Done canônica.

---

## 3. INSPEÇÃO INICIAL E PRESERVAÇÃO

Execute, sem operações remotas:

```powershell
git branch --show-current
git status --short
git log -5 --oneline
git branch -vv
git worktree list --porcelain
```

Localize e preserve os seguintes trabalhos:

### TASK-013

```text
Branch:
feature/task-TASK-013-timeout-cancellation-retry

Commit:
91de8d93ff50a26480317a574178a4d8c1b1fcc8

Estado:
IMPLEMENTATION COMPLETE — PENDING INTEGRATION
```

### TASK-014

```text
Branch:
feature/task-TASK-014-configuration-model-validation

Commit:
78da119434965b49f3846b48e633d5df068a0e0f

Worktree:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant\temp\task014

Estado:
IMPLEMENTATION COMPLETE — PENDING INTEGRATION
```

A TASK-014 reportou 288 testes aprovados em sua branch.

Não usar essa quantidade como baseline da TASK-015.

Preserve também os arquivos administrativos preexistentes relacionados à tentativa de sincronização com GitHub Project #22.

Não executar:

```powershell
git reset --hard
git clean -fd
git restore .
```

Não utilizar `git stash` indiscriminadamente.

Se existirem alterações não relacionadas no worktree principal, crie um worktree separado para a TASK-015.

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

Consulte o Kit IA Dev.

Leia:

```text
tasks/README.md
tasks/BACKLOG.md
tasks/DEPENDENCY_GRAPH.md
tasks/IMPLEMENTATION_ORDER.md
tasks/TRACEABILITY_MATRIX.md
tasks/DEFINITION_OF_READY.md
tasks/DEFINITION_OF_DONE.md
```

Localize e leia integralmente o arquivo:

```text
tasks/backlog/TASK-015-implementar-autorização-e-políticas-de-segurança.md
```

Se ele estiver em outro diretório, localizar o arquivo canônico sem recriá-lo.

**O escopo canônico prevalece sobre qualquer sugestão técnica deste prompt.**

---

## 5. DEFINITION OF READY

Dependências conhecidas:

```text
TASK-005 — DONE
TASK-009 — DONE
```

Confirmar:

- Dependências concluídas.
- Requirements identificados.
- ADRs aplicáveis consultados.
- Critérios de aceite testáveis.
- Escopo e exclusões documentados.
- Requisitos de segurança claros.
- Estratégia de testes definida.
- Riscos mapeados.
- Ausência de blockers.
- Autorização restrita à TASK-015.

Resultado:

`Definition of Ready: PASSED / BLOCKED`

Se BLOCKED, registrar evidências e parar.

Não antecipar outras Tasks.

---

## 6. BRANCH E WORKTREE LOCAL

Criar ou reutilizar:

```text
feature/task-TASK-015-authorization-security-policies
```

Diretório sugerido:

```text
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant\temp\task015
```

Criar o worktree somente se o caminho estiver livre e não houver outro worktree legítimo da TASK-015.

Usar a referência local válida de `develop`, após inspecioná-la.

Não derivar automaticamente da TASK-013 ou TASK-014.

Não fazer merge entre essas branches.

Se houver trabalho prévio da TASK-015, inspecionar e continuar, sem duplicar implementações.

---

## 7. PLANEJAMENTO TÉCNICO

Antes da implementação, produzir plano contendo:

1. Objetivo canônico da TASK-015.
2. Requisitos funcionais.
3. Requisitos de segurança.
4. ADRs aplicáveis.
5. Contratos de autorização existentes.
6. Recursos protegidos.
7. Sujeitos e identidades, quando previstos.
8. Políticas e permissões.
9. Modelo de decisão.
10. Regras de negação.
11. Tratamento de contexto ausente.
12. Tratamento de permissões inválidas.
13. Limites entre autenticação e autorização.
14. Integração com contratos existentes.
15. Testes.
16. Riscos e regressões.
17. Evidências esperadas.

Não criar uma infraestrutura completa de Identity Management fora do escopo.

---

## 8. ARQUITETURA OBRIGATÓRIA

Preservar:

**Modular Monolith + Ports & Adapters + SOLID + Provider Architecture.**

Projetos existentes:

```text
src/ObsAi.Domain/
src/ObsAi.Application/
src/ObsAi.Infrastructure/
src/ObsAi.Providers/
src/ObsAi.ObsIntegration/
src/ObsAi.Host/
```

Princípios obrigatórios:

- Dependency Inversion.
- Separação de responsabilidades.
- Interfaces explícitas.
- Negação por padrão.
- Testabilidade.
- Isolamento de providers.
- Ausência de dependências circulares.
- Compatibilidade com contratos anteriores.

Não depender de implementações concretas de OBS, YouTube, OAuth ou bancos de dados.

---

## 9. AUTORIZAÇÃO E POLÍTICAS

Confirmar no arquivo canônico o modelo requerido.

Quando aplicável, implementar:

- Contratos de autorização.
- Identificação explícita de operações protegidas.
- Avaliação determinística de políticas.
- Decisões de permitir/negar.
- Contexto mínimo necessário.
- Políticas por operação ou capability.
- Rejeição de permissões desconhecidas.
- Tratamento de identidade/contexto ausente.
- Isolamento entre sessões.
- Mensagens de erro seguras.

Todas as permissões precisam ter origem explícita e verificável no modelo aprovado.

Não confiar em campos arbitrários fornecidos pelo próprio solicitante como prova de autorização.

**A ausência de uma política válida não deve resultar em permissão automática.**

---

## 10. FAIL-CLOSED

Aplicar fail-closed nos cenários previstos:

- Política inexistente.
- Permissão desconhecida.
- Identidade ausente.
- Contexto inválido.
- Configuração de segurança inválida.
- Inconsistência de autorização.
- Falha interna na avaliação.
- Operação não reconhecida.
- Sessão encerrada ou inválida, quando aplicável.

A decisão de autorização deve ser explícita.

Não mascarar falhas internas como autorização concedida.

Não retornar detalhes internos sensíveis ao consumidor.

---

## 11. PRINCÍPIO DO MENOR PRIVILÉGIO

Implementar apenas os privilégios exigidos pelos critérios de aceite.

Considerar, conforme o modelo canônico:

- Operações autorizadas.
- Operações proibidas.
- Escopo da autorização.
- Limites por sessão.
- Separação de responsabilidades.
- Permissões padrão.
- Reavaliação de autorização quando necessária.

Não criar perfis administrativos irrestritos por conveniência.

Não introduzir privilégios globais sem requisito.

Não inventar hierarquias de papéis não aprovadas.

---

## 12. COMPATIBILIDADE COM AS TASKS ANTERIORES

Preservar os contratos integrados:

```text
TASK-005 — Contracts e Ports
TASK-006 — Domain
TASK-007 — Lifecycle
TASK-008 — Bounded Queues
TASK-009 — Input Validation
```

A TASK-013 e a TASK-014 estão implementadas apenas em branches locais separadas.

Não exigir APIs dessas Tasks para concluir a TASK-015, salvo dependência canônica expressa.

Não modificar seus worktrees.

Não copiar arquivos indiscriminadamente.

---

## 13. FORA DO ESCOPO

Não implementar:

- TASK-010 — triggers, moderação, blocklist e rate limiting.
- TASK-011 — Context Builder e Short-Term Memory.
- TASK-012 — validação e coordenação de respostas.
- TASK-013 — timeout, cancellation e retry.
- TASK-014 — modelo e validação de configuração.
- TASK-016 — redaction e erros seguros.
- TASK-017 — Secure Credential Store.
- TASK-018 — lifecycle BYOK e OAuth.
- TASK-024 — plugin OBS.
- Persistência.
- Interface gráfica.
- Named Pipes.
- Autenticação real de providers.
- Integrações de rede.
- Infraestrutura cloud.

A TASK-015 deve implementar somente sua fronteira de autorização.

---

## 14. TESTES AUTOMATIZADOS

Baseline de referência de `develop` após TASK-009:

```text
228 testes aprovados
```

Confirmar a contagem real no worktree da TASK-015.

Não somar automaticamente testes das branches TASK-013 e TASK-014.

Criar testes determinísticos para os critérios de aceite.

Considerar, quando aplicável:

1. Operação explicitamente autorizada.
2. Operação explicitamente negada.
3. Política inexistente.
4. Permissão desconhecida.
5. Contexto nulo.
6. Contexto inválido.
7. Identidade ausente.
8. Permissão insuficiente.
9. Escopo incorreto.
10. Contexto de outra sessão.
11. Estado de sessão inválido.
12. Falha na avaliação.
13. Negação por padrão.
14. Política imutável, quando exigida.
15. Decisão determinística.
16. Ausência de exposição de dados sensíveis.
17. Preservação dos limites arquiteturais.
18. Regressão de contratos anteriores.

Utilizar dados sintéticos.

Não utilizar OBS Studio.

Não utilizar rede real.

Não reduzir as verificações de segurança para aumentar artificialmente a aprovação.

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

Registrar:

- Exit code.
- Total de projetos.
- Avisos.
- Erros.
- Testes aprovados.
- Testes falhos.
- Testes ignorados.

Corrigir falhas antes de continuar.

Não enfraquecer o runner.

---

## 16. ARCHITECTURE GATE

Verificar:

- SOLID.
- Dependency Inversion.
- Ports & Adapters.
- Ausência de ciclos.
- Isolamento da Application.
- Contratos explícitos.
- Compatibilidade com ADRs.
- Ausência de dependências em providers concretos.
- Limites da TASK-015.

Executar testes arquiteturais.

Registrar evidências reais.

---

## 17. SECURITY AUDIT

Auditar especificamente:

- Permissão indevida.
- Escalada de privilégio.
- Bypass de autorização.
- Falhas de fail-closed.
- Políticas excessivamente permissivas.
- Confusão entre identidade e autorização.
- Contexto fornecido por origem não confiável.
- Vazamento entre sessões.
- Tratamento inseguro de exceções.
- Exposição de informações sensíveis.
- Falhas de concorrência, quando aplicáveis.
- Ausência de validação nos limites de confiança.

Corrigir findings Critical e High.

Tratar demais findings conforme a governança.

Registrar o resultado formal no relatório.

---

## 18. CODE REVIEW

Revisar integralmente o diff.

Avaliar:

- Correção funcional.
- Critérios de aceite.
- Segurança.
- SOLID.
- Encapsulamento.
- Testabilidade.
- Comportamento fail-closed.
- Compatibilidade com contratos existentes.
- Regressões.
- Exceções.
- Mensagens de erro.
- Manutenibilidade.
- Escopo.

Classificar findings conforme a governança.

Corrigir findings bloqueantes e executar novamente os gates impactados.

---

## 19. SECRET SCAN

Executar varredura dos arquivos alterados.

Verificar:

- Tokens.
- Senhas.
- Chaves privadas.
- API keys.
- Connection strings sensíveis.
- Credenciais.
- Logs de autenticação.
- Dados secretos em testes.

Não imprimir segredos.

Não realizar login GitHub.

Não acessar GitHub Projects.

---

## 20. DOCUMENTAÇÃO

Criar ou atualizar apenas os documentos necessários à TASK-015.

Registrar:

- Objetivo.
- Escopo.
- Requirements.
- ADRs.
- Modelo de autorização.
- Políticas implementadas.
- Comportamento fail-closed.
- Limites de confiança.
- Segurança.
- Critérios de aceite.
- Testes.
- Quality Gates.
- Architecture Gate.
- Security Audit.
- Code Review.
- Rastreabilidade.
- Estado local da Task.

Atualizar os índices relevantes.

Evitar documentação redundante.

Não modificar o estado de outras Tasks.

---

## 21. PROMPT HISTORY

Inspecionar:

```text
docs/prompts/history/
```

Últimos arquivos mencionados em trabalhos anteriores:

```text
prompt27.md
prompt28.md
```

Identificar o próximo número livre no worktree da TASK-015.

Não sobrescrever arquivos históricos.

Arquivar integralmente este prompt.

Não incluir credenciais.

Não presumir que um arquivo inexistente nesta branch não exista em outro worktree: verificar o histórico local acessível antes de escolher o número.

---

## 22. ESTADO ADMINISTRATIVO DA TASK

Após validação técnica:

- Registrar implementação concluída.
- Atualizar rastreabilidade.
- Atualizar backlog local.
- Atualizar o arquivo canônico.
- Preservar Dependency Graph consistente.

Se a Definition of Done exigir merge e validação pós-merge, utilizar:

```text
IN_PROGRESS — IMPLEMENTATION COMPLETE — PENDING INTEGRATION
```

Não marcar DONE antecipadamente.

Não promover automaticamente a TASK-016 ou outras dependentes para READY.

---

## 23. VALIDAÇÃO PRÉ-COMMIT

Execute:

```powershell
git status --short
git diff
git diff --check
```

Inspecione os arquivos staged e untracked.

Não incluir arquivos de outras Tasks.

Não incluir arquivos de GitHub Project Sync.

Não incluir caches ou artefatos temporários.

Confirmar:

```text
Definition of Ready: PASSED
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

Não declarar resultados sem execução real.

---

## 24. COMMIT LOCAL

Criar Conventional Commit.

Exemplo:

```text
feat(security): implement TASK-015 authorization policies
```

Executar somente commit local.

Não fazer push.

Não criar PR.

Não realizar merge.

Não excluir a branch.

Manter o worktree disponível para integração posterior.

---

## 25. RELATÓRIO FINAL

Preencher com evidências reais:

```text
OBS-AI-LIVE-ASSISTANT
TASK-015 — RELATÓRIO FINAL LOCAL

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

Build Warnings:
<quantidade>

Build Errors:
<quantidade>

Baseline Tests:
<quantidade observada>

Tests Added:
<quantidade>

Total Tests:
<quantidade>

Tests Passed:
<quantidade>

Tests Failed:
<quantidade>

Tests Skipped:
<quantidade>

Format:
PASSED / BLOCKED

Architecture Gate:
PASSED / BLOCKED

Security Audit:
PASSED / BLOCKED

Critical Findings:
<quantidade>

High Findings:
<quantidade>

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

Local Worktree:
<caminho>

Local Commit:
<hash completo>

Working Tree:
CLEAN / DIRTY

TASK-013:
PRESERVED — PENDING INTEGRATION

TASK-014:
PRESERVED — PENDING INTEGRATION

TASK-011:
NOT EXECUTED

TASK-012:
NOT EXECUTED

GitHub Issues:
NOT USED

GitHub Projects:
NOT USED

Git Push:
NOT EXECUTED

Pull Request:
NOT CREATED

Remote Merge:
NOT EXECUTED

TASK-015 State:
<estado conforme DoD>

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

## 26. CONDIÇÃO FINAL

**Execute exclusivamente a TASK-015.**

Preserve integralmente as TASK-013 e TASK-014.

Não execute TASK-010, TASK-011, TASK-012, TASK-016 ou qualquer outra Task.

Não acesse GitHub Issues ou Project #22.

Não solicite autenticação GitHub.

Não execute push, PR ou merge remoto.

Não modifique `main`, `hml`, releases ou OBS Studio.

Após testes, revisão, documentação e commit local, entregue o relatório final.

**STOP.**
