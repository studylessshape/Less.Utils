using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

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

        public static IEnumerable<T> Flat<T>(this IEnumerable<IEnumerable<T>> values, Func<T, bool> precidate)
        {
#if NET8_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(precidate);
#else
            if (precidate == null)
            {
                throw new ArgumentNullException(nameof(precidate));
            }
#endif
            Contract.EndContractBlock();

            foreach (var children in values)
            {
                if (children is null) continue;

                foreach (var child in children)
                {
                    if (!precidate(child)) continue;

                    yield return child;
                }
            }
        }
    }
}
