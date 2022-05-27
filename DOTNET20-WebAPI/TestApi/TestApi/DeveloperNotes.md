# Project: WepApi

## Packages

- Microsoft.EntityFrameworkCore.SqlServer, SQL Server Database Provider for EF Core.
- Microsoft.EntityFrameworkCore.Tools, Allows for code first migrations, Data Context object (DbContext) for db interrraction
- Swashbuckle.AspNetCore.Annotations, Allows for [SwaggerOperation(Tags = )]
## CLI

```pwsh
#Code first migration. pwd = C:\Custom\Repositories\Sprinto\DOTNET20-WebAPI\TestApi
Add-Migration InitialCreate -o Data/Migrations

#Apply Migration
Update-Database

```

## Folder structure

- Controllers, Models, Data (DbContext Data\Migrations\migration files)

## Referenecs & External Resources
- [Swashbuckle.AspNetCore.Annotations](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/README.md#swashbuckleaspnetcoreannotations)
```CSharp
//Example:
[HttpPost]

[SwaggerOperation(
    Summary = "Creates a new product",
    Description = "Requires admin privileges",
    OperationId = "CreateProduct",
    Tags = new[] { "Purchase", "Products" }
)]
public IActionResult Create([FromBody]Product product)

```