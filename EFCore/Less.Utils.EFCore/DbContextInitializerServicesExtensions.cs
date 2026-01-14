using Microsoft.Extensions.DependencyInjection;
using System;

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
            services.AddHostedService<DbContextInitializer>();
            return services;
        }
    }
}
