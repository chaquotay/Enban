using System;
using System.Collections.Generic;

namespace Enban.Text
{
    public partial class BICPattern
    {
        internal static bool DefaultIsKnownCountryCode(string code) => BIC.KnownCountryCodes.Contains(code);
        
        public static BICPattern Create(BICStyles styles = BICStyles.Lenient, string format="c", Predicate<string>? isKnownCountryCode = null)
        {
            return new BICPattern(styles, format, isKnownCountryCode ?? DefaultIsKnownCountryCode);
        }

        /// <summary>
        /// Creates a <see cref="BICPattern"/> for compact BICs using the default list of known country codes
        /// (see <see cref="BIC.KnownCountryCodes"/>)
        /// </summary>
        public static BICPattern Compact { get; } = CreateCompact();

        /// <summary>
        /// Creates a <see cref="BICPattern"/> for compact BICs using the given list of known country codes
        /// (<paramref name="allowedCountryCodes"/>)
        /// </summary>
        public static BICPattern CreateCompact(IEnumerable<string> allowedCountryCodes)
        {
            return CreateCompact(ToKnownCountryPredicate(allowedCountryCodes));
        }
        
        public static BICPattern CreateCompact(Predicate<string>? isKnownCountryCode = null)
        {
            return Create(BICStyles.Compact, "c", isKnownCountryCode);
        }
        
        /// <summary>
        /// Creates a <see cref="BICPattern"/> for full BICs using the default list of known country codes
        /// (see <see cref="BIC.KnownCountryCodes"/>)
        /// </summary>
        public static BICPattern Full { get; } = CreateFull();
        
        /// <summary>
        /// Creates a <see cref="BICPattern"/> for full BICs using the given list of known country codes
        /// (<paramref name="allowedCountryCodes"/>)
        /// </summary>
        public static BICPattern CreateFull(IEnumerable<string> allowedCountryCodes)
        {
            return CreateFull(ToKnownCountryPredicate(allowedCountryCodes));
        }
        private static BICPattern CreateFull(Predicate<string>? isKnownCountryCode = null)
        {
            return Create(BICStyles.Full, "f", isKnownCountryCode);
        }
        
        private static Predicate<string> ToKnownCountryPredicate(IEnumerable<string> allowedCountryCodes)
        {
            return new HashSet<string>(allowedCountryCodes, StringComparer.OrdinalIgnoreCase).Contains;
        }
    }
}