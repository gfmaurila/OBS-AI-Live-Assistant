# Contracts e ports da aplicação

Status: **IMPLEMENTED BY TASK-005**. Este documento descreve somente as fronteiras materializadas em `ObsAi.Application`; adapters e políticas continuam nas Tasks próprias.

## Objetivo e limites

Os contracts isolam a aplicação de SDKs, vendors, SQLite, APIs do Windows e detalhes do OBS. Eles transportam apenas modelos vendor-neutral e propagam `CancellationToken` em toda operação assíncrona. Não há seleção de provider, chamada de rede, persistência, acesso a segredo, retry, timeout, autorização ou ação sensível do OBS nesta entrega.

## Superfícies

| Boundary | Port principal | Capability opcional / contrato auxiliar |
|---|---|---|
| Chat | `IChatProvider` | `IChatPublisher` somente para adapters que realmente publicam |
| AI | `IAiProvider` | `IAiStreamingProvider` somente para streaming real |
| TTS | `ITtsProvider` | `ITtsStreamingProvider` somente para streaming real |
| Persistência | `IReadRepository<TEntity,TIdentifier>` | `IWriteRepository<TEntity,TIdentifier>` separado por ISP |
| Secrets | `ISecretReferenceStore` | verifica/remove referência opaca; não retorna material secreto |
| OBS | `IObsStatusReader` | `IObsTextOutput` estreito; não oferece comando OBS genérico |
| Observabilidade | `IOperationalEventSink` | `IHealthReporter` com mensagens e resumos explicitamente seguros |

## Garantias do contrato

- `CredentialReference` contém somente identificador opaco, normalizado e com representação textual redigida; requests de provider nunca recebem API key, token ou secret.
- `ChatMessage`, `AiResponse` e chunks externos são nomeados como conteúdo não confiável; validação efetiva pertence às Tasks de pipeline e segurança.
- `ProviderFailureCode`, `ProviderFailure` e `ProviderResult<T>` formam a classificação comum sem carregar exception, payload bruto ou detalhe de vendor.
- Streaming e publicação são interfaces independentes. Implementar o port básico não declara essas capabilities.
- `RequestContext` preserva correlação, sessão e deadline opcional sem definir políticas quantitativas.
- O port OBS publicado nesta Task permite leitura de status e saída textual aprovada; não concede autoridade para interpretar chat/AI como comando.

## Rastreabilidade e testes

Requirements: `RF-007`, `RF-016`, `RF-019`, `RF-024`, `RF-027`, `RF-028`; `RNF-009` a `RNF-011`, `RNF-017` a `RNF-019`. Segurança: `SEC-001`, `SEC-005`, `SEC-016` a `SEC-018`, `SEC-032` a `SEC-034`. Decisões: ADR-002, ADR-004 e ADR-011.

Os testes Unit verificam os invariantes de referências e resultados. Os Contract Tests verificam cancelamento, ausência de valores de secrets, classificação comum de falhas e honestidade das capabilities opcionais. Os Architecture Tests verificam ownership dos ports, ausência de namespaces/vendors concretos e manutenção das dependências permitidas.
