using System;
using Enban.Countries;

namespace Enban.Text
{
    /// <summary>
    /// Implements the IBAN patterns, both <see cref="Electronic">electronic</see> and <see cref="Print">print</see>.
    /// </summary>
    public sealed class IBANPattern : IPattern<IBAN>
    {
        private readonly IBANPatternType _type;
        private readonly ICountryProvider _countryProvider;

        /// <summary>
        /// Creates a pattern instance (print or electronic), using the provided <see cref="ICountryProvider"/>.
        /// </summary>
        /// <param name="patternType">the pattern variant (<see cref="IBANPatternType.Electronic"/> or <see cref="IBANPatternType.Print"/>)</param>
        /// <param name="countryProvider">a country provider</param>
        /// <returns>the constructed pattern instance</returns>
        public static IBANPattern Create(IBANPatternType patternType, ICountryProvider countryProvider)
        {
            return new IBANPattern(patternType, countryProvider);
        }

        /// <summary>
        /// Creates an electronic pattern instance, using the provided <see cref="ICountryProvider"/>.
        /// </summary>
        /// <param name="countryProvider">a country provider</param>
        /// <returns>the constructed pattern instance</returns>
        public static IBANPattern CreateElectronic(ICountryProvider countryProvider) => Create(IBANPatternType.Electronic, countryProvider);

        /// <summary>
        /// Provides an electronic pattern instance, using the <see cref="CountryProviders.Default">default country provider</see>.
        /// </summary>
        public static IBANPattern Electronic { get; } = new IBANPattern(IBANPatternType.Electronic, CountryProviders.Default);

        /// <summary>
        /// Creates a print pattern instance, using the provided <see cref="ICountryProvider"/>.
        /// </summary>
        /// <param name="countryProvider">a country provider</param>
        /// <returns>the constructed pattern instance</returns>
        public static IBANPattern CreatePrint(ICountryProvider countryProvider) => Create(IBANPatternType.Print, countryProvider);

        /// <summary>
        /// Provides a print pattern instance, using the <see cref="CountryProviders.Default">default country provider</see>.
        /// </summary>
        public static IBANPattern Print { get; } = new IBANPattern(IBANPatternType.Print, CountryProviders.Default);

        private IBANPattern(IBANPatternType type, ICountryProvider countryProvider)
        {
            _type = type;
            _countryProvider = countryProvider;
        }

        /// <inheritdoc />
        public string Format(IBAN value)
        {
            var country = value.Country;
            var accountNumber = value.AccountNumber;
            var countryCode = country.Code;
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
            var chars = text.ToCharArray();
            if (_type == IBANPatternType.Print)
            {
                chars = chars.RemoveWhitespaces();
            }

            var matchSuccess = chars.Length > 4 
                                && chars[0]>='A' && chars[0] <= 'Z'
                                && chars[1]>='A' && chars[1] <= 'Z'
                                && chars[2]>='0' && chars[2] <= '9'
                                && chars[3]>='0' && chars[3] <= '9';

            if (matchSuccess)
            {
                var country = new string(chars, 0, 2);

                var resolvedCountry = _countryProvider.GetCountryOrNull(country);

                if (resolvedCountry == null)
                    return ParseResult<IBAN>.ForFailure(new Exception($"Unknown country code: {country}"));

                var num = ((chars[2] - '0') * 10 + (chars[3] - '0'));

                var segments = resolvedCountry.AccountNumberFormatInfo?.StructureInfo?.Segments;
                if (segments == null)
                    return ParseResult<IBAN>.ForFailure(new Exception($"Format information unavailable for country {country}")); // TODO: Skip check when country format info missing?

                if (SegmentsMatcher.IsMatch(segments, chars, 4, chars.Length-4))
                {
                    var account = new string(chars, 4, chars.Length - 4);
                    return ParseResult<IBAN>.ForSuccess(new IBAN(new BBAN(resolvedCountry, account, false), num));
                }
                else
                {
                    return ParseResult<IBAN>.ForFailure(null);
                }
            }
            else
            {
                return ParseResult<IBAN>.ForFailure(null);
            }
        }
    }
}