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
            var num = accountNumber + countryCode + checkDigit;
            return Iso7064CheckDigit.Mod97.IsValid(num);
        }
    }
}