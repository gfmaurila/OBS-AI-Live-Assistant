# Decisões sobre o conhecimento

Este registro decide como o conhecimento disponível será tratado. Ele não contém decisões finais de Architecture e não substitui Requirements, Research ou ADRs.

| Assunto | Fonte | Contexto | Classificação | Estado | Justificativa | Impacto | Próxima ação |
|---|---|---|---|---|---|---|---|
| Limite do produto | `AGENTS.md`; regras de negócio | Aplicativo Windows independente, sem incorporar ou modificar o projeto OBS Truck Live Optimizer. | Regra vigente | ADOPT | Delimita produto e protege o ambiente OBS existente. | Restringe escopo e instalação. | Converter em requisitos de escopo e segurança. |
| Plataforma alvo | Contexto do cliente | Windows 10/11 x64 e OBS Studio 32.x x64. | CURRENT DIRECTION | ADOPT | É a compatibilidade pretendida, ainda sujeita à matriz técnica. | Orienta requisitos de compatibilidade. | Registrar critérios verificáveis; pesquisar matriz OBS. |
| Runtime principal | `AGENTS.md` | .NET 10 e C#, com C/C++ somente se integração nativa exigir. | CURRENT DIRECTION | ADOPT | Direção explícita e limitada pelo cliente. | Restringe stack candidata. | Formalizar em Requirements; confirmar em ADR posterior. |
| Isolamento de processo | `AGENTS.md`; arquitetura atual | Falha de IA, TTS, chat, banco ou rede não deve derrubar OBS. | Restrição de qualidade | ADOPT | É atributo central de confiabilidade. | Afeta limites de processo, IPC e degradação. | Tornar requisito não funcional mensurável. |
| YouTube Live Chat | `AGENTS.md`; contexto do cliente | Canal prioritário da V1. | Escopo V1 | ADOPT | Prioridade explícita, sem presumir detalhes da API. | Define integração principal de chat. | Elaborar requisitos; manter API/OAuth no Research. |
| TTS e falha segura | Contexto do cliente | Voz é parte do produto, mas não pode comprometer OBS nem resposta textual. | Capacidade e restrição | ADOPT | Comportamento funcional e de degradação já é conhecido. | Orienta requisitos e testes de falha. | Detalhar comportamento esperado em Requirements. |
| Entradas não confiáveis e moderação | `agent_docs/security.md` | Chat, provider, modelo, arquivos, APIs e IPC são não confiáveis. | Restrição de segurança | ADOPT | Previne prompt injection e ações indevidas no OBS. | Exige validação, autorização e moderação. | Produzir Security Requirements. |
| Rate limit, timeout e cancellation | `agent_docs/security.md`; dicionário 05 e 08 | Dependências externas e filas precisam de limites. | Restrição operacional | ADOPT | Evita espera infinita, abuso e exaustão de recursos. | Afeta todos os adapters. | Definir critérios em Requirements; parâmetros na Architecture. |
| GitFlow e Quality Gates | Governança; dicionário 11 e 18 | Fluxos do repositório já estão aprovados. | Governança | ADOPT | Controla mudanças sem definir o produto. | Mantém rastreabilidade e separação de fases. | Aplicar em cada Task. |
| Histórico de prompts | `docs/prompts/README.md` | Fonte de rastreabilidade não canônica. | Governança | ADOPT | Preserva a solicitação sem sobrescrever regras vigentes. | Exige prompt no mesmo commit. | Arquivar o prompt desta Task. |
| Provider Architecture | Contexto do cliente; dicionário 05, 08 e 16 | AI, TTS e chat devem usar fronteiras substituíveis. | Padrão candidato | ADAPT | Reduz acoplamento, mas contratos concretos dependem de requisitos. | Afeta extensibilidade e testes. | Definir capacidades mínimas antes da Architecture. |
| BYOK | Contexto do cliente; segurança | Credenciais do usuário sem provider obrigatório. | Modelo candidato | ADAPT | Alinha privacidade, escolha e custos; exige armazenamento seguro. | Afeta configuração, onboarding e secrets. | Detalhar jornadas e restrições em Requirements. |
| Abstração de chat | Contexto do cliente | YouTube V1 e possibilidade futura de outros providers. | Padrão candidato | ADAPT | Evita acoplamento sem trazer Twitch para V1. | Define boundary extensível. | Requerer apenas capacidades necessárias à V1. |
| LiveContext e Assistant Profiles | Contexto do cliente | Conceitos funcionais ainda sem modelo de domínio. | Conceito candidato | ADAPT | Há relevância clara, mas sem estrutura aprovada. | Alimenta descoberta funcional. | Definir necessidades, ciclo de vida e limites em Requirements. |
| Short-Term Memory | Contexto do cliente; dicionário 02, 15 e 17 | Continuidade de sessão com contexto limitado. | Capacidade candidata | ADAPT | Pode apoiar conversação sem implicar memória permanente. | Afeta privacidade, tokens e retenção temporária. | Delimitar valor, limites e descarte em Requirements. |
| Logging e observabilidade | Segurança; dicionário 08, 11 e 16 | Diagnóstico sem secrets ou conteúdo sensível desnecessário. | Prática transversal | ADAPT | Precisa ser dimensionada ao aplicativo desktop local. | Afeta suporte, privacidade e testes. | Definir eventos essenciais e dados proibidos. |
| Estratégia de testes | Engenharia; dicionário 11, 16 e 23 | Unit, integration, compatibility e failure tests conforme risco. | Prática transversal | ADAPT | O dicionário é genérico; a matriz real depende da Architecture. | Sustenta gates determinísticos. | Especificar testabilidade em Requirements e Testing. |
| Providers de IA nomeados | Contexto do cliente | OpenAI, Anthropic, Gemini e OpenRouter são opções, não escopo aprovado. | Catálogo de opções | REFERENCE | Evita vendor lock-in e decisão prematura. | Mantém pesquisa aberta. | Avaliar somente quando Requirements definir capacidades. |
| Providers TTS nomeados | Contexto do cliente | Windows TTS, Azure Speech e ElevenLabs são opções. | Catálogo de opções | REFERENCE | Não há evidência para selecionar provider nesta fase. | Mantém comparação futura. | Pesquisar após requisitos de voz. |
| Padrões genéricos do Kit IA Dev | 20 documentos do dicionário | Boas práticas de engenharia, segurança, provider boundaries e gates. | Referência de processo | REFERENCE | Nem todo padrão serve a aplicativo desktop integrado ao OBS. | Reduz adoção cega. | Adaptar somente com requisito ou risco comprovado. |
| Twitch | `AGENTS.md`; contexto do cliente | Provider de chat posterior à V1. | Escopo futuro | FUTURE | Exclusão explícita do escopo atual. | Evita expansão da V1. | Reavaliar somente com autorização futura. |
| RAG | Dicionário 02 e 15 | Arquitetura de recuperação de conhecimento não requerida atualmente. | Complexidade não justificada | OUT_OF_SCOPE | O produto não possui caso V1 aprovado para RAG. | Evita vector DB, embeddings e ingestão prematuros. | Não levar a Requirements V1. |
| Microsserviços e cloud | Dicionário 13, 16 e 20 | Padrões voltados a sistemas distribuídos/cloud. | Incompatível com direção atual | OUT_OF_SCOPE | A direção é aplicação desktop local e Modular Monolith. | Evita infraestrutura distribuída indevida. | Manter fora da V1. |
| Runtime multi-agent | Dicionário 17, 18 e 23 | Agentes do Kit governam engenharia, não o produto. | Complexidade não requerida | OUT_OF_SCOPE | Não há requisito de multi-agent no runtime. | Evita confundir workflow com funcionalidade. | Não levar a Requirements V1. |
| Plugin nativo, OBS WebSocket e Dock | Arquitetura atual; backlog | Fronteiras de integração competem ou podem se complementar. | Unknown controlado | REQUIRES_RESEARCH | Capacidades e limites oficiais não foram validados. | Pode alterar componentes e segurança. | Executar pesquisas OBS antes do ADR. |
| Roteamento de áudio TTS | Contexto do cliente; backlog | Native audio, source OBS e dispositivo virtual são alternativas. | Unknown controlado | REQUIRES_RESEARCH | Trade-offs de latência, monitoramento e compatibilidade são desconhecidos. | Afeta UX e estabilidade. | Pesquisar APIs e cenários oficiais. |
| Credential Manager e DPAPI | Segurança; backlog | Alternativas ou camadas complementares para secrets locais. | Unknown controlado | REQUIRES_RESEARCH | Threat model, escopo de usuário e ciclo de instalação não foram comparados. | Afeta BYOK e recuperação. | Pesquisar e decidir por ADR. |
| IA local | Dicionário 10; contexto do cliente | Ollama/local provider é possibilidade, não requisito V1. | Opção condicionada | REQUIRES_RESEARCH | Hardware, distribuição, licenças e qualidade variam. | Pode afetar instalador e suporte. | Avaliar viabilidade depois dos requisitos mínimos. |
| Installer, upgrade, repair e uninstall | Contexto do cliente; backlog | Ciclo de vida Windows é obrigatório, tecnologia não escolhida. | Unknown controlado | REQUIRES_RESEARCH | Requer compatibilidade com OBS, dados e credenciais. | Afeta distribuição e suporte. | Pesquisar tecnologias e políticas antes do ADR. |
| SQLite | Contexto do cliente; arquitetura atual | Persistência relacional local é direção V1. | CURRENT DIRECTION / REQUIREMENTS CANDIDATE | REQUIRES_ADR | Biblioteca, lifecycle, concorrência e recuperação ainda estão abertos. | Afeta dados, upgrade e uninstall. | Capturar necessidades; pesquisar; aprovar ADR. |
| Modular Monolith e Ports and Adapters | `AGENTS.md`; dicionário 16 | Direção arquitetural mínima, ainda não desenho final. | CURRENT DIRECTION | REQUIRES_ADR | Deve ser validada contra Requirements e integração OBS. | Orienta boundaries sem fixar estrutura. | Confirmar ou ajustar na fase Architecture por ADR. |
| Persistent Memory | Contexto do cliente; segurança | Memória durável pode conflitar com privacidade e retenção. | Decisão de produto pendente | REQUIRES_CLIENT_DECISION | Finalidade, consentimento e controles não são inferíveis. | Pode ampliar dados e responsabilidades legais. | Formular opções e perguntas na fase Requirements. |
| Advanced Skills | Inventário do Kit e do repositório | Pacote oficial não foi localizado; somente 10 Skills básicas existem. | Dependência de tooling ausente | BLOCKED | Não é permitido inventar ou reconstruir o pacote. | Limita capacidades opcionais do ambiente de IA. | Manter bloqueado; não bloquear Requirements. |

## Contagem por estado primário

| Estado | Quantidade |
|---|---:|
| ADOPT | 10 |
| ADAPT | 7 |
| REFERENCE | 3 |
| FUTURE | 1 |
| OUT_OF_SCOPE | 3 |
| REQUIRES_RESEARCH | 5 |
| REQUIRES_ADR | 2 |
| REQUIRES_CLIENT_DECISION | 1 |
| BLOCKED | 1 |

Total: **33 registros**.
