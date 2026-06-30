# Branch Guide

## `01-messy-controller`

### Problem
The website works, but controllers contain too many responsibilities.

### Look at
- Controllers
- SQL inside controller actions
- Business logic inside controller actions

### Concept
A working application can still be difficult to maintain.

### Intentionally not solved yet
- Service Layer
- Repository Layer
- Dependency Injection-based separation

---

## `02-service-layer-di`

### Problem
The project needs to support new features and multiple developers.

### Look at
- Services
- Repositories
- Interfaces
- `Program.cs` dependency registration

### Concept
Separate responsibilities and use Dependency Injection to connect layers.

### Intentionally not solved yet
- Legacy database adaptation for external API contracts
- DTO-focused API design

---

## `03-adapting-to-legacy-db`

### Problem
The database cannot be changed, but new business requirements still arrive.

### Look at
- DTOs
- Manual mapping
- Dapper queries
- API endpoints

### Concept
The application often needs to adapt to the database, not the other way around.

### Intentionally not solved yet
- AutoMapper
- Entity Framework Core
- Advanced architecture patterns
