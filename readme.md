
>
> &nbsp;
> This is dot net 8 project
> &nbsp;

**Slight detour**

This is code-first EF8 approach. I personally do not like it. There is too much magic involved. And complete detachment from the underlying storage induces the complexity in the code.

I think database-first EF8 approach delivers much simpler and effective C#.  Yes, with a good dollop of SQL statements strings.

## using two db contexts on one table

**Why?** Idea/aim is to use multiple contexts (here I use two) for situations when one client operates on one table. Here I do simple CRUD on the `Service` class/data.

If you do want to follow the setup on your dev machine you will need a clean and tidy dev machine. If that is not the case you can guarantee, I suggest you start it and work inside new and clean container. I am sure you will understand why. Here we go.

1. install dot net 8
     2. https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.203-windows-x64-installer
3. install ef for sqlite
    4. `dotnet add component Microsoft.EntityFrameworkCore.Sqlite`
5. install the ef designer
    1. `dotnet add component Microsoft.EntityFrameworkCore.Design`

`csproj` should look like this (as it is here) after you finish this long(er) setup and run the exe:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.3">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.3" />
  </ItemGroup>

</Project>
```

### prepare the db install the dotnet ef

That means prepare the table Services inside. There are two ways to do this: complex and simple. Simple is to install and use DB Browser for SQLite, before you start this program.  Complex way is to do `dotnet ef` based migration.

First you need the `dotnet ef` tool:

```
dotnet tool install --global dotnet-ef
dotnet ef --version
```
Than use it
```
dotnet ef migrations add InitialCreate
dotnet ef database update

```
The very long note about `dotnet ef` based migrations:

>
> When you execute dotnet ef migrations add InitialCreate, Entity Framework Core looks for a class that inherits from DbContext in your project. 
> It then generates a migration file based on the differences between the current state of the database (which it infers from the existing 
> migrations and the model snapshot) and the current state of your entity classes.
>
> By default, Entity Framework Core uses the AppDbContext class as the database context. It looks for this class within your project and uses it 
> to generate the migration.
>
> If your DbContext class has a different name or is located in a different namespace, you can specify the context type explicitly using the `--context` option when running migration commands.
>
> For example, if your DbContext class is named MyDbContext:
```
dotnet ef migrations add InitialCreate --context MyDbContext
```
> This tells Entity Framework Core to use MyDbContext as the context for generating the migration.
> 
> Similarly, when you run dotnet ef database update, Entity Framework Core applies the migrations to the database based on the context specified 
> in the migration files. If you have multiple contexts, you can specify which context to use by providing the --context option. Otherwise, Entity 
> Framework Core will use the default context specified in the migrations
