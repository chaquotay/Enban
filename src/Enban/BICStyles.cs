using System;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// Determines the styles permitted in BIC string arguments that are passed to <see cref="BICPattern.Parse" /> and
    /// <see cref="BIC.TryParse(string,BICStyles,out Enban.BIC?)" />
    /// </summary>
    [Flags]
    public enum BICStyles
    {
        /// <summary>
        /// Composite style which tries to be as "forgiving" as possible, e.g. by allowing leading and trailing
        /// white-spaces (<see cref="AllowWhite"/>), accepting lower-case letters (<see cref="IgnoreCase"/>), and not
        /// requiring or disallowing primary office branch codes ('XXX').
        /// </summary>
        Lenient = IgnoreCase | AllowWhite,
        
        /// <summary>
        /// Accept lower-case letters.
        /// </summary>
        IgnoreCase = 1,
        
        /// <summary>
        /// Allow leading white space
        /// </summary>
        AllowLeadingWhite = 2,
        
        /// <summary>
        /// Allow trailing white space
        /// </summary>
        AllowTrailingWhite = 8,
        
        /// <summary>
        /// Disallow primary office branch codes ('XXX' at the end of an eleven character BIC).
        /// </summary>
        DisallowPrimaryOfficeBranchCode = 32,
        
        /// <summary>
        /// Always require the full eleven letter BIC variant, even if the branch code is 'XXX' (the optional code for
        /// primary offices).
        /// </summary>
        RequirePrimaryOfficeBranchCode = 64,
        
        /// <summary>
        /// Strict compact format (no primary office branch code allowed)
        /// </summary>
        Compact = DisallowPrimaryOfficeBranchCode,
        
        /// <summary>
        /// Strict full format (primary office branch code always required)
        /// </summary>
        Full = RequirePrimaryOfficeBranchCode,
        
        /// <summary>
        /// Composite style, combining all white-space styles (<see cref="AllowLeadingWhite"/> and
        /// <see cref="AllowTrailingWhite"/>). 
        /// </summary>
        AllowWhite = AllowLeadingWhite | AllowTrailingWhite
    }
}