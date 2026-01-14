using System.Collections.Generic;

namespace Less.Utils.Linq
{
    /// <summary>
    /// extend methods for <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class IEnumerableExtesion
    {
        /// <summary>
        /// flat enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<T> Flat<T>(this IEnumerable<IEnumerable<T>> values)
        {
            foreach (var children in values)
            {
                if (children is null) continue;

                foreach (var child in children)
                {
                    yield return child;
                }
            }
        }
    }
}
