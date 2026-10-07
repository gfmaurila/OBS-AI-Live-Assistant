# Requirements do OBS-AI-Live-Assistant

## Objetivo

Definir o comportamento, os atributos de qualidade, os limites e as dependências da V1 de uma aplicação Windows independente que recebe solicitações do streamer ou do YouTube Live Chat e produz respostas configuráveis em texto e/ou voz, sem tornar a estabilidade do OBS Studio dependente de chat, IA, TTS, persistência ou rede.

## Contexto e classificação

Este documento consolida a fase de Requirements. Ele deriva do contrato do projeto, das regras de negócio e segurança, do Knowledge Quality Gate `PASSED`, do mapa de conhecimento e do backlog de Research. Não seleciona transportes IPC, bibliotecas, schema, armazenamento de secrets, integração de áudio ou tecnologia de installer.

Classificações aplicáveis: `CONFIRMED`, `ASSUMPTION`, `REQUIRES_RESEARCH`, `REQUIRES_ADR`, `REQUIRES_CLIENT_DECISION`, `OUT_OF_SCOPE_V1` e `FUTURE`.

## Stakeholders

- streamer que configura e opera o assistente;
- viewers que enviam mensagens durante a live;
- equipe de desenvolvimento, segurança, teste, suporte e distribuição;
- mantenedores das integrações com OBS Studio e providers;
- titulares dos dados e credenciais utilizados pelo produto.

## Atores e sistemas externos

| Ator | Responsabilidade no contexto do produto |
|---|---|
| Streamer | Controlar ativação, configuração, providers, perfil, contexto, moderação e saídas. |
| Viewer | Enviar mensagens não confiáveis pelo chat da live. |
| OBS-AI-Live-Assistant | Orquestrar o fluxo funcional e apresentar estado e diagnóstico. |
| OBS Studio | Hospedar a transmissão e consumir apenas capacidades autorizadas da integração. |
| Chat Provider | Fornecer mensagens e estado do chat; YouTube Live Chat é o escopo V1. |
| AI Provider | Produzir respostas sob contrato de provider e credencial do usuário. |
| TTS Provider | Converter texto aprovado em voz quando habilitado. |
| Sistema Operacional | Oferecer o ambiente Windows, ciclo de vida local e controles de segurança. |
| Serviços externos | Disponibilizar APIs sujeitas a autenticação, quotas, latência e indisponibilidade. |

## Escopo V1

Estão no escopo: operação local em Windows 10/11 x64 com OBS Studio 32.x x64; entrada manual e por YouTube Live Chat; triggers configuráveis inicialmente compatíveis com `@assistente` e `!ia`; moderação, rate limiting e filas limitadas; processamento por AI Provider; respostas em texto e TTS configuráveis; BYOK; perfis, contexto limitado de live, sessões, configuração persistente, logging seguro e diagnóstico; instalação, upgrade, repair e uninstall.

Integração multi-provider é requisito de capacidade. A implementação de todos os providers conhecidos não é requisito da V1. OpenAI, Anthropic, Gemini, Ollama, OpenRouter e Custom são candidatos de AI Provider; Windows TTS, Azure Speech e ElevenLabs são candidatos de TTS Provider; somente YouTube é Chat Provider V1. A seleção concreta permanece dependente de Research, decisão de produto e ADR quando aplicável.

## Capacidades principais

O comportamento de referência é:

```text
Live Chat
-> recebimento e normalização
-> detecção de trigger
-> moderação
-> rate limiting
-> fila limitada de solicitações
-> construção de contexto limitado
-> AI Provider
-> validação da resposta
-> fila de resposta
-> texto e/ou TTS
```

Esse fluxo descreve resultados e controles funcionais; não prescreve componentes, processos ou tecnologias internas.

## Requisitos funcionais

Os **36 requisitos funcionais** `RF-001` a `RF-036`, com prioridade, origem, dependências e critérios de aceite, estão em [FUNCTIONAL_REQUIREMENTS.md](FUNCTIONAL_REQUIREMENTS.md).

## Requisitos não funcionais

Os **28 requisitos não funcionais** `RNF-001` a `RNF-028` estão em [NON_FUNCTIONAL_REQUIREMENTS.md](NON_FUNCTIONAL_REQUIREMENTS.md). Eles cobrem isolamento de falhas, segurança, privacidade, desempenho observável, resiliência, dados, compatibilidade, instalação, manutenção, extensibilidade, testabilidade e acessibilidade.

## Dados a persistir

O produto deve persistir somente os dados necessários e autorizados para configuração, metadados não secretos de providers, Assistant Profiles, sessões, estado operacional necessário, regras de moderação e histórico mínimo habilitado. Estatísticas e Persistent Memory não são assumidas como V1. O schema, a biblioteca, migrations e o lifecycle do banco pertencem a Research e Architecture; SQLite permanece `CURRENT DIRECTION + REQUIRES_ADR`.

Credenciais e tokens são secrets e não podem ser armazenados em plaintext no banco ou em arquivos de configuração. A escolha entre Windows Credential Manager, DPAPI ou alternativa segura permanece `REQUIRES_RESEARCH + REQUIRES_ADR`.

## Segurança

Chat, respostas de AI Provider, respostas de APIs, arquivos, configuração importada, persistência e mensagens de integração são entradas não confiáveis. Devem existir validação, limites, moderação, autorização e logging seguro em seus respectivos limites de confiança.

Conteúdo do chat nunca autoriza diretamente ação sensível no OBS Studio. Qualquer capacidade futura desse tipo exige política explícita, allowlist e controle do streamer.

## Constraints, assumptions e riscos

- [Constraints](CONSTRAINTS.md): 15 restrições.
- [Assumptions](ASSUMPTIONS.md): 9 hipóteses explícitas.
- [Riscos](RISKS.md): 14 riscos acompanhados.

## Dependências, Research e ADRs

- [Research Dependencies](RESEARCH_DEPENDENCIES.md): 24 itens do backlog mapeados a impactos de requisito e Architecture.
- [ADR Candidates](ADR_CANDIDATES.md): 10 decisões candidatas, sem decisão antecipada.
- [Open Questions](OPEN_QUESTIONS.md): 15 perguntas não bloqueadoras para o fechamento desta fase, mas algumas bloqueiam decisões posteriores.

## Fora do escopo

Os 15 itens classificados como `OUT_OF_SCOPE_V1` ou `FUTURE` estão em [OUT_OF_SCOPE_V1.md](OUT_OF_SCOPE_V1.md), incluindo Twitch, Kick, telemetria ATS/ETS2, TruckHub, automatic narrator, advanced analytics, RAG, runtime multi-agent, microservices, mensageria distribuída, Kubernetes e cloud infrastructure.

## Rastreabilidade

A relação entre fontes, requisitos, Research, candidatos a ADR e critérios de aceite está em [REQUIREMENTS_TRACEABILITY.md](REQUIREMENTS_TRACEABILITY.md). Prompt History é somente evidência operacional e não foi usado como fonte canônica quando havia documentação aprovada equivalente.

## Status da fase

- Knowledge Quality Gate: **PASSED**
- Requirements: **CONCLUÍDO**
- Requirements Quality Gate: **PASSED**
- Blocking Questions de Requirements: **0**
- Architecture Readiness: **READY**
- Security Requirements detalhados: **NOT STARTED**
- Research técnico externo: **NOT STARTED**
- Architecture: **NOT STARTED**
- Product Source Code: **NOT CREATED**
- Implementation: **NOT STARTED**

Architecture pode iniciar sua preparação com segurança, mas não pode resolver as decisões identificadas sem executar as dependências de Research, a fase de Security Requirements e os ADRs aplicáveis.
