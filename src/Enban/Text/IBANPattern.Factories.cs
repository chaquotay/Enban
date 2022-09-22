namespace Enban.Text
{
    public partial class IBANPattern
    {
        /// <summary>
        /// Creates an electronic pattern instance, using the provided <see cref="ICountryProvider"/>.
        /// </summary>
        /// <param name="countryAccountPatterns">country account patterns</param>
        /// <returns>the constructed pattern instance</returns>
        public static IBANPattern CreateElectronic(CountryAccountPatterns? countryAccountPatterns = null) => 
            new(IBANStyles.Electronic, "e", countryAccountPatterns);

        /// <summary>
        /// Provides an electronic pattern instance, using the <see cref="IBAN.KnownCountryAccountPatterns">default country provider</see>.
        /// </summary>
        public static IBANPattern Electronic { get; } = CreateElectronic();

        /// <summary>
        /// Creates a print pattern instance, using the provided <see cref="ICountryProvider"/>.
        /// </summary>
        /// <param name="countryAccountPatterns">country account patterns</param>
        /// <returns>the constructed pattern instance</returns>
        public static IBANPattern CreatePrint(CountryAccountPatterns? countryAccountPatterns = null) => 
            new(IBANStyles.Print, "p", countryAccountPatterns);

        /// <summary>
        /// Provides a print pattern instance, using the <see cref="CountryProviders.Default">default country provider</see>.
        /// </summary>
        public static IBANPattern Print { get; } = CreatePrint();
    }
}