using Xunit;

namespace Enban.Test
{
    public class BBANTest
    {
        [Fact]
        public void ToIBAN_GivenNoExplicitCheckDigit_ComputesCorrectCheckDigit()
        {
            var iban = new BBAN("DE", "210501700012345678").ToIBAN();
            Assert.Equal(68, iban.CheckDigit);
        }
    }
}
