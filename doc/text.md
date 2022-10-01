# Text handling

Enban supports both the "normal", BCL-inspired approach for parsing and formatting (`TryParse` and `IFormattable`), and a pattern-based approach.

A pattern is an object capable of parsing from text to a specific type, and formatting an instance of a specific type to text. The pattern
knows

Both main Enban types ([`IBAN`](api/Enban.IBAN.html) and [`BIC`](api/Enban.BIC.html))
have their own pattern types ([`IBANPattern`](api/Enban.Text.IBANPattern.html) and
[`BICPattern`](api/Enban.Text.BICPattern.html)), which both implement the generic
[`IPattern<T>`](api/Enban.Text.IPattern-1.html) interface, which have simple `Format`
and `Parse` methods. The result of `Parse` is a [`ParseResult<T>`](api/Enban.Text.ParseResult-1.html),
which

## IBAN

### Parsing

IBANs can be parsed in the following ways:

1. `IBAN.TryParse(string text, out IBAN result)`: convenient short-hand for `IBAN.TryParse(string text, IBANStyles.Lenient, out IBAN result)`.  
2. `IBAN.TryParse(string text, IBANStyles style, out IBAN result)`: parses the text according to the specified `style`, returning `true` if the given `text` could be parsed (then the parsed value is return through the `result` out parameter), otherwise `false`.
3. `IBANPattern.Electronic.Parse(string text)`: parses the text according to the `IBANStyles.Electronic` style, returning the corresponding `ParseResult<IBAN>`.
4. `IBANPattern.Print.Parse(string text)`: parses the text according to the `IBANStyles.Print` style, returning the corresponding `ParseResult<IBAN>`.

### Formatting

IBAN can be "formatted" (converted to text) in the following ways:

1. `IBAN.ToString()`: convenient short-hand for `IBAN.ToString("e", CultureInfo.CurrentCulture)`
2. `IBAN.ToString(string format, IFormatProvider formatProvider)`: formats the IBAN according to the supported standard IBAN format specifiers (see below)
3. `IBANPattern.Electronic.Format(IBAN iban)`: formats the IBAN using the "electronic" format (format specifier "e", see below)
4. `IBANPattern.Print.Format(IBAN iban)`: formats the IBAN using the "print" format (format specifier "e", see below)

The following standard format specifiers are supported by Enban:

* `e`: the "electronic" format, without any white-spaces, e.g. "DE07123412341234123412"
* `p`: the "print" format, with white-spaces after every forth character, e.g. "DE07 1234 1234 1234 1234 12"