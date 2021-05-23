# Persistence Layer
The persistence layer is responsible for defining and managing interactions with a database or datastore.
Entity Framework Core CLI tools manage database migrations.

## Installing tools
.NET CLI
```
 dotnet tool install -- global dotnet-ef
```

## Create Migrations
Creates a new migration.
```
Add-Migration <Description> -OutputDir Migrations
```

## Remove Migrations
Removes the last added migration (rolls back the code changes that were done for the migration).
```
Remove-Migration -Context Net5WebTemplateDbContext
```

Revert all migrations
```
Update-Database 0
```

## Apply Migrations
Applies the created migrations to the database.
```
Update-Database
```

## Drop Database
Delete database
```
Drop-Database
```