namespace Less.Utils
{
    public static class Result
    {
        /// <summary>
        /// Create Ok Result.
        /// </summary>
        /// <typeparam name="T">Result Ok value type</typeparam>
        /// <typeparam name="TError">Result Error value type</typeparam>
        /// <param name="value">Ok value</param>
        /// <returns><see cref="Result{T, TError}"/></returns>
        public static Result<T, TError> Ok<T, TError>(T value) => Result<T, TError>.NewOk(value);

        /// <summary>
        /// Create Error Result.
        /// </summary>
        /// <typeparam name="T">Result Ok value type</typeparam>
        /// <typeparam name="TError">Result Error value type</typeparam>
        /// <param name="errorValue">Error value</param>
        /// <returns><see cref="Result{T, TError}"/></returns>
        public static Result<T, TError> Err<T, TError>(TError errorValue) => Result<T, TError>.NewError(errorValue);
    }
}
