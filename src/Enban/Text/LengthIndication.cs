using System;

namespace Enban.Text
{
    /// <summary>
    /// Length indications in account number patterns, e.g. "4!n4!n12!c"
    /// </summary>
    internal readonly struct LengthIndication : IEquatable<LengthIndication>
    {
        private readonly bool _isFixed;
        private readonly int _length;
        public int Length => _length;
        
        private LengthIndication(int length, bool isFixed)
        {
            _length = length;
            _isFixed = isFixed;
        }

        public bool IsFixed => _isFixed;
        public bool IsMaximum => !_isFixed;

        public string CodeExpression => $"{typeof(LengthIndication).FullName}.{(_isFixed ? nameof(Fixed) : nameof(Maximum))}({_length})";
        
        public override string ToString()
        {
            return IsFixed ? _length + "!" : _length.ToString();
        }

        /// <summary>
        /// Fixed length, indicated in patterns by an exclamation mark, e.g. "4!n"
        /// </summary>
        public static LengthIndication Fixed(int length) => new(length, true);
        
        /// <summary>
        /// Maximum length, indicated in patterns by a missing exclamation mark, e.g. "4n"
        /// </summary>
        public static LengthIndication Maximum(int length) => new(length, false);
        
        public static bool operator ==(LengthIndication x, LengthIndication y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(LengthIndication x, LengthIndication y)
        {
            return !(x == y);
        }

        public bool Equals(LengthIndication other)
        {
            return Length == other.Length && _isFixed == other._isFixed;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj.GetType() == GetType() && Equals((LengthIndication)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_isFixed.GetHashCode() * 397) ^ Length;
            }
        }
    }
}