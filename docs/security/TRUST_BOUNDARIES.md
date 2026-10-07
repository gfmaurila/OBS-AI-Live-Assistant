# Trust Boundaries

Nenhuma travessia de boundary concede confiança ou autoridade implicitamente. Chat, respostas externas, AI output, arquivos, persistência e mensagens de integração são validados no boundary aplicável.

```text
Viewer -> Chat Platform -> Chat Provider boundary -> Assistant Core
Assistant Core -> AI Provider
Assistant Core -> TTS Provider
Assistant Core <-> OBS Integration <-> OBS Studio
Assistant Core <-> Local Storage
Streamer <-> Interface do Assistant
Installer/Updater <-> Operating System / OBS installation
```

| ID | Boundary | Dados / ativos | Postura de confiança | Controles requeridos | Research/ADR |
|---|---|---|---|---|---|
| TB-001 | Viewer → Chat Platform | Mensagem, identidade mínima | Viewer e conteúdo não confiáveis | Limites, moderação, normalização, anti-spam; nenhuma autoridade OBS | RES-014; ADR-008 |
| TB-002 | Chat Platform/Provider → Assistant Core | Eventos de chat, estado, OAuth | API e payload externos não confiáveis | Autenticidade da conexão, validação de contrato, quotas, timeout, minimização | RES-014, RES-024; ADR-004 |
| TB-003 | Assistant Core → AI Provider | Prompt limitado, resposta, credencial | Provider e output não confiáveis | Secret separado, minimização, timeout, limites e validação de output | RES-015, RES-024; ADR-004, ADR-005 |
| TB-004 | Assistant Core → TTS Provider | Texto aprovado, áudio/estado, credencial | Provider e retorno não confiáveis | Validação, duração/fila limitada, timeout, cancellation e degradação | RES-017; ADR-006 |
| TB-005 | Assistant Core ↔ OBS Integration/OBS | Comandos autorizados, eventos, estado | Alto impacto; mensagens não são confiáveis por origem apenas | Autenticação/autorização, allowlist, validação, limites, versionamento e falha segura | RES-007 a RES-010; ADR-001 a ADR-003 |
| TB-006 | Assistant Core ↔ Local Storage | Configuração, sessão, histórico autorizado, logs | Dados persistidos e arquivos são não confiáveis na leitura | Validação, permissões mínimas, integridade, recovery, retenção; secrets separados | RES-011 a RES-013, RES-023; ADR-004, ADR-005, ADR-009 |
| TB-007 | Streamer/UI → Assistant Core | Configuração, ação manual, autorização | Identidade local não elimina validação; AI output nunca herda autoridade | Validação, confirmação para ações sensíveis e política explícita | ADR-008 |
| TB-008 | Installer/Updater → OS e instalação OBS | Pacotes, binários, configuração, dados | Pacote e origem devem ser verificáveis | Integridade, origem confiável, menor privilégio, rollback e escopo explícito | RES-003, RES-018 a RES-022; ADR-007 |

O transporte, protocolo e mecanismo de autenticação de cada boundary não são decididos aqui.
