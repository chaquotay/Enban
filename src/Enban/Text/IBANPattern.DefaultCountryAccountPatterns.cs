using Enban.Countries;

namespace Enban.Text
{
    public partial class IBANPattern
    {
        public static CountryAccountPatterns DefaultCountryAccountPatterns { get; } = new(PregeneratedCountryAccountPatterns.Default);
    }
}