# Quality Gates

## Gates ativos na fase documental

| Gate | Evidência obrigatória |
|---|---|
| Documentation | Os artefatos solicitados existem, são legíveis, usam `pt-BR` quando destinados a pessoas e possuem navegação válida. |
| Scope Compliance | As alterações permanecem no escopo documental e de governança, sem iniciar fases posteriores. |
| Knowledge Compliance | Fatos, direções, hipóteses, necessidades de pesquisa e decisões estão diferenciados. |
| Security Check | O conteúdo alterado não contém secrets nem orientação insegura sobre credenciais. |
| GitFlow | O trabalho ocorre na feature da Task e segue o fluxo de PR aprovado. |
| Prompt Traceability | O prompt autorizador é arquivado no mesmo commit da Task. |
| Code Review | O diff completo da Task é revisado por severidade antes do merge. |
| Secret Scan | O conteúdo staged é verificado contra credenciais, tokens, chaves e passwords prováveis. |

Finding Critical, finding High não resolvido, secret, conflito, alteração fora do escopo, validação obrigatória reprovada, push incompleto ou divergência local/remota bloqueiam o merge.

## Gates por estágio de entrega

### DEVELOPMENT GATE

Aplica-se a `feature/task-* -> develop`. Exige validação da Task, Code Review, correções obrigatórias, Prompt Traceability, Secret Check, commit, push e PR válidos.

### HML GATE

Aplica-se a `develop -> hml` quando um conjunto coerente de alterações estiver pronto para homologação. Exige revisão do escopo agregado, Quality Gates aplicáveis, PR e autorização de promoção.

### RELEASE GATE

Aplica-se à criação e estabilização de `release/1.0.0XXXX` a partir de `hml` aprovado. Permite somente ajustes finais, correções de release, documentação, versionamento, metadados e correções bloqueadoras aprovadas.

### PRODUCTION GATE

Aplica-se a `release/1.0.0XXXX -> main`. Exige Final Quality Gate, PR aprovado, confirmação de versão estável e autorização de produção. Tag e GitHub Release ocorrem somente após o merge de uma release real aprovada.

## Gates futuros de implementação

Os itens abaixo permanecem **NOT APPLICABLE UNTIL IMPLEMENTATION**. Eles não podem ser marcados como PASSED antes de existirem comandos determinísticos e artefatos correspondentes:

- Build
- Unit Tests
- Integration Tests
- Architecture Validation
- Installer Tests
- OBS Compatibility Tests
- Regression Tests
