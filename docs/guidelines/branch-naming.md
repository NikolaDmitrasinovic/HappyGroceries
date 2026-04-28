# Branch naming convention

This repository uses a strict branch naming convention to keep pull requests
consistent, readable, and easy to reason about.

Branch names are validated automatically via GitHub Actions.
Pull requests with invalid branch names cannot be merged.

Dependabot-created branches are exempt from these rules.

---

## Format

### Components

- **`type`**  
  Describes the nature of the change.

- **`SCOPE`**  
  Indicates the affected area of the system.

- **`issue`** _(optional)_  
  Issue number related to the change.  
  Required for certain branch types.

- **`short-description`**  
  A short, kebab-case description of the change.

---

## Allowed types

| Type       | Description                      |
| ---------- | -------------------------------- |
| `feat`     | New functionality                |
| `fix`      | Bug fix                          |
| `refactor` | Behavior-preserving refactor     |
| `chore`    | Tooling / repository maintenance |
| `docs`     | Documentation-only changes       |
| `test`     | Tests only                       |
| `ci`       | CI / GitHub Actions              |
| `hotfix`   | Urgent fixes on released code    |

---

## Allowed scopes

| Scope   | Meaning         |
| ------- | --------------- |
| `BE`    | Backend         |
| `FE`    | Frontend        |
| `CI`    | CI / automation |
| `INFRA` | Infrastructure  |
| `DOCS`  | Documentation   |

---

## Issue number rules

An issue number is **required** for the following branch types:

- `feat`
- `fix`
- `chore`

Other types may omit the issue number.

---

## Scope restrictions

- `docs` branches **must** use the `DOCS` scope
- `ci` branches **must** use the `CI` scope
- `BE` and `FE` scopes **cannot** be used with `docs` or `ci` types

---

## Examples

### With issue number

feat/BE-12_run-db-via-docker
fix/FE-31_fix-capacity-form-validation
chore/CI-77_add-issue-and-pr-templates

### Without issue number

hotfix/CI_patch-runner-timeout
docs/DOCS_add-branch-naming-doc
ci/CI_add-branch-name-validation

---

## Invalid examples

feat/BE_run-db-via-docker # missing issue number
docs/BE_update-readme # wrong scope
ci/FE_add-github-action # wrong scope
feature/add-stuff # unsupported type

---

## Why this exists

This convention helps:

- keep pull requests consistent
- link code changes to issues when appropriate
- avoid ambiguity about the scope of changes
- automate validation via CI
