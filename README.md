[![Build status](https://dev.azure.com/marlongayle/Net5WebTemplate/_apis/build/status/Net5WebTemplate-CI)](https://dev.azure.com/marlongayle/Net5WebTemplate/_build/latest?definitionId=3)
[![Build](https://github.com/marlonajgayle/Net5WebTemplate/actions/workflows/dotnet.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/Net5WebTemplate/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/marlonajgayle/Net5WebTemplate/actions/workflows/codeql-analysis.yml/badge.svg?branch=develop)](https://github.com/marlonajgayle/Net5WebTemplate/actions/workflows/codeql-analysis.yml)

# .NET 5 Web Template
A multi-project solution .NET template for creating a an enterprise level application that includes the use of Swagger, API Versioning, 
Localization, NLog, Fluent Validation, Fluent Email, IP Rate Limiting using the .NET 5 Framework and are guided by the Clean Architecture 
principles enabling rapid application development.

## Purpose
This template serves two purposes, to standardize the creation of ASP.NET Core 5 Web API projects and share knowledge on ways 
to implement projects with enterprise level considerations.

The project template consist of scafolding for API Versioning, Ip Rate Limiting, Security Headers, CORS, Application Logging, CQRS, Localization,
JWT Authorization, Swagger (Open API) and Unit Tests.

## Getting Started
Use the instructions provided below to get the project up and running.

### Prerequisites
You will need the following tools:
* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.8 or later)
* [.NET Core SDK 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

### Instructions
1. Run `dotnet new --install Clean.Architecture.Solution.Template` to install the project template
2. Create a folder for your solution and cd into it.
3. Run `dotnet new net5webtemplate --name "MyProject"` to create a new project
4. Run `dotnet ef migrations add Initial --context <ProjectName>DbContext` to add migation with EF Core 
5. Run `dotnet ef database update Initial` to create application database.

## Technologies and Third Party Libraries
* .NET 5
* ASP.NET Core 5
* Entity Framework Core 5
* xUnit
* AspNetCoreRateLimit
* Fluent Assertions
* Fluent Email
* Fluent Validation
* MediatR
* Moq
* NLog
* NWebsec
* Swashbuckle

## Contributions
- [Demar-j](https://github.com/Demar-j) - Implemented unit tests.

## Credits
This solution's structure was heavily infuenced by [Jason Taylor's](https://github.com/jasontaylordev) Clean Architecture model.

## Versions
The [master](https://github.com/marlonajgayle/Net5WebTemplate/master) branch is running .NET 5.0

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/marlonajgayle/Net5WebTemplate/master/LICENSE.md) file for details.