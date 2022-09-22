namespace Enban.Text
{
    public partial class IBANPattern
    {
        public static IBANPattern Create(IBANStyles styles = IBANStyles.Lenient, string format = "e",
            CountryAccountPatterns? countryAccountPatterns = null)
        {
            return new IBANPattern(styles, format, countryAccountPatterns ?? IBAN.KnownCountryAccountPatterns);
        }
        
        /// <summary>
        /// Creates an electronic pattern instance, using the provided <see cref="ICountryProvider"/>.
        /// </summary>
        /// <param name="countryAccountPatterns">country account patterns</param>
        /// <returns>the constructed pattern instance</returns>
        public static IBANPattern CreateElectronic(CountryAccountPatterns? countryAccountPatterns = null) => 
            Create(IBANStyles.Electronic, "e", countryAccountPatterns);

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
            Create(IBANStyles.Print, "p", countryAccountPatterns);

        /// <summary>
        /// Provides a print pattern instance, using the <see cref="CountryProviders.Default">default country provider</see>.
        /// </summary>
        public static IBANPattern Print { get; } = CreatePrint(IBAN.KnownCountryAccountPatterns);
    }
}