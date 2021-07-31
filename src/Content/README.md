<img align="left" width="116" height="116" src="https://raw.githubusercontent.com/marlonajgayle/Net5WebTemplate/develop/src/Content/.template.config/icon.png" />

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
1. Install the latest [.NET Core 5 SDK](https://dotnet.microsoft.com/download). 
2. Run `dotnet new --install Net5WebTemplate::1.0.0-preview.2` to install the project template
3. Then navigate to the location you would like to create to project
4. Run `dotnet new net5webapi -o "MyProject"` to create a new project

### Docker Setup
ASP.NET Core Web API uses HTTPS and relies on certificates for trust, identity and encryption. 
To run Net5WebTemplate application Docker over HTTPS during development do the following:
1. Generate certificate using 'dotnet dev-certs' (for localhost use Only!).
On Windows using Linux Containers
```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx  -p your_password
dotnet dev-certs https --trust
````
When using PowerShell, replace %USERPROFILE% with $env:USERPROFILE.

On macOS or Linux
```
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p { password here }
dotnet dev-certs https --trust
```
2. Build and run Docker containers run Docker compose located in the solution directory
`docker-compose -f 'docker-compose.yml' up --build`

### Database Setup
To setup the SQL Server database following the instrcutions below:
1. Reveiw the connection string in appsettings.Local.json and update the database name.
2. Run `dotnet ef migrations add Initial --context <ProjectName>DbContext` to add migation with EF Core 
3. Run `dotnet ef database update Initial` to create application database.

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

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/marlonajgayle/Net5WebTemplate/master/LICENSE.md) file for details.