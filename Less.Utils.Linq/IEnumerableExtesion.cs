using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Less.Utils.Linq
{
    /// <summary>
    /// extend methods for <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class IEnumerableExtesion
    {
        /// <summary>
        /// Flat enumerable.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="values">All items.</param>
        /// <returns>Sequence after flat.</returns>
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

        /// <summary>
        /// Flat items with <paramref name="predicate"/>
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="values">All items.</param>
        /// <param name="predicate">Filter items.</param>
        /// <returns>Sequence after flat.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="predicate"/> is <see langword="null"/>.</exception>
        public static IEnumerable<T> Flat<T>(this IEnumerable<IEnumerable<T>> values, Func<T, bool> predicate)
        {
#if NET8_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(predicate);
#else
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
#endif
            Contract.EndContractBlock();

            foreach (var children in values)
            {
                if (children is null) continue;

                foreach (var child in children.Where(predicate))
                {
                    yield return child;
                }
            }
        }
    }
}
