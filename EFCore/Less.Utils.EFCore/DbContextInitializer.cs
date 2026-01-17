using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Less.Utils.EFCore
{
    internal class DbContextInitializer
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<DbContextInitializer> logger;

        public DbContextInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.logger = serviceProvider.GetRequiredService<ILogger<DbContextInitializer>>();
        }

        public async Task EnsureDbContextCreateAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            var builder = (DbContextInitializerBuilder)scope.ServiceProvider.GetRequiredService<IDbContextInitializerBuilder>();
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
                        var created = await context.Database.EnsureCreatedAsync(stoppingToken);
                        if (created)
                        {
                            logger.LogInformation("Success create database {database} (From assembly {assembly}).", dbContextType.FullName, dbContextType.Assembly.FullName);
                        }
                        else
                        {
                            logger.LogInformation("Database {database} already exists (From assembly {assembly}).", dbContextType.FullName, dbContextType.Assembly.FullName);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    logger.LogDebug("The operations of creating database has been canceled.");
                    break;
                }
            }
        }
    }
}
