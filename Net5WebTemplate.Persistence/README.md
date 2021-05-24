# Persistence Layer
The persistence layer is responsible for defining and managing interactions with a database or datastore.
Entity Framework Core CLI tools manage database migrations.

## Installing tools
Visual Studio 2019 Package Manager Console
```
Install-Package Micorsoft.EntityFrameworkCore.Tools
```

Update EF Core Tools
```
Update-Package Micorsoft.EntityFrameworkCore.Tools
```

## Create Migrations
Creates a new migration.
```
Add-Migration <Description> -Context Net5WebTemplateDbContext -OutputDir Migrations
```

## Remove Migrations
Removes the last added migration (rolls back the code changes that were done for the migration).
```
Remove-Migration -Context Net5WebTemplateDbContext
```

Revert all migrations
```
Remove-Migration 0 -Context Net5WebTemplateDbContext
```

## Apply Migrations
Applies the created migrations to the database.
```
Update-Database -Context Net5WebTemplateDbContext
```

## Drop Database
Delete database
```
Drop-Database -Context Net5WebTemplateDbContext
```