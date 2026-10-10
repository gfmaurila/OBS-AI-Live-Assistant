# Native Build Gate: plugin OBS

## Objetivo e limites

Este procedimento define evidências futuras para adquirir, configurar, compilar e testar o skeleton C++ do plugin em Windows x64. A publicação deste documento não executa o Gate nem aprova toolchain, build ou compatibilidade. Nesta etapa documental não se adquirem dependências, não se cria binário e não se altera a instalação do OBS.

O Gate limita-se ao lifecycle e aos eventos em memória definidos na TASK-024. Dock, Qt, áudio, IPC, Assistant Core, providers, banco, rede e secrets não pertencem a este Gate.

## Pré-requisitos Windows

- Windows x64 cuja edição, versão, build e arquitetura tenham sido registradas no início da execução.
- OBS Studio x64 portátil para cada versão da matriz; diretório de dados/perfil isolado e sem vínculo com a instalação ou perfil principal.
- Visual Studio 2026 com workload C++ e Windows SDK, mais CMake compatível: toolchain inicial proposta, ainda não validada.
- Visual Studio 2022 poderá ser usado somente após registrar incompatibilidade concreta, reproduzível e relevante com VS 2026; registrar a mesma evidência e os motivos do fallback.
- Git capaz de obter o repositório oficial pelo commit completo; PowerShell e utilitário SHA-256 disponíveis.
- Espaço temporário suficiente. Definir uma raiz de execução exclusiva e registrá-la antes da aquisição.
- Manifesto oficial de dependências compatível com cada versão OBS testada, contendo versão, URL de origem e SHA-256 de cada pacote. O `buildspec.json` do commit candidato declara OBS sources 31.1.1, não as versões 32.1.2/32.2.2 propostas; portanto, sua lista não habilita esses alvos e a execução fica `BLOCKED` antes de baixar ou extrair sources/dependências incompatíveis.

Não instalar ferramentas ou pacotes como parte deste Gate sem autorização da Task de toolchain. Usar apenas ferramentas já disponíveis e registrar suas versões reais.

## Matriz OBS

Executar cada linha de forma independente, com fonte, build e diretório portátil próprios. A matriz é proposta; seus valores não significam suporte aprovado.

| OBS Studio | Papel proposto | Resultado atual |
|---|---|---|
| 32.1.2 x64 | Baseline mínima proposta | BUILD NOT VALIDATED; COMPATIBILITY NOT VERIFIED |
| 32.2.2 x64 | Alvo adicional proposto | BUILD NOT VALIDATED; COMPATIBILITY NOT VERIFIED |

## Template, revisão e dependências

- Repositório: [obsproject/obs-plugintemplate](https://github.com/obsproject/obs-plugintemplate).
- Commit candidato: [`3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc`](https://github.com/obsproject/obs-plugintemplate/commit/3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc).
- Presets oficiais versionados: [CMakePresets.json no commit candidato](https://github.com/obsproject/obs-plugintemplate/blob/3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc/CMakePresets.json). Essa revisão apresenta `windows-x64`/`windows-ci-x64`, gerador Visual Studio 17 2022, Windows SDK 10.0.22621 e CMake mínimo 3.28.0.
- O [buildspec versionado](https://github.com/obsproject/obs-plugintemplate/blob/3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc/buildspec.json) publica: OBS sources 31.1.1, Windows x64 SHA-256 `2c8427c10b55ac6d68008df2e9a3e82f4647aaad18f105e30d4713c2de678ccf`; pre-built obs-deps 2025-07-11, SHA-256 `c8c642c1070dc31ce9a0f1e4cef5bb992f4bff4882255788b5da12129e85caa7`; pre-built Qt6 2025-07-11, SHA-256 `0e76bf0555dd5382838850b748d3dcfab44a1e1058441309ab54e1a65b156d0a`; Qt6 debug symbols 2025-07-11, SHA-256 `11b7be92cf66a273299b8f3515c07a5cfb61614b59a4e67f7fc5ecba5e2bdf21`. São valores publicados no manifesto, ainda não comparados a arquivos baixados. O preset base desativa Qt; não adquirir Qt para a TASK-024. OBS sources 31.1.1 não são adequados para validar OBS 32.1.2/32.2.2.
- Não usar `master` como fonte de build; obter e confirmar o SHA completo candidato.
- Registrar commit, URL, versão, caminho e SHA-256 de cada dependência e artefato OBS usado. Sources e dependências precisam ser oficialmente compatíveis com a versão OBS da matriz; ausência, incompatibilidade ou divergência bloqueia extração e build.

## Verificação SHA-256

Antes de extrair cada arquivo baixado, calcular seu SHA-256 com ferramenta do Windows, por exemplo `Get-FileHash -Algorithm SHA256 -LiteralPath <arquivo>`. Comparar com o hash publicado oficialmente e capturar os dois valores e a origem na evidência da versão. Rejeitar arquivo sem hash publicado, comparação divergente, origem inesperada ou arquivo alterado após verificação. Não registrar credenciais em comandos ou evidências.

## Aquisição e extração futura

1. Criar raiz temporária nova, exclusiva da execução, sem reutilizar diretórios de outras Tasks ou worktrees.
2. Clonar/baixar o template oficial e fazer checkout detached no commit candidato completo; registrar a confirmação de `HEAD`.
3. Consultar o manifesto/release oficial de dependências para aquele commit e montar inventário de URLs, versões e hashes esperados. Se não existir inventário verificável, marcar `BLOCKED`.
4. Baixar somente de origens oficiais para a raiz temporária. Calcular e comparar SHA-256 de todos os arquivos antes de extraí-los. Não usar os OBS sources 31.1.1 do buildspec candidato para as linhas OBS 32.1.2/32.2.2.
5. Extrair para diretórios isolados por OBS e arquitetura. Não escrever em `%ProgramData%`, `%APPDATA%`, diretório da instalação OBS ou perfil real.
6. Registrar versões observadas do Windows, Git, Visual Studio/MSVC, CMake, Windows SDK e OBS portátil antes da configuração.

## Presets planejados

Inspecionar o `CMakePresets.json` do commit fixado. O commit candidato declara `windows-x64` e `windows-ci-x64`; confirmar que estão presentes no checkout adquirido. Configurar e compilar com preset de CI quando compatível; caso contrário, registrar o preset Windows x64 usado e as opções explícitas. Não editar silenciosamente os presets durante o Gate.

Comandos planejados, a ajustar somente conforme os presets e scripts reais da revisão fixada:

```powershell
cmake --list-presets
cmake --preset windows-ci-x64
cmake --build --preset windows-ci-x64 --config RelWithDebInfo
ctest --preset windows-ci-x64 --output-on-failure
```

Se a revisão não declarar preset de teste, executar apenas testes existentes e registrar `NOT APPLICABLE` para CTest. Não declarar testes inexistentes como aprovados. Repetir configure/build/test em diretórios limpos e independentes para cada versão OBS.

## Smoke test com OBS portátil isolado

Para cada linha da matriz:

1. Criar cópia portátil OBS x64 dentro da raiz temporária própria e confirmar versão/binários.
2. Configurar o modo portátil e um perfil/configuração isolado. Antes de iniciar, registrar caminhos e verificar que não apontam para instalação ou perfil principal.
3. Instalar o artefato apenas no layout privado da cópia portátil, respeitando o layout correspondente àquela versão.
4. Iniciar OBS portátil e verificar load, evento `FINISHED_LOADING`, atualização em memória em `STREAMING_STARTED`/`STREAMING_STOPPED`, unload limpo e ausência de callbacks após unload.
5. Exercitar repetição de load/unload e falhas de inicialização/registro sem travar ou encerrar o OBS.
6. Encerrar somente o processo OBS portátil iniciado pelo teste. Confirmar que instalação principal e perfil real não foram abertos nem alterados.

## Critérios de segurança

- Nenhum arquivo é extraído ou instalado fora da raiz temporária da execução.
- Nenhuma etapa modifica instalação principal do OBS, `%APPDATA%\obs-studio`, perfil real, quarentena de Tasks ou worktrees preexistentes.
- O plugin não acessa rede, providers, banco, IA, TTS ou secrets; callbacks não fazem I/O síncrono e permanecem curtos.
- Logs e evidências excluem tokens, credenciais, conteúdo privado e payloads não necessários.
- Hashes são verificados antes da extração; versões desconhecidas ou artefatos sem origem/hash confiável falham fechados.
- O smoke test encerra apenas o OBS portátil iniciado por aquela execução.

## Evidências exigidas por versão

Manter um pacote de evidências separado para OBS 32.1.2 e 32.2.2: identidade do Windows; versões de ferramentas; commit do template; inventário de dependências, URLs e hashes esperados/observados; preset e comandos exatos; logs de configure/build/test; caminhos do OBS portátil e perfil isolado; logs mínimos do smoke test; resultado de lifecycle e falhas; confirmação de que instalação/perfil reais permaneceram intocados; conclusão e responsável/revisor. Não incluir dumps ou logs contendo segredos.

## Veredictos

- **PASSED** — Aquisição fixada e hashes oficiais conferidos; configure, build x64, testes aplicáveis e smoke test passaram para aquela versão; critérios de segurança satisfeitos; evidências completas revisadas. O resultado aplica-se somente à combinação exata de OBS, Windows e toolchain registrada.
- **BLOCKED** — Fonte/hash/dependência ausente ou divergente; ambiente não identificado; ferramenta/preset incompatível sem fallback demonstrado; build/teste/smoke não executado ou falhou; isolamento não comprovado; risco à instalação/perfil real; ou evidência incompleta.

Um sucesso em uma versão não aprova a outra. Suporte declarado exige aprovação individual de cada combinação da matriz; versão não testada continua sem compatibilidade verificada.

## Limpeza dos artefatos temporários

Ao final da execução futura, inventariar e resolver caminhos absolutos da raiz exclusiva criada por aquela execução. Confirmar que ela está dentro do diretório temporário explicitamente designado para o Gate e que não contém nem aponta para arquivos preexistentes. Encerrar apenas processos OBS portáteis iniciados pela execução. Remover somente essa raiz temporária própria depois de arquivar evidências aprovadas em local autorizado. Se a raiz, propriedade ou conteúdo forem ambíguos, não remover; registrar e pedir direção. Nunca executar limpeza de worktrees, branches, diretórios de outras Tasks, instalação/perfil OBS ou `%APPDATA%\obs-studio`.
