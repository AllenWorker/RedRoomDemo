# Workshop Outline (3 Hours)

## 1) Introduction

- Why real-world projects become difficult to maintain
- Why "it works" is not the same as "it is easy to change"

## 2) Branch 01: `01-messy-controller`

- The application works, but the controller is doing too much
- Controllers include SQL, data access, and business logic
- New requirement: unmatched successful payments

## 3) Branch 02: `02-service-layer-di`

- Introduce Service Layer, Repository Layer, and Dependency Injection
- Why DI and N-Tier help team development
- How teams reduce merge conflicts by separating responsibilities

## 4) Branch 03: `03-adapting-to-legacy-db`

- The database cannot be redesigned
- DTOs and manual mapping as stable application/API contracts
- Dapper as a practical tool for legacy databases

## 5) Summary and Q&A

- What changed across branches
- Why architecture decisions are business decisions
- Open discussion and questions
