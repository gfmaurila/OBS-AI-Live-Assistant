# Pesquisa — SQLite

SQLite é adequado ao V1 local por ser embutido e transacional. WAL melhora concorrência de leitura, mas mantém **um writer**, exige arquivos `-wal/-shm` no mesmo host e checkpoint controlado. Se WAL for usado, a biblioteca deve incluir a correção do bug WAL-reset: SQLite 3.51.3 ou backports 3.50.7/3.44.6, no mínimo.

Requisitos para ADR: single-writer, transações curtas, migrations versionadas e reversíveis quando possível, `integrity_check`/health, Backup API para snapshot consistente, retenção e recuperação documentadas. Cópia de arquivo em uso não é backup. SQLite core não criptografa dados; secrets permanecem no secret store.

Fontes: SRC-018..020. Confidence: **HIGH**. Status: **RESOLVED**.
