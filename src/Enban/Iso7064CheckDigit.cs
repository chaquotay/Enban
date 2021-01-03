using System;
using System.Numerics;

namespace Enban
{
    internal class Iso7064CheckDigit
    {
        public static Iso7064CheckDigit Mod97 { get; } = new Iso7064CheckDigit(97);

        private static readonly BigInteger[] PowersOfTen = new BigInteger[18];

        static Iso7064CheckDigit()
        {
            BigInteger pot = 1;
            for (var i = 0; i < PowersOfTen.Length; i++)
            {
                PowersOfTen[i] = pot;
                pot *= 10;
            }
        }

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
            long[] parts = new long[8];
            int[] partDigitsArr = new int[8];
            var partIndex = 0;
            var partDigits = 0;
            var partDigitsThreshold = 16;

            foreach(var c in num)
            {

                var dec = parts[partIndex];
                if (c >= '0' && c <= '9')
                {
                    dec = dec * 10 + (c - '0');
                    partDigits++;
                }
                else if(c>='A' && c<='J')
                {
                    dec = dec * 10 + 1;
                    dec = dec * 10 + (c - 'A');
                    partDigits += 2;
                }
                else if (c >= 'K' && c <= 'T')
                {
                    dec = dec * 10 + 2;
                    dec = dec * 10 + (c - 'K');
                    partDigits += 2;
                }
                else if (c >= 'U' && c <= 'Z')
                {
                    dec = dec * 10 + 3;
                    dec = dec * 10 + (c - 'U');
                    partDigits += 2;
                }
                else
                {
                    throw new ArgumentException("invalid character: " + c);
                }

                parts[partIndex] = dec;
                if (partDigits >= partDigitsThreshold)
                {
                    partDigitsArr[partIndex] = partDigits;
                    partIndex++;
                    partDigits = 0;
                }
            }

            var result = BigInteger.Zero;
            for (var i = 0; i <= partIndex-1; i++)
            {
                result *= PowersOfTen[partDigitsArr[i]];
                result += parts[i];
            }

            result *= PowersOfTen[partDigits];
            result += parts[partIndex];

            return result;
        }
    }
}
