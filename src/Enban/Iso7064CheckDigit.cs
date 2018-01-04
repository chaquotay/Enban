using System.Numerics;
using System.Text.RegularExpressions;

namespace Enban
{
    internal class Iso7064CheckDigit
    {
        public static Iso7064CheckDigit Mod97 { get; } = new Iso7064CheckDigit(97);

        private readonly int _mod;

        public Iso7064CheckDigit(int mod)
        {
            _mod = mod;
        }

        public bool IsValid(string num)
        {
            var integer = ToNumber(num);
            var rest = integer % _mod;
            return rest.Equals(1);
        }

        public int Compute(string num)
        {
            var integer = ToNumber(num);
            var rest = integer % _mod;
            return (int) (98 - rest);
        }

        private static BigInteger ToNumber(string num)
        {
            num = Regex.Replace(num, "[A-Z]", m => (m.Value[0] - 'A' + 10).ToString());
            var integer = BigInteger.Parse(num);
            return integer;
        }
    }
}
