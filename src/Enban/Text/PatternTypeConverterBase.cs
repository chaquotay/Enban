using System;
using System.ComponentModel;
using System.Globalization;

namespace Enban.Text
{
    internal abstract class PatternTypeConverterBase<T> : TypeConverter
    {
        private readonly IPattern<T> _pattern;

        protected PatternTypeConverterBase(IPattern<T> pattern)
        {
            _pattern = pattern;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string text)
                return _pattern.Parse(text).Value;
            
            return base.ConvertFrom(context, culture, value);
        }

        public override object? ConvertTo(ITypeDescriptorContext context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is T typedValue)
                return _pattern.Format(typedValue);
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}