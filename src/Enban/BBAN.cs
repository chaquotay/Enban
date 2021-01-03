using System;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// Basis Bank Account Number (or short: BBAN), consisting of a country and a bank account number, which
    /// adheres to the country's account number structure.
    /// </summary>
    public struct BBAN : IEquatable<BBAN>
    {
        /// <summary>
        /// The country associated with this BBAN.
        /// </summary>
        public Country Country { get; }
        
        /// <summary>
        /// The bank account number of this BBAN.
        /// </summary>
        public string AccountNumber { get; }

        /// <summary>
        /// Constructs a BBAN, given an ISO 3166-1 alpha-2 code and a bank account number, which is validated
        /// according to the country's account number structure.
        /// The country code is resolved using the <see cref="CountryProviders.Default">default country provider</see>.
        /// </summary>
        /// <param name="countryCode">the country's ISO 3166-1 alpha-2 code</param>
        /// <param name="bankAccountNumber">the actual bank account number</param>
        public BBAN(string countryCode, string bankAccountNumber)
            : this(CountryProviders.Default[countryCode], bankAccountNumber)
        {
            
        }

        /// <summary>
        /// Constructs a BBAN, given a country and a bank account number, which is validated
        /// according to the country's account number structure.
        /// </summary>
        /// <param name="country">the country</param>
        /// <param name="bankAccountNumber">the actual bank account number</param>
        public BBAN(Country country, string bankAccountNumber)
            : this(country, bankAccountNumber, true)
        {
            
        }

        internal BBAN(Country country, string bankAccountNumber, bool validate)
        {
            Country = country;

            if (string.IsNullOrEmpty(bankAccountNumber))
                throw new ArgumentException("null or empty", nameof(bankAccountNumber));

            if (validate)
            {
                var segments = Country.AccountNumberFormatInfo.StructureInfo.Segments
                    ?? throw new InvalidOperationException($"Cannot validate {nameof(BBAN)} without structural information about the account number of country {Country.Name}");

                if (!SegmentsMatcher.IsMatch(segments, bankAccountNumber.ToCharArray()))
                {
                    throw new ArgumentException($"Account number {bankAccountNumber} violates the pattern of account numbers of country {Country.Name}");
                }

            }

            AccountNumber = bankAccountNumber;
        }

        /// <summary>
        /// Constructs an <see cref="IBAN"/> for this BBAN (country and account number),
        /// suplemented by a valid check digit (according to ISO/IEC 7064 (MOD97-10)).
        /// </summary>
        /// <returns>the constructed IBAN</returns>
        public IBAN ToIBAN()
        {
            var checkDigit = CheckDigitUtil.Compute(Country.Code, AccountNumber);
            return ToIBAN(checkDigit);
        }

        /// <summary>
        /// Constructs an <see cref="IBAN"/> for this BBAN (country and account number),
        /// using a provided check digit (which can be invalid according to ISO/IEC 7064 (MOD97-10)).
        /// </summary>
        /// <param name="checkDigit">a check digit</param>
        /// <returns>the constructed IBAN</returns>
        public IBAN ToIBAN(int checkDigit)
        {
            return new IBAN(this, checkDigit);
        }

        /// <inheritdoc />
        public override string ToString() => AccountNumber;

        /// <inheritdoc />
        public bool Equals(BBAN other)
        {
            return Equals(Country.Code, other.Country.Code) && string.Equals(AccountNumber, other.AccountNumber);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is BBAN bban && Equals(bban);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Country.GetHashCode() * 397) ^ AccountNumber.GetHashCode();
            }
        }
    }
}