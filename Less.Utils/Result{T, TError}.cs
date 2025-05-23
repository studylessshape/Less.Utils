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

        public int Tag => tag;
        public bool IsOk => Tag == OK;
        public bool IsError => Tag == Err;

        public T ResultValue => resultValue;
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

        public Result<T, TError> WithErrorValue(TError errorValue)
        {
            this.errorValue = errorValue;
            return this;
        }

        public Result<T, TError> WithResultValue(T resultValue)
        {
            this.resultValue = resultValue;
            return this;
        }

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

        public static Result<T, TError> NewOk(T resultValue)
        {
            return new Result<T, TError>(resultValue, OK);
        }

        public static Result<T, TError> NewError(TError errorValue)
        {
            return new Result<T, TError>(errorValue, Err);
        }

        public static implicit operator Result<T, TError>(T resultValue)
        {
            return NewOk(resultValue);
        }

        public static implicit operator Result<T, TError>(TError errorValue)
        {
            return NewError(errorValue);
        }

        public static Result<T, TError> FromOk<TOtherError>(Result<T, TOtherError> result)
        {
            return NewOk(result.ResultValue);
        }

        public static Result<T, TError> FromErr<TOther>(Result<TOther, TError> result)
        {
            return NewError(result.ErrorValue);
        }

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
