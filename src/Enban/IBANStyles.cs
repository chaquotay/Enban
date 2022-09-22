using System;

namespace Enban
{
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
        
        AllowInvalidCheckDigit = 128,
        
        Print = AllowIntermediateWhite,
        
        Electronic = 0,
    }
}