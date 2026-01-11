# Local Database Setup (PostgreSQL + User Secrets)

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

The database is exposed on:

- localhost:5432

Credentials and database name are provided via environment variables.

## Environment Variables

The repository contains a .env.example file that documents the required
environment variables for running PostgreSQL locally.

Typical setup:
cp .env.example .env

Then adjust values in .env as needed.

⚠️ .env is ignored by Git and should never be committed.

Docker Compose reads these values when starting the PostgreSQL container.

User Secrets

Sensitive application configuration (such as the database connection string)
is stored using .NET User Secrets, keeping secrets out of source control.

Initialize user secrets (once per machine)
dotnet user-secrets init
Set the connection string
dotnet user-secrets set "ConnectionStrings:Default" \
"Host=localhost;Port=5432;Database=<db_name>;User id=<user>;Password=<password>;Include Error Detail=true"
The values should match those defined in .env.

## Entity Framework Core Migrations
Creating migrations (Package Manager Console)

Migrations are created using the Package Manager Console in Visual Studio.
Add-Migration <MigrationName> Data/Migrations`
  -Project <InfrastructureProject> `
  -StartupProject <ApiProject>
  
Example:
Add-Migration AddProductAuditFields `
  -Project MyApp.Infrastructure `
  -StartupProject MyApp.Api
Applying Migrations

✅ Manual database updates are usually not required

When running the API locally, migrations are applied automatically on startup
using:
app.UseMigration<InventoryDbContext>();

This ensures:

The database schema stays in sync

No manual Update-Database is needed during normal development

If necessary, migrations can still be applied manually:
Update-Database

Notes

- User Secrets are local-only and not used in CI or production
- Docker volumes persist database data between restarts
- This setup may evolve when production or cloud environments are introduced