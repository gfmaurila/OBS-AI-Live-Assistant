# Backlog de Research

Este backlog identifica pesquisas necessárias; nenhuma delas foi executada nesta Task. A prioridade e o momento de execução devem ser definidos após Requirements, de acordo com os riscos e decisões que realmente permanecerem relevantes.

| ID | Tema | Pergunta de pesquisa | Evidência esperada | Alimenta |
|---|---|---|---|---|
| RES-001 | OBS Studio 32.x Plugin SDK | Quais APIs, ABI, headers, bibliotecas e toolchain são oficialmente suportados para plugins x64? | Fontes oficiais, versões, restrições de build e protótipo controlado quando autorizado. | ADR de integração nativa; compatibilidade |
| RES-002 | Headers, libs e build nativo | Como obter, versionar e compilar dependências do OBS sem acoplar o produto ao source tree do OBS? | Procedimento reproduzível e matriz de versões. | Architecture; build; installer |
| RES-003 | Caminhos oficiais de instalação de plugins | Quais diretórios e regras oficiais se aplicam a binários, data files e dados do usuário? | Caminhos suportados, permissões e comportamento por instalação. | Installer; upgrade; uninstall |
| RES-004 | OBS Dock | Quais opções oficiais existem para Dock, UI embutida e ciclo de vida? | Capacidades, limitações, thread model e compatibilidade. | Requirements; ADR de UI/integração |
| RES-005 | Native audio | Quais APIs permitem produzir, monitorar ou encaminhar áudio TTS no OBS com segurança? | Opções oficiais, formatos, latência, threading e failure modes. | ADR de áudio |
| RES-006 | OBS Source e dispositivo virtual | Quando usar source do OBS, monitoramento ou virtual audio device para TTS? | Comparação de UX, configuração, portabilidade e riscos. | Requirements; ADR de áudio |
| RES-007 | OBS WebSocket | Quais comandos, eventos, autenticação e garantias existem no OBS 32.x? | Matriz de capacidades e limites. | Divisão de responsabilidades; ADR |
| RES-008 | Divisão de responsabilidades | O que precisa residir em plugin nativo, Assistant Core e UI/Dock? | Opções comparadas por estabilidade, segurança e manutenção. | Architecture; ADR |
| RES-009 | C++ ↔ .NET IPC | Qual transporte e protocolo atendem autenticação local, versionamento, timeout, cancellation e reconexão? | Comparação com threat model e failure tests propostos. | ADR de IPC |
| RES-010 | Process isolation | Como conter crashes, hangs e backpressure sem bloquear threads do OBS? | Modelo de processos, watchdog/restart e degradação segura. | Requirements não funcionais; Architecture |
| RES-011 | SQLite lifecycle | Qual biblioteca e estratégia cobrem schema, migrations, concorrência, backup, integridade e recuperação? | Comparação técnica e plano de ciclo de vida. | ADR de persistência |
| RES-012 | Windows Credential Manager | Quais limites, APIs, escopo de usuário e comportamento de instalação/upgrade oferece? | Evidência oficial e threat model. | ADR de secrets |
| RES-013 | DPAPI | Quando DPAPI deve ser usada diretamente, combinada a outro store ou evitada? | Escopos, portabilidade, recuperação e riscos. | ADR de secrets |
| RES-014 | YouTube Live Chat API | Quais APIs, OAuth scopes, quotas, polling/stream, revogação e políticas se aplicam? | Fluxos oficiais, limites e failure modes. | Requirements de chat; ADR do adapter |
| RES-015 | AI provider abstraction | Quais capacidades mínimas comuns suportam streaming, modelos, token usage, erros, BYOK e cancellation? | Matriz de providers baseada em requisitos. | Contrato de port; ADR |
| RES-016 | Local AI viability | IA local atende hardware alvo, qualidade, latência, distribuição, licenças e suporte? | Benchmark e matriz de hardware futuros. | Client Decision; ADR de provider |
| RES-017 | TTS abstraction | Quais capacidades mínimas comuns cobrem vozes, streaming, formatos, cancelamento e erros? | Matriz de Windows TTS, Azure Speech, ElevenLabs e opções aprovadas. | Requirements; ADR do adapter |
| RES-018 | Installer | Qual tecnologia Windows instala aplicativo e integração OBS com permissões, assinatura e rollback adequados? | Comparação de tecnologias e protótipo futuro. | ADR de instalação |
| RES-019 | Upgrade | Como atualizar app, plugin, schema e configuração de forma compatível e recuperável? | Política de versionamento, rollback e migrations. | Requirements; ADR de instalação/dados |
| RES-020 | Repair | Como reparar binários e configuração sem apagar dados ou credenciais válidas? | Matriz de componentes reparáveis e testes propostos. | Requirements de lifecycle |
| RES-021 | Uninstall | O que deve ser removido ou preservado, inclusive plugin, logs, dados e secrets? | Política explícita e opções ao usuário. | Requirements de lifecycle |
| RES-022 | OBS compatibility strategy | O suporte será por versão exata ou faixa 32.x, e como a compatibilidade será detectada e testada? | Matriz de versões, política de suporte e regressão. | Client Decision; Testing; release gate |
| RES-023 | Persistent memory e retenção | Se aprovada, como memória persistente atenderá finalidade, consentimento, exportação e exclusão? | Opções de produto e segurança após decisão do cliente. | Requirements; Security; ADR de dados |
| RES-024 | Provider privacy and terms | Quais políticas de retenção, treinamento, região e uso se aplicam aos providers escolhidos? | Comparação vigente no momento da seleção. | Security Requirements; seleção de provider |

## Fora deste backlog atual

RAG, Graph RAG, runtime multi-agent, cloud infrastructure, microsserviços, Twitch e outros providers futuros não justificam pesquisa na V1 sem mudança aprovada de escopo.
