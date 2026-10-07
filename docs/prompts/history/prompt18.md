# Prompt 18 — Autorização para executar TASK-002 completa

- **Data:** 2026-10-07
- **Autorizou:** TASK-002 — Materializar projetos e regras de dependência
- **IA:** OpenCode
- **Resultado:** DONE (relatado em `docs/reports/TASK-002_REPORT.md`)

---

AUTORIZAÇÃO — EXECUTAR TASK-002 COMPLETA

PROJETO:
OBS-AI-Live-Assistant

REPOSITÓRIO:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

TASK:
TASK-002 — Materializar projetos e regras de dependência

IA DE EXECUÇÃO:
OpenCode

## 1. REGRA PRINCIPAL — LEIA TODO O SETUP ANTES DE EXECUTAR

Você está assumindo um projeto que já passou por: Bootstrap, Project Discovery, Knowledge Discovery, Knowledge Quality Gate, Requirements, Security Requirements, Technical Research, Architecture, ADRs, Backlog, Dependency Graph, Definition of Ready, Definition of Done e Quality Gates, e cuja TASK-001 foi executada, validada e mergeada em `develop`. NÃO refaça essas etapas. ANTES DE ALTERAR QUALQUER ARQUIVO, leia e respeite integralmente o setup, a governança e a documentação existente. O REPOSITÓRIO É A FONTE DA VERDADE. Não use este prompt para substituir informações mais específicas já aprovadas e versionadas no repositório.

## 2. LEITURA OBRIGATÓRIA

Leia primeiro: AGENTS.md, CLAUDE.md, PROJECT_SKILLS.md, README.md. Depois leia o conteúdo relevante de: .ai/, agent_docs/, docs/governance/, docs/knowledge/, docs/requirements/, docs/security/, docs/research/, docs/architecture/, docs/reports/, tasks/. Obrigatoriamente localizar e ler: tasks/README.md, tasks/BACKLOG.md, tasks/DEPENDENCY_GRAPH.md, tasks/IMPLEMENTATION_ORDER.md, tasks/TRACEABILITY_MATRIX.md, tasks/DEFINITION_OF_READY.md, tasks/DEFINITION_OF_DONE.md, tasks/RELEASE_PLAN.md. Localize também o arquivo canônico correspondente a TASK-002 e leia integralmente. Se houver diferença entre este prompt e a definição específica da TASK-002, preserve Requirements, Architecture, ADRs, Security Requirements e critérios de aceite canônicos.

## 3. ESTADO ESPERADO

Validar pelo repositório: TASK-001 `DONE` em `develop`; Working Tree CLEAN; Product Source Code scaffolded (projetos vazios); `TASK-002` em `tasks/ready/`; `TASK-003` e `TASK-004` aguardando `TASK-002`; OBS NÃO MODIFICADO. Não confiar apenas nesses valores; CONFIRMAR NO REPOSITÓRIO.

## 4. IDIOMA

Toda documentação humana deve permanecer em PORTUGUÊS DO BRASIL (pt-BR): README, Tasks, relatórios, descrições, PR, documentação de desenvolvimento. Preservar nomes técnicos oficiais em inglês quando apropriado.

## 5. GIT — PREPARAÇÃO

Antes de modificar qualquer arquivo: git fetch origin; git branch --show-current; git status; git status --short; git log --oneline --decorate -15. O Working Tree deve estar CLEAN. Depois: git switch develop; git pull --ff-only origin develop. Confirmar develop local = origin/develop. Somente então criar a branch da TASK-002, usando a convenção definida pela governança. Preferencialmente feature/task-TASK-002-projects-dependencies, mas a convenção canônica existente no repositório prevalece.

## 6. DEFINITION OF READY

ANTES da implementação, validar formalmente a TASK-002 contra tasks/DEFINITION_OF_READY.md: objetivo claro; Requirements conhecidos (RNF-017, RNF-018, RNF-019, RNF-028; SEC-007, SEC-032); ADRs aplicáveis conhecidos (ADR-004, ADR-011); dependências concluídas (TASK-001); critérios de aceite definidos; testes esperados definidos (ARCHITECTURE; UNIT para convenções auxiliares); Security impact identificado; nenhum blocker aberto; branch determinada. Se PASSED, prosseguir automaticamente. Se BLOCKED, parar e informar exatamente o blocker.

## 7. EXECUTAR SOMENTE TASK-002

Implementar integralmente TASK-002 — Materializar projetos e regras de dependência. NÃO executar TASK-003, TASK-004 nem outras Tasks mesmo que fiquem READY. A TASK-002 deve seguir exatamente Requirements + Architecture + ADRs + Security + critérios de aceite + Definition of Done.

## 8. ESCOPO

A TASK-002 materializa a estrutura física e as regras de dependência. Criar SOMENTE o previsto na TASK-002 e na Architecture aprovada: projetos `ObsAi.*` de `src/` (Domain, Application, Infrastructure, Providers, ObsIntegration, Host como composition root Exe), referências mínimas entre projetos, registro na solution, e o projeto de testes determinístico `tests/Architecture/ObsAi.Architecture.Tests` exigido pelos Testes Obrigatórios (ARCHITECTURE + UNIT de convenções auxiliares). NÃO assumir que outros itens são obrigatórios. A TASK-002 CANÔNICA determina o escopo.

## 9. .NET

A direção aprovada é .NET 10, com a fundação da TASK-001 (global.json SDK 10.0.401, Directory.Build.props, Directory.Packages.props com Central Package Management, .editorconfig, .slnx) reutilizada e nunca recriada. Não instalar SDK automaticamente. Se a versão necessária estiver disponível, prosseguir. Se não estiver, BLOCKER.

## 10. ESTRUTURA ARQUITETURAL

Respeitar integralmente: docs/architecture/ARCHITECTURE.md; docs/architecture/PROJECT_STRUCTURE.md; docs/architecture/ARCHITECTURE_DECISION_MAP.md; docs/architecture/decisions/. Arquitetura aprovada: Modular Monolith + Ports and Adapters + Provider Architecture (ADR-011). Não transformar em microservices. Não adicionar Kafka, RabbitMQ, Redis, Kubernetes, brokers externos ou cloud infrastructure sem Requirement explícito. Domain e Application permanecem livres de adapters, SDKs e APIs do Windows.

## 11. SOLID

SOLID é obrigatório, especialmente SRP, OCP, LSP, ISP e DIP. Mas não criar abstrações artificiais apenas para dizer que SOLID foi utilizado.

## 12. DEPENDÊNCIAS

Respeitar as Dependency Rules definidas pela Architecture e por PROJECT_STRUCTURE.md. Referências proibidas (quando adicionadas) devem fazer o teste de arquitetura falhar e bloquear o merge: Domain dependendo de qualquer camada; Application/adapters dependendo de Host; produção dependendo de tests; dependências fora do mapa permitido; ciclos; `PackageReference` em projetos `src/`.

## 13. NÃO IMPLEMENTAR FUNCIONALIDADES FUTURAS

Nesta TASK-002 NÃO implementar: YouTube Live Chat; AI Provider; OpenAI; Anthropic; Gemini; Ollama; OpenRouter; TTS; SQLite funcional; Named Pipes funcional; OBS plugin funcional; OBS Dock; OBS audio; OAuth; Credential Manager; DPAPI; installer; update; telemetry; RAG; TruckHub; ports/contracts (TASK-005); domínio de sessão/perfil (TASK-006); arquitetura completa de testes (TASK-004). Esses itens pertencem às Tasks posteriores, exceto o mínimo estrutural explicitamente exigido pela TASK-002.

## 14. NÃO MODIFICAR OBS

NÃO modificar C:\Program Files\obs-studio, C:\Users\gfmau\AppData\Roaming\obs-studio, instalar plugin ou alterar configuração do OBS. OBS deve permanecer NÃO MODIFICADO.

## 15. BUILD

Após materializar os projetos, executar build completo apropriado (dotnet restore; dotnet build, ou os comandos canônicos definidos pelo projeto). Não ocultar warnings relevantes. Resultado necessário: Build Gate PASSED com 0 avisos e 0 erros. Se falhar: investigar, corrigir, executar novamente.

## 16. TESTES

Executar todos os testes obrigatórios da TASK-002 conforme definido pela Task (ARCHITECTURE; UNIT para convenções auxiliares). Não inventar testes irrelevantes além dos obrigatórios e das convenções auxiliares. Executar dotnet test ou comando canônico equivalente. Resultado necessário para merge: Mandatory Tests PASSED.

## 17. ARCHITECTURE VALIDATION

Resultado deve ser Architecture Gate PASSED, validando: projetos esperados existem; projetos registrados na solution; referências permitidas corretas; referências proibidas ausentes; direção de dependências; boundaries válidos; nenhuma capability futura antecipada; SOLID respeitado.

## 18. SECURITY VALIDATION

Mesmo sendo estrutura, validar ausência de secret, credential, API key, token, path sensível indevido, configuração insegura desnecessária e dependência externa desnecessária. Resultado: Security Gate PASSED.

## 19. CODE REVIEW

Executar Code Review completo usando as Skills instaladas quando aplicável. Revisar correctness, architecture, SOLID, maintainability, security, dependency management, build configuration, testability, naming, documentation e scope compliance. Classificar findings CRITICAL/HIGH/MEDIUM/LOW/INFO. Obrigatório antes do merge: Critical = 0 e High = 0. Corrigir Medium quando seguro e dentro do escopo, e re-executar os gates após a correção.

## 20. SECRET SCAN

Executar Secret Scan. Resultado obrigatório: Secrets NONE. Nunca versionar credenciais reais.

## 21. DOCUMENTAÇÃO

Atualizar apenas documentação afetada pela implementação: README.md, docs/architecture/PROJECT_STRUCTURE.md, docs/governance/QUALITY_GATES.md, docs/reports/TASK-002_REPORT.md, AGENTS.md (Commands/Repository Structure), tasks/ (README, BACKLOG, DEFINITION_OF_READY, DEPENDENCY_GRAPH, IMPLEMENTATION_ORDER) quando necessário. Não reescrever documentação sem necessidade.

## 22. STATUS DA TASK

Ao concluir a implementação e todos os Gates, TASK-002 deve passar para DONE usando o mecanismo canônico de tasks/, preservando histórico e rastreabilidade (mover o arquivo canônico de `ready/` para `done/`, Status DONE e registro de Execução).

## 23. DEPENDENCY GRAPH APÓS TASK-002

Depois que TASK-002 estiver realmente DONE, recalcular o estado das dependências, identificar quais Tasks ficaram READY (esperado: TASK-003 e TASK-004) e atualizar DEPENDENCY_GRAPH, BACKLOG, IMPLEMENTATION_ORDER ou equivalentes SOMENTE quando a governança exigir. Não executar as novas Tasks READY.

## 24. PROMPT TRACEABILITY

Inspecionar docs/prompts/history/. Último conhecido: prompt17.md. NÃO assumir automaticamente que o próximo é prompt18.md; calcular pelo estado real do diretório. Arquivar ESTE PROMPT INTEGRALMENTE no próximo número válido. Nunca sobrescrever histórico. Não arquivar secrets.

## 25. DEFINITION OF DONE

Validar integralmente tasks/DEFINITION_OF_DONE.md e confirmar quando aplicável: Implementation COMPLETE; Build PASSED; Mandatory Tests PASSED; Architecture Gate PASSED; Security Gate PASSED; Acceptance Criteria PASSED; Code Review PASSED; Critical Findings 0; High Findings 0; Secret Scan PASSED; Documentation UPDATED; Prompt ARCHIVED; Git VALID.

## 26. VALIDAÇÃO PRÉ-COMMIT

Executar git status, git diff, git diff --check e novamente os comandos obrigatórios de build, testes e format quando necessário. Não versionar bin/, obj/, logs, temporary files, IDE cache, credentials ou secrets.

## 27. COMMIT

Criar Conventional Commit apropriado (sugestão: feat: materialize projects and dependency rules). O mesmo commit da Task deve incluir quando aplicável: implementation, tests, documentation, task status e prompt history.

## 28. PUSH

Push da feature branch e confirmar local HEAD = remote HEAD.

## 29. PULL REQUEST

Criar PR feature/task-TASK-002-... -> develop em PORTUGUÊS DO BRASIL, incluindo objetivo, escopo, arquivos/projetos criados, múltiplas decisões arquiteturais respeitadas, testes, Build Gate, Architecture Gate, Security Gate, Code Review, Acceptance Criteria, riscos e observações.

## 30. PR VALIDATION

Antes do merge confirmar Build PASSED, Tests PASSED, Architecture PASSED, Security PASSED, Acceptance Criteria PASSED, Critical Findings 0, High Findings 0, Secrets NONE, Prompt Traceability PASSED, GitFlow PASSED, PR MERGEABLE.

## 31. MERGE AUTOMÁTICO PARA DEVELOP

Se TODOS os Gates obrigatórios passarem, MERGE AUTOMÁTICO de feature/task-TASK-002-... para develop, sem pedir autorização intermediária. Se algum Gate obrigatório falhar, NÃO MERGEAR e corrigir quando possível dentro do escopo.

## 32. PÓS-MERGE

Depois do merge: git switch develop; git pull --ff-only origin develop; confirmar develop local = origin/develop; executar validação pós-merge apropriada; excluir feature local e remota quando seguro; confirmar Working Tree CLEAN.

## 33. NÃO PROMOVER

TASK-002 termina em develop. NÃO executar develop -> hml, NÃO criar release/1.0.0XXXX, NÃO alterar main, NÃO criar tag nem GitHub Release. O fluxo posterior feature/task-* -> develop -> hml -> release/1.0.0XXXX -> main será executado somente quando a governança autorizar a promoção.

## 34. NÃO EXECUTAR TASK-003/004

Mesmo que TASK-003, TASK-004 ou outras Tasks fiquem READY, NÃO EXECUTAR. Somente informar no relatório final as Novas Tasks READY. Depois, STOP.

## 35. INTERRUPÇÃO / RETOMADA

Se a execução for interrompida, NÃO DESCARTAR TRABALHO VÁLIDO. Na retomada: inspecionar Git; identificar branch, arquivos alterados e último Gate concluído; continuar do ponto exato. NÃO usar automaticamente git reset --hard, git restore ., git clean -fd ou git checkout -- . Não reiniciar TASK-002 do zero se houver trabalho válido.

## 36. RESULTADO FINAL OBRIGATÓRIO

Retornar relatório completo com: TASK, TASK STATUS, Projects Created, Projects list, Solution, Project References, Dependency Rules, Forbidden Dependencies, .NET, Restore, Build, Warnings, Errors, Tests, Total Tests, Architecture Gate, Security Gate, Acceptance Criteria, Definition of Done, Code Review, Critical/High/Medium/Low Findings, Secrets, Prompt arquivado, Branch, Commit, Push, Pull Request, PR Number, PR Validation, Merge para develop, Merge Commit, Develop local/remoto/sincronizada, Feature local/remota, Working Tree, Novas Tasks READY, Tasks BLOCKED, hml, Release, main, OBS, PRÓXIMO. Depois: STOP.

EXECUTE A TASK-002 DO SETUP ATÉ O FIM. Não pare depois de criar arquivos, depois do build, depois dos testes, antes do Code Review, antes do Prompt Archive, antes do commit/push/PR ou antes do merge para develop se todos os Gates passarem. Não pule automaticamente para TASK-003. Ao finalizar: RELATÓRIO FINAL -> STOP.