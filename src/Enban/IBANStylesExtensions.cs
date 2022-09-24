namespace Enban
{
    internal static class IBANStylesExtensions
    {
        public static bool HasFlagFast(this IBANStyles value, IBANStyles flag)
        {
            return (value & flag) != 0;
        }
    }
}