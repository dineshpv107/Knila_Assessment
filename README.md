# Knila\_Assessment

This is a Contact Management application built using Angular for the frontend and ASP.NET Core Web API for the backend. It allows users to manage contacts with features like adding, editing, and deleting contacts, along with JWT authentication, logging, and unit testing.

## Features

* **Contact List**: View, add, edit, delete, and sort contacts.
* **Responsive UI**: Optimized for mobile using Bootstrap.
* **JWT Authentication**: Secured API endpoints.
* **Logging**: Implemented using Serilog.
* **Unit Tests**: Includes xUnit-based tests for API logic.

## Technologies Used

* **Frontend**: Angular, Bootstrap
* **Backend**: ASP.NET Core Web API, Entity Framework Core
* **Database**: SQL Server
* **Logging**: Serilog
* **Unit Testing**: NUnit, Moq

## Setup Instructions

### Backend

1. Clone the repository and navigate to `Api` folder.
2. Restore dependencies:

   ```bash
   dotnet restore
   ```
3. Update the connection string in `appsettings.json`.
4. Run the API:

   ```bash
   dotnet run
   ```

### Frontend

1. Navigate to the `Frontend` folder and install dependencies:

   ```bash
   npm install
   ```
2. Configure the API URL in `src/environments/environment.ts`.
3. Run the Angular development server:

   ```bash
   ng serve
   ```

### Unit Tests (API)

1. Navigate to the `Knila.Test` folder and restore dependencies:

   ```bash
   dotnet restore
   ```
2. Run the tests:

   ```bash
   dotnet test
   ```

## Directory Structure

```
Knila Assessment 
├── Knila Assessment
 │ 
 ├── Connected Services 
 │ 
 ├── Dependencies 
 │ 
 ├── Properties
  │ 
  ├── Controllers 
  │ 
  ├── ExdionLogs
    │ 
    ├── Logs 
    │ 
    ├── appsettings.json 
    │ 
    ├── Knila Assessment.http 
    │ 
    └── Program.cs
    ├── Knila.BAL
      │ 
      ├── Dependencies 
      │ 
      ├── Implementations 
      │ 
      └── Interface
    ├── Knila.DAL 
    │ 
    ├── Dependencies 
    │ 
    └── Models 
    └── Knila.Tests
├── Frontend
│   ├── src
│   └── angular.json
└── README.md
```

## API Endpoints

* **GET** `/api/Contact/GetAllContacts` - List contacts
* **POST** `/api/Contact/AddOrUpdateContact` - Add a new contact
* **POST** `/api/Contact/AddOrUpdateContact` - Edit a contact
* **POST** `/api/Contact/RemoveContacts` - Delete a contact

## License

MIT License

---
