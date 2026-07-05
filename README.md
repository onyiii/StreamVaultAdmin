\# StreamVault Admin



\## Overview



StreamVault Admin is an ASP.NET Core MVC application for managing streaming content. Administrators can create, view, edit, and delete content items (Movies,audiobook, music album and Series). The application uses Entity Framework Core with SQL Server and demonstrates inheritance mapping using a shared base class for content entities.



\---



\## Prerequisites



\* .NET 10 SDK

\* SQL Server LocalDB or SQL Server Express

\* Visual Studio 2022 (latest version)



\---



\## Running the Application



1\. Clone the repository.



```bash

git clone https://github.com/onyiii/StreamVaultAdmin

```



2\. Open the solution in Visual Studio 2022.



3\. Restore NuGet packages.



4\. Press \*\*F5\*\* or run:



```bash

dotnet run

```



5\. The application will launch automatically in the browser.



\---



\## Database Creation and Seeding



The database is created automatically on first run.



During application startup:



\* Entity Framework Core applies any pending migrations.

\* The database is created if it does not already exist.

\* Sample content is seeded automatically.



Example seeded data includes:



* Movies
* Series
* AudioBook
* MusicALbums



No manual database setup is required. Simply run the application and the database will be generated automatically.



\---



\## Design Decisions



\### Architecture



The solution follows a simple layered structure:



\* \*\*Models\*\* – Domain entities.

\* \*\*Data\*\* – DbContext, configuration, and seed data.

\* \*\*Controllers\*\* – MVC controllers handling requests.

\* \*\*Views\*\* – Razor views for the user interface.



This structure keeps the project easy to understand while maintaining separation of concerns.



\---



\### Inheritance Strategy



A common abstract base class, `BaseProperties`, is used to store fields shared by all content types.



Common properties include:



\* Title

\* ReleaseYear

\* Genre

\* Details



Specialized content types inherit from this base class:



```c#

BaseProperties

&#x20;   ├── Movie

&#x20;   └── Series

```



Additional properties:



\*\*Movie\*\*



\* DurationMinutes



\*\*Series\*\*



\* NumberOfSeasons



This approach eliminates duplicated code and allows future content types to be added easily.



\---



\### Persistence Approach



Entity Framework Core uses \*\*Table-Per-Hierarchy (TPH)\*\* inheritance mapping.



A single table stores all content records, while a discriminator column identifies the content type.



Benefits:



\* Simple database design.

\* Faster querying across all content.

\* Minimal schema complexity.

\* Native EF Core support.



Example:



| Id | Title        | ReleaseYear | DurationMinutes | NumberOfSeasons | Discriminator |

| -- | ------------ | ----------- | --------------- | --------------- | ------------- |

| 1  | Inception    | 2010        | 148             | NULL            | Movie         |

| 2  | Breaking Bad | 2008        | NULL            | 5               | Series        |



\---



\## Validation



Model validation is implemented using Data Annotations.



Examples include:



\* Required fields

\* Range validation

\* String length validation



Invalid data is prevented from being saved to the database.



\---



\## What I Would Do Next With More Time



If additional time were available, I would:



1\. Add unit and integration tests.

2\. Add authentication and role-based authorization.

3\. Implement pagination.

4\. Add logging and error handling.

5\. Enhance the UI with a more modern responsive design.

6\. Introduce dedicated view models per content type.

7\. Potentially use the clean code architecture.





\---



\## Technologies Used



\* ASP.NET Core MVC

\* .NET 10

\* Entity Framework Core

\* SQL Server

\* Bootstrap

\* Razor Views



