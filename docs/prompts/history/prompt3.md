AUTORIZADO.

Prossiga SOMENTE com:

```text
KNOWLEDGE QUALITY GATE
+
REQUIREMENTS
```

para o projeto:

```text
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant
```

## Regras obrigatórias

O projeto `OBS-AI-Live-Assistant` é um projeto novo e independente.

NÃO reutilize, copie, altere ou sobrescreva arquivos pertencentes ao:

```text
OBS Truck Live Optimizer
```

ou qualquer outro projeto existente dentro de:

```text
C:\Users\gfmau\AppData\Roaming\obs-studio
```

Os documentos existentes nesse ambiente podem ser utilizados SOMENTE como referência arquitetural quando forem relevantes.

Qualquer conflito entre decisões existentes do ambiente OBS e requisitos do novo produto deverá ser registrado explicitamente.

---

## 1. KNOWLEDGE QUALITY GATE

Valide se o conhecimento disponível é suficiente para produzir Requirements confiáveis.

Classifique cada decisão relevante como:

```text
CONFIRMED
ASSUMPTION
REQUIRES_RESEARCH
REQUIRES_ADR
REQUIRES_CLIENT_DECISION
OUT_OF_SCOPE_V1
```

Não transforme hipóteses em requisitos confirmados.

Registre gaps de conhecimento.

Especial atenção para:

```text
OBS native plugin development
OBS 32.1.2 compatibility
OBS plugin installation paths
OBS plugin packaging
OBS Dock integration
OBS audio integration
OBS WebSocket versus native plugin responsibilities
C++ ↔ .NET communication
SQLite
Windows Credential Manager / DPAPI
AI provider abstraction
TTS provider abstraction
YouTube Live Chat integration
local AI provider viability
installer technology
upgrade / uninstall strategy
```

Quando a informação depender de documentação oficial ou comportamento específico de uma tecnologia, marque:

```text
REQUIRES_RESEARCH
```

Não invente a resposta.

---

## 2. REQUIREMENTS

Após passar pelo Knowledge Quality Gate, produza os Requirements do produto.

Os Requirements devem definir O QUE o sistema precisa fazer.

Não transformar Requirements prematuramente em decisões detalhadas de implementação.

---

## 3. OBJETIVO DO PRODUTO

O `OBS-AI-Live-Assistant` será um assistente inteligente para transmissões realizadas através do OBS Studio.

Fluxo principal:

```text
Viewer
   ↓
Live Chat
   ↓
OBS-AI-Live-Assistant
   ↓
AI
   ↓
Response
   ├── Text → Chat
   └── Voice → OBS
```

O espectador poderá chamar o assistente durante a live.

Exemplos:

```text
@assistente qual é a rota atual?
```

ou:

```text
!ia qual é a rota atual?
```

A IA deverá poder responder:

```text
Texto
+
Voz
```

conforme configuração do streamer.

---

## 4. BYOK — BRING YOUR OWN KEY

O produto deverá seguir:

```text
BYOK
Bring Your Own Key
```

O usuário utilizará suas próprias credenciais/providers de IA e TTS quando necessário.

O produto não deverá depender obrigatoriamente de uma única empresa de IA.

Planejar arquitetura extensível para providers.

Exemplos conceituais:

```text
AI Providers
- OpenAI
- Anthropic
- Gemini
- Ollama
- OpenRouter
- Future Providers

TTS Providers
- Windows/local TTS
- Azure Speech
- ElevenLabs
- Future Providers
```

Esses nomes NÃO significam que todos deverão ser implementados na V1.

Requirements deverá separar claramente:

```text
V1
Future
Optional
```

---

## 5. PLATAFORMA DE CHAT V1

Prioridade inicial:

```text
YouTube Live Chat
```

A arquitetura deverá permitir posteriormente:

```text
Twitch
Other platforms
```

Não implementar essas integrações agora.

---

## 6. MODOS DE INTERAÇÃO

Planejar requisitos para:

```text
Mention
Command
Manual
Text
Voice
```

Prioridade V1:

```text
@assistente
!ia
manual activation
```

Modos avançados:

```text
Auto
Narrator
```

devem ser classificados inicialmente como:

```text
FUTURE / OPTIONAL
```

salvo justificativa diferente nos Requirements.

---

## 7. CONTROLE DE FILA

O produto precisa impedir que múltiplos espectadores façam a IA falar simultaneamente.

Requirements devem contemplar:

```text
request queue
TTS queue
per-user cooldown
global cooldown
maximum queue size
maximum response length
request timeout
cancellation
anti-spam
```

Esses valores deverão ser configuráveis quando apropriado.

---

## 8. MODERAÇÃO E SEGURANÇA

Chat e respostas de IA devem ser tratados como conteúdo não confiável.

Requirements devem contemplar:

```text
input validation
input moderation
output validation
output moderation
rate limiting
blocked users
ignored users
blocked terms
prompt injection considerations
secret protection
safe logging
```

A IA nunca poderá executar diretamente comandos sensíveis no OBS originados do chat sem uma camada explícita de autorização/controle.

---

## 9. CONTEXTO DA LIVE

Definir requisitos para um:

```text
LiveContext
```

capaz de fornecer à IA contexto controlado da transmissão.

Possíveis informações:

```text
streamer
live title
platform
game
OBS scene
assistant profile
recent interactions
session information
custom context
```

O contexto enviado ao provider deverá ser limitado ao necessário.

Não assumir histórico infinito.

---

## 10. PERSONALIDADE

O streamer deverá poder configurar um perfil do assistente.

Exemplo conceitual:

```text
Name
Language
Personality
Response Style
Response Length
Voice
```

A personalidade não deverá estar hardcoded.

---

## 11. BANCO SQL

A aplicação utilizará banco relacional.

Direção atual:

```text
SQLite
```

para armazenamento local.

Planejar requisitos para:

```text
settings
provider metadata
assistant profiles
live sessions
chat users when required
interactions
commands
moderation rules
usage statistics
schema migrations
data retention
```

Não armazenar secrets em texto puro no banco.

---

## 12. CREDENCIAIS

São considerados secrets:

```text
API Keys
OAuth Tokens
Refresh Tokens
Provider Secrets
Passwords
```

Requirements devem exigir armazenamento seguro.

Direções a serem avaliadas posteriormente:

```text
Windows Credential Manager
DPAPI
```

A escolha definitiva pertence à fase de Architecture/Security Research.

Secrets nunca poderão aparecer em logs.

---

## 13. DIRETÓRIOS

Source repository:

```text
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant
```

Application data desejado:

```text
C:\Users\gfmau\AppData\Roaming\obs-studio\obs-ai-live-assistant
```

ou:

```text
%APPDATA%\obs-studio\obs-ai-live-assistant
```

Estrutura conceitual:

```text
config/
data/
database/
logs/
cache/
temp/
```

IMPORTANTE:

Não assumir que DLLs/binários do plugin serão instalados nesse diretório.

O caminho de instalação do plugin deverá ser pesquisado e validado separadamente.

---

## 14. OBS

Versão atualmente instalada no ambiente de desenvolvimento:

```text
OBS Studio 32.1.2 x64
```

O produto deverá ser projetado para não comprometer a estabilidade do OBS.

Requisito fundamental:

```text
Assistant failure != OBS failure
```

Falhas de:

```text
AI
TTS
Chat
Database
Network
Provider
Assistant Core
```

não devem encerrar ou comprometer a transmissão quando tecnicamente possível.

---

## 15. NATIVE PLUGIN VS OBS WEBSOCKET

NÃO decida isso definitivamente nos Requirements.

Registre como questão arquitetural:

```text
REQUIRES_ADR
```

A Architecture deverá determinar quais capacidades exigem:

```text
Native OBS Plugin
```

e quais podem utilizar:

```text
OBS WebSocket
IPC
External Core
```

Evite duplicação de responsabilidades.

---

## 16. ÁUDIO

A resposta TTS deverá poder ser ouvida na transmissão.

Porém o mecanismo definitivo ainda não está escolhido.

Registrar:

```text
TTS AUDIO ROUTING:
REQUIRES_RESEARCH
+
REQUIRES_ADR
```

Avaliar posteriormente:

```text
dedicated OBS audio source
native plugin audio
virtual audio device
media source
other supported mechanism
```

Não escolher arbitrariamente nesta fase.

---

## 17. STACK — DIREÇÃO ATUAL

Registrar como constraint/direção aprovada:

```text
Assistant Core:
C# / .NET 10

Native OBS Integration:
C/C++ when technically required

Database:
SQLite / SQL

Operating System V1:
Windows 10 / Windows 11 x64

OBS Target Environment:
OBS Studio 32.x x64
```

Architecture deverá validar os limites entre esses componentes.

---

## 18. INSTALADOR

O objetivo de produto é possuir futuramente:

```text
OBS-AI-Live-Assistant-Setup.exe
```

Experiência desejada:

```text
Install
   ↓
Detect OBS
   ↓
Install required OBS component
   ↓
Install Assistant Core
   ↓
Prepare application data
   ↓
Ready
```

Também deverão existir requisitos para:

```text
upgrade
repair
uninstall
```

No uninstall, considerar opção de:

```text
Keep user data
```

ou:

```text
Remove application data
```

Tecnologia do instalador:

```text
REQUIRES_ADR
```

---

## 19. GIT

Git não está disponível atualmente.

Registrar como bloqueio de engenharia:

```text
GIT:
MISSING
```

O projeto deverá futuramente utilizar:

```text
main
develop
hml
```

e:

```text
feature/task-...
release/...
```

Não tente criar GitFlow enquanto Git não estiver instalado/configurado.

---

## 20. ESCOPO PROPOSTO DA V1

Avalie e formalize uma V1 mínima contendo:

```text
OBS integration
Assistant Core
OBS control/interface required by V1
SQLite
YouTube Live Chat
@assistente
!ia
Manual interaction
AI provider abstraction
At least one usable AI provider
Basic TTS
Text response
Voice response
Request queue
TTS queue
Rate limiting
Basic moderation
Assistant profile
Live context
Secure credential handling
Logging
Configuration
Installer
Unit tests
Integration tests
```

Classifique explicitamente o que ficar fora da V1.

---

## 21. FORA DA V1 POR PADRÃO

Considere inicialmente:

```text
Twitch
Kick
ATS Telemetry
ETS2 Telemetry
TruckHub integration
RAG
Multi-Agent runtime
Kafka
RabbitMQ
Redis
Microservices
Kubernetes
Cloud infrastructure
Automatic Narrator
Advanced analytics
```

como:

```text
OUT_OF_SCOPE_V1
```

Isso não impede evolução futura.

---

## 22. NÃO IMPLEMENTAR

Nesta etapa:

```text
DO NOT create source code.
DO NOT initialize Git.
DO NOT install dependencies.
DO NOT create the database.
DO NOT modify OBS.
DO NOT modify OBS Truck Live Optimizer.
DO NOT create the native plugin.
DO NOT connect YouTube.
DO NOT connect AI providers.
DO NOT configure TTS.
DO NOT choose installer technology.
DO NOT resolve ADRs prematurely.
```

---

## 23. ARTEFATOS ESPERADOS

Utilize os padrões e nomenclaturas já estabelecidos pelo Kit IA Dev.

Produza os artefatos necessários para:

```text
Knowledge Quality Gate
Requirements
Open Questions
Assumptions
Constraints
Risks
Out-of-Scope V1
Research Backlog
ADR Candidates
```

Evite criar documentos redundantes se o Kit já possuir arquivo equivalente.

---

## 24. RESULTADO ESPERADO

Ao terminar, apresente algo semelhante a:

```text
OBS-AI-Live-Assistant

KNOWLEDGE QUALITY GATE:
PASS / CONDITIONAL PASS / BLOCKED

REQUIREMENTS:
COMPLETED

V1 Scope:
DEFINED

Constraints:
DEFINED

Assumptions:
DOCUMENTED

Open Questions:
DOCUMENTED

Research Backlog:
DEFINED

ADR Candidates:
DEFINED

Implementation:
NOT STARTED

NEXT PROPOSED PHASE:
ARCHITECTURE
+
SECURITY RESEARCH
+
TECHNICAL RESEARCH

CLIENT_APPROVAL_GATE:
PENDING_APPROVAL
```

PARE ao terminar.

NÃO inicie Architecture automaticamente.

Aguarde minha autorização explícita.