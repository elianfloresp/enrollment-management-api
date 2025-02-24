# Enrollment Management API

## Overview

This is the back-end of the enrollment tracking system, developed in .NET 8 with Entity Framework Core. The API enables the management of courses, students, and enrollments, ensuring integration with an SQLite database.

## Technologies Used

- .NET 8
- Entity Framework Core
- SQLite
- Swagger (for API documentation)
- CORS enabled for front-end communication

## Installation and Configuration

### 1. Clone the Repository

```sh
git clone <REPOSITORY_URL>
cd backend
```

### 2. Configure the Database

The API uses SQLite. Migrations are already created, and you only need to apply them:

```sh
dotnet ef database update
```

If you want to recreate the database, use:

```sh
dotnet ef database drop --force
```

### 3. Run the API

```sh
dotnet run
```

The API will run at `http://localhost:5288` (HTTP) A API será executada em `http://localhost:5000`.(HTTPS).

## Connecting with the Front-end

The front-end may run on `http://localhost:5173` or `http://localhost:5174`, depending on the port assigned by Vite. Make sure that in the API's `Program.cs` file, the CORS settings allow connections from these origins.

## Available Endpoints

Complete documentation is available via Swagger at `http://localhost:5288/swagger`.

### Courses

- **GET /courses** - List all courses
- **POST /courses** - Create a new course
- **PUT /courses/{id}** - Update an existing course
- **DELETE /courses/{id}** - Remove a course

### Students

- **GET /students** - List all students
- **POST /students** - Register a student (only 18 years or older)
- **PUT /students/{id}** - Update an existing student
- **DELETE /students/{id}** - Remove a student

### Enrollments

- **POST /enrollments** - Enroll a student in a course
- **DELETE /enrollments/{studentId}/{courseId}** - Remove a student from a course
- **GET /courses/{id}/students** - List students in a course
- **GET /students/{id}/courses** - List courses of a student

## Running Tests

```sh
dotnet test
```

## Final Considerations

- Ensure the API is running before testing the front-end.
- Use tools like Postman or Swagger to manually test the endpoints.

