#pragma once

#include <atomic>
#include <cstdint>
#include <mutex>

namespace obs_ai {

static_assert(std::atomic<std::uint64_t>::is_always_lock_free,
	      "The frontend callback state must remain lock-free on the x64 target");

enum class FrontendEvent : std::uint8_t {
	FinishedLoading,
	StreamingStarted,
	StreamingStopped,
	Other,
};

enum class LogLevel : std::uint8_t { Info, Error };
enum class LogCode : std::uint8_t { ModuleLoaded, ModuleUnloaded, CallbackRegistrationFailed };

using EventCallback = void (*)(FrontendEvent event, void *context) noexcept;

class FrontendEventRegistrar {
public:
	virtual ~FrontendEventRegistrar() = default;
	virtual bool add(EventCallback callback, void *context) noexcept = 0;
	virtual void remove(EventCallback callback, void *context) noexcept = 0;
};

class EventLogger {
public:
	virtual ~EventLogger() = default;
	virtual void write(LogLevel level, LogCode code) noexcept = 0;
};

struct ModuleHealth final {
	bool loaded;
	bool finished_loading;
	bool streaming;
};

class ModuleLifecycle final {
public:
	ModuleLifecycle(FrontendEventRegistrar &registrar, EventLogger &logger) noexcept;

	ModuleLifecycle(const ModuleLifecycle &) = delete;
	ModuleLifecycle &operator=(const ModuleLifecycle &) = delete;

	bool load();
	void unload();
	ModuleHealth health() const noexcept;

private:
	static void dispatch(FrontendEvent event, void *context) noexcept;
	void handle(FrontendEvent event) noexcept;
	void invalidate_current_run() noexcept;

	static constexpr std::uint64_t LoadedFlag = 1ULL;
	static constexpr std::uint64_t FinishedLoadingFlag = 2ULL;
	static constexpr std::uint64_t StreamingFlag = 4ULL;
	static constexpr unsigned GenerationShift = 3;

	FrontendEventRegistrar &registrar_;
	EventLogger &logger_;
	std::mutex lifecycle_mutex_;
	std::atomic<std::uint64_t> state_{0};
	bool registered_ = false;
};

} // namespace obs_ai
