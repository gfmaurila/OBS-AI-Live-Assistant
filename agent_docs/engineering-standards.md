# Engineering Standards

- Prefer verifiable correctness over implementation speed.
- Use strict type safety where supported.
- Keep responsibilities small, names explicit, dependencies visible, and modules testable.
- Apply SOLID, dependency inversion, and separation of concerns where they reduce actual coupling.
- Do not add patterns, layers, services, brokers, or infrastructure solely for anticipated future use.
- Fix bugs by first creating a failing regression test when technically feasible.
- Use unit, integration, contract, database, security, OBS integration, and installer tests according to risk.
- Validate failure cases, including unavailable providers, invalid credentials, disconnected chat, closed OBS, locked/corrupt database, rate limits, timeouts, and unavailable networks.
- Research current official documentation before non-trivial library, platform, OBS, YouTube, Windows security, or installer decisions.
- Explain non-obvious workarounds and trade-offs; do not comment obvious mechanics.
- Put deterministic formatting and analysis rules in tools, not prose.
- Run documented quality gates before considering a task complete.
- Update requirements, ADRs, diagrams, security documentation, and runbooks when changes affect their contracts.
