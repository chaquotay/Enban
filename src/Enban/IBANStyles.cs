using System;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// Determines the styles permitted in IBAN string arguments that are passed to <see cref="IBANPattern.Parse" /> and
    /// <see cref="IBAN.TryParse(string,IBANStyles,out Enban.IBAN?)" />
    /// </summary>
    [Flags]
    public enum IBANStyles
    {
        /// <summary>
        /// Composite style which tries to be as "forgiving" as possible, e.g. by allowing leading and trailing
        /// white-spaces, accepting lower-case letters.
        /// </summary>
        Lenient = IgnoreCase | AllowLeadingWhite | AllowTrailingWhite | AllowIntermediateWhite | AllowInvalidCheckDigit,
        
        /// <summary>
        /// Accept lower-case letters.
        /// </summary>
        IgnoreCase = 8,
        
        /// <summary>
        /// Allow leading white space
        /// </summary>
        AllowLeadingWhite = 16,
        
        /// <summary>
        /// Allow trailing white space
        /// </summary>
        AllowTrailingWhite = 32,
        
        /// <summary>
        /// Allow intermediate white space
        /// </summary>
        AllowIntermediateWhite = 64,
        
        /// <summary>
        /// Allows invalid check digits
        /// </summary>
        AllowInvalidCheckDigit = 128,
        
        /// <summary>
        /// Strict style which only allows intermediate white-space used in the IBAN 'print' pattern
        /// </summary>
        Print = AllowIntermediateWhite,
        
        /// <summary>
        /// Strictest style which does not allow white-space or lower-case characters
        /// </summary>
        Electronic = 0,
    }
}