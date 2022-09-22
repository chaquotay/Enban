using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Enban.Text
{
    /// <summary>
    /// Provides format information about account numbers.
    /// </summary>
    internal class AccountNumberFormatInfo
    {
        public AccountNumberFormatInfo(params Segment[] segments)
        {
            Segments = segments;
        }
        public Segment[] Segments { get; }
        
        public static bool TryParse(string patternText, [NotNullWhen(true)] out AccountNumberFormatInfo? info)
        {
            info = null;
            
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
                select new Tuple<short, LengthIndication>(parsedNum,
                    isFixed ? LengthIndication.Fixed : LengthIndication.Maximum)
            ).ToList();

            var chars = (
                from System.Text.RegularExpressions.Capture capture in match.Groups["CHAR"].Captures
                let charClass = ToChararcterClass(capture.Value[0])
                where charClass.HasValue
                select charClass.Value
            ).ToList();

            if (chars.Count != counts.Count) // invalid char class!?!
                return false;

            var segments = counts.Zip(chars, (cnt, chr) => new Segment(chr, cnt.Item1, cnt.Item2)).ToArray();
            info = new AccountNumberFormatInfo(segments);
            return true;

            CharacterClass? ToChararcterClass(char c)
            {
                return c switch
                {
                    'n' => CharacterClass.Digits,
                    'c' => CharacterClass.AlphanumericCharacters,
                    'a' => CharacterClass.UpperCaseLetters,
                    'e' => CharacterClass.BlankSpace,
                    _ => null
                };
            }
        }

    }
}