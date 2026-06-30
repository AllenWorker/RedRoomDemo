# Branch: 01b-manual-service

This branch demonstrates the first attempt to improve a messy controller-based ASP.NET Core MVC application.

It introduces:

- Service classes
- Repository classes
- Moving business logic out of controllers
- Moving SQL out of controllers

It intentionally does not introduce:

- Dependency Injection
- Interfaces
- Service registration
- Repository abstraction

The main lesson is that this branch is better than putting everything in controllers, but it still has tight coupling because controllers manually create services and services manually create repositories.

Expected dependency chain:

```text
Controller
  directly creates Service with new
    Service
      directly creates Repository with new
        Repository
          uses Dapper and SQLite
```

This is intentionally not ideal. The next branch, `02-service-layer-di`, will improve this by introducing interfaces and ASP.NET Core built-in Dependency Injection.
