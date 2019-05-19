# AE Customer API

A simple CRUD Customer API that allows:
- Adding customers
  - First name, last name, and date of birth fields
- Editing customers
- Deleting customers
- Searching for a customer by partial name match (first or last name)

**Note**: It uses EF Core In Memory database for convenience but ideally this should be only used for testing purposes. 

## Developer Requirements
- .NET Core 2.2
- Visual Studio 2017 (Recommended)

## Set up (using Visual Studio)
- Clone the repository
- In Visual Studio, locate the folder and open the AECustomerAppSolution.sln file in Visual Studio
- Ensure that the solution builds successfully by rebuilding the solution. selecting  Build > Rebuild.
- Set the startup project to be AE.CustomerApp.API 
- Run the project (either in debug mode or without). To run in debug mode press F5, otherwise, press Ctrl + F5 to run without debug mode.

## Technology Stack
- ASP.NET Core 2.2 API
- Entity Framework Core (In memory database)
- XUnit 
- Swagger/OpenAPI support
- Dependency Injection

## Future Extensions (if time permits)
- Currently uses In Memory database for convenience. In memory database should only be used for testing purposes, therefore, should not be used in future.
- Logging needs to be added to the solution, this can be added to the Global exception handler.
- Unit tests and integration tests need to be written.application.
- Add Health check endpoint to check the health of the API.
- Add custom API Exceptions and exception handling for easier debugging and more detailed information.
