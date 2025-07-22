using System;

namespace Less.Utils
{
    /// <summary>
    /// 简单复制 FSharpResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public struct Result<T, TError> : IEquatable<Result<T, TError>>
    {
        private const int OK = 0;
        private const int Err = 1;

        internal int _tag;
        internal T resultValue;
        internal TError errorValue;

        /// <summary>
        /// Result Tag
        /// </summary>
        public int Tag => _tag;

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
            this._tag = tag;
            errorValue = default;
        }

        internal Result(TError errorValue, int tag)
        {
            this.errorValue = errorValue;
            this._tag = tag;
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
        /// <typeparam name="TError2"></typeparam>
        /// <param name="predicite"></param>
        /// <returns></returns>
        public Result<T, TError2> WrapErr<TError2>(Func<TError, TError2> predicite)
        {
            if (IsError)
            {
                return Result<T, TError2>.NewError(predicite(ErrorValue));
            }
            else
            {
                return Result<T, TError2>.NewOk(ResultValue);
            }
        }

        public Result<T2, TError2> Wrap<T2, TError2>(Func<T, T2> mapOkValue, Func<TError, TError2> mapErrValue)
        {
            if (IsError)
            {
                return Result<T2, TError2>.NewError(mapErrValue(ErrorValue));
            }
            else
            {
                return Result<T2, TError2>.NewOk(mapOkValue(ResultValue));
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

        /// <inheritdoc/>
        public override string ToString()
        {
            if (IsOk)
            {
                return $"Ok({ResultValue.ToString()})";
            }
            else
            {
                return $"Err({ErrorValue.ToString()})";
            }
        }

        /// <inheritdoc/>
        public bool Equals(Result<T, TError> other)
        {
            int tag = _tag;
            int tag2 = other._tag;
            if (tag == tag2)
            {
                if (Tag == 0)
                {
                    T x = resultValue;
                    T y = other.resultValue;
                    return x.Equals(y);
                }
                TError x2 = errorValue;
                TError y2 = other.errorValue;
                return x2.Equals(y2);
            }
            return false;
        }
    }
}
