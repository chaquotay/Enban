using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// A collection of country-specific account patterns
    /// </summary>
    public class CountryAccountPatterns
    {
        private readonly Dictionary<string, Segment[]> _patterns;

        /// <summary>
        /// Constructs an empty instance
        /// </summary>
        public CountryAccountPatterns()
        {
            _patterns = new(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Constructs an instance and copies all pattern from the <paramref name="source"/> instance.
        /// </summary>
        /// <param name="source">the source instance</param>
        public CountryAccountPatterns(CountryAccountPatterns source)
        {
            _patterns = new Dictionary<string, Segment[]>(source._patterns, StringComparer.OrdinalIgnoreCase);
        }
        
        /// <summary>
        /// Adds a pattern like <em>8!n16!c</em> for a given country
        /// </summary>
        /// <param name="countryCode">ISO 3166-2 country code</param>
        /// <param name="accountPattern">account number pattern</param>
        /// <exception cref="ArgumentException">thrown if <paramref name="accountPattern"/> is not a valid pattern</exception>
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

        /// <summary>
        /// Gets the account number pattern associated with the specified <paramref name="countryCode"/>.
        /// </summary>
        /// <param name="countryCode">ISO 3166-2 country code</param>
        /// <param name="pattern">account number pattern</param>
        /// <returns><see langword="true" /> if the <see cref="CountryAccountPatterns" /> contains an element with the specified country code; otherwise, <see langword="false" />.</returns>
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

        /// <summary>
        /// Adds the specified <paramref name="pattern"/> for the specified country. 
        /// </summary>
        /// <param name="countryCode">ISO 3166-2 country code</param>
        /// <param name="pattern">account number pattern</param>
        /// <exception cref="ArgumentException">if <paramref name="pattern"/> is empty</exception>
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