using System;

namespace Enban.Text
{
    internal sealed class BBANPattern : IPattern<BBAN>
    {
        public string Format(BBAN value)
        {
            throw new NotImplementedException();
        }

        public ParseResult<BBAN> Parse(string text)
        {
            throw new NotImplementedException();
        }
    }
}