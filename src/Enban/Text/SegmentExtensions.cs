using System.Linq;

namespace Enban.Text
{
    internal static class SegmentExtensions
    {
        public static string ToPattern(this Segment[] segments)
        {
            return string.Join("", segments.Select(s => s.ToPattern()));
        }
    }
}