# ADR-0001: Internal Mediator Abstraction for CQRS and Domain Events

## Status
Superseded by ADR-0003

## Date
2026-01-17

## Context
The solution uses a CQRS-style approach (commands/queries + handlers) and dispatches domain events (raised by aggregates) during EF Core `SaveChanges` via an interceptor.

The project currently uses (or considered using) MediatR directly. While MediatR is well-tested and feature-rich, most advanced features are not needed at this stage. Direct dependency on MediatR also couples application code to a third-party library and makes later changes harder.

## Decision
Introduce an internal mediator abstraction (in an internal project) that provides only the minimal capabilities needed:

- `Send<TResponse>(IRequest<TResponse>)` for commands/queries
- `Publish(INotification)` for domain events
- Handler resolution via built-in DI (no assembly scanning in v1)

The CQRS interfaces (`ICommand`, `ICommandHandler`, etc.) will depend on the internal abstractions so the application code does not depend on MediatR types.

## Rationale
- Keeps application code decoupled from third-party libraries
- Fits vertical-slice module structure (each module registers its handlers explicitly)
- Enables learning and experimentation while keeping scope controlled
- Allows later replacement with a MediatR adapter with minimal churn
- Supports current domain event dispatching model (EF interceptor → Publish)

## Scope (v1)
- Internal project (e.g., `Shared.Messaging` or `BuildingBlocks.Messaging`)
- Interfaces:
  - `IMediator`, `IRequest<TResponse>`, `IRequestHandler<TRequest,TResponse>`
  - `INotification`, `INotificationHandler<TNotification>`
  - `Unit`
- Implementation:
  - `DefaultMediator` resolving handlers from `IServiceProvider`
  - `Send` requires exactly one handler; throws if missing
  - `Publish` supports zero-to-many handlers; no handlers is not an error

## Out of Scope (for now)
- Pipeline behaviors (validation/logging/transactions)
- Handler scanning/auto-registration
- Streaming requests
- Performance optimizations (delegate caching, etc.)
- Concurrency/ordering controls for notification handlers
- Cross-process messaging (brokers)

## Consequences
### Positive
- Clear application boundary: use cases are executed via a single entry point
- Simpler dependency graph and easier long-term evolution
- Domain events remain decoupled from consumers
- Easy to expand gradually as requirements appear

### Negative / Risks
- Custom code must be maintained and tested
- Reflection-based invocation may be slower than optimized libraries (not relevant for current needs)
- Missing advanced features may require future work

## Alternatives Considered
1. Use MediatR directly everywhere
   - Pros: mature, feature-rich, well-tested
   - Cons: tight coupling, larger surface area than needed

2. Wrap MediatR immediately with an adapter (no custom mediator)
   - Pros: decouples app code, minimal effort
   - Cons: less learning; still carries MediatR assumptions everywhere

3. Directly invoke handlers/services without a mediator
   - Pros: simplest code path
   - Cons: cross-cutting concerns and orchestration spread across the codebase; weaker boundaries

## Follow-up / Revisit
Revisit this ADR if any of the following becomes true:
- We need pipeline behaviors for validation/logging/transactions
- Reflection overhead becomes measurable and problematic
- We want automatic handler registration/scanning
- We decide to standardize on MediatR (swap via adapter)
