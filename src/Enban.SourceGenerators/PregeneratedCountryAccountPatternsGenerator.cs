using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Enban.Text;
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
source.AppendLine("namespace Enban.Text {");
source.AppendLine("");
source.AppendLine("    public partial class IBANPattern {");
source.AppendLine("");
source.AppendLine("        static partial void InitDefault() {");

foreach (var countryNode in GetCountries(countriesPath))
{
    source.AppendLine($"        // " + countryNode.GetAttribute("bban-structure"));
    source.AppendLine($"        DefaultCountryAccountPatterns.Add(\"" + countryNode.GetAttribute("code") + "\", ");

    if (Segment.TryParse(countryNode.GetAttribute("bban-structure"), out var segments))
    {
        var segmentsText = string.Join(",\r\n",
            segments.Select(segment =>
                $"new Text.Segment(Text.CharacterClass.{segment.CharacterClass}, {segment.Length}, Text.LengthIndication.{segment.LengthIndication})"));
        source.AppendLine(segmentsText);
    }
    
    source.AppendLine($"            );");
}


source.AppendLine("        }");
source.AppendLine("    }");
source.AppendLine("}");
            
            
            
                context.AddSource("IBANPattern.g.cs", source.ToString());
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
        

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
    
}