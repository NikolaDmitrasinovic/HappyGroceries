# Pull Request Naming Convention

## Why this exists

PR titles become commit messages on `main` (especially when using squash merges).
Clear, consistent titles make the project history easy to scan and understand later.

Labels are useful in GitHub UI, but they do not appear in git history.

---

## PR Title Format

Use the following format for all pull requests:
```scss
type(scope): short, imperative summary
```

### Examples

- feat(auditing): populate audit fields via SaveChanges interceptor
- fix(api): return 404 when product is not found
- docs(github): add bug report issue template
- refactor(domain): extract Money value object
- chore(ci): enforce PR title format

---

## Types

Use the type that best describes the primary purpose of the change:

|Type     |	When to use it                             |
|feat	  |New feature or new behavior                 |
|fix	  |Bug fix                                     |
|docs	  |Documentation only                          |
|refactor |	Code change with no behavior change        |
|test	  |Tests only                                  |
|chore	  |Tooling, CI, dependencies, repo maintenance |
|perf	  |Performance improvements                    |

If unsure, prefer feat, fix, or refactor.

---

## Scope

The scope indicates which part of the system is affected.

Examples:
- api
- domain
- auditing
- inventory, spending, etc.
- github
- ci
- db

Use one scope only. If multiple areas are touched, pick the most important one.

---

## Style rules
- Use imperative mood: `add`, `fix`, `remove`, `populate`, not `added` or `adding`
- Keep it short and specific
- Avoid implementation noise in the title
- One PR = one main concern

--- 

## Examples of good vs bad

❌ `Add stuff for auditing`
✅ `feat(auditing): populate audit fields on save`

❌ `Bugfix`
✅ `fix(domain): prevent negative stock quantity`

❌ `Update PR template`
✅ `docs(github): update pull request template`

---

# Pull Request Template

Your existing template is already good. I’ll only make very light adjustments to align with your goals and keep it focused.

Create or update:
```bash
.github/pull_request_template.md
```

```md
## Description

<!-- What changed and why (1–3 sentences). -->

## Key changes

- 

## Notes / Edge cases

<!-- Anything reviewers should pay attention to (optional). -->

## Checklist

- [ ] I kept the change focused (no unrelated refactors)
- [ ] Tests added/updated (or N/A with reason)
- [ ] Docs updated (or N/A)
- [ ] No secrets or environment-specific overrides committed
```

### Why this template works well
- Short enough to never feel annoying
- Encourages focus and discipline
- Pairs naturally with structured PR titles
- Scales if collaborators join later
- Doesn’t force “fake” content when not needed