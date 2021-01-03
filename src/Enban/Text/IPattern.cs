namespace Enban.Text
{
    /// <summary>
    /// Common interface of pattern implementations
    /// </summary>
    /// <typeparam name="T">the type, which the implementation can <see cref="Format">format</see> and <see cref="Parse">parse</see>.</typeparam>
    public interface IPattern<T>
        where T : struct
    {
        /// <summary>
        /// Formats a value based on this pattern.
        /// </summary>
        /// <param name="value">the value to be formatted</param>
        /// <returns>the formatted value as a string</returns>
        string Format(T value);

        /// <summary>
        /// Tries to parse a string representation of a value of type <typeparam name="T"/>.
        /// </summary>
        /// <param name="text">the text to be parsed</param>
        /// <returns>the parse result (in case of both success and failure)</returns>
        ParseResult<T> Parse(string text);
    }
}