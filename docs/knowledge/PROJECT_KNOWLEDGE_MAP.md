# Mapa de conhecimento do projeto

Este mapa relaciona as fontes disponíveis aos conceitos relevantes para o OBS-AI-Live-Assistant. As classificações controlam como cada conceito pode alimentar Requirements; elas não aprovam Architecture nem autorizam implementação.

## Fontes avaliadas

- contrato e documentação canônica deste repositório;
- 20 documentos do Knowledge Dictionary em `D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\dicionario`;
- inventário das 10 Skills básicas e verificação da ausência do pacote oficial de Advanced Skills;
- histórico de prompts, usado somente para rastreabilidade e nunca como fonte canônica.

## Mapa

| Fonte | Conceito | Relevância para o projeto | Classificação | Candidato a requisito | Research | ADR | Future / Out of Scope |
|---|---|---|---|---|---|---|---|
| Contexto do cliente; `agent_docs/business-rules.md` | OBS Studio 32.x x64 | Plataforma hospedeira e limite de compatibilidade pretendido. | ADOPT | Sim | Sim | Sim | Não |
| `agent_docs/architecture.md`; backlog | OBS Native Plugin | Pode fornecer capacidades que não estejam disponíveis fora do processo do OBS. | REQUIRES_RESEARCH | Sim, condicionado | Sim | Sim | Não |
| `agent_docs/architecture.md`; dicionário 05 e 16 | OBS WebSocket | Candidato a integração externa com menor acoplamento ao processo do OBS. | REQUIRES_RESEARCH | Sim, condicionado | Sim | Sim | Não |
| Contexto do cliente; backlog | OBS Dock | Opção de interface integrada cujo suporte e ciclo de vida precisam ser validados. | REQUIRES_RESEARCH | Sim, condicionado | Sim | Sim | Não |
| Contexto do cliente; backlog | OBS Audio | Necessário para reproduzir TTS sem comprometer monitoramento ou estabilidade. | REQUIRES_RESEARCH | Sim | Sim | Sim | Não |
| `agent_docs/architecture.md` | C/C++ | Permitido somente para capacidade nativa OBS comprovadamente necessária. | ADAPT | Sim | Sim | Sim | Não |
| `AGENTS.md`; contexto do cliente | .NET 10 | Runtime principal definido como direção atual. | ADOPT | Sim | Não | Sim | Não |
| `AGENTS.md`; contexto do cliente | C# | Linguagem principal definida como direção atual. | ADOPT | Sim | Não | Sim | Não |
| `agent_docs/architecture.md`; backlog | IPC | Fronteira provável entre componentes nativos e Assistant Core. | REQUIRES_RESEARCH | Sim | Sim | Sim | Não |
| `AGENTS.md`; `agent_docs/architecture.md` | Process Isolation | Falhas de IA, TTS, chat ou rede não podem derrubar o OBS quando tecnicamente possível. | ADOPT | Sim | Sim | Sim | Não |
| Contexto do cliente; `agent_docs/architecture.md` | SQLite | Direção V1 para persistência relacional local, ainda não uma decisão final. | REQUIRES_ADR | Sim | Sim | Sim | Não |
| Backlog; dicionário 01, 11 e 16 | Database Lifecycle | Migrations, concorrência, backup, corrupção, retenção e permissões precisam de desenho explícito. | REQUIRES_RESEARCH | Sim | Sim | Sim | Não |
| Contexto do cliente; `agent_docs/security.md`; dicionário 08 | BYOK | Usuário fornece credenciais dos providers sem vendor lock-in obrigatório. | ADAPT | Sim | Sim | Sim | Não |
| `agent_docs/security.md`; backlog | Credential Storage | Secrets não podem ficar em texto puro, banco, documentação ou logs. | ADOPT | Sim | Sim | Sim | Não |
| Backlog; contexto de segurança | Windows Credential Manager | Candidato para proteção de credenciais por usuário no Windows. | REQUIRES_RESEARCH | Sim, condicionado | Sim | Sim | Não |
| Backlog; contexto de segurança | DPAPI | Candidato ou componente de proteção local, dependente de threat model e ciclo de vida. | REQUIRES_RESEARCH | Sim, condicionado | Sim | Sim | Não |
| Contexto do cliente; dicionário 08, 10 e 23 | AI Providers | Fronteira multi-provider necessária para BYOK e degradação controlada. | ADAPT | Sim | Sim | Sim | Não |
| Contexto do cliente | OpenAI | Provider candidato; não obrigatório nem aprovado para V1. | REFERENCE | Sim, como opção | Sim | Não | Não |
| Contexto do cliente | Anthropic | Provider candidato; não obrigatório nem aprovado para V1. | REFERENCE | Sim, como opção | Sim | Não | Não |
| Contexto do cliente | Gemini | Provider candidato; não obrigatório nem aprovado para V1. | REFERENCE | Sim, como opção | Sim | Não | Não |
| Contexto do cliente; dicionário 10 | Ollama | Opção de IA local cuja viabilidade depende de hardware, distribuição e suporte. | REQUIRES_RESEARCH | Sim, condicionado | Sim | Sim | Não |
| Contexto do cliente | OpenRouter | Gateway candidato; termos, privacidade, compatibilidade e limites não foram avaliados. | REFERENCE | Sim, como opção | Sim | Não | Não |
| Contexto do cliente; dicionário 05 e 08 | Custom Providers | Extensibilidade pode reduzir acoplamento, mas o contrato mínimo precisa ser delimitado. | ADAPT | Sim | Sim | Sim | Não |
| Contexto do cliente; backlog | TTS | Resposta em voz é capacidade do produto e deve falhar sem afetar OBS ou texto. | ADOPT | Sim | Sim | Sim | Não |
| Contexto do cliente | Windows TTS | Provider TTS local candidato; qualidade, vozes e licenciamento precisam ser avaliados. | REFERENCE | Sim, como opção | Sim | Não | Não |
| Contexto do cliente | Azure Speech | Provider TTS cloud candidato; não aprovado nem obrigatório. | REFERENCE | Sim, como opção | Sim | Não | Não |
| Contexto do cliente | ElevenLabs | Provider TTS cloud candidato; não aprovado nem obrigatório. | REFERENCE | Sim, como opção | Sim | Não | Não |
| Contexto do cliente; dicionário 03, 05 e 08 | Chat Providers | Fronteira para entradas de chat com contratos, limites e falhas controlados. | ADAPT | Sim | Sim | Sim | Não |
| `AGENTS.md`; contexto do cliente | YouTube Live Chat | Prioridade V1, condicionada à validação de API, OAuth, quotas e revogação. | ADOPT | Sim | Sim | Sim | Não |
| `AGENTS.md`; contexto do cliente | Twitch | Provider futuro, excluído da V1 salvo nova aprovação. | FUTURE | Não, na V1 | Não agora | Não agora | Future |
| `agent_docs/security.md` | Moderation | Entradas e respostas precisam de política de moderação configurável e auditável. | ADOPT | Sim | Não | Sim | Não |
| `agent_docs/security.md`; dicionário 02, 15, 17 e 23 | Prompt Injection | Chat e conteúdo de providers são dados não confiáveis e não concedem autorização. | ADOPT | Sim | Não | Sim | Não |
| `agent_docs/security.md`; dicionário 05, 08 e 17 | Rate Limiting | Protege providers, custos, UX e estabilidade contra abuso ou rajadas. | ADOPT | Sim | Não | Sim | Não |
| Contexto do cliente; dicionário 05, 10 e 16 | Queues | Necessárias conceitualmente para backpressure; tecnologia ainda não foi escolhida. | ADAPT | Sim | Sim | Sim | Não |
| `agent_docs/security.md`; dicionário 05 e 08 | Timeout | Toda dependência externa ou IPC deve possuir limites explícitos. | ADOPT | Sim | Não | Sim | Não |
| `agent_docs/security.md`; dicionário 05, 08 e 23 | Cancellation | Operações demoradas precisam aceitar cancelamento e liberar recursos de forma segura. | ADOPT | Sim | Não | Sim | Não |
| Contexto do cliente | LiveContext | Estado efêmero da live é conceito de domínio candidato, ainda sem modelo aprovado. | ADAPT | Sim | Não | Sim | Não |
| Contexto do cliente | Assistant Profiles | Configuração de comportamento/voz é conceito de domínio candidato. | ADAPT | Sim | Não | Sim | Não |
| Contexto do cliente; dicionário 02, 15 e 17 | Short-Term Memory | Histórico limitado da sessão pode melhorar continuidade, sujeito a limites e privacidade. | ADAPT | Sim | Sim | Sim | Não |
| Contexto do cliente; dicionário 02, 15 e 17 | Persistent Memory | Persistência de memória exige finalidade, consentimento, retenção e exclusão definidos. | REQUIRES_CLIENT_DECISION | Sim, condicionado | Sim | Sim | Não |
| `agent_docs/security.md`; dicionário 10, 15 e 17 | Retention | Dados, memória e logs precisam de políticas por categoria. | ADOPT | Sim | Sim | Sim | Não |
| `agent_docs/security.md`; dicionário 08, 11 e 16 | Logging | Logs estruturados devem evitar secrets, prompts sensíveis e conteúdo desnecessário. | ADOPT | Sim | Não | Sim | Não |
| Dicionário 08, 10, 11, 16 e 17 | Observability | Latência, erros, filas e providers precisam de diagnóstico sem expor dados. | ADAPT | Sim | Não | Sim | Não |
| Contexto do cliente; backlog | Installer | Distribuição Windows faz parte do ciclo de vida, mas a tecnologia depende de Research/ADR. | REQUIRES_RESEARCH | Sim | Sim | Sim | Não |
| Contexto do cliente; backlog | Upgrade | Deve preservar compatibilidade, dados e credenciais ou oferecer rollback controlado. | REQUIRES_RESEARCH | Sim | Sim | Sim | Não |
| Contexto do cliente; backlog | Repair | Precisa distinguir binários, configuração, dados e credenciais. | REQUIRES_RESEARCH | Sim | Sim | Sim | Não |
| Contexto do cliente; backlog | Uninstall | Deve declarar o que remove, preserva e como trata dados e credenciais. | REQUIRES_RESEARCH | Sim | Sim | Sim | Não |
| `agent_docs/engineering-standards.md`; dicionário 11, 16 e 23 | Testing | Estratégia deve cobrir domínio, adapters, falhas, segurança e integrações controladas. | ADOPT | Sim | Não | Sim | Não |
| Contexto do cliente; backlog | OBS Compatibility | Matriz e política de compatibilidade precisam ser validadas contra OBS 32.x x64. | REQUIRES_RESEARCH | Sim | Sim | Sim | Não |
| `agent_docs/security.md`; dicionário 03, 05, 08, 15, 17 e 23 | Security | Requisito transversal desde entradas não confiáveis até secrets e ações no OBS. | ADOPT | Sim | Não | Sim | Não |
| `docs/governance/GITFLOW.md`; dicionário 11 e 18 | GitFlow | Fluxo canônico do repositório controla Tasks, homologação e releases. | ADOPT | Não funcional | Não | Não | Não |
| `docs/governance/QUALITY_GATES.md`; dicionário 11, 17 e 18 | Quality Gates | Evidências determinísticas complementam revisão por IA e controlam progressão. | ADOPT | Não funcional | Não | Não | Não |
| `docs/prompts/README.md`; histórico | Prompt Traceability | Prompt de cada Task é arquivado no mesmo commit; o conteúdo histórico não é canônico. | ADOPT | Não funcional | Não | Não | Não |
| Dicionário 02 e 15; `agent_docs/architecture.md` | RAG | Não há requisito aprovado; complexidade não deve ser introduzida na V1 sem justificativa. | OUT_OF_SCOPE | Não | Não agora | Não agora | Out of scope V1 |
| Dicionário 13 e 16; `agent_docs/architecture.md` | Microservices / cloud infrastructure | Conflita com a direção de aplicação desktop local e Modular Monolith. | OUT_OF_SCOPE | Não | Não agora | Não agora | Out of scope V1 |
| Dicionário 17, 18 e 23 | Multi-agent runtime | O workflow de engenharia não implica runtime multi-agent no produto. | OUT_OF_SCOPE | Não | Não agora | Não agora | Out of scope V1 |
| Inventário do Kit IA Dev e do repositório | Advanced Skills | Pacote oficial ausente; as 10 Skills básicas cobrem o gate e Requirements. | BLOCKED | Não | Não | Não | Non-blocking for Requirements |

## Regra de uso

Somente itens `ADOPT` e `ADAPT` podem alimentar diretamente a elaboração inicial de Requirements, sempre respeitando escopo e linguagem normativa. Itens `REQUIRES_RESEARCH`, `REQUIRES_ADR` e `REQUIRES_CLIENT_DECISION` podem aparecer como dependências, restrições ou perguntas em aberto, mas não como decisões resolvidas. Itens `FUTURE` e `OUT_OF_SCOPE` ficam fora da V1.

## Atualização de Research — 2026-10-06

A evidência técnica externa foi consolidada em [`docs/research/RESEARCH_MATRIX.md`](../research/RESEARCH_MATRIX.md). Classificações `REQUIRES_RESEARCH` acima passam a ter evidência para Architecture, mas permanecem `REQUIRES_ADR` ou `REQUIRES_CLIENT_DECISION` quando a escolha final ainda não foi autorizada.
