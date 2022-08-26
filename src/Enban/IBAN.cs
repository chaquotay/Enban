using System;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// International Bank Account Number (or short: IBAN).
    /// </summary>
    public struct IBAN : IFormattable, IEquatable<IBAN>
    {
        /// <summary>
        /// The check digit of the IBAN. The check digit might be wrong, e.g. if it was constructed by
        /// <see cref="IBANPattern.Parse">parsing an IBAN string</see> with an invalid check digit.
        /// 
        /// In order to verify the validity of the check digit retrieve the value of <see cref="CheckDigitValid"/>.
        /// </summary>
        public int CheckDigit { get; }

        /// <summary>
        /// The underlying <see cref="BBAN">basic bank account number</see>.
        /// </summary>
        public BBAN BBAN { get; }

        /// <summary>
        /// Constructs an IBAN, given an country's code, the actual bank account number and a check digit.
        /// The structure of the account number gets checked, and malformed account numbers throw an
        /// <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="countryCode">the country's ISO 3166-1 alpha-2 code</param>
        /// <param name="bankAccountNumber">the actual account number</param>
        /// <param name="checkDigit">the check digit</param>
        /// <exception cref="ArgumentException">if the account number is malformed</exception>
        public IBAN(string countryCode, string bankAccountNumber, int checkDigit)
            : this(new BBAN(countryCode, bankAccountNumber), checkDigit)
        {
            
        }

        /// <summary>
        /// Constructs an IBAN, given an existing BBAN and its check digit.
        /// </summary>
        /// <param name="bankAccountNumber">the actual account number and the associated country, represented as a <see cref="BBAN"/></param>
        /// <param name="checkDigit">the check digit</param>
        public IBAN(BBAN bankAccountNumber, int checkDigit)
        {
            CheckDigit = checkDigit;
            BBAN = bankAccountNumber;
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
            return ToString("G", null);
        }

        /// <summary>
        /// Verifies the validity of the check digit.
        /// </summary>
        public bool CheckDigitValid => Country != null && CheckDigitUtil.IsValid(Country.Code, AccountNumber, CheckDigit);

        /// <summary>
        /// The country associated with this IBAN (and its underlying <see cref="BBAN"/>).
        /// </summary>
        public Country Country => BBAN.Country;

        /// <summary>
        /// The code of the country associated with this IBAN (and its underlying <see cref="BBAN"/>).
        /// </summary>
        public string CountryCode => BBAN.Country.Code;

        /// <summary>
        /// The account number associated with this IBAN (and its underlying <see cref="BBAN"/>)
        /// </summary>
        public string AccountNumber => BBAN.AccountNumber;

        /// <inheritdoc />
        public bool Equals(IBAN other)
        {
            return CheckDigit.Equals(other.CheckDigit) && BBAN.Equals(other.BBAN);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is IBAN && Equals((IBAN) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (CheckDigit.GetHashCode() * 397) ^ BBAN.GetHashCode();
            }
        }
    }
}
