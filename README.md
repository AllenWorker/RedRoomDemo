# RedRoomDemo

## Branch: 02-service-layer-di

This branch demonstrates the first refactoring step from a messy controller-based legacy application into a more maintainable ASP.NET Core structure.

It introduces:

- Service Layer
- Repository Layer
- ASP.NET Core built-in Dependency Injection
- Interface-based development
- Separation of HTTP handling, business logic, and data access

It does **NOT** introduce yet:

- Entity Framework Core
- AutoMapper
- Full Clean Architecture
- Database schema redesign

The legacy database problems are intentionally preserved for workshop teaching purposes. For example, `PaymentTransactions` still does not have `OrderId`, so payment matching still relies on imperfect `TransactionReference` to `OrderNumber` matching.
