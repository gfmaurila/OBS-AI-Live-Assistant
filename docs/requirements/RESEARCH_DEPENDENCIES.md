# Dependências de Research

Este mapa referencia o backlog canônico em [`docs/research/RESEARCH_BACKLOG.md`](../research/RESEARCH_BACKLOG.md). Nenhuma pesquisa externa foi executada nesta Task. `Blocking?` indica bloqueio de decisão/implementação posterior, não do fechamento de Requirements.

| Research Item | Requirements relacionados | Blocking? | Impacto em Architecture |
|---|---|---|---|
| RES-001 — OBS Studio 32.x Plugin SDK | RF-032; RNF-001, RNF-024 | SIM — integração nativa | Define viabilidade, ABI e toolchain de componente nativo. |
| RES-002 — Headers, libs e build nativo | RF-032; RNF-019, RNF-020, RNF-024 | SIM — build/installer nativo | Define dependências reproduzíveis e packaging. |
| RES-003 — Caminhos oficiais de instalação de plugins | RF-032, RF-035; RNF-020, RNF-021, RNF-025 | SIM — Installation Design | Define escopo de arquivos, permissões e coexistência. |
| RES-004 — OBS Dock | RF-001, RF-031; RNF-025, RNF-027 | SIM — se Dock for escolhida | Compara UI embutida e externa sem antecipar escolha. |
| RES-005 — Native audio | RF-023 a RF-025; RNF-002, RNF-015 | SIM — TTS audio design | Determina capacidades, threading, formatos e riscos. |
| RES-006 — OBS Source e dispositivo virtual | RF-022 a RF-025; RNF-025 | SIM — TTS audio design | Compara rotas de entrega e monitoramento. |
| RES-007 — OBS WebSocket | RF-005, RF-022, RF-036; RNF-006, RNF-025 | SIM — integration boundary | Define capacidades, autenticação e limites externos. |
| RES-008 — Divisão de responsabilidades | RF-001, RF-022, RF-036; RNF-001, RNF-018 | SIM — Architecture | Delimita OBS, Assistant Core e UI/Dock. |
| RES-009 — C++ ↔ .NET IPC | RF-026, RF-027; RNF-004, RNF-009, RNF-010, RNF-026 | SIM — se houver fronteira nativa | Define contrato, autenticação, versionamento e failure modes. |
| RES-010 — Process isolation | RF-001, RF-013, RF-021, RF-027; RNF-001, RNF-008, RNF-016 | SIM — Architecture | Define contenção, backpressure, restart e degradação. |
| RES-011 — SQLite lifecycle | RF-028 a RF-030, RF-033; RNF-012, RNF-022, RNF-023 | SIM — Data Design | Define biblioteca, migrations, concorrência e recovery. |
| RES-012 — Windows Credential Manager | RF-018; RNF-003 | SIM — Security/Architecture | Subsidia storage e lifecycle de secrets. |
| RES-013 — DPAPI | RF-018; RNF-003 | SIM — Security/Architecture | Compara escopo, backup e recuperação de secrets. |
| RES-014 — YouTube Live Chat API | RF-005 a RF-007, RF-018, RF-027; RNF-006, RNF-011, RNF-017 | SIM — Chat/Security Design | Define OAuth, scopes, quotas, revogação e adapter. |
| RES-015 — AI provider abstraction | RF-016 a RF-019, RF-026, RF-027; RNF-010, RNF-011, RNF-017 | SIM — Provider Design | Define capacidades mínimas e erros do port de IA. |
| RES-016 — Local AI viability | RF-016, RF-017; RNF-015, RNF-016 | NÃO — opção futura/condicionada | Informa decisão sobre provider local e hardware. |
| RES-017 — TTS abstraction | RF-024 a RF-027; RNF-010, RNF-011, RNF-017 | SIM — TTS Provider Design | Define contrato de voz, formato, erro e cancelamento. |
| RES-018 — Installer | RF-032; RNF-020, RNF-024 | SIM — Installation Design | Compara tecnologia, privilégios, assinatura e rollback. |
| RES-019 — Upgrade | RF-033; RNF-012, RNF-020 | SIM — Installation/Data Design | Define versionamento, migration e recuperação. |
| RES-020 — Repair | RF-034; RNF-021 | SIM — Installation Design | Define componentes reparáveis e preservação. |
| RES-021 — Uninstall | RF-035; RNF-003, RNF-021, RNF-023 | SIM — Installation/Security | Define remoção/preservação de dados, logs e secrets. |
| RES-022 — OBS compatibility strategy | RF-032; RNF-019, RNF-024 | SIM — compatibility policy | Define faixa suportada, detecção e matriz de testes. |
| RES-023 — Persistent memory e retenção | RF-015, RF-030; RNF-005, RNF-023 | SIM — somente se aprovada | Define finalidade, consentimento, retenção e exclusão. |
| RES-024 — Provider privacy and terms | RF-006, RF-020, RF-030; RNF-005, RNF-007, RNF-023 | SIM — seleção de provider | Informa privacidade, região, retenção e uso de dados. |

Itens marcados `SIM` não impedem descrever o que é necessário; impedem escolher ou implementar com segurança o como correspondente.
