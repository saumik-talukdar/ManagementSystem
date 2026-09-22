# Management System

A backend management system built with **C# and ASP.NET Core Web API**.

This project is primarily a learning project for understanding the .NET ecosystem and building a RESTful backend using practical, incremental architecture.

The project currently uses an in-memory data store and will gradually evolve into a database-backed application using **Entity Framework Core and PostgreSQL**.

---

## Tech Stack

### Current

- C#
- .NET 10
- ASP.NET Core Web API
- Controllers
- Built-in Dependency Injection
- Model Binding
- Data Annotations / Validation
- DTOs
- LINQ
- In-memory collections

### Planned

- Entity Framework Core
- PostgreSQL
- Database migrations
- Authentication & Authorization
- JWT
- Global exception handling
- Logging
- Pagination
- Filtering
- Sorting
- Automated testing
- Docker
- OpenAPI / Swagger
- Production-oriented configuration

---

## Project Goals

The main goal of this project is to learn how to build a backend application properly with the .NET ecosystem.

The project is intentionally being developed incrementally instead of introducing a large architecture from the beginning.

The learning progression is roughly:

```text
C# Fundamentals
       ↓
ASP.NET Core Fundamentals
       ↓
REST API
       ↓
Dependency Injection
       ↓
Model Binding
       ↓
DTOs & Validation
       ↓
LINQ
       ↓
Entity Framework Core
       ↓
PostgreSQL
       ↓
Authentication & Authorization
       ↓
Testing
       ↓
Docker
       ↓
Production-oriented architecture
````

---

# Current Architecture

The current application is intentionally simple:

```text
HTTP Request
     ↓
Kestrel
     ↓
ASP.NET Core Middleware
     ↓
Routing
     ↓
Controller
     ↓
DTO
     ↓
Validation
     ↓
Service
     ↓
In-Memory List
     ↓
Service
     ↓
DTO
     ↓
HTTP Response
```

Current project structure:

```text
ManagementSystem/
│
├── ManagementSystem.sln
│
└── ManagementSystem.Api/
    │
    ├── Controllers/
    │   └── StudentsController.cs
    │
    ├── DTOs/
    │   └── Students/
    │       ├── CreateStudentRequest.cs
    │       ├── UpdateStudentRequest.cs
    │       └── StudentResponse.cs
    │
    ├── Models/
    │   └── Student.cs
    │
    ├── Services/
    │   └── StudentService.cs
    │
    ├── Properties/
    │   └── launchSettings.json
    │
    ├── Program.cs
    ├── appsettings.json
    ├── appsettings.Development.json
    └── ManagementSystem.Api.csproj
```

---

# Student API

The first resource implemented in the project is `Student`.

## Student Model

```csharp
public class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int Age { get; set; }

    public string Department { get; set; } = string.Empty;
}
```

---

# API Endpoints

Base URL:

```text
http://localhost:5244
```

> The port may change depending on the local launch configuration.

## Get all students

```http
GET /api/students
```

Example:

```bash
curl http://localhost:5244/api/students
```

Response:

```json
[
  {
    "id": 1,
    "name": "Saumik",
    "email": "saumik@example.com",
    "age": 22,
    "department": "CSE"
  },
  {
    "id": 2,
    "name": "Rahim",
    "email": "rahim@example.com",
    "age": 23,
    "department": "CSE"
  }
]
```

---

## Get student by ID

```http
GET /api/students/{id}
```

Example:

```bash
curl http://localhost:5244/api/students/1
```

Possible responses:

```text
200 OK
404 Not Found
```

---

## Create student

```http
POST /api/students
```

Request:

```json
{
  "name": "Karim",
  "email": "karim@example.com",
  "age": 21,
  "department": "EEE"
}
```

Example:

```bash
curl -X POST http://localhost:5244/api/students \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Karim",
    "email": "karim@example.com",
    "age": 21,
    "department": "EEE"
  }'
```

Response:

```text
201 Created
```

Example response:

```json
{
  "id": 3,
  "name": "Karim",
  "email": "karim@example.com",
  "age": 21,
  "department": "EEE"
}
```

The client does not provide the `id`.

The server assigns it.

---

## Update student

```http
PUT /api/students/{id}
```

Example:

```bash
curl -X PUT http://localhost:5244/api/students/1 \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Saumik Updated",
    "email": "saumik.updated@example.com",
    "age": 23,
    "department": "CSE"
  }'
```

Possible responses:

```text
200 OK
404 Not Found
400 Bad Request
```

---

## Delete student

```http
DELETE /api/students/{id}
```

Example:

```bash
curl -i -X DELETE http://localhost:5244/api/students/1
```

Possible responses:

```text
204 No Content
404 Not Found
```

---

# DTOs

The API does not directly use the `Student` model as its external request/response contract.

Instead, DTOs are used.

## CreateStudentRequest

Used when creating a student.

```csharp
public class CreateStudentRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(16, 100)]
    public int Age { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Department { get; set; } = string.Empty;
}
```

The `Id` is intentionally not part of the request.

---

## UpdateStudentRequest

Used when updating a student.

```csharp
public class UpdateStudentRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(16, 100)]
    public int Age { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Department { get; set; } = string.Empty;
}
```

---

## StudentResponse

Used when returning student data to the client.

```csharp
public class StudentResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int Age { get; set; }

    public string Department { get; set; } = string.Empty;
}
```

This creates a clear separation between:

```text
API Request
     ↓
Request DTO
     ↓
Domain/Application Model
     ↓
Response DTO
     ↓
API Response
```

---

# Validation

The project currently uses ASP.NET Core's built-in validation system with Data Annotations.

Examples:

```csharp
[Required]
```

Ensures a value is supplied.

```csharp
[EmailAddress]
```

Validates basic email formatting.

```csharp
[Range(16, 100)]
```

Restricts the age range.

```csharp
[StringLength(100, MinimumLength = 2)]
```

Restricts string length.

Because the controller uses:

```csharp
[ApiController]
```

ASP.NET Core automatically returns a `400 Bad Request` when model validation fails.

Example invalid request:

```json
{
  "name": "",
  "email": "not-an-email",
  "age": 10,
  "department": ""
}
```

---

# Model Binding

ASP.NET Core automatically maps HTTP request data to C# parameters and objects.

For example:

```csharp
[HttpPut("{id:int}")]
public ActionResult<StudentResponse> Update(
    int id,
    UpdateStudentRequest request)
```

For:

```http
PUT /api/students/5
```

and:

```json
{
  "name": "Saumik",
  "email": "saumik@example.com",
  "age": 22,
  "department": "CSE"
}
```

ASP.NET Core binds:

```text
Route
/api/students/5
       ↓
int id = 5

JSON body
       ↓
UpdateStudentRequest
```

Explicit binding can be specified using:

```csharp
[FromRoute]
[FromQuery]
[FromBody]
[FromHeader]
[FromForm]
[FromServices]
```

---

# Dependency Injection

ASP.NET Core provides a built-in dependency injection container.

The student service is registered in `Program.cs`:

```csharp
builder.Services.AddScoped<StudentService>();
```

The controller receives it through constructor injection:

```csharp
public StudentsController(StudentService studentService)
{
    _studentService = studentService;
}
```

The basic flow is:

```text
ASP.NET Core DI Container
        ↓
StudentService
        ↓
StudentsController
```

Currently `StudentService` uses an in-memory collection.

Later it will depend on EF Core's `DbContext`.

---

# Service Layer

The controller handles HTTP-related concerns.

The service handles student operations.

Example:

```csharp
public class StudentService
{
    private readonly List<Student> _students = [...];

    public List<Student> GetAll()
    {
        return _students;
    }

    public Student? GetById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    // Create, Update and Delete...
}
```

This keeps the controller from directly managing the data collection.

Current flow:

```text
Controller
    ↓
StudentService
    ↓
List<Student>
```

Future flow:

```text
Controller
    ↓
StudentService
    ↓
DbContext
    ↓
PostgreSQL
```

---

# LINQ

LINQ (Language Integrated Query) is used to query and transform collections and other data sources.

Examples currently used:

```csharp
_students.FirstOrDefault(s => s.Id == id);
```

```csharp
students.Select(student => new StudentResponse
{
    Id = student.Id,
    Name = student.Name
});
```

LINQ is particularly important because EF Core uses LINQ to query databases.

For example, a future query may look like:

```csharp
var students = await _dbContext.Students
    .Where(s => s.Department == "CSE")
    .ToListAsync();
```

EF Core can translate the LINQ expression into SQL.

---

# ASP.NET Core Request Flow

The current request lifecycle can be simplified as:

```text
Client
  │
  │ HTTP Request
  ▼
Kestrel
  │
  ▼
ASP.NET Core Middleware
  │
  ▼
Routing
  │
  ▼
Controller
  │
  ├── Model Binding
  │
  ├── Validation
  │
  ▼
Service
  │
  ▼
In-Memory Data
  │
  ▼
Service
  │
  ▼
Controller
  │
  ▼
DTO
  │
  ▼
JSON
  │
  ▼
HTTP Response
```

---

# HTTP Status Codes Currently Used

| Status            | Meaning                            | Example            |
| ----------------- | ---------------------------------- | ------------------ |
| `200 OK`          | Successful request                 | GET / PUT          |
| `201 Created`     | Resource created                   | POST               |
| `204 No Content`  | Successful deletion                | DELETE             |
| `400 Bad Request` | Invalid request/validation failure | Invalid DTO        |
| `404 Not Found`   | Resource doesn't exist             | Unknown student ID |

---

# Running the Project

Make sure the .NET SDK is installed:

```bash
dotnet --version
```

Run the API:

```bash
dotnet run --project ManagementSystem.Api --launch-profile http
```

The API should become available at the URL shown by ASP.NET Core, for example:

```text
http://localhost:5244
```

---

# Development Notes

The project currently uses the HTTP launch profile for local development.

HTTPS configuration is available in the ASP.NET Core project, but local development currently focuses on the HTTP profile while the development certificate configuration is handled separately.

The API is not currently containerized.

Docker support will be introduced later when PostgreSQL and the rest of the infrastructure are added.

---

# Architecture Evolution

The architecture will intentionally evolve as the project becomes more complex.

### Current

```text
ManagementSystem.Api
│
├── Controllers
├── DTOs
├── Models
└── Services
```

### Planned

```text
ManagementSystem.sln
│
├── ManagementSystem.Api
│
├── ManagementSystem.Application
│
├── ManagementSystem.Domain
│
└── ManagementSystem.Infrastructure
```

The separation will only be introduced when the project has enough complexity to justify it.

The goal is to understand **why** each layer exists rather than introducing architecture for its own sake.

---

# Learning Topics Covered

* [x] .NET SDK and runtime
* [x] .NET project structure
* [x] `.sln` and `.csproj`
* [x] `Program.cs`
* [x] Kestrel
* [x] ASP.NET Core middleware
* [x] Routing
* [x] Controllers
* [x] `ControllerBase`
* [x] HTTP methods
* [x] `ActionResult<T>`
* [x] HTTP status codes
* [x] Dependency Injection
* [x] `Transient`, `Scoped`, and `Singleton`
* [x] Model binding
* [x] `[FromRoute]`
* [x] `[FromQuery]`
* [x] `[FromBody]`
* [x] `[FromHeader]`
* [x] `[FromForm]`
* [x] `[FromServices]`
* [x] DTOs
* [x] Data annotation validation
* [x] `[ApiController]`
* [x] Basic LINQ
* [x] Advanced LINQ
* [ ] Entity Framework Core
* [ ] PostgreSQL
* [ ] EF Core migrations
* [ ] Relationships
* [ ] Async database operations
* [ ] Pagination
* [ ] Filtering
* [ ] Sorting
* [ ] Global exception handling
* [ ] Authentication
* [ ] Authorization
* [ ] JWT
* [ ] Logging
* [ ] Automated testing
* [ ] Docker
* [ ] Production configuration
* [ ] Deployment

---

# Design Principles

The project follows a few principles:

### 1. Keep the architecture proportional to the problem

Avoid introducing abstractions just because they are commonly seen in enterprise projects.

### 2. Understand the framework before abstracting it

The initial implementation deliberately uses straightforward ASP.NET Core features before introducing additional architectural layers.

### 3. Separate responsibilities

Controllers should primarily deal with HTTP.

Services should handle application operations.

DTOs should define API contracts.

Entities/models should represent internal application data.

### 4. Prefer explicit code while learning

Manual DTO mapping is currently intentional.

Once the underlying mechanics are understood, repetitive code can be evaluated for abstraction.

### 5. Evolve architecture with complexity

The project will move toward a layered architecture only when there is a practical reason for the separation.

---

# Future Database Architecture

The current in-memory implementation:

```text
StudentService
      ↓
List<Student>
```

will eventually become:

```text
StudentsController
       ↓
StudentService
       ↓
EF Core
       ↓
DbContext
       ↓
PostgreSQL
```

This will introduce:

* `DbContext`
* Entity configuration
* Database migrations
* LINQ-to-SQL translation
* Async database operations
* Relationships
* Transactions
* Query optimization

---

# Project Status

**Current stage: ASP.NET Core fundamentals + DTOs + validation + basic LINQ**

The next major milestone is:

```text
LINQ
  ↓
Entity Framework Core
  ↓
PostgreSQL
```

The project is intentionally developed as a learning exercise, with the architecture becoming more sophisticated as new requirements are introduced.

