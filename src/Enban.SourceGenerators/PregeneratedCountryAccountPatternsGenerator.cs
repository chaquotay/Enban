using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.CodeAnalysis;

namespace Enban.SourceGenerators
{
    [Generator]
    public class PregeneratedCountryAccountPatternsGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var countriesPath = context.AdditionalFiles.FirstOrDefault(f => Path.GetFileName(f.Path) == "countries.xml")?.Path;
            if (countriesPath != null)
            {
                var source = new StringBuilder();
                source.AppendLine("using System.Collections.Generic;");
source.AppendLine($"// {DateTime.Now:G}");
source.AppendLine("namespace Enban.Countries {");
source.AppendLine("");
source.AppendLine("    internal partial class PregeneratedCountryAccountPatterns {");
source.AppendLine("");
source.AppendLine("        static partial void InitDefault() {");

foreach (var countryNode in GetCountries(countriesPath))
{
    source.AppendLine($"        // " + countryNode.GetAttribute("bban-structure"));
    source.AppendLine($"        Default.Add(\"" + countryNode.GetAttribute("code") + "\", ");
    source.AppendLine($"            new Text.AccountNumberFormatInfo{{StructureInfo = new Text.StructureInfo(0, new List<Text.Segment>{{");

    foreach (var segment in ParsePattern(countryNode.GetAttribute("bban-structure")))
    {
        var cc = "Unknown";
        if (segment.Item1 == 'n')
        {
            cc = "Digits";
        }
        else if (segment.Item1 == 'c')
        {
            cc = "AlphanumericCharacters";
        }
        else if (segment.Item1 == 'a')
        {
            cc = "UpperCaseLetters";
        }
        else if (segment.Item1 == 'e')
        {
            cc = "BlankSpace";
        }

        source.AppendLine($"         new Text.Segment(Text.CharacterClass.{cc}, {segment.Item2}, Text.LengthIndication.{(segment.Item3 ? "Fixed" : "Maximum")}),");
    }
    
    source.AppendLine($"            }})}});");
}


source.AppendLine("        }");
source.AppendLine("    }");
source.AppendLine("}");
            
            
            
                context.AddSource("PregeneratedCountryAccountPatterns.g.cs", source.ToString());
            }
        }
        
        private List<XmlElement> GetCountries(string filePath)
        {
            var dict = new List<XmlElement>();
            var doc = new XmlDocument();
            doc.Load(filePath);
            var formatNodes = doc.SelectNodes("/countries/country")?.OfType<XmlElement>();

            if (formatNodes != null)
            {
                foreach (XmlElement formatNode in formatNodes)
                {
                    dict.Add(formatNode);
                }
            }
            return dict;
        }
        
        public List<Tuple<char, int, bool>> ParsePattern(string patternText)
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

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
    
}