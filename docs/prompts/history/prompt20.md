# Prompt 20 — Retomada segura da TASK-004 após rate limit (2026-10-07)

Autorização do cliente para retomar a **TASK-004 — Estabelecer a arquitetura de testes e os quality gates** do último checkpoint válido, sem reiniciar a execução nem descartar alterações.

## Prompt operacional

```text
CONTINUAR TASK-004 — RETOMADA SEGURA APÓS RATE LIMIT

Projeto: OBS-AI-Live-Assistant

Repositório:
D:\Empresa\GFMaurila\projetos\OBS-AI-Live-Assistant

A execução anterior foi interrompida por:

Error from provider (Console): Rate limit exceeded.

NÃO reinicie a TASK-004 do zero.

1. Recuperar o estado real

Execute:

git branch --show-current
git status
git status --short
git diff -- tasks/DEPENDENCY_GRAPH.md
git diff -- tasks/DEFINITION_OF_READY.md
git log -5 --oneline

Identifique:
- Branch atual.
- Arquivos modificados.
- Etapas já concluídas.
- Etapa exata da interrupção.
- Existência de commit ou PR anterior.

Preserve todas as alterações válidas.

2. Verificar o Dependency Graph

A última operação registrada envolveu tasks/DEPENDENCY_GRAPH.md.

Confira se as alterações são necessárias e consistentes com o estado real das Tasks.

Não marcar TASK-004 como DONE antes de cumprir seus critérios de aceite e Quality Gates.

Não sobrescrever alterações válidas.

3. Continuar a execução

Retome a leitura de tasks/DEFINITION_OF_READY.md e do arquivo canônico da TASK-004.

Siga o prompt operacional já autorizado, preservando todos os requisitos:

- Implementação exclusiva da TASK-004.
- Build e testes.
- Architecture Gate.
- Security Gate.
- Code Review.
- Secret Scan.
- Prompt History.
- Definition of Done.
- Commit e push.
- PR para develop.
- Merge automático somente com gates aprovados.
- Validação pós-merge.
- Limpeza segura da feature.
- Relatório final.

4. Restrições

NÃO executar outras Tasks.

NÃO modificar hml, release ou main.

NÃO modificar a instalação ou configurações do OBS.

NÃO executar git reset --hard, git clean -fd ou descarte indiscriminado de alterações.

Se o rate limit continuar, preserve o estado e interrompa sem simular conclusão.

5. Finalização

Entregue o relatório final da TASK-004 no formato anteriormente definido, com evidências reais de execução.

Somente declarar DONE após o merge e a validação pós-merge.

STOP.
```
