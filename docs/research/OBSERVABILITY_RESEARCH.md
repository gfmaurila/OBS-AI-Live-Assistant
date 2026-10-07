# Pesquisa — observabilidade local

Adotar logging estruturado local com níveis, event IDs, correlation IDs por request e health resumido por adapter. Aplicar classificação de dados e redaction central antes de qualquer sink; nunca registrar secrets, headers de auth, tokens, chat/AI completos ou stack data sensível por padrão. Rotação, limite total, retenção e exportação diagnóstica devem ser configuráveis.

`Microsoft.Extensions.Compliance.Redaction` oferece primitives oficiais, mas não substitui allowlist de campos nem testes de vazamento. Diagnóstico deve funcionar offline, sem stack cloud obrigatória. Telemetry externa é opt-in futuro e depende de decisão de privacidade.

Fontes: SRC-051/052. Confidence: **HIGH**. Status: **RESOLVED**.
