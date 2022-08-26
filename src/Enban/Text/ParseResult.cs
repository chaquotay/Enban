using System;

namespace Enban.Text
{
    /// <summary>
    /// The result of a <see cref="IPattern{T}.Parse"/> operation.
    /// </summary>
    /// <typeparam name="T">The type which was parsed</typeparam>
    public abstract class ParseResult<T>
    {
        private ParseResult()
        {
            
        }

        /// <summary>
        /// Indicates whether the parse operation was successful. 
        /// </summary>
        /// <returns><c>true</c> if the parse operation was successful; otherwise <c>false</c>.</returns>
        /// <remarks>This returns <c>true</c> if and only if fetching the value with the <see cref="Value"/> property
        /// will return with no exception.</remarks>
        public abstract bool Success { get; }

        /// <summary>
        /// The failure reason in form of an <see cref="System.Exception"/> in case
        /// the parse operation was not successful.
        /// </summary>
        /// <exception cref="InvalidOperationException">The parse operation succeeded.</exception>
        public virtual Exception Exception => throw new InvalidOperationException();

        /// <summary>
        /// Gets the value from the parse operation if it was successful, or throws an exception indicating the parse
        /// failure otherwise. 
        /// </summary>
        /// <returns>The result of the parsing operation if it was successful.</returns>
        /// <remarks>This method is exactly equivalent to fetching the <see cref="Value"/> property, but more explicit
        /// in terms of throwing an exception on failure.</remarks>
        public T Value => GetValueOrThrow();

        /// <summary>
        /// Returns the successfully parsed value or throws an exception (<see cref="Exception"/>)
        /// in case the parse operation was not successful.
        /// <returns>the parsed value</returns>
        /// </summary>
        public abstract T GetValueOrThrow();

        /// <summary>
        /// Returns the <see cref="Success">success value</see>, and sets the out parameter to either the specified
        /// <paramref name="failureValue">failure value</paramref> of <typeparamref name="T"/> or the
        /// <see cref="Value">successful parse result value</see>.
        /// </summary>
        /// <param name="failureValue">the value assigned to "result" in case the parse operation failed</param>
        /// <param name="result">the parsed value or "failureValue" in case the parse operation failed</param>
        /// <returns>whether the parse operation was successful; if true, then "failureValue" contains "Value", otherwise it contains the "failureValue"</returns>
        public abstract bool TryGetValue(T failureValue, out T result);

        private class SuccessResult : ParseResult<T>
        {
            private readonly T _value;

            public SuccessResult(T value)
            {
                _value = value;
            }
            
            public override bool Success => true;
            public override T GetValueOrThrow() => _value;
            public override bool TryGetValue(T failureValue, out T result)
            {
                result = _value;
                return true;
            }
        }

        internal static ParseResult<T> ForSuccess(T value)
        {
            return new SuccessResult(value);
        }
        
        private class FailureResult : ParseResult<T>
        {
            private readonly Exception _exception;

            public FailureResult(Exception exception)
            {
                _exception = exception;
            }
            
            public override bool Success => false;
            public override T GetValueOrThrow() => throw _exception;
            public override bool TryGetValue(T failureValue, out T result)
            {
                result = failureValue;
                return false;
            }
        }

        internal static ParseResult<T> ForFailure(Exception ex)
        {
            return new FailureResult(ex);
        }
    }
}