using System;
using Enban.Text;

namespace Enban
{
    public struct BBAN : IEquatable<BBAN>
    {
        public Country Country { get; }
        public string AccountNumber { get; }

        public BBAN(string countryCode, string bankAccountNumber)
            : this(CountryProviders.Default[countryCode], bankAccountNumber)
        {
            
        }

        public BBAN(Country country, string bankAccountNumber)
            : this(country, bankAccountNumber, true)
        {
            
        }

        internal BBAN(Country country, string bankAccountNumber, bool validate)
        {
            Country = country ?? throw new ArgumentNullException(nameof(country));

            if (string.IsNullOrEmpty(bankAccountNumber))
                throw new ArgumentException("null or empty", nameof(bankAccountNumber));

            if (validate)
            {
                var segments = Country.AccountNumberFormatInfo?.StructureInfo?.Segments
                    ?? throw new InvalidOperationException($"Cannot validate {nameof(BBAN)} without structural information about the account number of country {Country.Name}");

                if (!PatternConverter.IsMatch(bankAccountNumber, segments))
                {
                    throw new ArgumentException($"Account number {bankAccountNumber} violates the pattern of account numbers of country {Country.Name}");
                }

            }

            AccountNumber = bankAccountNumber;
        }

        public IBAN ToIBAN()
        {
            var checkDigit = CheckDigitUtil.Compute(Country.Code, AccountNumber);
            return ToIBAN(checkDigit);
        }

        public IBAN ToIBAN(int checkDigit)
        {
            return new IBAN(this, checkDigit);
        }

        public override string ToString() => AccountNumber;

        public bool Equals(BBAN other)
        {
            return Equals(Country.Code, other.Country.Code) && string.Equals(AccountNumber, other.AccountNumber);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is BBAN && Equals((BBAN) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Country != null ? Country.GetHashCode() : 0) * 397) ^ (AccountNumber != null ? AccountNumber.GetHashCode() : 0);
            }
        }
    }
}