using System.Collections.Generic;

namespace Enban.Text
{
    internal static class SegmentsMatcher
    {
        public static bool IsMatch(List<Segment> segments, char[] chars)
        {
            return IsMatch(segments, chars, 0, chars.Length);
        }

        public static bool IsMatch(List<Segment> segments, char[] chars, int offset, int len)
        {
            if (offset + len > chars.Length)
                return false;

            var expectedLen = 0;
            var segmentsCount = segments.Count;

            for (var i = 0; i < segmentsCount; i++)
            {
                var segment = segments[i];
                var segmentLength = segment.Length;
                expectedLen += segmentLength;
            }

            if (expectedLen != len)
                return false;

            for (var i=0; i<segmentsCount; i++)
            {
                var segment = segments[i];
                var segmentCharacterClass = segment.CharacterClass;
                var segmentLength = segment.Length;

                for (var segOffset = 0; segOffset < segmentLength; segOffset++)
                {
                    var c = chars[offset++];
                    if (segmentCharacterClass == CharacterClass.Digits)
                    {
                        if (c < '0' || c > '9')
                            return false;
                    } else if (segmentCharacterClass == CharacterClass.UpperCaseLetters)
                    {
                        if (c < 'A' || c > 'Z')
                            return false;
                    } else if (segmentCharacterClass == CharacterClass.AlphanumericCharacters)
                    {
                        if ((c < '0' || c > '9') && (c < 'A' || c > 'Z') && (c < 'a' || c > 'z'))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}