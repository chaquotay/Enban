using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Enban.Countries;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// International Bank Account Number (or short: IBAN).
    /// </summary>
    public class IBAN : IFormattable, IEquatable<IBAN>
    {
        /// <summary>
        /// The check digit of the IBAN. The check digit might be wrong, e.g. if it was constructed by
        /// <see cref="IBANPattern.Parse">parsing an IBAN string</see> with an invalid check digit.
        /// 
        /// In order to verify the validity of the check digit retrieve the value of <see cref="CheckDigitValid"/>.
        /// </summary>
        public int CheckDigit { get; }

        /// <summary>
        /// The bank account number
        /// </summary>
        public string AccountNumber { get; }
        
        /// <summary>
        /// The <a href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</a> country code
        /// </summary>
        public string CountryCode { get; }

        /// <summary>
        /// Constructs an IBAN. The structure of the account number gets checked, and malformed account numbers throw an
        /// <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="iban">the IBAN</param>
        /// <exception cref="ArgumentException">if the account number is malformed</exception>
        public IBAN(string iban)
        {
            if (IBANPattern.ParseAndValidate(iban, IBANStyles.Lenient, KnownCountryAccountPatterns, out var cc,
                    out var cd, out var ac, out var error))
            {
                CountryCode = cc;
                CheckDigit = cd;
                AccountNumber = ac;
            }
            else
            {
                throw new ArgumentException(error, nameof(iban));
            }
        }

        public IBAN(string countryCode, string accountNumber)
            : this(countryCode, null, accountNumber)
        {
            
        }

        public IBAN(string countryCode, int checkDigit, string accountNumber)
            : this(countryCode, (int?)checkDigit, accountNumber)
        {
        }

        private IBAN(string countryCode, int? checkDigit, string accountNumber)
        {
            var formatInfo = KnownCountryAccountPatterns.Get(countryCode);
            if (formatInfo == null)
            {
                throw new ArgumentException($"unsupported country code: {countryCode}");
            }

            if (!SegmentsMatcher.IsMatch(formatInfo.Segments, accountNumber.ToCharArray()))
            {
                throw new ArgumentException($"invalid account number format: {accountNumber}");
            }
            
            CountryCode = countryCode;
            CheckDigit = checkDigit ?? CheckDigitUtil.Compute(countryCode, accountNumber);
            AccountNumber = accountNumber;
        }

        /// <inheritdoc />
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var pattern = IBANPattern.Electronic;

            if ("e".Equals(format) || string.IsNullOrEmpty(format) || "G".Equals(format))
            {
                pattern = IBANPattern.Electronic;
            }
            else if ("p".Equals(format))
            {
                pattern = IBANPattern.Print;
            }

            return pattern.Format(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Verifies the validity of the check digit.
        /// </summary>
        public bool CheckDigitValid => CheckDigitUtil.IsValid(CountryCode, AccountNumber, CheckDigit);
        
        /// <summary>
        /// Tries to parse a given IBAN string using <see cref="TryParse(string,IBANStyles,out System.Nullable{Enban.IBAN})"/> with <see cref="IBANStyles.Lenient"/>.
        /// </summary>
        /// <param name="text">the BIC string to parse</param>
        /// <param name="parsed">the parsed BIC, if the format is valid</param>
        /// <returns>whether <paramref name="text"/> was parsed sucessfully</returns>
        public static bool TryParse(string text, [NotNullWhen(true)] out IBAN? parsed)
        {
            return TryParse(text, IBANStyles.Lenient, out parsed);
        }
        
        /// <summary>
        /// Tries to parse a given BIC string.
        /// </summary>
        /// <param name="text">the BIC string to parse</param>
        /// <param name="style"></param>
        /// <param name="parsed">the parsed BIC, if the format is valid</param>
        /// <returns>whether <paramref name="text"/> was parsed sucessfully</returns>
        public static bool TryParse(string text, IBANStyles style, [NotNullWhen(true)] out IBAN? parsed)
        {
            var isValid = IBANPattern.ParseAndValidate(text, style, KnownCountryAccountPatterns, out var cc, out var cd, out var ac, out _);
            
            if (isValid)
            {
                parsed = new IBAN(cc, cd, ac);
                return true;
            }

            parsed = null;
            return false;
        }

        /// <inheritdoc />
        public bool Equals(IBAN other)
        {
            return CheckDigit.Equals(other.CheckDigit) && CountryCode.Equals(other.CountryCode) &&
                   AccountNumber.Equals(other.AccountNumber);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is IBAN iban && Equals(iban);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CountryCode.GetHashCode();
                hashCode = (hashCode * 397) ^ CheckDigit.GetHashCode();
                hashCode = (hashCode * 397) ^ AccountNumber.GetHashCode();
                return hashCode;
            }
        }

        public static CountryAccountPatterns KnownCountryAccountPatterns { get; } = new(PregeneratedCountryAccountPatterns.Default);
    }
}
