using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Less.Utils.EFCore.Test
{
    [TestClass]
    public sealed class TestLessUtilsEFCore
    {
        [TestMethod]
        public async Task TestDbContextInitializer()
        {
            var dbFileName = "TestDbContext.db";
            var services = new ServiceCollection();
            services
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                })
                .AddDbContext<TestDbContext>(options =>
                {
                    options.UseSqlite($"Data Source={dbFileName}");
                })
                .AddDbInitializer(builder =>
                {
                    builder.RegistDbContext<TestDbContext>();
                });
            var provider = services.BuildServiceProvider();
            await provider.EnsureDatabaseCreateAsync();
            Assert.IsTrue(File.Exists(dbFileName));
        }
    }
}
