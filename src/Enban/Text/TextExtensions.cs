using System;

namespace Enban.Text
{
    internal static class TextExtensions
    {
        public static char[] Trim(this char[] text)
        {
            return text.Trim(true, true, true);
        }
        
        private static bool IsWhitespace(char c)
        {
            return c == ' ' || c == '\t';
        }

        public static char[] ToTrimmedCharArray(this string s, bool trimStart, bool trimMiddle, bool trimEnd)
        {
            return Trim(s.ToCharArray(), trimStart, trimMiddle, trimEnd);
        }
        
        public static char[] Trim(this char[] text, bool trimStart, bool trimMiddle, bool trimEnd)
        {
            if (!trimStart && !trimMiddle && !trimEnd)
                return text;
            
            var offset = 0;
            var chars = new char[text.Length];

            var prefixLen = 0;
            while (prefixLen < text.Length && IsWhitespace(text[prefixLen]))
                prefixLen++;

            if (prefixLen == text.Length)
            {
                if (trimStart || trimEnd)
                {
                    return Array.Empty<char>();
                }
                else
                {
                    return text;
                }
            }

            var suffixLen = 0;
            for (var i = text.Length - 1; i >= 0; i--)
            {
                if (IsWhitespace(text[i]))
                {
                    suffixLen++;
                }
                else
                {
                    break;
                }
            }
            
            if (!trimStart && prefixLen>0)
            {
                Array.Copy(text, 0, chars, 0, prefixLen);
                offset = prefixLen;
            }

            if (trimMiddle)
            {
                var lastIndex = text.Length - suffixLen - 1;
                for(var index=prefixLen; index<=lastIndex; index++)
                {
                    var c = text[index];
                    if (!IsWhitespace(c))
                    {
                        chars[offset++] = c;
                    }
                }
            }
            else
            {
                var middleLen = text.Length - suffixLen - prefixLen;
                if (middleLen > 0)
                {
                    Array.Copy(text, prefixLen, chars, offset, middleLen);
                    offset += middleLen;
                }
            }

            if (!trimEnd && suffixLen > 0)
            {
                Array.Copy(text, text.Length-suffixLen, chars, offset, suffixLen);
                offset += suffixLen;
            }
            
            var result = new char[offset];
            Array.Copy(chars, 0, result, 0, result.Length);
            return result;
        }
    }
}
