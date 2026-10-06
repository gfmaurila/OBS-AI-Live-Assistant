# Hooks — Deferred Deterministic Enforcement

No hook is enabled during engineering bootstrap because the project does not yet have approved restore, build, test, lint, formatting, type-check, or native-build commands.

After solution and tooling bootstrap, define and verify the real commands in `AGENTS.md`, then configure a Claude Code pre-commit hook that runs the approved deterministic checks. Do not leave placeholder commands in an active `.claude/settings.json` file.

Enabling hooks requires a dedicated approved task and must not be used to simulate missing Git functionality.
