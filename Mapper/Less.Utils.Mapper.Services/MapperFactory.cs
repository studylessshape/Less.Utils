using Microsoft.Extensions.DependencyInjection;
using System;

namespace Less.Utils.Mapper.Services
{
    internal class MapperFactory : IMapperFactory
    {
        private readonly IServiceProvider service;

        public MapperFactory(IServiceProvider service)
        {
            this.service = service;
        }

        public IMapper<T1, T2> GetMapper<T1, T2>()
            where T1 : class, new()
            where T2 : class, new()
        {
            var mapper12 = service.GetService<IMapper<T1, T2>>();
            if (mapper12 != null)
            {
                return mapper12;
            }

            var mapper21 = service.GetService<IMapper<T2, T1>>();
            if (mapper21 != null)
            {
                return new MapperWrapper<T1, T2>(mapper21);
            }

            throw new InvalidOperationException($"Can't get IMapper<{typeof(T1).Name}, {typeof(T2).Name}> from service.");
        }

        public T2 MapTo<T1, T2>(T1 from)
            where T1 : class, new()
            where T2 : class, new()
        {
            return GetMapper<T1, T2>().MapTo(from);
        }
    }
}
