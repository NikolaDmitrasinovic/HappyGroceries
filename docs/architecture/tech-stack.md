# Tech Stack

This project is built as a **modular monolith** with a web frontend, designed to evolve incrementally without premature optimization.

---

## Backend

- **Platform**: .NET (ASP.NET Core, targeting .NET 8+)
- **Language**: C#
- **Architecture**:
  - Modular monolith
  - Layered structure:
    - Api
    - Domain
    - Application
    - Infrastructure
  - Domain-centric design with explicit boundaries per feature
- **API Style**:
  - REST
  - OpenAPI (Swagger) as the contract source of truth

---

## Database

- **Database**: PostgreSQL
- **Data Access**: Entity Framework Core
- **Migrations**: EF Core migrations
- **Local Development**: PostgreSQL running in Docker

### Rationale
- Open-source and cloud-agnostic
- Strong Docker support
- Common industry choice with good learning value

---

## Frontend

- **Framework**: React
- **Build Tooling**: Vite
- **Language**: TypeScript
- **API Integration**:
  - Generated client from OpenAPI specification

### Rationale
- Widely used in industry
- Aligns with current professional experience
- Large ecosystem and long-term viability

---

## Containerization & Local Development

- **Containerization**: Docker
- **Orchestration**: Docker Compose
- **Local Services**:
  - API
  - PostgreSQL
  - Frontend (added later)

---

## Authentication & Authorization

- **Status**: Not implemented initially
- **Planned Approach**: OpenID Connect (OIDC)

### Rationale
- Avoid early complexity
- Introduce only when real user scenarios exist

---

## Mobile Support (Future)

- **Planned Approach**:
  - React Native (via Expo)
  - Shared API client with web frontend

### Rationale
- Reuse React knowledge
- Keep backend unchanged

---

## Guiding Principles

- Start simple, evolve incrementally
- Avoid premature abstraction
- Prefer explicit boundaries over shared code
- Optimize for learning, clarity, and maintainability
