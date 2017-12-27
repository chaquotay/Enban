namespace Enban.Text
{
    /// <summary>
    /// A part of a pattern, basically a tuple consisting of character class, a length and a fixed/maximum flag.
    /// </summary>
    internal struct Segment
    {
        public CharacterClass CharacterClass { get; }
        public short Length { get; }
        public LengthIndication LengthIndication { get; }

        public Segment(CharacterClass characterClass, short length, LengthIndication lengthIndication)
        {
            CharacterClass = characterClass;
            Length = length;
            LengthIndication = lengthIndication;
        }
    }
}