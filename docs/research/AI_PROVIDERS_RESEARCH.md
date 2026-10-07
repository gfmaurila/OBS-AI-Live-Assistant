# Pesquisa — abstração de AI Providers

OpenAI, Anthropic, Gemini, OpenRouter e endpoints OpenAI-compatible divergem em schema, autenticação, streaming, usage, rate-limit e erros. O port comum futuro deve representar: secret por referência; provider/model; system instruction separada de input não confiável; mensagens normalizadas; streaming incremental; cancellation/timeout; limite de output; usage/finish reason/request ID opcionais; erros normalizados e metadata de retry.

Capacidades específicas devem ficar em `capabilities`, não ser fingidas pelo menor denominador comum. Tool calling ou ações sensíveis não entram implicitamente no contrato V1. Retries só em falhas idempotentes/transientes e limitados. A seleção do(s) provider(s) V1 permanece decisão do produto/ADR e requer revisão atual de termos.

Fontes: SRC-031..037. Confidence: **MEDIUM**, pois APIs evoluem. Status: **PARTIALLY_RESOLVED**.
