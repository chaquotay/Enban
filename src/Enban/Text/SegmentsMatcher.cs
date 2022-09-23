namespace Enban.Text
{
    internal static class SegmentsMatcher
    {
        public static bool IsMatch(Segment[] segments, char[] chars)
        {
            return IsMatch(segments, chars, 0, chars.Length);
        }

        public static bool IsMatch(Segment[] segments, char[] chars, int offset, int len)
        {
            if (offset + len > chars.Length)
                return false;

            var expectedLen = 0;
            var segmentsCount = segments.Length;

            for (var i = 0; i < segmentsCount; i++)
            {
                var segment = segments[i];
                var segmentLength = segment.LengthIndication.Length;
                expectedLen += segmentLength;
            }

            if (expectedLen != len)
                return false;

            for (var i=0; i<segmentsCount; i++)
            {
                var segment = segments[i];
                var segmentCharacterClass = segment.CharacterClass;
                var segmentLength = segment.LengthIndication.Length;

                for (var segOffset = 0; segOffset < segmentLength; segOffset++)
                {
                    var c = chars[offset++];
                    if (!segmentCharacterClass.Contains(c))
                        return false;
                }
            }

            return true;
        }
    }
}