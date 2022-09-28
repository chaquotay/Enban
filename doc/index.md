# Enban

Enban is an IBAN/BIC API for .NET, including parsing, validation and formatting.

## Parsing, validation and formatting of IBANs

```csharp
if(IBAN.TryParse("GI75NWBK000000007099453", out var iban) && iban.CheckDigitValid)
{
    Console.WriteLine($"IBAN '{iban:p}' is valid!");
}
```

prints `IBAN 'GI75 NWBK 0000 0000 7099 453' is valid!`

## Parsing and formatting of BICs

```csharp
if(BIC.TryParse("COBADEFFXXX", out var bic)) {
    Console.WriteLine($"BIC is: {bic:c}");
}
```

prints `BIC is: COBADEFF`