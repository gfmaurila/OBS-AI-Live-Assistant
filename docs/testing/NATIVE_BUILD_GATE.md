# Native Build Gate: plugin OBS

## Objetivo e limites

Este procedimento define evidências futuras para adquirir, configurar, compilar e testar o skeleton C++ do plugin em Windows x64. A publicação deste documento não executa o Gate nem aprova toolchain, build ou compatibilidade. Nesta etapa documental não se adquirem dependências, não se cria binário e não se altera a instalação do OBS.

O Gate limita-se ao lifecycle e aos eventos em memória definidos na TASK-024. Dock, Qt, áudio, IPC, Assistant Core, providers, banco, rede e secrets não pertencem a este Gate.

## Pré-requisitos Windows

- Windows x64 cuja edição, versão, build e arquitetura tenham sido registradas no início da execução.
- OBS Studio x64 portátil para cada versão da matriz; diretório de dados/perfil isolado e sem vínculo com a instalação ou perfil principal.
- Visual Studio Community 2026 com MSVC x64, Windows SDK 10.0.26100.0 e CMake 4.3.1-msvc1: toolchain exercitada experimentalmente na TASK-025B para OBS 32.2.2. Isso não aprova compatibilidade nem o Gate completo.
- A toolchain upstream de referência do OBS 32.1.2 é Visual Studio 2022 / Windows SDK 10.0.22621.0. Um build experimental da baseline 32.1.2 foi realizado com Visual Studio Community 2026, MSVC 19.51.36260.0 e SDK 10.0.22621.0; isso comprova o build específico registrado, não runtime nem compatibilidade geral. VS 2022 continua fallback condicionado a incompatibilidade demonstrada, não requisito para repetir o resultado já obtido.
- Visual Studio 2022 poderá ser usado somente após registrar incompatibilidade concreta, reproduzível e relevante com VS 2026; registrar a mesma evidência e os motivos do fallback.
- Git capaz de obter o repositório oficial pelo commit completo; PowerShell e utilitário SHA-256 disponíveis.
- Espaço temporário suficiente. Definir uma raiz de execução exclusiva e registrá-la antes da aquisição.
- Manifesto oficial de dependências compatível com cada versão OBS testada, contendo versão, URL de origem e SHA-256 de cada pacote. O `buildspec.json` do commit candidato declara OBS sources 31.1.1, não as versões 32.1.2/32.2.2 propostas; portanto, sua lista não habilita esses alvos e a execução fica `BLOCKED` antes de baixar ou extrair sources/dependências incompatíveis.

Não instalar ferramentas ou pacotes como parte deste Gate sem autorização da Task de toolchain. Usar apenas ferramentas já disponíveis e registrar suas versões reais.

## Matriz OBS

Executar cada linha de forma independente, com fonte, build e diretório portátil próprios. A matriz é proposta; seus valores não significam suporte aprovado.

| OBS Studio | Papel proposto | Resultado atual |
|---|---|---|
| 32.1.2 x64 | Baseline mínima proposta | EXPERIMENTAL BUILD VALIDATED: configure auxiliar Win32 e x64, build de `libobs`/`obs-frontend-api`, prefixo de desenvolvimento, configure/build do plugin e CTest (7 cenários) passaram. DLL x64 SHA-256 `5FE2EE8C5E86637F1784A181DBC190EC7BA17EE364D014026CBB08EDDC905EF2`. Runtime NOT TESTED; compatibilidade NOT VERIFIED; Native Build Gate PENDING. |
| 32.2.2 x64 | Alvo experimental adicional | EXPERIMENTAL: configure/build x64 e CTest (7 cenários) passaram. Smoke anterior confirmou `Portable mode: true`, `Startup complete` e `event=module.loaded` com a DLL anterior SHA-256 `B95F96CA24742BBFE32F99A5EBD22B27EEAD62B416E4E4B994D52D9F92B1D5E6`; o caminho de registro do callback foi executado, mas entrega de `FINISHED_LOADING` não foi comprovada. A DLL corrigida tem SHA-256 `B1D44067B3D9E5B06EE296B7EA33DA63E1FDED93DE8DCBB4475FC4C2A789617D`; EXIT, remoção de callback, unload e shutdown normal dessa versão corrigida permanecem NOT VERIFIED. Eventos de streaming NOT TESTED; Native Build Gate PENDING. |

### Evidências anteriores — não equivalem à aprovação do Gate

- TASK-025A: arquivos oficiais, hashes e extração foram registrados para as duas versões candidatas. Essas evidências comprovam a identidade dos artefatos, não a compatibilidade do plugin.
- TASK-025B: configure CMake e build C++ experimental passaram para OBS 32.2.2 x64 com Visual Studio Community 2026, MSVC 19.51.36260.0, CMake 4.3.1-msvc1 e Windows SDK 10.0.26100.0. O resultado é EXPERIMENTAL e não cobre os testes de lifecycle exigidos pela Task.
- TASK-025C/025N: load e inicialização do módulo experimental foram observados/inferidos pelos registros disponíveis; unload normal e lifecycle completo não foram validados. Callbacks frontend não foram exercitados. Classificação: EXPERIMENTAL / NOT VALIDATED.
- TASK-024 runtime smoke inicial (OBS 32.2.2): commit `83a70b0e8b6ad73cedfb7486d6c7c1f5a7ea9f09`; DLL x64 SHA-256 `B95F96CA24742BBFE32F99A5EBD22B27EEAD62B416E4E4B994D52D9F92B1D5E6`. O runtime oficial portátil foi copiado para uma raiz temporária nova, sem importar configuração do usuário, e iniciado com `--portable --multi`. O log confirmou `Portable mode: true`, `Startup complete`, carregamento do módulo e `event=module.loaded`. O caminho de registro do callback foi executado por `obs_module_load`; entrega de `FINISHED_LOADING` não ficou comprovada. Não houve transmissão; eventos de streaming não foram testados. Esse smoke usou a DLL anterior à correção de EXIT e, portanto, não valida a remoção corrigida nem o shutdown da DLL corrigida.
- TASK-024 OBS 32.1.2 experimental: sources oficiais e dependências x64/x86/Qt6 verificadas foram configuradas isoladamente com Visual Studio Community 2026, MSVC 19.51.36260.0, CMake 4.3.1 e Windows SDK 10.0.22621.0. Configure auxiliar Win32 e configure x64 passaram; build oficial de `libobs` e `obs-frontend-api` passou; prefixo de desenvolvimento foi validado por resolução dos pacotes CMake e bibliotecas x64. O plugin foi configurado e compilado contra esse prefixo, sem reutilizar dependências 32.2.2. CTest passou com `PASS 7 lifecycle scenarios`. DLL x64 SHA-256: `5FE2EE8C5E86637F1784A181DBC190EC7BA17EE364D014026CBB08EDDC905EF2`. Runtime 32.1.2 NOT TESTED; compatibilidade NOT VERIFIED.
- TASK-024 build experimental OBS 32.2.2: configure e build x64 passaram com Visual Studio Community 2026, MSVC 19.51.36260.0, CMake 4.3.1-msvc1 e Windows SDK 10.0.26100.0; CTest passou com sete cenários. A DLL após a correção de lifecycle tem SHA-256 `B1D44067B3D9E5B06EE296B7EA33DA63E1FDED93DE8DCBB4475FC4C2A789617D`. O runtime smoke disponível que confirmou module load usou a DLL anterior (`B95F...`), não esta DLL corrigida. Para a DLL corrigida, recebimento de EXIT, remoção do callback via runtime, `obs_module_unload` e encerramento normal estão NOT VERIFIED. Eventos de streaming NOT TESTED.
- TASK-025W: a validação visual da instalação portátil permanente confirmou perfis/coleções e mídias com ressalvas, mas a DLL experimental não estava instalada nessa cópia. Essa evidência não valida o plugin.
- OBS 32.1.2 x64: build experimental e sete cenários determinísticos estão validados conforme evidência acima; runtime e compatibilidade permanecem NOT TESTED / NOT VERIFIED.

O veredicto do Native Build Gate permanece PENDING. Embora configure/build e testes determinísticos estejam registrados para as duas baselines, ainda faltam smoke tests runtime isolados da DLL correspondente em cada versão, observação verificável do lifecycle corrigido (incluindo EXIT e unload) e revisão/aprovação formal das evidências. Um resultado de build ou o smoke de uma DLL anterior não aprova compatibilidade da versão corrigida.

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

Inspecionar o `CMakePresets.json` do commit fixado. O commit candidato declara `windows-x64` e `windows-ci-x64` com gerador Visual Studio 17 2022 e SDK 10.0.22621.0. Esses presets não representam a toolchain experimental VS 2026/SDK 26100. Para 32.2.2, a TASK-025B confirmou experimentalmente o gerador `Visual Studio 18 2026`; registrar o comando e as opções explícitas em vez de reutilizar o preset incompatível. Não alterar silenciosamente os presets upstream.

Comandos planejados, a ajustar somente conforme os presets e scripts reais da revisão fixada:

```powershell
cmake -S native/ObsAi.ObsPlugin -B <BUILD_DIR> -G "Visual Studio 18 2026" -A "x64,version=10.0.26100" "-DCMAKE_PREFIX_PATH=<OBS_DEV_PREFIX>;<OBS_DEPS_PREFIX>" "-DOBS_TARGET_VERSION=32.2.2" -DOBS_AI_BUILD_PLUGIN=ON -DOBS_AI_BUILD_TESTS=ON
cmake --build <BUILD_DIR> --config RelWithDebInfo --target ALL_BUILD
ctest --test-dir <BUILD_DIR> -C RelWithDebInfo --output-on-failure
```

Os comandos acima registram a combinação experimental já exercitada para 32.2.2; substituir os caminhos pelos prefixos locais previamente validados e manter cada diretório de build novo e exclusivo. Para 32.1.2, usar a fonte/dependências e toolchain correspondentes somente quando o pacote de desenvolvimento compatível estiver disponível; não reutilizar o prefixo 32.2.2. Se a revisão não declarar teste aplicável, registrar `NOT APPLICABLE` para CTest em vez de declarar testes inexistentes como aprovados.

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
