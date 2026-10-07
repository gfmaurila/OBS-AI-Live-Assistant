# Fluxo de integração OBS

## Matriz de responsabilidades

| Capacidade | Native Component | obs-websocket | Assistant Core |
|---|---:|---:|---:|
| Dock e frontend lifecycle | Responsável | Não suporta | Estado e commands de UI |
| Source de áudio customizada | Somente se ADR-006 validado | Não suporta | Gera/entrega buffers aprovados |
| Estado, scenes, sources e events expostos | Evitar duplicação | Preferido | Policy + adapter |
| Autorização de ação sensível | Enforce contrato recebido | Transporte autenticado | Responsável, deny-by-default |
| Providers, moderação, persistência, secrets | Proibido | Não aplicável | Responsável |
| Health | Estado local/IPC | Capability/connection | Estado agregado |

```mermaid
sequenceDiagram
    participant S as Streamer/Dock
    participant P as Native OBS Component
    participant C as Assistant Core
    participant W as obs-websocket
    participant O as OBS Studio
    S->>P: comando de UI validável
    P->>C: IPC request versionado
    C->>C: policy + authorization + allowlist
    alt capability WebSocket
        C->>W: request autenticado
        W->>O: capability suportada
        O-->>W: result/event
        W-->>C: result validado
    else capability nativa aprovada
        C-->>P: comando mínimo autorizado
        P->>O: frontend/libobs API
    end
    C-->>P: estado redacted
    P-->>S: feedback
```
