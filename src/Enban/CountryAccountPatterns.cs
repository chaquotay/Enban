using System;
using System.Collections.Generic;
using Enban.Text;

namespace Enban
{
    public class CountryAccountPatterns
    {
        private readonly Dictionary<string, AccountNumberFormatInfo> _patterns;

        public CountryAccountPatterns()
        {
            _patterns = new(StringComparer.OrdinalIgnoreCase);
        }

        public CountryAccountPatterns(CountryAccountPatterns source)
        {
            _patterns = new Dictionary<string, AccountNumberFormatInfo>(source._patterns, StringComparer.OrdinalIgnoreCase);
        }
        
        /// <summary>
        /// Adds a pattern like <em>8!n16!c</em> for a given country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="accountPattern"></param>
        public void Add(string countryCode, string accountPattern)
        {
            if (AccountNumberFormatInfo.TryParse(accountPattern, out var info))
            {
                Add(countryCode, info);
            }
            else
            {
                throw new ArgumentException($"invalid pattern: {accountPattern}", nameof(accountPattern));
            }
        }

        public string? GetPattern(string countryCode)
        {
            if (_patterns.TryGetValue(countryCode, out var info))
            {
                return info.ToPattern();
            }
            else
            {
                return null;
            }
        }

        internal void Add(string countryCode, AccountNumberFormatInfo pattern)
        {
            _patterns.Add(countryCode, pattern);
        }

        internal AccountNumberFormatInfo? Get(string countryCode) => _patterns.TryGetValue(countryCode, out var info) ? info : null;
    }
}