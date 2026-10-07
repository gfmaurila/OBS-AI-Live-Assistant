# Protótipo `compat-sniff` — sonda de compatibilidade

Protótipo read-only da **TASK-003** para coletar evidências da matriz de compatibilidade (OBS 32.x x64, Windows 10/11 x64 e .NET 10) sem modificar o OBS nem o sistema.

## Propósito

- Detectar, de forma determinística e sem privilégio administrativo, fatos do ambiente: sistema operacional, arquitetura, runtime .NET e instalação OBS (caminho, versões e plugin `obs-websocket`).
- Classificar cada dimensão conforme a política declarada na [matriz de compatibilidade](../../docs/architecture/compatibility/COMPATIBILITY_MATRIX.md) e emitir um veredito fail-closed.
- Servir de evidência executável para a ADR-010 (estratégia de compatibilidade) e para o OBS Compatibility Gate da Task.

## Comportamento

- Somente leitura: verifica arquivos/caminhos padrão de instalação e APIs BCL (`Environment`, `RuntimeInformation`, `FileVersionInfo`). Não abre registro, rede, secret ou processo externo; não altera nada.
- Saída JSON (stdout) com os fatos coletados, as classificações por dimensão e o veredito.
- Código de saída: `0` se veredito `supported`; `1` caso contrário (fail-closed para combinações desconhecidas/não suportadas).

## Execução

```text
dotnet run --project prototypes\compat-sniff\CompatibilityProbe.csproj
```

O arquivo de projeto herda as convenções comuns do repositório (`Directory.Build.props` herdado: `net10.0`, `TreatWarningsAsErrors`, `Nullable`, formatação IDE). O protótipo **não** faz parte da solution `OBS-AI-Live-Assistant.slnx` nem da matriz de dependências dos projetos `src/`; é uma ferramenta de evidência isolada.

## Evidência registrada

A execução em 2026-10-07 no ambiente de desenvolvimento declarado está registrada na matriz de compatibilidade e no relatório `docs/reports/TASK-003_REPORT.md`.