# Modelo de domínio de sessão, perfil e contexto

Status: **IMPLEMENTADO PELA TASK-006**.

Este documento registra o contrato de domínio mínimo para sessão, perfil do assistente e contexto ao vivo. O modelo reside em `ObsAi.Domain`, não depende de adapters ou infraestrutura e não implementa orquestração, persistência ou memória persistente.

## Modelo

### `AssistantProfile`

Representa a identidade e as instruções editoriais aprovadas para o assistente:

- `AssistantProfileId` é um identificador opaco e não vazio;
- nome, personalidade, estilo de resposta e instruções de comportamento são obrigatórios;
- textos são normalizados nas bordas, devem ser imprimíveis e respeitam limites fornecidos explicitamente pelo chamador;
- atualização produz uma nova instância e preserva a identidade;
- a representação textual expõe somente o identificador, sem repetir instruções potencialmente sensíveis.

### `LiveContext`

Representa apenas o contexto efêmero de uma live:

- pertence a exatamente um `SessionId`;
- aceita somente campos do enum `LiveContextField`: streamer, título da live, plataforma, jogo, cena do OBS e contexto personalizado;
- limita quantidade de campos, tamanho individual e tamanho agregado conforme `LiveContextLimits` fornecido pelo chamador;
- rejeita campos desconhecidos, valores vazios, caracteres de controle e estouro de limites;
- não possui mecanismo de persistência nem superfície de Persistent Memory;
- ao término da sessão, limpa os valores e bloqueia qualquer leitura posterior.

### `AssistantSession`

Delimita um perfil e um contexto ao vivo:

- começa no estado `Active`, com identificador, perfil, contexto da mesma sessão e timestamp UTC;
- rejeita contexto pertencente a outra sessão e timestamps não UTC;
- termina uma única vez, sem aceitar cronologia anterior ao início;
- no encerramento, limpa o `LiveContext`, registra o timestamp e passa para `Ended`;
- uma sessão encerrada não expõe nem reutiliza o contexto.

## Limites e configuração

O domínio valida limites, mas não define números de produto. A origem e os valores configuráveis serão tratados pelas Tasks de configuração e composição apropriadas. Essa separação evita antecipar `TASK-014` e mantém o domínio determinístico.

## Fora de escopo

- lifecycle/orquestração do Assistant Core (`TASK-007`);
- construção operacional de contexto e Short-Term Memory (`TASK-011`);
- persistência SQLite (`TASK-019` a `TASK-021`);
- providers, rede, autenticação, TTS, chat ou comandos OBS;
- memória persistente, desabilitada conforme ADR-009.

## Rastreabilidade

| Critério da TASK-006 | Implementação | Evidência automatizada |
|---|---|---|
| Contexto allowlisted, limitado e por sessão | `LiveContextField`, `LiveContextLimits`, `LiveContext` | `LiveContextTests`, `DomainDataMinimizationTests` |
| Encerramento impede reutilização | `AssistantSession.End` e `LiveContext.Clear` | `AssistantSessionTests`, `DomainDataMinimizationTests` |
| Persistent Memory permanece ausente | ausência de tipo, port ou persistência no domínio | `DomainModelArchitectureTests`, `DomainDataMinimizationTests` |
| Sem capability/provider/infraestrutura fora do escopo | assembly `ObsAi.Domain` sem referência externa | `DomainModelArchitectureTests`, `ProjectDependencyTests` |
| Perfil com invariantes explícitas | `AssistantProfile`, `AssistantProfileLimits` | `AssistantProfileTests` |

Requisitos cobertos: RF-003, RF-004, RF-015, RF-029; RNF-005, RNF-018, RNF-023, RNF-026; SEC-024, SEC-025 e SEC-031. Decisões aplicadas: ADR-008, baseline efêmera da ADR-009 e ADR-011.
