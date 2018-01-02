using System;
using Enban.Countries;
using Enban.Text;
using Xunit;
using Xunit.Abstractions;

namespace Enban.Test
{
    public class Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestMethodGI()
        {
            var parsed = IBANPattern.Electronic.Parse("GI75NWBK000000007099453");
            Assert.True(parsed.Success);
            Assert.Equal("GI75 NWBK 0000 0000 7099 453", parsed.Value.ToString("p", null));
        }

        [Fact]
        public void TestMethodCZ()
        {
            var parsed = IBANPattern.Electronic.Parse("CZ6508000000192000145399");
            Assert.True(parsed.Success);
            Assert.Equal("CZ65 0800 0000 1920 0014 5399", parsed.Value.ToString("p", null));
        }

        [Fact]
        public void TestGermany()
        {
            var parsed = IBANPattern.Electronic.Parse("DE89370400440532013000");
            Assert.True(parsed.Success);
            Assert.Equal(new BBAN(CountryProviders.Default["DE"], "370400440532013000").ToIBAN(89), parsed.Value);

            var notParsed = IBANPattern.Electronic.Parse("DE89370400440532013000X");
            Assert.False(notParsed.Success);
        }

        [Fact]
        public void TestValid()
        {
            var iban = new BBAN(CountryProviders.Default["DE"], "210501700012345678").ToIBAN(68);
            Assert.True(iban.CheckDigitValid);

            var iban2 = new BBAN(CountryProviders.Default["DE"], "210501709012345678").ToIBAN(68);
            Assert.False(iban2.CheckDigitValid);
        }

        [Fact]
        public void UsageParseValidateFormat()
        {
            var parsed = IBANPattern.Electronic.Parse("GI75NWBK000000007099453");
            if (parsed.Success && parsed.Value.CheckDigitValid)
            {
                Console.WriteLine($"IBAN '{parsed.Value:p}' is valid!");
            }
        }

        [Fact]
        public void UsageConstructIBAN()
        {
            var germany = CountryProviders.Default["DE"];
            var iban = new BBAN(germany, "210501700012345678").ToIBAN();

            Console.WriteLine($"IBAN with check digit (valid: {iban.CheckDigitValid}): " + IBANPattern.Print.Format(iban));
        }

        private ITestOutputHelper Console => _testOutputHelper;
    }
}