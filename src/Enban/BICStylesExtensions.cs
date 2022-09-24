namespace Enban
{
    internal static class BICStylesExtensions
    {
        public static bool HasFlagFast(this BICStyles value, BICStyles flag)
        {
            return (value & flag) != 0;
        }
    }
}