// Filepath: fintechs-exhibitu/01_Core_Domain/Entities/Transaction.cs
namespace GlobalBank.Domain.Entities;

public class Transaction {
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid FromAccountId { get; private set; }
    public Guid ToAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public string CurrencyCode { get; private set; }
    public string PhysicalAssetReference { get; private set; } // Link to specific library asset
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public Transaction(Guid from, Guid to, decimal amount, string currency, string assetRef) {
        if (amount <= 0) throw new ArgumentException("Amount must be positive.");
        FromAccountId = from;
        ToAccountId = to;
        Amount = amount;
        CurrencyCode = currency;
        PhysicalAssetReference = assetRef;
    }
}
