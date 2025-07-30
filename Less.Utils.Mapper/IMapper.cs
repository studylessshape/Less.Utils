namespace Less.Utils.Mapper
{
    /// <summary>
    /// Provider method <see cref="MapTo(T1)"/>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface IMapper<T1, T2>
    {
        /// <summary>
        /// Map object to <typeparamref name="T2"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        T2 MapTo(T1 from);

        /// <summary>
        /// Map object to <typeparamref name="T1"/>
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        T1 MapTo(T2 from);
    }
}
