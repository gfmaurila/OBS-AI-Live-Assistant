# Pesquisa — secrets no Windows

| Opção | Modelo | Rotação/uninstall | Interop | Recomendação |
|---|---|---|---|---|
| Credential Manager | Credential set do usuário | APIs Write/Read/Delete | Win32 e wrappers .NET | Preferida para API keys/tokens discretos |
| DPAPI user-scope | Criptografia ligada ao usuário/máquina | App gerencia blob e lifecycle | Win32/.NET | Para blobs app-owned quando necessário |
| DPAPI machine-scope | Qualquer usuário local pode decriptar sob o escopo da máquina | Risco maior | Win32/.NET | Não usar por padrão |

O produto deve armazenar apenas uma referência ao secret nos dados comuns. UI mascara valor, logs/exceptions/prompts nunca o recebem, rotação substitui atomicamente e disconnect revoga/remove quando aplicável. Backup/restauração e opção de remoção no uninstall precisam de ADR/política.

Fontes: SRC-021..023. Confidence: **HIGH**. Status: **RESOLVED**.
