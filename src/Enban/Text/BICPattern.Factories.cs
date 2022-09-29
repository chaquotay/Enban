using System;
using System.Collections.Generic;

namespace Enban.Text
{
    public partial class BICPattern
    {
        internal static bool DefaultIsKnownCountryCode(string code) => DefaultCountryCodes.Contains(code);

        /// <summary>
        /// Creates a <see cref="BICPattern"/> for compact BICs using the default list of known country codes
        /// (see <see cref="DefaultCountryCodes"/>)
        /// </summary>
        public static BICPattern Compact { get; } = CreateCompact();

        /// <summary>
        /// Creates a <see cref="BICPattern"/> for compact BICs using the specified list of known country codes
        /// (<paramref name="allowedCountryCodes"/>)
        /// </summary>
        public static BICPattern CreateCompact(IEnumerable<string> allowedCountryCodes)
        {
            return CreateCompact(ToKnownCountryPredicate(allowedCountryCodes));
        }
        
        /// <summary>
        /// Creates a <see cref="BICPattern"/> for compact BICs using the specified predicate for known country code.
        /// </summary>
        public static BICPattern CreateCompact(Predicate<string>? isKnownCountryCode = null)
        {
            return new(BICStyles.Compact, "c", isKnownCountryCode);
        }
        
        /// <summary>
        /// Creates a <see cref="BICPattern"/> for full BICs using the default list of known country codes
        /// (see <see cref="DefaultCountryCodes"/>)
        /// </summary>
        public static BICPattern Full { get; } = CreateFull();
        
        /// <summary>
        /// Creates a <see cref="BICPattern"/> for full BICs using the specified list of known country codes
        /// (<paramref name="allowedCountryCodes"/>)
        /// </summary>
        public static BICPattern CreateFull(IEnumerable<string> allowedCountryCodes)
        {
            return CreateFull(ToKnownCountryPredicate(allowedCountryCodes));
        }
        
        /// <summary>
        /// Creates a <see cref="BICPattern"/> for full BICs using the specified predicate for known country code.
        /// </summary>
        /// <param name="isKnownCountryCode"></param>
        /// <returns></returns>
        private static BICPattern CreateFull(Predicate<string>? isKnownCountryCode = null)
        {
            return new(BICStyles.Full, "f", isKnownCountryCode);
        }

        internal static BICPattern Lenient { get; } = new(BICStyles.Lenient, "f", null);
        
        private static Predicate<string> ToKnownCountryPredicate(IEnumerable<string> allowedCountryCodes)
        {
            return new HashSet<string>(allowedCountryCodes, StringComparer.OrdinalIgnoreCase).Contains;
        }
    }
}