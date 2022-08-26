namespace Enban.Text
{
    /// <summary>
    /// Generic interface supporting parsing and formatting. Parsing always results in a <see cref="ParseResult{T}"/>
    /// which can represent success or failure.
    /// </summary>
    /// <typeparam name="T">Type of value to parse or format.</typeparam>
    public interface IPattern<T>
    {
        /// <summary>
        /// Formats the given value as text according to the rules of this pattern.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>The value formatted according to this pattern.</returns>
        string Format(T value);

        /// <summary>
        /// Parses the given text value according to the rules of this pattern.
        /// </summary>
        /// <param name="text">The text value to parse.</param>
        /// <returns>The result of parsing, which may be successful or unsuccessful. (The value returned is never null.)</returns>
        ParseResult<T> Parse(string text);
    }
}