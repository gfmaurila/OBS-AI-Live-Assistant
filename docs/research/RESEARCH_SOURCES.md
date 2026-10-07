# Registro de fontes oficiais

Todas as fontes foram acessadas em **2026-10-06**. Versões e limites são fotografia dessa data e devem ser revalidados antes da implementação/release.

| ID | Fonte oficial | Evidência usada |
|---|---|---|
| SRC-001 | https://github.com/obsproject/obs-studio/releases | OBS 32.2.2 estável; releases e artefatos x64. |
| SRC-002 | https://github.com/obsproject/obs-studio | Código, headers e build oficiais. |
| SRC-003 | https://docs.obsproject.com/reference-frontend-api | Eventos e API de Dock do frontend. |
| SRC-004 | https://github.com/obsproject/obs-plugintemplate | Template CMake e toolchain Windows. |
| SRC-005 | https://obsproject.com/kb/developer-guide | Plugins nativos, scripts e WebSocket. |
| SRC-006 | https://obsproject.com/kb/plugins-guide | Layout de plugin aplicável ao OBS 32. |
| SRC-007 | https://obsproject.com/kb/legacy-plugin-locations | Transição de layout no OBS 33/34. |
| SRC-008 | https://github.com/obsproject/obs-studio/blob/master/docs/sphinx/plugins.rst | Registro de sources via libobs. |
| SRC-009 | https://github.com/obsproject/obs-studio/blob/master/libobs/obs-source.h | Capacidades de source e áudio. |
| SRC-010 | https://obsproject.com/kb/application-audio-capture-guide | Captura de áudio de aplicativo no Windows. |
| SRC-011 | https://obsproject.com/kb/audio-sources | Sources, monitoramento e risco de eco. |
| SRC-012 | https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md | Protocolo 5.x, auth, eventos e requests. |
| SRC-013 | https://obsproject.com/kb/obs-studio-28-plugin-compatibility | obs-websocket integrado desde OBS 28. |
| SRC-014 | https://learn.microsoft.com/en-us/windows/win32/ipc/named-pipes | Modelo de Named Pipes. |
| SRC-015 | https://learn.microsoft.com/en-us/windows/win32/ipc/named-pipe-security-and-access-rights | DACL, acesso remoto e logon SID. |
| SRC-016 | https://learn.microsoft.com/en-us/dotnet/standard/io/pipe-operations | Suporte .NET a pipes duplex. |
| SRC-017 | https://learn.microsoft.com/en-us/windows/win32/procthread/job-objects | Gestão e limites de processos. |
| SRC-018 | https://www.sqlite.org/wal.html | WAL, concorrência e correções de corrupção. |
| SRC-019 | https://sqlite.org/backup.html | Online Backup API e snapshots. |
| SRC-020 | https://www.sqlite.org/see/doc/trunk/www/readme.wiki | Criptografia não pertence ao core SQLite. |
| SRC-021 | https://learn.microsoft.com/en-us/windows/win32/api/wincred/nf-wincred-credwritew | Escrita no credential set do usuário. |
| SRC-022 | https://learn.microsoft.com/en-us/windows/win32/api/wincred/ | APIs Credential Manager. |
| SRC-023 | https://learn.microsoft.com/en-us/windows/win32/api/dpapi/nf-dpapi-cryptprotectdata | Escopos e integridade do DPAPI. |
| SRC-024 | https://developers.google.com/youtube/v3/live/docs | Recursos da Live Streaming API. |
| SRC-025 | https://developers.google.com/youtube/v3/live/docs/liveChatMessages/list | Polling interval, erros e paginação. |
| SRC-026 | https://developers.google.com/youtube/v3/live/streaming-live-chat | Streaming gRPC de chat. |
| SRC-027 | https://developers.google.com/youtube/v3/live/docs/liveBroadcasts | Descoberta de `liveChatId`. |
| SRC-028 | https://developers.google.com/youtube/v3/guides/auth/installed-apps | OAuth desktop, loopback e PKCE. |
| SRC-029 | https://developers.google.com/youtube/v3/determine_quota_cost | Quota e custos por operação. |
| SRC-030 | https://developers.google.com/youtube/terms/developer-policies | Retenção, consentimento e exclusão. |
| SRC-031 | https://platform.openai.com/docs/api-reference/authentication | API key e autenticação OpenAI. |
| SRC-032 | https://platform.openai.com/docs/api-reference/responses-streaming | Streaming e eventos OpenAI. |
| SRC-033 | https://platform.claude.com/docs/en/api/errors | Erros, request ID, retries e falha em stream. |
| SRC-034 | https://platform.claude.com/docs/en/api/messages | Modelo de mensagens Anthropic. |
| SRC-035 | https://ai.google.dev/api | API e streaming Gemini. |
| SRC-036 | https://ai.google.dev/gemini-api/docs/api-key | Autenticação por API key. |
| SRC-037 | https://openrouter.ai/docs/api/api-reference/chat/send-chat-completion-request | Chat, streaming e usage OpenRouter. |
| SRC-038 | https://docs.ollama.com/api/introduction | API local e compatibilidade OpenAI. |
| SRC-039 | https://docs.ollama.com/windows | Requisitos Windows, disco e GPU. |
| SRC-040 | https://learn.microsoft.com/en-us/uwp/api/windows.media.speechsynthesis.speechsynthesizer | TTS local e vozes instaladas. |
| SRC-041 | https://learn.microsoft.com/en-us/azure/cognitive-services/speech-service/rest-text-to-speech | Azure Speech, SSML e formatos. |
| SRC-042 | https://elevenlabs.io/docs/api-reference/text-to-speech/stream | Streaming, formatos e autenticação. |
| SRC-043 | https://elevenlabs.io/docs/api-reference/reducing-latency | Trade-offs de streaming/latência. |
| SRC-044 | https://learn.microsoft.com/en-us/windows/msix/ | Capacidades e ciclo de vida MSIX. |
| SRC-045 | https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/packaging/ | Opções packaged e unpackaged. |
| SRC-046 | https://docs.firegiant.com/wix/ | WiX Toolset e Burn. |
| SRC-047 | https://jrsoftware.org/ishelp/topic_setupsection.htm | Inno Setup: arquitetura, signing e uninstall. |
| SRC-048 | https://jrsoftware.org/ishelp/topic_admininstallmode.htm | Instalação administrativa/per-user. |
| SRC-049 | https://jrsoftware.org/ishelp/topic_setup_signeduninstaller.htm | Assinatura do uninstaller. |
| SRC-050 | https://learn.microsoft.com/en-us/windows/msix/app-installer/auto-update-and-repair--overview | Update/repair por App Installer. |
| SRC-051 | https://learn.microsoft.com/en-us/dotnet/core/extensions/logging/source-generation | Logging estruturado e redaction. |
| SRC-052 | https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.compliance.redaction | API de classificação/redaction. |
| SRC-053 | https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/code-signing-options | Opções de assinatura Windows. |
| SRC-054 | https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/smartscreen-reputation | Reputação SmartScreen e publisher. |
| SRC-055 | https://learn.microsoft.com/en-us/dotnet/core/install/windows | Matriz .NET 10 por edição/arquitetura Windows. |
| SRC-056 | https://learn.microsoft.com/en-us/dotnet/core/releases-and-support | .NET 10 LTS e suporte até novembro de 2028. |

Total de fontes oficiais únicas: **56**.
