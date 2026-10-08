# Validação e normalização de entradas

Status: **IMPLEMENTADO PELA TASK-009**.

Este documento registra o contrato de validação e normalização de entradas do Assistant Core: a fronteira que transforma chat não confiável em uma representação funcional comum, rejeitando conteúdo ausente, malformado, mal codificado ou acima do limite antes de qualquer trigger, moderação, fila ou provider (RF-006, RF-007, RNF-004, RNF-005, SEC-001, SEC-002, SEC-019, SEC-024). A implementação reside em `ObsAi.Application/Validation` e é intencionalmente **session-agnostic**, **vendor-neutral** e **síncrona**: não referencia sessão, perfil, contexto, providers, persistência, rede, secrets ou integração com o OBS.

## Posição no fluxo (ADR-004, ADR-008)

Conforme `diagrams/CHAT_AI_TTS_FLOW.md`, a entrada atravessa primeiro esta fronteira:

```text
Chat (YouTube / Manual) -> Schema + Size Validation -> Normalization -> Trigger Detection -> Moderation -> ...
```

Qualquer entrada rejeitada aqui **nunca alcança** trigger, moderação, fila bounded ou provider (SEC-002, THT-001): a API é `falha-fechada` e devolve um diagnóstico seguro (`InputRejection`) em vez do conteúdo. Este contrato cobre somente entrada; validação e moderação de saída pertencem às Tasks de pipeline (TASK-010, TASK-012).

## Representação comum

`ChatMessage` (TASK-005) é o transporte vendor-neutral **não confiável** que chega do adapter de chat. Após validação, a fronteira produz `NormalizedChatMessage`, a representação funcional comum com exactamente seis campos:

| Campo | Função | Preservado por |
|---|---|---|
| `ProviderId` | Origem (provider de chat) | segurança/anti-abuso |
| `ChannelReference` | Canal da transmissão | escopo da sessão |
| `SenderReference` | Identidade operacional mínima do remetente | cooldown/anti-abuso por viewer |
| `MessageReference` | Referência opaca da mensagem | deduplicação (flood/repetido) |
| `Text` | Texto normalizado | conteúdo funcional |
| `ReceivedAtUtc` | Timestamp de recebimento | ordenação/diagnóstico |

Nenhum outro metadado é carregado adiante (RNF-005, SEC-024): a representação preserva só o necessário para os controles de abuso e deduplicação de Tasks posteriores.

## Validação de schema, encoding e tamanho

`ChatInputValidator.Validate(input, limits)` retorna `ChatInputValidationResult` (aceito com `NormalizedChatMessage` OU rejeitado com `InputRejection`; nunca ambos, nunca nenhum). Dados não confiáveis nunca lançam; apenas `null` de argumento ou limites inválidos lançam, como erro de contrato do programador.

Ordem determinística dos controles, por campo:

1. **Presença** — referências (channel/message/sender) e texto `null` → `MissingRequiredField`.
2. **Limite de tamanho bruto** — texto e referências acima dos respectivos limites são rejeitados **antes** de qualquer normalização, varredura integral ou alocação (`OversizeText`/`OversizeReference`), evitando custo premium sob carga adversária (SEC-019).
3. **Campos obrigatórios** — referências e texto vazios ou somente espaços → `MissingRequiredField`.
4. **Encoding bem-formado** — surrogate órfão (alto/baixo sem par) em texto ou referência → `MalformedEncoding` (Unicode malformado).
5. **Imprimibilidade/schema** — caractere de controle em referência → `MalformedReference`; no texto → `MalformedText`.
6. **Normalização** — texto passa por NFC (Form C), `Trim` e colapso de execuções de espaço para um único espaço; referências são apenas aparadas. Entradas equivalentes (espaços redundantes; pré-composta vs. decomposta) produzem o mesmo campo comum (RF-007).
7. **Limite de tamanho normalizado** — texto ou referência acima do limite configurado → `OversizeText`/`OversizeReference`.
8. **Timestamp** — `ReceivedAtUtc == default` → `MalformedTimestamp`.

`InputRejectionReason` é o diagnóstico seguro: carrega somente a classificação, nunca o payload, o texto ou a identidade operacional, e pode ser surfacido em logs sem vazar conteúdo (RNF-004, SEC-024).

## Configuração e limites

`InputValidationLimits.Create(maximumTextLength, maximumReferenceLength)` valida e congela os limites:

- ambas as dimensões `>= 1`; tudo o mais lança `ArgumentOutOfRangeException`.
- O registro é imutável e não possui setters públicos.
- Os valores numéricos de produto **não são fixados pela arquitetura** (SEC-002, `INPUT_OUTPUT_SECURITY.md`): os limites são configuráveis pelo streamer numa Task futura de configuração (TASK-014); esta Task entrega o mecanismo, não números de produto.

## Fail-closed

A superfície é deliberadamente estreita:

- `ChatInputValidator` é uma operação pura, síncrona e sem estado.
- `ChatInputValidationResult` só pode ser construído pelas fábricas `Accepted`/`Rejected` (construtor privado), garantindo a invariante exatamente-um-estado.
- O namespace declara apenas os seis tipos aprovados; nenhuma interface de port, nenhum sink, nenhuma capability opcional.
- `ObsAi.Application` continua referenciando somente `ObsAi.Domain` (regra 0/1 de dependência exigida pelos Architecture Tests).

## Fora de escopo

- triggers, moderação/blocklist e rate limiting (TASK-010);
- autorização e políticas de segurança (TASK-015);
- recepção, quota e reconexão do YouTube (TASK-031);
- configuração persistente dos limites (TASK-014);
- validação/moderação de saída (pipeline de resposta);
- números de produto para limites.

## Rastreabilidade

| Critério da TASK-009 | Implementação | Evidência automatizada |
|---|---|---|
| Input ausente/malformado/oversize falha antes de provider/fila | `ChatInputValidator` falha-fechado com `InputRejection` e `Message = null` | `ChatInputValidationTests`, `ChatInputValidationSecurityTests` |
| Dados normalizados preservam apenas o necessário | `NormalizedChatMessage` com exatamente 6 campos mínimos | `ChatInputValidationTests`, `ChatInputValidationContractTests` |
| Identidade operacional mínima e origem preservadas | `ProviderId`, `ChannelReference`, `SenderReference`, `MessageReference` | `ChatInputValidationSecurityTests` |
| Encoding e esquema validados antes de qualquer sink | surrogate órfão, caracteres de controle e timestamp ausente rejeitados | `ChatInputValidationTests`, `ChatInputValidationSecurityTests` (flood adversarial, 5.000 oversize) |
| Limites configuráveis sem números de produto | `InputValidationLimits.Create` imutável (dimensionamento `>= 1`) | `ChatInputValidationTests`, `ApplicationInputValidationArchitectureTests` |
| Diagnóstico seguro sem vazar payload | `InputRejection` sem acesso ao payload; `ToString` redigido | `ChatInputValidationSecurityTests`, `ChatInputValidationContractTests` |
| Escopo sem capability/provider/infra fora da V1 | namespace com 6 tipos, síncrono, sem ports/sinks | `ApplicationInputValidationArchitectureTests` |

Requisitos cobertos: RF-006, RF-007; RNF-004, RNF-005; SEC-001, SEC-002, SEC-019, SEC-024. Decisões aplicadas: ADR-004 (validação nos boundaries antes do uso) e ADR-008 (controles centrais, sem confiar em UI).
