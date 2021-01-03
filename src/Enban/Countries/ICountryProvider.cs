using System.Collections.ObjectModel;

namespace Enban.Countries
{
    public interface ICountryProvider
    {
        ReadOnlyCollection<string> Codes { get; }

        Country this[string code] { get; }

        Country? GetCountryOrNull(string code);
    }
}