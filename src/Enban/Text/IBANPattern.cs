using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Enban.Text
{
    /// <summary>
    /// Implements the IBAN patterns, both <see cref="Electronic">electronic</see> and <see cref="Print">print</see>.
    /// </summary>
    public sealed partial class IBANPattern : IPattern<IBAN>
    {
        private readonly CountryAccountPatterns _countryAccountPatterns;
        private readonly IBANStyles _styles;
        private readonly bool _addWhitespaces;

        private IBANPattern(IBANStyles styles, bool addWhitespaces, CountryAccountPatterns countryAccountPatterns)
        {
            _countryAccountPatterns = countryAccountPatterns;
            _styles = styles;
            _addWhitespaces = addWhitespaces;
        }

        /// <inheritdoc />
        public string Format(IBAN value)
        {
            var accountNumber = value.AccountNumber;
            var countryCode = value.CountryCode;
            var checkDigit = value.CheckDigit;

            var electronicChars = new char[accountNumber.Length + 4];
            electronicChars[0] = countryCode[0];
            electronicChars[1] = countryCode[1];

            var lastCheckDigit = checkDigit % 10;
            var firstCheckDigit = (checkDigit - lastCheckDigit) / 10;
            electronicChars[2] = (char) ('0' + firstCheckDigit);
            electronicChars[3] = (char) ('0' + lastCheckDigit);

            accountNumber.CopyTo(0, electronicChars, 4, accountNumber.Length);

            if (_addWhitespaces)
            {
                var rest = electronicChars.Length % 4;
                var fullSegmentCount = electronicChars.Length / 4;

                var notLastSegmentCount = rest == 0 ? fullSegmentCount - 1 : fullSegmentCount;
                var lastSegmentLength = rest == 0 ? 4 : rest;
                var printLength = notLastSegmentCount * 5 + lastSegmentLength;
                var printChars = new char[printLength];

                var segmentIndex = 0;
                while (segmentIndex < notLastSegmentCount)
                {
                    var electronicPosition = segmentIndex << 2;
                    var printPosition = electronicPosition + segmentIndex;

                    printChars[printPosition + 0] = electronicChars[electronicPosition + 0];
                    printChars[printPosition + 1] = electronicChars[electronicPosition + 1];
                    printChars[printPosition + 2] = electronicChars[electronicPosition + 2];
                    printChars[printPosition + 3] = electronicChars[electronicPosition + 3];

                    var spacePosition = printPosition + 4;
                    printChars[spacePosition] = ' ';
                    segmentIndex++;
                }

                var lastElectronicPosition = segmentIndex << 2;
                var lastPrintPosition = lastElectronicPosition + segmentIndex;

                if (lastPrintPosition + 0 >= 0 && printChars.Length > lastPrintPosition + 0)
                    printChars[lastPrintPosition + 0] = electronicChars[lastElectronicPosition + 0];
                if (lastPrintPosition + 1 >= 0 && printChars.Length > lastPrintPosition + 1)
                    printChars[lastPrintPosition + 1] = electronicChars[lastElectronicPosition + 1];
                if (lastPrintPosition + 2 >= 0 && printChars.Length > lastPrintPosition + 2)
                    printChars[lastPrintPosition + 2] = electronicChars[lastElectronicPosition + 2];
                if (lastPrintPosition + 3 >= 0 && printChars.Length > lastPrintPosition + 3)
                    printChars[lastPrintPosition + 3] = electronicChars[lastElectronicPosition + 3];

                return new string(printChars);
            }
            else
            {
                return new string(electronicChars);
            }
        }

        /// <inheritdoc />
        public ParseResult<IBAN> Parse(string text)
        {
            return ParseAndValidate(text, _styles, _countryAccountPatterns, out var cc, out var cd,
                out var ac, out var error)
                ? ParseResult<IBAN>.ForSuccess(new IBAN(cc, cd, ac))
                : ParseResult<IBAN>.ForFailure(new FormatException(error));
        }

        internal static bool ParseAndValidate(string text, IBANStyles style, CountryAccountPatterns countryAccountPatterns, out string countryCode, out int checkDigit, out string accountNumber, [NotNullWhen(false)] out string? error)
        {
            countryCode = "";
            checkDigit = 0;
            accountNumber = "";
            error = null;

            if (string.IsNullOrWhiteSpace(text))
            {
                error = "text is null or empty";
                return false;
            }
            
            var chars = ToNormalizedCharArray(text, style);
            
            if (chars.Length < 5) // two characters for country coude, two characters for check digit, at least one char for account number
            {
                error = "text is too short";
                return false;
            }

            var matchSuccess = chars.Length > 4 
                               && chars[0]>='A' && chars[0] <= 'Z'
                               && chars[1]>='A' && chars[1] <= 'Z'
                               && chars[2]>='0' && chars[2] <= '9'
                               && chars[3]>='0' && chars[3] <= '9';

            if (matchSuccess)
            {
                countryCode = new string(chars, 0, 2);
                checkDigit = (chars[2] - '0') * 10 + (chars[3] - '0');

                if (!countryAccountPatterns.TryGetSegments(countryCode, out var segments))
                {
                    error = $"Format information unavailable for country {countryCode}";
                    return false;
                }
                
                if (SegmentsMatcher.IsMatch(segments, chars, 4, chars.Length-4))
                {
                    accountNumber = new string(chars, 4, chars.Length - 4);
                    
                    if (style.HasFlag(IBANStyles.AllowInvalidCheckDigit) || CheckDigitUtil.IsValid(countryCode, accountNumber, checkDigit))
                    {
                        return true;
                    }
                    else
                    {
                        error = "invalid check digit";
                        return false;
                    }
                }
            }

            error = "wrong format";
            return false;
        }
        
        private static char[] ToNormalizedCharArray(string s, IBANStyles style)
        {
            if (style.HasFlag(IBANStyles.IgnoreCase))
                s = s.ToUpperInvariant();
            
            var trimStart = style.HasFlag(IBANStyles.AllowLeadingWhite);
            var trimMiddle = style.HasFlag(IBANStyles.AllowIntermediateWhite);
            var trimEnd = style.HasFlag(IBANStyles.AllowTrailingWhite);

            return s.ToTrimmedCharArray(trimStart, trimMiddle, trimEnd);
        }

    }
}