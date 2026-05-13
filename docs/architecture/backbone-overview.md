# NELI App Overview

## Purpose
NELI is a personal groceries and spending tracking application focused on managing household products, purchase receipts, and shopping-related insights.

The backend is being developed as a modular monolith with clear domain boundaries and an emphasis on incremental architectural evolution.

Current functionality focuses on:

tracking inventory products and stock levels
recording purchase receipts and receipt lines
preparing the foundation for future spending analysis and shopping workflows

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
- Raise restock warning domain events when stock crosses into low-stock state

Current scope:
- Inventory currently exposes only the endpoints needed for the initial MVP flow
- Full CRUD support is intentionally deferred until broader application workflows are implemented

Implementation notes:
- Product update logic already exists at the application/domain level
- Public API surface remains intentionally minimal during early development

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