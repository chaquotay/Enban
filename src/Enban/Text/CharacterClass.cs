using System;
using System.Collections.Generic;
using System.Linq;

namespace Enban.Text
{
    internal readonly struct CharacterClass : IEquatable<CharacterClass>
    {
        private readonly short _classIndex;

        private static readonly char[] ClassSymbolsByIndex = { 'n','a','c','e' };

        private static readonly Dictionary<char, short> ClassIndicesBySymbol = ClassSymbolsByIndex
            .Select((s, i) => (Symbol: s, Index: (short)i))
            .ToDictionary(t => t.Symbol, t => t.Index);
        
        private static readonly string[] CodeExpressionsByIndex = new string[]{
            nameof(Digits),
            nameof(UpperCaseLetters),
            nameof(AlphanumericCharacters),
            nameof(BlankSpace),
        }.Select(m => $"{typeof(CharacterClass).FullName}.{m}").ToArray();

        private static readonly Predicate<char>[] ClassPredicatesByIndex =
        {
            IsDigit,
            IsUpperCaseLetters,
            IsAlphanumericCharacters,
            IsBlankSpace
        };

        public string CodeExpression => CodeExpressionsByIndex[_classIndex];

        private CharacterClass(short classIndex)
        {
            _classIndex = classIndex;
        }
        
        public override string ToString() => ClassSymbolsByIndex[_classIndex].ToString();

        public bool Contains(char c) => ClassPredicatesByIndex[_classIndex].Invoke(c);

        /// <summary>
        /// Digits (numeric characters 0 to 9 only), characters representation 'n'.
        /// </summary>
        public static readonly CharacterClass Digits = Create('n');

        private static bool IsDigit(char c)
        {
            return c is not (< '0' or > '9');
        }

        /// <summary>
        /// Upper case letters (alphabetic characters A-Z only), characters representation 'a'.
        /// </summary>
        public static readonly CharacterClass UpperCaseLetters = Create('a');

        private static bool IsUpperCaseLetters(char c)
        {
            return c is not (< 'A' or > 'Z');
        }

        /// <summary>
        /// Upper and lower case alphanumeric characters (A-Z, a-z and 0-9), characters representation 'c'.
        /// </summary>
        public static readonly CharacterClass AlphanumericCharacters = Create('c');

        private static bool IsAlphanumericCharacters(char c)
        {
            return c is not ((< '0' or > '9') and (< 'A' or > 'Z') and (< 'a' or > 'z'));
        }

        /// <summary>
        /// Blank space, characters representation 'e'.
        /// </summary>
        public static readonly CharacterClass BlankSpace = Create('e');

        private static bool IsBlankSpace(char c) => c == ' ';

        public static CharacterClass? FromSymbol(char symbol)
        {
            return ClassIndicesBySymbol.TryGetValue(symbol, out var index) ? new CharacterClass(index) : null;
        }

        private static CharacterClass Create(char symbol) => FromSymbol(symbol) ?? throw new ArgumentException($"unknown symbol: {symbol}", nameof(symbol));

        public static bool operator ==(CharacterClass x, CharacterClass y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(CharacterClass x, CharacterClass y)
        {
            return !(x == y);
        }

        public bool Equals(CharacterClass other) => _classIndex == other._classIndex;

        public override bool Equals(object? obj) => obj is CharacterClass characterClass && Equals(characterClass);

        public override int GetHashCode()
        {
            return _classIndex.GetHashCode();
        }
    }
}