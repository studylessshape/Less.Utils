using System;
using System.Collections.Generic;
using System.Linq;

namespace Less.Utils.ResultExtensions
{
    /// <summary>
    /// <seealso href="https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/language-specification/expressions#12204-the-query-expression-pattern"/>
    /// </summary>
    public static class ResultLinqExtensions
    {
        /// <summary>
        /// <para>If <paramref name="value"/> is error or doesn't meet the condition of predicate, it will back error result</para>
        /// <para><b>Notic: </b>If <paramref name="predicate"/> is <see langword="false"/>, it will back <see langword="default"/> error value</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="value"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Result<T, TError> Where<T, TError>(this Result<T, TError> value, Func<T, bool> predicate)
        {
            if (value.IsError)
            {
                return value;
            }

            if (!predicate(value.ResultValue))
            {
                return Result<T, TError>.NewError(default);
            }

            return value;
        }

        /// <summary>
        /// Select ok result from <typeparamref name="T1"/> to <typeparamref name="T2"/> by <paramref name="map"/>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="value"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Result<T2, TError> Select<T1, TError, T2>(this Result<T1, TError> value, Func<T1, T2> map)
        {
            return value.WrapOk(map);
        }

        /// <summary>
        /// Select many to <typeparamref name="T3"/>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="value"></param>
        /// <param name="mapValue"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public static Result<T3, TError> SelectMany<T1, TError, T2, T3>(this Result<T1, TError> value, Func<T1, Result<T2, TError>> mapValue, Func<T1, T2, T3> project)
        {
            if (value.IsError)
            {
                return Result<T3, TError>.NewError(value.ErrorValue);
            }

            Result<T2, TError> value2 = mapValue(value.ResultValue);
            if (value2.IsError)
            {
                return Result<T3, TError>.NewError(value2.ErrorValue);
            }

            return Result<T3, TError>.NewOk(project(value.ResultValue, value2.ResultValue));
        }
    }
}
