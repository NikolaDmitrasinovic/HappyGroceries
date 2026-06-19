# NELI™️

NELI™️ is a small and practical grocery tracking application focused on inventory management, purchase tracking, and restock workflows.

Using it, you will be able to track groceries, monitor stock levels, and manage shopping and receipt workflows.

## Current status
Early-stage modular monolith focused on backend development.

Implemented modules:
- Inventory
- Receipt

## Features
Current capabilities:
- Product inventory tracking
- Stock thresholds and low-stock warnings
- Receipt and receipt line tracking
- REST API with versioning
- PostgreSQL + EF Core migrations
- Dockerized local database setup

## Architecture
- Modular monolith
- ASP.NET Core
- EF Core + PostgreSQL
- Domain-oriented design
- Internal mediator/pipeline behaviors

Link to:
- `docs/architecture/tech-stack.md`
- `docs/architecture/backend-overview.md`

## Running locally

### Requirements
- .NET SDK
- Docker

### Local setup
The project uses Docker Compose for local PostgreSQL development.
The following services are started through Docker Compose:
- PostgreSQL
- Seq (structured log viewer)
Database migrations are applied automatically when the API starts.

Seq is available at: http://localhost:5341

### Visual Studio
The recommended local setup is using Visual Studio multi-project startup.

Startup order:
- `docker-compose`
- API project
Running the solution starts PostgreSQL in Docker and then launches the API, which automatically applies pending EF Core migrations during startup.

### Manual startup
Start PostgreSQL:
`docker compose up -d`

Run the API:
`dotnet run --project apps/api/Bootstrapper/Api`

### Health check
Once running, the API health endpoint is available at:
`/health`

## Project goals
### The project emphasizes:
- modular architecture and explicit domain boundaries
- pragmatic Domain-Driven Design concepts
- incremental evolution instead of premature optimization
- clean and maintainable development practices
- learning through solving real implementation problems

### Future goals include:
- spending insights and purchase analysis
- tighter integration between inventory and receipts
- improved shopping and restock workflows
- frontend and mobile clients
- event-driven communication between modules where appropriate

The project intentionally evolves feature-by-feature to allow architecture and design decisions to emerge from real requirements rather than theoretical abstraction.

## Status / Disclaimer
Work in progress.
Architecture and APIs may evolve significantly.

The NELI™️ name and logos are the property of Nikola Dmitrašinović.
