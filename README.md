= SPEC-1: API de Gestión de Asesores 
:sectnums:
:toc:


== Background

Este proyecto consiste en el desarrollo de una API REST para la gestión de asesores, donde los usuarios pueden crear, obtener, actualizar, eliminar y listar asesores. La API se construirá utilizando .NET 8, ASP.NET Core, y C# 12, con persistencia en memoria utilizando EF Core 8, y pruebas unitarias con xUnit. La documentación de la API se gestionará mediante Swagger, y se proporcionará un script o comando para ejecutar la aplicación con un solo clic.

== Requirements

*Must-Have:*

1. **API de Gestión de Asesores:**
   - Crear un asesor con los siguientes campos:
     - Nombre (Longitud: Máx 255 | Requerido)
     - SIN (Longitud: Exactamente 9 | Requerido | Enmascarado | Único)
     - Dirección (Longitud: Máx 255 | Opcional)
     - Teléfono (Longitud: Exactamente 8 | Opcional | Enmascarado)
     - Estado de Salud (Verde/Amarillo/Rojo) - Generado aleatoriamente en el backend con las siguientes probabilidades: Verde=60%, Amarillo=20%, Rojo=20%
   - Obtener un asesor por su identificador único.
   - Actualizar la información de un asesor.
   - Eliminar un asesor de la base de datos.
   - Listar todos los asesores disponibles en el sistema.

2. **Backend:**
   - Plataforma: .NET 8, ASP.NET Core, C# 12.
   - Acceso a datos utilizando EF Core 8 con persistencia en memoria.
   - Pruebas unitarias con xUnit.
   - Documentación de la API con Swagger.
   - Incluir un archivo README y un script para ejecutar la aplicación con un solo comando.

== Method

=== Arquitectura de la API

La API seguirá un patrón de arquitectura limpia (Clean Architecture) que separa las preocupaciones en capas, promoviendo un código modular y fácil de mantener.

[plantuml, architecture, png]
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

## Instrucciones de Ejecución

### 1. Clonar el Repositorio

bash
git clone https://github.com/ronnymarca81/ApexaTechnicalSolution.git
cd ApexaTechnicalSolution

### 2. Restaurar Paquetes NuGet
bash
dotnet restore

### 3. Ejecutar la Aplicación
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

