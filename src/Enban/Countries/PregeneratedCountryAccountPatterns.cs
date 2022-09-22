namespace Enban.Countries
{
    internal partial class PregeneratedCountryAccountPatterns
    {
        internal static CountryAccountPatterns Default { get; } = new();

        static PregeneratedCountryAccountPatterns()
        {
            InitDefault();
        }

        static partial void InitDefault();
    }
}