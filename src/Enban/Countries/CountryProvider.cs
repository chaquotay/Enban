using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Enban.Countries
{
    class CountryProvider : ICountryProvider
    {
        private readonly Dictionary<string, Country> _countries;

        public CountryProvider(ICountrySource source)
        {
            var codes = source.GetCodes().Distinct().ToList();
            _countries =
                (from code in codes
                    let country = source.ForCode(code)
                    where country != null
                    select new {code, country})
                .ToDictionary(x => x.code, x => x.country);

            Codes = new ReadOnlyCollection<string>(_countries.Keys.ToList());
        }

        public ReadOnlyCollection<string> Codes { get; }

        public Country this[string code]
        {
            get
            {
                if (TryGetCountry(code, out var c))
                {
                    return c;
                }

                throw new UnknownCountryException(code);
            }
        }

        public Country? GetCountryOrNull(string code)
        {
            TryGetCountry(code, out var c);
            return c;
        }

        private bool TryGetCountry(string code, [NotNullWhen(true)] out Country? country)
        {
            if (!string.IsNullOrWhiteSpace(code) && _countries.TryGetValue(code, out var c) && c!=null)
            {
                country = c;
                return true;
            }

            country = null;
            return false;
        }
    }
}