# Matriz de compatibilidade

Status: **ESTABELECIDA (v1)** — validada na **TASK-003** em 2026-10-07. Esta matriz é a baseline executável da [ADR-010](../decisions/ADR-010-compatibility-strategy.md) e o insumo do release gate (RNF-024). Ela não é promessa comercial: combinações sem teste são recusadas (fail-closed) e a política de Windows 10 é decisão do cliente antes do release (RES-022, RES-027).

## 1. Baseline declarada

| Dimensão | Baseline V1 | Versão mínima | Referência atual | Origem |
|---|---|---|---|---|
| Sistema operacional | Windows 11 **x64** | build >= 22000 | Windows 11 25H2 (build 26200) | CON-001; ADR-010; RES-027 |
| Sistema operacional | Windows 10 **x64** — somente edições oficialmente suportadas pelo runtime; política comercial pendente | build >= 19041 | não prometido genericamente | CON-001; ADR-010; RES-027 |
| Runtime .NET | .NET 10 **x64** | 10.0 (LTS até 2028-11) | 10.0.12 | RES-027; `global.json` |
| OBS Studio | 32.x **x64** | 32.1.x (mínimo testado: 32.1.2) | 32.2.2 (referência de pesquisa 2026-10-06) | CON-002; ADR-010; RES-022 |
| obs-websocket (protocolo 5.x) | requerido para o Adapter OBS WebSocket | presença do plugin verificada | presente no ambiente testado | ADR-001; RES-007 |

Regras:

- A faixa suportada na V1 compreende a baseline acima até as versões **testadas e declaradas por release**. OBS 33+ e demais majors não são prometidos; cada major exige reconstrução/teste do plugin e validação de layout version-aware (RES-001, RES-003, RES-022).
- Windows 10 (edições comuns) não é prometido genericamente: o suporte oficial atual do .NET 10 no Windows 10 limita-se a edições LTSC/Enterprise (RES-027). A política comercial será fechada antes do release (RNF-024, RNF-020).
- `OBS 32.1.2 instalado` é o ambiente de desenvolvimento declarado, não a política completa (RNF-024 Observações).

## 2. Combinações testadas (evidência)

| Data | Sistema | Arquitetura | Build | .NET runtime | OBS | obs-websocket | Classificação | Evidência |
|---|---|---|---|---|---|---|---|---|
| 2026-10-07 | Windows 11 25H2 (Pro) | x64 | 10.0.26200 | 10.0.12 | 32.1.2 | presente | `supported` | Protótipo `prototypes/compat-sniff` (veredito `supported`, exit 0) + observação de registro: API de compatibilidade reporta `ProductName "Windows 10 Pro"` para build 26200 — ambiguidade que reforça o fail-closed por build/edição |

## 3. Fail-closed (combinações desconhecidas)

| Dimensão | Condição | Classificação |
|---|---|---|
| SO | não Windows | `unsupported` |
| SO | Windows 32 bits | `unsupported` |
| SO | build >= 22000 (Windows 11) | `supported` |
| SO | build 19041..21999 (Windows 10) | `unknown` — exige política de edições antes do release |
| SO | build < 19041 | `unsupported` |
| .NET | major 10 (LTS) | `supported` |
| .NET | major < 10 | `unsupported` |
| .NET | major > 10 | `unknown` |
| OBS | 32.x com minor >= 1 | `supported` |
| OBS | 32.0.x | `unsupported` |
| OBS | major < 32 | `unsupported` |
| OBS | major > 32 | `unknown` |
| OBS | não instalado / caminho não detectado | `unknown` |

**Veredito global:** qualquer dimensão `unsupported` → `unsupported`; caso contrário, qualquer dimensão `unknown` → `unknown`; somente tudo `supported` → `supported`. O veredito é emitido pelo protótipo e será reaplicado pelo Compatibility Gate do produto.

`unknown` **não** significa suportado: o produto recusa (não executa ou instala ação sensível) ou entra em aviso explícito antes de qualquer operação irreversível; ambiente incompatível é distinguido de falha transitória (RNF-024).

## 4. Detecção em runtime (futuro, fora do escopo desta Task)

- Detectar no startup: SO (arquitetura/build), runtime .NET e OBS (versão e caminho padrão); consultar RPC `GetVersion` (protocolo 5.x) e capabilities quando o Adapter OBS WebSocket existir (RES-007); validar layout de instalação version-aware (RES-003) na instalação (TASK-045 / ADR-007).
- O protótipo prova a lógica de classificação determinística; a integração ao produto acontece nas Tasks de implementação (TASK-005+), sem ampliar o escopo aprovado.
- A matriz final será revista na **TASK-051** (Integração e hardening — matriz final).

## 5. Rastreabilidade

| Item | Referências |
|---|---|
| Requirements | RF-032; RNF-019, RNF-024, RNF-025 |
| Security | SEC-027, SEC-029, SEC-032 |
| ADRs | ADR-010 (ACCEPTED), ADR-001 (ACCEPTED), ADR-007 (PROPOSED) |
| Research | RES-001, RES-002, RES-003, RES-022, RES-027 |
| Constraints | CON-001, CON-002, CON-009, CON-015 |

## 6. Próximos passos (gate de release)

- [ ] Fechar a política comercial de Windows 10 (edições) antes do release (RNF-024, RNF-020, RES-027).
- [ ] Expandir combinações testadas em laboratório conforme capacidade, para majors/minors selecionadas; nunca declarar suporte sem teste (RES-022).
- [ ] Revalidar a matriz em cada release com smoke/regression (OBS Compatibility Gate; TASK-051).