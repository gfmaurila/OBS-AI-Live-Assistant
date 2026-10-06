# GitFlow

## Branches

- Permanent: `main`, `develop`, `hml`.
- Task work: `feature/task-*` created from synchronized `develop`.
- Release preparation: `release/x.y.z` when separately authorized.

No task works directly on `main`, `develop`, or `hml`.

## Flow

```text
feature/task-*
-> Pull Request
-> develop
-> Pull Request / promotion gate
-> hml
-> release/x.y.z
-> Pull Request
-> main
```

## Automation Boundary

A `feature/task-*` pull request may be merged automatically into `develop` after validation, review, security checks, remote synchronization, and all required quality gates pass. Critical or unresolved high-severity findings block the merge.

Promotion from `develop` to `hml` is not automatic per task. `main` remains protected by its release and approval gates. No force push or alternate-branch push may be used to bypass a failure.

After a successful feature merge, synchronize and validate local `develop`, then delete the feature branch locally and remotely only when safe.
