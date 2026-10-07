# Secrets, BYOK e OAuth

## BYOK

BYOK permite que o streamer inclua, valide, use, atualize, substitua e remova credenciais de providers sem acoplamento obrigatório a um vendor. O produto deve mostrar somente identificação não secreta e representação mascarada suficiente para o usuário reconhecer a configuração.

O lifecycle mínimo exige:

1. entrada por superfície apropriada, sem persistência transitória desnecessária;
2. validação sem ecoar o valor integral;
3. armazenamento separado da configuração comum e protegido contra leitura casual;
4. acesso somente ao componente e à operação que precisam da credencial;
5. substituição e remoção verificáveis;
6. revogação ou orientação de revogação quando o provider controlar o segredo;
7. comportamento explícito em upgrade, repair, backup e uninstall.

É proibido armazenar secrets em plaintext em SQLite, `appsettings`, JSON, `.env` versionado, logs, exceptions, telemetria, prompts, histórico de prompts ou relatórios. Também é proibido registrar secrets no Git.

## OAuth

Quando uma integração confirmada usar OAuth, o produto deve:

- usar fluxo apropriado a aplicativo local e somente scopes necessários;
- distinguir autorização, access token, refresh token, expiração e revogação;
- tratar falha de refresh como estado recuperável, sem loop ilimitado;
- interromper novos usos após revogação, logout ou desconexão confirmada;
- não inferir refresh token ou fluxo de um provider que não o ofereça;
- informar ao streamer o estado sem revelar token ou authorization code.

Os requisitos oficiais do YouTube e de cada provider serão verificados em `RES-014` e pesquisas específicas futuras, sem pesquisa externa nesta Task.

## Decisão pendente

Windows Credential Manager, DPAPI ou alternativa segura são opções a comparar. A escolha, escopo por usuário/máquina, recuperação, migração e interação com installer permanecem `REQUIRES_RESEARCH + REQUIRES_ADR` (`RES-012`, `RES-013`, `ADR-005`).
