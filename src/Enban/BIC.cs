using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Enban.Text;

namespace Enban
{
    /// <summary>
    /// A <em>Business Identifier Code</em> according to <a href="https://en.wikipedia.org/wiki/ISO_9362">ISO 9362</a>.
    /// </summary>
    [TypeConverter(typeof(BICTypeConverter))]
    public class BIC : IFormattable, IEquatable<BIC>, IComparable<BIC>, IComparable
    {
        /// <summary>
        /// The institution/bank code
        /// </summary>
        public string InstitutionCode { get; }
        
        /// <summary>
        /// The <a href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO 3166-1 alpha-2</a> country code
        /// </summary>
        public string CountryCode { get; }
        
        /// <summary>
        /// The location code
        /// </summary>
        public string LocationCode { get; }
        
        /// <summary>
        /// The branch code, or "XXX", if no explicit branch code was provided (typically for primary offices).
        /// </summary>
        public string BranchCode { get; }
        
        internal BIC(string institutionCode, string countryCode, string locationCode, string branchCode)
        {
            InstitutionCode = institutionCode;
            CountryCode = countryCode;
            LocationCode = locationCode;
            BranchCode = branchCode;
        }

        /// <summary>
        /// Constructs a BIC from a string. An <see cref="ArgumentException"/> will be thrown if the given string is not a valid BIC.
        /// </summary>
        /// <param name="bic">the BIC in string form</param>
        /// <exception cref="ArgumentException">if the given <paramref name="bic"/> is not a valid BIC</exception>
        public BIC(string bic)
        {
            var isValid = BICPattern.ParseAndValidate(bic, BICStyles.Lenient, BICPattern.DefaultIsKnownCountryCode, out var ic,
                out var cc, out var lc, out var bc, out var error);
            if (isValid)
            {
                InstitutionCode = ic;
                CountryCode = cc;
                LocationCode = lc;
                BranchCode = bc;
            }
            else
            {
                throw new ArgumentException("Invalid format: " + error);
            }
        }

        /// <summary>
        /// Formats this BIC according in the compact format (<see cref="BICPattern.Compact"/>) 
        /// </summary>
        /// <returns>the formatted BIC</returns>
        public override string ToString()
        {
            return ToString("c", null);
        }
        
        /// <summary>
        /// Formats this BIC according to the given format string.
        /// <list type="bullet">
        /// <item><paramref name="format"/> is <c>"F"</c> or <c>"f"</c>: format this BIC using <see cref="BICPattern.Full"/></item>
        /// <item><paramref name="format"/> is anything else (including <c>null</c>): format this BIC using <see cref="BICPattern.Compact"/></item>
        /// </list>
        /// </summary>
        /// <param name="format">format string</param>
        /// <param name="formatProvider">ignored</param>
        /// <returns>The formatted BIC</returns>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return BICPattern.Format(this, format);
        }

        /// <summary>
        /// Tries to parse a given BIC string using <see cref="TryParse(string,BICStyles,out Enban.BIC)"/> with
        /// <see cref="BICStyles.Lenient"/>.
        /// </summary>
        /// <param name="text">the BIC string to parse</param>
        /// <param name="parsed">the parsed BIC, if the format is valid</param>
        /// <returns>whether <paramref name="text"/> was parsed sucessfully</returns>
        public static bool TryParse(string text, [NotNullWhen(true)] out BIC? parsed)
        {
            return TryParse(text, BICStyles.Lenient, out parsed);
        }
        
        /// <summary>
        /// Tries to parse a given BIC string.
        /// </summary>
        /// <param name="text">the BIC string to parse</param>
        /// <param name="style"></param>
        /// <param name="parsed">the parsed BIC, if the format is valid</param>
        /// <returns>whether <paramref name="text"/> was parsed sucessfully</returns>
        public static bool TryParse(string text, BICStyles style, [NotNullWhen(true)] out BIC? parsed)
        {
            var isValid = BICPattern.ParseAndValidate(text, style, BICPattern.DefaultIsKnownCountryCode, out var ic, out var cc,
                out var lc, out var bc, out _);
            
            if (isValid)
            {
                parsed = new BIC(ic, cc, lc, bc);
                return true;
            }

            parsed = null;
            return false;
        }

        public int CompareTo(BIC other)
        {
            int cmp;
            
            cmp = string.Compare(InstitutionCode, other.InstitutionCode, StringComparison.Ordinal);
            if (cmp != 0)
                return cmp;
            
            cmp = string.Compare(CountryCode, other.CountryCode, StringComparison.Ordinal);
            if (cmp != 0)
                return cmp;
            
            cmp = String.Compare(LocationCode, other.LocationCode, StringComparison.Ordinal);
            if (cmp != 0)
                return cmp;
            
            cmp = string.Compare(BranchCode, other.BranchCode, StringComparison.Ordinal);
            if (cmp != 0)
                return cmp;

            return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            if (obj is BIC bic)
                return CompareTo(bic);

            throw new ArgumentException($"cannot compare {nameof(BIC)} to {obj.GetType().FullName}");
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is BIC bic && Equals(bic);
        }

        /// <inheritdoc />
        public bool Equals(BIC other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return InstitutionCode == other.InstitutionCode && CountryCode == other.CountryCode && LocationCode == other.LocationCode && BranchCode == other.BranchCode;
        }

        public static bool operator ==(BIC x, BIC y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.Equals(y);
        }

        public static bool operator !=(BIC x, BIC y)
        {
            return !(x == y);
        }
        
        public void Deconstruct(out string institutionCode, out string countryCode, out string locationCode, out string branchCode)
        {
            institutionCode = InstitutionCode;
            countryCode = CountryCode;
            locationCode = LocationCode;
            branchCode = BranchCode;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = InstitutionCode.GetHashCode();
                hashCode = (hashCode * 397) ^ CountryCode.GetHashCode();
                hashCode = (hashCode * 397) ^ LocationCode.GetHashCode();
                hashCode = (hashCode * 397) ^ BranchCode.GetHashCode();
                return hashCode;
            }
        }
    }
}