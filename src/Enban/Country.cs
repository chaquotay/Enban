using Enban.Text;

namespace Enban
{
    /// <summary>
    /// A country, identified by its <see href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2 code</see> (<see cref="Code"/>).
    /// This class is the source of the IBAN prefix, but also provides structural information about the <see cref="BBAN"/>.
    /// </summary>
    public class Country
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
    }
}