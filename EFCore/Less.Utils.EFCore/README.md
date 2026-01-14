# Less.Utils.EFCore

## DbContextInitializer

Initialize `DbContext` when start `Host`.

```csharp
services.AddDbInitializer(builder =>
{
    builder.RegistDbContext<MySqlDbContext>()
        .RegistDbContext<PostgresDbContext>()
        .RegistDbContext<SqliteDbContext>();
});
```