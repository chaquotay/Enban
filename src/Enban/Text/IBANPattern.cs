using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Enban.Countries;

namespace Enban.Text
{
    public sealed class IBANPattern : IPattern<IBAN>
    {
        private readonly IBANPatternType _type;
        private readonly ICountryProvider _countryProvider;

        public static IBANPattern Create(IBANPatternType patternType, ICountryProvider countryProvider)
        {
            return new IBANPattern(patternType, countryProvider);
        }

        public static IBANPattern CreateElectronic(ICountryProvider countryProvider) => Create(IBANPatternType.Electronic, countryProvider);

        public static IBANPattern Electronic { get; } = new IBANPattern(IBANPatternType.Electronic, CountryProviders.Default);

        public static IBANPattern CreatePrint(ICountryProvider countryProvider) => Create(IBANPatternType.Print, countryProvider);

        public static IBANPattern Print { get; } = new IBANPattern(IBANPatternType.Print, CountryProviders.Default);

        private IBANPattern(IBANPatternType type, ICountryProvider countryProvider)
        {
            _type = type;
            _countryProvider = countryProvider;
        }

        public string Format(IBAN value)
        {
            throw new NotImplementedException();
        }

        private static readonly Regex GeneralPattern = new Regex("^(?<COUNTRY>[A-Z]{2})(?<CHECK>[0-9]{2})(?<ACCOUNT>.+)$");

        public ParseResult<IBAN> Parse(string text)
        {
            if (text == null)
                return ParseResult<IBAN>.ForFailure(null);

            if (_type == IBANPatternType.Print)
            {
                // Remove whitespace
                text = Regex.Replace(text, "\\s+", "");
            }

            var match = GeneralPattern.Match(text);
            if (match.Success)
            {
                var country = match.Groups["COUNTRY"].Value;
                var check = match.Groups["CHECK"].Value;
                var account = match.Groups["ACCOUNT"].Value;

                var resolvedCountry = _countryProvider.GetCountryOrNull(country);

                if (resolvedCountry == null)
                    return ParseResult<IBAN>.ForFailure(new Exception($"Unknown country code: {country}"));

                var num = 0;
                if (!int.TryParse(check, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
                    return ParseResult<IBAN>.ForFailure(new Exception($"Check digit not a number: {check}"));

                var segments = resolvedCountry.AccountNumberFormatInfo?.StructureInfo?.Segments;
                if(segments==null)
                    return ParseResult<IBAN>.ForFailure(new Exception($"Format information unavailable for country {country}")); // TODO: Skip check when country format info missing?

                if (PatternConverter.IsMatch(account, segments))
                {
                    return ParseResult<IBAN>.ForSuccess(new IBAN(new BBAN(resolvedCountry, account), num));
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