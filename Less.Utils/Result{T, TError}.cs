using System;

namespace Less.Utils
{
    /// <summary>
    /// 简单复制 FSharpResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public struct Result<T, TError>
    {
        private const int OK = 0;
        private const int Err = 1;

        internal int tag;
        internal T resultValue;
        internal TError errorValue;

        /// <summary>
        /// Result Tag
        /// </summary>
        public int Tag => tag;

        /// <summary>
        /// Ok
        /// </summary>
        public bool IsOk => Tag == OK;

        /// <summary>
        /// Error
        /// </summary>
        public bool IsError => Tag == Err;

        /// <summary>
        /// When <see cref="IsOk"/> is <see langword="true"/>, can use this value to get result.
        /// </summary>
        public T ResultValue => resultValue;
        /// <summary>
        /// When <see cref="IsError"/> is <see langword="true"/>, can use this value to get error.
        /// </summary>
        public TError ErrorValue => errorValue;

        internal Result(T resultValue, int tag)
        {
            this.resultValue = resultValue;
            this.tag = tag;
            errorValue = default;
        }

        internal Result(TError errorValue, int tag)
        {
            this.errorValue = errorValue;
            this.tag = tag;
            resultValue = default;
        }

        /// <summary>
        /// If <see cref="IsOk"/> is <see langword="true"/>, will set result by <paramref name="predicite"/>.
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="predicite"></param>
        /// <returns></returns>
        public Result<TNew, TError> WrapOk<TNew>(Func<T, TNew> predicite)
        {
            if (IsError)
            {
                return Result<TNew, TError>.NewError(ErrorValue);
            }
            else
            {
                return Result<TNew, TError>.NewOk(predicite(ResultValue));
            }
        }

        /// <summary>
        /// If <see cref="IsError"/> is <see langword="true"/>, will set error by <paramref name="predicite"/>.
        /// </summary>
        /// <typeparam name="TErr"></typeparam>
        /// <param name="predicite"></param>
        /// <returns></returns>
        public Result<T, TErr> WrapErr<TErr>(Func<TError, TErr> predicite)
        {
            if (IsError)
            {
                return Result<T, TErr>.NewError(predicite(ErrorValue));
            }
            else
            {
                return Result<T, TErr>.NewOk(ResultValue);
            }
        }

        /// <summary>
        /// New ok result.
        /// </summary>
        /// <param name="resultValue"></param>
        /// <returns></returns>
        public static Result<T, TError> NewOk(T resultValue)
        {
            return new Result<T, TError>(resultValue, OK);
        }

        /// <summary>
        /// New error result.
        /// </summary>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static Result<T, TError> NewError(TError errorValue)
        {
            return new Result<T, TError>(errorValue, Err);
        }

        /// <summary>
        /// Turn to ok result. If <typeparamref name="T"/> is <typeparamref name="TError"/>, it can't be used.
        /// </summary>
        /// <param name="resultValue"></param>
        public static implicit operator Result<T, TError>(T resultValue)
        {
            return NewOk(resultValue);
        }

        /// <summary>
        /// Turn to error result. If <typeparamref name="T"/> is <typeparamref name="TError"/>, it can't be used.
        /// </summary>
        /// <param name="errorValue"></param>
        public static implicit operator Result<T, TError>(TError errorValue)
        {
            return NewError(errorValue);
        }

        /// <summary>
        /// From other result with same result type but different error type.
        /// </summary>
        /// <typeparam name="TOtherError"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Result<T, TError> FromOk<TOtherError>(Result<T, TOtherError> result)
        {
            return NewOk(result.ResultValue);
        }

        /// <summary>
        /// From other result with same error type but different result type.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Result<T, TError> FromErr<TOther>(Result<TOther, TError> result)
        {
            return NewError(result.ErrorValue);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (IsOk)
            {
                return ResultValue.ToString();
            }
            else
            {
                return ErrorValue.ToString();
            }
        }
    }
}
