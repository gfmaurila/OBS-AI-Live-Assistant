#include "obs_ai/module_lifecycle.hpp"

namespace obs_ai {
namespace {

constexpr std::uint64_t FlagsMask = 7ULL;

std::uint64_t next_generation(std::uint64_t state) noexcept
{
	constexpr unsigned generation_shift = 3;
	return ((state >> generation_shift) + 1ULL) << generation_shift;
}

} // namespace

ModuleLifecycle::ModuleLifecycle(FrontendEventRegistrar &registrar, EventLogger &logger) noexcept
	: registrar_(registrar), logger_(logger)
{
}

bool ModuleLifecycle::load()
{
	std::lock_guard<std::mutex> lock(lifecycle_mutex_);
	const auto current = state_.load(std::memory_order_acquire);
	if ((current & LoadedFlag) != 0)
		return true;

	const auto run_state = next_generation(current) | LoadedFlag;
	state_.store(run_state, std::memory_order_release);

	if (!registrar_.add(&ModuleLifecycle::dispatch, this)) {
		invalidate_current_run();
		registrar_.remove(&ModuleLifecycle::dispatch, this);
		registered_.store(false, std::memory_order_release);
		logger_.write(LogLevel::Error, LogCode::CallbackRegistrationFailed);
		return false;
	}

	registered_.store(true, std::memory_order_release);
	logger_.write(LogLevel::Info, LogCode::ModuleLoaded);
	return true;
}

void ModuleLifecycle::unload()
{
	std::lock_guard<std::mutex> lock(lifecycle_mutex_);
	const auto current = state_.load(std::memory_order_acquire);
	const bool was_loaded = (current & LoadedFlag) != 0;
	const bool was_registered = registered_.exchange(false, std::memory_order_acq_rel);
	if (!was_loaded && !was_registered)
		return;

	// Invalidate the run before unregistering so an in-flight callback cannot
	// mutate health after shutdown has started. The context remains process-lived.
	invalidate_current_run();
	if (was_registered)
		registrar_.remove(&ModuleLifecycle::dispatch, this);
	if (was_loaded)
		logger_.write(LogLevel::Info, LogCode::ModuleUnloaded);
}

ModuleHealth ModuleLifecycle::health() const noexcept
{
	const auto state = state_.load(std::memory_order_acquire);
	return {
		(state & LoadedFlag) != 0,
		(state & FinishedLoadingFlag) != 0,
		(state & StreamingFlag) != 0,
	};
}

void ModuleLifecycle::dispatch(FrontendEvent event, void *context) noexcept
{
	if (context != nullptr)
		static_cast<ModuleLifecycle *>(context)->handle(event);
}

void ModuleLifecycle::handle(FrontendEvent event) noexcept
{
	std::uint64_t mask = 0;
	bool set = true;
	switch (event) {
	case FrontendEvent::FinishedLoading:
		mask = FinishedLoadingFlag;
		break;
	case FrontendEvent::StreamingStarted:
		mask = StreamingFlag;
		break;
	case FrontendEvent::StreamingStopped:
		mask = StreamingFlag;
		set = false;
		break;
	case FrontendEvent::Exit:
		detach_frontend_callback();
		return;
	case FrontendEvent::Other:
		return;
	}

	auto observed = state_.load(std::memory_order_acquire);
	const auto callback_generation = observed >> GenerationShift;
	for (;;) {
		if ((observed & LoadedFlag) == 0 || (observed >> GenerationShift) != callback_generation)
			return;

		const auto flags = (observed & FlagsMask) | (set ? mask : 0ULL);
		const auto next = (observed & ~FlagsMask) | (set ? flags : (flags & ~mask));
		if (state_.compare_exchange_weak(observed, next, std::memory_order_acq_rel, std::memory_order_acquire))
			return;
	}
}

void ModuleLifecycle::detach_frontend_callback() noexcept
{
	// OBS emits EXIT while the frontend API is still alive, then destroys that
	// API before calling module unload. Detach here so unload stays idempotent.
	if (registered_.exchange(false, std::memory_order_acq_rel))
		registrar_.remove(&ModuleLifecycle::dispatch, this);
}

void ModuleLifecycle::invalidate_current_run() noexcept
{
	auto observed = state_.load(std::memory_order_acquire);
	for (;;) {
		const auto next = next_generation(observed);
		if (state_.compare_exchange_weak(observed, next, std::memory_order_acq_rel, std::memory_order_acquire))
			return;
	}
}

} // namespace obs_ai
