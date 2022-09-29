# Introduction

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

# Version history / changes

## 2.0.0-beta.1

Changes since 1.0.x:

* Added [`BIC`](api/Enban.BIC.html) type for *Business Identifier Codes*
* Removed `BBAN` type
* Changed [`IBAN`](api/Enban.IBAN.html) type from `struct` to `class`
* Added type converters
* C# 8 nullable reference type support
* Deconstruct methods to support C# 7 deconstruction
* Changed target to .NET Standard 2.0

## 1.0.1

* Improved null handling in empty default values of `IBAN` and `BBAN`

## 1.0.0

* Fixed problem with check digits with leading zeros

## 0.1.0-alpha 

* First release