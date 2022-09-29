using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Enban.Test
{
    public class BICTest
    {
        [Theory]
        [InlineData("DRESDEFF370", "DRESDEFF370")]
        [InlineData("  DRESDEFF370   ", "DRESDEFF370")]
        [InlineData("DRESDEFFXXX", "DRESDEFF")]
        [InlineData("DRESDEFF", "DRESDEFF")]
        public void TryParseValid(string text, string expecedString)
        {
            var parsed = BIC.TryParse(text, out var bic);
            Assert.True(parsed);
            Assert.Equal(expecedString, bic.ToString());
        }
        
        [Theory]
        [MemberData(nameof(InvalidBics))]
        public void ParseInvalid(string text)
        {
            var parsed = BIC.TryParse(text, BICStyles.Compact, out var bic);
            Assert.False(parsed);
            Assert.Null(bic);
        }
        
        [Theory]
        [MemberData(nameof(InvalidBics))]
        public void ConstructInvalid(string text)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _ = new BIC(text);
            });
        }

        [Theory]
        [MemberData(nameof(ValidBics))]
        public void ParseValid(string text, BICStyles bicStyles, string institutionCode, string countryCode, string locationCode, string branchCode)
        {
            BIC.TryParse(text, bicStyles, out var bic);
            
            Assert.NotNull(bic);
            Assert.Equal(institutionCode, bic.InstitutionCode);
            Assert.Equal(countryCode, bic.CountryCode);
            Assert.Equal(locationCode, bic.LocationCode);
            Assert.Equal(branchCode, bic.BranchCode);
        }
        
        [Fact]
        public void Deconstruct()
        {
            var iban = new BIC("DRESDEFFXXX");
            var (institutionCode, countryCode, locationCode, branchCode) = iban;
            
            Assert.Equal("DRES", institutionCode);
            Assert.Equal("DE", countryCode);
            Assert.Equal("FF", locationCode);
            Assert.Equal("XXX", branchCode);
        }
        
        public static IEnumerable<object[]> ValidBics()
        {
            return new List<object[]>
            {
                new object[]{ "DRESDEFF", BICStyles.Compact, "DRES", "DE", "FF", "XXX" },
                new object[]{ "DRESDEFF370", BICStyles.Compact, "DRES", "DE", "FF", "370"},
                new object[]{ "DRESDEFFXXX", BICStyles.Full, "DRES", "DE", "FF", "XXX"},
                new object[]{ "DRESDEFF370", BICStyles.Full, "DRES", "DE", "FF", "370"}
            };
        }


        public static IEnumerable<object[]> InvalidBics()
        {
            return new[]
            {
                "DRESDEFF37", // Too short
                "DRESDEFF3700", // Too long
                "_%&$DEFF370", // Invalid Institution Code
                "DRES99FF370", // Invalid Country Code
                "DRES_%FF370", // Invalid Country Code
                "DRESZZFF370", // Unknown Country Code
                "DRESDE_%370", // Invalid Location code
                "DRESDEFO370", // Invalid Location code (O not allowed as second char)
                "DRESDEFFXYZ", // Invalid Branch Code starting with X, but not all Xs
                "DRESDEFF_%$", // Invalid Branch Code
            }.Select(s => new object[]{s});
        }
    }
}