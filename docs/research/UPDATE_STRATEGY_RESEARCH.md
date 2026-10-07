# Pesquisa — estratégia de atualização

| Opção | Segurança | Rollback | UX | Recomendação V1 |
|---|---|---|---|---|
| Manual | Usuário verifica/instala artefato assinado | Reinstalação explícita | Menos conveniente | Aceitável inicial |
| Installer-based | Verificação, repair e rollback centralizados | Planejável | Boa | Preferida |
| In-app automática | Maior superfície e signing/feed críticos | Complexo | Melhor | FUTURE após maturidade |

Nenhum update deve ocorrer enquanto OBS carrega o plugin. O fluxo valida assinatura/hash, compatibilidade OBS, fecha processos com consentimento, faz backup/migration e recupera versão anterior ou falha sem alterar instalação válida. MSIX App Installer suporta auto-update/repair, mas não resolve sozinho o deployment externo do plugin.

Fontes: SRC-044/045/050/053/054. Confidence: **HIGH** para controles, **MEDIUM** para tecnologia. Status: **RESOLVED**.
