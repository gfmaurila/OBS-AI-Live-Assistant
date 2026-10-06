# Execution Plan

## Project Stage Order

```text
KIT IA DEV
-> DOCUMENTATION BASELINE
-> ADVANCED SKILLS (when the official package is available)
-> KNOWLEDGE QUALITY GATE
-> REQUIREMENTS
-> RESEARCH
-> ARCHITECTURE
-> ADR
-> BACKLOG
-> TASKS
-> IMPLEMENTATION
```

The more detailed mandatory stage sequence in `AGENTS.md` remains authoritative. This view highlights the immediate project progression. The current stop condition is the unavailable Advanced Skills package and the not-yet-passed Knowledge Quality Gate.

## Task Completion Flow

```text
TASK READY
-> feature/task-*
-> execution
-> validation
-> code review
-> required fixes
-> archive prompt
-> security check
-> commit
-> push
-> pull request
-> PR validation
-> merge to develop
-> post-merge validation
-> feature cleanup
```

Automatic completion ends at `develop` and applies only when every required gate passes. It never authorizes promotion to `hml` or `main`.
