using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Less.Utils.EFCore
{
    internal class DbContextInitializer : BackgroundService
    {
        private readonly DbContextInitializerBuilder builder;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<DbContextInitializer> logger;

        public DbContextInitializer(IDbContextInitializerBuilder builder, IServiceProvider serviceProvider, ILogger<DbContextInitializer> logger)
        {
            this.builder = (DbContextInitializerBuilder)builder;
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            foreach (var dbContextType in builder.DbContextTypes.Distinct())
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }

                try
                {
                    var dbContextOb = scope.ServiceProvider.GetRequiredService(dbContextType);
                    if (dbContextOb is DbContext context)
                    {
                        await context.Database.EnsureCreatedAsync(stoppingToken);
                        logger.LogInformation("Success create database {database} (From assembly {assembly})", dbContextType.FullName, dbContextType.Assembly.FullName);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
