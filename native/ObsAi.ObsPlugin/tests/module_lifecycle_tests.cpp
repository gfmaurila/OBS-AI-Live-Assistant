#include "obs_ai/module_lifecycle.hpp"

#include <array>
#include <iostream>
#include <string_view>
#include <thread>
#include <vector>

namespace {

using namespace obs_ai;

struct LoggedEvent final {
	LogLevel level;
	LogCode code;
};

class FakeLogger final : public EventLogger {
public:
	void write(LogLevel level, LogCode code) noexcept override { entries.push_back({level, code}); }
	std::vector<LoggedEvent> entries;
};

class FakeRegistrar final : public FrontendEventRegistrar {
public:
	bool add(EventCallback callback, void *context) noexcept override
	{
		++add_calls;
		last_callback = callback;
		last_context = context;
		registered = true;
		return !fail_registration;
	}

	void remove(EventCallback callback, void *context) noexcept override
	{
		if (callback == last_callback && context == last_context) {
			++remove_calls;
			registered = false;
		}
	}

	void emit(FrontendEvent event) const noexcept
	{
		if (registered && last_callback != nullptr)
			last_callback(event, last_context);
	}

	bool fail_registration = false;
	bool registered = false;
	unsigned add_calls = 0;
	unsigned remove_calls = 0;
	EventCallback last_callback = nullptr;
	void *last_context = nullptr;
};

int failures = 0;

void expect(bool condition, std::string_view label)
{
	if (!condition) {
		++failures;
		std::cerr << "FAIL " << label << '\n';
	}
}

void test_load_and_events()
{
	FakeRegistrar registrar;
	FakeLogger logger;
	ModuleLifecycle module(registrar, logger);

	expect(module.load(), "load succeeds");
	expect(module.load(), "repeated load is idempotent");
	expect(registrar.add_calls == 1, "callback registered once");
	expect(module.health().loaded, "health reports loaded");

	registrar.emit(FrontendEvent::FinishedLoading);
	expect(module.health().finished_loading, "finished loading recorded");
	registrar.emit(FrontendEvent::StreamingStarted);
	expect(module.health().streaming, "streaming started recorded");
	registrar.emit(FrontendEvent::StreamingStopped);
	expect(!module.health().streaming, "streaming stopped recorded");
	registrar.emit(FrontendEvent::Other);
	expect(module.health().loaded && module.health().finished_loading, "unrelated event has no effect");
	expect(logger.entries.size() == 1 && logger.entries[0].code == LogCode::ModuleLoaded,
	       "load emits only a structured event code");
}

void test_unload_is_idempotent_and_stale_callback_is_ignored()
{
	FakeRegistrar registrar;
	FakeLogger logger;
	ModuleLifecycle module(registrar, logger);
	module.load();
	registrar.emit(FrontendEvent::StreamingStarted);
	const auto stale_callback = registrar.last_callback;
	void *const stale_context = registrar.last_context;

	module.unload();
	module.unload();
	expect(registrar.remove_calls == 1, "callback removed exactly once");
	expect(!module.health().loaded && !module.health().streaming, "unload clears in-memory health");
	if (stale_callback != nullptr)
		stale_callback(FrontendEvent::FinishedLoading, stale_context);
	expect(!module.health().finished_loading, "callback after unload cannot mutate health");
	expect(logger.entries.size() == 2 && logger.entries[1].code == LogCode::ModuleUnloaded,
	       "unload emits one structured event code");
}

void test_failed_registration_rolls_back()
{
	FakeRegistrar registrar;
	FakeLogger logger;
	registrar.fail_registration = true;
	ModuleLifecycle module(registrar, logger);

	expect(!module.load(), "failed registration fails module load");
	expect(!module.health().loaded, "failed registration leaves module inactive");
	expect(registrar.remove_calls == 1 && !registrar.registered, "partial registration is rolled back");
	expect(logger.entries.size() == 1 && logger.entries[0].level == LogLevel::Error &&
	           logger.entries[0].code == LogCode::CallbackRegistrationFailed,
	       "failure log is a fixed code without external payload");
}

void test_reload_starts_with_clean_health()
{
	FakeRegistrar registrar;
	FakeLogger logger;
	ModuleLifecycle module(registrar, logger);
	module.load();
	registrar.emit(FrontendEvent::FinishedLoading);
	module.unload();
	expect(module.load(), "reload succeeds");
	const auto health = module.health();
	expect(health.loaded && !health.finished_loading && !health.streaming,
	       "reload starts with a clean in-memory state");
	expect(registrar.add_calls == 2, "reload registers callback once for new lifecycle");
}

void test_concurrent_load_is_idempotent()
{
	FakeRegistrar registrar;
	FakeLogger logger;
	ModuleLifecycle module(registrar, logger);
	std::array<std::thread, 8> callers;
	std::array<bool, 8> results{};
	for (std::size_t index = 0; index < callers.size(); ++index)
		callers[index] = std::thread([&module, &results, index] { results[index] = module.load(); });
	for (auto &caller : callers)
		caller.join();

	bool all_succeeded = true;
	for (const bool result : results)
		all_succeeded = all_succeeded && result;
	expect(all_succeeded, "concurrent load calls succeed");
	expect(registrar.add_calls == 1, "concurrent load registers callback exactly once");
	expect(logger.entries.size() == 1 && logger.entries[0].code == LogCode::ModuleLoaded,
	       "concurrent load emits one load log");
}

} // namespace

int main()
{
	test_load_and_events();
	test_unload_is_idempotent_and_stale_callback_is_ignored();
	test_failed_registration_rolls_back();
	test_reload_starts_with_clean_health();
	test_concurrent_load_is_idempotent();
	if (failures != 0)
		return 1;
	std::cout << "PASS 5 lifecycle scenarios\n";
	return 0;
}
