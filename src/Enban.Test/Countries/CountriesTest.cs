using System.Collections.Generic;
using System.Xml;
using Enban.Countries;
using Enban.Text;
using Xunit;

namespace Enban.Test.Countries
{
    public class CountriesTest
    {
        [Theory]
        [MemberData(nameof(Countries))]
        public void ParseElectronicIBAN(Example example)
        {
            var iban = IBANPattern.Electronic.Parse(example.IBANElectronic);

            Assert.True(iban.Success);
            Assert.Equal(example.BBAN, iban.Value.AccountNumber);
        }

        [Theory]
        [MemberData(nameof(Countries))]
        public void ParsePrintIBAN(Example example)
        {
            var iban = IBANPattern.Print.Parse(example.IBANPrint);

            Assert.True(iban.Success);
            Assert.Equal(example.BBAN, iban.Value.AccountNumber);
        }

        [Theory]
        [MemberData(nameof(Countries))]
        public void FormatElectronicIBAN(Example example)
        {
            var countries = CountryProviders.Default;

            var ibanText = new BBAN(countries[example.CountryCode], example.BBAN).ToIBAN().ToString("e", null);

            Assert.Equal(example.IBANElectronic, ibanText);
        }

        [Theory]
        [MemberData(nameof(Countries))]
        public void FormatPrintIBAN(Example example)
        {
            var countries = CountryProviders.Default;

            var ibanText = new BBAN(countries[example.CountryCode], example.BBAN).ToIBAN().ToString("p", null);

            Assert.Equal(example.IBANPrint, ibanText);
        }

        public class Example
        {
            public string CountryCode { get; set; }
            public string CountryName { get; set; }
            public string BBAN { get; set; }
            public string IBANElectronic { get; set; }
            public string IBANPrint { get; set; }
        }

        public static IEnumerable<object[]> Countries()
        {
            var doc = new XmlDocument();
            doc.Load(@"Countries\countries.xml");

            var countries = doc.SelectNodes("countries/country");
            if (countries != null)
            {
                foreach (XmlElement c in countries)
                {
                    var code = c.GetAttribute("code");
                    var name = c.GetAttribute("name");
                    var bban = c.SelectSingleNode("example/@bban")?.Value;
                    var ibanElectronic = c.SelectSingleNode("example/@iban-electronic")?.Value;
                    var ibanPrint = c.SelectSingleNode("example/@iban-print")?.Value;

                    if (!string.IsNullOrWhiteSpace(bban))
                    {
                        yield return new object[]{ new Example
                        {
                            CountryCode = code,
                            CountryName = name,
                            BBAN = bban,
                            IBANElectronic = ibanElectronic,
                            IBANPrint = ibanPrint
                        } };

                    }
                }
            }
        }
    }
}
