using System;
using System.Diagnostics;
using Enban.Text;
using Xunit;
using Xunit.Abstractions;

namespace Enban.Test.Text
{
    public class IBANPatternTest
    {
        private readonly ITestOutputHelper _output;

        public IBANPatternTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ParseAndFormat_GI()
        {
            var parsed = IBANPattern.Electronic.Parse("GI75NWBK000000007099453");
            Assert.True(parsed.Success);
            Assert.Equal("GI75 NWBK 0000 0000 7099 453", IBANPattern.Print.Format(parsed.Value));
        }

        [Fact]
        public void ParseAndFormat_CZ()
        {
            var parsed = IBANPattern.Electronic.Parse("CZ6508000000192000145399");
            Assert.True(parsed.Success);
            Assert.Equal("CZ65 0800 0000 1920 0014 5399", IBANPattern.Print.Format(parsed.Value));
        }

        [Fact]
        public void Parse_GivenValidGermanIBAN_ThenParsedCorrectly()
        {
            var parsed = IBANPattern.Electronic.Parse("DE89370400440532013000");
            Assert.True(parsed.Success);
            Assert.Equal("DE", parsed.Value.CountryCode);
            Assert.Equal(89, parsed.Value.CheckDigit);
            Assert.Equal("370400440532013000", parsed.Value.AccountNumber);
        }

        [Fact]
        public void Parse_GivenInvalidIBANTooLong_ThenParseFailure()
        {
            var notParsed = IBANPattern.Electronic.Parse("DE89370400440532013000X");
            Assert.False(notParsed.Success);
        }

        [Fact]
        public void Parse_GivenInvalidIBANTooShort_ThenParseFailure()
        {
            var notParsed = IBANPattern.Electronic.Parse("DE89370400440532013");
            Assert.False(notParsed.Success);
        }

        [Fact(Skip = "Explicit only")]
        //[Fact]
        public void Performance()
        {
            var sw = new Stopwatch();
            sw.Start();
            var dummy = 0;
            var count = 100000;
            var pattern = IBANPattern.Print;
            for (var i = 0; i < count; i++)
            {
                var iban = pattern.Parse("SE45 5000 0000 0583 9825 7466").GetValueOrThrow();
                var text = pattern.Format(iban);
                var isValid = iban.CheckDigitValid;
                
                if (text != null ^ isValid)
                {
                    dummy ^= text?.GetHashCode() ?? 0;
                }
            }

            sw.Stop();
            _output.WriteLine($"{sw.ElapsedMilliseconds}ms");
            _output.WriteLine($"{((double)sw.ElapsedMilliseconds) / count: #0.000}ms per IBAN");
            _output.WriteLine($"{count / ((double)sw.ElapsedMilliseconds): #0.000} IBANs per ms");
        }
    }
}