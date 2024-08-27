<h1 align="center">Welcome to Apexa Technical Assesment API 👋</h1>

## Background


This project involves developing a REST API for advisor management, where users can create, get, update, delete, and list advisors. The API will be built using .NET 8, ASP.NET Core, and C# 12, with in-memory persistence using EF Core 8, and unit testing using xUnit. API documentation will be managed using Swagger, and a script or command will be provided to run the application with a single click.

## Requirements

1. **Apexa Technical Assesment API:**  
   - Create an advisor with the following fields:
      - Name (Length: Max 255 | Required)
      - SIN (Length: Exactly 9 | Required | Masked | Unique)
      - Address (Length: Max 255 | Optional)
      - Phone (Length: Exactly 8 | Optional | Masked)
      - Health Status (Green/Yellow/Red) - Randomly generated in the backend with the following probabilities: Green=60%, Yellow=20%, Red=20%
   - Get an advisor by its unique identifier.
   - Update an advisor's information.
   - Delete an advisor from the database.
   - List all advisors available in the system.

2. **Backend:**
   - Platform: .NET 8, ASP.NET Core, C# 12.
   - Data access using EF Core 8 with in-memory persistence.
   - Unit testing with xUnit.
   - API documentation with Swagger.
   - Include a README file and a script to run the application with a single command.

## Method

### API Architecture

The API will follow a Clean Architecture pattern that separates concerns into layers, promoting modular and maintainable code.

----
    package "API" {
        component "Controllers" {
        }
        component "Application" {
        }
        component "Domain" {
        }
        component "Infrastructure" {
        }
    }
    Controllers -[hidden]-> Application : Uses
    Application --> Domain : Business Logic
    Infrastructure --> Domain : Data Access
----

**Presentation Layer (Controllers):** Will contain the ASP.NET Core controllers that will handle HTTP requests.

**Application Layer:** Will implement the business logic, including services and use cases.

**Domain Layer:** Will include the entities and business rules. The `Advisor` entity will be defined here with its properties and validations.

**Infrastructure Layer:** Will contain the EF Core implementation for accessing the in-memory database.

### Database Schema 

Although in-memory database will be used, the schema for the persistence of advisor data will defined.

----

entity Advisor {
    int Id
    string Name
    string SIN
    string Address
    string Phone
    string HealthStatus
}

----

### Health State Generation

The health state will be generated randomly when creating an advisor using the following probabilities:

- Green: 60%
- Yellow: 20%
- Red: 20%

This process will be implemented in the Application layer during the creation of the advisor.

### Unit Testing

The tests will be developed using xUnit, covering the following aspects:

- Creating an advisor with valid and invalid data.
- Updating an existing advisor.
- Deleting an advisor.
- Obtaining and listing advisors.

## Implementation

1. Setting up the .NET project with ASP.NET Core and EF Core.
2. Implementing the controllers and services.
3. Setting up the in-memory database.
4. Implementing unit tests with xUnit.
5. Documenting the API with Swagger.
6. Preparing the script for project execution.
   
## API and Test Execution Instructions

### 1. Clone the respository

```bash
git clone https://github.com/ronnymarca81/ApexaTechnicalSolution.git
cd ApexaTechnicalSolution
```

### 2. Restore NuGet Packages
```bash
dotnet restore
```
### 3. Execute the API

3.1. Return to the ApexaTechnicalSolution folder with cd..

3.2 dotnet run --project ApexaTechnicalApi/ApexaTechnicalApi.csproj
The API project and the tests share the solution, so you must indicate the project to run.

3.3. The API will be available at https://localhost:5225 and the Swagger documentation at https://localhost:5225/swagger.
![Swagger dashboard](https://github.com/user-attachments/assets/8b6e3d7e-4f02-4ca0-b123-6c1b899fae14)

3.4. Create a user to be able to generate authentication with JWT, as a result a token will be generated.
![Create user](https://github.com/user-attachments/assets/805d67da-913f-45a7-b51c-26cfceb16327)

3.5. Copy the token with Auth
![Token](https://github.com/user-attachments/assets/d38fab9b-521b-44c1-a88b-fc87ec365b70)

3.6. Click on the Authorize button located on the top right of the dashboard (Image on 3.3).

3.7. In the text box type the word bearer space paste the token and click on the Authorize button.
![Authorize](https://github.com/user-attachments/assets/c069cc9a-cf15-48a0-962d-5505f865e89c)

3.8. Click on the action to be performed.
Example:
Click on Post, then Try it out, add the parameters and click on the Execute button.
![Post](https://github.com/user-attachments/assets/10851a96-3ec0-440b-9239-e531e6a6d87b)

The response will be displayed as follows
![Responses](https://github.com/user-attachments/assets/4c66e32a-7b14-41aa-89ae-091f8c307a1f)

### 4. Execute the Test

4.1. Restore NuGet Packages.
```bash
dotnet restore ApexaTechnicalApi.Tests/ApexaTechnicalApi.Tests.csproj
```
4.2. Return to the ApexaTechnicalSolution folder with cd..

4.3. dotnet test
![Test](https://github.com/user-attachments/assets/c5d9870c-7241-4329-ac39-c11f88a58486)

## Milestones

1. Initial setup and project structure.
2. Implementation of the API with all CRUD operations for advisors and users.
3. Development of unit tests.
4. Documentation and preparation of the execution script.
5. Final review and deployment in the repository.

## Gathering Results

- Evaluation of compliance with functional and non-functional requirements.
- Verification of the correct operation of the API through Swagger.
- Execution of unit tests and review of coverage.

