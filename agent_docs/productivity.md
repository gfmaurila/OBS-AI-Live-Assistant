# Productivity and Tooling

## Current Environment

- .NET SDK 10 is available.
- Visual Studio Community 2026, MSVC, CMake, and Windows SDKs are available.
- OBS Studio 32.1.2 x64 is installed for development validation.
- `rtk` must prefix shell commands in this environment.
- Git is unavailable: `GIT_SETUP: BLOCKED`.

## Pending Tooling

- Product restore, build, test, formatting, static analysis, and native build commands remain undefined until the solution and toolchain tasks are approved.
- GitFlow branches, commits, pushes, and pull requests must not be simulated while Git is unavailable.
- The Claude pre-commit hook remains disabled until real deterministic commands exist.

## Working Practices

- Use repository artifacts for handoffs between Requirements, Architecture, Tech Lead, Development, Testing, Review, Security, DevOps, and Documentation roles.
- Use a task-specific branch only after Git is installed and the relevant task is approved.
- Keep tool output focused and retain evidence for quality gates.
