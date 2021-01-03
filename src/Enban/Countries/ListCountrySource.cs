using System.Collections.Generic;
using System.Linq;

namespace Enban.Countries
{
    internal class ListCountrySource : ICountrySource
    {
        private Dictionary<string, Country> _countries;

        public ListCountrySource(IEnumerable<Country> countries)
        {
            _countries = ToCountriesByCode(countries);
        }

        public ListCountrySource(params Country[] countries)
        {
            _countries = ToCountriesByCode(countries);
        }

        private static Dictionary<string, Country> ToCountriesByCode(IEnumerable<Country> countries)
        {
            return countries
                .GroupBy(c => c.Code)
                .Where(g => g.Count() == 1)
                .ToDictionary(g => g.Key, g => g.Single());
        }

        public IEnumerable<string> GetCodes()
        {
            return _countries.Keys;
        }

        public Country? ForCode(string code)
        {
            _countries.TryGetValue(code, out var country);
            return country;
        }
    }
}
