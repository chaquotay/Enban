namespace Enban
{
    internal class CheckDigitUtil
    {
        public static CheckDigit Compute(string countryCode, string accountNumber)
        {
            return Iso7064CheckDigit.Mod97.Compute(accountNumber + countryCode + "00");
        }

        public static bool IsValid(string countryCode, string accountNumber, CheckDigit checkDigit)
        {
            var num = accountNumber + countryCode + checkDigit;
            return Iso7064CheckDigit.Mod97.IsValid(num);
        }
    }
}