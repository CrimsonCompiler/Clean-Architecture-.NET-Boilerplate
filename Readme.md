
# Clean Architecture .NET Boilerplate
> Developed by Tousif Tasrik

A production-ready, scalable backend API template built with **ASP.NET Core**, demonstrating **Clean Architecture** principles, **CQRS**, and **Domain-Driven Design (DDD)** concepts. This project serves as a robust foundation for building enterprise-grade applications.

## Features

- **Clean Architecture:** Strict separation of concerns across Domain, Application, Infrastructure, and API layers.
- **CQRS Pattern:** Implemented using **MediatR** to decouple read and write operations for better scalability.
- **Robust Validation:** Input validation and business rule enforcement using **FluentValidation**.
- **Automated Mapping:** Seamless object-to-object mapping using **AutoMapper**.
- **Generic Repository Pattern:** Reusable data access layer utilizing **Entity Framework Core**.
- **Interactive API Docs:** Modern API documentation powered by **Scalar**.
- **Database Agnostic:** Currently using SQLite for easy setup, but easily swappable to SQL Server/PostgreSQL.

## ️ Tech Stack

- **Framework:** .NET 8 / ASP.NET Core Web API
- **Language:** C#
- **ORM:** Entity Framework Core
- **Database:** SQLite
- **Libraries:** MediatR, FluentValidation, AutoMapper
- **API Documentation:** Scalar / OpenAPI

## Architecture Overview

The solution is structured into four distinct layers, following the Dependency Rule (dependencies point inwards):

```text
┌─────────────────────────────────────────┐
│           API Layer (Entry Point)       │
│  (Controllers, HTTP Handling, DI Setup) │
└──────────────┬──────────────────────────┘
               │ Depends on
               ▼
─────────────────────────────────────────┐
│      Infrastructure Layer (External)   │
│  (EF Core, DbContext, Repository Impl) │
└──────────────┬─────────────────────────┘
               │ Depends on
               ▼
┌─────────────────────────────────────────┐
│       Application Layer (Business)      │
│  (CQRS, MediatR, Validation, Mapping)   │
└──────────────┬──────────────────────────┘
               │ Depends on
               ▼
┌─────────────────────────────────────────┐
│         Domain Layer (Core Rules)       │
│  (Entities, Interfaces, Business Rules) │
└─────────────────────────────────────────┘
```

## Project Structure

```text
src/
├── CA.Api/                 # Presentation Layer (Controllers, Program.cs)
├── CA.Application/         # Application Layer (Commands, Queries, DTOs, Validators)
├── CA.Domain/              # Domain Layer (Entities, Interfaces)
└── CA.Infrastructure/      # Infrastructure Layer (DbContext, Repositories)
```

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher
- An IDE like Visual Studio, JetBrains Rider, or VS Code

### Installation & Running

1. **Clone the repository:**
   ```bash
   git clone https://github.com/CrimsonCompiler/Clean-Architecture-.NET-Boilerplate.git
   cd Clean-Architecture-.NET-Boilerplate
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Run the API:**
   ```bash
   dotnet run --project src/CA.Api/CA.Api.csproj
   ```

4. **Access API Documentation:**
   Open your browser and navigate to:
    - **Scalar UI:** `https://localhost:5001/scalar/v1` (or your configured port)
    - **Health Check:** `https://localhost:5001/`

##Testing the API

You can test the endpoints directly from the Scalar UI or using Postman/cURL.

**Create a new Product:**
```bash
curl -X POST "https://localhost:5001/api/Products" \
-H "Content-Type: application/json" \
-d '{
  "name": "Gaming Laptop",
  "description": "High performance gaming laptop",
  "price": 150000.00,
  "stockQuantity": 10
}'
```

**Get all Products:**
```bash
curl -X GET "https://localhost:5001/api/Products"
```

## Why this Project?

This boilerplate is designed to solve common enterprise development challenges:
- **Maintainability:** Changes in the database or UI do not affect the core business logic.
- **Testability:** Business logic in the Application layer can be easily unit tested without needing a real database.
- **Scalability:** The CQRS pattern allows read and write operations to be scaled independently.
