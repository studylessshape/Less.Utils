using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Less.Utils.Mapper.Services
{
    /// <summary>
    /// Regist mapper to <see cref="IServiceCollection"/>
    /// </summary>
    public static class MapperServiceExtensions
    {
        /// <summary>
        /// Add the implements of <see cref="IMapper{T1, T2}"/> and <see cref="IMapperFactory"/> to services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="needAssembly"></param>
        /// <param name="needAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMappers(this IServiceCollection services, Assembly needAssembly, params Assembly[] needAssemblies)
        {
            services.AddSingleton<IMapperFactory, MapperFactory>();
            var mapperInterfaceName = typeof(IMapper<,>).Name;

            var assemblies = new List<Assembly>() { needAssembly };
            if (needAssemblies != null)
            {
                assemblies.AddRange(needAssemblies);
            }

            foreach (var assemblyClasses in assemblies.Select(assembly => assembly.ExportedTypes.Where(ty => ty.IsClass && !ty.IsAbstract && ty.GetInterfaces().Any(ty => ty.Name == mapperInterfaceName))))
            {
                foreach (var mapperClass in assemblyClasses)
                {
                    var mapperInterfaces = mapperClass.GetInterfaces().Where(i => i.Name == mapperInterfaceName);
                    foreach (var mapperInterface in mapperInterfaces)
                    {
                        var alreadyInService = services.FirstOrDefault(s => s.ServiceType == mapperInterface);
                        if (alreadyInService != null)
                        {
                            throw new System.InvalidOperationException($"Duplicate mapper implement: {mapperClass} and {alreadyInService.ImplementationType}");
                        }

                        services.AddTransient(mapperInterface, mapperClass);
                    }
                }
            }

            return services;
        }
    }
}
