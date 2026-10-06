# Quality Gates

## Active Documentation-Phase Gates

| Gate | Required evidence |
|---|---|
| Documentation | Requested artifacts exist, are readable, and have valid navigation. |
| Scope Compliance | Changes remain documentation/governance only and do not start later phases. |
| Knowledge Compliance | Facts, directions, assumptions, research needs, and decisions are distinguished. |
| Security Check | Changed content contains no secrets or unsafe credential guidance. |
| GitFlow | Work occurs on the task feature branch and follows the approved PR flow. |
| Prompt Traceability | The authorizing prompt is archived in the same task commit. |
| Code Review | The full task diff is reviewed by severity before merge. |
| Secret Scan | Staged content is scanned for likely credentials, tokens, keys, and passwords. |

Any critical finding, unresolved high finding, secret, conflict, out-of-scope change, failed required validation, incomplete push, or local/remote mismatch blocks merge.

## Future Implementation Gates

The following gates are **NOT APPLICABLE UNTIL IMPLEMENTATION** and cannot be reported as passing before their deterministic commands and artifacts exist:

- Build
- Unit Tests
- Integration Tests
- Architecture Validation
- Installer Validation
- OBS Compatibility
- Regression Tests
