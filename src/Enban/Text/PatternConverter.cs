using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Enban.Text
{
    internal static class PatternConverter // TODO: better name! Split into PatternParser and PatternInterpreter?
    {
        public static List<Tuple<char, int, bool>> ParsePattern(string patternText)
        {
            var pattern = new Regex("(?<COUNTRY>[A-Z]{2})((?<COUNT>[1-9][0-9]*!?)(?<CHAR>[nace]))+");
            var match = pattern.Match(patternText);
            if (match.Success)
            {
                var g = match.Groups["COUNT"];

                var counts = (
                    from Capture capture in g.Captures
                    let count = capture.Value
                    let isFixed = count.EndsWith("!")
                    let num = isFixed ? count.Substring(0, count.Length - 1) : count.Substring(0, count.Length)
                    let parsedNum = int.Parse(num)
                    select new Tuple<int, bool>(parsedNum, isFixed)
                    ).ToList();

                var chars = (
                    from Capture capture in match.Groups["CHAR"].Captures
                    select capture.Value[0]
                    ).ToList();

                return counts.Zip(chars, (cnt, chr) => new Tuple<char, int, bool>(chr, cnt.Item1, cnt.Item2)).ToList();
            }
            else
            {
                return null;
            }
        }

        private static readonly Dictionary<CharacterClass, string> ClassRegexes = new Dictionary<CharacterClass, string>
        {
            {CharacterClass.Digits, "[0-9]" },
            {CharacterClass.UpperCaseLetters, "[A-Z]" },
            {CharacterClass.AlphanumericCharacters, "[0-9A-Za-z]" },
            {CharacterClass.BlankSpace, "[ ]" }
        };

        public static bool IsMatch(string text, IEnumerable<Segment> segments)
        {
            var regex = new Regex("^" + ToBBANRegex(segments) +"$");
            return regex.IsMatch(text);
        }

        private static string ToBBANRegex(IEnumerable<Segment> segments)
        {
            var regexes = segments.Select(ToBBANRegex).ToList();
            return string.Join("", regexes);
        }

        private static string ToBBANRegex(Segment segment)
        {
            string charRegex = null;
            if (!ClassRegexes.TryGetValue(segment.CharacterClass, out charRegex))
            {
                throw new ArgumentException($"Unknown character class: {segment.CharacterClass}");
            }

            var count = segment.Length;
            if (count < 0)
            {
                throw new ArgumentException($"Invalid segment length: {count}");
            }

            string countRegex = null;
            if (segment.LengthIndication == LengthIndication.Fixed)
            {
                countRegex = "{" + count + "}";
            }
            else if (segment.LengthIndication == LengthIndication.Maximum)
            {
                countRegex = "{0," + count + "}";
            }
            else
            {
                throw new ArgumentException($"Unknown length indication: {segment.LengthIndication}");
            }

            var regex = charRegex + countRegex;
            return regex;
        }
    }
}