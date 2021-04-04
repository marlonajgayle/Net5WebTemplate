# Infrastructure Layer

## Executing  Entity Framework Core Commands
Adds a new Migration
```
Add-Migration AddingIdentity -OutputDir Identity/Migrations
```


Removes the last migration (rolls back the code changes that were done for the migration).
```
Remove-Migration -Context ApplicationIdentityDbContext
```