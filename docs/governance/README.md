# Governança

Este diretório é a fonte detalhada e legível por pessoas para a governança de execução. As regras operacionais concisas e obrigatórias permanecem em [`.ai/governance/README.md`](../../.ai/governance/README.md), e o contrato geral do repositório permanece em [`AGENTS.md`](../../AGENTS.md).

- [Plano de execução](EXECUTION_PLAN.md)
- [GitFlow](GITFLOW.md)
- [Quality Gates](QUALITY_GATES.md)
- [Knowledge Quality Gate](KNOWLEDGE_QUALITY_GATE.md)

## Idioma oficial da documentação

A documentação humana do projeto utiliza prioritariamente português do Brasil (`pt-BR`). A regra inclui:

- `README.md` da raiz e `docs/README.md`;
- documentação de Project, Requirements, Architecture, Research, Security, Testing, Reports, Governance e Knowledge;
- títulos, descrições e critérios de aceite das Tasks;
- descrições de pull requests e relatórios de execução;
- demais artefatos destinados à leitura humana.

Código-fonte, nomes de classes, interfaces, métodos, propriedades, namespaces e projetos, comandos, branches, Conventional Commits, identificadores, APIs, protocolos, bibliotecas, nomes oficiais de tecnologias, termos técnicos consagrados e palavras-chave exigidas por ferramentas podem permanecer em inglês quando a tradução prejudicar a clareza ou a compatibilidade.

Não se deve traduzir mecanicamente `AGENTS.md`, `CLAUDE.md` ou `SKILL.md` quando sua linguagem, estrutura ou palavras-chave atenderem a Claude, Codex, Copilot ou outras ferramentas.

## Regra de descrição das Tasks

Toda Task mantém um identificador técnico, como `TASK-001`, mas possui título, descrição humana e critérios de aceite em `pt-BR`. Termos técnicos e identificadores podem permanecer em inglês.

Exemplo:

```text
TASK-001

Título:
Pesquisar estratégia de integração com OBS Studio

Descrição:
Avaliar as opções de integração nativa, OBS WebSocket e IPC para determinar
quais capacidades deverão ser utilizadas pelo OBS-AI-Live-Assistant.

Critérios de aceite:
- opções documentadas;
- riscos identificados;
- recomendação registrada;
- necessidade de ADR identificada.
```

Estes documentos descrevem processo. Eles não aprovam Requirements, Architecture, implementação ou promoção para `hml`, `release/*` ou `main`.
