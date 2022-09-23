using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Enban.Text
{
    internal class CharacterClass : IEquatable<CharacterClass>
    {
        public char Symbol { get; }
        public string Name { get; }

        private static readonly Dictionary<char, CharacterClass> CharacterClasses = new();

        private CharacterClass(char symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }

        public override string ToString() => Symbol.ToString();

        /// <summary>
        /// Digits (numeric characters 0 to 9 only), characters representation 'n'.
        /// </summary>
        public static readonly CharacterClass Digits = CreateAndRegister('n');
        
        /// <summary>
        /// Upper case letters (alphabetic characters A-Z only), characters representation 'a'.
        /// </summary>
        public static readonly CharacterClass UpperCaseLetters = CreateAndRegister('a');
        
        /// <summary>
        /// Upper and lower case alphanumeric characters (A-Z, a-z and 0-9), characters representation 'c'.
        /// </summary>
        public static readonly CharacterClass AlphanumericCharacters = CreateAndRegister('c');
        
        /// <summary>
        /// Blank space, characters representation 'e'.
        /// </summary>
        public static readonly CharacterClass BlankSpace = CreateAndRegister('e');

        public static CharacterClass? FromSymbol(char symbol)
        {
            return CharacterClasses.TryGetValue(symbol, out var cc) ? cc : null;
        }

        private static CharacterClass CreateAndRegister(char symbol, [CallerMemberName] string? name = null)
        {
            if (name==null || string.IsNullOrEmpty(name))
                throw new ArgumentException("name is empty", nameof(name));
            
            return CharacterClasses[symbol] = new CharacterClass(symbol, name);
        }

        public static bool operator ==(CharacterClass? x, CharacterClass? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return x.Symbol == y.Symbol;
        }

        public static bool operator !=(CharacterClass? x, CharacterClass? y)
        {
            return !(x == y);
        }

        public bool Equals(CharacterClass? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Symbol == other.Symbol;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CharacterClass)obj);
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }
    }
}