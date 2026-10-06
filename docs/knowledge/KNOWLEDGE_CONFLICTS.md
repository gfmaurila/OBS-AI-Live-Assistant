# Conflitos de conhecimento

| Fontes | Conflito potencial | Tratamento atual | Estado |
|---|---|---|---|
| Referência CMS/Azure versus OBS-AI-Live-Assistant | A referência contém Azure, CMS, multi-tenant, React, bancos em cloud, mensageria, RAG, workers e Architecture específica da plataforma. | Usar somente padrões de organização, navegação, Governance, gates, Reports e estados de Tasks. | RESOLVED FOR BASELINE |
| Direção do produto versus design não aprovado | O contexto atual cita .NET, SQLite, limites de providers e possíveis mecanismos do OBS. | Rotular direções e hipóteses; exigir Research e ADRs para escolhas abertas. | OPEN / CONTROLLED |
| Caminho esperado de dados versus incerteza da instalação | `%APPDATA%\obs-studio\obs-ai-live-assistant` é previsto, mas caminhos oficiais de plugin e dados exigem pesquisa. | Tratar o caminho de runtime como hipótese e não criá-lo. | REQUIRES_RESEARCH |
| Skills básicas versus Advanced Skills | Existem dez Skills básicas; o pacote oficial das oito Advanced Skills está ausente. | Manter as Skills básicas inalteradas e reportar Advanced Skills como bloqueadas. | BLOCKED |

Nenhum conflito foi resolvido copiando a Architecture funcional do projeto de referência.
