namespace Enban.Text
{
    public partial class IBANPattern
    {
        public static CountryAccountPatterns DefaultCountryAccountPatterns { get; } = new();

        static IBANPattern() => InitDefault();
        
        static partial void InitDefault();
    }
}