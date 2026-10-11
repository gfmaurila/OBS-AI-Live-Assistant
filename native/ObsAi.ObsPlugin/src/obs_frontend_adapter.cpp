#include "obs_ai/module_lifecycle.hpp"

#include <obs-frontend-api.h>

namespace obs_ai {
namespace {

static_assert(std::atomic<EventCallback>::is_always_lock_free,
	      "Frontend callback pointer access must remain lock-free on the x64 target");
static_assert(std::atomic<void *>::is_always_lock_free,
	      "Frontend callback context access must remain lock-free on the x64 target");

FrontendEvent from_obs_event(obs_frontend_event event) noexcept
{
	switch (event) {
	case OBS_FRONTEND_EVENT_FINISHED_LOADING:
		return FrontendEvent::FinishedLoading;
	case OBS_FRONTEND_EVENT_STREAMING_STARTED:
		return FrontendEvent::StreamingStarted;
	case OBS_FRONTEND_EVENT_STREAMING_STOPPED:
		return FrontendEvent::StreamingStopped;
	case OBS_FRONTEND_EVENT_EXIT:
		return FrontendEvent::Exit;
	default:
		return FrontendEvent::Other;
	}
}

class ObsFrontendEventRegistrar final : public FrontendEventRegistrar {
public:
	bool add(EventCallback callback, void *context) noexcept override
	{
		if (callback == nullptr || callback_.load(std::memory_order_acquire) != nullptr)
			return false;

		context_.store(context, std::memory_order_relaxed);
		callback_.store(callback, std::memory_order_release);
		obs_frontend_add_event_callback(&ObsFrontendEventRegistrar::dispatch, this);
		return true;
	}

	void remove(EventCallback callback, void *context) noexcept override
	{
		if (callback_.load(std::memory_order_acquire) != callback ||
		    context_.load(std::memory_order_acquire) != context)
			return;

		obs_frontend_remove_event_callback(&ObsFrontendEventRegistrar::dispatch, this);
		callback_.store(nullptr, std::memory_order_release);
		context_.store(nullptr, std::memory_order_release);
	}

private:
	static void dispatch(enum obs_frontend_event event, void *private_data)
	{
		auto *self = static_cast<ObsFrontendEventRegistrar *>(private_data);
		if (self == nullptr)
			return;

		const auto callback = self->callback_.load(std::memory_order_acquire);
		if (callback == nullptr)
			return;

		void *const context = self->context_.load(std::memory_order_acquire);
		callback(from_obs_event(event), context);
	}

	std::atomic<EventCallback> callback_{nullptr};
	std::atomic<void *> context_{nullptr};
};

class ObsEventLogger final : public EventLogger {
public:
	void write(LogLevel level, LogCode code) noexcept override
	{
		int obs_level = level == LogLevel::Error ? LOG_ERROR : LOG_INFO;
		const char *event_name = "module.unknown";
		switch (code) {
		case LogCode::ModuleLoaded:
			event_name = "module.loaded";
			break;
		case LogCode::ModuleUnloaded:
			event_name = "module.unloaded";
			break;
		case LogCode::CallbackRegistrationFailed:
			event_name = "frontend_callback.registration_failed";
			break;
		}
		blog(obs_level, "[obs-ai-live-assistant] event=%s", event_name);
	}
};

ObsFrontendEventRegistrar registrar;
ObsEventLogger logger;
ModuleLifecycle lifecycle(registrar, logger);

} // namespace

ModuleLifecycle &module_lifecycle() noexcept
{
	return lifecycle;
}

} // namespace obs_ai
