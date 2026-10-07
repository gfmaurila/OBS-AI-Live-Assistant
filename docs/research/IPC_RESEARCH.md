# Pesquisa — IPC C++ ↔ .NET

| Opção | Segurança | Complexidade | Streaming/versionamento | Deploy | Recomendação |
|---|---|---|---|---|---|
| Named Pipes | ACL/DACL Win32; negar rede | Baixa | Protocolo próprio | Nativo em C++/.NET | Preferida |
| gRPC | TLS/auth configuráveis | Alta | Protobuf e streaming fortes | Runtime/deps adicionais | Reserva se contrato crescer |
| Local sockets | Exige ACL/auth próprios | Média | Protocolo próprio | Moderado | Sem vantagem clara |
| Shared memory | Sincronização e ACL difíceis | Alta | Excelente throughput | Complexo | Apenas áudio de alto volume comprovado |
| COM | Security/registration próprios | Alta e acoplada | Contratos fortes | Registro/lifecycle | Não preferida |

Named Pipes deve usar DACL explícita para o usuário/logon SID, negar `NETWORK`, limitar instâncias/tamanho, autenticar a sessão, enquadrar mensagens e negociar versão. A ACL default é insuficiente. Timeouts, cancellation, backpressure e reconexão são requisitos do protocolo futuro.

Fontes: SRC-014..016 e documentação oficial gRPC. Confidence: **HIGH** para baseline, **MEDIUM** para desenho do protocolo. Status: **RESOLVED**.
