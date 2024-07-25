# DDD Template with Repository Pattern, Mediator Pattern, Specification Pattern, and Unit of Work Pattern
## Introduction
This project is a template application using Domain-Driven Design (DDD) with C# and .NET 8. 
It implements various design patterns like Repository Pattern, Mediator Pattern, Specification Pattern, and Unit of Work Pattern to promote clean and scalable software architecture. 
The application also utilizes ASP.NET Core Identity for authentication and authorization, ensuring secure user management.

Feel free to fork it and use it however you desire.

### Project Structure
Domain: Contains domain entities, repository interfaces, specifications, and other business logic abstractions.
Application: Includes application services, DTOs, Mediator handlers, validations with Fluent Validation, and use cases.
Infrastructure: Implements repositories, specifications with Ardalis.Specification, database context, and Unit of Work.
API: The application interface with RESTful endpoints.

## Patterns Used
### Repository Pattern
The Repository Pattern is used to abstract data access and encapsulate persistence logic, allowing for a more domain-focused and testable development approach.

### Mediator Pattern
Used to mediate communication between different parts of the system, the Mediator Pattern helps reduce coupling between components. In this project, it is implemented using the Mediatr library.

### Specification Pattern
This pattern allows for creating search criteria and validation rules in a reusable and expressive manner, promoting a clear separation of business logic. It is implemented using the Ardalis.Specification library.

### Unit of Work Pattern
Manages transactions consistently, ensuring all database operations are executed atomically and maintaining data integrity. The pattern is applied automatically throughout the application and can be ignored when needed with the IgnoreGlobalTransactionAttribute.

### ASP.NET Core Identity
Used to manage authentication and authorization, providing a complete framework for user management, including login, registration, and roles.

### Fluent Validation
Used to validate objects and ensure that user data meets certain criteria before being processed by the application, it can be used to validate sync and async.
