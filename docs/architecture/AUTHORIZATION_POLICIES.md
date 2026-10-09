# Autorização e políticas de segurança

## Objetivo

A TASK-015 materializa em `ObsAi.Application.Authorization` uma fronteira síncrona e determinística para decidir se uma operação protegida pode prosseguir. A fronteira aplica deny-by-default, menor privilégio, escopo exato por sessão e capability e separa conteúdo não confiável de autoridade.

Nenhuma capability concreta é aprovada por esta implementação. Os identificadores usados por consumidores futuros somente poderão ser autorizados quando estiverem presentes em uma `AuthorizationPolicy` explícita e quando uma autoridade confiável confirmar exatamente a mesma sessão e capability.

## Contratos

- `CapabilityId`: identificador canônico, limitado e adequado a comparação exata.
- `AuthorizationPolicy`: snapshot imutável de uma allowlist; lista vazia nega tudo e não existe fallback allow-all.
- `AuthorizationRequest`: contexto de correlação, lease de operação, capability e origem. Não contém role, permission, identity, conteúdo ou flag de autorização fornecida pelo solicitante.
- `IAuthorizationAuthority`: port da fronteira confiável que verificará a autoridade do streamer. Autenticação, OAuth e armazenamento de credenciais não fazem parte deste contrato.
- `AuthorizationGrant`: evidência limitada a uma sessão e uma capability, retornada pelo port confiável e nunca aceita como campo do request.
- `AuthorizationDecision`: resultado explícito, correlacionável e sem payload, credencial, identidade ou texto de exceção.
- `AuthorizationGate`: policy gate fail-closed.

## Modelo de decisão

O gate permite uma operação somente quando todos os controles abaixo são satisfeitos:

1. request, contexto, lease e capability estão presentes;
2. correlation ID e session ID são válidos;
3. o lease pertence à mesma sessão e continua ativo;
4. a origem é `TrustedControl`; viewer e saída de IA são sempre negados;
5. existe política válida com correspondência exata na allowlist;
6. existe uma implementação de `IAuthorizationAuthority`;
7. a autoridade confirma identidade e permissão;
8. o grant retornado corresponde exatamente à sessão e à capability;
9. o lease continua ativo após a avaliação da autoridade.

Qualquer caminho incompleto, valor desconhecido, divergência de escopo ou exceção resulta em uma razão de negação estável. A revalidação final do lease reduz a janela de revogação durante a chamada ao port. O consumidor ainda deve concluir a operação pelo lifecycle existente e não deve tratar `AuthorizationDecision` como credencial persistente ou reutilizável.

## Limites de confiança

`AuthorizationRequestOrigin` classifica a origem, mas não prova autoridade. Mesmo uma solicitação classificada como `TrustedControl` precisa passar pela allowlist e por `IAuthorizationAuthority`. Chat e AI output não chegam ao port de autoridade e não podem elevar privilégio.

A implementação concreta de autenticação do streamer, a emissão baseada em credenciais, integrações OBS, IPC, providers, persistência e ações protegidas pertencem a Tasks futuras. O adapter que implementar `IAuthorizationAuthority` será uma fronteira confiável e deverá obter identidade por mecanismo autenticado independente do conteúdo do solicitante.

## Segurança e observabilidade

As decisões expõem apenas `CorrelationId`, `SessionId`, `OperationId`, `Capability`, resultado e razão estável. Payloads, identidade, secrets e mensagens internas não fazem parte do modelo. Exceções do port são convertidas em `InternalEvaluationFailure`, sem propagação de detalhes.

## Rastreabilidade

- Requirements: RF-010, RF-020, RF-036; RNF-004, RNF-006, RNF-007.
- Security Requirements: SEC-003 a SEC-007 e SEC-032.
- ADRs: ADR-001, ADR-008 e ADR-011.
- Testes: `AuthorizationGateTests`, `AuthorizationSecurityTests` e `ApplicationAuthorizationArchitectureTests`.

Estado: implementação local concluída; integração e validação pós-merge pendentes.
