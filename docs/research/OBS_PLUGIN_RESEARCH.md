# Pesquisa — plugin OBS 32.x

## Evidência e conclusão

OBS plugins são módulos nativos carregados no processo do OBS. O template oficial fornece CMake, Visual Studio 2022 e automação de build; headers/libs devem ser fixados à versão testada. Em 2026-10-06, **32.2.2** é a release estável da linha alvo.

No OBS 32, o layout recomendado documentado é `C:\ProgramData\obs-studio\plugins\<plugin>\bin\64bit\<plugin>.dll` e `data\...`. O OBS 33 introduz layout simplificado e o OBS 34 remove o layout de `Program Files`; o installer precisa detectar a versão e nunca copiar arquivos durante execução do OBS.

Recomendação para ADR: plugin x64 mínimo, dependências versionadas, callbacks curtos e sem acesso direto a providers/banco. Testar cada versão suportada; não assumir estabilidade ABI entre majors.

## Native Plugin versus WebSocket versus Hybrid

| Capability/atributo | Native Plugin | WebSocket | Hybrid |
|---|---|---|---|
| Dock Qt oficial | Sim | Não | Plugin |
| Source de áudio customizada | Sim | Não | Plugin, se aprovada |
| Estado/cenas/sources expostos | Sim | Sim | WebSocket preferível |
| Providers, banco e regras | Tecnicamente possível, inadequado | Processo externo | Processo externo |
| Segurança/failure impact | Alto acoplamento ao OBS | Boundary autenticável | Menor componente privilegiado |
| Distribuição/manutenção | DLL por versão/toolchain | Sem plugin para capacidades expostas | Mais componentes, responsabilidades claras |
| Recomendação | Apenas capacidades inevitáveis | Operações suportadas pelo protocolo | **Recomendada** |

Fontes: SRC-001..007. Confidence: **HIGH**. Status: **RESOLVED**.
