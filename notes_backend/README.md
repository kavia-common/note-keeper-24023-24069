# Notes Backend (Ocean Professional)

A modern .NET 8 RESTful API to create, read, update, and delete notes. The API follows a clean, layered architecture and exposes OpenAPI docs at /docs.

## Tech
- .NET 8
- ASP.NET Core Web API
- EF Core (SQLite for preview, PostgreSQL via env var)
- NSwag for Swagger UI

## Run
- Dev/preview: falls back to SQLite automatically.
  - dotnet run
  - Visit http://localhost:3001/docs

- With PostgreSQL (preferred in integrated env with notes_database):
  - Set NOTES_DB_CONNECTION env var (see .env.example)
  - dotnet run

## Endpoints
- GET /api/notes
- GET /api/notes/{id}
- POST /api/notes
- PUT /api/notes/{id}
- DELETE /api/notes/{id}
- GET /api/health
- GET / (root health)

## Request/Response Models
See the OpenAPI documentation in /docs for details.

## Migrations (optional)
If modifying the data model:
- dotnet ef migrations add Init
- dotnet ef database update

EF tooling uses SQLite by default via DesignTimeDbContextFactory.

## Env Vars
See .env.example. If NOTES_DB_CONNECTION is unset, SQLite file notes.db is used.

## Style
Ocean Professional theme: blue (#2563EB) and amber (#F59E0B) accents reflected in API metadata and clean structure.
