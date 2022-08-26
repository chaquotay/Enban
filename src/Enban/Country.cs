using System;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// A country, identified by its <see href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2 code</see> (<see cref="Code"/>).
    /// This class is the source of the IBAN prefix, but also provides structural information about the <see cref="BBAN"/>.
    /// </summary>
    public class Country : IEquatable<Country>, IFormattable
    {
        /// <summary>
        /// IBAN prefix country code (ISO 3166)
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// The descriptive name of the country.
        /// </summary>
        public string Name { get; }

        internal AccountNumberFormatInfo AccountNumberFormatInfo { get; }

        internal Country(string code, string name, AccountNumberFormatInfo accountNumberFormatInfo)
        {
            Code = code;
            Name = name;
            AccountNumberFormatInfo = accountNumberFormatInfo;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Code;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if ("n".Equals(format, StringComparison.CurrentCultureIgnoreCase))
                return Name;

            return Code;
        }

        public bool Equals(Country other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Code == other.Code;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Country)obj);
        }

        public override int GetHashCode()
        {
            return (Code != null ? Code.GetHashCode() : 0);
        }
    }
}