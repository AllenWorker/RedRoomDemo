# RedRoomDemo

## Branch: 03-adapting-to-legacy-db

This branch demonstrates how a modern ASP.NET Core application adapts to an existing production database that cannot be redesigned.

It introduces:

- DTOs as API contracts
- Manual mapping in the service layer
- REST API endpoints for partners (`/api/orders`, `/api/orders/{id}`, `/api/payments/unmatched`)
- Business-oriented API shape independent from legacy schema details
- Dapper-based SQL flexibility for legacy data adaptation

It does **NOT** introduce yet:

- Entity Framework Core
- AutoMapper
- CQRS / MediatR / Unit of Work / Generic Repository
- Database schema redesign

The legacy database problems are intentionally preserved. For example, `PaymentTransactions` still does not have `OrderId`, so payment matching still relies on imperfect `TransactionReference` to `OrderNumber` matching.

AutoMapper is intentionally not introduced yet, so the mapping process stays explicit and easy to teach in a workshop setting.
