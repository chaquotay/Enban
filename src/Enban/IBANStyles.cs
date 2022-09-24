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
        /// Composite style which tries to be as "forgiving" as possible, e.g. by allowing white-spaces
        /// (<see cref="AllowWhite"/>), accepting lower-case letters (<see cref="IgnoreCase"/>) and ignoring invalid
        /// check digits (<see cref="AllowInvalidCheckDigit"/>).
        /// </summary>
        Lenient = IgnoreCase | AllowWhite | AllowInvalidCheckDigit,
        
        /// <summary>
        /// Accept lower-case letters.
        /// </summary>
        IgnoreCase = 1,
        
        /// <summary>
        /// Allow leading white space
        /// </summary>
        AllowLeadingWhite = 2,
        
        /// <summary>
        /// Allow intermediate white space
        /// </summary>
        AllowIntermediateWhite = 4,
        
        /// <summary>
        /// Allow trailing white space
        /// </summary>
        AllowTrailingWhite = 8,
        
        /// <summary>
        /// Allows invalid check digits
        /// </summary>
        AllowInvalidCheckDigit = 16,
        
        /// <summary>
        /// Strict style which only allows intermediate white-space used in the IBAN 'print' pattern
        /// </summary>
        Print = AllowIntermediateWhite,
        
        /// <summary>
        /// Strictest style which does not allow white-space or lower-case characters
        /// </summary>
        Electronic = 0,
        
        /// <summary>
        /// Composite style, combining all white-space styles (<see cref="AllowLeadingWhite"/>,
        /// <see cref="AllowIntermediateWhite"/> and <see cref="AllowTrailingWhite"/>). 
        /// </summary>
        AllowWhite = AllowLeadingWhite | AllowIntermediateWhite | AllowTrailingWhite
    }
}