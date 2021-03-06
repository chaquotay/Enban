﻿using System;

namespace Enban
{
    internal class CheckDigitUtil
    {
        public static int Compute(string countryCode, string accountNumber)
        {
            return Iso7064CheckDigit.Mod97.Compute(accountNumber + countryCode + "00");
        }

        public static bool IsValid(string countryCode, string accountNumber, int checkDigit)
        {
            if(countryCode==null || countryCode.Length!=2)
                throw new ArgumentException(nameof(countryCode));

            if(accountNumber==null || countryCode.Length==0)
                throw new ArgumentException(nameof(accountNumber));

            if(checkDigit<0 || checkDigit>99)
                throw new ArgumentException(nameof(checkDigit));

            var len = accountNumber.Length;
            var num1 = new char[len + 4];
            accountNumber.CopyTo(0, num1, 0, len);

            var offset = len;
            num1[offset++] = countryCode[0];
            num1[offset++] = countryCode[1];

            var cd1 = checkDigit % 10;
            var cd0 = checkDigit / 10;

            num1[offset++] = (char) (cd0 + '0');
            num1[offset] = (char) (cd1 + '0');

            var num = new string(num1);
            return Iso7064CheckDigit.Mod97.IsValid(num);
        }
    }
}