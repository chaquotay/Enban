using System;

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
        {
            Country = country ?? throw new ArgumentNullException(nameof(country));

            if(string.IsNullOrEmpty(bankAccountNumber))
                throw new ArgumentException("null or empty", nameof(bankAccountNumber));

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