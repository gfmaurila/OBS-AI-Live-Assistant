# Direcionamento de arquitetura

## Direção aprovada

- Modular monolith para o Assistant Core.
- Ports and Adapters para integrações externas.
- Arquitetura de providers para recursos de IA, TTS e chat.
- C# / .NET 10 para o Assistant Core.
- C/C++ apenas para capabilities nativas do OBS que não possam ser atendidas com segurança por integração externa suportada.
- SQLite como direção relacional local para V1.
- Windows 10 e Windows 11 x64; OBS Studio 32.x x64, com compatibilidade declarada por versão testada.
- O Assistant Core executa em processo separado para conter suas falhas e proteger o OBS.

## Limites entre componentes

A integração OBS aprovada é híbrida (ADR-001): o componente nativo mínimo atende lifecycle e apenas capabilities que exigem API nativa; obs-websocket atende estado, eventos e operações já expostos de forma suportada. A capability concreta deve ser justificada e aprovada na Task correspondente. Dock, frontend events e áudio não são automaticamente incluídos em toda Task de plugin.

Chat, IA, TTS, moderação, memória, contexto, providers, persistência, segurança, configuração e regras de negócio permanecem no Assistant Core. O plugin não contém acesso a providers, banco ou rede, e não recebe autoridade para ações sensíveis a partir de conteúdo de chat.

Conforme ADR-003, o Assistant Core é um processo separado. A ausência, falha ou encerramento do Core não pode impedir o OBS de carregar, transmitir ou descarregar o plugin. A comunicação entre processos permanece sujeita a desenho e aprovação próprios.

## Decisões ainda abertas

- Capabilities OBS e eventos de frontend exigidos por cada Task, demonstrados por requisito e evidência técnica.
- Toolchain CMake/MSVC, dependências e versão do OBS efetivamente validadas em Windows x64.
- Mecanismo e compatibilidade do Dock, na Task correspondente.
- Transporte, protocolo, autenticação, limites e lifecycle do IPC C++/.NET, na Task correspondente.
- Roteamento de áudio TTS para OBS, na Task correspondente.
- Proteção de credenciais.
- Biblioteca SQLite, migração, concorrência e recuperação.
- Bibliotecas concretas de IA, TTS e YouTube.
- Viabilidade de provider local.
- Tecnologia de instalação, layout, upgrade, reparo e desinstalação.
- Caminhos exatos de instalação do plugin e dados por versão OBS.

Cada decisão material exige pesquisa técnica oficial e ADR na fase autorizada. A proposta de baseline e os pontos não validados do plugin estão registrados em [Pesquisa do plugin OBS](../docs/research/OBS_PLUGIN_RESEARCH.md) e [Native Build Gate](../docs/testing/NATIVE_BUILD_GATE.md).

### Limites específicos da TASK-024

Dock, captura/roteamento de áudio e IPC pertencem às Tasks que definirem e aprovarem essas capacidades. Eles não fazem parte automaticamente da TASK-024. O skeleton dessa Task mantém health e lifecycle locais em memória, sem criar protocolo ou UI.

## Compatibilidade

Conforme ADR-010, não se promete suporte genérico apenas pelo major do OBS ou pela edição do Windows. A compatibilidade é versionada: registrar a versão mínima proposta, testar cada versão candidata separadamente e declarar suporte apenas para combinações aprovadas com evidência reproduzível. Versões desconhecidas falham de forma segura. A baseline OBS proposta nesta etapa documental ainda não representa aprovação de suporte.

## Não objetivos explícitos para V1

Microservices, Kafka, RabbitMQ, Redis, Kubernetes, infraestrutura cloud, RAG, runtime multi-agent, telemetria ATS/ETS2, integração TruckHub, Twitch, Kick, narração automática e analytics avançados ficam fora de V1, salvo aprovação separada.
