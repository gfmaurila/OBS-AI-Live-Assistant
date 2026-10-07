# Definition of Done

Uma Task somente pode mudar para `DONE` depois de concluir, quando aplicável:

- implementação completa dentro do escopo aprovado;
- restore e build aprovados com comandos reais documentados;
- Unit, Integration, Architecture, Contract, Security, IPC, Failure, OBS Compatibility, Installer e End-to-End Tests aplicáveis aprovados;
- critérios de aceite e requisitos rastreados aprovados;
- validação de segurança e privacidade aprovada;
- Code Review aprovado;
- findings Critical = 0 e High = 0;
- findings Medium corrigidos quando seguros ou encaminhados explicitamente;
- Secret Scan aprovado com `Secrets: NONE`;
- documentação, ADRs, diagramas, comandos e rastreabilidade atualizados quando afetados;
- prompt autorizador arquivado no mesmo commit;
- commit e push concluídos na branch própria;
- pull request para `develop` criado e validado;
- merge em `develop` concluído somente após todos os gates;
- validação pós-merge aprovada;
- feature local e remota excluídas com segurança;
- `develop` local igual a `origin/develop` e Working Tree `CLEAN`.

Gate não aplicável deve ser marcado `NOT APPLICABLE` com justificativa; nunca `PASSED` sem execução. Promoção para `hml`, release, `main`, tag ou GitHub Release não integra o DoD de uma Task individual.
