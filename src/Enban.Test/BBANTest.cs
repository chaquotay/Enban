using Enban.Countries;
using Xunit;

namespace Enban.Test
{
    public class BBANTest
    {
        private Country Germany { get; }
        private ICountryProvider CountryProvider { get; }

        public BBANTest()
        {
            CountryProvider = new CountryProvider(new PregeneratedCountrySource());
            Germany = CountryProvider["DE"];
        }

        [Fact]
        public void ToIBAN_GivenNoExplicitCheckDigit_ComputesCorrectCheckDigit()
        {
            var iban = new BBAN(Germany, "210501700012345678").ToIBAN();
            Assert.Equal(new CheckDigit(68), iban.CheckDigit);
        }
    }
}
