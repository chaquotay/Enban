using System;

namespace Enban.Text
{
    internal static class TextExtensions
    {
        public static char[] RemoveWhitespaces(this char[] text)
        {
            var chars = new char[text.Length];
            var offset = 0;

            foreach (var c in text)
            {
                if (c != ' ' && c != '\t')
                {
                    chars[offset++] = c;
                }
            }

            var result = new char[offset];
            Array.Copy(chars, 0, result, 0, result.Length);
            return result;
        }

    }
}
