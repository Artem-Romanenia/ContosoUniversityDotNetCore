### This is a fork from Jimmy Bogard's ContosoUniversity on ASP.NET Core example repository.
It demonstrates how [MediatR.Extensions.Microsoft.AspNetCore.Mvc](https://github.com/Artem-Romanenia/MediatR.Extensions.Microsoft.AspNetCore.Mvc) extension can be applied to shorten the amount of repetitive Mvc controller related code to a minimum.

Most of the Mvc controllers that are responsible for handling MediatR requests are eliminated completely, one controller is left for demonstration of another aspect of MediatR.Extensions.Microsoft.AspNetCore.Mvc behavior.

To see the differences with original repository, go [here](https://github.com/jbogard/ContosoUniversityDotNetCore/compare/master...Artem-Romanenia:mediatr-mvc-extension).

# ContosoUniversity on ASP.NET Core with .NET Core

Contoso University, the way I would write it.

To run, create a database "ContosoUniversity" and run the SchemaAndData.sql script against it. Modify the connection string in appsettings and go!

## Things demonstrated

- CQRS and MediatR
- AutoMapper
- Feature folders and vertical slices
- HtmlTags
- Entity Framework Core

## How to run

First run the build script (Build.ps1). This will set up the local database using RoundhousE. Open the solution and run!
