# Requisitos de segurança

Prioridades: `MUST`, `SHOULD`, `COULD`, `WON'T V1`. Todos os requisitos abaixo estão `CONFIRMED`, salvo dependência explicitamente classificada. Mecanismos técnicos permanecem para Research/ADR.

## SEC-001 — Tratar dados externos como não confiáveis
- **Descrição:** chat, respostas de APIs/providers, AI output, arquivos, persistência e mensagens de integração devem ser tratados como não confiáveis.
- **Motivação:** origem ou formato esperado não garantem segurança. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RNF-004, RNF-007; `agent_docs/security.md`. **Ativo protegido:** AST-005 a AST-016. **Ameaça:** THT-001, THT-006, THT-011.
- **Dependências:** boundaries definidos. **Critérios de aceite:** entrada atravessa validação aplicável antes de afetar estado ou saída; conteúdo inválido produz falha controlada.
- **Research/ADR:** ADR-002, ADR-004. **Observações:** confiança não é herdada do provider.

## SEC-002 — Validar e limitar entradas
- **Descrição:** entradas devem ter contrato, campos, encoding e tamanho validados antes de processamento.
- **Motivação:** conter malformed input e exaustão. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-002, RF-007, RNF-004. **Ativo:** AST-005, AST-007, AST-015. **Ameaça:** THT-001, THT-004.
- **Dependências:** limites configuráveis. **Aceite:** entrada ausente, malformada ou acima do limite não alcança AI/TTS/OBS e não altera configuração válida.
- **Research/ADR:** ADR-004, ADR-008. **Observações:** valores são configuráveis; não definidos nesta fase.

## SEC-003 — Moderar chat e solicitações
- **Descrição:** solicitações devem passar por política de moderação e antiabuso antes do provider.
- **Motivação:** reduzir conteúdo proibido, spam e abuso. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-010 a RF-012. **Ativo:** AST-007, AST-014, AST-017. **Ameaça:** THT-002, THT-003.
- **Dependências:** identidade mínima, regras e blocklist. **Aceite:** solicitação reprovada não alcança provider e gera resultado controlado sem payload sensível integral.
- **Research/ADR:** ADR-008. **Observações:** política deve ser testável e configurável dentro de limites seguros.

## SEC-004 — Resistir a Prompt Injection
- **Descrição:** conteúdo de viewer não pode substituir instruções, obter secrets, acessar arquivos, executar comandos, alterar configuração ou autorizar OBS.
- **Motivação:** separar dados de autoridade. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-010, RF-014, RF-036; RNF-007. **Ativo:** AST-001 a AST-017. **Ameaça:** THT-003.
- **Dependências:** delimitação de contexto e autorização. **Aceite:** casos de injeção não revelam secret nem acionam operação local/OBS e terminam em resposta bloqueada ou limitada.
- **Research/ADR:** ADR-008. **Observações:** mitigação não depende somente do prompt do modelo.

## SEC-005 — Validar AI output
- **Descrição:** toda saída de IA deve ser limitada e validada/moderada conforme destino antes de texto, TTS, persistência ou ação.
- **Motivação:** AI output é não confiável. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-020, RNF-007. **Ativo:** AST-008, AST-014, AST-017. **Ameaça:** THT-006, THT-007.
- **Dependências:** política por destino. **Aceite:** saída reprovada não é publicada, narrada, persistida nem convertida em comando.
- **Research/ADR:** RES-015, RES-024; ADR-004, ADR-008. **Observações:** saída nunca herda autoridade do streamer.

## SEC-006 — Autorizar ações sensíveis do OBS
- **Descrição:** ação sensível exige política explícita, allowlist, validação, autorização do streamer, limites e auditabilidade.
- **Motivação:** proteger transmissão e configuração. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-036, RNF-006, RNF-025. **Ativo:** AST-013, AST-014, AST-016. **Ameaça:** THT-008, THT-009.
- **Dependências:** capability e boundary aprovados. **Aceite:** chat, trigger ou AI output isoladamente nunca executa ação sensível; ação não allowlisted é negada.
- **Research/ADR:** RES-007 a RES-009; ADR-001, ADR-002. **Observações:** CHAT NÃO DEVE EXECUTAR DIRETAMENTE COMANDOS SENSÍVEIS DO OBS.

## SEC-007 — Aplicar least privilege
- **Descrição:** componentes, providers, instalação e operações devem receber somente acessos necessários à finalidade autorizada.
- **Motivação:** limitar blast radius. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RNF-006, RNF-020. **Ativo:** AST-001 a AST-020. **Ameaça:** THT-009, THT-020.
- **Dependências:** Architecture define responsabilidades. **Aceite:** cada permissão/scopo tem finalidade rastreável; acesso não necessário é ausente ou negado.
- **Research/ADR:** RES-003, RES-007, RES-014, RES-018; ADR-001, ADR-005, ADR-007. **Observações:** sem escolher mecanismo.

## SEC-008 — Classificar e separar secrets
- **Descrição:** API keys, access/refresh tokens e credenciais devem ser classificados como secrets e separados de configuração e dados comuns.
- **Motivação:** evitar exposição e propagação. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-018, RNF-003. **Ativo:** AST-001 a AST-004. **Ameaça:** THT-010, THT-012.
- **Dependências:** secret storage. **Aceite:** nenhum secret em plaintext existe em SQLite, JSON, appsettings, `.env` versionado, docs, prompts ou repositório.
- **Research/ADR:** RES-012, RES-013; ADR-005. **Observações:** metadata não secreta pode ser persistida separadamente.

## SEC-009 — Controlar lifecycle de credenciais BYOK
- **Descrição:** inclusão, validação, atualização, substituição, remoção e revogação/orientação devem ser seguras e verificáveis.
- **Motivação:** proteger toda a vida útil, não só o armazenamento. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-017, RF-018. **Ativo:** AST-001 a AST-004. **Ameaça:** THT-010, THT-012.
- **Dependências:** provider selecionado. **Aceite:** remoção impede novos usos locais; substituição não mantém cópia acessível da credencial anterior; falha não exibe valor.
- **Research/ADR:** RES-012 a RES-015, RES-021; ADR-005. **Observações:** revogação depende das capacidades oficiais do provider.

## SEC-010 — Mascarar secrets em interfaces
- **Descrição:** interfaces não devem exibir, copiar automaticamente ou confirmar integralmente secrets.
- **Motivação:** reduzir shoulder surfing e vazamento operacional. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-018, RNF-003. **Ativo:** AST-001 a AST-004. **Ameaça:** THT-010.
- **Dependências:** UX futura. **Aceite:** após gravação, UI apresenta somente identificação não secreta/valor mascarado; erros não ecoam o secret.
- **Research/ADR:** ADR-005, ADR-008. **Observações:** recuperação do valor original pela UI não é requisito.

## SEC-011 — Impedir secrets em logs, erros, telemetria e prompts
- **Descrição:** secrets devem ser removidos antes de qualquer registro, exception, telemetria, diagnóstico ou envio em prompt.
- **Motivação:** evitar canais secundários de vazamento. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RNF-003, RNF-013. **Ativo:** AST-001 a AST-004, AST-011. **Ameaça:** THT-010, THT-017.
- **Dependências:** redaction. **Aceite:** testes com valores sentinela não os encontram nas saídas; falha de provider não os replica.
- **Research/ADR:** SRES-005; ADR-005, ADR-008. **Observações:** telemetria não está aprovada; a proibição vale se vier a existir.

## SEC-012 — Proteger OAuth
- **Descrição:** OAuth deve usar scopes mínimos e controlar authorization code, tokens, expiração, refresh, revogação e desconexão.
- **Motivação:** tokens concedem acesso delegado. **Prioridade:** MUST. **Status:** CONFIRMED quando OAuth for aplicável / REQUIRES_RESEARCH.
- **Origem:** RF-005, RF-018. **Ativo:** AST-002, AST-003. **Ameaça:** THT-012, THT-013.
- **Dependências:** requisitos oficiais do provider. **Aceite:** token expirado/revogado não é usado como válido; desconexão impede novos usos e não revela token.
- **Research/ADR:** RES-014; ADR-005. **Observações:** não presume refresh token para todo provider.

## SEC-013 — Armazenar secrets com proteção apropriada
- **Descrição:** secrets persistentes não podem ficar em plaintext e devem ter acesso mínimo e lifecycle compatível com usuário, backup e instalação.
- **Motivação:** proteger credenciais locais. **Prioridade:** MUST. **Status:** REQUIRES_RESEARCH + REQUIRES_ADR para o mecanismo.
- **Origem:** RNF-003. **Ativo:** AST-001 a AST-004. **Ameaça:** THT-010, THT-014.
- **Dependências:** Research Windows. **Aceite:** revisão futura demonstra ausência de plaintext e nega leitura fora do escopo autorizado; upgrade/repair/uninstall têm comportamento definido.
- **Research/ADR:** RES-012, RES-013, RES-021; ADR-005. **Observações:** não escolhe DPAPI ou Credential Manager.

## SEC-014 — Aplicar rate limiting e cooldown
- **Descrição:** solicitações devem respeitar cooldown por viewer, cooldown global e limites de provider.
- **Motivação:** conter abuso, quota e custo. **Prioridade:** MUST. **Status:** CONFIRMED; valores configuráveis.
- **Origem:** RF-011, RNF-008. **Ativo:** AST-014, AST-015. **Ameaça:** THT-002, THT-004.
- **Dependências:** identidade mínima e configuração. **Aceite:** solicitação acima do limite não chega ao provider e não amplia fila; alteração válida governa solicitações seguintes.
- **Research/ADR:** ADR-003, ADR-008. **Observações:** thresholds são `REQUIRES_CLIENT_DECISION`.

## SEC-015 — Limitar filas e concorrência
- **Descrição:** filas de request, resposta e TTS e a concorrência devem possuir capacidade finita e política explícita de saturação.
- **Motivação:** impedir crescimento de memória e competição com OBS. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-013, RF-021, RF-025; RNF-008. **Ativo:** AST-014, AST-015. **Ameaça:** THT-004, THT-005.
- **Dependências:** configuração segura. **Aceite:** ao saturar, trabalho novo não aumenta a fila e recebe descarte/rejeição controlada; concorrência não excede o limite.
- **Research/ADR:** RES-010; ADR-003. **Observações:** política de ordenação é posterior.

## SEC-016 — Aplicar timeout
- **Descrição:** operações externas, IPC e trabalho potencialmente bloqueante devem ter timeout observável e configurável dentro de limites seguros.
- **Motivação:** impedir espera infinita. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-019, RF-027; RNF-009. **Ativo:** AST-014 a AST-016. **Ameaça:** THT-005, THT-015.
- **Dependências:** contratos futuros. **Aceite:** dependência que não responde termina em estado explícito, libera capacidade e não derruba OBS.
- **Research/ADR:** RES-009, RES-010, RES-014, RES-015, RES-017; ADR-002 a ADR-004. **Observações:** valores não fixados.

## SEC-017 — Propagar cancellation com segurança
- **Descrição:** trabalho cancelável deve parar novas saídas, liberar recursos e ignorar resultados tardios.
- **Motivação:** preservar controle e evitar efeitos após cancelamento. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-026, RNF-010. **Ativo:** AST-014, AST-015. **Ameaça:** THT-005.
- **Dependências:** contratos de providers e integração. **Aceite:** após cancelamento confirmado, nenhuma nova publicação/TTS ocorre e capacidade é recuperada.
- **Research/ADR:** RES-009, RES-015, RES-017; ADR-002 a ADR-004. **Observações:** mecanismo posterior.

## SEC-018 — Limitar retry e falhar de forma segura
- **Descrição:** retries devem ser limitados, interrompíveis e não multiplicar efeitos; chamadas podem ser temporariamente suspensas após falhas repetidas.
- **Motivação:** evitar retry storm e custo. **Prioridade:** MUST. **Status:** CONFIRMED; política configurável.
- **Origem:** RNF-011, RNF-016. **Ativo:** AST-014, AST-015. **Ameaça:** THT-005, THT-015.
- **Dependências:** classificação de erro. **Aceite:** falha persistente não gera loop ilimitado; estado degradado e recuperação são observáveis.
- **Research/ADR:** RES-010, RES-014 a RES-017; ADR-003, ADR-004. **Observações:** circuit breaking é necessidade conceitual, não tecnologia decidida.

## SEC-019 — Limitar consumo de recursos
- **Descrição:** entrada/saída, TTS, memória, logs, banco e trabalho simultâneo devem ter limites e comportamento de saturação.
- **Motivação:** resistir a DoS local ou remoto. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RNF-008, RNF-016, RNF-023. **Ativo:** AST-011, AST-012, AST-014, AST-015. **Ameaça:** THT-004, THT-016, THT-017.
- **Dependências:** métricas e configuração. **Aceite:** teste de sobrecarga controlado ativa backpressure/recusa e não causa crescimento ilimitado ou indisponibilidade do OBS.
- **Research/ADR:** RES-010, RES-011; ADR-003, ADR-009. **Observações:** thresholds dependem de medição.

## SEC-020 — Isolar falhas do OBS
- **Descrição:** falhas de AI, TTS, Chat, banco, Assistant Core e rede não podem encerrar ou corromper o OBS.
- **Motivação:** continuidade da transmissão é crítica. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RNF-001. **Ativo:** AST-013, AST-014, AST-016. **Ameaça:** THT-008, THT-015.
- **Dependências:** Research de integração/isolation. **Aceite:** falhas injetadas em cada dependência mantêm o processo OBS operacional e produzem estado degradado seguro.
- **Research/ADR:** RES-007 a RES-010; ADR-001 a ADR-003. **Observações:** mecanismo não definido.

## SEC-021 — Degradar sem ampliar privilégio ou impacto
- **Descrição:** indisponibilidade deve desativar apenas a capacidade afetada, preservar controle do streamer e nunca habilitar bypass inseguro.
- **Motivação:** falha segura. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RNF-002, RNF-011. **Ativo:** AST-014, AST-015. **Ameaça:** THT-015.
- **Dependências:** estados operacionais. **Aceite:** TTS indisponível não impede texto autorizado; IA/chat indisponível não habilita comando alternativo nem derruba OBS.
- **Research/ADR:** ADR-003, ADR-004, ADR-006. **Observações:** fallback não pode reduzir controles.

## SEC-022 — Proteger armazenamento local
- **Descrição:** banco, configuração, cache, logs e temporários devem ter acesso mínimo, validação, limpeza e separação de secrets.
- **Motivação:** dados locais podem ser lidos ou adulterados. **Prioridade:** MUST. **Status:** CONFIRMED / REQUIRES_RESEARCH.
- **Origem:** RF-028, RNF-003, RNF-012. **Ativo:** AST-005, AST-006, AST-009 a AST-012. **Ameaça:** THT-014, THT-016.
- **Dependências:** lifecycle do banco/OS. **Aceite:** arquivo adulterado ou incompatível é rejeitado/recuperado sem sucesso silencioso; temporários são removidos conforme lifecycle.
- **Research/ADR:** RES-011 a RES-013; ADR-004, ADR-005. **Observações:** não escolhe criptografia/ACL.

## SEC-023 — Preservar integridade e recuperação dos dados
- **Descrição:** escrita, migration, upgrade e recovery devem detectar falha e evitar estado parcial tratado como válido.
- **Motivação:** corrupção altera comportamento e configuração. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RNF-012, RNF-022. **Ativo:** AST-005, AST-006, AST-012. **Ameaça:** THT-016.
- **Dependências:** Data/Installation Design. **Aceite:** falha simulada não é reportada como sucesso e resulta em estado anterior válido ou diagnóstico recuperável.
- **Research/ADR:** RES-011, RES-019; ADR-004, ADR-007. **Observações:** backup não é decidido aqui.

## SEC-024 — Minimizar dados e finalidade
- **Descrição:** coletar, enviar e persistir somente dados necessários a finalidade aprovada.
- **Motivação:** reduzir exposição de streamer/viewers. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-006, RF-014, RF-030; RNF-005. **Ativo:** AST-006 a AST-010, AST-017, AST-018. **Ameaça:** THT-007, THT-018.
- **Dependências:** provider e funcionalidades habilitadas. **Aceite:** cada campo coletado/enviado possui finalidade; dado desnecessário não integra contexto, log ou persistência.
- **Research/ADR:** RES-023, RES-024; ADR-009. **Observações:** analytics avançado é OUT_OF_SCOPE_V1.

## SEC-025 — Controlar retenção e exclusão
- **Descrição:** categorias de sessão, memória, histórico, logs e dados de viewer devem ter retenção, expiração e exclusão explícitas.
- **Motivação:** evitar retenção indefinida. **Prioridade:** MUST. **Status:** REQUIRES_CLIENT_DECISION para prazos.
- **Origem:** RF-015, RF-030, RNF-023. **Ativo:** AST-006, AST-009 a AST-011, AST-017, AST-018. **Ameaça:** THT-018.
- **Dependências:** finalidade e política. **Aceite:** dados expirados/excluídos deixam de ser usados e interfaces distinguem categorias preservadas/removidas.
- **Research/ADR:** RES-023, RES-024; ADR-009. **Observações:** nenhum prazo quantitativo inventado.

## SEC-026 — Registrar eventos de forma segura e auditável
- **Descrição:** eventos de autorização, falha, timeout, saturação e degradação devem ser diagnosticáveis com redaction, minimização e correlação não sensível quando aplicável.
- **Motivação:** suportar investigação sem criar vazamento. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-031, RNF-013, RNF-014. **Ativo:** AST-011. **Ameaça:** THT-010, THT-017, THT-019.
- **Dependências:** política de logs. **Aceite:** diagnóstico distingue etapa/provider e decisão de autorização sem conter secret ou payload integral sensível.
- **Research/ADR:** SRES-005; ADR-008. **Observações:** structured logging é preferível quando aplicável, não tecnologia final.

## SEC-027 — Verificar origem, integridade e privilégio do installer
- **Descrição:** instalação deve usar pacote de origem/integridade verificáveis, escopo declarado e menor privilégio.
- **Motivação:** installer possui alto impacto local. **Prioridade:** MUST. **Status:** REQUIRES_RESEARCH.
- **Origem:** RF-032, RNF-020. **Ativo:** AST-019, AST-020. **Ameaça:** THT-020, THT-021.
- **Dependências:** tecnologia de installer e signing. **Aceite:** pacote adulterado/não confiável é recusado; instalação não altera caminhos/configurações fora do escopo autorizado.
- **Research/ADR:** RES-003, RES-018; ADR-007. **Observações:** code signing não foi escolhido.

## SEC-028 — Proteger upgrade, repair e uninstall
- **Descrição:** lifecycle deve preservar integridade, oferecer falha/rollback controlado e tratar dados/secrets conforme escolha explícita.
- **Motivação:** manutenção pode expor ou perder ativos. **Prioridade:** MUST. **Status:** REQUIRES_RESEARCH.
- **Origem:** RF-033 a RF-035; RNF-020, RNF-021. **Ativo:** AST-001 a AST-005, AST-012, AST-019. **Ameaça:** THT-020, THT-021.
- **Dependências:** políticas de dados e credenciais. **Aceite:** falha não é reportada como sucesso; cada categoria removida/preservada é informada; uninstall não afeta OBS/integrações alheias.
- **Research/ADR:** RES-019 a RES-021; ADR-007. **Observações:** update integrity é requisito, mecanismo posterior.

## SEC-029 — Governar supply chain
- **Descrição:** dependências devem ser mínimas, rastreáveis, verificadas quanto a integridade/vulnerabilidades conhecidas e atualizáveis.
- **Motivação:** dependência comprometida compromete o produto. **Prioridade:** MUST. **Status:** CONFIRMED; processo futuro.
- **Origem:** Engineering Standards, RNF-020, RNF-028. **Ativo:** AST-019, AST-020. **Ameaça:** THT-021, THT-022.
- **Dependências:** tooling e stack aprovados. **Aceite:** cada dependência futura possui origem/versão rastreável e finding crítico/alto impede release até tratamento explícito.
- **Research/ADR:** SRES-012; ADR-007. **Observações:** nenhuma ferramenta instalada nesta Task.

## SEC-030 — Proteger boundaries locais e IPC
- **Descrição:** endpoint/mensagem local futuro deve validar identidade/autorização, versão, tamanho, sequência e origem conforme risco.
- **Motivação:** “local” não significa confiável. **Prioridade:** MUST. **Status:** REQUIRES_RESEARCH + REQUIRES_ADR se o boundary existir.
- **Origem:** RNF-004, RNF-006. **Ativo:** AST-015, AST-016. **Ameaça:** THT-008, THT-009, THT-019.
- **Dependências:** escolha da integração. **Aceite:** cliente não autorizado, mensagem inválida/repetida ou versão incompatível não produz ação; falha permanece contida.
- **Research/ADR:** RES-009, RES-010; ADR-002, ADR-003. **Observações:** não presume que IPC existirá.

## SEC-031 — Isolar sessão e contexto
- **Descrição:** LiveContext, memória e resultados devem pertencer à sessão e solicitação corretas; resultados tardios não podem migrar de contexto.
- **Motivação:** impedir mistura e divulgação indevida. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RF-004, RF-014, RF-015, RF-029; RNF-026. **Ativo:** AST-006, AST-008, AST-009. **Ameaça:** THT-007, THT-019.
- **Dependências:** lifecycle de sessão. **Aceite:** encerramento impede uso posterior do contexto efêmero; resposta atrasada/cancelada não é publicada em nova sessão.
- **Research/ADR:** RES-023; ADR-009. **Observações:** persistent memory não é presumida.

## SEC-032 — Tornar controles de segurança testáveis
- **Descrição:** controles e decisões de autorização devem produzir evidência determinística suficiente para testes e revisão.
- **Motivação:** documentação sozinha não prova proteção. **Prioridade:** MUST. **Status:** CONFIRMED.
- **Origem:** RNF-019, RNF-028. **Ativo:** todos. **Ameaça:** todas conforme risco.
- **Dependências:** estratégia futura de testes. **Aceite:** plano futuro mapeia SEC/threat/risk a teste ou inspeção; Critical/High e secrets bloqueiam progressão.
- **Research/ADR:** RES-022; ADR-010. **Observações:** build/test ainda NOT APPLICABLE.

## SEC-033 — Proteger comunicação externa
- **Descrição:** comunicação com providers deve verificar destino/autenticidade, proteger credenciais e conteúdo em trânsito e rejeitar respostas incompatíveis.
- **Motivação:** rede e endpoint externo são não confiáveis. **Prioridade:** MUST. **Status:** CONFIRMED / REQUIRES_RESEARCH.
- **Origem:** RF-005, RF-016 a RF-019, RNF-011. **Ativo:** AST-001 a AST-004, AST-006 a AST-009. **Ameaça:** THT-011, THT-013.
- **Dependências:** requisitos oficiais do provider. **Aceite:** endpoint/destino incompatível ou resposta inválida não é aceita; segredo não aparece em URL/log/payload não autorizado.
- **Research/ADR:** RES-014, RES-015, RES-017, RES-024; ADR-004, ADR-005. **Observações:** protocolo/versão não são fixados aqui.

## SEC-034 — Avaliar privacidade e segurança de providers
- **Descrição:** provider só pode ser habilitado após avaliar dados enviados, retenção, região/termos, autenticação, quotas e resposta a comprometimento.
- **Motivação:** BYOK não transfere todo o risco ao usuário. **Prioridade:** MUST. **Status:** REQUIRES_RESEARCH.
- **Origem:** RF-016, RF-017; RNF-005, RNF-017. **Ativo:** AST-001 a AST-010, AST-017, AST-018. **Ameaça:** THT-006, THT-011, THT-018.
- **Dependências:** seleção de provider. **Aceite:** provider habilitado possui matriz de capacidades/riscos e dados enviados rastreáveis; incapacidade crítica bloqueia habilitação.
- **Research/ADR:** RES-014 a RES-017, RES-024; ADR-004. **Observações:** não seleciona vendors.
