# Governança de engenharia

## Ordem das fases

```text
BOOTSTRAP
-> PROJECT DISCOVERY
-> KNOWLEDGE DISCOVERY
-> KNOWLEDGE QUALITY GATE
-> REQUIREMENTS
-> SECURITY REQUIREMENTS
-> ARCHITECTURE
-> DATABASE DESIGN
-> OBS INTEGRATION DESIGN
-> AI PROVIDER DESIGN
-> TTS PROVIDER DESIGN
-> CHAT PROVIDER DESIGN
-> INSTALLATION DESIGN
-> EXECUTION PLAN
-> DEPENDENCY GRAPH
-> BACKLOG
-> TASKS
-> CLIENT APPROVAL GATE
-> IMPLEMENTATION
```

O bootstrap atual não autoriza Requirements, Architecture nem implementação.

## Estados de decisão

- `CONFIRMED`
- `ASSUMPTION`
- `REQUIRES_RESEARCH`
- `REQUIRES_ADR`
- `REQUIRES_CLIENT_DECISION`
- `OUT_OF_SCOPE_V1`
- `BLOCKED`

Não promova uma hipótese ou item de Research a decisão confirmada sem evidência e a aprovação exigida.

## Fluxo de papéis

```text
Requirements -> Architect -> Tech Lead -> Developer -> Tester -> Reviewer -> Documentation
                         \-> Security
                         \-> DevOps
```

Os papéis delimitam responsabilidades. Eles não implicam execução autônoma, permissões ampliadas nem runtime multi-agent no produto.

## Gates globais

- Requirements estão completos, verificáveis e delimitados.
- Architecture, Security, banco de dados, integração com OBS, limites de providers e estratégia de instalação foram revisados.
- Dependências e Tasks estão mapeadas.
- Build, testes, análise estática, verificações de segurança e gates de documentação possuem evidências determinísticas quando a implementação começar.
- Nenhum conflito Critical não resolvido está oculto.
- Nenhuma implementação começa sem aprovação explícita do cliente.

## Limites protegidos

- Não modifique `%APPDATA%\obs-studio` durante planejamento ou bootstrap.
- Não copie nem altere artefatos do OBS Truck Live Optimizer.
- Não armazene nem registre secrets em logs.
- Não simule Git, testes, builds, integrações ou aprovações.

## Regra de rastreabilidade de prompts

Toda Task que produzir alteração no repositório deve seguir esta sequência:

```text
Prompt
-> Execução
-> Validação
-> Arquivamento do prompt
-> Verificação de secrets
-> git add
-> commit
-> push
```

Arquive o prompt operacional em `docs/prompts/history/` antes do commit da
Task. Use o próximo nome sequencial `prompt<number>.md` disponível, nunca
sobrescreva um prompt histórico existente e inclua o prompt arquivado no mesmo
commit e push das alterações que ele autorizou.

Não arquive valores de secrets. Se um prompt contiver credencial, password,
token, private key, connection string com secret ou API key, pare e providencie
tratamento seguro antes de colocar o prompt no Git.

Prompts históricos fornecem rastreabilidade; eles não são Requirements nem
decisões arquiteturais automaticamente aprovados. Requirements canônicos,
documentação de Architecture e ADRs prevalecem.

## Regra de commit e push da Task

Toda alteração autorizada no repositório deve ser comitada e enviada na branch
da própria Task. Não finalize uma Task modificadora com alterações intencionais
sem commit ou sem push.

Uma Task pode terminar com push bloqueado somente quando falha de autenticação,
indisponibilidade remota, conflito ou outro erro técnico o impedir. Informe o
estado do commit, o push bloqueado e o motivo exato. Nunca envie para outra
branch para contornar a falha.

## Regra de conclusão da feature

Toda branch `feature/task-*` concluída com sucesso segue este fluxo:

```text
Implementação / Documentação
-> Validação
-> Code Review
-> Correções obrigatórias
-> Arquivamento do prompt
-> Verificação de segurança
-> Commit
-> Push
-> Pull Request
-> Validação do PR
-> Merge para develop
-> Validação pós-merge
-> Limpeza da feature
```

Quando todos os Quality Gates obrigatórios forem aprovados, nenhuma autorização
adicional do cliente será exigida entre a conclusão da Task, o Code Review, a
criação do pull request e o merge em `develop`. Finding Critical ou High não
resolvido, falha em validação obrigatória, secret detectado, conflito Git, pull
request inválido, alteração fora do escopo, push incompleto ou diferença
local/remota inesperada bloqueiam o merge.

Após um merge bem-sucedido, valide que `develop` local e remota contêm o merge e
que a working tree está limpa. Exclua a feature local e remota somente depois de
confirmar que o pull request foi mergeado e que a limpeza é segura.

Esta regra autoriza somente merges de `feature/task-*` em `develop`. Ela não
autoriza promoção de `develop` para `hml`, criação de `release/1.0.0XXXX` a
partir de `hml` aprovada ou merge de uma branch `release/*` em `main`; essas
transições permanecem sujeitas a gates próprios do projeto.

## Regra de idioma da documentação

A documentação do projeto destinada à leitura humana e as descrições de pull
requests utilizam português do Brasil (`pt-BR`). Código-fonte, identificadores
técnicos, comandos, nomes de branches, Conventional Commits, nomes oficiais de
tecnologias, termos técnicos consagrados e palavras-chave estruturais exigidas
por ferramentas podem permanecer em inglês. Não traduza mecanicamente
`AGENTS.md`, `CLAUDE.md` ou `SKILL.md` quando isso enfraquecer as instruções das
ferramentas.

Toda Task mantém seu identificador técnico, mas possui título, descrição e
critérios de aceite destinados à leitura humana em `pt-BR`.

## Regra do fluxo de entrega

O fluxo de entrega é `feature/task-* -> develop -> hml ->
release/1.0.0XXXX -> main`. A autorização de merge automático aplica-se somente
a um pull request bem-sucedido de `feature/task-*` para `develop`. Promoção para
`hml`, criação de release, merge em `main`, criação de tag e publicação de
GitHub Release exigem gates próprios aprovados. Correções exclusivas da release
devem retornar a `develop` e, quando aplicável, a `hml` após a publicação em
produção.

A governança detalhada e destinada à leitura humana está indexada em
[`docs/governance/README.md`](../../docs/governance/README.md). Este arquivo
permanece o baseline conciso e obrigatório do fluxo; os documentos detalhados
não podem enfraquecer estas regras.
