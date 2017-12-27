namespace Enban.Text
{
    public interface IPattern<T>
    {
        string Format(T value);
        ParseResult<T> Parse(string text);
    }
}