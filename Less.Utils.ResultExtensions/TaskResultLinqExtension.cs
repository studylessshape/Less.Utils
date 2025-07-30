using System;
using System.Threading.Tasks;

namespace Less.Utils.ResultExtensions
{
    /// <summary>
    /// <seealso href="https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/language-specification/expressions#12204-the-query-expression-pattern"/>
    /// </summary>
    public static class TaskResultLinqExtension
    {
        /// <summary>
        /// <para>If <paramref name="task"/> is error or doesn't meet the condition of predicate, it will back error result</para>
        /// <para><b>Notic: </b>If <paramref name="predicate"/> is <see langword="false"/>, it will back <see langword="default"/> error value</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="task"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<Result<T, TError>> Where<T, TError>(this Task<Result<T, TError>> task, Func<T, bool> predicate)
        {
            var result = await task;
            if (result.IsError)
            {
                return result;
            }

            if (!predicate(result.ResultValue))
            {
                return Result<T, TError>.NewError(default);
            }

            return result;
        }

        /// <summary>
        /// Select ok result from <typeparamref name="T1"/> to <typeparamref name="T2"/> by <paramref name="map"/>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="task"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static async Task<Result<T2, TError>> Select<T1, TError, T2>(this Task<Result<T1, TError>> task, Func<T1, T2> map)
        {
            return (await task).WrapOk(map);
        }

        /// <summary>
        /// Select many to <typeparamref name="T3"/>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="task"></param>
        /// <param name="mapValue"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public static async Task<Result<T3, TError>> SelectMany<T1, TError, T2, T3>(this Task<Result<T1, TError>> task, Func<T1, Task<Result<T2, TError>>> mapValue, Func<T1, T2, T3> project)
        {
            var result1 = await task;
            if (result1.IsError)
            {
                return Result<T3, TError>.NewError(result1.ErrorValue);
            }

            var result2 = await mapValue(result1.ResultValue);
            if (result2.IsError)
            {
                return Result<T3, TError>.NewError(result2.ErrorValue);
            }

            return Result<T3, TError>.NewOk(project(result1.ResultValue, result2.ResultValue));
        }
    }
}
