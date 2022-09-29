using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// International Bank Account Number (or short: IBAN).
    /// </summary>
    public class IBAN : IFormattable, IEquatable<IBAN>, IComparable<IBAN>, IComparable
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
            if (IBANPattern.ParseAndValidate(iban, IBANStyles.Lenient, IBANPattern.DefaultCountryAccountPatterns, out var cc,
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

        /// <summary>
        /// Constructs an IBAN from a given <paramref name="countryCode"/> and <paramref name="accountNumber"/>.
        /// The corresponding <see cref="CheckDigit"/> is calculated automatically.
        /// </summary>
        /// <param name="countryCode">ISO 3166-2 country code</param>
        /// <param name="accountNumber">the account number</param>
        /// <exception cref="ArgumentException">if the <paramref name="countryCode"/> is not supported or the <paramref name="accountNumber"/> does not conform to the account number format of the specified country.</exception>
        public IBAN(string countryCode, string accountNumber)
            : this(countryCode, null, accountNumber)
        {
            
        }

        /// <summary>
        /// Constructs an IBAN from a given <paramref name="countryCode"/>, <paramref name="checkDigit"/> and
        /// <paramref name="accountNumber"/>. The <paramref name="checkDigit"/> is not validated. In order to verify
        /// if the specified <paramref name="checkDigit"/> is valid you have to check <see cref="CheckDigitValid"/>.
        /// </summary>
        /// <param name="countryCode">ISO 3166-2 country code</param>
        /// <param name="checkDigit">the check digit</param>
        /// <param name="accountNumber">the account number</param>
        /// <exception cref="ArgumentException">if the <paramref name="countryCode"/> is not supported or the <paramref name="accountNumber"/> does not conform to the account number format of the specified country.</exception>
        public IBAN(string countryCode, int checkDigit, string accountNumber)
            : this(countryCode, (int?)checkDigit, accountNumber)
        {
        }

        private IBAN(string countryCode, int? checkDigit, string accountNumber)
        {
            if (!IBANPattern.DefaultCountryAccountPatterns.TryGetSegments(countryCode, out var segments))
            {
                throw new ArgumentException($"unsupported country code: {countryCode}");
            }

            if (!SegmentsMatcher.IsMatch(segments, accountNumber.ToCharArray()))
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
            return IBANPattern.Format(this, format);
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
        /// Tries to parse a given IBAN string using <see cref="TryParse(string,IBANStyles,out Enban.IBAN?)"/> with <see cref="IBANStyles.Lenient"/>.
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
            var isValid = IBANPattern.ParseAndValidate(text, style, IBANPattern.DefaultCountryAccountPatterns, out var cc, out var cd, out var ac, out _);
            
            if (isValid)
            {
                parsed = new IBAN(cc, cd, ac);
                return true;
            }

            parsed = null;
            return false;
        }
        
        public static bool operator ==(IBAN x, IBAN y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null))
                return false;

            if (ReferenceEquals(y, null))
                return false;

            return x.Equals(y);
        }

        public static bool operator !=(IBAN x, IBAN y)
        {
            return !(x == y);
        }

        /// <inheritdoc />
        public bool Equals(IBAN other)
        {
            return CheckDigit.Equals(other.CheckDigit) && CountryCode.Equals(other.CountryCode) &&
                   AccountNumber.Equals(other.AccountNumber);
        }

        public int CompareTo(IBAN other)
        {
            int cmp;
            
            cmp = string.Compare(CountryCode, other.CountryCode, StringComparison.Ordinal);
            if (cmp != 0)
                return cmp;

            cmp = CheckDigit.CompareTo(other.CheckDigit);
            if (cmp != 0)
                return cmp;
            
            cmp = String.Compare(AccountNumber, other.AccountNumber, StringComparison.Ordinal);
            if (cmp != 0)
                return cmp;
            
            return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is IBAN iban)
                return CompareTo(iban);

            throw new ArgumentException($"cannot compare {nameof(IBAN)} to {obj.GetType().FullName}");
        }

        public void Deconstruct(out string countryCode, out int checkDigit, out string accountNumber)
        {
            countryCode = CountryCode;
            checkDigit = CheckDigit;
            accountNumber = AccountNumber;
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
    }
}
