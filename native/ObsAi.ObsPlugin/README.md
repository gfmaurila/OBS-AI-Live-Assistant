# Plugin nativo do OBS — ObsAi.ObsPlugin

Este módulo mínimo mantém no processo do OBS somente o lifecycle e um estado de saúde transitório em memória. Ele registra `FINISHED_LOADING`, `STREAMING_STARTED` e `STREAMING_STOPPED`; os callbacks apenas atualizam flags atômicas e não fazem I/O, chamadas de rede ou processamento de negócio.

## Base e limites

- Estrutura de build adaptada do template oficial [`obsproject/obs-plugintemplate`](https://github.com/obsproject/obs-plugintemplate), commit `3e7d7ac3b5342cd7d9b88890b9c70b472d1520fc`.
- Dependências CMake: pacotes de desenvolvimento oficiais da mesma versão OBS que será compilada (`libobs` e `obs-frontend-api`). Não habilitar Qt.
- A cópia do template e as dependências validadas das TASK-025 são referências somente de leitura; esta árvore contém apenas o código do produto.
- Nenhum chat, IA, TTS, Dock, IPC, áudio, banco, credencial, provider ou serviço externo.

## Configure, build e testes

Executar em uma área de build exclusiva, fora da árvore de fontes. `OBS_DEV_PREFIX` deve apontar para o prefixo de desenvolvimento gerado a partir das sources da versão-alvo; `OBS_DEPS_PREFIX` aponta para o pacote oficial obs-deps correspondente. Os diretórios e logs devem ser novos para cada versão.

```powershell
cmake -S native/ObsAi.ObsPlugin -B <BUILD_DIR> -G "Visual Studio 18 2026" -A "x64,version=10.0.26100" -DCMAKE_PREFIX_PATH="<OBS_DEV_PREFIX>;<OBS_DEPS_PREFIX>" "-DOBS_TARGET_VERSION=32.2.2" -DOBS_AI_BUILD_PLUGIN=ON -DOBS_AI_BUILD_TESTS=ON
cmake --build <BUILD_DIR> --config RelWithDebInfo --parallel
ctest --test-dir <BUILD_DIR> -C RelWithDebInfo --output-on-failure
```

Para OBS 32.1.2, usar uma árvore de build independente e a toolchain/SDK efetivamente aprovados para essa baseline. Não reutilizar o gerador, SDK ou prefixos de desenvolvimento de 32.2.2 para declarar compatibilidade com 32.1.2.

O build não instala o módulo. O smoke test com OBS portátil é uma etapa separada e exige autorização específica para iniciar a GUI.
