using System.Collections.Generic;

namespace Enban.Countries
{
    interface ICountrySource
    {
        IEnumerable<string> GetCodes();
        Country? ForCode(string code);
    }
}