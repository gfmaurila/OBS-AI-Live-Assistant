# Requisitos não funcionais

As prioridades são `MUST`, `SHOULD`, `COULD` e `WON'T V1`. Valores quantitativos não sustentados por fonte permanecem dependentes de Research, decisão de produto, threat model ou testes futuros.

## RNF-001 — Isolar falhas do OBS Studio

- **Descrição:** falhas do AI Provider, TTS Provider, Chat Provider, banco de dados ou Assistant Core não devem encerrar, travar nem corromper o estado do OBS Studio.
- **Motivação:** preservar a transmissão mesmo quando o assistente ou uma dependência falhar.
- **Prioridade:** MUST
- **Status:** CONFIRMED; mecanismo REQUIRES_RESEARCH / REQUIRES_ADR
- **Origem:** `AGENTS.md`; regras de negócio; Knowledge Decisions.
- **Dependências:** RF-001, RF-027; integração OBS.
- **Critérios de aceite:** para cada classe de falha listada, um teste de falha controlada demonstra que o OBS continua responsivo e que o assistente registra estado degradado seguro.
- **Observações:** não prescreve processo, transporte ou watchdog.
- **Research/ADR relacionado:** RES-008, RES-010; ADR-001, ADR-003.

## RNF-002 — Degradar capacidades de forma independente

- **Descrição:** indisponibilidade de chat, IA, TTS, persistência ou integração de saída deve afetar somente as capacidades dependentes sempre que tecnicamente possível.
- **Motivação:** evitar falha total quando ainda existe função útil e segura.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** regras de negócio; Project Overview.
- **Dependências:** RF-022 a RF-027.
- **Critérios de aceite:** com TTS indisponível, texto elegível continua disponível; com chat indisponível, a entrada manual permanece utilizável; estados degradados não são apresentados como saudáveis.
- **Observações:** cada degradação deve respeitar segurança e consistência.
- **Research/ADR relacionado:** RES-010, RES-015, RES-017; ADR-003, ADR-004, ADR-006.

## RNF-003 — Proteger secrets durante todo o lifecycle

- **Descrição:** API keys, OAuth access tokens, refresh tokens, passwords, provider secrets e credenciais OBS não podem ser armazenados, exibidos ou registrados em plaintext.
- **Motivação:** reduzir exposição de credenciais BYOK e tokens de integração.
- **Prioridade:** MUST
- **Status:** CONFIRMED; mecanismo REQUIRES_RESEARCH / REQUIRES_ADR
- **Origem:** `agent_docs/security.md`.
- **Dependências:** RF-018, RF-032 a RF-035.
- **Critérios de aceite:** varreduras e testes de lifecycle não encontram valor integral de secret em banco, configuração, logs, prompts, relatórios, crash reports ou artefatos; remoção/revogação impede novo uso.
- **Observações:** Credential Manager e DPAPI são candidatos, não decisões.
- **Research/ADR relacionado:** RES-012, RES-013, RES-014; ADR-005, ADR-007.

## RNF-004 — Validar entradas não confiáveis

- **Descrição:** chat, saída de IA, respostas de providers, arquivos, configuração importada, persistência, eventos OBS e mensagens de integração devem ser validados e limitados antes de alcançar sinks sensíveis.
- **Motivação:** prevenir abuso, injeção, corrupção e propagação de conteúdo inválido.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** `agent_docs/security.md`.
- **Dependências:** RF-002, RF-007, RF-010, RF-020, RF-036.
- **Critérios de aceite:** entradas ausentes, malformadas ou acima de limites são rejeitadas antes do sink e produzem diagnóstico seguro; conteúdo de chat ou IA não autoriza ação sensível do OBS.
- **Observações:** schemas e bibliotecas serão definidos depois.
- **Research/ADR relacionado:** Security Requirements; ADR-001, ADR-002, ADR-008.

## RNF-005 — Minimizar dados e preservar privacidade

- **Descrição:** o produto deve coletar, enviar, persistir e exibir somente dados necessários e autorizados para a finalidade ativa.
- **Motivação:** limitar exposição de viewer, streamer, conteúdo e contexto de live.
- **Prioridade:** MUST
- **Status:** CONFIRMED; categorias e retenção REQUIRES_CLIENT_DECISION
- **Origem:** segurança; Knowledge Map.
- **Dependências:** RF-004, RF-006, RF-014, RF-030.
- **Critérios de aceite:** cada categoria persistida ou enviada a provider possui finalidade identificável; histórico ilimitado e secrets não entram em prompts; dados desabilitados pela política deixam de ser coletados.
- **Observações:** termos e políticas dos providers escolhidos exigem avaliação vigente.
- **Research/ADR relacionado:** RES-023, RES-024; ADR-009; OQ-008.

## RNF-006 — Aplicar menor privilégio e autorização explícita

- **Descrição:** processos, arquivos, providers e capacidades OBS devem operar com o menor privilégio necessário, e ações sensíveis devem exigir autorização explícita do streamer e allowlist.
- **Motivação:** limitar impacto de abuso, prompt injection ou comprometimento de componente.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** `AGENTS.md`; `agent_docs/security.md`.
- **Dependências:** RF-036; RNF-004.
- **Critérios de aceite:** ações não autorizadas ou não allowlisted são negadas; mensagem de viewer e saída de IA, isoladas ou combinadas, não elevam privilégios nem executam ação sensível.
- **Observações:** nenhuma ação OBS específica é aprovada nesta fase.
- **Research/ADR relacionado:** RES-007 a RES-009; ADR-001, ADR-002; Security Requirements.

## RNF-007 — Resistir a prompt injection e output abuse

- **Descrição:** instruções do sistema, dados de usuário e conteúdo externo devem permanecer separados por controles que impeçam conteúdo não confiável de obter autoridade ou contornar moderação.
- **Motivação:** reduzir ações indevidas, vazamento de contexto e publicação de conteúdo impróprio.
- **Prioridade:** MUST
- **Status:** CONFIRMED; política detalhada NOT STARTED
- **Origem:** `agent_docs/security.md`; Knowledge Map.
- **Dependências:** RF-010, RF-014, RF-020, RF-036.
- **Critérios de aceite:** cenários de injeção conhecidos não revelam secrets, não ignoram controles de saída e não acionam OBS; resposta reprovada não é exibida nem narrada.
- **Observações:** Security Requirements definirá ameaças e política de teste.
- **Research/ADR relacionado:** RES-024; ADR-008; Security Requirements.

## RNF-008 — Limitar concorrência e backpressure

- **Descrição:** filas e processamento de solicitações, respostas e TTS devem possuir capacidade e concorrência limitadas, com comportamento definido para saturação.
- **Motivação:** evitar exaustão de recursos, sobreposição e custo ilimitado.
- **Prioridade:** MUST
- **Status:** CONFIRMED; valores REQUIRES_RESEARCH / PRODUCT DECISION
- **Origem:** regras de negócio; segurança.
- **Dependências:** RF-011, RF-013, RF-021, RF-025.
- **Critérios de aceite:** carga acima do limite não provoca crescimento ilimitado; itens excedentes recebem tratamento observável; concorrência efetiva não supera a configuração aprovada.
- **Observações:** não define tecnologia de fila nem números.
- **Research/ADR relacionado:** RES-010; ADR-003; OQ-010.

## RNF-009 — Aplicar timeout a operações limitáveis

- **Descrição:** chamadas externas, comunicação entre boundaries e operações potencialmente demoradas devem possuir timeout configurado e resultado explícito.
- **Motivação:** impedir espera infinita e acúmulo silencioso.
- **Prioridade:** MUST
- **Status:** CONFIRMED; valores REQUIRES_RESEARCH
- **Origem:** segurança; Knowledge Decisions.
- **Dependências:** RF-019, RF-027.
- **Critérios de aceite:** dependência que não responde termina no estado de timeout, libera capacidade e não é registrada como sucesso nem bloqueia o OBS.
- **Observações:** valores podem variar por operação/provider.
- **Research/ADR relacionado:** RES-009, RES-010, RES-014 a RES-017; ADR-002 a ADR-004.

## RNF-010 — Propagar cancelamento com segurança

- **Descrição:** operações canceláveis devem observar o cancelamento em tempo limitado pela capacidade do provider, impedir novas saídas e liberar recursos.
- **Motivação:** devolver controle ao streamer e evitar trabalho órfão.
- **Prioridade:** MUST
- **Status:** CONFIRMED; garantias por provider REQUIRES_RESEARCH
- **Origem:** segurança; Knowledge Map.
- **Dependências:** RF-026.
- **Critérios de aceite:** após cancelamento confirmado, a operação não publica nem inicia nova saída; eventual limitação do provider fica visível e não mantém fila bloqueada indefinidamente.
- **Observações:** não promete cancelamento remoto quando o provider não o suportar.
- **Research/ADR relacionado:** RES-009, RES-015, RES-017; ADR-002, ADR-004.

## RNF-011 — Recuperar conexão e classificar falhas de providers

- **Descrição:** desconexão, quota, rate limit, credencial inválida, revogação e indisponibilidade devem ser distinguíveis e permitir recuperação controlada quando segura.
- **Motivação:** oferecer resiliência sem retries destrutivos ou custo oculto.
- **Prioridade:** MUST
- **Status:** CONFIRMED; retry policy REQUIRES_RESEARCH / REQUIRES_ADR
- **Origem:** engenharia; backlog de Research.
- **Dependências:** RF-005, RF-019, RF-024, RF-027.
- **Critérios de aceite:** cada falha suportada gera estado próprio e ação coerente; retry não ocorre quando puder duplicar saída, custo ou violar indicação do provider.
- **Observações:** backoff e número de tentativas não são fixados.
- **Research/ADR relacionado:** RES-014, RES-015, RES-017; ADR-004.

## RNF-012 — Preservar integridade dos dados

- **Descrição:** gravações, leitura, migrations e recuperação devem detectar e tratar falhas sem substituir silenciosamente dados válidos nem expor secrets.
- **Motivação:** evitar corrupção ou perda de configuração, sessão e histórico autorizado.
- **Prioridade:** MUST
- **Status:** CONFIRMED; tecnologia CURRENT DIRECTION / REQUIRES_ADR
- **Origem:** engenharia; direção SQLite.
- **Dependências:** RF-028 a RF-030, RF-033.
- **Critérios de aceite:** falha de escrita ou leitura não é reportada como sucesso; corrupção simulada produz diagnóstico e caminho de recuperação sem derrubar OBS; migrations incompatíveis são bloqueadas.
- **Observações:** biblioteca, schema, backup e migrations não são definidos aqui.
- **Research/ADR relacionado:** RES-011, RES-019; ADR-004.

## RNF-013 — Produzir logging seguro

- **Descrição:** logs devem registrar eventos operacionais necessários com redação de secrets, headers sensíveis, query values, payloads e detalhes de exceção não seguros.
- **Motivação:** permitir suporte sem criar canal de vazamento.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** `agent_docs/security.md`; Knowledge Decisions.
- **Dependências:** RF-031; RNF-003, RNF-005.
- **Critérios de aceite:** casos de erro com credenciais e conteúdo sensível produzem logs úteis sem valores integrais; o nível de logging não pode desativar a redação obrigatória.
- **Observações:** formato e biblioteca pertencem à Architecture.
- **Research/ADR relacionado:** Security Requirements; ADR-008.

## RNF-014 — Oferecer observabilidade operacional

- **Descrição:** o sistema deve tornar observáveis estado, latência, erro, timeout, cancelamento, saturação de fila e provider envolvido, sem telemetria remota obrigatória.
- **Motivação:** permitir operação, diagnóstico e testes de falha.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** Knowledge Map; regras de negócio.
- **Dependências:** RF-031; RNF-013.
- **Critérios de aceite:** o streamer distingue saudável, degradado, desconectado, saturado e falho para cada capacidade essencial; diagnósticos correlacionam uma operação sem expor dados proibidos.
- **Observações:** métricas específicas serão refinadas depois.
- **Research/ADR relacionado:** ADR-008; Security Requirements.

## RNF-015 — Tornar latência e desempenho verificáveis

- **Descrição:** tempos de recebimento, fila, provider e entrega devem ser mensuráveis, e a aplicação não deve bloquear interação do streamer ou threads críticas do OBS.
- **Motivação:** manter a live operável e permitir estabelecer metas com evidência.
- **Prioridade:** MUST
- **Status:** CONFIRMED; metas quantitativas REQUIRES_RESEARCH / PRODUCT DECISION
- **Origem:** regras de negócio; engenharia.
- **Dependências:** RNF-001, RNF-014.
- **Critérios de aceite:** testes conseguem medir cada etapa relevante e detectar bloqueio; nenhuma meta numérica é declarada aprovada antes de baseline e critérios de produto.
- **Observações:** não fixa percentis ou segundos nesta fase.
- **Research/ADR relacionado:** RES-005, RES-006, RES-010, RES-015, RES-017; OQ-010.

## RNF-016 — Limitar uso de recursos locais

- **Descrição:** CPU, memória, disco, rede, filas e armazenamento devem permanecer limitáveis e observáveis durante operação normal e sobrecarga.
- **Motivação:** evitar competição descontrolada com OBS e outras integrações.
- **Prioridade:** MUST
- **Status:** CONFIRMED; limites REQUIRES_RESEARCH
- **Origem:** proteção do OBS; engenharia.
- **Dependências:** RNF-008, RNF-015.
- **Critérios de aceite:** carga acima do configurado ativa backpressure/degradação em vez de consumo ilimitado; armazenamento sujeito a retenção não cresce indefinidamente.
- **Observações:** budgets dependem de testes no hardware alvo.
- **Research/ADR relacionado:** RES-010, RES-022; ADR-003, ADR-010.

## RNF-017 — Manter providers substituíveis

- **Descrição:** AI, TTS e Chat Providers devem poder ser substituídos por implementações compatíveis sem alterar regras centrais de trigger, moderação, autorização e lifecycle.
- **Motivação:** reduzir vendor coupling e sustentar BYOK.
- **Prioridade:** MUST
- **Status:** CONFIRMED; contratos REQUIRES_RESEARCH / REQUIRES_ADR
- **Origem:** `AGENTS.md`; Knowledge Decisions.
- **Dependências:** RF-007, RF-016, RF-024.
- **Critérios de aceite:** contract tests verificam capacidades e erros comuns; peculiaridade de um provider fica contida em sua boundary e não enfraquece controles centrais.
- **Observações:** não obriga mais de um provider de cada tipo na primeira entrega.
- **Research/ADR relacionado:** RES-014, RES-015, RES-017; ADR-004.

## RNF-018 — Favorecer manutenção com responsabilidades delimitadas

- **Descrição:** responsabilidades de domínio, integrações, persistência, segurança e OBS devem possuir boundaries explícitas e mudanças localizadas.
- **Motivação:** reduzir regressões e tornar o produto sustentável.
- **Prioridade:** MUST
- **Status:** CURRENT DIRECTION / REQUIRES_ADR
- **Origem:** `AGENTS.md`; `agent_docs/architecture.md`.
- **Dependências:** RNF-017.
- **Critérios de aceite:** Architecture futura demonstra ownership e dependências de cada boundary; uma integração não acessa diretamente secrets ou dados fora de seu contrato.
- **Observações:** Modular Monolith e Ports and Adapters são direções, não desenho final aprovado.
- **Research/ADR relacionado:** ADR-003, ADR-004, ADR-008.

## RNF-019 — Permitir testes determinísticos

- **Descrição:** regras de domínio, adapters, falhas, segurança, persistência, compatibilidade OBS e installer devem ser verificáveis por testes proporcionais ao risco.
- **Motivação:** complementar revisão por IA com evidência repetível.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** `agent_docs/engineering-standards.md`; Quality Gates.
- **Dependências:** todos os RFs e RNFs aplicáveis.
- **Critérios de aceite:** o plano de testes futuro associa requisitos a testes unitários, integração, contrato, banco, segurança, OBS e installer; falhas críticas possuem cenários reproduzíveis.
- **Observações:** comandos permanecem pendentes até existir solução aprovada.
- **Research/ADR relacionado:** RES-022; ADR-010; Testing Design.

## RNF-020 — Instalar e atualizar com recuperação

- **Descrição:** instalação e upgrade devem verificar compatibilidade, preservar dados autorizados, proteger secrets e oferecer recuperação ou rollback quando uma alteração não puder ser concluída.
- **Motivação:** evitar instalação parcial e perda silenciosa.
- **Prioridade:** MUST
- **Status:** CONFIRMED; mecanismo REQUIRES_RESEARCH / REQUIRES_ADR
- **Origem:** backlog de Research; segurança.
- **Dependências:** RF-032, RF-033; RNF-003, RNF-012, RNF-024.
- **Critérios de aceite:** falha induzida não é reportada como sucesso e deixa caminho documentado de recuperação; incompatibilidade é detectada antes de alteração irreversível.
- **Observações:** tecnologia de installer e rollback não são escolhidas.
- **Research/ADR relacionado:** RES-002, RES-003, RES-018, RES-019; ADR-007, ADR-010.

## RNF-021 — Reparar e desinstalar sem dano colateral

- **Descrição:** repair e uninstall devem distinguir binários, integração, configuração, dados, logs e secrets, sem remover artefatos de outros projetos ou integrações.
- **Motivação:** preservar controle do usuário e coexistência segura.
- **Prioridade:** MUST
- **Status:** CONFIRMED; defaults REQUIRES_CLIENT_DECISION
- **Origem:** `AGENTS.md`; backlog de Research.
- **Dependências:** RF-034, RF-035.
- **Critérios de aceite:** a operação declara categorias afetadas, respeita escolhas aprovadas e não altera o OBS Truck Live Optimizer nem conteúdo externo ao produto.
- **Observações:** política de preservação permanece aberta.
- **Research/ADR relacionado:** RES-003, RES-020, RES-021; ADR-007; OQ-012.

## RNF-022 — Persistir configuração de forma consistente

- **Descrição:** configurações e metadados autorizados devem permanecer consistentes entre execuções e mudanças inválidas não podem substituir o último estado válido.
- **Motivação:** garantir operação previsível e recuperação.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** regras de negócio; RF-028.
- **Dependências:** RF-002, RF-028; RNF-012.
- **Critérios de aceite:** reinício restaura o último estado confirmado; escrita incompleta ou configuração inválida mantém o estado anterior ou entra em recuperação explícita.
- **Observações:** formato e transações pertencem ao Data Design.
- **Research/ADR relacionado:** RES-011; ADR-004.

## RNF-023 — Aplicar retenção e descarte por categoria

- **Descrição:** contexto temporário, histórico, logs, diagnósticos e eventual memória persistente devem ter finalidade, retenção, limpeza e exclusão definidas por categoria.
- **Motivação:** impedir retenção indiscriminada e crescimento ilimitado.
- **Prioridade:** MUST
- **Status:** CONFIRMED para política; valores REQUIRES_CLIENT_DECISION
- **Origem:** regras de negócio; segurança.
- **Dependências:** RF-004, RF-015, RF-030.
- **Critérios de aceite:** dados expirados ou excluídos deixam de ser usados pelo produto; nova categoria não é retida sem política explícita; Persistent Memory permanece inativa sem aprovação.
- **Observações:** requisitos legais específicos não são inferidos nesta fase.
- **Research/ADR relacionado:** RES-023, RES-024; ADR-009; OQ-007, OQ-008.

## RNF-024 — Verificar compatibilidade do ambiente

- **Descrição:** o produto deve detectar e comunicar compatibilidade com Windows 10/11 x64 e com a política aprovada para OBS Studio 32.x x64.
- **Motivação:** prevenir execução ou instalação em ambiente não suportado.
- **Prioridade:** MUST
- **Status:** CONFIRMED; faixa exata REQUIRES_RESEARCH / CLIENT DECISION
- **Origem:** `AGENTS.md`; Project Scope.
- **Dependências:** RF-032; integração OBS.
- **Critérios de aceite:** ambiente incompatível é distinguido de falha transitória; matriz futura cobre versões suportadas e regressões sem alegar suporte não testado.
- **Observações:** OBS 32.1.2 instalado é ambiente de desenvolvimento, não toda a política.
- **Research/ADR relacionado:** RES-001, RES-002, RES-022; ADR-010; OQ-009.

## RNF-025 — Coexistir com OBS e outras integrações

- **Descrição:** o produto não deve modificar configuração externa do OBS sem autorização explícita nem assumir exclusividade sobre cenas, fontes, áudio, atalhos ou outros recursos compartilhados.
- **Motivação:** evitar regressões na live e conflito com outras integrações.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** contrato do repositório; segurança.
- **Dependências:** RF-032, RF-036.
- **Critérios de aceite:** instalação e operação preservam configuração não autorizada; conflito detectável produz orientação e não sobrescrita silenciosa.
- **Observações:** regras concretas dependem das capacidades escolhidas.
- **Research/ADR relacionado:** RES-003 a RES-008; ADR-001, ADR-006, ADR-007.

## RNF-026 — Encerrar e reiniciar de forma segura

- **Descrição:** encerramento, crash ou reinício do assistente deve deixar filas, sessões e dados em estado identificável e recuperável, sem afetar o OBS.
- **Motivação:** evitar operações órfãs e estado ambíguo após falha.
- **Prioridade:** MUST
- **Status:** CONFIRMED; mecanismo REQUIRES_ADR
- **Origem:** regras de negócio; engenharia.
- **Dependências:** RF-001, RF-026, RF-029; RNF-001, RNF-012.
- **Critérios de aceite:** encerramento durante operação não publica saída posterior indevida; reinício detecta estado incompleto e aplica recuperação explícita; OBS permanece operacional.
- **Observações:** política de retomada de item em fila será decidida depois.
- **Research/ADR relacionado:** RES-009, RES-010, RES-011; ADR-002, ADR-003, ADR-004.

## RNF-027 — Oferecer acessibilidade operacional básica

- **Descrição:** controles essenciais e estados críticos devem ser operáveis por teclado e comunicados também por texto, sem depender exclusivamente de cor ou áudio.
- **Motivação:** manter controle acessível durante a live.
- **Prioridade:** SHOULD
- **Status:** ASSUMPTION; refinamento REQUIRES_PRODUCT_DECISION
- **Origem:** OQ-015; baseline de qualidade de UI.
- **Dependências:** RF-001, RF-031.
- **Critérios de aceite:** iniciar/pausar/cancelar e reconhecer falhas essenciais é possível sem mouse e sem depender somente de áudio/cor; critérios adicionais permanecem registrados.
- **Observações:** padrão e escopo completos serão definidos no UI Design.
- **Research/ADR relacionado:** OQ-015; ADR-008.

## RNF-028 — Manter governança e rastreabilidade

- **Descrição:** requisitos, Research, ADRs, testes, Tasks e mudanças futuras devem manter identificadores e evidências rastreáveis sem promover hipótese a decisão.
- **Motivação:** permitir auditoria e progressão segura entre fases.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** governança; Quality Gates.
- **Dependências:** REQUIREMENTS_TRACEABILITY.md; processo de desenvolvimento.
- **Critérios de aceite:** cada decisão material futura referencia requisito e evidência; item `REQUIRES_RESEARCH`, `REQUIRES_ADR` ou `REQUIRES_CLIENT_DECISION` não aparece como aprovado sem seu gate.
- **Observações:** requisito de engenharia, não funcionalidade do runtime.
- **Research/ADR relacionado:** todos os itens aplicáveis.
