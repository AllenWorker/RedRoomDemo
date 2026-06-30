# Real-World ASP.NET Core Workshop

**Understanding how ASP.NET Core applications evolve from messy controllers to maintainable architecture under real business constraints.**

This repository is designed as a **branch-based workshop**.

- The `master` branch is a documentation landing page.
- Students should **not** start coding from `master`.
- Please switch to the workshop branches below.

## Start with the workshop branches

```bash
git checkout 01-messy-controller
git checkout 01b-manual-service
git checkout 02-service-layer-di
git checkout 03-adapting-to-legacy-db
```

## Branch Guide

| Branch | Purpose |
|---|---|
| `01-messy-controller` | Shows a working but hard-to-maintain legacy-style MVC application where controllers contain SQL, business logic, and data access. |
| `01b-manual-service` | Shows a first refactoring attempt where controllers call service classes and services call repositories, but dependencies are still manually created with `new`. |
| `02-service-layer-di` | Introduces Service Layer, Repository Layer, and ASP.NET Core built-in Dependency Injection. |
| `03-adapting-to-legacy-db` | Demonstrates adapting the application to a legacy database without changing the schema, using DTOs, manual mapping, and Dapper. |

## Core Message

Architecture is not about making a project look complicated. It is about making future changes safer, faster, and easier for teams to work on.

## Workshop Materials

- [Setup Guide](docs/Setup.md)
- [Workshop Outline](docs/Workshop-Outline.md)
- [Branch Guide](docs/Branch-Guide.md)
- [Exercise 01](exercises/Exercise-01.md)
- [Exercise 02](exercises/Exercise-02.md)
- [Exercise 03](exercises/Exercise-03.md)
