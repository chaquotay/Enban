using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Enban.Text
{
    /// <summary>
    /// A part of a pattern, basically a tuple consisting of character class and a length (fixed or maximum).
    /// </summary>
    internal struct Segment : IEquatable<Segment>
    {
        private readonly CharacterClass _characterClass;
        private readonly LengthIndication _lengthIndication;

        public CharacterClass CharacterClass => _characterClass;

        public LengthIndication LengthIndication => _lengthIndication;

        public Segment(CharacterClass characterClass, LengthIndication lengthIndication)
        {
            _characterClass = characterClass;
            _lengthIndication = lengthIndication;
        }

        /// <summary>
        /// Invokes <see cref="ToPattern"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToPattern();

        /// <summary>
        /// Converts this segment into the equivalent pattern string (e.g. <em>"4!n5!c"</em>)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string ToPattern() => $"{LengthIndication}{CharacterClass}";

        public bool Equals(Segment other)
        {
            return _characterClass == other._characterClass && _lengthIndication == other._lengthIndication;
        }

        public override bool Equals(object obj)
        {
            return obj is Segment other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _characterClass.GetHashCode();
                hashCode = (hashCode * 397) ^ _lengthIndication.GetHashCode();
                return hashCode;
            }
        }

        public static bool TryParse(string patternText, [NotNullWhen(true)] out Segment[]? segments)
        {
            segments = null;
            
            var pattern = new System.Text.RegularExpressions.Regex("((?<COUNT>[1-9][0-9]*!?)(?<CHAR>[nace]))+");
            var match = pattern.Match(patternText);
            if (!match.Success)
                return false;

            var g = match.Groups["COUNT"];

            var counts = (
                from System.Text.RegularExpressions.Capture capture in g.Captures
                let count = capture.Value
                let isFixed = count.EndsWith("!")
                let num = isFixed ? count.Substring(0, count.Length - 1) : count.Substring(0, count.Length)
                let parsedNum = short.Parse(num)
                select isFixed ? LengthIndication.Fixed(parsedNum) : LengthIndication.Maximum(parsedNum)
            ).ToList();

            var chars = (
                from System.Text.RegularExpressions.Capture capture in match.Groups["CHAR"].Captures
                let charClass = CharacterClass.FromSymbol(capture.Value[0])
                where charClass != null
                select (CharacterClass)charClass
            ).ToList();

            if (chars.Count != counts.Count) // invalid char class!?!
                return false;

            segments = counts.Zip(chars, (cnt, chr) => new Segment(chr, cnt)).ToArray();
            return true;
        }
    }
}