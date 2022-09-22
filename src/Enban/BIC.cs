using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// A <em>Business Identifier Code</em> according to <a href="https://en.wikipedia.org/wiki/ISO_9362">ISO 9362</a>
    /// </summary>
    public class BIC : IFormattable, IEquatable<BIC>
    {
        /// <summary>
        /// The institution/bank code
        /// </summary>
        public string InstitutionCode { get; }
        
        /// <summary>
        /// The <a href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</a> country code
        /// </summary>
        public string CountryCode { get; }
        
        /// <summary>
        /// The location code
        /// </summary>
        public string LocationCode { get; }
        
        /// <summary>
        /// The branch code, or "XXX", if no explicit branch code was provided (typically for primary offices).
        /// </summary>
        public string BranchCode { get; }
        
        internal BIC(string institutionCode, string countryCode, string locationCode, string branchCode)
        {
            InstitutionCode = institutionCode;
            CountryCode = countryCode;
            LocationCode = locationCode;
            BranchCode = branchCode;
        }

        /// <summary>
        /// Constructs a BIC from a string. An <see cref="ArgumentException"/> will be thrown if the given string is not a valid BIC.
        /// </summary>
        /// <param name="bic">the BIC in string form</param>
        /// <exception cref="ArgumentException">if the given <paramref name="bic"/> is not a valid BIC</exception>
        public BIC(string bic)
        {
            var isValid = BICPattern.ParseAndValidate(bic, BICStyles.Lenient, BICPattern.DefaultIsKnownCountryCode, out var ic,
                out var cc, out var lc, out var bc, out var error);
            if (isValid)
            {
                InstitutionCode = ic;
                CountryCode = cc;
                LocationCode = lc;
                BranchCode = bc;
            }
            else
            {
                throw new ArgumentException("Invalid format: " + error);
            }
        }

        /// <summary>
        /// Formats this BIC according in the compact format (<see cref="BICPattern.Compact"/>) 
        /// </summary>
        /// <returns>the formatted BIC</returns>
        public override string ToString()
        {
            return BICPattern.Format(this, "c");
        }
        
        /// <summary>
        /// Formats this BIC according to the given format string.
        /// <list type="bullet">
        /// <item><paramref name="format"/> is <c>"F"</c> or <c>"f"</c>: format this BIC using <see cref="BICPattern.Full"/></item>
        /// <item><paramref name="format"/> is anything else (including <c>null</c>): format this BIC using <see cref="BICPattern.Compact"/></item>
        /// </list>
        /// </summary>
        /// <param name="format">format string</param>
        /// <param name="formatProvider">ignored</param>
        /// <returns>The formatted BIC</returns>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if ("F".Equals(format, StringComparison.OrdinalIgnoreCase))
                return BICPattern.Full.Format(this);

            return BICPattern.Compact.Format(this);
        }

        /// <summary>
        /// Tries to parse a given BIC string using <see cref="TryParse(string,BICStyles,out Enban.BIC)"/> with
        /// <see cref="BICStyles.Lenient"/>.
        /// </summary>
        /// <param name="text">the BIC string to parse</param>
        /// <param name="parsed">the parsed BIC, if the format is valid</param>
        /// <returns>whether <paramref name="text"/> was parsed sucessfully</returns>
        public static bool TryParse(string text, [NotNullWhen(true)] out BIC? parsed)
        {
            return TryParse(text, BICStyles.Lenient, out parsed);
        }
        
        /// <summary>
        /// Tries to parse a given BIC string.
        /// </summary>
        /// <param name="text">the BIC string to parse</param>
        /// <param name="style"></param>
        /// <param name="parsed">the parsed BIC, if the format is valid</param>
        /// <returns>whether <paramref name="text"/> was parsed sucessfully</returns>
        public static bool TryParse(string text, BICStyles style, [NotNullWhen(true)] out BIC? parsed)
        {
            var isValid = BICPattern.ParseAndValidate(text, style, BICPattern.DefaultIsKnownCountryCode, out var ic, out var cc,
                out var lc, out var bc, out _);
            
            if (isValid)
            {
                parsed = new BIC(ic, cc, lc, bc);
                return true;
            }

            parsed = null;
            return false;
        }
        
        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is BIC bic && Equals(bic);
        }

        /// <inheritdoc />
        public bool Equals(BIC other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return InstitutionCode == other.InstitutionCode && CountryCode == other.CountryCode && LocationCode == other.LocationCode && BranchCode == other.BranchCode;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = InstitutionCode.GetHashCode();
                hashCode = (hashCode * 397) ^ CountryCode.GetHashCode();
                hashCode = (hashCode * 397) ^ LocationCode.GetHashCode();
                hashCode = (hashCode * 397) ^ (BranchCode != null ? BranchCode.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        /// Collection of known country codes.
        /// </summary>
        public static ISet<string> KnownCountryCodes { get; } = new HashSet<string>(StringComparer.Ordinal)
        {
            // Official codes
            "AD",
            "AE",
            "AF",
            "AG",
            "AI",
            "AL",
            "AM",
            "AO",
            "AQ",
            "AR",
            "AS",
            "AT",
            "AU",
            "AW",
            "AX",
            "AZ",
            "BA",
            "BB",
            "BD",
            "BE",
            "BF",
            "BG",
            "BH",
            "BI",
            "BJ",
            "BL",
            "BM",
            "BN",
            "BO",
            "BQ",
            "BR",
            "BS",
            "BT",
            "BV",
            "BW",
            "BY",
            "BZ",
            "CA",
            "CC",
            "CD",
            "CF",
            "CG",
            "CH",
            "CI",
            "CK",
            "CL",
            "CM",
            "CN",
            "CO",
            "CR",
            "CU",
            "CV",
            "CW",
            "CX",
            "CY",
            "CZ",
            "DE",
            "DJ",
            "DK",
            "DM",
            "DO",
            "DZ",
            "EC",
            "EE",
            "EG",
            "EH",
            "ER",
            "ES",
            "ET",
            "FI",
            "FJ",
            "FK",
            "FM",
            "FO",
            "FR",
            "GA",
            "GB",
            "GD",
            "GE",
            "GF",
            "GG",
            "GH",
            "GI",
            "GL",
            "GM",
            "GN",
            "GP",
            "GQ",
            "GR",
            "GS",
            "GT",
            "GU",
            "GW",
            "GY",
            "HK",
            "HM",
            "HN",
            "HR",
            "HT",
            "HU",
            "ID",
            "IE",
            "IL",
            "IM",
            "IN",
            "IO",
            "IQ",
            "IR",
            "IS",
            "IT",
            "JE",
            "JM",
            "JO",
            "JP",
            "KE",
            "KG",
            "KH",
            "KI",
            "KM",
            "KN",
            "KP",
            "KR",
            "KW",
            "KY",
            "KZ",
            "LA",
            "LB",
            "LC",
            "LI",
            "LK",
            "LR",
            "LS",
            "LT",
            "LU",
            "LV",
            "LY",
            "MA",
            "MC",
            "MD",
            "ME",
            "MF",
            "MG",
            "MH",
            "MK",
            "ML",
            "MM",
            "MN",
            "MO",
            "MP",
            "MQ",
            "MR",
            "MS",
            "MT",
            "MU",
            "MV",
            "MW",
            "MX",
            "MY",
            "MZ",
            "NA",
            "NC",
            "NE",
            "NF",
            "NG",
            "NI",
            "NL",
            "NO",
            "NP",
            "NR",
            "NU",
            "NZ",
            "OM",
            "PA",
            "PE",
            "PF",
            "PG",
            "PH",
            "PK",
            "PL",
            "PM",
            "PN",
            "PR",
            "PS",
            "PT",
            "PW",
            "PY",
            "QA",
            "RE",
            "RO",
            "RS",
            "RU",
            "RW",
            "SA",
            "SB",
            "SC",
            "SD",
            "SE",
            "SG",
            "SH",
            "SI",
            "SJ",
            "SK",
            "SL",
            "SM",
            "SN",
            "SO",
            "SR",
            "SS",
            "ST",
            "SV",
            "SX",
            "SY",
            "SZ",
            "TC",
            "TD",
            "TF",
            "TG",
            "TH",
            "TJ",
            "TK",
            "TL",
            "TM",
            "TN",
            "TO",
            "TR",
            "TT",
            "TV",
            "TW",
            "TZ",
            "UA",
            "UG",
            "UM",
            "US",
            "UY",
            "UZ",
            "VA",
            "VC",
            "VE",
            "VG",
            "VI",
            "VN",
            "VU",
            "WF",
            "WS",
            "YE",
            "YT",
            "ZA",
            "ZM",
            "ZW",
            
            // Inofficial codes
            "XK", // (exceptionally, SWIFT has assigned the code XK to Republic of Kosovo, which does not have an ISO 3166-1 country code)
        };
    }
}