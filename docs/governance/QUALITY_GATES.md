# Quality Gates

## Gates ativos na fase documental

| Gate | Evidência obrigatória |
|---|---|
| Documentation | Os artefatos solicitados existem, são legíveis, usam `pt-BR` quando destinados a pessoas e possuem navegação válida. |
| Scope Compliance | As alterações permanecem no escopo documental e de governança, sem iniciar fases posteriores. |
| Knowledge Compliance | Fatos, direções, hipóteses, necessidades de pesquisa e decisões estão diferenciados. |
| Security Check | O conteúdo alterado não contém secrets nem orientação insegura sobre credenciais. |
| GitFlow | O trabalho ocorre na feature da Task e segue o fluxo de PR aprovado. |
| Prompt Traceability | O prompt autorizador é arquivado no mesmo commit da Task. |
| Code Review | O diff completo da Task é revisado por severidade antes do merge. |
| Secret Scan | O conteúdo staged é verificado contra credenciais, tokens, chaves e passwords prováveis. |

Finding Critical, finding High não resolvido, secret, conflito, alteração fora do escopo, validação obrigatória reprovada, push incompleto ou divergência local/remota bloqueiam o merge.

## Gates por estágio de entrega

### DEVELOPMENT GATE

Aplica-se a `feature/task-* -> develop`. Exige validação da Task, Code Review, correções obrigatórias, Prompt Traceability, Secret Check, commit, push e PR válidos.

### HML GATE

Aplica-se a `develop -> hml` quando um conjunto coerente de alterações estiver pronto para homologação. Exige revisão do escopo agregado, Quality Gates aplicáveis, PR e autorização de promoção.

### RELEASE GATE

Aplica-se à criação e estabilização de `release/1.0.0XXXX` a partir de `hml` aprovado. Permite somente ajustes finais, correções de release, documentação, versionamento, metadados e correções bloqueadoras aprovadas.

### PRODUCTION GATE

Aplica-se a `release/1.0.0XXXX -> main`. Exige Final Quality Gate, PR aprovado, confirmação de versão estável e autorização de produção. Tag e GitHub Release ocorrem somente após o merge de uma release real aprovada.

## Gates de implementação

A partir de `TASK-001` (fundação da solution), os gates abaixo possuem comandos determinísticos e são aplicáveis à solução: **Build**, **Format validation** (`dotnet restore`, `dotnet build --no-restore`, `dotnet format --verify-no-changes --no-restore` — registrados em `AGENTS.md`).

A partir de `TASK-002`, os gates **Architecture Validation** e **Unit Tests** (convenções auxiliares) são aplicáveis via `dotnet test --no-build` no projeto `tests/Architecture/ObsAi.Architecture.Tests`. Qualquer `ProjectReference` ou `PackageReference` fora das regras autorizadas pelo grafo falha nesses testes e bloqueia o merge.

A `TASK-004` estabeleceu a **Arquitetura de Testes e Quality Gates** (`docs/testing/TEST_ARCHITECTURE.md`), os scaffolds das categorias `Unit`, `Integration`, `Contracts`, `Security`, `FailureIsolation` e `Installer` sob `tests/`, o runner determinístico `tooling/quality-gates.ps1` e os **TestingFoundationTests** (que validam a arquitetura de testes no mesmo `ObsAi.Architecture.Tests`).

Categorias sem suite executável permanecem **NOT APPLICABLE UNTIL IMPLEMENTATION** e não podem ser marcadas como PASSED sem comandos e artefatos reais:

- **Unit Tests** — contracts TASK-005, domínio TASK-006, lifecycle TASK-007, filas TASK-008, validação TASK-009, resiliência TASK-013, configuração TASK-014 e autorização TASK-015 `EXECUTED`
- **Security Tests** — controles de TASK-006/007/008/009, resiliência segura da TASK-013, configuração não secreta da TASK-014 e autorização deny-by-default da TASK-015 `EXECUTED`; controles restantes aplicáveis em Tasks futuras
- **Integration Tests** — lifecycle TASK-007, filas TASK-008 e composição queue/resilience TASK-013 `EXECUTED`; adapters/providers permanecem `NOT EXECUTED`
- **FailureIsolation Tests** — shutdown/publicação TASK-007, filas TASK-008 e retry/falhas normalizadas TASK-013 `EXECUTED`; IPC permanece `NOT EXECUTED` (TASK-026+)
- **Overload/anti-bússola determinístico (TASK-008)** — floods adversários de `QueueFlowTests`/`QueueSecurityTests` provam que carga excedente não expande memória nem derruba o runner, sem `Task.Delay` arbitrário (RNF-019)
- **Installer Tests** — âncora do scaffold `EXECUTED`; comportamento de produto `NOT EXECUTED` (aplicável TASK-046+)
- **OBS Compatibility Tests** — `PARTIAL` (smoke TASK-003); formal `NOT CREATED` (TASK-051)
- **Regression Tests** — `NOT CREATED` (TASK-049/TASK-050)

Contagens exatas são registradas nos relatórios das Tasks. A TASK-007 consolidou 119 testes determinísticos; a TASK-008 elevou o total para 166; a TASK-009 para 228; a implementação local da TASK-013 elevou para 271; e as implementações locais da TASK-014 e da TASK-015 foram consolidadas na branch `integration/task-013-014-015-local`, totalizando **372** testes determinísticos, cobrindo Unit, Integration, Security, FailureIsolation, Architecture, Contracts e Installer.
