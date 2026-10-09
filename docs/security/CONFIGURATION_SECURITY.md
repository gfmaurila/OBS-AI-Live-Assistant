# Segurança da configuração — TASK-014

## Superfície analisada

A configuração é entrada não confiável no boundary streamer/UI → Assistant Core.
Esta Task não lê arquivos, não persiste dados, não acessa rede e não executa ações
OBS. O ator relevante é um usuário/processo local capaz de fornecer uma candidata
malformada ou excessiva.

## Controles implementados

- validação fail-closed do snapshot completo antes da aplicação;
- preservação do último snapshot válido diante de qualquer erro;
- limites confiáveis para quantidades, textos, rate limits, input e filas;
- allowlists independentes de AI e TTS providers;
- metadata e credenciais somente por referências tipadas e opacas;
- mensagens de erro constantes, sem valores externos ou stack trace;
- cópias read-only e isolamento entre instâncias de configuração.

## Resultado da auditoria

- `CRITICAL`: 0
- `HIGH`: 0
- `MEDIUM`: 0 após separar as allowlists por finalidade e rejeitar com segurança
  entradas nulas na coleção de filas
- `LOW`: 0
- `INFO`: armazenamento, UI, serialização, BYOK/OAuth e redaction geral continuam
  explicitamente atribuídos às Tasks futuras correspondentes.

Os testes `ConfigurationSecurityTests` cobrem exposição estrutural de secrets,
redaction de referências, mensagens seguras, exaustão de recursos, entradas nulas,
aplicação fail-closed, isolamento e ausência de coleções compartilhadas.
