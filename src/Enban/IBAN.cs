using System;
using System.Text.RegularExpressions;

namespace Enban
{
    public struct IBAN : IFormattable, IEquatable<IBAN>
    {
        public CheckDigit CheckDigit { get; }
        public BBAN BBAN { get; }

        public IBAN(BBAN bankAccountNumber, CheckDigit checkDigit)
        {
            CheckDigit = checkDigit;
            BBAN = bankAccountNumber;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var x = Country.Code + CheckDigit.Value.ToString("00") + AccountNumber;
            if ("e".Equals(format) || string.IsNullOrEmpty(format) || "G".Equals(format))
            {
                // Electronic (no spaces)
                return x;
            }
            else if ("p".Equals(format))
            {
                // Print (with spaces after every forth character)
                return Regex.Replace(x, ".{4}(?!$)", m => m.Value + " ");
            }

            return null;
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