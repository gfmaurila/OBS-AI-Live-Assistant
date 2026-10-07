# Pesquisa — OBS Dock

A OBS Frontend API oferece `obs_frontend_add_dock_by_id`, que registra um `QWidget`, integra-o ao menu Docks e preserva o lifecycle de visibilidade; a remoção usa `obs_frontend_remove_dock`. Essa é a abordagem oficial para UI embutida.

Recomendação para ADR: Dock fino, responsável por apresentação e sinalização de estado, sem providers, persistência ou processamento pesado. Toda chamada deve respeitar thread/lifecycle do frontend e tolerar Core indisponível.

Fonte: SRC-003. Confidence: **HIGH**. Status: **RESOLVED**.
