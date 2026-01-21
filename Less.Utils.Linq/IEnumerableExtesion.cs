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
#if NET8_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(values);
#else
            if (values is null) throw new ArgumentNullException(nameof(values));
#endif

            return values.Where(v => v is not null).SelectMany(v => v);
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
            ArgumentNullException.ThrowIfNull(values);
            ArgumentNullException.ThrowIfNull(predicate);
#else
            if (values is null) throw new ArgumentNullException(nameof(values));
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
#endif

            return values.Where(v => v is not null)
                    .SelectMany(v => v.Where(predicate));
        }
    }
}
