using System;

namespace Enban.Text
{
    internal static class TextExtensions
    {
        public static char[] ToTrimmedCharArray(this string s, bool trimStart, bool trimMiddle, bool trimEnd)
        {
            return Trim(s.ToCharArray(), trimStart, trimMiddle, trimEnd);
        }
        
        public static char[] Trim(this char[] text)
        {
            return text.Trim(true, true, true);
        }
        
        public static char[] Trim(this char[] text, bool trimStart, bool trimMiddle, bool trimEnd)
        {
            // No trimming at all -> just return text
            if (!trimStart && !trimMiddle && !trimEnd)
                return text;
            
            var buffer = new char[text.Length]; // temporary buffer for trimmed chars
            var offset = 0; // position of next write inside buffer
            
            var leadingWhiteSpaceCount = CountLeadingWhiteSpace(text);

            if (leadingWhiteSpaceCount == text.Length) // only white-space
            {
                if (trimStart || trimEnd)
                {
                    // trim at start or end results in empty char array
                    return Array.Empty<char>();
                }
                else
                {
                    // only trimming in the middle allowed, but not applicable in text full of white-space
                    return text;
                }
            }
            
            var trailingWhiteSpaceCount = CountTrailingWhiteSpace(text);
            
            if (!trimStart && leadingWhiteSpaceCount>0)
            {
                // Copy leading WS if no trimming at the start
                Array.Copy(text, 0, buffer, 0, leadingWhiteSpaceCount);
                offset = leadingWhiteSpaceCount;
            }
            
            if (trimMiddle)
            {
                // Filter white-space inside the part between leading and trailing WS
                var lastIndex = text.Length - trailingWhiteSpaceCount - 1;
                for(var index=leadingWhiteSpaceCount; index<=lastIndex; index++)
                {
                    var c = text[index];
                    if (!IsWhitespace(c))
                    {
                        buffer[offset++] = c;
                    }
                }
            }
            else
            {
                // Copy middle part without filtering WS
                var middleLen = text.Length - trailingWhiteSpaceCount - leadingWhiteSpaceCount;
                if (middleLen > 0)
                {
                    Array.Copy(text, leadingWhiteSpaceCount, buffer, offset, middleLen);
                    offset += middleLen;
                }
            }

            if (!trimEnd && trailingWhiteSpaceCount > 0)
            {
                // Copy trialing WS if no trimming at the end
                Array.Copy(text, text.Length-trailingWhiteSpaceCount, buffer, offset, trailingWhiteSpaceCount);
                offset += trailingWhiteSpaceCount;
            }
            
            Array.Resize(ref buffer, offset);
            return buffer;
            
            int CountLeadingWhiteSpace(char[] chars)
            {
                for (var i = 0; i < chars.Length; i++)
                {
                    if (!IsWhitespace(chars[i]))
                    {
                        return i;
                    }
                }
                return chars.Length;
            }

            int CountTrailingWhiteSpace(char[] chars)
            {
                var count = 0;
                for (var i = chars.Length - 1; i >= 0; i--)
                {
                    if (!IsWhitespace(chars[i]))
                    {
                        return count;
                    }

                    count++;
                }
                return chars.Length;
            }
        }
        
        private static bool IsWhitespace(char c)
        {
            return c == ' ' || c == '\t';
        }
    }
}
