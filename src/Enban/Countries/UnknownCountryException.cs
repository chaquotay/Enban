using System;

namespace Enban.Countries
{
    class UnknownCountryException : Exception
    {
        public string CountryCode { get; }

        public UnknownCountryException(string countryCode)
        {
            CountryCode = countryCode;
        }

    }
}