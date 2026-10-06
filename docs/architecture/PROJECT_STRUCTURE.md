# Estrutura do projeto

## Estado

Este documento representa somente a estrutura documental atual e uma disposição conceitual do produto. Ele não é uma especificação final de Architecture.

```text
OBS-AI-Live-Assistant/
|-- .ai/                 # papéis de IA, referências de conhecimento e governança obrigatória
|-- .claude/             # regras do Claude, documentação de hooks e Skills do projeto
|-- .codex/              # Skills do projeto para Codex
|-- .github/             # orientações de ferramentas e futura configuração de CI
|-- agent_docs/          # restrições especializadas de engenharia
|-- docs/                # documentação canônica do projeto
|-- tasks/               # ciclo de vida das Tasks e futuro grafo de dependências
|-- AGENTS.md             # contrato para múltiplas ferramentas
|-- PROJECT_SKILLS.md     # registro das Skills instaladas
`-- estrutura futura do produto
    |-- assistant-core/   # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
    |-- obs-integration/  # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
    |-- tests/            # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
    |-- installer/        # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
    `-- tooling/          # PLANNED / NOT CREATED / SUBJECT TO ARCHITECTURE APPROVAL
```

## Limite físico atual

Existem somente artefatos de governança, agentes, Skills, histórico de prompts, estados de Tasks e documentação. Não existem `src/`, solution, projeto nativo, banco de dados, instalador ou Dock do OBS.

## Restrições de planejamento

- `assistant-core/` é um nome conceitual para responsabilidades da aplicação externa; seu nome final e estrutura física exigem aprovação de Architecture.
- `obs-integration/` não significa que um plugin nativo foi escolhido. A divisão de responsabilidades entre plugin nativo, OBS WebSocket e IPC está marcada como `REQUIRES_RESEARCH`, `REQUIRES_ADR` e, quando aplicável, `REQUIRES_CLIENT_DECISION`.
- Diretórios de produto não devem ser criados antes de o fluxo obrigatório alcançar a Task aprovada correspondente.
- Nenhuma estrutura do projeto de referência CMS/Azure constitui requisito deste projeto.
