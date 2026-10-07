# Security Requirements

Esta pasta contém a especificação canônica de segurança da V1 do OBS-AI-Live-Assistant. Os documentos definem **o que** deve ser protegido e quais resultados são verificáveis; mecanismos técnicos permanecem para Research, Architecture e ADR.

## Documentos

- [Requisitos de segurança](SECURITY_REQUIREMENTS.md)
- [Threat Model](THREAT_MODEL.md)
- [Trust Boundaries](TRUST_BOUNDARIES.md)
- [Classificação de ativos](ASSET_CLASSIFICATION.md)
- [Secrets e BYOK](SECRETS_AND_BYOK.md)
- [Segurança de entrada e saída](INPUT_OUTPUT_SECURITY.md)
- [Proteção do OBS](OBS_SECURITY.md)
- [Logging e privacidade](LOGGING_AND_PRIVACY.md)
- [Riscos de segurança](SECURITY_RISKS.md)
- [Dependências de Research](SECURITY_RESEARCH_DEPENDENCIES.md)
- [Rastreabilidade](SECURITY_TRACEABILITY.md)
- [Security Quality Gate](SECURITY_QUALITY_GATE.md)

## Estado

- Requirements Quality Gate: **PASSED**
- Security Requirements: **CONCLUÍDO**
- Security Quality Gate: **PASSED**
- Architecture Security Readiness: **READY**
- Research externo: **NOT STARTED**
- Architecture: **NOT STARTED**

`READY` autoriza somente a preparação da próxima fase. Não aprova mecanismo de secrets, IPC, integração OBS, banco, installer, provider ou implementação.
