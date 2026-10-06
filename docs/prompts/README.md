# Operational Prompt History

`docs/prompts/history/` contains the chronological history of operational
prompts used to configure, document, and evolve this project. The archive is
versioned for traceability and auditing.

Historical prompts are inputs and execution records. They are not
automatically approved requirements or architectural decisions. Canonical
requirements are produced and approved through the Requirements process.
Canonical architectural decisions are maintained in the Architecture
documentation and Architecture Decision Records (ADRs). Newer canonical
documentation takes precedence over historical prompts.

Prompt files must never contain secrets, credentials, passwords, access or
refresh tokens, private keys, connection strings with secrets, or API keys. If
a prompt contains sensitive data, stop and arrange safe redaction or handling
before archiving it in Git.

For every operational prompt that produces a repository change:

1. Complete and validate the authorized change.
2. Archive the prompt in `docs/prompts/history/` using the next available
   sequential `prompt<number>.md` filename.
3. Run the secret check over the prompt and every changed file.
4. Include the archived prompt in the same task commit as the resulting
   changes.
5. Push that commit to the branch used for the task.

Never overwrite an existing historical prompt.
