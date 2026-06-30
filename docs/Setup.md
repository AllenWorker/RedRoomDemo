# Setup Guide

This workshop uses a branch-based repository. Start from a workshop branch, not from `master`.

## Required

- Laptop
- Git
- Visual Studio 2026 Community **or** Visual Studio 2022 Community
- .NET SDK compatible with the workshop project
- ASP.NET and Web Development workload

## Optional IDEs

You can also use VS Code or Rider if you can run:

```bash
dotnet --version
dotnet restore
dotnet run
```

## Quick Start

```bash
git clone <repository-url>
cd <repository-folder>
git checkout 01-messy-controller
dotnet restore
dotnet run
```

Replace `<repository-url>` and `<repository-folder>` with your own values.
