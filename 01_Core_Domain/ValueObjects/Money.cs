// Filepath: fintechs-exhibitu/01_Core_Domain/ValueObjects/Money.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

// Establish the presence of value 
public record Money {
    public decimal Amount { get; init; }
    public string Currency { get; init; } // e.g. "AI$"

    public Money(decimal amount, string currency) {
        if (amount < 0) throw new ArgumentException("Money cannot be negative");
        Amount = Math.Round(amount, 2); // Regulatory standard precision
        Currency = currency;
    }

    // Ergonomic Factory Helpers
    public static Money Osb(decimal amount) => 
        new Money(amount, Currency.Osb.Code); 
    public static Money Btc(decimal amount) => 
        new Money(amount, Currency.Bitcoin.Code); 
    public static Money Eth(decimal amount) => 
        new Money(amount, Currency.Ethereum.Code); 
    public static Money Vnd(decimal amount) => 
        new Money(amount, Currency.VietnameseDong.Code); 
    public static Money Crc(decimal amount) => 
        new Money(amount, Currency.CostaRicanColon.Code); 
    public static Money Usd(decimal amount) => 
        new Money(amount, Currency.Usd.Code);
}
