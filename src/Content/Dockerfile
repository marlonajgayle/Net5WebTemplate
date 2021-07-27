#To build this dockerfile, run the follwoing command from the solution diretory:
# docker build --tag net5api .

# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
ARG Configuration=Release
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true
WORKDIR /app

# Copy the csproj files and restore dependecies (via Nuget)
COPY "Net5WebTemplate.sln" "."
COPY "src/Net5WebTemplate.Api/*.csproj" "src/Net5WebTemplate.Api/"
COPY "src/Net5WebTemplate.Application/*.csproj" "src/Net5WebTemplate.Application/"
COPY "src/Net5WebTemplate.Domain/*.csproj" "src/Net5WebTemplate.Domain/"
COPY "src/Net5WebTemplate.Infrastructure/*.csproj" "src/Net5WebTemplate.Infrastructure/"
COPY "src/Net5WebTemplate.Persistence/*.csproj" "src/Net5WebTemplate.Persistence/"
COPY "tests/Net5WebTemplate.UnitTests/*.csproj" "tests/Net5WebTemplate.UnitTests/"
RUN dotnet restore

# Copy the project files and build release
COPY "src/Net5WebTemplate.Api/." "src/Net5WebTemplate.Api/"
COPY "src/Net5WebTemplate.Application/." "src/Net5WebTemplate.Application/"
COPY "src/Net5WebTemplate.Domain/." "src/Net5WebTemplate.Domain/"
COPY "src/Net5WebTemplate.Infrastructure/." "src/Net5WebTemplate.Infrastructure/"
COPY "src/Net5WebTemplate.Persistence/." "src/Net5WebTemplate.Persistence/"
COPY "tests/Net5WebTemplate.UnitTests/." "tests/Net5WebTemplate.UnitTests/"
RUN dotnet build "src/Net5WebTemplate.Api/Net5WebTemplate.Api.csproj"  --configuration $Configuration 
RUN dotnet test  "tests/Net5WebTemplate.UnitTests/Net5WebTemplate.UnitTests.csproj" --configuration $Configuration --no-build
RUN dotnet publish "src/Net5WebTemplate.Api/Net5WebTemplate.Api.csproj" --configuration $Configuration  --no-build --output out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
EXPOSE 80 443
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Net5WebTemplate.Api.dll"]