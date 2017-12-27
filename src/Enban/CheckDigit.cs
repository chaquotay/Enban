using System;

namespace Enban
{
    public struct CheckDigit : IEquatable<CheckDigit>
    {
        public int Value { get; }

        public CheckDigit(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString("00");
        }

        public static implicit operator CheckDigit(int value)
        {
            return new CheckDigit(value);
        }

        public bool Equals(CheckDigit other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is CheckDigit && Equals((CheckDigit) obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}