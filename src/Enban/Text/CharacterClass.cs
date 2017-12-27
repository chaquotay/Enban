namespace Enban.Text
{
    /// <summary>
    /// Character classes used in BBAN patterns, e.g. "4!n4!n12!c".
    /// </summary>
    internal enum CharacterClass
    {
        /// <summary>
        /// Digits (numeric characters 0 to 9 only), characters representation 'n'.
        /// </summary>
        Digits,

        /// <summary>
        /// Upper case letters (alphabetic characters A-Z only), characters representation 'a'.
        /// </summary>
        UpperCaseLetters,

        /// <summary>
        /// Upper and lower case alphanumeric characters (A-Z, a-z and 0-9), characters representation 'c'.
        /// </summary>
        AlphanumericCharacters,

        /// <summary>
        /// Blank space, characters representation 'e'.
        /// </summary>
        BlankSpace
    }
}