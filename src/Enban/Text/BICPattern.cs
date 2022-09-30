using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Enban.Text
{
    /// <summary>
    /// Implements the BIC patterns, both <see cref="Full">electronic</see> and <see cref="Compact">print</see>.
    /// </summary>
    public sealed partial class BICPattern : IPattern<BIC>
    {
        private readonly BICStyles _styles;
        private readonly string _format;
        private readonly Predicate<string> _isKnownCountryCode;

        private static readonly Dictionary<string, Func<BIC, string>> Formatters = new()
        {
            { "f", FormatFull },
            { "c", FormatCompact },
            { "G", FormatCompact }, // TODO: remove?
            { "o", FormatOriginal },
        };

        private BICPattern(BICStyles styles = BICStyles.Lenient, string format="c", Predicate<string>? isKnownCountryCode = null)
        {
            _styles = styles;
            _format = format;
            _isKnownCountryCode = isKnownCountryCode ?? DefaultIsKnownCountryCode;
        }

        /// <inheritdoc />
        public string Format(BIC value) => Format(value, _format);

        internal static string Format(BIC value, string? format)
        {
            if(!Formatters.TryGetValue(format ?? "c", out var formatter))
                throw new ArgumentException($"invalid format: {format}", nameof(format));

            return formatter.Invoke(value);
        }
        
        private static string FormatFull(BIC value)
        {
            return $"{value.InstitutionCode}{value.CountryCode}{value.LocationCode}{value.BranchCode}";
        }
        
        private static string FormatCompact(BIC value)
        {
            var compactedBranchCode = "XXX".Equals(value.BranchCode) ? "": value.BranchCode;
            return $"{value.InstitutionCode}{value.CountryCode}{value.LocationCode}{compactedBranchCode}";
        }
        
        private static string FormatOriginal(BIC value)
        {
            var originalBranchCode = value.ConstructedWithoutBranchCode ? "": value.BranchCode;
            return $"{value.InstitutionCode}{value.CountryCode}{value.LocationCode}{originalBranchCode}";
        }

        /// <inheritdoc />
        public ParseResult<BIC> Parse(string text)
        {
            var valid = ParseAndValidate(text, _styles, _isKnownCountryCode, out var ic, out var cc, out var lc,
                out var bc, out var error);
            if (valid)
            {
                return ParseResult<BIC>.ForValue(new BIC(ic, cc, lc, bc));
            }
            else
            {
                return ParseResult<BIC>.ForException(new ArgumentException(error, nameof(text)));
            }
        }
        
        /// <returns>the parse format error or <c>null</c> if no error was found</returns>
        internal static bool ParseAndValidate(string text, BICStyles style, Predicate<string> isKnownCountryCode, out string institutionCode, out string countryCode, out string locationCode, out string? branchCode, [NotNullWhen(false)] out string? error)
        {
            text = Normalize(text, style);
            
            institutionCode = "";
            countryCode = "";
            locationCode = "";
            branchCode = null;

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
                branchCode = null;

                if (style.HasFlagFast(BICStyles.RequirePrimaryOfficeBranchCode))
                {
                    error = "primary branch code is missing";
                    return false;
                }
            }
            else if (text.EndsWith("XXX"))
            {
                branchCode = "XXX";

                if (style.HasFlagFast(BICStyles.DisallowPrimaryOfficeBranchCode))
                {
                    error = "primary branch code is present, but not allowed";
                    return false;
                }
            }
            else
            {
                branchCode = text.Substring(8, 3);
            }

            error = Validate(isKnownCountryCode, institutionCode, countryCode, locationCode, branchCode ?? "XXX");

            if (error == null)
                return true;
            
            institutionCode = "";
            countryCode = "";
            locationCode = "";
            branchCode = null;
            
            return false;
        }
        
        private static string Normalize(string s, BICStyles style) // TODO: char[]? ToTrimmedCharArray()?
        {
            if (style.HasFlagFast(BICStyles.AllowLeadingWhite))
                s = s.TrimStart();

            if (style.HasFlagFast(BICStyles.AllowTrailingWhite))
                s = s.TrimEnd();

            if (style.HasFlagFast(BICStyles.IgnoreCase))
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