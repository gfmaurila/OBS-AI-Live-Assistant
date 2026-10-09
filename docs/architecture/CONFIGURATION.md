# Modelo e validação de configuração

Este documento registra o incremento local da `TASK-014`. O contrato fica em
`ObsAi.Application.Configuration` e implementa somente configuração não secreta,
validação atômica e estado em memória. Persistência, UI, credential store e adapters
concretos permanecem fora desta Task.

## Modelo aprovado

`ConfigurationDraft` representa uma candidata não confiável. Ela reúne:

- perfis imutáveis de `ObsAi.Domain` e o identificador do perfil selecionado;
- triggers habilitados;
- ativação independente de TTS;
- seleção de AI/TTS provider por `ProviderId`, `ProviderConfigurationReference` e
  `CredentialReference`, sem valores de opção ou credencial;
- limites de rate limiting, entrada e filas bounded.

`AssistantConfiguration` é criado somente pelo `ConfigurationValidator` após a
validação completa. Suas coleções são cópias read-only e reutilizam
`AssistantProfile`, `InputValidationLimits` e `WorkQueueSettings`.

## Limites e defaults

`ConfigurationPolicy` é a entrada confiável da composition root. Ela fornece:

- máximos de quantidade/tamanho para perfis e triggers;
- faixas mínima/máxima e defaults explícitos para cooldowns, limites de entrada,
  capacidade e concorrência;
- defaults de saturação por estágio de fila;
- allowlists separadas para AI Providers e TTS Providers.

Nenhum número de produto é hardcoded no modelo. Os defaults numéricos precisam
estar dentro das faixas aprovadas. Quando valores opcionais são omitidos, aplicam-se
os defaults fornecidos pela policy. TTS omitido assume `false`, providers permanecem
ausentes e triggers omitidos resultam em lista vazia, defaults fail-closed que não
iniciam trabalho externo.

## Validação atômica

O validator rejeita de forma determinística:

- configuração, perfis ou perfil selecionado ausentes;
- perfil selecionado fora da coleção, IDs duplicados ou limites de texto excedidos;
- triggers vazios, com controle, duplicados ou acima dos limites;
- provider desconhecido ou aprovado para a finalidade errada;
- TTS habilitado sem provider TTS;
- números fora das faixas, enums desconhecidos e filas duplicadas ou com entradas nulas.

`ConfigurationState.TryApply` serializa validação e troca de snapshot. Uma candidata
inválida retorna erros seguros e preserva a mesma instância do último estado válido;
leitores nunca observam configuração parcial.

## Segurança

- O modelo comum não possui campos para API key, token, password, connection string
  ou valor secreto.
- Credenciais e metadata específica são apenas referências opacas; seus `ToString()`
  são redacted.
- Erros contêm path, código e mensagem constante, sem ecoar o valor rejeitado.
- Faixas confiáveis impedem que uma candidata aumente filas, concorrência ou campos
  além do limite aprovado.
- Estados e snapshots não compartilham coleções mutáveis.

O armazenamento protegido de credenciais pertence à `TASK-017`; lifecycle BYOK/OAuth
pertence à `TASK-018`; persistência SQLite pertence às `TASK-019` a `TASK-021`;
redaction geral pertence à `TASK-016`.

## Rastreabilidade

| Requisito/decisão | Implementação | Evidência |
|---|---|---|
| RF-002, RNF-022 | `ConfigurationValidator`, `ConfigurationState` | `ConfigurationValidatorTests`, `ConfigurationStateTests` |
| RF-003 | perfis e seleção validados | `ConfigurationValidatorTests` |
| RF-008, RF-011 | triggers e `RateLimitSettings` limitados | `ConfigurationValidatorTests` |
| RF-017, RF-023 | referências de provider por finalidade e TTS independente | Unit e Security Tests |
| SEC-002, SEC-014 | bounds de entrada, filas e rate limits | Unit e Security Tests |
| SEC-008 | ausência de secret values e referências redacted | `ConfigurationSecurityTests` |
| ADR-004, ADR-008 | boundary vendor-neutral e validação antes de aplicar | Architecture Tests |
