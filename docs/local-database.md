# Local Database Setup (PostgreSQL + User Secrets)

## TL;DR

- PostgreSQL runs locally in Docker
- Credentials are defined via `.env` (see `.env.example`)
- The API uses **.NET User Secrets** for the connection string
- EF Core migrations are applied **automatically on API startup**
- New migrations are created using **Package Manager Console**

---

## Overview

For local development, the API uses:

- **PostgreSQL** running in a Docker container
- **.NET User Secrets** for sensitive configuration
- **Automatic EF Core migrations** on API startup

This setup is intended **for local development only**.

---

## PostgreSQL (Docker)

PostgreSQL is started using Docker Compose.

```bash
docker compose up -d
```
The database is exposed on:

- localhost:5432

Credentials and database name are provided via environment variables.

---

## Environment Variables

The repository contains a .env.example file that documents the required
environment variables for running PostgreSQL locally.

Typical setup:
```bash
cp .env.example .env
```
Then adjust values in .env as needed.

⚠️ .env is ignored by Git and should never be committed.

Docker Compose reads these values when starting the PostgreSQL container.

---

## User Secrets

Sensitive application configuration (such as the database connection string)
is stored using .NET User Secrets, keeping secrets out of source control.

Initialize user secrets (once per machine)

```bash
dotnet user-secrets init
```

Set the connection string
```bash
dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Port=5432;Database=<db_name>;User Id=<user>;Password=<password>;Include Error Detail=true"
```
The values should match those defined in `.env`.

---

## Entity Framework Core Migrations
Creating migrations (Package Manager Console)

Migrations are created using the Package Manager Console in Visual Studio.
```powershell
Add-Migration <MigrationName> -OutputDir Data/Migrations -Project <InfrastructureProject> -StartupProject <ApiProject>
```

---

## Applying Migrations

✅ Manual database updates are usually not required

When running the API locally, migrations are applied automatically on startup
using:
```csharp
app.UseMigration<InventoryDbContext>();
```
This ensures:
- The database schema stays in sync
- No manual Update-Database is needed during normal development

If necessary, migrations can still be applied manually:
```powershell
Update-Database
```
---

Notes
- User Secrets are local-only and not used in CI or production
- Docker volumes persist database data between restarts
- This setup may evolve when production or cloud environments are introduced
