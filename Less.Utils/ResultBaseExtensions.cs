using System;
using System.Threading.Tasks;

namespace Less.Utils
{
    /// <summary>
    /// <see cref="Result{T, TError}" /> 的扩展方法
    /// </summary>
    public static class ResultBaseExtensions
    {
        /// <summary>
        /// Turn to <see cref="Task"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Task<Result<T, TError>> ToTask<T, TError>(this Result<T, TError> result)
        {
            return Task.FromResult(result);
        }

        /// <summary>
        /// Translate <typeparamref name="T"/> to <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Result<T, TError> ToOk<T, TError>(this T result)
        {
            return Result<T, TError>.NewOk(result);
        }

        /// <summary>
        /// Translate <typeparamref name="TError"/> to <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Result<T, TError> ToErr<T, TError>(this TError result)
        {
            return Result<T, TError>.NewError(result);
        }

        /// <summary>
        /// Provide task result wrap ok method
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="result"></param>
        /// <param name="wrap"></param>
        /// <returns></returns>
        public static async Task<Result<TNew, TError>> WrapOk<TNew, T, TError>(this Task<Result<T, TError>> result, Func<T, TNew> wrap)
        {
            return (await result).WrapOk(wrap);
        }

        /// <summary>
        /// Provide task result wrap error method
        /// </summary>
        /// <typeparam name="TNewError"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="result"></param>
        /// <param name="wrap"></param>
        /// <returns></returns>
        public static async Task<Result<T, TNewError>> WrapErr<TNewError, T, TError>(this Task<Result<T, TError>> result, Func<TError, TNewError> wrap)
        {
            return (await result).WrapErr(wrap);
        }
    }
}
