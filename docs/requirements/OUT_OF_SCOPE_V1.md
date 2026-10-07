# Fora do escopo da V1

Os itens abaixo não constituem compromisso da V1. `FUTURE` permite reavaliação mediante nova autorização; `OUT_OF_SCOPE_V1` registra exclusão explícita do escopo atual.

| ID | Item | Classificação | Justificativa / condição de reavaliação |
|---|---|---|---|
| OOS-001 | Twitch como Chat Provider | FUTURE | Reavaliar após estabilização do contrato de chat e autorização de nova fase. |
| OOS-002 | Kick como Chat Provider | OUT_OF_SCOPE_V1 | Não existe requisito nem Research aprovado para a V1. |
| OOS-003 | Telemetria de American Truck Simulator (ATS) | OUT_OF_SCOPE_V1 | Pertence a outro domínio e ampliaria integrações sem requisito aprovado. |
| OOS-004 | Telemetria de Euro Truck Simulator 2 (ETS2) | OUT_OF_SCOPE_V1 | Pertence a outro domínio e ampliaria integrações sem requisito aprovado. |
| OOS-005 | Integração TruckHub | OUT_OF_SCOPE_V1 | Não faz parte do propósito confirmado do assistente de live. |
| OOS-006 | Automatic narrator | OUT_OF_SCOPE_V1 | A V1 responde a trigger ou acionamento manual; narração autônoma ampliaria escopo e risco de áudio. |
| OOS-007 | Advanced analytics | OUT_OF_SCOPE_V1 | Métricas operacionais necessárias não equivalem a produto de analytics. |
| OOS-008 | RAG ou Graph RAG | OUT_OF_SCOPE_V1 | Não há caso de uso aprovado que justifique ingestão, embeddings ou vector database. |
| OOS-009 | Runtime multi-agent no produto | OUT_OF_SCOPE_V1 | Agentes do workflow de engenharia não definem funcionalidade do runtime. |
| OOS-010 | Microservices | OUT_OF_SCOPE_V1 | Conflita com a direção de aplicação desktop local e modular monolith. |
| OOS-011 | Kafka | OUT_OF_SCOPE_V1 | Mensageria distribuída não é necessária para filas locais limitadas. |
| OOS-012 | RabbitMQ | OUT_OF_SCOPE_V1 | Mensageria distribuída não é necessária para filas locais limitadas. |
| OOS-013 | Redis | OUT_OF_SCOPE_V1 | Cache ou broker externo não possui requisito aprovado para a aplicação local. |
| OOS-014 | Kubernetes | OUT_OF_SCOPE_V1 | Orquestração de containers não se aplica à direção local da V1. |
| OOS-015 | Cloud infrastructure obrigatória do produto | OUT_OF_SCOPE_V1 | Providers externos podem ser consumidos, mas o produto não exige backend cloud próprio. |

IA local permanece uma opção condicionada a `RES-016` e decisão do cliente; não é classificada aqui como compromisso nem como exclusão definitiva.
