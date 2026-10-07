# Documentação do projeto

Este diretório é o ponto de entrada central da documentação do OBS-AI-Live-Assistant. Os documentos distinguem direção atual, hipóteses, requisitos aprovados e decisões arquiteturais.

## Áreas da documentação

- [Arquitetura](architecture/README.md) — baseline concluída, ADRs, diagramas, dependency rules, [compatibilidade](architecture/compatibility/README.md) e rastreabilidade.
- Projeto — [visão geral](project/PROJECT_OVERVIEW.md), [escopo](project/PROJECT_SCOPE.md) e [navegação do registro de Skills](project/PROJECT_SKILLS.md).
- [Governança](governance/README.md) — ordem de execução, idioma oficial, GitFlow e Quality Gates.
- Conhecimento — [mapa](knowledge/PROJECT_KNOWLEDGE_MAP.md), [decisões de tratamento das fontes](knowledge/KNOWLEDGE_DECISIONS.md) e [conflitos](knowledge/KNOWLEDGE_CONFLICTS.md).
- [Pesquisa](research/README.md) — inventário, fontes oficiais, matrizes, recomendações e Research Quality Gate concluído.
- [Requirements](requirements/README.md) — requisitos V1 concluídos, rastreados e aprovados pelo Requirements Quality Gate.
- [Security](security/README.md) — requisitos, Threat Model, Trust Boundaries, riscos e gate de segurança concluídos.
- [Testing](testing/README.md) — arquitetura de testes, categorias, comandos e estado dos quality gates.
- [Reports](reports/README.md) — relatórios factuais sobre o estado do projeto.
- [Histórico de prompts](prompts/README.md) — arquivo cronológico de prompts operacionais.
- [Tasks](../tasks/README.md) — backlog executável, Dependency Graph, Waves, rastreabilidade, DoR, DoD e Release Plan da V1.

## Regras das fontes canônicas

- `AGENTS.md` é o contrato conciso para as ferramentas de IA.
- `agent_docs/` contém restrições especializadas vigentes.
- `.ai/governance/README.md` contém o baseline operacional obrigatório para os agentes.
- `docs/governance/` contém o processo detalhado e legível por pessoas.
- Requirements aprovados, ADRs e documentação canônica mais recente prevalecem sobre prompts históricos e hipóteses de planejamento.

Prompts históricos e projetos de referência são evidências e contexto; eles não aprovam Requirements ou Architecture por conta própria.
