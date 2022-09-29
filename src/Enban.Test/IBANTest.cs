using System;
using System.Globalization;
using Enban.Text;
using Xunit;

namespace Enban.Test
{
    public class IBANTest
    {
        [Fact]
        public void ToString_GivenPrintFormat_ThenPackedInSegmentsOfFourWithSmallerLastSegment()
        {
            var iban = new IBAN("DE89370400440532013000");

            Assert.Equal("DE89 3704 0044 0532 0130 00", iban.ToString("p", CultureInfo.InvariantCulture));
        }

        [Fact]
        public void ToString_GivenPrintFormat_ThenPackedInSegmentsOfFour()
        {
            var iban = new IBAN("LU280019400644750000");

            Assert.Equal("LU28 0019 4006 4475 0000", iban.ToString("p", CultureInfo.InvariantCulture));
        }

        [Fact]
        public void ToString_GivenElectonicFormat_ThenConcatenatedString()
        {
            var iban = new IBAN("DE89370400440532013000");

            Assert.Equal("DE89370400440532013000", iban.ToString("e", CultureInfo.InvariantCulture));
        }

        [Fact]
        public void Construct_GivenCorrectFormat_ThenInstanceConstructed()
        {
            _ = new IBAN("DE89370400440532013000");
        }

        [Fact]
        public void Construct_GivenWrongFormat_ThenException()
        {
            Assert.Throws<ArgumentException>(() => new IBAN("DE89X70400440532013000"));
        }

        [Fact]
        public void CheckDigitValid_GivenCorrectCheckDigit_ThenTrue()
        {
            var iban = new IBAN("DE68210501700012345678");
            Assert.True(iban.CheckDigitValid);
        }

        [Fact]
        public void CheckDigitValid_GivenWrongCheckDigit_ThenFalse()
        {
            var iban = new IBAN("DE68210501709012345678");
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
            
            Assert.Null(defaultIban);
        }

        [Fact]
        public void Parse_NotAllowInvalidCheckDigit_WithInvalidCheckDigit()
        {
            var noInvalidCheckDigit = IBANStyles.Lenient & ~IBANStyles.AllowInvalidCheckDigit;
            var parsed = IBAN.TryParse("DE68210501709012345678", noInvalidCheckDigit, out _);

            Assert.False(parsed);
        }
        
        [Fact]
        public void Parse_NotAllowInvalidCheckDigit_WithValidCheckDigit()
        {
            var noInvalidCheckDigit = IBANStyles.Lenient & ~IBANStyles.AllowInvalidCheckDigit;
            var parsed = IBAN.TryParse("DE68210501700012345678", noInvalidCheckDigit, out var iban);

            Assert.True(parsed);
            Assert.True(iban.CheckDigitValid);
        }

        [Fact]
        public void Deconstruct()
        {
            var iban = new IBAN("DE68210501700012345678");
            var (countryCode, checkDigit, accountNumber) = iban;
            
            Assert.Equal("DE", countryCode);
            Assert.Equal(68, checkDigit);
            Assert.Equal("210501700012345678", accountNumber);
        }
    }
}