using System;
using System.Numerics;

namespace Enban
{
    internal class Iso7064CheckDigit
    {
        public static Iso7064CheckDigit Mod97 { get; } = new(97);

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
            var parts = new long[8];
            var partDigitsArr = new int[8];
            var partIndex = 0;
            var partDigits = 0;
            var partDigitsThreshold = 16;

            foreach(var c in num)
            {
                var dec = parts[partIndex];
                if (c is >= '0' and <= '9')
                {
                    dec = dec * 10 + (c - '0');
                    partDigits++;
                }
                else if(c is >= 'A' and <= 'J')
                {
                    dec = dec * 10 + 1;
                    dec = dec * 10 + (c - 'A');
                    partDigits += 2;
                }
                else if (c is >= 'K' and <= 'T')
                {
                    dec = dec * 10 + 2;
                    dec = dec * 10 + (c - 'K');
                    partDigits += 2;
                }
                else if (c is >= 'U' and <= 'Z')
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
