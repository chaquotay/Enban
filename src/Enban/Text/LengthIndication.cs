namespace Enban.Text
{
    /// <summary>
    /// Length indications in BBAN patterns, e.g. "4!n4!n12!c"
    /// </summary>
    internal enum LengthIndication
    {
        /// <summary>
        /// Fixed length, indicated in patterns by an exclamation mark, e.g. "4!n"
        /// </summary>
        Fixed,

        /// <summary>
        /// Maximum length, indicated in patterns by a missing exclamation mark, e.g. "4n"
        /// </summary>
        Maximum
    }
}