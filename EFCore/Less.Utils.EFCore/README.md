# Less.Utils.EFCore

## DbContextInitializer

Initialize `DbContext`.

```csharp
var services = new ServiceCollection();
services.AddDbInitializer(builder =>
{
    builder.RegistDbContext<MySqlDbContext>()
        .RegistDbContext<PostgresDbContext>()
        .RegistDbContext<SqliteDbContext>();
});

var provider = services.BuildServiceProvider();
            await provider.EnsureDatabaseCreateAsync();
```

The Example can see https://github.com/studylessshape/Less.Utils/blob/master/Test/Less.Utils.EFCore.Test/TestLessUtilsEFCore.cs#L11