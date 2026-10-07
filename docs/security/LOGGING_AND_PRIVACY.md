# Logging, privacidade e armazenamento local

## Logging seguro

Logs e diagnósticos devem ser úteis para distinguir provider, etapa, timeout, cancellation, saturação e estado degradado, preferencialmente por eventos estruturados e correlation identifier não sensível quando aplicável. Nunca devem conter credenciais em plaintext, authorization codes, tokens, payload integral sensível, prompts completos por padrão ou stack traces que revelem secrets.

Redaction e masking devem ser aplicados antes da gravação e também a erros retornados por providers. Falha no filtro não autoriza registro do valor original. O mecanismo e os testes de redaction dependem da stack aprovada.

## Privacidade e minimização

Mensagens, usernames, IDs, sessões, histórico, memória, logs e analytics somente podem ser coletados, enviados ou persistidos quando necessários a uma finalidade aprovada. Analytics avançado não faz parte da V1. A interface deve tornar visíveis as categorias persistidas e permitir as operações de exclusão aprovadas.

Prazos de retenção por categoria ainda não possuem fonte quantitativa e permanecem `REQUIRES_CLIENT_DECISION`. Até sua definição, nenhum prazo é inventado e dados condicionais não são presumidos como persistidos.

## Armazenamento local

Banco, configuração, cache, logs e arquivos temporários devem observar menor acesso, validação na leitura, integridade, limpeza e lifecycle definido. Secrets ficam separados e nunca em plaintext nesses locais. Corrupção, arquivo adulterado ou versão incompatível não pode ser tratado como sucesso silencioso.

Criptografia, ACLs, tecnologia do banco e mecanismo de recovery permanecem para Research e ADR.

## Installer e lifecycle

Installer, upgrade, repair e uninstall futuros devem:

- ter origem e integridade verificáveis;
- solicitar somente privilégios necessários;
- declarar arquivos e locais afetados;
- falhar ou reverter de forma controlada;
- preservar ou remover configuração, dados, logs e secrets conforme política explícita;
- não alterar configuração externa do OBS sem autorização.

Code signing, formato de pacote, rollback e update integrity são `REQUIRES_RESEARCH`.

## Supply chain

Dependências devem ser minimizadas, ter origem e integridade verificáveis, passar por revisão de vulnerabilidades conhecidas e possuir estratégia de atualização compatível com o risco. Esta Task não seleciona nem instala ferramentas.
