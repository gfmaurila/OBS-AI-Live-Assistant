# Backlog consolidado de Research

Inventário único derivado de `RESEARCH_DEPENDENCIES.md`, `SECURITY_RESEARCH_DEPENDENCIES.md`, Open Questions e candidatos a ADR. Duplicações conceituais foram consolidadas sem perder os IDs de origem.

| ID | Título | Pergunta | Origem e rastreabilidade | Prioridade | Blocking? | Fontes necessárias | Status |
|---|---|---|---|---|---|---|---|
| RES-001 | SDK de plugin OBS 32.x | Quais APIs, ABI e toolchain suportam plugin x64? | RDEP-001; ADR-001/010; RF-001/032 | MUST | Sim | OBS oficial | RESOLVED |
| RES-002 | Build nativo | Como obter e compilar dependências sem acoplar ao source tree? | RDEP-002; ADR-007/010 | MUST | Sim | OBS template/repo | RESOLVED |
| RES-003 | Instalação de plugin | Quais layouts e permissões são suportados? | RDEP-003; ADR-007 | MUST | Sim | OBS KB | RESOLVED |
| RES-004 | OBS Dock | Qual API e lifecycle de UI embutida são suportados? | RDEP-004; OQ-002; ADR-001/008 | MUST | Sim | OBS Frontend API | RESOLVED |
| RES-005 | Áudio nativo | Como entregar áudio TTS com controle do OBS? | RDEP-005; OQ-003; ADR-006 | MUST | Sim | libobs/OBS KB | PARTIALLY_RESOLVED |
| RES-006 | Source versus captura/dispositivo | Qual alternativa oferece melhor UX e isolamento? | RDEP-006; ADR-006 | MUST | Sim | OBS KB; protótipo futuro | PARTIALLY_RESOLVED |
| RES-007 | OBS WebSocket | Quais requests, events, autenticação e limites existem? | RDEP-007; OQ-001; ADR-001 | MUST | Sim | protocolo oficial | RESOLVED |
| RES-008 | Divisão de responsabilidades | O que deve ficar dentro e fora do OBS? | RDEP-008; ADR-001/003 | MUST | Sim | síntese RES-001/004/007 | RESOLVED |
| RES-009 | IPC C++ ↔ .NET | Qual transporte atende segurança, versionamento e falha? | RDEP-009; SRES-008; ADR-002 | MUST | Sim | Microsoft/gRPC | RESOLVED |
| RES-010 | Isolamento de processo | Como conter crash, hang e recursos do Assistant Core? | RDEP-010; ADR-002/003 | MUST | Sim | Microsoft Win32 | RESOLVED |
| RES-011 | SQLite lifecycle | Como tratar concorrência, backup, integridade e migração? | RDEP-011; SRES-006; ADR-004/009 | MUST | Sim | SQLite oficial | RESOLVED |
| RES-012 | Windows Credential Manager | É adequado para credenciais BYOK discretas? | RDEP-012; SRES-001/003; ADR-005 | MUST | Sim | Microsoft Win32 | RESOLVED |
| RES-013 | DPAPI | Quando usar DPAPI diretamente? | RDEP-013; SRES-001/003; ADR-005 | MUST | Sim | Microsoft Win32 | RESOLVED |
| RES-014 | YouTube Live Chat | Quais APIs, OAuth, quotas, erros e políticas se aplicam? | RDEP-014; SRES-002; OQ-012; ADR-004 | MUST | Sim | Google/YouTube | RESOLVED |
| RES-015 | Abstração de IA | Qual contrato mínimo comum aos providers? | RDEP-015; ADR-004 | MUST | Não | providers oficiais | PARTIALLY_RESOLVED |
| RES-016 | IA local | Ollama deve integrar o V1? | RDEP-016; OQ-006; ADR-004 | COULD | Não | Ollama; benchmark futuro | CLIENT_DECISION |
| RES-017 | Abstração TTS | Qual contrato mínimo comum a TTS local/cloud? | RDEP-017; ADR-004/006 | MUST | Não | Microsoft/ElevenLabs | PARTIALLY_RESOLVED |
| RES-018 | Installer | Qual tecnologia cobre app e plugin com rollback? | RDEP-018; SRES-007; OQ-008; ADR-007 | MUST | Sim | Microsoft/WiX/Inno | PARTIALLY_RESOLVED |
| RES-019 | Upgrade | Como atualizar com integridade e compatibilidade? | RDEP-019; SRES-009; ADR-007 | MUST | Sim | Microsoft/installer | RESOLVED |
| RES-020 | Repair | Como reparar sem apagar dados ou secrets válidos? | RDEP-020; ADR-007 | MUST | Não | installer oficial | RESOLVED |
| RES-021 | Uninstall | Quais dados e credenciais remover ou preservar? | RDEP-021; ADR-005/007 | MUST | Não | Requirements + política | CLIENT_DECISION |
| RES-022 | Faixa de compatibilidade OBS | Suportar versão exata ou toda 32.x? | RDEP-022; OQ-013; ADR-010 | MUST | Não | OBS releases/KB | CLIENT_DECISION |
| RES-023 | Memória e retenção | Memória persistente será habilitada e sob qual política? | RDEP-023; SRES-010; ADR-009 | SHOULD | Não | política do produto | CLIENT_DECISION |
| RES-024 | Privacidade dos providers | Quais termos aplicar aos providers selecionados? | RDEP-024; SRES-011; ADR-004/009 | MUST | Não | termos vigentes | PARTIALLY_RESOLVED |
| RES-025 | Observabilidade e redaction | Como produzir diagnóstico local sem expor dados? | SRES-005; SEC-020..023; ADR-008 | MUST | Sim | Microsoft .NET | RESOLVED |
| RES-026 | Supply chain e assinatura | Como verificar dependências, binários, installer e updates? | SRES-007/009/012; SEC-031..034; ADR-007 | MUST | Sim | Microsoft; fornecedores | RESOLVED |
| RES-027 | Compatibilidade .NET 10 / Windows | Quais edições Windows alvo são oficialmente suportadas pelo runtime? | CON-001/002; RNF-019/020; ADR-007/010 | MUST | Não | Microsoft .NET | CLIENT_DECISION |

## Resultado

- RESOLVED: 16
- PARTIALLY_RESOLVED: 6
- CLIENT_DECISION: 5
- BLOCKED: 0
- Total: 27

`PARTIALLY_RESOLVED` indica evidência suficiente para Architecture, mas escolha final dependente de ADR, provider ou protótipo. Não há pesquisa bloqueada.
