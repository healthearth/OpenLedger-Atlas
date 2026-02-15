// fintechs-exhibitu/01_Core_Domain/Services/ExchangeEngine.cs
// © 2026 Andrew Kieckhefer. All rights reserved.
namespace OpenLedgerAtlas.Domain.Services;

public static class ExchangeEngine
{
    public static decimal Convert(decimal amount, string currencyCode)
        => amount; // placeholder until real FX logic

    public static decimal ConvertToAiDollar(decimal amount, string currencyCode)
        => Convert(amount, currencyCode);
}
