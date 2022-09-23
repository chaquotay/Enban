namespace Enban.Text
{
    public partial class IBANPattern
    {
        /// <summary>
        /// Default country account patterns, initialized from an internal account pattern source. 
        /// </summary>
        public static CountryAccountPatterns DefaultCountryAccountPatterns { get; } = new();

        static IBANPattern() => InitDefault();
        
        static partial void InitDefault();
    }
}