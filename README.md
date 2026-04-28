# SchoolProject

## Overview

`SchoolProject` is a .NET solution for managing student data with a clean layered architecture.
The solution includes an API project, a core business layer, an infrastructure layer for persistence, and a service layer for application logic.

## Solution Structure

- `SchoolProject.slnx` - root solution file that references all projects.

### Projects

1. `SchoolProject.Api`
   - Entry point for the application.
   - Hosts the Web API using ASP.NET Core.
   - Configures middleware, localization, Swagger, and dependency injection.
   - Contains API controllers and custom middleware.

2. `SchoolProject.Core`
   - Contains business logic, commands, validators, and shared response models.
   - Houses MediatR handlers, FluentValidation rules, AutoMapper profiles, and pipeline behaviors.
   - Defines common response wrappers and request validation behavior.

3. `SchoolProject.Infrastructure`
   - Implements data access and repository patterns.
   - Contains `AppDbContext`, Entity Framework configurations, and migrations.
   - Provides concrete repository implementations and database dependency registration.

4. `SchoolProject.Service`
   - Contains service interfaces and concrete implementations.
   - Acts as the application service layer between controllers and repositories.

## Key Folders

### `SchoolProject.Api`

- `Controllers/` - API endpoints such as `StudentController`.
- `Middlewares/` - shared middleware, including error handling.
- `Properties/` - launch and debug settings.
- `appsettings.json` / `appsettings.Development.json` - configuration and connection strings.

### `SchoolProject.Core`

- `Behaviours/` - pipeline behaviors like validation.
- `Features/Students/` - student-related commands, models, and validators.
- `Mapping/Students/` - AutoMapper configuration for student models.
- `Resources/` - localization resources for shared text and messages.
- `Bases/` - response wrappers and common response handling.

### `SchoolProject.Infrastructure`

- `Data/` - EF Core database context and entity configurations.
- `Repositories/` - repository implementations.
- `Migrations/` - EF Core migrations for schema changes.
- `InfrastructureBases/` - generic repository abstractions.
- `Abstracts/` - repository interfaces.

### `SchoolProject.Service`

- `Abstracts/` - service interfaces for application operations.
- `Implementations/` - concrete service implementations.

## How It Works

- `SchoolProject.Api` bootstraps the application.
- `Core` registers cross-cutting services such as MediatR, FluentValidation, and AutoMapper.
- `Service` contains business-facing service logic.
- `Infrastructure` handles database access and repository wiring.

## Running the Project

1. Open `SchoolProject.slnx` in Visual Studio or VS Code.
2. Set `SchoolProject.Api` as the startup project.
3. Run the application.
4. Use Swagger UI in development mode to explore endpoints.
