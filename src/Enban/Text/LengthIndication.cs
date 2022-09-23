using System;
using System.Runtime.CompilerServices;

namespace Enban.Text
{
    /// <summary>
    /// Length indications in account number patterns, e.g. "4!n4!n12!c"
    /// </summary>
    internal class LengthIndication : IEquatable<LengthIndication>
    {
        private readonly Mode _mode;
        public int Length { get; }

        private enum Mode
        {
            Fixed,
            Maximum
        }
        
        private LengthIndication(int length, Mode mode)
        {
            _mode = mode;
            Length = length;
        }

        public bool IsFixed => _mode == Mode.Fixed;
        public bool IsMaximum => _mode == Mode.Maximum;

        public string Name => $"{_mode}({Length})";
        
        public override string ToString()
        {
            return IsFixed ? Length + "!" : Length.ToString();
        }

        /// <summary>
        /// Fixed length, indicated in patterns by an exclamation mark, e.g. "4!n"
        /// </summary>
        public static LengthIndication Fixed(int length) => new(length, Mode.Fixed);
        
        /// <summary>
        /// Maximum length, indicated in patterns by a missing exclamation mark, e.g. "4n"
        /// </summary>
        public static LengthIndication Maximum(int length) => new(length, Mode.Maximum);
        
        public static bool operator ==(LengthIndication? x, LengthIndication? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return x.Length == y.Length && x._mode == y._mode;
        }

        public static bool operator !=(LengthIndication? x, LengthIndication? y)
        {
            return !(x == y);
        }

        public bool Equals(LengthIndication? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _mode == other._mode && Length == other.Length;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LengthIndication)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)_mode * 397) ^ Length;
            }
        }
    }
}