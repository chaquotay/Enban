using System;

namespace Enban.Text
{
    /// <summary>
    /// The result of <see cref="IPattern{T}.Parse"/>, for both success and failure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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

        /// <summary>
        /// Indicated that the parse operation was successful.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// The failure reason in form of an <see cref="System.Exception"/> in case
        /// the parse operation was not successful.
        /// </summary>
        public Exception Exception
        {
            get
            {
                if(Success)
                    throw new InvalidOperationException();

                return _exception;
            }
        }

        /// <summary>
        /// Returns the successfully parsed value or throws an exception (<see cref="Exception"/>)
        /// in case the parse operation was not successful.
        /// </summary>
        public T Value => GetValueOrThrow();

        /// <summary>
        /// Returns the successfully parsed value or throws an exception (<see cref="Exception"/>)
        /// in case the parse operation was not successful.
        /// <returns>the parsed value</returns>
        /// </summary>
        public T GetValueOrThrow()
        {
            if (Success)
                return _value;

            throw new Exception("Parse failure", _exception);
        }

        /// <summary>
        /// Tries to retrieve the parse result value
        /// </summary>
        /// <param name="failureValue">the value assigned to "result" in case the parse operation failed</param>
        /// <param name="result">the parsed value or "failureValue" in case the parse operation failed</param>
        /// <returns>whether the parse operation was successful; if true, then "failureValue" contains "Value", otherwise it contains the "failureValue"</returns>
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