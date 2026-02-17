# Golden Gems Backend

A C# ASP.NET Core 10.0 backend service for the Golden Gems application.

## Project Overview

- **Framework**: ASP.NET Core 10.0
- **Database**: PostgreSQL (via Npgsql.EntityFrameworkCore)
- **Authentication**: JWT Bearer (JSON Web Tokens)
- **API Documentation**: Swagger/OpenAPI (via Swashbuckle.AspNetCore)
- **ORM**: Entity Framework Core 10.0.2

## Key Features

- User authentication and authorization with JWT
- User registration and login system
- Entity management with database migrations
- RESTful API endpoints
- Swagger UI for API documentation

## Project Structure

```
GoldenGemsBackEnd/
├── Controllers/          # API endpoint controllers
├── Models/              # Domain models and entities
├── DTOs/                # Data Transfer Objects
├── Services/            # Business logic services
├── Repositories/        # Data access layer
├── Data/                # DbContext and database configuration
├── Middleware/          # Custom middleware components
├── Migrations/          # Entity Framework migrations
├── Configurations/      # Application configuration files
├── Properties/          # Project properties
├── Program.cs           # Application startup configuration
├── appsettings.json     # Configuration settings
├── appsettings.Development.json  # Development-specific settings
└── GoldenGemsBackEnd.csproj      # Project file
```

## Getting Started

### Prerequisites

- .NET 10.0 SDK
- PostgreSQL database

### Setup

1. **Install dependencies**:
   ```bash
   dotnet restore
   ```

2. **Configure database connection** in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=goldengemdb;User Id=postgres;Password=your_password;"
     }
   }
   ```

3. **Run database migrations**:
   ```bash
   dotnet ef database update
   ```

4. **Run the application**:
   ```bash
   dotnet run
   ```

The API will be available at `http://localhost:5000` and Swagger UI at `http://localhost:5000/swagger`.

## Recent Work

- JWT authentication integration
- User login and registration
- Security model and people entity management
- Connection string configuration
- Entity ordering and organization

## Dependencies

- **Microsoft.AspNetCore.Authentication.JwtBearer** (10.0.2) - JWT authentication
- **Microsoft.AspNetCore.OpenApi** (10.0.2) - OpenAPI support
- **Microsoft.EntityFrameworkCore** (10.0.2) - ORM
- **Microsoft.EntityFrameworkCore.Design** (10.0.2) - EF design-time tools
- **Npgsql.EntityFrameworkCore.PostgreSQL** (10.0.0) - PostgreSQL provider
- **Swashbuckle.AspNetCore** (10.1.0) - Swagger/OpenAPI UI

## API Testing

Test HTTP requests are available in:
- `GoldenGemsBackEnd.http` - Main API tests
- `api-tests.http` - Additional API tests

## Development Notes

- Nullable reference types are enabled (`<Nullable>enable</Nullable>`)
- Implicit usings are enabled for cleaner code
- Repository pattern is used for data access
- Services layer handles business logic
- JWT tokens are used for stateless authentication

## Git Workflow

- Main branch: `master`
- Current branch: `claude/serene-bouman` (git worktree)
- All changes should be committed with clear, descriptive messages
