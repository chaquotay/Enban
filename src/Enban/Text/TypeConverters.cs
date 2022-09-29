namespace Enban.Text
{
    internal sealed class IBANTypeConverter : PatternTypeConverterBase<IBAN>
    {
        public IBANTypeConverter() : base(IBANPattern.Electronic) { }
    }

    internal sealed class BICTypeConverter : PatternTypeConverterBase<BIC>
    {
        public BICTypeConverter() : base(BICPattern.Lenient) { }
    }
}