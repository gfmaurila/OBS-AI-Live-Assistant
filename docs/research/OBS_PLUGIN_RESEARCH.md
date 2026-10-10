# Pesquisa técnica: plugin OBS 32.x

## Estado da evidência

Este documento separa evidência da proposta de execução. Pesquisa documental não equivale a build, smoke test ou aprovação de compatibilidade.

- **SOURCE VERIFIED** — O repositório oficial `obsproject/obs-plugintemplate` disponibiliza projeto CMake e `CMakePresets.json`; na revisão candidata, os presets Windows são `windows-x64` e `windows-ci-x64`, com gerador `Visual Studio 17 2022`, Windows SDK `10.0.22621` e CMake mínimo `3.28.0`. Referências versionadas: [presets CMake](https://github.com/obsproject/obs-plugintemplate/blob/3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc/CMakePresets.json) e [buildspec](https://github.com/obsproject/obs-plugintemplate/blob/3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc/buildspec.json).
- **PROPOSED** — OBS Studio 32.1.2 x64 como baseline mínima proposta; OBS Studio 32.2.2 x64 como alvo adicional. São versões distintas da matriz e não uma faixa automaticamente suportada.
- **PROPOSED** — Iniciar avaliação de toolchain com Visual Studio 2026, MSVC e CMake disponíveis no ambiente declarado. A configuração oficial do template ainda indica VS 2022; por isso, VS 2022 é fallback condicionado a incompatibilidade concreta reproduzível com VS 2026.
- **BUILD NOT VALIDATED** — Nenhuma configuração, compilação ou teste do plugin foi executado para este relatório.
- **COMPATIBILITY NOT VERIFIED** — Não há validação do plugin em OBS 32.1.2 nem 32.2.2. Não declarar suporte a nenhuma dessas versões com base apenas nesta pesquisa.

## Identificação do ambiente e divergência

O inventário de ferramentas do projeto declara Visual Studio Community 2026, MSVC, CMake e Windows SDK disponíveis. A evidência anterior de compatibilidade OBS 32.1.2 em `ADR-010` descreve Windows 11 25H2 x64 build 26200, runtime .NET 10.0.12 e OBS 32.1.2, mas valida apenas o protótipo de detecção, não um plugin C++.

A identidade/versão efetiva do Windows e as versões efetivamente invocadas de Visual Studio, MSVC, CMake e Windows SDK devem ser coletadas no Windows durante a futura aquisição e execução do Gate. A divergência entre inventário declarado e ambiente de build ainda não foi reconciliada. Esta pesquisa não afirma que tais ferramentas foram verificadas nesta execução.

## Template, revisão e dependências

Template candidato: `https://github.com/obsproject/obs-plugintemplate`, revisão candidata `3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc`. A revisão deverá ser baixada por SHA completo e conferida antes do uso; não usar uma branch mutável como fonte reproduzível.

O preset oficial publicado declara CMake mínimo 3.28.0 e gerador Visual Studio 17 2022, com arquitetura x64 e Windows SDK 10.0.22621 no preset consultado. Esses valores descrevem a configuração do template naquela referência, não provam compatibilidade com VS 2026, com a revisão candidata ou com OBS 32.1.2/32.2.2.

O `buildspec.json` oficial da revisão candidata registra as dependências abaixo. Os hashes são os valores publicados no próprio template e ainda precisam ser comparados com os arquivos obtidos antes da extração. Há uma incompatibilidade relevante: o manifesto fixa sources OBS **31.1.1**, que não corresponde às versões candidatas 32.1.2/32.2.2. Logo, esses sources não podem ser usados para validar a matriz proposta. É necessário obter manifesto oficial compatível com cada versão OBS ou outra associação oficial verificável, sem substituir hashes por valores presumidos.

| Dependência publicada no buildspec | Versão | SHA-256 Windows x64 | Aplicação nesta Task |
|---|---|---|---|
| OBS sources | 31.1.1 | `2c8427c10b55ac6d68008df2e9a3e82f4647aaad18f105e30d4713c2de678ccf` | Incompatível com matriz 32.1.2/32.2.2; não usar para esses alvos |
| Pre-built obs-deps | 2025-07-11 | `c8c642c1070dc31ce9a0f1e4cef5bb992f4bff4882255788b5da12129e85caa7` | Publicado para o template; confirmar compatibilidade com sources OBS fixados |
| Pre-built Qt6 | 2025-07-11 | `0e76bf0555dd5382838850b748d3dcfab44a1e1058441309ab54e1a65b156d0a` | O template desativa Qt no preset base; não adquirir para o escopo sem Qt da TASK-024 |
| Qt6 debug symbols | 2025-07-11 | `11b7be92cf66a273299b8f3515c07a5cfb61614b59a4e67f7fc5ecba5e2bdf21` | Não aplicável enquanto Qt estiver desativado |

Esses valores identificam o conteúdo do manifesto oficial na revisão candidata; não são hashes calculados nesta execução e não demonstram que os artefatos foram baixados ou verificados. Obter sources e dependências oficialmente correspondentes às versões OBS alvo, comparar SHA-256 antes da extração e registrar a evidência no Native Build Gate. A incompatibilidade dos sources mantém a aquisição bloqueada.

## Baseline candidata

| Dimensão | Baseline/proposta | Estado |
|---|---|---|
| OBS mínimo candidato | 32.1.2 x64 | PROPOSED; compatibilidade não verificada |
| OBS adicional | 32.2.2 x64 | PROPOSED; compatibilidade não verificada |
| Template | `obsproject/obs-plugintemplate` | SOURCE VERIFIED |
| Revisão do template | `3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc` | Candidato fixado; aquisição e conteúdo ainda precisam de verificação local |
| Toolchain inicial | Visual Studio 2026 / MSVC / CMake | PROPOSED; build não validado |
| Fallback | Visual Studio 2022 | Condicionado à incompatibilidade demonstrada com VS 2026 |
| Dependências e SHA-256 | `buildspec.json` oficial da revisão fixada | Sources declarados como OBS 31.1.1; incompatíveis com os alvos 32.x candidatos; hashes registrados acima, verificação de artefatos pendente |

## Direção arquitetural relacionada

ADR-001 aprovou integração híbrida: usar o componente OBS nativo apenas para capabilities comprovadamente nativas e preferir obs-websocket para capacidades já expostas. ADR-003 exige Assistant Core em processo separado. ADR-010 exige compatibilidade versionada e declaração limitada às versões efetivamente aprovadas nos testes. Estes ADRs permanecem inalterados.

A TASK-024 documentalmente refinada trata apenas de skeleton e lifecycle mínimo. Dock, áudio e IPC ficam nas Tasks correspondentes e não são inferidos deste estudo.

## Próxima evidência necessária

Executar, em Windows isolado, a aquisição reproduzível dos sources e dependências oficiais da revisão fixada; confirmar as versões e hashes antes de extrair; registrar a identidade do Windows e toolchain real; então executar o procedimento versionado em [`NATIVE_BUILD_GATE.md`](../testing/NATIVE_BUILD_GATE.md), uma versão OBS por vez. Até essas etapas ocorrerem, o build permanece não validado e a compatibilidade não verificada.

## Fontes

- [Template oficial obsproject/obs-plugintemplate](https://github.com/obsproject/obs-plugintemplate)
- [CMakePresets.json do template oficial](https://github.com/obsproject/obs-plugintemplate/blob/master/CMakePresets.json)
- [CMakePresets.json na revisão candidata](https://github.com/obsproject/obs-plugintemplate/blob/3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc/CMakePresets.json)
- [Buildspec e hashes oficiais na revisão candidata](https://github.com/obsproject/obs-plugintemplate/blob/3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc/buildspec.json)
- [Commit candidato do template](https://github.com/obsproject/obs-plugintemplate/commit/3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc)
- [Releases oficiais do OBS Studio](https://github.com/obsproject/obs-studio/releases)
- [ADR-001: integração híbrida](../architecture/decisions/ADR-001-obs-integration-strategy.md)
- [ADR-003: isolamento e lifecycle](../architecture/decisions/ADR-003-process-isolation-and-queues.md)
- [ADR-010: compatibilidade](../architecture/decisions/ADR-010-compatibility-strategy.md)
