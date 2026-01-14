using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Less.Utils.EFCore.Test
{
    [TestClass]
    public sealed class TestLessUtilsEFCore
    {
        [TestMethod]
        public async Task TestDbContextInitializer()
        {
            var dbFileName = "TestDbContext.db";
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<TestDbContext>(options =>
                    {
                        options.UseSqlite($"Data Source={dbFileName}")
                            .EnableSensitiveDataLogging(context.HostingEnvironment.IsDevelopment());
                    })
                    .AddDbInitializer(builder =>
                    {
                        builder.RegistDbContext<TestDbContext>();
                    });
                })
                .Build();
            var hostThread = new Thread(async () =>
            {
                await host.RunAsync();
            });
            hostThread.Start();
            // wait create dbcontext
            await Task.Delay(2000);
            await host.StopAsync();
            Assert.IsTrue(File.Exists(dbFileName));
        }
    }
}
