#include "obs_ai/module_lifecycle.hpp"

#include <obs-module.h>

namespace obs_ai {
ModuleLifecycle &module_lifecycle() noexcept;
}

OBS_DECLARE_MODULE()

extern "C" MODULE_EXPORT bool obs_module_load(void)
{
	try {
		return obs_ai::module_lifecycle().load();
	} catch (...) {
		blog(LOG_ERROR, "[obs-ai-live-assistant] event=module.load_failed code=unexpected_exception");
		return false;
	}
}

extern "C" MODULE_EXPORT void obs_module_unload(void)
{
	try {
		obs_ai::module_lifecycle().unload();
	} catch (...) {
		blog(LOG_ERROR, "[obs-ai-live-assistant] event=module.unload_failed code=unexpected_exception");
	}
}
