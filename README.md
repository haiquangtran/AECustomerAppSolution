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
- Visual Studio or dotnet CLI

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

## Set up with Visual Studio
### Run the project with Visual Studio
- Clone the repository
- In Visual Studio, locate the folder and open the AECustomerAppSolution.sln file in Visual Studio
- Ensure that the solution builds successfully by rebuilding the solution. selecting  Build > Rebuild.
- Set the startup project to be AE.CustomerApp.API 
- Run the project (either in debug mode or without). To run in debug mode press F5, otherwise, press Ctrl + F5 to run without debug mode.
- Navigate to the URL: https://localhost:{portNumber}/swagger where portNumber is the localhost port number i.e. https://localhost:44312/swagger
- All done!
## Run the tests with Visual Studio
- Open Visual Studio
- Select Tests > Run > Run all tests
- Tests should now be run

## Set up with dotnet CLI
### Run the project with dotnet command
- open command line, navigate to solution directory
- build the solution by typing in ``dotnet build``, ensure the build is successful
- Run the API project by typing in ``dotnet run --project ./AE.CustomerApp.Api``
- You should now see that the application is running on localhost port.
- Navigate to the URL: https://localhost:{portNumber}/swagger where portNumber is the localhost port number i.e. https://localhost:44312/swagger
- All done!
- For more informaton see the following url: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run?tabs=netcore21

### Run the tests with dotnet CLI
- open command line, navigate to solution directory
- build the solution by typing in ``dotnet build``, ensure the build is successful
- To run the tests, use the following command ``dotnet run``
- Tests should now be run
- For more informaton see the following url: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test?tabs=netcore20

## Future Extensions
- Currently uses In Memory database for convenience. In memory database should only be used for testing purposes, therefore, should not be used in future.
- Logging needs to be added to the solution, this can be added to the Global exception handler.
- Add Health check endpoint to check the health of the API.
- Add custom API Exceptions and custom exception handling for easier debugging and more detailed information.
