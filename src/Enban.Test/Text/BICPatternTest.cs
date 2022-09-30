using Enban.Text;
using Xunit;

namespace Enban.Test.Text
{
    public class BICPatternTest
    {
        [Theory]
        [InlineData("DRESDEFF370", "c", "DRESDEFF370")]
        [InlineData("DRESDEFF", "c", "DRESDEFF")]
        [InlineData("DRESDEFFXXX", "c", "DRESDEFF")]
        [InlineData("DRESDEFF370", "f", "DRESDEFF370")]
        [InlineData("DRESDEFFXXX", "f", "DRESDEFFXXX")]
        [InlineData("DRESDEFF", "f", "DRESDEFFXXX")]
        [InlineData("DRESDEFF370", "o", "DRESDEFF370")]
        [InlineData("DRESDEFFXXX", "o", "DRESDEFFXXX")]
        [InlineData("DRESDEFF", "o", "DRESDEFF")]
        public void Format(string bic, string format, string expectedText)
        {
            var parsed = BIC.TryParse(bic, BICStyles.Lenient, out var value);
            
            Assert.True(parsed);
            Assert.Equal(expectedText, BICPattern.Format(value, format));
        }
        
        [Theory]
        [InlineData("DRESDEFF370", false, "DRESDEFF370")]
        [InlineData("DRESDEFF", false, "DRESDEFF")]
        [InlineData("DRESDEFF370", true, "DRESDEFF370")]
        [InlineData("DRESDEFFXXX", true, "DRESDEFF")]
        public void ParseValid(string bic, bool fullPattern, string expectedText)
        {
            var parsed = (fullPattern ? BICPattern.Full : BICPattern.Compact).Parse(bic);

            Assert.True(parsed.Success);
            Assert.Equal(expectedText, parsed.Value.ToString("c", null));
        }
        
        [Theory]
        [InlineData("", false)]
        [InlineData("", true)]
        [InlineData(null, false)]
        [InlineData(null, true)]
        [InlineData("DRESDEFF37", false)] // Wrong Length
        [InlineData("DRESDEFF37", true)] // Wrong Length
        [InlineData("DRESDEFF3700", false)] // Wrong Length
        [InlineData("DRESDEFF3700", true)] // Wrong Length
        [InlineData("DRESZZFF370", false)] // Invalid Country Code
        [InlineData("DRESZZFF370", true)] // Invalid Country Code
        [InlineData("DRES99FF370", false)] // Invalid Country Code
        [InlineData("DRES99FF370", true)] // Invalid Country Code
        [InlineData("DRESDEFFXYZ", false)] // Branch Code starting with X, but not all Xs
        [InlineData("DRESDEFFXYZ", true)] // Branch Code starting with X, but not all Xs
        [InlineData("DRESDEFO370", false)] // O not allowed as second char in location code
        [InlineData("DRESDEFO370", true)] // O not allowed as second char in location code
        [InlineData("DRESDEFFXXX", false)]
        [InlineData("DRESDEFF", true)]
        public void ParseInvalid(string bic, bool fullPattern)
        {
            var parsed = (fullPattern ? BICPattern.Full : BICPattern.Compact).Parse(bic);
            
            Assert.False(parsed.Success);
        }
    }
}