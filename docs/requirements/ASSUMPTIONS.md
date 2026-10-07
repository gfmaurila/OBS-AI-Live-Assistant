# Assumptions

| ID | Descrição | Origem | Impacto | Como validar | Status |
|---|---|---|---|---|---|
| ASM-001 | O streamer é o operador primário e possui autoridade para ativar, pausar e configurar o assistente. | Regras de negócio | Define permissões e jornadas. | Validar em Product Decision e testes de aceitação. | ASSUMPTION |
| ASM-002 | Uma única instalação local atenderá inicialmente a um usuário Windows por vez. | Direção de app local | Afeta escopo de configuração e secrets. | Confirmar modelo de usuário na Architecture. | ASSUMPTION / REQUIRES_CLIENT_DECISION |
| ASM-003 | `@assistente` e `!ia` serão triggers iniciais configuráveis, não comandos hardcoded imutáveis. | Regras de negócio | Afeta detecção e UX. | Validar configuração durante detalhamento de produto. | ASSUMPTION |
| ASM-004 | Resposta textual pode ser usada sem TTS e TTS pode ser desabilitado sem impedir texto. | Regras de negócio | Permite degradação segura. | Validar em critérios de aceitação e UX. | ASSUMPTION |
| ASM-005 | LiveContext contém apenas dados necessários à live atual e termina com a sessão. | Regras de negócio | Limita privacidade e retenção. | Definir campos e descarte em Architecture/Data Design. | ASSUMPTION / REQUIRES_ADR |
| ASM-006 | O histórico de interações da V1 será mínimo, configurável e voltado a continuidade/diagnóstico, não arquivo indiscriminado do chat. | Segurança; regras de negócio | Afeta dados e privacidade. | Product Decision e Security Requirements. | ASSUMPTION / REQUIRES_CLIENT_DECISION |
| ASM-007 | Valores de cooldown, tamanhos, duração, timeout e capacidade de fila serão configuráveis dentro de limites seguros. | Segurança | Evita números inventados nesta fase. | Research, threat model e testes de carga futuros. | ASSUMPTION / REQUIRES_RESEARCH |
| ASM-008 | O diretório de dados poderá ficar sob `%APPDATA%`, mas o caminho exato não está aprovado. | Project Overview | Afeta installer e dados. | RES-003, RES-018 e ADR de instalação. | ASSUMPTION / REQUIRES_RESEARCH |
| ASM-009 | Ao menos um AI Provider e um TTS Provider serão selecionados para a V1, sem compromisso atual com os candidatos nomeados. | Direção multi-provider | Necessário para entrega utilizável. | Product Decision após RES-015 e RES-017. | ASSUMPTION / REQUIRES_CLIENT_DECISION |
