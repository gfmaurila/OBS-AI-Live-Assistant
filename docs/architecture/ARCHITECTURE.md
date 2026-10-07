# Architecture Baseline

Data: 2026-10-07. Status: **CONCLUÍDA**. Escopo: baseline conceitual e decisões arquiteturais; implementação permanece **NOT STARTED**.

## Visão geral

O produto adota um **Modular Monolith** em C#/.NET 10, com Ports and Adapters e contratos de providers, executado no processo externo **Assistant Core**. Uma integração nativa C++ mínima permanece dentro do processo do OBS apenas para Dock, lifecycle/frontend events e eventual source de áudio validada. `obs-websocket` é preferido para capacidades já expostas. O boundary entre o plugin e o Core usa Named Pipes locais, protegidos e versionados.

Essa arquitetura preserva a regra central: falha do Assistant Core, chat, AI, TTS, SQLite ou rede não deve se transformar em falha do OBS Studio.

## Goals

- proteger a continuidade do OBS por isolamento de processo e degradação independente;
- suportar YouTube Live Chat V1, AI e TTS multi-provider com BYOK;
- conter entradas e saídas não confiáveis antes de qualquer sink;
- manter concorrência, filas, custo, retenção e recursos limitáveis;
- permitir extensão por adapters sem alterar regras centrais;
- tornar decisões, falhas, segurança e compatibilidade verificáveis.

## Non-Goals

Microservices, brokers externos, Kafka, RabbitMQ, Redis, Kubernetes, cloud infrastructure, RAG, runtime multi-agent, Twitch e analytics avançado não pertencem à V1. Esta baseline não escolhe vendors finais, schema, bibliotecas, valores de timeout/limite, tecnologia final de installer nem implementa qualquer componente.

## Princípios

1. OBS primeiro: código in-process mínimo, não bloqueante e sem lógica de negócio.
2. Negar por padrão: chat, AI output, IPC, provider, arquivo e persistência são não confiáveis.
3. Autoridade não flui com conteúdo: texto nunca se torna comando por interpretação do modelo.
4. Boundaries explícitos: domínio e aplicação dependem de ports, não de OBS, SQLite, Windows ou vendors.
5. Limites antes de escala: filas bounded, timeout, cancellation e backpressure locais.
6. Dados mínimos: secrets separados; memória e retenção por finalidade e sessão.
7. Falha observável e contida: sem retry infinito, sucesso silencioso ou fallback que reduza controles.
8. SOLID pragmático: abstrações somente para boundaries reais e variação comprovada.

## System Context e containers

O streamer opera o Assistant por Dock/controles; viewers interagem pelo YouTube. O Assistant Core conversa com providers externos e com o OBS por dois adapters complementares: `obs-websocket` para RPC/eventos suportados e Native OBS Component para capabilities inevitavelmente in-process. SQLite, Secure Credential Store, configuração e logs são recursos locais separados.

Diagramas: [Context](diagrams/SYSTEM_CONTEXT.md), [Containers](diagrams/CONTAINER_VIEW.md), [Components](diagrams/COMPONENT_VIEW.md) e [Deployment](diagrams/DEPLOYMENT_VIEW.md).

## Componentes e ownership

| Boundary | Responsabilidade | Não pode assumir |
|---|---|---|
| Native OBS Component | lifecycle do módulo, Dock fino, frontend events, bridge IPC, health e eventual source de áudio aprovada | providers, banco, secrets, prompts, moderação ou orquestração |
| OBS WebSocket Adapter | discovery de capabilities, estado/eventos e requests suportados | Dock, source customizada ou bypass da policy de autorização |
| Application Orchestration | sessão, pipeline, casos de uso, queues, timeout/cancellation e degradação | detalhes de vendors ou acesso direto ao OBS |
| Domain | políticas de trigger, moderação, autorização, sessão, contexto e estados | OBS, SQLite, HTTP, Windows APIs ou SDKs de vendors |
| Provider Management | seleção, capability negotiation, erros normalizados e contract enforcement | expor secrets ou enfraquecer controles centrais |
| Infrastructure | adapters YouTube/AI/TTS/OBS, SQLite, secret store e logging | regras de negócio ou autoridade implícita |

## Processos e lifecycle

O OBS carrega apenas o componente nativo. O Assistant Core é processo separado e pode ser iniciado explicitamente pelo streamer ou por launcher externo aprovado; o plugin não deve bloquear o startup do OBS aguardando Core. Startup e shutdown são idempotentes. Perda do Core fecha a sessão IPC, marca o Dock como degradado e mantém o OBS operante. Reconexão exige nova negociação de versão, identidade de sessão e health. Resultados de sessão anterior ou cancelada são descartados.

Shutdown ordenado: parar novas entradas, cancelar operações, fechar queues, impedir novas saídas, concluir somente transações seguras, persistir estado autorizado e encerrar IPC. Crash recovery nunca retoma automaticamente publicação ou TTS de trabalho ambíguo. Job Objects podem ser avaliados na implementação, mas não são requisito do boundary.

## IPC

Named Pipes locais são o transporte aceito. O endpoint deve aplicar DACL explícita ao usuário/logon SID, negar acesso de rede, limitar instâncias e tamanho e validar ambos os peers conforme o modelo implementado. O envelope conceitual contém `protocolVersion`, `messageType`, `messageId`, `correlationId`, `sessionId`, timestamp/expiry e payload versionado.

O contrato suporta request/response e events; cada request possui timeout e cancellation. Mensagens desconhecidas, oversize, repetidas, expiradas, fora de sequência ou incompatíveis são recusadas. Backpressure impede o plugin de acumular eventos. Reconnect não reaproveita autoridade nem estado de sessão anterior. O protocolo detalhado pertence ao Integration Design.

## Pipeline

```text
Live Chat / entrada manual
-> Message Listener
-> Normalization + schema/size validation
-> Trigger Detection
-> Moderation + blocklist
-> Rate Limiter
-> bounded Request Queue
-> Context Builder
-> AI Provider Adapter
-> Response Validation/Moderation
-> bounded Response Queue
-> Text Output e/ou bounded TTS Queue
-> Output Adapter autorizado
```

Cada transição preserva `correlationId`, `sessionId`, cancellation e resultado classificado. Requests, respostas e TTS usam queues locais bounded; não há broker externo. A capacidade e a concorrência são configurações validadas dentro de limites seguros. Saturação rejeita ou descarta segundo política explícita, nunca expande memória. Uma fila lenta não bloqueia as demais.

## Contexto e memória

`LiveContext` é um modelo conceitual de sessão e pode conter streamer, título da live, plataforma, jogo, cena OBS observada, Assistant Profile, interações recentes limitadas e contexto customizado autorizado. Campos são allowlisted por finalidade; scene/eventos não são autoridade.

Short-Term Memory é limitada, pertence à sessão e é descartável. Persistent Memory permanece **DEFERRED** e desabilitada no baseline até decisão sobre finalidade, consentimento, retenção e exclusão. A extensão futura deve usar um port próprio e não alterar o pipeline nem tornar retenção obrigatória.

## Providers

Ports conceituais equivalentes a `IChatProvider`, `IAiProvider` e `ITtsProvider` representam contratos mínimos, capabilities opcionais e erros normalizados. Adapters candidatos: YouTube V1; OpenAI, Anthropic, Gemini, OpenRouter, Ollama e endpoints OpenAI-compatible; Windows TTS, Azure Speech e ElevenLabs. A lista não aprova todos para V1.

Contratos recebem referências de credencial, nunca valores na configuração de domínio. Streaming, usage, formato de áudio e cancellation são capabilities negociadas, não promessas universais. LSP exige que um adapter não declare capability que não cumpra; ISP separa chat, AI, TTS, secret store, persistence, OBS e observability; DIP mantém aplicação dependente dos ports.

## Persistence

SQLite é aceito para persistência local V1 de Settings, provider metadata não secreta, Profiles, Sessions, Interactions somente quando habilitadas, regras de Moderation, estado operacional necessário e Statistics somente se aprovadas. Commands sensíveis não são armazenados como autoridade reutilizável. Secrets nunca ficam no banco.

O adapter mantém single-writer, transações curtas, migrations versionadas, backup consistente pela Backup API, health/integrity check, retenção por categoria e recovery explícito. Se WAL for escolhido no Data Design, a versão deve incluir a correção indicada por RES-011. Cópia do arquivo em uso não é backup. Schema, biblioteca e política quantitativa pertencem ao Database Design.

## Secure Credential Storage

Windows Credential Manager é aceito para API keys, OAuth access/refresh tokens e outros secrets discretos por usuário. DPAPI user-scope é complementar somente para blobs app-owned que não se ajustem ao Credential Manager. DPAPI machine-scope não é default. SQLite armazena apenas referência opaca e metadata não secreta.

UI, logs, prompts, crash reports e diagnósticos nunca recebem o valor integral. Rotação/substituição é atômica; disconnect remove uso local e orienta revogação no provider. Backup, repair e uninstall obedecem política explícita e não exportam secrets.

## Security Architecture

Os oito trust boundaries estão representados em [Security Boundaries](diagrams/SECURITY_BOUNDARIES.md). Os controles cobrem SEC-001..034:

- validação de schema, encoding, tamanho, versão e estado em cada boundary;
- moderação de entrada e saída, rate limiting por viewer/global e queues finitas;
- instruções confiáveis separadas de conteúdo; prompt injection não concede tool/action authority;
- policy engine/authorization gate deny-by-default antes de qualquer ação sensível;
- allowlist de capabilities OBS e confirmação do streamer quando uma ação futura exigir;
- redaction central e allowlist de campos de log;
- credentials por referência e least privilege por processo, arquivo, OAuth scope e adapter;
- provider isolation, TLS/destino oficial e validação de resposta;
- DACL, autenticação/autorização, versionamento e limites no IPC;
- pacote assinado, integridade, rollback e supply-chain gates no installer.

Não se alega segurança absoluta. Os riscos permanecem `OPEN / CONTROLLED` até implementação e testes demonstrarem os controles.

## OBS Integration e UI

O Dock é apresentação e controle: enable/disable, perfil, provider/connection status, TTS, fila resumida, diagnóstico redacted e acesso a settings. Configurações sensíveis usam masking e confirmação; o Dock não recupera secrets. Processamento pesado e I/O ficam fora das threads do OBS.

`obs-websocket` é preferido para consultar estado e executar requests expostos; autenticação permanece habilitada mesmo em localhost e capabilities são descobertas por `GetVersion`. O componente nativo cobre somente Dock, lifecycle/frontend events e áudio nativo se ADR-006 for validado. A [matriz e fluxo](diagrams/OBS_INTEGRATION_FLOW.md) evitam sobreposição.

## TTS e áudio

TTS generation ocorre no Core após validação do texto. O resultado entra em fila bounded com duração máxima, cancelamento, volume/mute e lifecycle explícitos. Falha de TTS não bloqueia texto. A reprodução preferida para protótipo é áudio do processo externo capturado pelo OBS; source nativa é alternativa se controle de track/mixer comprovadamente exigir. Virtual audio driver não é baseline.

ADR-006 permanece `PROPOSED`: latência, cancelamento, monitoramento, mute, gravação/stream tracks, eco e crash devem ser validados em protótipo. Nunca há rede ou síntese em callback do OBS.

## Observability

Logging local estruturado usa event IDs, severity, `correlationId` e health resumido por adapter. Redaction ocorre antes de qualquer sink; payloads completos, chat/AI completos, tokens, auth headers e query values sensíveis ficam fora por padrão. Arquivos têm rotação, limite total e retenção configurável. Health distingue healthy, degraded, disconnected, saturated e failed. Métricas locais incluem profundidade/rejeição das queues, latência por etapa, timeout/cancellation e falha por provider. Não há infraestrutura cloud obrigatória.

## Error handling e resiliência

Timeout e cancellation atravessam ports. Retries são limitados, com backoff/jitter e apenas para falhas transientes/idempotentes; respostas publicadas, TTS iniciado e operações de escrita não são repetidos cegamente. Falha repetida pode suspender temporariamente um adapter. IPC disconnect, erro de banco, falha TTS e chat disconnect produzem estados separados e recuperação explícita. Nenhum retry é infinito.

## Deployment, installer e update

O deployment está em [Deployment View](diagrams/DEPLOYMENT_VIEW.md). O futuro `OBS-AI-Live-Assistant-Setup.exe` detectará OBS/Windows/arquitetura, exigirá OBS fechado para trocar plugin, instalará Core e plugin como componentes separados, verificará prerequisites, versão/layout e assinatura, preservará dados autorizados e oferecerá rollback, repair e uninstall por categoria.

Update V1 será manual ou installer-based, com installer-based preferido; in-app automatic updater fica `FUTURE`. WiX/Burn e Inno Setup permanecem finalistas para spike. Code signing Authenticode e timestamp, hash/origem verificáveis e dependências rastreáveis são release gates. O default de remover/preservar dados, logs e secrets requer decisão do cliente antes do Installation Design final.

## Compatibility

A baseline técnica é OBS Studio 32.x x64 e Windows 11 x64. Cada release declara versão mínima e matriz realmente testada, negocia RPC/capabilities e falha fechado em combinação desconhecida. Não há promessa de ABI irrestrita entre versões.

C#/.NET 10 permanece a direção do Core. A pesquisa identificou que o suporte oficial atual do .NET 10 ao Windows 10 é restrito às edições LTSC/Enterprise listadas; portanto Windows 10 genérico não é prometido. A decisão entre restringir edições ou revisar target/runtime é `REQUIRES_CLIENT_DECISION` antes do release e não bloqueia backlog.

## SOLID e dependency rules

- **SRP:** cada módulo possui um motivo de mudança; integração OBS, providers, segurança, persistência e UI são separados.
- **OCP:** adapters são adicionados por registro/composição, sem editar políticas centrais.
- **LSP:** contract tests exigem semântica comum e capability honesty.
- **ISP:** ports pequenos por capacidade evitam interfaces universais de provider/OBS.
- **DIP:** Domain/Application dependem de ports; adapters dependem desses contratos.

Direção: `Domain <- Application <- Adapters/Infrastructure <- Host/Composition`. `ObsBridge.Contracts` é contrato estreito compartilhado, sem SDK do OBS. Domain não referencia OBS, SQLite, YouTube, vendors TTS/AI ou Windows APIs. O Native Plugin não referencia assemblies de negócio.

## Test Architecture

- Unit: políticas, estados, moderação, autorização, context e retry classification.
- Integration: SQLite, secret store, providers simulados e logging/redaction.
- Architecture Tests: dependências e ausência de vendor/OBS no Domain.
- IPC Tests: framing, versão, DACL, malformed/oversize, timeout, reconnect e cancellation.
- Provider Contract Tests: capabilities, erros, limites e substituibilidade.
- Failure Isolation Tests: crash/hang/disconnect do Core e adapters sem afetar OBS.
- Security Tests: prompt injection, output abuse, sentinel secrets, authz e DoS controlado.
- Installer Tests: clean install, upgrade, rollback, repair, uninstall e adulteração.
- OBS Compatibility Tests: matriz declarada, Dock, WebSocket e áudio aprovado.

Nenhum teste executável é criado nesta fase.

## Failure modes

| Falha | Comportamento arquitetural |
|---|---|
| Core crash/hang | plugin desconecta, Dock mostra degradado, OBS continua; restart/reconnect explícito |
| IPC inválido/desconectado | recusa/fail closed, sem bloquear thread OBS ou repetir ação |
| Chat offline/quota | entrada manual permanece; backoff limitado |
| AI indisponível | request termina classificado; fila libera capacidade; nenhuma saída falsa |
| TTS indisponível | texto aprovado continua; áudio é descartado/cancelado conforme política |
| SQLite locked/corrupt | escrita não vira sucesso; modo degradado/recovery; OBS independente |
| Queue saturated | nova carga é recusada/descartada observavelmente |
| Secret ausente/revogado | provider fica indisponível, sem ecoar credencial |
| Shutdown | novas entradas cessam; trabalho cancela; saída tardia é ignorada |

Detalhes: [Failure Isolation](diagrams/FAILURE_ISOLATION.md).

## ADR Summary

Onze ADRs formalizam estilo, integração OBS, IPC, isolamento/queues, providers/persistence, secrets, TTS, installer/update, UI/configuração, memória/retenção e compatibilidade. Sete estão `ACCEPTED`, três `PROPOSED` e um `DEFERRED`. Consulte [decisions/README.md](decisions/README.md).

## Traceability

O [Architecture Decision Map](ARCHITECTURE_DECISION_MAP.md) liga RF/RNF, SEC, Research, ADR, componentes e futuras fases. Requirements Traceability e Security Traceability foram estendidas sem duplicar critérios canônicos.

## Known Limitations e Open Decisions

- providers concretos de AI/TTS V1: decisão de produto antes das respectivas Tasks;
- Ollama: OPTIONAL/FUTURE;
- Persistent Memory: desabilitada/DEFERRED;
- prazos de retenção e defaults de uninstall: decisão do cliente;
- TTS routing e tecnologia do installer: exigem spike;
- faixa comercial OBS 32.x e política Windows 10/.NET 10: decisão pré-release;
- valores quantitativos de filas, cooldown, timeout e duração: definidos por threat model, baseline e testes.

São sete grupos de decisões abertas, todos não bloqueantes para decomposição do backlog.

## Implementation Guidance

Implementação futura deve respeitar os boundaries e criar contratos antes dos adapters. Composition roots são os únicos locais que conhecem implementações. Nenhum provider acessa DB/secret store fora de port explícito. Toda nova capability OBS requer policy, allowlist e threat review. Mudanças de protocolo, dados ou compatibilidade exigem versão/migration e atualização do ADR. Gates futuros devem incluir deterministic build/test, architecture tests, secret scan e matrizes de falha/compatibilidade.

## Readiness

Architecture Quality Gate: **PASSED**. Security Architecture Review: **PASSED**. Backlog Readiness: **READY**. Próxima fase autorizável: Backlog + Dependency Graph + Implementation Tasks; implementação continua proibida até o Client Approval Gate.
