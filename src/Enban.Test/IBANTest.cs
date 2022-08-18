using System;
using Enban.Text;
using Xunit;

namespace Enban.Test
{
    public class IBANTest
    {
        [Fact]
        public void ToString_GivenPrintFormat_ThenPackedInSegmentsOfFourWithSmallerLastSegment()
        {
            var iban = new BBAN("DE", "370400440532013000").ToIBAN(89);

            Assert.Equal("DE89 3704 0044 0532 0130 00", iban.ToString("p", null));
        }

        [Fact]
        public void ToString_GivenPrintFormat_ThenPackedInSegmentsOfFour()
        {
            var iban = new BBAN("LU", "0019400644750000").ToIBAN(28);

            Assert.Equal("LU28 0019 4006 4475 0000", iban.ToString("p", null));
        }

        [Fact]
        public void ToString_GivenElectonicFormat_ThenConcatenatedString()
        {
            var iban = new BBAN("DE", "370400440532013000").ToIBAN(89);

            Assert.Equal("DE89370400440532013000", iban.ToString("e", null));
        }

        [Fact]
        public void Construct_GivenCorrectFormat_ThenInstanceConstructed()
        {
            var iban = new IBAN("DE", "370400440532013000", 89);
        }

        [Fact]
        public void Construct_GivenWrongFormat_ThenException()
        {
            Assert.Throws<ArgumentException>(() => new IBAN("DE", "X70400440532013000", 89));
        }

        [Fact]
        public void CheckDigitValid_GivenCorrectCheckDigit_ThenTrue()
        {
            var iban = new BBAN("DE", "210501700012345678").ToIBAN(68);
            Assert.True(iban.CheckDigitValid);
        }

        [Fact]
        public void CheckDigitValid_GivenWrongCheckDigit_ThenFalse()
        {
            var iban = new BBAN("DE", "210501709012345678").ToIBAN(68);
            Assert.False(iban.CheckDigitValid);
        }

        [Theory]
        [InlineData("DE68210501700012345678")]
        [InlineData("XK051212012345678906")] // Check digit with leading zero
        public void CheckIsValid(string iban)
        {
            var parsed = IBANPattern.Print.Parse(iban);
            Assert.True(parsed.Success);
            Assert.True(parsed.Value.CheckDigitValid);
        }
        
        [Fact]
        public void DefaultValue()
        {
            var defaultIban = default(IBAN);
            
            Assert.Equal(defaultIban, defaultIban);
            Assert.Null(defaultIban.Country);
            Assert.Null(defaultIban.AccountNumber);
            Assert.Equal(0, defaultIban.CheckDigit);
            Assert.False(defaultIban.CheckDigitValid);
            _ = defaultIban.GetHashCode();
        }
    }
}