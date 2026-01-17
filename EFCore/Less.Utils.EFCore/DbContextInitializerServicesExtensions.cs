using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Less.Utils.EFCore
{
    public static class DbContextInitializerServicesExtensions
    {
        public static IServiceCollection AddDbInitializer(this IServiceCollection services, Action<IDbContextInitializerBuilder> builderAction)
        {
            services.AddScoped(typeof(IDbContextInitializerBuilder), _ =>
            {
                var builder = new DbContextInitializerBuilder();
                builderAction(builder);
                return builder;
            });
            return services;
        }

        public static async Task EnsureDatabaseCreateAsync(this IServiceProvider serviceProvider, CancellationToken token = default)
        {
            var initializer = new DbContextInitializer(serviceProvider);
            await initializer.EnsureDbContextCreateAsync(token);
        }
    }
}
