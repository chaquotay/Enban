using System;
using System.Collections.Generic;

namespace Enban.Text
{
    /// <summary>
    /// A part of a pattern, basically a tuple consisting of character class, a length and a fixed/maximum flag.
    /// </summary>
    internal struct Segment : IEquatable<Segment>
    {
        private readonly CharacterClass _characterClass;
        private readonly short _length;
        private readonly LengthIndication _lengthIndication;

        public CharacterClass CharacterClass => _characterClass;

        public short Length => _length;

        public LengthIndication LengthIndication => _lengthIndication;

        public Segment(CharacterClass characterClass, short length, LengthIndication lengthIndication)
        {
            _characterClass = characterClass;
            _length = length;
            _lengthIndication = lengthIndication;
        }

        public bool Equals(Segment other)
        {
            return _characterClass == other._characterClass && _length == other._length && _lengthIndication == other._lengthIndication;
        }

        public override bool Equals(object obj)
        {
            return obj is Segment other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)_characterClass;
                hashCode = (hashCode * 397) ^ _length;
                hashCode = (hashCode * 397) ^ (int)_lengthIndication;
                return hashCode;
            }
        }

        public static IEqualityComparer<Segment[]> EqualityComparer { get; } = new SegmentsEqualityComparer();

        private class SegmentsEqualityComparer : IEqualityComparer<Segment[]>
        {
            public bool Equals(Segment[] x, Segment[] y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (x == null || y == null)
                    return false;

                if (x.Length != y.Length)
                    return false;

                for (var i = 0; i < x.Length; i++)
                {
#if INLINE
                    Segment tempQualifier = x[i];
                    Segment other = y[i];
                    if (!(tempQualifier._characterClass == other._characterClass && tempQualifier._length == other._length && tempQualifier._lengthIndication == other._lengthIndication))
                        return false;
#else
                    if (!x[i].Equals(y[i]))
                        return false;
#endif
                }

                return true;
            }

            public int GetHashCode(Segment[] obj)
            {
                unchecked
                {
                    var hashCode = 0;
                    foreach (var segment in obj)
                    {
#if INLINE
                        var hashCode1 = (int) segment._characterClass;
                        hashCode1 = (hashCode1 * 397) ^ segment._length;
                        hashCode1 = (hashCode1 * 397) ^ (int) segment._lengthIndication;
                        
                        hashCode = (hashCode * 397) ^ hashCode1;
#else
                        hashCode = (hashCode * 397) ^ segment.GetHashCode();
#endif
                    }
                    return hashCode;
                }
            }
        }

    }
}