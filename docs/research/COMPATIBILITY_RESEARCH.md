# Pesquisa — compatibilidade OBS

Não há evidência para prometer compatibilidade binária irrestrita. O produto deve declarar versão mínima e conjunto testado, detectar versão/RPC/capabilities, recusar combinações desconhecidas de forma segura e reconstruir/testar o plugin contra novas majors. A mudança oficial de layout iniciada no OBS 33 demonstra que installer e runtime devem ser version-aware.

Recomendação: baseline de Architecture para OBS 32.x x64, com 32.2.2 como versão corrente de referência em 2026-10-06; o cliente ainda decide se o suporte comercial cobre uma versão mínima, todas as minors selecionadas ou apenas a release validada. Cada release exige smoke/regression matrix.

## .NET 10 e Windows

.NET 10 é LTS até novembro de 2028. Na matriz Microsoft vigente, Windows 11 x64 é suportado; Windows 10 aparece apenas nas edições LTSC/Enterprise listadas. Portanto, “Windows 10” não pode ser prometido genericamente sem restringir edições ou revisar o target/runtime. Essa decisão não impede desenhar a Architecture em .NET 10, mas bloqueia a política final de compatibilidade e release.

Fontes: SRC-001..007/012/055/056. Confidence: **HIGH**. Status: **CLIENT_DECISION** para faixas OBS/Windows; pesquisa técnica concluída.
