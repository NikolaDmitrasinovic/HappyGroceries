# NELI App Overview

## Purpose
NELI is a personal groceries and spending tracking application focused on managing household products, purchase receipts, and shopping-related insights.

The backend is being developed as a modular monolith with clear domain boundaries and an emphasis on incremental architectural evolution.

Current functionality focuses on:

- tracking inventory products and stock levels
- recording purchase receipts and receipt lines
- preparing the foundation for future spending analysis and shopping workflows

The project is intentionally evolving feature-by-feature to balance learning, maintainability, and real-world architectural practices.

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
- Create products
- Query products and low-stock products
- Track stock and threshold values
- Consume product stock
- Replenish product stock
- Raise restock warning domain events when stock crosses into low-stock state

Current scope:
- Inventory currently exposes only the endpoints needed for the initial MVP flow
- API surface is intentionally introduced incrementally through versioned endpoints

Implementation notes:
- Product update logic already exists at the application/domain level
- Stock operations are currently evolving through the v2 API
- Some earlier endpoints remain temporarily for backward compatibility during API evolution

### Receipt
Purpose:
- Records purchase receipts and receipt lines

Current capabilities:
- Open purchase receipt
- Add receipt lines
- Finalize receipt
- Store purchase snapshots such as product name, price, quantity
- Store optional ProductId references for future Inventory integration

Planned direction:
- Connect receipt lines to Inventory products
- Expand shopping and spending analysis workflows

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
- Receipt lines store purchase snapshots
- Domain currently generates IDs
- Strongly typed IDs are a future consideration

## Near-term roadmap
- Seq/logging improvements
- Integration tests
- Build/test CI pipeline improvements
- Continue API v2 evolution and endpoint cleanup
