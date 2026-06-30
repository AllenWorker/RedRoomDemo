# Branch: 01-messy-controller

This branch is the starting point for the ASP.NET Core MVC workshop.

It demonstrates a working but hard-to-maintain legacy-style application where controllers contain too many responsibilities.

In this branch, controllers directly handle:

- HTTP request handling
- Business decisions
- Dapper SQL queries
- SQLite connection creation
- View model assembly
- Payment matching hints

This is intentional. The goal is to show a common real-world starting point before refactoring begins.

The application works, but the design has clear problems:

- Controllers are large and difficult to read.
- SQL is mixed with HTTP controller actions.
- Business logic is difficult to test separately.
- Data access code is duplicated across controllers.
- Changing database access or matching rules can require editing controller code.

This branch does not introduce:

- Service classes
- Repository classes
- Dependency Injection for application services
- Interfaces
- Entity Framework Core
- AutoMapper
- Clean Architecture, CQRS, MediatR, Unit of Work, or Generic Repository

Expected dependency shape:

```text
Controller
  directly opens SQLite connection
  directly runs Dapper SQL
  directly performs business logic
  directly returns MVC views
```

The next branch, `01b-manual-service`, improves this by moving business logic into services and SQL into repositories, while still intentionally creating dependencies manually with `new`.
