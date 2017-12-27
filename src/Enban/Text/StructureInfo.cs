using System.Collections.Generic;
using System.Linq;

namespace Enban.Text
{
    internal class StructureInfo
    {
        public ICollection<Segment> Segments { get; }

        public int Start { get; }
        public int Length { get; }

        public StructureInfo(int start, ICollection<Segment> segments)
        {
            Segments = segments;
            Start = start;
            Length = segments.Sum(s => s.Length); // TODO: Length Indication
        }

        public int End => Start + Length - 1;
    }
}