# ADR-0002: Define query rules as EF-translatable expressions

## Status
Accepted

## Date
2026-02-22

## Context
We need to reuse query rules (e.g., "low stock" = `Stock <= Threshold`) across handlers without duplicating logic in multiple EF Core queries.

The domain currently exposes computed properties (e.g., `IsLowStock`) which EF Core cannot translate into SQL, so querying on these requires duplicating the rule in `.Where(...)`.

We want a solution that:
- is simple to implement during early project stages
- keeps query code readable
- remains compatible with EF Core translation
- allows future evolution toward more composable query patterns if needed

## Decision
We will define reusable query rules as `Expression<Func<TEntity, bool>>` predicates (EF-translatable expressions) and use them in EF Core queries.

Example:
- `ProductPredicates.IsLowStockExpression = p => p.Stock <= p.Threshold`

Handlers will use these expressions in `.Where(...)` to avoid repeating business rules in query code.

## Rationale
This approach:
- eliminates duplication of core query rules while remaining EF Core compatible
- keeps the implementation minimal (no new abstractions/framework)
- is easy to locate and maintain (single source of truth)
- fits the current project stage (MVP+), where query complexity is still low

## Scope
- Define a small set of reusable EF-translatable expressions for common query rules.
- Use them in query handlers instead of repeating the conditions inline.

## Out of Scope
- Introducing a Specification pattern abstraction (e.g., `ISpecification<T>`)
- Implementing composition helpers (`And`, `Or`, `Not`) for query rules
- Dynamic filtering/search/pagination infrastructure

## Consequences
### Positive
- Single source of truth for query rules like low-stock detection
- Cleaner handlers and less duplicated logic
- No additional framework code to maintain
- Easy to evolve gradually

### Negative / Risks
- Expression predicates can become numerous if many distinct query rules appear
- Without a Specification abstraction, composing complex predicates may become awkward later

## Alternatives Considered
1. Duplicate the rule in each query (`Stock <= Threshold`)
   - Pros: simplest in the moment
   - Cons: duplication grows quickly and risks inconsistencies

2. Add Specification pattern now
   - Pros: composability and stronger structure for complex querying
   - Cons: additional abstractions and complexity before it is needed

3. Query in memory using computed properties (`.ToListAsync()` then `.Where(p => p.IsLowStock)`)
   - Pros: reuses domain property
   - Cons: does not scale; pulls unnecessary data; poor performance

## Follow-up / Revisit
Revisit this decision if:
- we introduce filtering/search across many dimensions (status, category, owner, etc.)
- we need frequent composition of query rules (AND/OR/NOT)
- the number of shared predicates grows enough to hurt discoverability

At that point, consider introducing a small Specification abstraction and optional composition helpers.