<h1 align="center">Welcome to Apexa Technical Assesment API 👋</h1>

## Background


This project involves developing a REST API for advisor management, where users can create, get, update, delete, and list advisors. The API will be built using .NET 8, ASP.NET Core, and C# 12, with in-memory persistence using EF Core 8, and unit testing using xUnit. API documentation will be managed using Swagger, and a script or command will be provided to run the application with a single click.

## Requirements

1. **AApexa Technical Assesment API:**  
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
@startuml
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
@enduml
----

**Capa de Presentación (Controllers):** Contendrá los controladores ASP.NET Core que manejarán las solicitudes HTTP.

**Capa de Aplicación (Application):** Implementará la lógica de negocio, incluyendo servicios y casos de uso.

**Capa de Dominio (Domain):** Incluirá las entidades y reglas de negocio. Aquí se definirá la entidad `Advisor` con sus propiedades y validaciones.

**Capa de Infraestructura (Infrastructure):** Contendrá la implementación de EF Core para el acceso a la base de datos en memoria.

=== Esquema de Base de Datos

Aunque utilizaremos una base de datos en memoria, definiremos el esquema para la persistencia de datos de los asesores.

[plantuml, database, png]
----
@startuml
entity Advisor {
    int Id
    string Name
    string SIN
    string Address
    string Phone
    string HealthStatus
}
@enduml
----

=== Generación del Estado de Salud

El estado de salud se generará aleatoriamente al crear un asesor utilizando las siguientes probabilidades:

- Verde: 60%
- Amarillo: 20%
- Rojo: 20%

Este proceso se implementará en la capa de Aplicación durante la creación del asesor.

=== Pruebas Unitarias

Las pruebas se desarrollarán utilizando xUnit, cubriendo los siguientes aspectos:

- Creación de un asesor con datos válidos e inválidos.
- Actualización de un asesor existente.
- Eliminación de un asesor.
- Obtención y listado de asesores.

== Implementation

=== Backend

1. Configuración del proyecto .NET con ASP.NET Core y EF Core.
2. Implementación de los controladores y servicios.
3. Configuración de la base de datos en memoria.
4. Implementación de las pruebas unitarias con xUnit.
5. Documentación de la API con Swagger.
6. Preparación del script para la ejecución del proyecto.

## Instrucciones de Ejecución de la API y el Test

### 1. Clonar el Repositorio

bash
git clone https://github.com/ronnymarca81/ApexaTechnicalSolution.git
cd ApexaTechnicalSolution

### 2. Restaurar Paquetes NuGet
bash
dotnet restore

### 3. Ejecutar API
bash
3.1. Regresar a la carpeta ApexaTechnicalSolution con cd..

3.2 dotnet run --project ApexaTechnicalApi/ApexaTechnicalApi.csproj
El proyecto del API y las pruebas comparten la solucion por tal razon debe indicar el proyecto a ejecutar.

3.3. La API estará disponible en https://localhost:5225 y la documentación de Swagger en https://localhost:5225/swagger.
![Swagger dashboard](https://github.com/user-attachments/assets/8b6e3d7e-4f02-4ca0-b123-6c1b899fae14)

3.4. Crear un usuario para poder generar autenticacion con JWT como resultdo generara un token.
![Create user](https://github.com/user-attachments/assets/805d67da-913f-45a7-b51c-26cfceb16327)

3.5. Copiar el token con Auth
![Token](https://github.com/user-attachments/assets/d38fab9b-521b-44c1-a88b-fc87ec365b70)

3.6. Dar clic en el boton Authorize situado en la parte superior derecha del dashboard (Image on 3.3).

3.7. En el cuadro de texto digitar la palabra bearer espacio pegar el token y clic en el boton Authorize.
![Authorize](https://github.com/user-attachments/assets/c069cc9a-cf15-48a0-962d-5505f865e89c)

3.8. Clic en la accion a realizar. 
Ejemplo: 
Clic en Post, luego Try it out, agregar los parametros y clic en el boton Execute.
![Post](https://github.com/user-attachments/assets/10851a96-3ec0-440b-9239-e531e6a6d87b)

La respuesta se visualizara de la siguiente manera
![Responses](https://github.com/user-attachments/assets/4c66e32a-7b14-41aa-89ae-091f8c307a1f)

### 4. Ejecutar Test

4.1. Restaurar Paquetes NuGet.
bash
dotnet restore ApexaTechnicalApi.Tests/ApexaTechnicalApi.Tests.csproj

4.2. Regresar a la carpeta ApexaTechnicalSolution con cd..

4.3. dotnet test
![Test](https://github.com/user-attachments/assets/c5d9870c-7241-4329-ac39-c11f88a58486)

== Milestones

1. Configuración inicial y estructura del proyecto.
2. Implementación de la API con todas las operaciones CRUD.
3. Desarrollo de las pruebas unitarias.
4. Documentación y preparación del script de ejecución.
5. Revisión final y despliegue en el repositorio.

== Gathering Results

- Evaluación del cumplimiento de los requisitos funcionales y no funcionales.
- Verificación del correcto funcionamiento de la API a través de Swagger.
- Ejecución de pruebas unitarias y revisión de la cobertura.

