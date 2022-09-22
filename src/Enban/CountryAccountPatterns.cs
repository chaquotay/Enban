using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Enban.Text;

namespace Enban
{
    public class CountryAccountPatterns
    {
        private readonly Dictionary<string, Segment[]> _patterns;

        public CountryAccountPatterns()
        {
            _patterns = new(StringComparer.OrdinalIgnoreCase);
        }

        public CountryAccountPatterns(CountryAccountPatterns source)
        {
            _patterns = new Dictionary<string, Segment[]>(source._patterns, StringComparer.OrdinalIgnoreCase);
        }
        
        /// <summary>
        /// Adds a pattern like <em>8!n16!c</em> for a given country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="accountPattern"></param>
        public void Add(string countryCode, string accountPattern)
        {
            if (Segment.TryParse(accountPattern, out var segments))
            {
                Add(countryCode, segments);
            }
            else
            {
                throw new ArgumentException($"invalid pattern: {accountPattern}", nameof(accountPattern));
            }
        }

        public bool TryGetPattern(string countryCode, [NotNullWhen(true)] out string? pattern)
        {
            if (_patterns.TryGetValue(countryCode, out var info))
            {
                pattern = info.ToPattern();
                return true;
            }
            else
            {
                pattern = null;
                return false;
            }
        }

        internal void Add(string countryCode, params Segment[] pattern)
        {
            if (pattern.Length == 0)
                throw new ArgumentException("empty pattern array", nameof(pattern));
            
            _patterns.Add(countryCode, pattern);
        }

        internal bool TryGetSegments(string countryCode, [NotNullWhen(true)] out Segment[]? segments)
        {
            return _patterns.TryGetValue(countryCode, out segments);
        }
    }
}