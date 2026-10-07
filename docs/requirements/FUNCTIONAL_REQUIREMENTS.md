# Requisitos funcionais

As prioridades são `MUST`, `SHOULD`, `COULD` e `WON'T V1`. O status indica o grau de confirmação e não substitui a prioridade.

## RF-001 — Controlar o ciclo de operação

- **Descrição:** o streamer deve poder iniciar, pausar, retomar e encerrar o assistente sem encerrar o OBS Studio.
- **Motivação:** manter controle explícito durante a live.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** `agent_docs/business-rules.md`; Project Overview
- **Dependências:** estado da sessão; integração OBS.
- **Critérios de aceite:** dado o assistente ativo, quando o streamer o pausa, novas solicitações não são enviadas a providers; ao encerrar o assistente, o OBS continua operando.
- **Observações:** o comportamento de solicitações já em andamento deve respeitar RF-026.
- **Research/ADR relacionado:** RES-008, RES-010; ADR-001, ADR-003.

## RF-002 — Gerenciar configuração do assistente

- **Descrição:** o streamer deve poder consultar e alterar configurações operacionais suportadas, com validação antes da aplicação.
- **Motivação:** adaptar comportamento e limites à live.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** regras de negócio.
- **Dependências:** RF-028; RNF-004.
- **Critérios de aceite:** dada uma alteração válida, o sistema a aplica e apresenta o estado resultante; dada uma alteração inválida, rejeita-a com mensagem segura e preserva a configuração anterior.
- **Observações:** valores e faixas ainda não confirmados permanecem configuráveis dentro de limites seguros.
- **Research/ADR relacionado:** ADR-004, ADR-008.

## RF-003 — Gerenciar Assistant Profiles

- **Descrição:** o streamer deve poder criar, selecionar, editar e excluir perfis de personalidade, estilo e comportamento da resposta.
- **Motivação:** evitar comportamento hardcoded e permitir diferentes contextos de live.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** regras de negócio; Knowledge Decisions.
- **Dependências:** RF-002, RF-028.
- **Critérios de aceite:** ao selecionar um perfil válido, solicitações subsequentes usam suas configurações; excluir um perfil em uso exige seleção de alternativa ou confirmação segura.
- **Observações:** perfil não pode conter secrets.
- **Research/ADR relacionado:** ADR-008.

## RF-004 — Gerenciar LiveContext

- **Descrição:** o streamer deve poder definir e revisar contexto necessário à live atual, com início e término vinculados à sessão.
- **Motivação:** tornar respostas relevantes sem histórico ilimitado.
- **Prioridade:** MUST
- **Status:** CONFIRMED com modelo pendente
- **Origem:** regras de negócio; Knowledge Decisions.
- **Dependências:** RF-029; RNF-005, RNF-023.
- **Critérios de aceite:** ao encerrar uma sessão, o contexto efêmero deixa de ser utilizado; nenhum campo não autorizado é incluído silenciosamente.
- **Observações:** conteúdo e limites exatos não são definidos nesta fase.
- **Research/ADR relacionado:** ADR-008; OQ-008.

## RF-005 — Conectar ao YouTube Live Chat

- **Descrição:** a V1 deve permitir conectar e desconectar uma live elegível do YouTube, informando estado de autenticação e conexão.
- **Motivação:** YouTube Live Chat é o Chat Provider prioritário da V1.
- **Prioridade:** MUST
- **Status:** CONFIRMED / REQUIRES_RESEARCH para API e OAuth
- **Origem:** `AGENTS.md`; Project Scope.
- **Dependências:** credenciais/autorização, RF-007, RNF-011.
- **Critérios de aceite:** com autorização válida, o sistema identifica a conexão ativa; com token inválido, revogado ou quota indisponível, não recebe como se estivesse conectado e informa falha recuperável sem expor token.
- **Observações:** scopes, polling/stream e lifecycle de token não são decididos aqui.
- **Research/ADR relacionado:** RES-014; ADR-001, ADR-004.

## RF-006 — Receber mensagens do chat

- **Descrição:** o sistema deve receber mensagens e metadados mínimos necessários da live conectada, preservando a identidade operacional necessária aos controles de abuso.
- **Motivação:** alimentar o fluxo de solicitações e rate limiting.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** regras de negócio; fluxo funcional de referência.
- **Dependências:** RF-005, RF-007, RNF-005.
- **Critérios de aceite:** mensagens recebidas são associadas à live e ao remetente necessário para os controles; eventos sem campos obrigatórios são rejeitados com diagnóstico seguro.
- **Observações:** minimização de dados é obrigatória.
- **Research/ADR relacionado:** RES-014, RES-024.

## RF-007 — Normalizar mensagens

- **Descrição:** mensagens de chat devem ser convertidas a uma representação funcional comum antes de trigger, moderação ou fila.
- **Motivação:** separar o contrato do produto das peculiaridades do provider.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** fluxo funcional de referência; provider boundaries.
- **Dependências:** RF-006; RNF-017.
- **Critérios de aceite:** entradas equivalentes produzem os campos comuns esperados; entrada ausente, malformada ou acima do limite é rejeitada sem alcançar o AI Provider.
- **Observações:** o formato interno pertence à Architecture.
- **Research/ADR relacionado:** RES-014; ADR-004.

## RF-008 — Detectar triggers configurados

- **Descrição:** o sistema deve identificar solicitações de chat somente quando corresponderem a triggers habilitados, incluindo suporte inicial pretendido a `@assistente` e `!ia`.
- **Motivação:** impedir processamento indiscriminado do chat.
- **Prioridade:** MUST
- **Status:** CONFIRMED; lista inicial parcialmente ASSUMPTION
- **Origem:** regras de negócio; ASM-003.
- **Dependências:** RF-002, RF-007.
- **Critérios de aceite:** dada mensagem sem trigger habilitado, ela não é enviada ao AI Provider; dada mensagem com trigger válido, ela segue para moderação.
- **Observações:** trigger não concede autorização para ação OBS.
- **Research/ADR relacionado:** nenhum.

## RF-009 — Aceitar acionamento manual do streamer

- **Descrição:** o streamer deve poder enviar solicitação manual sem depender do chat.
- **Motivação:** possibilitar controle e uso assistido durante a live.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** contexto do produto; regras de negócio.
- **Dependências:** RF-011, RF-014.
- **Critérios de aceite:** uma solicitação manual válida entra no mesmo conjunto de controles aplicáveis antes do AI Provider; entrada inválida é rejeitada com feedback.
- **Observações:** permissões do streamer não tornam output de IA confiável.
- **Research/ADR relacionado:** ADR-008.

## RF-010 — Moderar entrada

- **Descrição:** solicitações devem passar por regras de validação e moderação antes de serem enviadas ao AI Provider.
- **Motivação:** reduzir abuso, conteúdo proibido e prompt injection.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** `agent_docs/security.md`; Knowledge Map.
- **Dependências:** RF-007, RF-009, RF-013.
- **Critérios de aceite:** solicitação reprovada não alcança o AI Provider e produz resultado controlado; regras aplicadas podem ser diagnosticadas sem registrar conteúdo sensível desnecessário.
- **Observações:** política detalhada pertence à fase Security Requirements.
- **Research/ADR relacionado:** ADR-008; Security Requirements.

## RF-011 — Aplicar rate limiting e anti-spam

- **Descrição:** o sistema deve aplicar cooldown por usuário, cooldown global, limite de entrada e tratamento de mensagens repetidas antes da fila de IA.
- **Motivação:** conter abuso, custos e sobrecarga.
- **Prioridade:** MUST
- **Status:** CONFIRMED; valores REQUIRES_CLIENT_DECISION
- **Origem:** segurança; regras de negócio.
- **Dependências:** RF-006, RF-009, RF-002.
- **Critérios de aceite:** uma solicitação que excede limite ativo não é enviada ao provider; alterações válidas de limite passam a governar solicitações subsequentes.
- **Observações:** nenhum valor numérico é fixado nesta fase.
- **Research/ADR relacionado:** OQ-010; ADR-008.

## RF-012 — Gerenciar blocklist

- **Descrição:** o streamer deve poder bloquear e desbloquear usuários ou padrões suportados, com aplicação antes da fila.
- **Motivação:** oferecer controle operacional contra abuso recorrente.
- **Prioridade:** SHOULD
- **Status:** CONFIRMED
- **Origem:** solicitação autorizadora; baseline de moderação.
- **Dependências:** RF-006, RF-010, RF-028.
- **Critérios de aceite:** mensagem de usuário bloqueado não alcança o AI Provider; remover o bloqueio permite que mensagens futuras voltem aos demais controles.
- **Observações:** escopo e correspondência de padrões devem evitar bloqueios ambíguos.
- **Research/ADR relacionado:** Security Requirements.

## RF-013 — Controlar fila de solicitações

- **Descrição:** solicitações aprovadas devem entrar em fila limitada, com estado visível e comportamento definido para saturação.
- **Motivação:** impedir processamento simultâneo ou ilimitado.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** regras de negócio; segurança.
- **Dependências:** RF-011, RF-026; RNF-008.
- **Critérios de aceite:** ao atingir a capacidade configurada, novas solicitações não ampliam a fila e recebem tratamento controlado; a fila não processa concorrência além do limite aprovado.
- **Observações:** tecnologia e política exata de ordenação pertencem à Architecture.
- **Research/ADR relacionado:** RES-010; ADR-003.

## RF-014 — Construir contexto limitado da solicitação

- **Descrição:** antes da IA, o sistema deve compor somente instruções, perfil, LiveContext e memória de curto prazo necessários e autorizados.
- **Motivação:** melhorar relevância com minimização de dados e tokens.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** regras de negócio; segurança.
- **Dependências:** RF-003, RF-004, RF-015; RNF-005.
- **Critérios de aceite:** contexto enviado não inclui histórico ilimitado nem secrets; itens fora da sessão ou não autorizados não são incorporados.
- **Observações:** o formato do prompt não é requisito arquitetural.
- **Research/ADR relacionado:** ADR-008, ADR-009.

## RF-015 — Manter Short-Term Memory limitada

- **Descrição:** o sistema deve poder manter contexto temporário da sessão dentro de limites configurados e descartá-lo conforme a política da sessão.
- **Motivação:** permitir continuidade sem criar memória persistente implícita.
- **Prioridade:** SHOULD
- **Status:** CONFIRMED com limites pendentes
- **Origem:** Knowledge Decisions; regras de negócio.
- **Dependências:** RF-004, RF-029; RNF-023.
- **Critérios de aceite:** dados de sessão anterior não aparecem em nova sessão quando não houver retenção explicitamente autorizada; o streamer pode limpar o contexto temporário.
- **Observações:** Persistent Memory não está confirmada para V1.
- **Research/ADR relacionado:** RES-023; ADR-009.

## RF-016 — Suportar múltiplos AI Providers

- **Descrição:** o produto deve permitir o uso de AI Providers implementados e compatíveis sem tornar um vendor específico obrigatório.
- **Motivação:** viabilizar BYOK, escolha e evolução controlada.
- **Prioridade:** MUST
- **Status:** CONFIRMED; contrato REQUIRES_RESEARCH/ADR
- **Origem:** `AGENTS.md`; Knowledge Decisions.
- **Dependências:** RF-017 a RF-019; RNF-017.
- **Critérios de aceite:** trocar entre dois providers implementados preserva os critérios funcionais de trigger, moderação e saída; ausência de um provider específico não impede o uso de outro provider compatível.
- **Observações:** não obriga implementar todos os candidatos na V1.
- **Research/ADR relacionado:** RES-015, RES-016; ADR-004.

## RF-017 — Selecionar e configurar AI Provider

- **Descrição:** o streamer deve poder selecionar um AI Provider disponível e definir opções suportadas, como modelo e parâmetros expostos com segurança.
- **Motivação:** adaptar capacidade, custo e comportamento.
- **Prioridade:** MUST
- **Status:** CONFIRMED; providers específicos REQUIRES_CLIENT_DECISION
- **Origem:** regras de negócio; direção multi-provider.
- **Dependências:** RF-016, RF-018, RF-002.
- **Critérios de aceite:** somente opções válidas para o provider podem ser salvas; troca de provider não revela nem reutiliza credencial incompatível.
- **Observações:** catálogo conhecido não representa escopo aprovado.
- **Research/ADR relacionado:** RES-015, RES-016; OQ-005.

## RF-018 — Gerenciar credenciais BYOK

- **Descrição:** o usuário deve poder fornecer, validar, substituir e remover suas credenciais de provider de forma segura.
- **Motivação:** BYOK é requisito fundamental e reduz acoplamento comercial.
- **Prioridade:** MUST
- **Status:** CONFIRMED; storage REQUIRES_RESEARCH/ADR
- **Origem:** `AGENTS.md`; `agent_docs/security.md`.
- **Dependências:** RF-017, RF-023; RNF-003.
- **Critérios de aceite:** credencial válida pode ser associada ao provider; credencial inválida gera erro seguro; valor nunca é exibido integralmente nem registrado em log; após remoção, não pode autenticar nova operação.
- **Observações:** OAuth access/refresh tokens e API keys obedecem ao mesmo princípio de segredo.
- **Research/ADR relacionado:** RES-012, RES-013, RES-014; ADR-005.

## RF-019 — Processar solicitação por AI Provider

- **Descrição:** o sistema deve enviar a solicitação aprovada ao provider selecionado e produzir resultado ou falha classificada.
- **Motivação:** gerar a resposta central do assistente.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** objetivo do produto; fluxo de referência.
- **Dependências:** RF-013, RF-014, RF-016 a RF-018, RF-027.
- **Critérios de aceite:** somente solicitações aprovadas chegam ao provider; indisponibilidade, quota, credencial inválida, cancelamento e timeout produzem estado controlado sem interromper OBS.
- **Observações:** streaming é capacidade candidata, não requisito confirmado.
- **Research/ADR relacionado:** RES-015; ADR-004.

## RF-020 — Validar e moderar resposta de IA

- **Descrição:** toda saída do AI Provider deve ser tratada como não confiável, limitada e validada antes de exibição, persistência ou TTS.
- **Motivação:** prevenir conteúdo abusivo, prompt injection refletida e saída imprópria.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** `agent_docs/security.md`.
- **Dependências:** RF-019, RF-021, RF-024.
- **Critérios de aceite:** resposta reprovada ou acima do limite não é publicada nem narrada; falha de validação produz resultado seguro e diagnosticável.
- **Observações:** regras detalhadas pertencem a Security Requirements.
- **Research/ADR relacionado:** RES-024; Security Requirements.

## RF-021 — Enfileirar respostas aprovadas

- **Descrição:** respostas aprovadas devem ser coordenadas por uma fila limitada antes das saídas de texto e voz.
- **Motivação:** preservar ordem e impedir sobreposição descontrolada.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** fluxo funcional de referência; regras de negócio.
- **Dependências:** RF-020, RF-022, RF-025; RNF-008.
- **Critérios de aceite:** respostas simultâneas não ultrapassam o limite de saída; saturação não cria crescimento ilimitado e tem comportamento observável.
- **Observações:** política de prioridade permanece decisão posterior.
- **Research/ADR relacionado:** RES-010; ADR-003.

## RF-022 — Entregar resposta textual

- **Descrição:** o sistema deve disponibilizar a resposta textual aprovada em destino configurado e suportado, incluindo o YouTube Live Chat quando essa saída estiver habilitada e autorizada, e informar falhas de entrega.
- **Motivação:** texto é uma saída principal e deve funcionar independentemente de TTS.
- **Prioridade:** MUST
- **Status:** CONFIRMED; destino OBS REQUIRES_RESEARCH
- **Origem:** objetivo do produto; ASM-004.
- **Dependências:** RF-020, RF-021; integração OBS.
- **Critérios de aceite:** com TTS desabilitado, uma resposta aprovada ainda pode ser disponibilizada em texto; quando a saída para o YouTube está habilitada e autorizada, a resposta é publicada na live conectada ou a falha é informada; falha na integração não encerra OBS nem perde o diagnóstico.
- **Observações:** mecanismo de exibição não é definido nesta fase.
- **Research/ADR relacionado:** RES-007, RES-008, RES-014; ADR-001, ADR-004.

## RF-023 — Ativar ou desativar TTS

- **Descrição:** o streamer deve poder habilitar ou desabilitar a saída de voz sem desabilitar a resposta textual.
- **Motivação:** oferecer controle e degradação independente.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** regras de negócio.
- **Dependências:** RF-002, RF-022, RF-024.
- **Critérios de aceite:** quando TTS está desabilitado, nenhuma síntese é solicitada e o texto continua elegível; a alteração passa a valer para respostas subsequentes.
- **Observações:** não define roteamento de áudio.
- **Research/ADR relacionado:** RES-005, RES-006; ADR-006.

## RF-024 — Selecionar e configurar TTS Provider

- **Descrição:** o streamer deve poder selecionar um TTS Provider implementado, credencial quando aplicável, voz e opções suportadas.
- **Motivação:** permitir saída de voz configurável e multi-provider.
- **Prioridade:** MUST
- **Status:** CONFIRMED; providers específicos REQUIRES_CLIENT_DECISION
- **Origem:** `AGENTS.md`; regras de negócio.
- **Dependências:** RF-018, RF-023, RF-025.
- **Critérios de aceite:** configurações inválidas são rejeitadas; provider indisponível não impede saída textual; credenciais seguem RF-018.
- **Observações:** Windows TTS, Azure Speech, ElevenLabs e futuros providers são candidatos.
- **Research/ADR relacionado:** RES-017; ADR-004, ADR-006; OQ-005.

## RF-025 — Controlar fila e duração de TTS

- **Descrição:** sínteses aprovadas devem entrar em fila limitada, respeitar duração máxima configurada e evitar reprodução simultânea não autorizada.
- **Motivação:** impedir sobreposição, spam e bloqueio prolongado da live.
- **Prioridade:** MUST
- **Status:** CONFIRMED; valores REQUIRES_CLIENT_DECISION
- **Origem:** segurança; regras de negócio.
- **Dependências:** RF-021, RF-024, RF-026.
- **Critérios de aceite:** texto cuja duração estimada excede o limite recebe tratamento definido antes da reprodução; fila cheia não cresce sem limite.
- **Observações:** volume e demais opções só serão expostos se suportados de forma verificável.
- **Research/ADR relacionado:** RES-005, RES-006, RES-017; ADR-006; OQ-010.

## RF-026 — Cancelar operações

- **Descrição:** o streamer deve poder cancelar solicitação, geração de resposta ou reprodução TTS quando a etapa ainda for cancelável.
- **Motivação:** recuperar controle diante de conteúdo, atraso ou mudança da live.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** segurança; Knowledge Map.
- **Dependências:** RF-013, RF-019, RF-025.
- **Critérios de aceite:** após confirmação de cancelamento, nenhuma nova saída daquela operação é publicada; recursos e estado da fila são liberados de forma observável.
- **Observações:** garantias específicas dependem das capacidades dos providers.
- **Research/ADR relacionado:** RES-009, RES-015, RES-017; ADR-002, ADR-004.

## RF-027 — Tratar timeout e falhas operacionais

- **Descrição:** operações externas e potencialmente demoradas devem terminar por sucesso, falha classificada, timeout ou cancelamento, com feedback seguro e possibilidade de nova tentativa quando aplicável.
- **Motivação:** evitar espera infinita e estados silenciosos.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** segurança; engenharia.
- **Dependências:** todos os providers, RF-026; RNF-009 a RNF-011.
- **Critérios de aceite:** perda de chat, provider indisponível, credencial inválida, quota, erro TTS, fila saturada ou erro de banco não é informado como sucesso e não derruba OBS.
- **Observações:** políticas de retry e valores dependem de Architecture e risco de duplicação/custo.
- **Research/ADR relacionado:** RES-009, RES-010, RES-014 a RES-017; ADR-002 a ADR-004.

## RF-028 — Persistir configuração e metadados autorizados

- **Descrição:** o sistema deve persistir configurações, metadados não secretos de providers, perfis e regras necessárias entre execuções.
- **Motivação:** permitir operação consistente sem reconfiguração integral.
- **Prioridade:** MUST
- **Status:** CONFIRMED; tecnologia CURRENT DIRECTION / REQUIRES_ADR
- **Origem:** regras de negócio; direção SQLite.
- **Dependências:** RNF-012, RNF-022; RF-018 exclui secrets em plaintext.
- **Critérios de aceite:** reiniciar o aplicativo restaura dados autorizados consistentes; falha de leitura não derruba OBS e não substitui silenciosamente dados válidos por defaults.
- **Observações:** schema e migrations não são definidos aqui.
- **Research/ADR relacionado:** RES-011; ADR-004.

## RF-029 — Gerenciar sessões de live

- **Descrição:** o sistema deve iniciar, identificar e encerrar sessões para delimitar LiveContext, filas e dados temporários.
- **Motivação:** separar lives e controlar lifecycle de dados.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** regras de negócio; conceitos LiveContext e sessões.
- **Dependências:** RF-004, RF-015, RF-030.
- **Critérios de aceite:** dados temporários de uma sessão não são usados automaticamente em outra; encerrar sessão interrompe novas entradas e inicia o descarte conforme política.
- **Observações:** identificador e schema pertencem ao Data Design.
- **Research/ADR relacionado:** ADR-004, ADR-009.

## RF-030 — Gerenciar histórico mínimo e exclusão

- **Descrição:** quando habilitado, o sistema deve permitir consultar e excluir o histórico de interações estritamente necessário, sujeito a retenção definida.
- **Motivação:** conciliar continuidade, diagnóstico e privacidade.
- **Prioridade:** SHOULD
- **Status:** ASSUMPTION / REQUIRES_CLIENT_DECISION
- **Origem:** ASM-006; segurança.
- **Dependências:** RF-029; RNF-005, RNF-023.
- **Critérios de aceite:** desabilitar retenção impede novos registros além do operacional estritamente necessário; exclusão confirmada torna os itens indisponíveis ao produto conforme política aprovada.
- **Observações:** não confirma Persistent Memory nem estatísticas avançadas.
- **Research/ADR relacionado:** RES-023, RES-024; ADR-009; OQ-007, OQ-008.

## RF-031 — Exibir estado e diagnóstico

- **Descrição:** o sistema deve informar ao streamer estados essenciais de sessão, OBS, chat, AI Provider, TTS Provider, filas e erros acionáveis.
- **Motivação:** permitir operação e suporte sem expor secrets.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** logging/observabilidade no Knowledge Map.
- **Dependências:** RNF-013, RNF-014.
- **Critérios de aceite:** uma dependência desconectada é distinguível de estado saudável; mensagens não incluem chave, token, payload sensível integral ou stack trace inseguro.
- **Observações:** telemetria remota não é assumida.
- **Research/ADR relacionado:** ADR-008; Security Requirements.

## RF-032 — Instalar o produto

- **Descrição:** deve existir uma experiência de instalação que verifique pré-requisitos, detecte compatibilidade com OBS e instale somente componentes autorizados.
- **Motivação:** tornar a V1 distribuível e reduzir configuração incorreta.
- **Prioridade:** MUST
- **Status:** CONFIRMED / REQUIRES_RESEARCH para mecanismo
- **Origem:** contexto do cliente; Research Backlog.
- **Dependências:** política de compatibilidade e Installation Design.
- **Critérios de aceite:** ambiente incompatível é informado antes de alteração irreversível; instalação não modifica configuração externa do OBS sem autorização; falha não é apresentada como sucesso.
- **Observações:** tecnologia e caminhos não são escolhidos nesta fase.
- **Research/ADR relacionado:** RES-002, RES-003, RES-018, RES-022; ADR-007, ADR-010.

## RF-033 — Atualizar o produto

- **Descrição:** o produto deve suportar upgrade compatível, preservando ou migrando com segurança configurações e dados autorizados, com recuperação quando necessário.
- **Motivação:** manter suporte sem perda silenciosa de dados.
- **Prioridade:** MUST
- **Status:** CONFIRMED / REQUIRES_RESEARCH
- **Origem:** Research Backlog; Knowledge Decisions.
- **Dependências:** RF-028, RF-032; RNF-020.
- **Critérios de aceite:** upgrade incompatível é bloqueado com orientação; falha não deixa estado falsamente concluído; secrets não são expostos durante migração.
- **Observações:** mecanismo de rollback depende do design.
- **Research/ADR relacionado:** RES-011, RES-019; ADR-004, ADR-007.

## RF-034 — Reparar a instalação

- **Descrição:** o usuário deve poder reparar componentes instalados sem apagar silenciosamente configuração, dados ou credenciais válidas.
- **Motivação:** recuperar binários/configuração operacional com baixo risco.
- **Prioridade:** SHOULD
- **Status:** CONFIRMED / REQUIRES_RESEARCH
- **Origem:** Research Backlog.
- **Dependências:** RF-032, RF-028.
- **Critérios de aceite:** antes do repair, o usuário é informado sobre categorias afetadas; componentes preservados continuam disponíveis e segredos não são revelados.
- **Observações:** matriz de componentes reparáveis ainda não existe.
- **Research/ADR relacionado:** RES-020; ADR-007.

## RF-035 — Desinstalar o produto

- **Descrição:** o usuário deve poder remover o produto e seus componentes, com tratamento explícito para dados, logs e credenciais.
- **Motivação:** garantir lifecycle controlado e privacidade.
- **Prioridade:** MUST
- **Status:** CONFIRMED; política REQUIRES_CLIENT_DECISION
- **Origem:** Research Backlog; segurança.
- **Dependências:** RF-032, RF-028; RNF-021.
- **Critérios de aceite:** o uninstall declara o que será removido ou preservado; não remove arquivos de outros projetos/integrações; secrets não permanecem por acidente contra a política aprovada.
- **Observações:** defaults de preservação exigem decisão de produto.
- **Research/ADR relacionado:** RES-003, RES-021; ADR-007; OQ-012.

## RF-036 — Autorizar ações sensíveis do OBS

- **Descrição:** qualquer ação sensível atual ou futura voltada ao OBS deve exigir política explícita, allowlist e autorização do streamer; conteúdo de chat ou de IA nunca basta.
- **Motivação:** proteger transmissão, cenas, fontes e configuração contra abuso e prompt injection.
- **Prioridade:** MUST
- **Status:** CONFIRMED
- **Origem:** `AGENTS.md`; `agent_docs/security.md`.
- **Dependências:** integração OBS; RF-010, RF-020.
- **Critérios de aceite:** mensagem de viewer ou saída de IA que descreve comando sensível não o executa; ação não allowlisted é negada e registrada sem conteúdo sensível.
- **Observações:** nenhuma ação sensível específica é aprovada por este requisito.
- **Research/ADR relacionado:** RES-007, RES-008; ADR-001; Security Requirements; OQ-014.
