using System;

namespace Enban.Text
{
    public sealed class ParseResult<T>
    {
        private readonly Exception _exception;
        private readonly T _value;

        private ParseResult(bool success, T value, Exception exception)
        {
            _value = value;
            _exception = exception;
            Success = success;
        }

        public bool Success { get; }

        public Exception Exception
        {
            get
            {
                if(Success)
                    throw new InvalidOperationException();

                return _exception;
            }
        }

        public T Value => GetValueOrThrow();

        public T GetValueOrThrow()
        {
            if (Success)
                return _value;

            throw new Exception("Parse failure", _exception);
        }

        public bool TryGetValue(T failureValue, out T result)
        {
            result = Success ? _value : failureValue;
            return Success;
        }

        internal static ParseResult<T> ForSuccess(T value)
        {
            return new ParseResult<T>(true, value, null);
        }

        internal static ParseResult<T> ForFailure(Exception ex)
        {
            return new ParseResult<T>(false, default(T), ex);
        }
    }
}