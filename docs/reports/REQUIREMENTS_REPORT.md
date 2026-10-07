# Relatório de Requirements

## Identificação

- Data: 2026-10-06
- Projeto: OBS-AI-Live-Assistant
- Task: `requirements`
- Branch de trabalho: `feature/task-requirements`
- Natureza: retomada do trabalho interrompido por limite de uso
- Trabalho anterior preservado: **SIM**

## Fontes analisadas

Foram usados o contrato `AGENTS.md`, regras especializadas em `agent_docs/`, documentação de projeto, governança, mapa/decisões/conflitos de Knowledge, Knowledge Quality Gate aprovado e o backlog canônico de Research. O histórico de prompts foi usado somente para rastreabilidade. Não houve pesquisa técnica externa nem leitura ou modificação da configuração do OBS.

O Knowledge Quality Gate de entrada está **PASSED** e o relatório anterior registra Requirements Readiness **READY**.

## Artefatos e contagens

| Artefato | Quantidade / estado |
|---|---:|
| Requisitos Funcionais | 36 |
| Requisitos Não Funcionais | 28 |
| Constraints | 15 |
| Assumptions | 9 |
| Open Questions | 15 |
| Blocking Questions de Requirements | 0 |
| Risks | 14 |
| Research Dependencies | 24 |
| ADR Candidates | 10 |
| Out of Scope V1 / Future | 15 |
| Requirements Traceability | PASSED |

## Síntese

O escopo V1 cobre integração controlada com OBS Studio 32.x x64 em Windows 10/11 x64, lifecycle do assistente, Assistant Profiles, LiveContext, YouTube Live Chat, normalização, triggers, acionamento manual, moderação, rate limiting, filas limitadas, AI/TTS Providers, BYOK, validação de resposta, texto, voz, timeout, cancellation, persistência autorizada, sessões, histórico mínimo condicionado, logging, diagnóstico e lifecycle de instalação.

Os requisitos de segurança tratam chat e respostas de IA como não confiáveis, proíbem secrets em plaintext, exigem minimização/redação, separam OAuth e credenciais de configuração comum e estabelecem que chat ou saída de IA nunca autorizam diretamente ações sensíveis do OBS.

O requisito de isolamento cobre explicitamente falhas de AI Provider, TTS Provider, Chat Provider, banco de dados e Assistant Core. O mecanismo permanece para Research e ADR.

## WHAT × HOW

As necessidades e critérios observáveis foram mantidos em Requirements. Plugin nativo versus OBS WebSocket, boundary C++/.NET, IPC, process isolation, lifecycle SQLite, secret storage, roteamento de áudio, tecnologia do installer, provider architecture, persistent memory e estratégia de compatibilidade foram registrados como direção, Research, Client Decision ou ADR Candidate, sem decisão final.

## Quality Gate e readiness

- Requirements Quality Gate: **PASSED**
- Architecture Readiness: **READY** para preparação, condicionada à execução prévia de Security Requirements e às pesquisas/ADRs aplicáveis
- Blockers de Requirements: **0**
- Research externo: **NOT STARTED**
- Security Requirements: **NOT STARTED**
- Architecture: **NOT STARTED**
- Product Source Code: **NOT CREATED**
- Implementation: **NOT STARTED**

## Validação da Task

| Validação | Resultado |
|---|---|
| Estrutura documental e navegação | PASSED |
| Critérios de aceite e prioridades | PASSED |
| WHAT × HOW | PASSED |
| UTF-8 e caracteres corrompidos | PASSED |
| Prompt Traceability (`prompt3.md`) | PASSED |
| Code Review — CRITICAL abertos | 0 |
| Code Review — HIGH abertos | 0 |
| Code Review — MEDIUM abertos | 0 |
| Code Review — LOW abertos | 0 |
| Security / Secret Scan | NONE |
| GitFlow — branch e escopo pré-entrega | PASSED |

O Code Review identificou inicialmente um finding `MEDIUM`: RF-022 não explicitava a entrega textual ao YouTube Live Chat. O requisito e seus vínculos de Research/ADR foram corrigidos; a revalidação não encontrou findings abertos. Como não existe runtime nesta Task, a auditoria de segurança avaliou as fronteiras e controles documentados e não identificou caminho explorável atual.

## Recomendação

Após a validação final e integração desta Task em `develop`, iniciar **SECURITY REQUIREMENTS** e preparar o Research priorizado que alimentará Architecture. Não iniciar Architecture ou implementação automaticamente.
