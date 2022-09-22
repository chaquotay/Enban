using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Enban.Text
{
    public sealed partial class BICPattern : IPattern<BIC>
    {
        private readonly BICStyles _parseStyles;
        private readonly bool _addPrimaryBranchCode;
        private readonly Predicate<string> _isKnownCountryCode;

        private BICPattern(BICStyles parseStyles, bool addPrimaryBranchCode, Predicate<string> isKnownCountryCode)
        {
            _parseStyles = parseStyles;
            _addPrimaryBranchCode = addPrimaryBranchCode;
            _isKnownCountryCode = isKnownCountryCode;
        }

        /// <inheritdoc />
        public string Format(BIC value)
        {
            string formatted;

            if (_addPrimaryBranchCode || !"XXX".Equals(value.BranchCode))
            {
                formatted = $"{value.InstitutionCode}{value.CountryCode}{value.LocationCode}{value.BranchCode}";
            }
            else
            {
                formatted = $"{value.InstitutionCode}{value.CountryCode}{value.LocationCode}";
            }

            return formatted;
        }

        /// <inheritdoc />
        public ParseResult<BIC> Parse(string text)
        {
            var valid = ParseAndValidate(text, _parseStyles, _isKnownCountryCode, out var ic, out var cc, out var lc,
                out var bc, out var error);
            if (valid)
            {
                return ParseResult<BIC>.ForSuccess(new BIC(ic, cc, lc, bc));
            }
            else
            {
                return ParseResult<BIC>.ForFailure(new ArgumentException(error, nameof(text)));
            }
        }
        
        /// <returns>the parse format error or <c>null</c> if no error was found</returns>
        internal static bool ParseAndValidate(string text, BICStyles style, Predicate<string> isKnownCountryCode, out string institutionCode, out string countryCode, out string locationCode, out string branchCode, [NotNullWhen(false)] out string? error)
        {
            text = Normalize(text, style);
            
            institutionCode = "";
            countryCode = "";
            locationCode = "";
            branchCode = "";

            if (string.IsNullOrWhiteSpace(text))
            {
                error = "text is null or empty";
                return false;
            }

            if (text.Length!=8 && text.Length!=11)
            {
                error = $"'{text}' has wrong size (expected: 8 or 11, actual: {text.Length})";
                return false;
            }

            institutionCode = text.Substring(0, 4);
            countryCode = text.Substring(4, 2);
            locationCode = text.Substring(6, 2);
            if (text.Length == 8)
            {
                branchCode = "XXX";

                if (style.HasFlag(BICStyles.RequirePrimaryOfficeBranchCode))
                {
                    error = "primary branch code is missing";
                    return false;
                }
            }
            else if (text.EndsWith("XXX"))
            {
                branchCode = "XXX";

                if (style.HasFlag(BICStyles.DisallowPrimaryOfficeBranchCode))
                {
                    error = "primary branch code is present, but not allowed";
                    return false;
                }
            }
            else
            {
                branchCode = text.Substring(8, 3);
            }

            error = Validate(isKnownCountryCode, institutionCode, countryCode, locationCode, branchCode);

            if (error == null)
                return true;
            
            institutionCode = "";
            countryCode = "";
            locationCode = "";
            branchCode = "";
            
            return false;
        }
        
        private static string Normalize(string s, BICStyles style)
        {
            if (style.HasFlag(BICStyles.AllowLeadingWhite))
                s = s.TrimStart();

            if (style.HasFlag(BICStyles.AllowTrailingWhite))
                s = s.TrimEnd();

            if (style.HasFlag(BICStyles.IgnoreCase))
                s = s.ToUpperInvariant();
                
            return s;
        }

        private static string? Validate(Predicate<string> isKnownCountryCode, string institutionCode, string countryCode, string locationCode, string branchCode)
        {
            return ValidateInstitutionCode(institutionCode)
                   ?? ValidateCountryCode(countryCode, isKnownCountryCode)
                   ?? ValidateLocationCode(locationCode)
                   ?? ValidateBranchCode(branchCode);
        }

        private static string? ValidateInstitutionCode(string institutionCode) => ValidateLettersOrDigits(institutionCode, 4);

        private static string? ValidateCountryCode(string countryCode, Predicate<string> isKnownCountryCode) =>
            ValidateLetters(countryCode, 2) ?? (isKnownCountryCode(countryCode) ? null : $"unknown country code: {countryCode}");

        private static string? ValidateLocationCode(string locationCode)
        {
            return ValidateLettersOrDigits(locationCode, 2) ?? ValidateZero();

            string? ValidateZero()
            {
                return locationCode[1] == 'O' ? "location code must not end with '0' (zero)" : null;
            }
        }

        private static string? ValidateBranchCode(string branchCode)
        {
            return ValidateLength(branchCode, 3)
                   ?? ValidateLettersOrDigits(branchCode, 3)
                   ?? ValidateX();
            
            string? ValidateX()
            {
                return branchCode.StartsWith("X") && !branchCode.Equals("XXX")
                    ? "branch code starts with 'X', but is not 'XXX'"
                    : null;
            }
        }

        private static string? ValidateLettersOrDigits(string chars, int expectedCount)
        {
            return ValidateLength(chars, expectedCount) ?? ValidateChars(chars, IsLetterOrDigit, "letter or digit");
        }

        private static string? ValidateLetters(string chars, int expectedCount)
        {
            return ValidateLength(chars, expectedCount) ?? ValidateChars(chars, IsLetter, "letter");
        }

        private static string? ValidateLength(string chars, int expectedCount)
        {
            return chars.Length != expectedCount ? $"wrong size (expected: {expectedCount}, actual: {chars.Length})" : null;
        }

        private static string? ValidateChars(string chars, Predicate<char> predicate, string charClassName)
        {
            return (from char c in chars where !predicate(c) select $"invalid character (expected: {charClassName}, actual: {c})").FirstOrDefault();
        }
        
        private static bool IsLetterOrDigit(char c) => (c >= '0' && c <= '9') || IsLetter(c);
        
        private static bool IsLetter(char c) => c >= 'A' && c <= 'Z';
    }
}