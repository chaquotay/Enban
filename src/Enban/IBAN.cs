using System;
using System.Text.RegularExpressions;
using Enban.Text;

namespace Enban
{
    public struct IBAN : IFormattable, IEquatable<IBAN>
    {
        public int CheckDigit { get; }
        public BBAN BBAN { get; }

        public IBAN(string countryCode, string bankAccountNumber, int checkDigit)
            : this(new BBAN(countryCode, bankAccountNumber), checkDigit)
        {
            
        }

        public IBAN(BBAN bankAccountNumber, int checkDigit)
        {
            CheckDigit = checkDigit;
            BBAN = bankAccountNumber;
        }

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

        public override string ToString()
        {
            return ToString("G", null);
        }

        public bool CheckDigitValid => CheckDigitUtil.IsValid(Country.Code, AccountNumber, CheckDigit);

        public Country Country => BBAN.Country;

        public string AccountNumber => BBAN.AccountNumber;

        public bool Equals(IBAN other)
        {
            return CheckDigit.Equals(other.CheckDigit) && BBAN.Equals(other.BBAN);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is IBAN && Equals((IBAN) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (CheckDigit.GetHashCode() * 397) ^ BBAN.GetHashCode();
            }
        }
    }
}