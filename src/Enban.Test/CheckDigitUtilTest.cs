using Xunit;

namespace Enban.Test
{
    public class CheckDigitUtilTest
    {
        [Fact]
        public void IsValid_OneDigitChecksum()
        {
            var bban = "0331234567890123456";
            var isValid = CheckDigitUtil.IsValid("AE", bban, 7);
            isValid = CheckDigitUtil.IsValid("BR", "00360305000010009795493C1", 18);

            Assert.True(isValid);
        }
    }
}
