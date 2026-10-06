# Visão geral do projeto

## Produto

**Nome:** OBS-AI-Live-Assistant

**Propósito — CURRENT DIRECTION:** oferecer um assistente independente para transmissões ao vivo no OBS Studio, capaz de receber solicitações de espectadores ou do streamer e retornar respostas configuráveis em texto e voz.

**Problema — CURRENT DIRECTION:** a interação durante a transmissão exige coordenação entre chat, IA, TTS, estado local e OBS sem permitir que falhas nessas dependências encerrem ou comprometam a transmissão.

## Direção atual

| Área | Entendimento atual | Estado |
|---|---|---|
| Ambiente alvo | Windows 10/11 x64 com OBS Studio 32.x x64 | CURRENT DIRECTION |
| Runtime principal | C# / .NET 10 | CURRENT DIRECTION |
| Código nativo | C/C++ somente para capacidades nativas validadas do OBS | CURRENT DIRECTION / REQUIRES_RESEARCH |
| Forma do produto | Aplicação local independente, com responsabilidades isoladas no Assistant Core | CURRENT DIRECTION / REQUIRES_ADR |
| Chat | Prioridade para YouTube Live Chat na V1 | CURRENT DIRECTION |
| IA | Múltiplos providers com credenciais pertencentes ao usuário | CURRENT DIRECTION |
| TTS | Múltiplos providers com respostas de voz configuráveis | CURRENT DIRECTION |
| Persistência | Dados relacionais locais com SQLite na V1 | CURRENT DIRECTION / REQUIRES_ADR |
| Dados de runtime | `%APPDATA%\obs-studio\obs-ai-live-assistant` | ASSUMPTION / REQUIRES_RESEARCH |

BYOK significa que as credenciais pertencem ao usuário. Isso não aprova uma lista de providers nem um mecanismo de armazenamento. Secrets nunca devem ser colocados no controle de versão, prompts, logs, configuração em texto puro ou SQLite.

## Integração com OBS

O produto deverá integrar-se ao OBS, mas a distribuição de responsabilidades entre plugin nativo, OBS WebSocket e IPC está marcada como `REQUIRES_RESEARCH`, `REQUIRES_ADR` e, potencialmente, `REQUIRES_CLIENT_DECISION`. Nenhum mecanismo é aprovado apenas por aparecer nesta visão geral.

## Intenção de confiabilidade

A direção atual estabelece que falhas de IA, TTS, chat, persistência e rede não devem se tornar falhas do OBS sempre que isso for tecnicamente possível. O comportamento exato de processos, recuperação e degradação depende de Requirements e validação de Architecture.
