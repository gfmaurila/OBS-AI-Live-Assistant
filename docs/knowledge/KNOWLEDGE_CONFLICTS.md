# Conflitos de conhecimento

Os conflitos abaixo não são resolvidos por inferência nesta Task. O estado `OPEN / CONTROLLED` significa que o ponto está identificado, possui caminho de resolução e não impede a elaboração segura de Requirements.

| Conflito | Fontes | Impacto | Risco | Estado | Caminho de resolução |
|---|---|---|---|---|---|
| OBS Native Plugin vs OBS WebSocket | Contexto do cliente; `agent_docs/architecture.md`; backlog | Define quais capacidades ficam dentro ou fora do processo OBS. | Crash do OBS, acoplamento nativo ou capacidades insuficientes. | OPEN / CONTROLLED | RESEARCH -> ADR |
| Same Process vs Process Isolation | Requisitos de estabilidade; padrões de plugin | Algumas capacidades nativas podem exigir execução no OBS, enquanto o Assistant Core deve permanecer isolado. | Falha de provider derrubar OBS ou IPC excessivamente complexo. | OPEN / CONTROLLED | REQUIREMENTS -> RESEARCH -> ADR |
| OBS Dock nativo vs UI externa | Contexto do cliente; backlog | Altera ciclo de vida, UX e dependências do OBS. | Interface indisponível, incompatível ou acoplada. | OPEN / CONTROLLED | RESEARCH -> REQUIREMENTS -> ADR |
| Native Audio vs OBS Source vs Virtual Audio Device | Contexto do cliente; backlog | Determina entrega, monitoramento e gravação do TTS. | Eco, áudio no dispositivo errado, incompatibilidade ou instabilidade. | OPEN / CONTROLLED | RESEARCH -> ADR |
| Credential Manager vs DPAPI vs outra estratégia | Segurança; backlog | Define proteção, escopo, backup, upgrade, repair e uninstall de credenciais BYOK. | Exposição, perda ou credencial irrecuperável. | OPEN / CONTROLLED | RESEARCH -> ADR |
| Direção SQLite vs lifecycle ainda indefinido | Contexto do cliente; dicionário; backlog | Banco é candidato V1, mas biblioteca, migrations, concorrência e recuperação não estão decididos. | Corrupção, perda de dados ou upgrade incompatível. | OPEN / CONTROLLED | REQUIREMENTS -> RESEARCH -> ADR |
| Cloud AI vs Local AI | Contexto do cliente; dicionário 08 e 10 | Afeta privacidade, hardware, custo, qualidade e distribuição. | Requisitos incompatíveis com hardware ou política do provider. | OPEN / CONTROLLED | REQUIREMENTS -> RESEARCH -> CLIENT DECISION / ADR |
| Persistent Memory vs Privacy/Retention | Contexto do cliente; `agent_docs/security.md`; dicionário 15 e 17 | Memória durável amplia dados armazenados e direitos do usuário. | Retenção excessiva, vazamento ou uso sem finalidade. | OPEN / CONTROLLED | CLIENT DECISION -> REQUIREMENTS -> ADR |
| Automatic Narrator vs V1 manual/chat trigger | Contexto do produto | Narração automática pode ampliar escopo além de solicitações manuais/chat. | Comportamento inesperado, spam de áudio e complexidade prematura. | OPEN / CONTROLLED | REQUIREMENTS -> CLIENT DECISION |
| Provider scope V1 | Direção multi-provider; providers nomeados | Multi-provider é direção, mas implementar todos os providers citados não é necessário. | Escopo inflado e contratos guiados por vendors. | OPEN / CONTROLLED | REQUIREMENTS -> CLIENT DECISION |
| Installer technologies | Backlog; dicionário 11 e 16 | MSI, MSIX, bootstrapper ou outras opções têm trade-offs diferentes com plugins OBS. | Instalação sem privilégios corretos, repair/uninstall incompletos. | OPEN / CONTROLLED | RESEARCH -> ADR |
| Upgrade strategy | Contexto do cliente; backlog | Atualização pode afetar binários nativos, schema, configuração e credenciais. | Versão incompatível ou perda de dados. | OPEN / CONTROLLED | REQUIREMENTS -> RESEARCH -> ADR |
| OBS compatibility: versão fixa vs faixa suportada | Target OBS 32.x; backlog | Uma versão única simplifica testes; faixa amplia uso e matriz de regressão. | Falha por ABI/API ou suporte indefinido. | OPEN / CONTROLLED | RESEARCH -> REQUIREMENTS -> CLIENT DECISION |
| Padrões cloud/microsserviços do dicionário vs desktop local | Dicionário 13, 16 e 20; `AGENTS.md` | Muitos padrões do Kit não se aplicam ao produto atual. | Introdução de cloud, brokers, containers ou microservices sem necessidade. | RESOLVED FOR REQUIREMENTS | OUT_OF_SCOPE |
| RAG/Agentic RAG/multi-agent vs V1 | Dicionário 02, 15, 17 e 23; restrições do projeto | O dicionário descreve soluções avançadas sem caso aprovado no produto. | Complexidade, dados e segurança sem valor demonstrado. | RESOLVED FOR REQUIREMENTS | OUT_OF_SCOPE |
| Workflow agentic de engenharia vs runtime do produto | Dicionário 17 e 18; governança | Agentes e Skills organizam desenvolvimento, não compõem automaticamente o produto. | Requisito fictício de orquestração multi-agent. | RESOLVED FOR REQUIREMENTS | OUT_OF_SCOPE |
| Skills básicas vs Advanced Skills ausentes | Inventário do Kit; `PROJECT_SKILLS.md` | O pacote avançado oficial não existe nos caminhos verificados. | Perda de conveniências opcionais, sem perda da base documental. | BLOCKED / NON-BLOCKING FOR REQUIREMENTS | Manter BLOCKED; instalar apenas pacote oficial futuro |
| Prompt History vs documentação canônica | `docs/prompts/README.md`; histórico | Prompts preservam pedidos antigos que podem divergir das regras atuais. | Reintrodução de instrução obsoleta. | RESOLVED | REFERENCE somente para rastreabilidade |

## Avaliação de bloqueio

Não existe conflito crítico sem controle que impeça Requirements. Os conflitos abertos podem ser expressos como restrições, perguntas, dependências de Research ou decisões posteriores. Requirements não deve resolver Architecture nem escolher tecnologia sem evidência.
