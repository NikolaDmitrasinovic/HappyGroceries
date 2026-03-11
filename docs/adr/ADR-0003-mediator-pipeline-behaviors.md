# ADR-0003: Mediator Pipeline Behaviors for Cross-Cutting Concerns

## Status
Accepted

## Date
2026-03-05

## Context
The internal mediator abstraction introduced in ADR-0001 successfully decoupled
application code from MediatR and provided request dispatching and domain event
publication.

As the project progressed beyond MVP, additional cross-cutting concerns emerged,
including:

- request validation
- request logging
- potential future concerns such as authorization, caching, and metrics

Implementing these concerns directly in handlers or controllers would duplicate
logic and violate separation of concerns.

The mediator already serves as the central execution boundary for application use cases (`Send`). This makes it a natural place to introduce middleware-like behavior around request execution.

However, the original mediator design in ADR-0001 only supported direct handler invocation and did not provide a mechanism for extending execution flow.

## Decision
Extend the internal mediator implementation to support **pipeline behaviors**.

Pipeline behaviors act as middleware around request handling and are executed
before and/or after the request handler.

The following abstractions are introduced:

- `RequestHandlerDelegate<TResponse>`
- `IPipelineBehavior<TRequest, TResponse>`

Mediator execution flow becomes:
Send(request) → Behavior 1 → Behavior 2 → Handler


Behaviors are resolved from dependency injection and composed dynamically
at runtime.

Execution order is determined by **DI registration order**.

## Rationale

Pipeline behaviors provide a consistent mechanism for implementing cross-cutting concerns without modifying individual handlers.

Benefits of this approach include:

 - separation of concerns between application logic and infrastructure
 - reusable implementations for validation and logging
 - extensibility for additional behaviors in the future
 - alignment with common CQRS/mediator patterns used in .NET ecosystems

This approach preserves the project’s goal of maintaining an internal mediator abstraction while still supporting patterns commonly associated with MediatR.

The decision is appropriate at this stage because the project has passed MVP and now requires stronger infrastructure guarantees such as validation and observability.

## Scope

This decision includes:

 - introduction of IPipelineBehavior<TRequest, TResponse>
 - introduction of RequestHandlerDelegate<TResponse>
 - updates to the mediator Send method to compose behaviors
 - execution ordering determined by DI registration order

The request logging behavior records execution time for each request handled through the mediator pipeline.

To improve observability, the logging behavior distinguishes slow requests using a lightweight threshold.

Requests that exceed the configured threshold are logged with Warning level, while normal successful requests continue to be logged with Information level.

The current slow-request threshold is: 300 milliseconds

### Rationale for the threshold

The threshold is intended to highlight requests that are noticeably slower than typical application operations while avoiding excessive log noise.

In the current architecture, most mediator requests consist of:

 - request validation
 - domain logic
 - database access
 - domain event dispatch

Typical execution times for such operations are generally below 150–200 ms under normal conditions.

A threshold of 300 ms provides a practical signal that:

 - the request performed more work than usual
 - a database query may be inefficient
 - external dependencies may be slowing down execution
 - performance regressions may be emerging

At the same time, it avoids flagging normal requests as slow.

### Implementation constraints

The threshold is intentionally implemented as a simple constant inside the logging behavior to keep the infrastructure lightweight.

This avoids introducing configuration complexity at the current stage of the project.

### Future considerations

The slow-request threshold may be revisited in the future if:

 - application performance characteristics change
 - configuration-based thresholds become desirable
 - more advanced telemetry or performance monitoring is introduced

## Out of Scope

This ADR does not introduce:

 - pipeline behaviors for Publish (domain events)
 - automatic assembly scanning for behaviors or handlers
 - performance optimizations such as delegate caching
 - validation or logging implementations themselves

These concerns are handled through separate issues.

## Consequences

### Positive

 - Enables consistent cross-cutting behavior across all requests
 - Keeps handlers focused on business logic
 - Simplifies implementation of validation and logging
 - Improves extensibility of the mediator infrastructure
 - Maintains independence from third-party mediator libraries

### Negative / Risks

 - Increased complexity in the mediator implementation
 - Reflection-based invocation may introduce minor overhead
 - Behavior execution order must be carefully managed via DI registration

## Alternatives Considered

### 1. Implement validation and logging directly in handlers

Rejected because it duplicates logic and mixes concerns.

### 2. Use MediatR pipeline behaviors

Rejected because the project intentionally avoids a direct dependency on MediatR.

### 3. Controller-level validation and logging

Rejected because it couples cross-cutting concerns to the web layer.

## Follow-up / Revisit

This decision should be revisited if:

 - mediator performance becomes a bottleneck
 - additional infrastructure behaviors (authorization, caching, metrics) are introduced
 - the project adopts an external mediator implementation
 - domain event dispatching requires similar pipeline behavior support
 