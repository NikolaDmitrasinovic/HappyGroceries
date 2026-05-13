# NELI App Overview

## Purpose
What problem the app solves and what kind of system it is becoming.

## Current architecture
- Modular monolith
- ASP.NET Core API
- PostgreSQL
- EF Core
- Internal mediator
- Domain-focused modules

## Modules

### Inventory
Purpose:
- Tracks products and stock levels

Current capabilities:
- Create/update products
- Track stock and thresholds
- Identify low-stock products
- Raise restock warning domain events

### Receipt
Purpose:
- Records purchase receipts and receipt lines

Current capabilities:
- Open purchase receipt
- Add receipt lines
- Finalize receipt
- Store purchase snapshots such as product name, price, quantity

Planned direction:
- Add ProductId placeholder
- Later connect receipt lines to Inventory products

## Cross-cutting concerns
- Exception handling with ProblemDetails
- Validation pipeline behavior
- Logging pipeline behavior
- Pagination helpers
- API versioning
- EF migrations
- Dockerized PostgreSQL

## Current boundaries / decisions
- Receipt is not yet integrated with Inventory
- Receipt lines store snapshots
- Domain currently generates IDs
- Strongly typed IDs are a future consideration

## Near-term roadmap
- ProductId on ReceiptLine
- Seq/logging improvements
- Integration tests
- Build/test CI pipeline improvements