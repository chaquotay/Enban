using Enban.Text;
using Xunit;

namespace Enban.Test.Text
{
    public class IBANPatternTest
    {
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
            Assert.Equal(new BBAN("DE", "370400440532013000").ToIBAN(89), parsed.Value);
        }

        [Fact]
        public void Parse_GivenInvalidGermanIBAN_ThenParseFailure()
        {
            var notParsed = IBANPattern.Electronic.Parse("DE89370400440532013000X");
            Assert.False(notParsed.Success);
        }
    }
}