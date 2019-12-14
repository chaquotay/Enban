using System;

namespace Enban.Text
{
    internal static class TextExtensions
    {
        public static char[] RemoveWhitespaces(this char[] text)
        {
            var whitespaceCount = 0;
            var inWhitespace = false;

            var src = text;
            var chars = new char[text.Length];
            var offset = 0;
            var start = 0;

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c == ' ' || c == '\t')
                {
                    //whitespaceCount++;
                    //if (!inWhitespace)
                    //{
                    //    var len = i - start;
                    //    Array.Copy(src, start, chars, offset, len);
                    //    offset += len;
                    //}
                    //inWhitespace = true;
                }
                else
                {
                    chars[offset++] = c;
                    //if (inWhitespace)
                    //{
                    //    start = i;
                    //    inWhitespace = false;
                    //}
                }
            }

            //if (!inWhitespace)
            //{
            //    Array.Copy(src, start, chars, offset, src.Length - start);
            //}

            //var t = new char[chars.Length - whitespaceCount];
            var t = new char[offset];
            Array.Copy(chars, 0, t, 0, t.Length);
            //var t = new string(chars, 0, chars.Length - whitespaceCount);
            return t;
        }

    }
}
