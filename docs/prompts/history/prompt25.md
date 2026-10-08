# PROMPT OPERACIONAL — TASK-009 (AUTORIZAÇÃO ORIGINAL)

**Projeto:** OBS-AI-Live-Assistant
**Repositório:** `D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant`
**IA executora:** big-pickle (opencode)
**Task autorizada:** TASK-009
**Destino autorizado:** `develop`
**Modalidade:** Execução integral, com merge automático condicionado aos Quality Gates.

> Este arquivo é o texto verbatim do prompt autorizador da TASK-009 recebido pelo
> agente em 2026-10-08, arquivado integralmente para cumprir o Gate de Prompt Traceability.

---

## 1. MISSÃO

Executar integralmente a **TASK-009 — Implementar validação e normalização de entradas**, desde a
recuperação do estado até a validação pós-merge, relatório final e STOP.

Fluxo obrigatório: Recuperar Estado → Definition of Ready → Feature Branch → Implementação → Testes
→ Quality Gates → Architecture Gate → Security Audit → Code Review → Correções → Secret Scan →
Documentação → Prompt History → Commit → Push → PR → Validação do PR → Merge em `develop` →
Validação pós-merge → Definition of Done → Rastreabilidade → Dependency Graph → Limpeza →
Relatório Final → STOP.

## 2. ESCOPO

- Implementar a **TASK-009** exclusivamente. Nada de TASK-010, TASK-013, TASK-014, TASK-024 ou
  qualquer outra.
- Use o arquivo canônico da TASK como referência obrigatória:
  `tasks/backlog/TASK-009-implementar-validação-e-normalização-de-entradas.md`.

## 3. RESTRIÇÕES DE TRABALHO

### 3.1 Git e Commits

- PROIBIDO executar `git reset --hard` em qualquer cenário.
- PROIBIDO executar `git clean -fd`.
- PROIBIDO executar `git restore .` ou `git checkout -- .`.
- PROIBIDO fazer push direto para `develop`; sempre via Pull Request.
- PROIBIDO merge manual/forçado em `develop`.
- PROIBIDO modificar `hml`, `release/1.0.0XXXX`, `main`, `tag`, `deploy` ou o OBS.
- Toda operação de GitHub DEVE usar a API REST com credencial via `curl.exe` e o token extraído do
  `git credential fill`, nunca uma URL com o token ou credenciais expostas.
- O token extraído NUNCA deve ser exibido ou armazenado. Deve residir apenas em variáveis locais e
  ser excluído imediatamente após o uso.
- Se a branch de feature já existir local ou remotamente, NÃO recriar: inspecionar e continuar.
- Abrir Pull Request base `develop` desta branch com o padrão e as regras usuais do projeto e com a
  revisão dos commits filtrados (não commitar `secret` e `CancellationToken`).

### 3.2 Idempotência (re-executável)

- Inspecionar o estado real do repositório antes de assumir conclusão.
- Se a implementação, os testes, a documentação, as demais evidências e as execuções exigidas já
  existirem e estiverem aprovadas, concluir formalmente. Se NÃO, adotar o plano de implementação
  abaixo e concluir.

### 3.3 Segurança

- Cobrir exclusivamente as SEC da TASK-009 conforme a descrição canônica.
- Implementar apenas o escopo da TASK-009.
- Nenhum parâmetro obrigatório da TASK-009 pode ser tabelado no código fora dos critérios canônicos.
- Não desenhar nenhuma capability, provider ou infraestrutura fora da V1.
- Em nenhuma circunstância usar secrets reais em testes ou do contrato.

### 3.4 Checking e Verificação

- Toda verificação deve ser determinística, sem evidência por hipótese.
- Nunca diga que um teste, build ou execução passou sem rodar e apresentar a saída.
- O secret scan é obrigatório e deve cobrir todo o conteúdo staged (novo, modificado, deletado).

## 4. SETUP DA TASK

### 4.1 Leitura do Setup

Ler integralmente na ordem a seguir, se e somente se existir: AGENTS.md, CLAUDE.md,
PROJECT_SKILLS.md, README.md, docs/README.md, .ai/README.md, .ai/agent/README.md,
.ai/governance/README.md, .ai/knowledge/README.md, docs/governance/README.md,
docs/governance/GITFLOW.md, docs/governance/QUALITY_GATES.md, docs/gov/e basics,
docs/testing/README.md, agent_docs/README.md, agent_docs/architecture.md,
agent_docs/roles.md, agent_docs/rules.md, agent_docs/business-rules.md,
agent_docs/engineering-standards.md, agent_docs/productivity.md, agent_docs/security.md,
docs/README.md, .claude/skills, .codex/skills e o Kit IA Dev. Não presumir que existam;
se ausentes, simplesmente registrar a ausência e continuar.

### 4.2 Definition of Ready (DoR) v1.0

Parametrizado na TASK-009 canônica:

- objetivo e limites revisados;
- Requirements/ADRs acima confirmados;
- dependências marcadas DONE;
- critérios e testes refinados sem blocker;
- branch `feature/task-TASK-009-<descricao>` determinável.

## 5. FORMA DA IMPLEMENTAÇÃO

A TASK-009 deve ser feita em `ObsAi.Application`, seguindo as decisões ADR-004 e ADR-008, com
preservação dos contratos da TASK-005, do domínio da TASK-006, do lifecycle da TASK-007 e das
filas da TASK-008. A implementação será entregue no namespace `ObsAi.Application.Validation`:

1. `InputValidationLimits` — record imutável via `Create(int maximumTextLength, int
   maximumReferenceLength)` que valida e congela dimensões `>= 1`; sem números de produto (SEC-002).
2. `InputRejectionReason` — classificação segura das rejeições (ausente, malformado, encoding,
   oversize, timestamp) sem payload.
3. `InputRejection` — diagnostico seguro com `ToString` redigido (RNF-004, SEC-024).
4. `NormalizedChatMessage` — representação comum com exatamente: `ProviderId`, `ChannelReference`,
   `SenderReference`, `MessageReference`, `Text`, `ReceivedAtUtc`.
5. `ChatInputValidationResult` — falha-fechada: aceito (`Message`) OU rejeitado (`Rejection`);
   nunca ambos, nunca nenhum; construção apenas por fábricas.
6. `ChatInputValidator.Validate(ChatMessage input, InputValidationLimits limits)` — validação de
   schema, encoding (surrogate órfão), tamanho (bruto antes da normalização e normalizado) e
   timestamp; normalização NFC + trim + colapso de espaços para o texto; trim para referências.

Testes obrigatórios: **UNIT**; **CONTRACT**; **SECURITY**; mais Architecture conforme as suites
existentes (0/1 dependency, superfície selada, sem capability/sink).

## 6. QUALITY GATES

- `dotnet restore`
- `dotnet build --no-restore`
- `dotnet test --no-build` (testes determinísticos, sem testes interativos)
- `dotnet format --verify-no-changes --no-restore`
- `powershell -ExecutionPolicy Bypass -File tooling\quality-gates.ps1` (rodar como runner oficial)
- Não inventar comandos.

TODOS os gates devem estar **PASSED** antes do merge.

## 7. RELATÓRIO FINAL

Após o merge e a verificação pós-merge, produzir o relatório final na ordem canônica do projeto
(retirado do workflow de TASK-001 a TASK-008): TASK; TASK STATUS; IA; Execução integral; DoR;
Implementação; Escopo; Projetos alterados; .NET SDK; Restore; Build (Warnings/Errors); Tests
(anteriores/novos/total); Architecture Gate; Security Gate; Acceptance Criteria; DoD; Code Review;
Critical/High/Medium/Low Findings; Secrets; Prompt arquivado (path); Branch; Commit; Push; Pull
Request; PR Number; PR Validation; Merge para develop; Merge Commit; Develop local/remoto/
sincronizada; Feature local/remota; Working Tree; Novas Tasks READY; Tasks BLOCKED;
hml/Release/main/OBS; PRÓXIMO; STOP.

Depois: **STOP** — não avançar para nenhuma Task seguinte sem nova autorização do cliente.