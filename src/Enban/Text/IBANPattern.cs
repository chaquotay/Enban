using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Enban.Countries;

namespace Enban.Text
{
    /// <summary>
    /// Implements the IBAN patterns, both <see cref="Electronic">electronic</see> and <see cref="Print">print</see>.
    /// </summary>
    public sealed class IBANPattern : IPattern<IBAN>
    {
        private readonly IBANPatternType _type;
        private readonly CountryAccountPatterns _countryAccountPatterns;
        private readonly IBANStyles _styles;

        /// <summary>
        /// Creates a pattern instance (print or electronic), using the provided <see cref="ICountryProvider"/>.
        /// </summary>
        /// <param name="patternType">the pattern variant (<see cref="IBANPatternType.Electronic"/> or <see cref="IBANPatternType.Print"/>)</param>
        /// <param name="countryAccountPatterns">country account patterns</param>
        /// <returns>the constructed pattern instance</returns>
        private static IBANPattern Create(IBANPatternType patternType, IBANStyles styles, CountryAccountPatterns countryAccountPatterns)
        {
            return new IBANPattern(patternType, styles, countryAccountPatterns);
        }

        /// <summary>
        /// Creates an electronic pattern instance, using the provided <see cref="ICountryProvider"/>.
        /// </summary>
        /// <param name="countryAccountPatterns">country account patterns</param>
        /// <returns>the constructed pattern instance</returns>
        public static IBANPattern CreateElectronic(CountryAccountPatterns countryAccountPatterns) => Create(IBANPatternType.Electronic, IBANStyles.Electronic, countryAccountPatterns);

        /// <summary>
        /// Provides an electronic pattern instance, using the <see cref="IBAN.KnownCountryAccountPatterns">default country provider</see>.
        /// </summary>
        public static IBANPattern Electronic { get; } = CreateElectronic(IBAN.KnownCountryAccountPatterns);

        /// <summary>
        /// Creates a print pattern instance, using the provided <see cref="ICountryProvider"/>.
        /// </summary>
        /// <param name="countryAccountPatterns">country account patterns</param>
        /// <returns>the constructed pattern instance</returns>
        public static IBANPattern CreatePrint(CountryAccountPatterns countryAccountPatterns) => Create(IBANPatternType.Print, IBANStyles.Print, countryAccountPatterns);

        /// <summary>
        /// Provides a print pattern instance, using the <see cref="CountryProviders.Default">default country provider</see>.
        /// </summary>
        public static IBANPattern Print { get; } = CreatePrint(IBAN.KnownCountryAccountPatterns);

        private IBANPattern(IBANPatternType type, IBANStyles styles, CountryAccountPatterns countryAccountPatterns)
        {
            _type = type;
            _countryAccountPatterns = countryAccountPatterns;
            _styles = styles;
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

            if (_type == IBANPatternType.Print)
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
            text = Normalize(text, style);

            countryCode = "";
            checkDigit = 0;
            accountNumber = "";
            error = null;
            
            if (string.IsNullOrWhiteSpace(text))
            {
                error = "text is null or empty";
                return false;
            }
            
            if (text.Length < 5) // two characters for country coude, two characters for check digit, at least one char for account number
            {
                error = "text is too short";
                return false;
            }

            var chars = text.ToCharArray();

            var matchSuccess = chars.Length > 4 
                               && chars[0]>='A' && chars[0] <= 'Z'
                               && chars[1]>='A' && chars[1] <= 'Z'
                               && chars[2]>='0' && chars[2] <= '9'
                               && chars[3]>='0' && chars[3] <= '9';

            if (matchSuccess)
            {
                countryCode = new string(chars, 0, 2);
                checkDigit = (chars[2] - '0') * 10 + (chars[3] - '0');
                
                var formatInfo = countryAccountPatterns.Get(countryCode);
                if (formatInfo == null)
                {
                    error = $"Format information unavailable for country {countryCode}";
                    return false;
                }

                var segments = formatInfo.Segments;
                if (SegmentsMatcher.IsMatch(segments, chars, 4, chars.Length-4))
                {
                    accountNumber = new string(chars, 4, chars.Length - 4);
                    return true;
                }
            }

            error = "wrong format";
            return false;
        }
        
        private static string Normalize(string s, IBANStyles style)
        {
            // TODO: change to char[]?
            
            if (style.HasFlag(IBANStyles.AllowLeadingWhite))
                s = s.TrimStart();

            if (style.HasFlag(IBANStyles.AllowTrailingWhite))
                s = s.TrimEnd();

            if (style.HasFlag(IBANStyles.AllowIntermediateWhite))
                s = new string(s.ToArray().RemoveWhitespaces());

            if (style.HasFlag(IBANStyles.IgnoreCase))
                s = s.ToUpperInvariant();
                
            return s;
        }

    }
}