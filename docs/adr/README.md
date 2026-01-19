# Architecture Decision Records (ADR)

This directory contains **Architecture Decision Records (ADRs)** for this project.

ADRs document **significant architectural decisions** made during development,
along with their context and rationale.

The goal is to:
- capture *why* decisions were made (not just *what* was done)
- make trade-offs explicit
- help future contributors (and future me) understand the architecture
- avoid re-litigating the same decisions repeatedly

---

## What should be an ADR?

Create an ADR when a decision:
- affects the overall architecture or core abstractions
- introduces or replaces a major pattern, dependency, or concept
- has long-term impact on maintainability or extensibility
- is not obvious from the code alone

Examples:
- Introducing an internal mediator abstraction
- Choosing CQRS / modular structure
- Deciding how domain events are dispatched
- Selecting persistence or messaging strategies

Non-examples:
- Small refactors
- Naming changes
- Minor implementation details

---

## Naming & numbering

- ADRs are numbered sequentially: `ADR-0001`, `ADR-0002`, etc.
- Numbers are **never reused**
- Filenames follow this format:

```text
ADR-0001-short-title.md
