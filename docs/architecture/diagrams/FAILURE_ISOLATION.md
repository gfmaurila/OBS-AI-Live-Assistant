# Failure Isolation

```mermaid
flowchart LR
    AI[AI failure] --> D[Core capability degraded]
    T[TTS failure] --> D
    C[Chat failure] --> D
    DB[SQLite failure] --> D
    Net[Network failure] --> D
    Crash[Core crash/hang] --> X[IPC disconnect]
    D --> H[Health + redacted diagnostics]
    X --> H
    H --> Dock[Dock shows degraded/disconnected]
    X -. no propagation .-> OBS[OBS keeps streaming]
    D -. no propagation .-> OBS
    Saturation[Queue saturation] --> Reject[Controlled rejection/backpressure]
    Reject --> H
```

| Evento | Contenção | Recuperação |
|---|---|---|
| Core crash/hang | process boundary + IPC loss | restart/reconnect explícito; nova sessão |
| provider timeout/quota | timeout, cancel e retry limitado | backoff; ação do streamer quando necessário |
| DB lock/corruption | adapter falha sem sucesso silencioso | health/integrity + recovery/backup |
| queue saturation | capacidade finita | recusa/descarte observável; capacidade liberada |
| shutdown | fecha entrada e cancela trabalho | sem publicar resultados tardios |

Nenhum mecanismo reinicia automaticamente uma saída ambígua ou reduz controles para “continuar funcionando”.
