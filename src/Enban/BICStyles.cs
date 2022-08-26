using System;

namespace Enban
{
    [Flags]
    public enum BICStyles
    {
        /// <summary>
        /// Composite style which tries to be as "forgiving" as possible, e.g. by allowing leading and trailing
        /// white-spaces, accepting lower-case letters, and not requiring or disallowing primary office branch codes
        /// ('XXX').
        /// </summary>
        Lenient = IgnoreCase | AllowLeadingWhite | AllowTrailingWhite,
        
        /// <summary>
        /// Disallow primary office branch codes ('XXX' at the end of an eleven character BIC).
        /// </summary>
        DisallowPrimaryOfficeBranchCode = 1,
        
        /// <summary>
        /// Always require the full eleven letter BIC variant, even if the branch code is 'XXX' (the optional code for
        /// primary offices).
        /// </summary>
        RequirePrimaryOfficeBranchCode = 2,
        
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
        /// Strict compact format no primary office branch code allowed)
        /// </summary>
        Compact = DisallowPrimaryOfficeBranchCode,
        
        /// <summary>
        /// Strict full format (primary office branch code always required)
        /// </summary>
        Full = RequirePrimaryOfficeBranchCode,
    }
}