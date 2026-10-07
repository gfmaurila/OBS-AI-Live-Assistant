# Pesquisa — isolamento de processo

| Opção | Confiabilidade | Complexidade | Impacto no OBS | Recomendação |
|---|---|---|---|---|
| Mesmo processo | Crash/hang pode derrubar OBS | Menor IPC | Alto | Rejeitar para Core |
| Processo separado | Falha, memória e restart contidos | Exige IPC/lifecycle | Baixo | Recomendada |

Windows Job Objects podem agrupar processos, aplicar limites e encerrar a árvore no fechamento. Isso é instrumento possível, não decisão final. O plugin deve sobreviver à perda de IPC, cancelar trabalho e nunca bloquear thread do OBS. O Core deve iniciar/parar de forma idempotente e expor health/liveness limitado.

Fonte: SRC-017. Confidence: **HIGH**. Status: **RESOLVED**.
