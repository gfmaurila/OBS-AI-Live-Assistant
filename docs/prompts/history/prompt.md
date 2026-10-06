# GERAR TEMPLATE BASE — OBS-AI-Live-Assistant

Você deverá analisar, planejar e gerar o **template estrutural inicial** do projeto:

```text
OBS-AI-Live-Assistant
```

Fale comigo em **português do Brasil**.

O código-fonte, nomes técnicos, documentação técnica, comentários estruturais, agentes, Skills e arquivos de engenharia devem seguir o padrão definido pelo **Kit IA Dev**.

---

# 1. CAMINHO DO PROJETO

Projeto:

```text
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant
```

Antes de qualquer alteração:

1. leia integralmente as instruções instaladas pelo Kit IA Dev;
2. leia `CLAUDE.md`;
3. leia `AGENTS.md`;
4. descubra os Agents disponíveis;
5. descubra as Skills disponíveis;
6. leia o Knowledge Dictionary;
7. analise a estrutura atual do repositório;
8. identifique arquivos existentes;
9. identifique decisões já documentadas;
10. não sobrescreva arquivos importantes silenciosamente.

---

# 2. KNOWLEDGE DICTIONARY

O Knowledge Dictionary oficial está localizado em:

```text
D:\Empresa\GFMaurila\projetos\Kit-IA-Dev\dicionario
```

Leia o conteúdo aplicável antes de produzir:

```text
Requirements
Architecture
Security Design
Database Design
Execution Plan
Backlog
Tasks
```

Use o conhecimento como referência.

Não copie padrões cegamente.

Adapte-os às necessidades reais do:

```text
OBS-AI-Live-Assistant
```

---

# 3. OBJETIVO DO PRODUTO

O projeto será um **assistente inteligente para transmissões ao vivo integrado ao OBS Studio**.

O sistema deverá permitir que espectadores conversem com um assistente de IA através do chat da transmissão.

Fluxo conceitual:

```text
Viewer
   ↓
Live Chat
   ↓
Message Listener
   ↓
Filters / Moderation / Rate Limit
   ↓
AI Assistant
   ↓
AI Provider
   ↓
Response
   ├──→ Text → Live Chat
   │
   └──→ TTS
           ↓
        OBS Audio
```

Exemplo:

```text
Viewer:
@assistente qual é a rota atual?

AI:
Estamos indo de Vancouver para Calgary.
Ainda faltam aproximadamente 620 km.
```

A resposta poderá aparecer:

```text
TEXT → Live Chat
VOICE → OBS
```

---

# 4. STACK PRINCIPAL

Utilize como direção tecnológica:

```text
Native OBS Integration:
C / C++

Assistant Core:
C# / .NET 10

Database:
SQLite / SQL

UI:
OBS-integrated Dock

Communication:
WebSocket / IPC when appropriate

Testing:
xUnit for .NET
appropriate native tests for C/C++

Operating System V1:
Windows 10 / Windows 11 x64
```

Não introduza microsserviços sem necessidade arquitetural comprovada.

A arquitetura inicial deverá priorizar:

```text
Modular Monolith
+
Plugin Architecture
+
Provider Architecture
```

---

# 5. OBS STUDIO

O produto deverá ser instalável e utilizável juntamente com o **OBS Studio**.

O plugin deverá fornecer integração nativa com OBS quando necessária.

Separar claramente:

```text
OBS Native Plugin
        │
        │ IPC / controlled integration
        ▼
Assistant Core
```

Responsabilidade preferencial do plugin nativo:

```text
OBS integration
OBS lifecycle
OBS events
Dock integration
audio integration
communication with Assistant Core
```

Responsabilidade preferencial do .NET Core:

```text
Chat
AI
TTS
Business Rules
Moderation
Memory
Context
Providers
Database
Security
Logging
Configuration
```

Não colocar toda a lógica de negócio dentro do plugin C/C++.

---

# 6. DIRETÓRIO DE DADOS DO OBS

O ambiente do usuário OBS está localizado em:

```text
C:\Users\gfmau\AppData\Roaming\obs-studio
```

Os dados específicos do projeto deverão ficar isolados preferencialmente em:

```text
C:\Users\gfmau\AppData\Roaming\obs-studio\obs-ai-live-assistant
```

Estrutura planejada:

```text
obs-ai-live-assistant/
├── config/
├── data/
├── database/
├── logs/
├── cache/
└── temp/
```

IMPORTANTE:

Não assuma que os binários do plugin nativo devem ser instalados diretamente nesse diretório.

Antes de definir o destino de:

```text
DLL
plugin binaries
dependencies
resources
```

descubra e documente a estrutura de instalação suportada pela versão alvo do OBS Studio.

Separe:

```text
Plugin Installation Path
```

de:

```text
Application Data Path
```

---

# 7. BANCO DE DADOS

Utilizar banco relacional SQL.

Para V1 utilizar preferencialmente:

```text
SQLite
```

Arquivo esperado:

```text
%APPDATA%\obs-studio\obs-ai-live-assistant\database\obs-ai-live-assistant.db
```

Planejar entidades para, no mínimo:

```text
Settings
Providers
Assistant Profiles
Chat Users
Live Sessions
Interactions
Commands
Moderation Rules
Usage Statistics
```

Não criar tabelas desnecessárias.

Utilizar migrations/versionamento de schema.

---

# 8. SEGURANÇA E CREDENCIAIS

O projeto seguirá:

```text
BYOK
Bring Your Own Key
```

Cada usuário poderá configurar seus próprios providers.

Nunca armazenar em texto puro:

```text
API Keys
OAuth Tokens
Refresh Tokens
Provider Secrets
Passwords
```

Não salvar segredos diretamente em:

```text
SQLite
appsettings.json
.env
logs
```

Planejar mecanismo seguro para Windows, utilizando recurso adequado como:

```text
Windows Credential Manager
DPAPI
```

ou outra solução tecnicamente justificada.

Nunca registrar secrets nos logs.

---

# 9. AI PROVIDER ARCHITECTURE

O sistema deverá possuir arquitetura extensível de providers.

Exemplo conceitual:

```text
IAIProvider
    │
    ├── OpenAIProvider
    ├── AnthropicProvider
    ├── GeminiProvider
    ├── OllamaProvider
    ├── OpenRouterProvider
    └── CustomProvider
```

IMPORTANTE:

Não acoplar o Core diretamente a um único fornecedor.

O usuário deverá poder escolher:

```text
Provider
Model
Configuration
Credentials
```

Providers poderão ser adicionados posteriormente sem modificar o núcleo principal.

---

# 10. TTS PROVIDER ARCHITECTURE

Aplicar o mesmo conceito para voz:

```text
ITtsProvider
     │
     ├── WindowsTtsProvider
     ├── AzureSpeechProvider
     ├── ElevenLabsProvider
     └── FutureProvider
```

O sistema deverá permitir:

```text
Voice selection
Language
Volume
Speed
Provider
Enable / Disable
```

O áudio gerado deverá poder ser reproduzido corretamente durante a transmissão através do OBS.

---

# 11. CHAT PROVIDER ARCHITECTURE

Preparar arquitetura extensível:

```text
IChatProvider
      │
      ├── YouTube
      ├── Twitch
      └── Future Providers
```

Para V1, priorizar:

```text
YouTube
```

Preparar a arquitetura para Twitch sem exigir implementação completa imediatamente.

---

# 12. MODOS DE ATIVAÇÃO

Planejar suporte para:

```text
Mention Mode
Command Mode
Manual Mode
Auto Mode
Narrator Mode
Silent Mode
Voice Mode
```

V1 deverá priorizar:

```text
@assistente pergunta
```

e:

```text
!ia pergunta
```

Também deverá existir possibilidade de ativação manual pelo streamer.

O modo automático não deverá responder indiscriminadamente a todas as mensagens.

---

# 13. MESSAGE PIPELINE

Planejar explicitamente:

```text
Live Chat
    ↓
Message Listener
    ↓
Message Normalization
    ↓
Trigger Detection
    ↓
Moderation
    ↓
Rate Limiter
    ↓
Request Queue
    ↓
Context Builder
    ↓
AI Provider
    ↓
Response Validation
    ↓
Response Queue
    ├──→ Text
    └──→ TTS Queue
              ↓
           OBS Audio
```

Este pipeline deverá evitar:

```text
spam
multiple simultaneous narrations
AI flooding
duplicate requests
excessive token usage
abusive users
```

---

# 14. RATE LIMITING

Planejar:

```text
Global cooldown
Per-user cooldown
Maximum queued messages
Maximum response length
Maximum TTS duration
Request timeout
```

Esses valores deverão ser configuráveis.

---

# 15. MODERAÇÃO

Antes da resposta ser narrada ou enviada ao chat:

```text
Input moderation
        ↓
AI
        ↓
Output moderation
```

Planejar:

```text
blocked words
blocked users
ignored users
maximum message size
maximum response size
anti-spam
prompt injection protection
unsafe-content controls
```

O streamer deverá poder configurar regras básicas.

---

# 16. PERSONALIDADE DO ASSISTENTE

O streamer deverá poder criar perfis de assistente.

Exemplo:

```text
Name:
Maia

Language:
pt-BR

Personality:
Friendly
Humorous
Slightly sarcastic
Non-offensive

Response Length:
Short

Voice:
Configured TTS Voice
```

Permitir futuramente múltiplos perfis.

---

# 17. CONTEXTO DA LIVE

Criar conceito de:

```text
LiveContext
```

Ele poderá conter:

```text
Streamer
Live title
Platform
Game
Current OBS Scene
Assistant Profile
Recent interactions
Session information
Custom context
```

A IA deverá receber somente o contexto necessário.

Evitar enviar histórico ilimitado para o provider.

---

# 18. MEMÓRIA

Separar:

```text
Short-Term Memory
```

de:

```text
Persistent Memory
```

Short-Term:

```text
contexto da live atual
mensagens recentes
respostas recentes
```

Persistent:

```text
configurações
perfis
preferências permitidas
estatísticas
```

Não criar armazenamento indiscriminado de conversas.

Planejar retenção e limpeza.

---

# 19. OBS DOCK

Planejar um Dock integrado ao OBS semelhante conceitualmente a:

```text
┌──────────────────────────────────┐
│ OBS AI Live Assistant            │
├──────────────────────────────────┤
│ Status: ● Online                 │
│                                  │
│ Platform: YouTube                │
│                                  │
│ AI Provider: [ Provider ▼ ]      │
│ Model:       [ Model    ▼ ]      │
│                                  │
│ TTS:          [ ON ]             │
│ Voice:        [ Voice ▼ ]        │
│                                  │
│ Trigger: @assistente             │
│                                  │
│ Queue: 2                         │
│                                  │
│ [ Start Assistant ]              │
│ [ Stop Assistant  ]              │
│                                  │
│ Recent Interactions              │
└──────────────────────────────────┘
```

Não considere esse desenho como UI final.

Ele representa somente os requisitos funcionais principais.

---

# 20. LOGGING E OBSERVABILIDADE

Implementar planejamento para logs estruturados.

Separar:

```text
Application Logs
AI Logs
Chat Logs
TTS Logs
OBS Integration Logs
Security Logs
```

Nunca registrar secrets.

Implementar níveis:

```text
Trace
Debug
Information
Warning
Error
Critical
```

Preparar diagnóstico para suporte.

---

# 21. RESILIÊNCIA

Providers externos podem falhar.

Planejar:

```text
Timeout
Retry
Cancellation
Circuit Breaker where appropriate
Provider health
Graceful degradation
```

Exemplo:

```text
AI unavailable
    ↓
Do not crash OBS
    ↓
Disable request temporarily
    ↓
Display status in Dock
    ↓
Log diagnostic
```

Uma falha do Assistant Core nunca deverá derrubar o OBS Studio.

---

# 22. ISOLAMENTO DE PROCESSO

Avalie seriamente manter:

```text
OBS Native Plugin
```

e:

```text
Assistant Core
```

em processos/componentes isolados.

Objetivo:

```text
AI failure ≠ OBS failure
TTS failure ≠ OBS failure
Chat failure ≠ OBS failure
Database failure ≠ OBS failure
```

Documente a decisão arquitetural.

---

# 23. INSTALADOR

O produto final deverá possuir instalação simples.

Experiência desejada:

```text
OBS-AI-Live-Assistant-Setup.exe
        ↓
Detect OBS
        ↓
Install Native Plugin
        ↓
Install Assistant Core
        ↓
Create application data structure
        ↓
Register required components
        ↓
Finish
```

Também planejar:

```text
Upgrade
Repair
Uninstall
```

Durante uninstall perguntar ou oferecer opção para:

```text
Keep settings/database
```

ou:

```text
Remove all application data
```

---

# 24. VERSIONAMENTO

Utilizar Semantic Versioning:

```text
MAJOR.MINOR.PATCH
```

Exemplo:

```text
1.0.0
1.1.0
1.1.1
2.0.0
```

Versionar separadamente quando necessário:

```text
OBS Plugin
Assistant Core
Database Schema
```

Garantir compatibilidade entre componentes.

---

# 25. GITFLOW

O projeto deverá utilizar:

```text
main
develop
hml
```

Fluxo:

```text
develop
   │
   ├── feature/task-001-...
   ├── feature/task-002-...
   └── feature/task-003-...
           │
           ▼
           PR
           │
           ▼
        develop
           │
           ▼
          hml
           │
           ▼
    release/1.0.0
           │
           ▼
          main
```

Cada Task deverá possuir sua própria branch quando aplicável.

A IA deverá:

```text
implement
test
review
commit
push
create PR
```

somente quando o estágio do pipeline permitir.

Nenhuma release para `main` sem Quality Gates.

---

# 26. TESTES

Planejar:

```text
Unit Tests
Integration Tests
Provider Contract Tests
Database Tests
Security Tests
OBS Integration Tests
Installer Tests
```

Testar especialmente:

```text
AI unavailable
TTS unavailable
Chat disconnected
OBS closed
database locked/corrupted
invalid API key
rate limit
network unavailable
provider timeout
```

---

# 27. FUTURAS EXTENSÕES

A arquitetura deverá permitir módulos futuros sem implementá-los agora.

Exemplos:

```text
ATS Telemetry
ETS2 Telemetry
Game Events
Stream Deck
Discord
Custom Webhooks
Custom Chat Providers
Additional AI Providers
Additional TTS Providers
RAG
Local Knowledge
Multi-Agent capabilities
```

Esses itens são:

```text
FUTURE / OUT OF V1
```

a menos que posteriormente aprovados.

---

# 28. ESCOPO DA V1

Priorizar uma primeira versão funcional pequena.

V1 proposta:

```text
OBS integration
OBS Dock
Assistant Core
SQLite
YouTube Chat
@assistente
!ia
Manual activation
One cloud AI provider
One local AI provider if viable
Basic TTS
Text response
Voice response
Queue
Rate limiting
Basic moderation
Assistant Profile
Live Context
Secure credentials
Logging
Installer
```

Evite implementar todas as integrações possíveis na V1.

A arquitetura deve ser extensível; a implementação inicial deve permanecer controlada.

---

# 29. ARQUITETURA DE REFERÊNCIA

A direção conceitual é:

```text
                    OBS STUDIO
                         │
                 Native OBS Plugin
                    C / C++
                         │
                  IPC / Bridge
                         │
                         ▼
              OBS-AI-Live-Assistant
                    .NET 10
                         │
        ┌────────────────┼────────────────┐
        │                │                │
        ▼                ▼                ▼
   Chat Gateway      AI Gateway       TTS Gateway
        │                │                │
        ▼                ▼                ▼
     YouTube          Providers         Voices
     Twitch             │
        │               │
        └───────┬───────┘
                │
                ▼
          Assistant Engine
                │
       ┌────────┼─────────┐
       ▼        ▼         ▼
    Context   Memory   Moderation
       │        │         │
       └────────┼─────────┘
                ▼
             SQLite
```

Não trate esse desenho como arquitetura final.

O Architect Agent deverá validar, melhorar e documentar a arquitetura definitiva.

---

# 30. SOLID E PRINCÍPIOS DE ENGENHARIA

Aplicar:

```text
SOLID
Clean Code
Separation of Concerns
Dependency Inversion
Dependency Injection
Interface Segregation
Fail Fast
Secure by Default
Testability
Observability
Extensibility
```

Não aplicar padrões apenas por moda.

Toda abstração importante deverá possuir justificativa arquitetural.

---

# 31. ORDEM OBRIGATÓRIA

NÃO comece criando código imediatamente.

Execute:

```text
BOOTSTRAP
   ↓
PROJECT DISCOVERY
   ↓
KNOWLEDGE DISCOVERY
   ↓
KNOWLEDGE QUALITY GATE
   ↓
REQUIREMENTS
   ↓
SECURITY REQUIREMENTS
   ↓
ARCHITECTURE
   ↓
DATABASE DESIGN
   ↓
OBS INTEGRATION DESIGN
   ↓
AI PROVIDER DESIGN
   ↓
TTS PROVIDER DESIGN
   ↓
CHAT PROVIDER DESIGN
   ↓
INSTALLATION DESIGN
   ↓
EXECUTION PLAN
   ↓
DEPENDENCY GRAPH
   ↓
BACKLOG
   ↓
TASKS
   ↓
CLIENT APPROVAL GATE
```

Somente depois:

```text
IMPLEMENTATION
```

---

# 32. DOCUMENTOS ESPERADOS

Utilize os padrões do Kit IA Dev.

Produza ou atualize, quando aplicável:

```text
REQUIREMENTS.md
ARCHITECTURE_PLAN.md
SECURITY_PLAN.md
DATABASE_DESIGN.md
OBS_INTEGRATION.md
AI_PROVIDER_ARCHITECTURE.md
TTS_PROVIDER_ARCHITECTURE.md
CHAT_PROVIDER_ARCHITECTURE.md
INSTALLATION_PLAN.md
EXECUTION_PLAN.md
DEPENDENCY_GRAPH.md
```

Além das Tasks exigidas pelo Kit.

Não duplique documentação que já possua equivalente oficial no Kit.

---

# 33. QUALITY GATES

Antes de permitir implementação, valide:

```text
Requirements complete
Architecture approved
Security reviewed
Database reviewed
OBS integration reviewed
Provider boundaries defined
Installer strategy defined
Tasks generated
Dependencies mapped
Testing strategy defined
No unresolved critical conflicts
```

Qualquer problema crítico:

```text
BLOCK IMPLEMENTATION
```

---

# 34. CLIENT APPROVAL GATE

Depois de gerar:

```text
Requirements
Architecture
Security
Database Design
Integration Designs
Execution Plan
Backlog
Tasks
Dependency Graph
```

PARE.

Mostre um resumo:

```text
OBS-AI-Live-Assistant

Planning: COMPLETED

Requirements: READY
Architecture: READY
Security: READY
Database: READY
OBS Integration: READY
AI Providers: READY
TTS Providers: READY
Chat Providers: READY
Installer: READY
Execution Plan: READY
Tasks: READY

Implementation:
NOT STARTED

CLIENT_APPROVAL_GATE:
PENDING_APPROVAL
```

NÃO implemente nenhuma Task.

Aguarde minha autorização explícita.

---

# 35. PRIMEIRA EXECUÇÃO

Comece agora por:

```text
BOOTSTRAP
+
PROJECT DISCOVERY
+
KNOWLEDGE DISCOVERY
```

Leia primeiro as instruções existentes do Kit IA Dev e o Knowledge Dictionary.

Depois analise:

```text
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant
```

Identifique:

```text
current project state
Git state
branches
installed Kit IA Dev components
Agents
Skills
existing documentation
existing source code
existing architecture decisions
conflicts
missing prerequisites
```

NÃO implemente código.

Ao concluir essa primeira análise:

```text
STOP.
```

Apresente os resultados e aguarde minha autorização para iniciar Requirements.