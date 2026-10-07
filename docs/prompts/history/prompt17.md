# Prompt 17 — Autorização para executar TASK-001 completa

- **Data:** 2026-10-07
- **Autorizou:** TASK-001 — Criar a fundação da solution e do tooling
- **IA:** OpenCode
- **Resultado:** DONE (prévia relatada em `docs/reports/TASK-001_REPORT.md`)

---

AUTORIZAÇÃO — EXECUTAR TASK-001 COMPLETA

PROJETO:
OBS-AI-Live-Assistant

REPOSITÓRIO:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

KIT IA DEV:
D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\Kit-IA-Dev

TASK:
TASK-001 — Criar a fundação da solution e do tooling

IA DE EXECUÇÃO:
OpenCode

## 1. REGRA PRINCIPAL — LEIA TODO O SETUP ANTES DE EXECUTAR

Você está assumindo um projeto que já passou por: Bootstrap, Knowledge Quality Gate, Requirements, Security Requirements, Technical Research, Architecture, ADRs, Backlog, Dependency Graph, Definition of Ready, Definition of Done e Quality Gates. NÃO refaça essas etapas. ANTES DE ALTERAR QUALQUER ARQUIVO, leia e respeite integralmente o setup, a governança e a documentação existente. O REPOSITÓRIO É A FONTE DA VERDADE. Não use este prompt para substituir informações mais específicas já aprovadas e versionadas no repositório.

## 2. LEITURA OBRIGATÓRIA

Leia primeiro: AGENTS.md, CLAUDE.md, PROJECT_SKILLS.md, README.md. Depois leia o conteúdo relevante de: .ai/, agent_docs/, docs/governance/, docs/knowledge/, docs/requirements/, docs/security/, docs/research/, docs/architecture/, docs/reports/, tasks/. Obrigatoriamente localizar e ler: tasks/README.md, tasks/BACKLOG.md, tasks/DEPENDENCY_GRAPH.md, tasks/IMPLEMENTATION_ORDER.md, tasks/TRACEABILITY_MATRIX.md, tasks/DEFINITION_OF_READY.md, tasks/DEFINITION_OF_DONE.md, tasks/RELEASE_PLAN.md. Localize também o arquivo canônico correspondente a TASK-001 e leia integralmente. Se houver diferença entre este prompt e a definição específica da TASK-001, preserve Requirements, Architecture, ADRs, Security Requirements e critérios de aceite canônicos.

## 3. ESTADO ESPERADO

Validar pelo repositório: Knowledge Quality Gate PASSED; Requirements Quality Gate PASSED; Security Quality Gate PASSED; Research Quality Gate PASSED; Architecture Quality Gate PASSED; Security Architecture Review PASSED; Backlog Quality Gate PASSED; Implementation Readiness READY; Tasks 52; Primeira Task READY TASK-001; Product Source Code NÃO CRIADO; Implementation NÃO INICIADA; OBS NÃO MODIFICADO. Não confiar apenas nesses valores; CONFIRMAR NO REPOSITÓRIO.

## 4. IDIOMA

Toda documentação humana deve permanecer em PORTUGUÊS DO BRASIL (pt-BR): README, Tasks, relatórios, descrições, PR, documentação de desenvolvimento. Preservar nomes técnicos oficiais em inglês quando apropriado.

## 5. GIT — PREPARAÇÃO

Antes de modificar qualquer arquivo: git fetch origin; git branch --show-current; git status; git status --short; git log --oneline --decorate -15. O Working Tree deve estar CLEAN. Depois: git switch develop; git pull --ff-only origin develop. Confirmar develop local = origin/develop. Somente então criar a branch da TASK-001, usando a convenção definida pela governança. Preferencialmente feature/task-TASK-001-foundation, mas a convenção canônica existente no repositório prevalece.

## 6. DEFINITION OF READY

ANTES da implementação, validar formalmente a TASK-001 contra tasks/DEFINITION_OF_READY.md: objetivo claro; Requirements conhecidos; ADRs aplicáveis conhecidos; dependências concluídas; critérios de aceite definidos; testes esperados definidos; Security impact identificado; nenhum blocker aberto; branch determinada. Se PASSED, prosseguir automaticamente. Se BLOCKED, parar e informar exatamente o blocker.

## 7. EXECUTAR SOMENTE TASK-001

Implementar integralmente TASK-001 — Criar a fundação da solution e do tooling. NÃO executar TASK-002. NÃO executar outras Tasks mesmo que fiquem READY posteriormente. A TASK-001 deve seguir exatamente Requirements + Architecture + ADRs + Security + critérios de aceite + Definition of Done.

## 8. ESCOPO

A TASK-001 é a fundação técnica do projeto. Criar SOMENTE aquilo que estiver previsto na TASK-001 e na Architecture aprovada. Pode incluir, SE previsto pela documentação: solution .NET; global.json; Directory.Build.props; Directory.Packages.props; .editorconfig; estrutura inicial src/; estrutura inicial tests/; projetos base; referências entre projetos; configurações comuns; warnings; nullable; analyzers; build configuration; test foundation; architecture test foundation; tooling mínimo; convenções de build. NÃO assumir que todos esses itens são obrigatórios. A TASK-001 CANÔNICA determina o escopo.

## 9. .NET

A direção aprovada é .NET 10. Confirmar SDK instalado antes de gerar arquivos: dotnet --info; dotnet --list-sdks; dotnet --list-runtimes. Não instalar SDK automaticamente. Se a versão necessária estiver disponível, prosseguir. Se não estiver, BLOCKER.

## 10. ESTRUTURA ARQUITETURAL

Respeitar integralmente: docs/architecture/ARCHITECTURE.md; docs/architecture/PROJECT_STRUCTURE.md; docs/architecture/ARCHITECTURE_DECISION_MAP.md; docs/architecture/decisions/. Arquitetura aprovada: Modular Monolith + Ports and Adapters + Provider Architecture. Não transformar em microservices. Não adicionar Kafka, RabbitMQ, Redis, Kubernetes ou cloud infrastructure sem Requirement explícito.

## 11. SOLID

SOLID é obrigatório, especialmente SRP, OCP, LSP, ISP e DIP. Mas não criar abstrações artificiais apenas para dizer que SOLID foi utilizado.

## 12. DEPENDÊNCIAS

Respeitar as Dependency Rules definidas pela Architecture. Não permitir que componentes internos centrais dependam diretamente, sem necessidade arquitetural, de OBS, SQLite, OpenAI, YouTube, TTS vendor, Windows APIs ou provider concreto.

## 13. NÃO IMPLEMENTAR FUNCIONALIDADES FUTURAS

Nesta TASK-001 NÃO implementar: YouTube Live Chat; AI Provider; OpenAI; Anthropic; Gemini; Ollama; OpenRouter; TTS; SQLite funcional; Named Pipes funcional; OBS plugin funcional; OBS Dock; OBS audio; OAuth; Credential Manager; DPAPI; installer; update; telemetry; RAG; TruckHub. Esses itens pertencem às Tasks posteriores.

## 14. NÃO MODIFICAR OBS

NÃO modificar C:\Program Files\obs-studio, C:\Users\gfmau\AppData\Roaming\obs-studio, instalar plugin ou alterar configuração do OBS. OBS deve permanecer NÃO MODIFICADO.

## 15. BUILD

Após implementar a fundação, executar build completo apropriado (preferencialmente dotnet restore; dotnet build, ou os comandos canônicos definidos pelo projeto). Não ocultar warnings relevantes. Resultado necessário: Build Gate PASSED. Se falhar: investigar, corrigir, executar novamente.

## 16. TESTES

Executar todos os testes aplicáveis à TASK-001, conforme definido pela Task (Unit Tests, Architecture Tests, Smoke Tests, Build Validation). Não inventar testes irrelevantes. Executar dotnet test ou comando canônico equivalente. Resultado necessário para merge: Mandatory Tests PASSED.

## 17. ARCHITECTURE VALIDATION

Validar estrutura física, dependency direction, referências entre projetos, separação de responsabilidades, ausência de dependências proibidas, conformidade com ADRs, SOLID, Ports and Adapters. Resultado: Architecture Gate PASSED ou BLOCKED.

## 18. SECURITY VALIDATION

Mesmo sendo foundation, validar ausência de secret, credential, API key, token, path sensível indevido, configuração insegura desnecessária e dependência suspeita. Resultado: Security Gate PASSED.

## 19. CODE REVIEW

Executar Code Review completo usando as Skills instaladas quando aplicável. Revisar correctness, architecture, SOLID, maintainability, security, dependency management, build configuration, testability, naming, documentation e scope compliance. Classificar findings CRITICAL/HIGH/MEDIUM/LOW/INFO. Obrigatório antes do merge: Critical = 0 e High = 0. Corrigir Medium quando seguro e dentro do escopo.

## 20. SECRET SCAN

Executar Secret Scan. Resultado obrigatório: Secrets NONE. Nunca versionar credenciais reais.

## 21. DOCUMENTAÇÃO

Atualizar apenas documentação afetada pela implementação: README.md, docs/README.md, docs/architecture/PROJECT_STRUCTURE.md, docs/reports/, tasks/ quando necessário. Não reescrever documentação sem necessidade.

## 22. STATUS DA TASK

Ao concluir a implementação e todos os Gates, TASK-001 deve passar para DONE usando o mecanismo canônico de tasks/, preservando histórico e rastreabilidade.

## 23. DEPENDENCY GRAPH APÓS TASK-001

Depois que TASK-001 estiver realmente DONE, recalcular o estado das dependências, identificar quais Tasks ficaram READY/BLOCKED/BACKLOG e atualizar DEPENDENCY_GRAPH, BACKLOG, IMPLEMENTATION_ORDER ou equivalentes SOMENTE quando a governança exigir. Não executar as novas Tasks READY.

## 24. PROMPT TRACEABILITY

Inspecionar docs/prompts/history/. Último conhecido: prompt16.md. NÃO assumir automaticamente que o próximo é prompt17.md; calcular pelo estado real do diretório. Arquivar ESTE PROMPT INTEGRALMENTE no próximo número válido. Nunca sobrescrever histórico. Não arquivar secrets.

## 25. DEFINITION OF DONE

Validar integralmente tasks/DEFINITION_OF_DONE.md e confirmar quando aplicável: Implementation COMPLETE; Build PASSED; Mandatory Tests PASSED; Architecture Gate PASSED; Security Gate PASSED; Acceptance Criteria PASSED; Code Review PASSED; Critical Findings 0; High Findings 0; Secret Scan PASSED; Documentation UPDATED; Prompt ARCHIVED; Git VALID.

## 26. VALIDAÇÃO PRÉ-COMMIT

Executar git status, git diff, git diff --check e novamente os comandos obrigatórios de build e testes quando necessário. Não versionar bin/, obj/, logs, temporary files, IDE cache, credentials ou secrets.

## 27. COMMIT

Criar Conventional Commit apropriado (sugestão: feat: establish solution foundation). O mesmo commit da Task deve incluir quando aplicável: implementation, tests, documentation, task status e prompt history.

## 28. PUSH

Push da feature branch e confirmar local HEAD = remote HEAD.

## 29. PULL REQUEST

Criar PR feature/task-TASK-001-... -> develop em PORTUGUÊS DO BRASIL, incluindo objetivo, escopo, arquivos/projetos criados, decisões arquiteturais respeitadas, testes, Build Gate, Architecture Gate, Security Gate, Code Review, Acceptance Criteria, riscos e observações.

## 30. PR VALIDATION

Antes do merge confirmar Build PASSED, Tests PASSED, Architecture PASSED, Security PASSED, Acceptance Criteria PASSED, Critical Findings 0, High Findings 0, Secrets NONE, Prompt Traceability PASSED, GitFlow PASSED, PR MERGEABLE.

## 31. MERGE AUTOMÁTICO PARA DEVELOP

Se TODOS os Gates obrigatórios passarem, MERGE AUTOMÁTICO de feature/task-TASK-001-... para develop, sem pedir autorização intermediária. Se algum Gate obrigatório falhar, NÃO MERGEAR e corrigir quando possível dentro do escopo.

## 32. PÓS-MERGE

Depois do merge: git switch develop; git pull --ff-only origin develop; confirmar develop local = origin/develop; executar validação pós-merge apropriada; excluir feature local e remota quando seguro; confirmar Working Tree CLEAN.

## 33. NÃO PROMOVER

TASK-001 termina em develop. NÃO executar develop -> hml, NÃO criar release/1.0.0XXXX, NÃO alterar main, NÃO criar tag nem GitHub Release. O fluxo posterior feature/task-* -> develop -> hml -> release/1.0.0XXXX -> main será executado somente quando a governança autorizar a promoção.

## 34. NÃO EXECUTAR TASK-002

Mesmo que TASK-002 ou outras Tasks fiquem READY, NÃO EXECUTAR. Somente informar no relatório final as Novas Tasks READY. Depois, STOP.

## 35. INTERRUPÇÃO / RETOMADA

Se a execução for interrompida, NÃO DESCARTAR TRABALHO VÁLIDO. Na retomada: inspecionar Git; identificar branch, arquivos alterados e último Gate concluído; continuar do ponto exato. NÃO usar automaticamente git reset --hard, git restore ., git clean -fd ou git checkout -- . Não reiniciar TASK-001 do zero se houver trabalho válido.

## 36. RESULTADO FINAL OBRIGATÓRIO

Retornar relatório completo com: TASK, TASK STATUS, IA, Definition of Ready, Implementation, Solution, Projects, .NET, Restore, Build, Tests, Total Tests, Architecture Gate, Security Gate, Acceptance Criteria, Definition of Done, Code Review, Critical/High/Medium/Low Findings, Secrets, Prompt arquivado, Branch, Commit, Push, Pull Request, PR Number, PR Validation, Merge para develop, Merge Commit, Develop local/remoto/sincronizada, Feature local/remota, Working Tree, Novas Tasks READY, Tasks BLOCKED, hml, Release, main, OBS, PRÓXIMO. Depois: STOP.

EXECUTE A TASK-001 DO SETUP ATÉ O FIM. Não pare depois de criar arquivos, depois do build, depois dos testes, antes do Code Review, antes do Prompt Archive, antes do commit/push/PR ou antes do merge para develop se todos os Gates passarem. Não pule automaticamente para TASK-002. Ao finalizar: RELATÓRIO FINAL -> STOP.