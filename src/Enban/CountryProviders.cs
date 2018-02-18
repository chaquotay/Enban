using Enban.Countries;

namespace Enban
{
    /// <summary>
    /// Access to <see cref="ICountryProvider"/> implementations.
    /// </summary>
    public static class CountryProviders
    {
        /// <summary>
        /// The only currently available <see cref="ICountryProvider"/>, which acts as the default.
        /// </summary>
        public static ICountryProvider Default { get; } = new CountryProvider(new PregeneratedCountrySource());
    }
}
