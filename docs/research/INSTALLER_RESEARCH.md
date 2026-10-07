# Pesquisa — installer Windows

| Opção | Vantagens | Desvantagens | Compatibilidade plugin OBS | Recomendação | Confidence |
|---|---|---|---|---|---|
| WiX/MSI + Burn | Repair/upgrade empresarial, bundle/prerequisites | Curva e authoring maiores | Custom actions version-aware | Finalista | MEDIUM |
| Inno Setup | EXE simples, bom controle de arquivos/uninstall/signing | Repair/rollback menos declarativos | Bom para ProgramData/layouts | Finalista pragmática | HIGH |
| MSIX puro | Integridade, uninstall/update do pacote | Deployment externo do plugin e elevação complicam modelo | Ajuste ruim para layouts OBS externos | Não preferir como pacote único | MEDIUM |

O installer deve detectar OBS e arquitetura, exigir fechamento do OBS, validar versão/layout, instalar Core e plugin separadamente, preservar configuração compatível, fazer rollback em falha e oferecer repair/uninstall. A escolha final requer ADR e protótipo em máquina limpa.

Fontes: SRC-044..050. Status: **PARTIALLY_RESOLVED**.
