# Escopo do projeto

Esta baseline classifica o contexto conhecido sem criar novos requisitos.

## V1 Current Direction

- Aplicação Windows local e independente, compatível com OBS Studio 32.x x64.
- Interação de espectadores priorizada por YouTube Live Chat e interação manual do streamer.
- Respostas configuráveis de IA em texto e de TTS em voz.
- BYOK e limites de providers para IA e TTS.
- Persistência relacional local com SQLite como direção atual.
- Proteção forte de secrets e isolamento de processo em relação ao OBS.
- Modular Monolith com Ports and Adapters como direção atual do Assistant Core.

Cada item permanece sujeito aos gates aplicáveis de Requirements, Research, Security, ADR e aprovação do cliente.

## Future

- Twitch e outros providers de chat.
- Providers adicionais de IA e TTS validados contra contratos aprovados.
- Suporte a IA local caso a pesquisa técnica comprove sua viabilidade.

Itens futuros não são compromissos da V1.

## Out of Scope V1

- Microservices e infraestrutura de mensageria distribuída.
- Kafka, RabbitMQ, Redis, Kubernetes e infraestrutura de cloud.
- RAG e runtime multi-agent no produto.
- Twitch, Kick, narração automática e analytics avançados, salvo aprovação específica.
- Funcionalidades do projeto de referência CMS/Azure, incluindo CMS, multi-tenancy, aplicações React e serviços específicos de cloud.
