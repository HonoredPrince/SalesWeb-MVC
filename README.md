# SalesWeb-MVC
A simple departament store sales manager built in C# with ASP.NET Core for a college discipline

# Requirements
- Install .NET SDK version 2.1 or greater
- Install MySQL (preferred) 

##### OBS: The project configuration is set to a connection string in MySQL, but any other relational database can be used, just modifiy the connection string and database type in the configuration files and rebuild the project, can use nuget to manage packages too

### Configuration Files

````Startup.cs````
````csharp
services.AddDbContext<SalesWebMVCContext>(
    options => options.UseMySql(Configuration.GetConnectionString("SalesWebMVCContext"), 
    builder => builder.MigrationsAssembly("SalesWebMVC"))
);
````

````appsettings.json````
````csharp
"ConnectionStrings": {
    "SalesWebMVCContext": "server=localhost;userid=developer;password=451236;database=saleswebmvcappdb"
}
````

#### Finally build and run the program, project is already configured to run a first migration with some data to view the application 

- CMD: **dotnet build** > **dotnet run**
- VS Studio: **Build and Start** the project
