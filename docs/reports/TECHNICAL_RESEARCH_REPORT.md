# Relatório — Technical Research

## Identificação

- Projeto: OBS-AI-Live-Assistant
- Task: `technical-research`
- Data de conclusão: 2026-10-06
- Branch de execução: `feature/task-technical-research`
- Architecture e implementação: **NOT STARTED**

## Fontes analisadas

Foram analisados integralmente os artefatos canônicos relevantes de projeto, governança, knowledge, requirements, security, reports, `.ai/`, `agent_docs/`, `AGENTS.md`, `CLAUDE.md` e `PROJECT_SKILLS.md`. A pesquisa externa usou 56 URLs oficiais: OBS Project, Microsoft/.NET/Windows, SQLite, Google/YouTube, OpenAI, Anthropic, Google Gemini, Ollama, OpenRouter e ElevenLabs. Registro em [`RESEARCH_SOURCES.md`](../research/RESEARCH_SOURCES.md).

## Inventário e resultado

| Métrica | Quantidade |
|---|---:|
| Research Items | 27 |
| RESOLVED | 16 |
| PARTIALLY_RESOLVED | 6 |
| CLIENT_DECISION | 5 |
| BLOCKED | 0 |
| Fontes oficiais únicas | 56 |
| ADR Candidates preparados | 10 |

## Principais conclusões

- A abordagem recomendada para futura decisão é Hybrid: plugin nativo mínimo para Dock e, se comprovado, áudio; Core, providers, regras e dados em processo separado.
- OBS WebSocket 5.x atende operações expostas e eventos, com auth e discovery de capabilities; não substitui Frontend API/libobs.
- Named Pipes com DACL explícita e protocolo versionado é a baseline de IPC recomendada.
- SQLite atende o V1 local com single-writer, migrations, Online Backup e versão que contenha a correção WAL documentada.
- Credential Manager é recomendado para credenciais discretas; DPAPI user-scope é opção complementar para blobs.
- YouTube V1 requer OAuth desktop com PKCE, escopos mínimos, descoberta do live chat, polling/streaming disciplinado, quota e revogação.
- IA local permanece OPTIONAL/FUTURE. Providers AI/TTS V1 ainda são decisão de produto.
- Installer tradicional assinado é melhor candidato para Core + plugin externo; WiX/Burn e Inno Setup seguem finalistas para ADR/protótipo.
- Logging é local, estruturado, rotacionado e redacted; supply chain/signing são gates futuros de release.
- .NET 10 é LTS até novembro de 2028, mas a matriz atual restringe Windows 10 a LTSC/Enterprise; o cliente deve fechar a política de edições antes do release.

## Confidence e pendências

Conclusões diretamente suportadas por documentação oficial foram marcadas `HIGH`; contratos de providers, áudio e tecnologia final de installer ficam `MEDIUM` por dependerem de seleção/protótipo. Não há conclusão `LOW`. Itens parciais possuem evidência suficiente para o ADR comparar opções. Decisões de cliente não fechadas: IA local no V1, política de uninstall, faixa comercial do OBS 32.x, memória persistente e edições Windows 10 suportadas.

## Impacto de segurança

A recomendação mantém BYOK fora de configuração/SQLite/logs, trata chat e output de IA como não confiáveis, impede autoridade direta sobre OBS, exige autenticação/autorização no IPC, limita filas/retries e não introduz trust boundary não documentada. Security Review não encontrou conflito com SEC-001..034.

## Quality Gates

- Requirements Quality Gate: **PASSED**.
- Security Quality Gate: **PASSED**.
- Research Quality Gate: **PASSED**.
- Requirements Traceability: **PASSED**.
- Security Traceability: **PASSED**.
- Architecture Readiness: **READY**.
- Architecture Security Readiness: **READY**.
- Blockers: **0**.

## Revisões

- Code Review documental: **APROVADO**.
- Security Review: **APROVADO**.
- Critical: 0; High: 0; Medium: 0; Low: 0.
- Secrets: **NONE**.

## Recomendação

Prosseguir, somente em Task autorizada posterior, para **ARCHITECTURE + ADRs**, usando os inputs preparados. Não iniciar implementação, installer, database ou alteração do OBS.
