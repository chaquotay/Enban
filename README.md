# Enban

Enban is an IBAN/BIC API for .NET, including parsing, validation and formatting.

## Usage

The following snippets show some basic uses of Enban.

### Parsing, validation and formatting of an IBAN

```csharp
var parsed = IBANPattern.Electronic.Parse("GI75NWBK000000007099453");
if (parsed.Success && parsed.Value.CheckDigitValid)
{
    Console.WriteLine($"IBAN '{parsed.Value:p}' is valid!");
}
```

prints `IBAN 'GI75 NWBK 0000 0000 7099 453' is valid!`

### Constructing an IBAN from a BBAN (with check digit)

```csharp
var germany = CountryProviders.Default["DE"];
var iban = new BBAN(germany, "210501700012345678").ToIBAN();

Console.WriteLine($"IBAN with check digit (valid: {iban.CheckDigitValid}): " + IBANPattern.Print.Format(iban));
```

prints `IBAN with check digit (valid: True): DE68 2105 0170 0012 3456 78`


## NuGet

Enban is [available on NuGet](https://www.nuget.org/packages/Enban).

## Build status

[![Build status](https://ci.appveyor.com/api/projects/status/1f9k8plkq3y1ijki/branch/master?svg=true)](https://ci.appveyor.com/project/chaquotay/enban-5adcw/branch/master)
