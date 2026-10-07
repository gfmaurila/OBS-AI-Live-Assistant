# Candidatos a ADR

Este registro identifica decisões futuras sem escolher alternativa. Cada candidato só pode ser decidido após Requirements aplicáveis, Research suficiente e fase autorizada.

| ID | Decisão candidata | Questão | Requirements relacionados | Research necessário | Estado |
|---|---|---|---|---|---|
| ADR-001 | Responsabilidades: plugin nativo, OBS WebSocket e UI/Dock | Quais capacidades ficam dentro e fora do processo OBS? | RF-001, RF-005, RF-022, RF-036; RNF-001, RNF-006, RNF-025 | RES-001, RES-004, RES-007, RES-008 | REQUIRES_RESEARCH |
| ADR-002 | Boundary C++ / .NET e IPC | Se houver componente nativo, qual contrato e transporte preservam segurança, compatibilidade, timeout e cancellation? | RF-026, RF-027; RNF-004, RNF-009, RNF-010, RNF-026 | RES-009, RES-010 | REQUIRES_RESEARCH |
| ADR-003 | Isolamento, lifecycle e filas | Como conter falhas, hangs e backpressure sem bloquear OBS? | RF-001, RF-013, RF-021, RF-025; RNF-001, RNF-008, RNF-016 | RES-008, RES-010 | REQUIRES_RESEARCH |
| ADR-004 | Provider architecture e persistência de configuração | Como delimitar adapters de chat/IA/TTS e persistir estado autorizado sem acoplamento? | RF-002, RF-005, RF-016 a RF-019, RF-024, RF-028; RNF-011, RNF-012, RNF-017, RNF-022 | RES-011, RES-014, RES-015, RES-017 | REQUIRES_RESEARCH |
| ADR-005 | Armazenamento e lifecycle de secrets | Qual mecanismo protege API keys e tokens durante install, upgrade, repair e uninstall? | RF-018; RNF-003, RNF-006 | RES-012, RES-013, RES-014, RES-021 | REQUIRES_RESEARCH |
| ADR-006 | Integração de áudio TTS | Como entregar, monitorar, limitar e cancelar áudio sem comprometer OBS? | RF-023 a RF-025; RNF-002, RNF-015, RNF-025 | RES-005, RES-006, RES-017 | REQUIRES_RESEARCH |
| ADR-007 | Tecnologia e lifecycle do installer | Como instalar, atualizar, reparar e remover app/integração com rollback e coexistência? | RF-032 a RF-035; RNF-020, RNF-021, RNF-024, RNF-025 | RES-002, RES-003, RES-018 a RES-022 | REQUIRES_RESEARCH |
| ADR-008 | Configuração, UI e controles operacionais | Como apresentar profiles, LiveContext, moderação, filas e diagnóstico com validação e acessibilidade? | RF-002 a RF-004, RF-009 a RF-012, RF-031; RNF-004, RNF-013, RNF-014, RNF-027 | RES-004; refinamento de UX/Security Requirements | NOT STARTED |
| ADR-009 | Memória, histórico e retenção | Quais dados temporários ou persistentes existem e qual o lifecycle de cada categoria? | RF-004, RF-015, RF-029, RF-030; RNF-005, RNF-023 | RES-011, RES-023, RES-024; Client Decision | REQUIRES_CLIENT_DECISION |
| ADR-010 | Estratégia de compatibilidade OBS | Qual versão/faixa 32.x é suportada e como detectar, testar e evoluir compatibilidade? | RF-032; RNF-019, RNF-024 | RES-001, RES-002, RES-022 | REQUIRES_RESEARCH / REQUIRES_CLIENT_DECISION |

Nenhum candidato acima constitui ADR final, escolha tecnológica ou aprovação de implementação.

## Inputs produzidos pela pesquisa técnica

| ADR | Evidência | Opções | Recomendação para decisão futura | Trade-offs / impacto de segurança | Confidence |
|---|---|---|---|---|---|
| ADR-001 | RES-001/004/007/008 | Native, WebSocket, Hybrid | Hybrid com plugin mínimo e Core externo | Mais componentes; reduz crash surface e autoridade in-process | HIGH |
| ADR-002 | RES-009/010 | Named Pipes, gRPC, sockets, shared memory, COM | Named Pipes com DACL e protocolo versionado | Protocolo próprio; forte suporte Windows C++/.NET | HIGH |
| ADR-003 | RES-008/010 | Same process, separate process | Core separado; lifecycle supervisionado | IPC adicional em troca de contenção de falha | HIGH |
| ADR-004 | RES-011/014/015/017/024 | Ports específicos ou abstração extensível | Ports normalizados com capabilities opcionais | Evita vendor lock-in; exige normalização cuidadosa | MEDIUM |
| ADR-005 | RES-012/013/021 | Credential Manager, DPAPI, combinação | Credential Manager para secrets discretos; DPAPI user-scope para blobs | Backup/uninstall precisam política; sem plaintext | HIGH |
| ADR-006 | RES-005/006/017 | Source nativa, captura de app, virtual device | Prototipar captura de app e source nativa; evitar driver default | UX versus superfície in-process | MEDIUM |
| ADR-007 | RES-002/003/018..022/026 | WiX/Burn, Inno, MSIX/híbrido | Installer tradicional assinado e version-aware; tecnologia após spike | Elevação, rollback e signing são release gates | MEDIUM |
| ADR-008 | RES-004/025 | Dock fino + UI/Core | Dock apenas de apresentação/controle; diagnóstico redacted | Preserva responsividade e minimiza dados no OBS | HIGH |
| ADR-009 | RES-011/023/024 | Sessão, histórico e memória persistente | Baseline temporária; persistência só após client decision | Minimização reduz privacidade/corrupção | HIGH |
| ADR-010 | RES-001..003/022 | Versão exata ou faixa testada | Versão mínima + matriz explícita e detecção fail-safe | Mais testes; evita promessa ABI inválida | HIGH |

As recomendações são **Research Evidence**, não decisões. Status dos candidatos permanece pendente até Architecture/ADR autorizada.
