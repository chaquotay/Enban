using Enban.Countries;

namespace Enban
{
    public static class CountryProviders
    {
        public static ICountryProvider Default { get; } = new CountryProvider(new PregeneratedCountrySource());
    }
}
