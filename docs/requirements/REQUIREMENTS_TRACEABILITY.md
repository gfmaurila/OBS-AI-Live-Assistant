# Rastreabilidade de Requirements

As fontes canônicas sustentam os requisitos; o [histórico de prompts](../prompts/README.md) registra autorização e contexto operacional, mas não é fonte canônica de produto. Os critérios resumidos abaixo remetem aos critérios completos nos arquivos de RF e RNF.

| Knowledge Source | Requirement | Research | ADR Candidate | Acceptance Criteria rastreável |
|---|---|---|---|---|
| `AGENTS.md`; `agent_docs/business-rules.md` — produto independente e controle do streamer | RF-001, RF-002, RF-009, RF-029 | RES-008, RES-010 | ADR-001, ADR-003, ADR-008 | Pausa impede novas chamadas; encerramento do assistente não encerra OBS; sessão delimita estado temporário. |
| Regras de negócio — Assistant Profile e LiveContext | RF-003, RF-004, RF-014, RF-015 | RES-023 | ADR-008, ADR-009 | Perfil selecionado afeta solicitações seguintes; contexto não autorizado ou de sessão anterior não é usado. |
| `AGENTS.md`; Project Scope — YouTube Live Chat V1 | RF-005, RF-006, RF-007 | RES-014, RES-024 | ADR-001, ADR-004 | Conexão expõe estado real; mensagens malformadas não alcançam IA; dados recebidos são minimizados. |
| Regras de negócio — `@assistente`, `!ia` e trigger configurável | RF-008; ASM-003 | — | ADR-008 | Mensagem sem trigger habilitado não alcança IA; trigger não concede autorização OBS. |
| `agent_docs/security.md` — moderação de entrada e antiabuso | RF-010 a RF-013; RNF-004, RNF-008 | RES-010 | ADR-003, ADR-008 | Entrada reprovada ou acima de limite não chega ao provider; fila não cresce sem limite. |
| Segurança e minimização — contexto de solicitação | RF-014, RF-015; RNF-005, RNF-007, RNF-023 | RES-023, RES-024 | ADR-008, ADR-009 | Prompt não contém secrets ou histórico ilimitado; dados expirados/excluídos deixam de ser usados. |
| `AGENTS.md`; Knowledge Decisions — AI multi-provider e BYOK | RF-016 a RF-019; RNF-003, RNF-017 | RES-012, RES-013, RES-015, RES-016 | ADR-004, ADR-005 | Troca de provider preserva regras centrais; secret não aparece integralmente; falhas são classificadas. |
| `agent_docs/security.md` — saída de IA não confiável | RF-020, RF-036; RNF-004, RNF-006, RNF-007 | RES-007, RES-008, RES-024 | ADR-001, ADR-008 | Resposta reprovada não é publicada/narrada; conteúdo de chat/IA não executa ação sensível. |
| Regras de negócio — respostas em texto e voz independentes | RF-021 a RF-025; RNF-002, RNF-008 | RES-005, RES-006, RES-017 | ADR-003, ADR-006 | Texto funciona com TTS desabilitado; sínteses ficam limitadas e não se sobrepõem sem autorização. |
| Segurança e engenharia — timeout, cancellation e erro | RF-026, RF-027; RNF-009 a RNF-011 | RES-009, RES-010, RES-014, RES-015, RES-017 | ADR-002 a ADR-004 | Operação termina em estado explícito; cancelamento impede nova saída; timeout libera capacidade. |
| Regras de negócio; direção SQLite | RF-028 a RF-030; RNF-012, RNF-022, RNF-023 | RES-011, RES-023, RES-024 | ADR-004, ADR-009 | Reinício restaura dados autorizados; falhas/corrupção não viram sucesso; retenção pode ser excluída. |
| Knowledge Map — logging e observabilidade | RF-031; RNF-013, RNF-014 | — | ADR-008 | Estados degradados são distinguíveis; logs e mensagens não expõem secrets ou payload sensível integral. |
| Research Backlog — install, upgrade, repair e uninstall | RF-032 a RF-035; RNF-020, RNF-021 | RES-002, RES-003, RES-018 a RES-021 | ADR-007 | Incompatibilidade é detectada; falha não vira sucesso; categorias removidas/preservadas são explícitas. |
| `AGENTS.md`; regras de negócio — proteção do OBS | RNF-001, RNF-002, RNF-025, RNF-026 | RES-007 a RES-010 | ADR-001 a ADR-003 | Falhas de IA, TTS, chat, banco e Assistant Core não derrubam OBS; encerramento deixa estado recuperável. |
| `agent_docs/security.md` — secrets, OAuth e least privilege | RF-018, RF-036; RNF-003, RNF-006, RNF-013 | RES-012 a RES-014, RES-021 | ADR-001, ADR-005, ADR-007 | Secret não é persistido/logado em plaintext; revogação impede novo uso; ação não allowlisted é negada. |
| Engenharia — desempenho e recursos | RNF-008, RNF-015, RNF-016 | RES-005, RES-006, RES-010, RES-022 | ADR-003, ADR-006, ADR-010 | Etapas têm latência mensurável; sobrecarga ativa backpressure; threads críticas do OBS não são bloqueadas. |
| `AGENTS.md`; Architecture Direction — manutenção e provider boundaries | RNF-017, RNF-018 | RES-008, RES-014, RES-015, RES-017 | ADR-003, ADR-004 | Peculiaridade de provider não enfraquece regras centrais; Architecture futura explicita ownership. |
| Engineering Standards; Quality Gates — testabilidade | RNF-019, RNF-028 | RES-022 | ADR-010 | Plano futuro mapeia riscos a testes determinísticos e cada decisão material referencia evidência. |
| Project Scope — Windows 10/11 x64, OBS 32.x x64 e coexistência | CON-001, CON-002, CON-009, CON-015; RNF-024, RNF-025 | RES-001 a RES-008, RES-022 | ADR-001, ADR-006, ADR-007, ADR-010 | Ambiente incompatível é detectado; configuração não autorizada e recursos de outras integrações não são sobrescritos. |
| `agent_docs/architecture.md` — non-goals V1 | OOS-001 a OOS-015 | — | — | Artefatos da V1 não introduzem chat futuro, telemetria de jogos, RAG, runtime multi-agent ou infraestrutura distribuída. |
| Knowledge Quality Gate | Todos os RF/RNF, assumptions, constraints e open questions | RES-001 a RES-024 | ADR-001 a ADR-010 | Estados `CONFIRMED`, `ASSUMPTION`, `REQUIRES_*`, `FUTURE` e `OUT_OF_SCOPE_V1` permanecem distinguíveis. |

## Cobertura

- RF cobertos: **36 de 36**.
- RNF cobertos: **28 de 28**.
- Research mapeado: **24 de 24 itens**.
- ADR Candidates mapeados: **10 de 10**.
- Prompt-base da fase: [`prompt3.md`](../prompts/history/prompt3.md), usado somente como rastreabilidade histórica.

Resultado da rastreabilidade: **PASSED**.
