using Xunit;

namespace Enban.Test
{
    public class IBANTest
    {
        [Fact]
        public void ToString_GivenPrintFormat_ThenPackedInSegmentsOfFour()
        {
            var iban = new BBAN("DE", "370400440532013000").ToIBAN(89);

            Assert.Equal("DE89 3704 0044 0532 0130 00", iban.ToString("p", null));
        }

        [Fact]
        public void ToString_GivenElectonicFormat_ThenConcatenatedString()
        {
            var iban = new BBAN("DE", "370400440532013000").ToIBAN(89);

            Assert.Equal("DE89370400440532013000", iban.ToString("e", null));
        }
    }
}