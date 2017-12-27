using Enban.Text;

namespace Enban
{
    public class Country
    {
        /// <summary>
        /// IBAN prefix country code (ISO 3166)
        /// </summary>
        public string Code { get; }

        public string Name { get; }

        internal AccountNumberFormatInfo AccountNumberFormatInfo { get; }

        internal Country(string code, string name, AccountNumberFormatInfo accountNumberFormatInfo)
        {
            Code = code;
            Name = name;
            AccountNumberFormatInfo = accountNumberFormatInfo;
        }

        public override string ToString()
        {
            return Code;
        }
    }
}