using Xunit;

namespace Enban.Test
{
    public class IBANTest
    {
        private Country Germany { get; }

        public IBANTest()
        {
            Germany = new Country("DE", "Germany", null);
        }

        [Fact]
        public void ToString_GivenPrintFormat_ThenPackedInSegmentsOfFour()
        {
            var iban = new BBAN(Germany, "370400440532013000").ToIBAN(new CheckDigit(89));

            Assert.Equal("DE89 3704 0044 0532 0130 00", iban.ToString("p", null));
        }

        [Fact]
        public void ToString_GivenElectonicFormat_ThenConcatenatedString()
        {
            var iban = new BBAN(Germany, "370400440532013000").ToIBAN(new CheckDigit(89));

            Assert.Equal("DE89370400440532013000", iban.ToString("e", null));
        }
    }
}