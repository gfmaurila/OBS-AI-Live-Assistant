# Documentação de Testing

Os testes do produto estão **NOT STARTED** porque não existem código-fonte funcional, projeto nativo, banco de dados ou instalador.

A `TASK-003` estabeleceu o **OBS Compatibility / smoke** por evidência executável: o protótipo read-only `prototypes/compat-sniff` foi executado no ambiente declarado (Windows 11 25H2 x64, .NET 10.0.12, OBS 32.1.2) e a matriz de compatibilidade é validada por testes determinísticos (`CompatibilityMatrixTests`, no projeto `ObsAi.Architecture.Tests`). As suítes executáveis de Integração, Contrato, Banco, Security, Installer e OBS Compatibility permanecem `NOT CREATED` até a `TASK-004` e Tasks posteriores.

Comandos determinísticos de restore, build, Unit Tests, Integration Tests, formatação, análise estática, build nativo, Installer Tests e OBS Compatibility Tests permanecem indefinidos até a aprovação das decisões correspondentes de tooling e Architecture.

As categorias futuras deverão considerar testes unitários, integração, contratos, banco de dados, Security, integração com OBS, instalador, compatibilidade, regressão e degradação em falhas. O escopo exato depende de Requirements e Architecture.
