namespace Less.Utils.Mapper.Services
{
    internal class MapperWrapper<T1, T2> : IMapper<T1, T2>
        where T1 : class, new()
        where T2 : class, new()
    {
        private readonly IMapper<T1, T2> mapper12;
        private readonly IMapper<T2, T1> mapper21;

        public MapperWrapper(IMapper<T1, T2> mapper12)
        {
            this.mapper12 = mapper12;
        }

        public MapperWrapper(IMapper<T2, T1> mapper21)
        {
            this.mapper21 = mapper21;
        }

        public T2 MapTo(T1 from)
        {
            if (mapper12 != null) return mapper12.MapTo(from);
            return mapper21!.MapTo(from);
        }

        public T1 MapTo(T2 from)
        {
            if (mapper12 != null) return mapper12.MapTo(from);
            return mapper21!.MapTo(from);
        }
    }
}
