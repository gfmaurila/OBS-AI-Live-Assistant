# Pesquisa — code signing

Executáveis, DLL nativa, installer e uninstaller distribuídos devem ser Authenticode-signed e timestamped por identidade estável. MSIX exige assinatura; para EXE/MSI fora da Store, certificado de publisher ou serviço suportado é necessário. Assinatura ajuda integridade e reputação, mas não garante aceitação imediata do SmartScreen; reputação também considera publisher/arquivo.

Certificado self-signed é apenas para teste. Chaves privadas não entram no repositório nem na máquina de desenvolvimento comum; CI/release deve usar secret/key service e verificar assinatura/hash antes de publicar. A aquisição do certificado e provider de signing permanece decisão operacional posterior.

Fontes: SRC-049/053/054. Confidence: **HIGH**. Status: **RESOLVED**.
