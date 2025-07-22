using System.Threading.Tasks;

namespace Less.Utils
{
    /// <summary>
    /// <see cref="Result{T, TError}" /> 的扩展方法
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Turn to <see cref="Task"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Task<Result<T, TError>> ToTask<T, TError>(Result<T, TError> result)
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
    }
}
