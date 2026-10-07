# Classificação de ativos

As classificações são qualitativas: `CRÍTICA`, `ALTA`, `MÉDIA` e `BAIXA`. Um ativo condicionado só existe quando a respectiva capacidade é habilitada; sua inclusão não afirma persistência ainda não aprovada.

| ID | Ativo | Existência / localização conceitual | Sensibilidade | Propriedades prioritárias | Requisitos |
|---|---|---|---|---|---|
| AST-001 | API Keys | Fornecidas pelo streamer para providers BYOK | CRÍTICA | Confidencialidade, integridade | SEC-008 a SEC-011 |
| AST-002 | OAuth Access Tokens | Emitidos por provider OAuth, quando aplicável | CRÍTICA | Confidencialidade, integridade, disponibilidade | SEC-008, SEC-012 |
| AST-003 | OAuth Refresh Tokens | Emitidos quando o fluxo autorizado os utilizar | CRÍTICA | Confidencialidade, integridade | SEC-008, SEC-012 |
| AST-004 | Outras credenciais de provider | Somente para providers selecionados que as exijam | CRÍTICA | Confidencialidade, integridade | SEC-008 a SEC-013 |
| AST-005 | Configurações | Configuração local não secreta e metadados de providers | ALTA | Integridade, disponibilidade | SEC-002, SEC-022, SEC-023 |
| AST-006 | Dados de sessão | Estado operacional e LiveContext da sessão | ALTA | Confidencialidade, integridade, disponibilidade | SEC-024, SEC-025, SEC-031 |
| AST-007 | Mensagens de chat | Recebidas do Chat Provider; não confiáveis | MÉDIA | Integridade, disponibilidade, confidencialidade quando retidas | SEC-001 a SEC-004 |
| AST-008 | Respostas da IA | Conteúdo externo não confiável | MÉDIA | Integridade, disponibilidade | SEC-001, SEC-005, SEC-031 |
| AST-009 | Memória de curto prazo | Contexto efêmero limitado à sessão | ALTA | Confidencialidade, integridade | SEC-024, SEC-025, SEC-031 |
| AST-010 | Histórico | Somente se habilitado e aprovado | ALTA | Confidencialidade, integridade | SEC-024, SEC-025 |
| AST-011 | Logs e diagnósticos | Eventos operacionais minimizados | ALTA | Confidencialidade, integridade, disponibilidade | SEC-011, SEC-026, SEC-032 |
| AST-012 | Banco local | Dados autorizados; nunca secrets em plaintext | ALTA | Confidencialidade, integridade, disponibilidade | SEC-022, SEC-023 |
| AST-013 | Configurações do OBS | Pertencem ao OBS e não ao assistente | CRÍTICA | Integridade, disponibilidade | SEC-006, SEC-020 |
| AST-014 | Processo OBS e transmissão | Processo hospedeiro e continuidade da live | CRÍTICA | Disponibilidade, integridade | SEC-006, SEC-020, SEC-021 |
| AST-015 | Assistant Core | Estado e operação do produto externo | ALTA | Integridade, disponibilidade | SEC-018 a SEC-021, SEC-030 |
| AST-016 | Integração OBS | Componente ou boundary futuro, ainda não escolhido | CRÍTICA | Integridade, disponibilidade, autenticidade | SEC-006, SEC-020, SEC-030 |
| AST-017 | Dados do streamer | Perfis, contexto e preferências fornecidos | ALTA | Confidencialidade, integridade | SEC-024, SEC-025 |
| AST-018 | Dados de viewers | Username/ID e conteúdo, somente quando necessários | ALTA | Confidencialidade, integridade | SEC-024, SEC-025 |
| AST-019 | Installer, updater e pacote | Artefatos futuros de distribuição | CRÍTICA | Autenticidade, integridade | SEC-027, SEC-028 |
| AST-020 | Binários e dependências | Artefatos futuros do produto e cadeia de fornecimento | CRÍTICA | Autenticidade, integridade | SEC-029 |

## Regras de tratamento

- Secrets nunca integram configuração comum, banco, prompt, histórico de prompt, relatório, log ou telemetria em plaintext.
- Dados de viewers e streamer são coletados e persistidos somente para finalidade aprovada e pelo período definido por categoria.
- Configurações externas do OBS não são alteradas sem autorização explícita.
- Mecanismos concretos de armazenamento, criptografia, permissões e assinatura permanecem `REQUIRES_RESEARCH + REQUIRES_ADR`.
