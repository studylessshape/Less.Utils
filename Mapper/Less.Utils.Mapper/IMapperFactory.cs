namespace Less.Utils.Mapper
{
    /// <summary>
    /// MapperFactory can directly use <see cref="IMapper{T1, T2}"/>
    /// </summary>
    public interface IMapperFactory
    {
        /// <summary>
        /// Map from <typeparamref name="T1"/> to <typeparamref name="T2"/>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        T2 MapTo<T1, T2>(T1 from)
            where T1 : class, new()
            where T2 : class, new();

        /// <summary>
        /// Get mapper
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">There is not such service <see cref="IMapper{T1, T2}"/>.</exception>
        IMapper<T1, T2> GetMapper<T1, T2>()
            where T1 : class, new()
            where T2 : class, new();
    }
}
