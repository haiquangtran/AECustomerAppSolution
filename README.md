# AE Customer API

A simple CRUD Customer API that allows:
- Adding customers
  - First name, last name, and date of birth fields
- Editing customers
- Deleting customers
- Searching for a customer by partial name match (first or last name)

**Swagger Url:** https://localhost:{portnumber}/swagger/index.html

**Note**: It uses EF Core In Memory database for convenience but ideally this should be only used for testing purposes. 

## Developer Requirements
- .NET Core 2.2
- Visual Studio 2017 (Recommended)

## Technology Stack
- ASP.NET Core 2.2 API
- Entity Framework Core (In memory database)
- XUnit, Moq
- Swagger/OpenAPI support
- Dependency Injection

## Design patterns used
- Uses Clean Architecture (https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- Dependency Injection
- SOLID principles

## Set up (using Visual Studio)
- Clone the repository
- In Visual Studio, locate the folder and open the AECustomerAppSolution.sln file in Visual Studio
- Ensure that the solution builds successfully by rebuilding the solution. selecting  Build > Rebuild.
- Set the startup project to be AE.CustomerApp.API 
- Run the project (either in debug mode or without). To run in debug mode press F5, otherwise, press Ctrl + F5 to run without debug mode.
- Navigate to the URL: https://localhost:{portNumber}/swagger where portNumber is the localhost port number i.e. https://localhost:44312/swagger

## Future Extensions (if time permits)
- Currently uses In Memory database for convenience. In memory database should only be used for testing purposes, therefore, should not be used in future.
- Logging needs to be added to the solution, this can be added to the Global exception handler.
- Add Health check endpoint to check the health of the API.
- Add custom API Exceptions and custom exception handling for easier debugging and more detailed information.
