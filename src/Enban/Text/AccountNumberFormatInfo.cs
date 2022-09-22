using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Enban.Text
{
    /// <summary>
    /// Provides format information about account numbers (BBAN/IBAN).
    /// </summary>
    internal class AccountNumberFormatInfo
    {
        public AccountNumberFormatInfo(params Segment[] segments)
        {
            Segments = segments.ToList();
        }
        public List<Segment> Segments { get; }


        public static bool TryParse(string pattern, [NotNullWhen(true)] out AccountNumberFormatInfo? info)
        {
            info = null;

            var segments = new List<Segment>();
            
            foreach (var segment in ParsePattern(pattern))
            {
                CharacterClass? cc = null;
                if (segment.Item1 == 'n')
                {
                    cc = CharacterClass.Digits;
                }
                else if (segment.Item1 == 'c')
                {
                    cc = CharacterClass.AlphanumericCharacters;
                }
                else if (segment.Item1 == 'a')
                {
                    cc = CharacterClass.UpperCaseLetters;
                }
                else if (segment.Item1 == 'e')
                {
                    cc = CharacterClass.BlankSpace;
                }

                if (!cc.HasValue)
                {
                    return false;
                }
                
                segments.Add(new Segment(cc.Value, (short)segment.Item2, segment.Item3 ? LengthIndication.Fixed : LengthIndication.Maximum));
            }

            info = new AccountNumberFormatInfo();
            info.Segments.AddRange(segments);
            return true;
        } 
        
        private static List<Tuple<char, int, bool>> ParsePattern(string patternText)
        {
            var pattern = new System.Text.RegularExpressions.Regex("((?<COUNT>[1-9][0-9]*!?)(?<CHAR>[nace]))+");
            var match = pattern.Match(patternText);
            if (match.Success)
            {
                var g = match.Groups["COUNT"];

                var counts = (
                    from System.Text.RegularExpressions.Capture capture in g.Captures
                    let count = capture.Value
                    let isFixed = count.EndsWith("!")
                    let num = isFixed ? count.Substring(0, count.Length - 1) : count.Substring(0, count.Length)
                    let parsedNum = int.Parse(num)
                    select new Tuple<int, bool>(parsedNum, isFixed)
                ).ToList();

                var chars = (
                    from System.Text.RegularExpressions.Capture capture in match.Groups["CHAR"].Captures
                    select capture.Value[0]
                ).ToList();

                return counts.Zip(chars, (cnt, chr) => new Tuple<char, int, bool>(chr, cnt.Item1, cnt.Item2)).ToList();
            }
            else
            {
                return new List<Tuple<char, int, bool>>();
            }
        }

    }
}