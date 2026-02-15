// Filepath: OpenLedger-Atlas/01_Core_Domain/Entities/Transaction.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

namespace OpenLedgerAtlas.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }

    public Guid FromAccountId { get; private set; }
    public Guid ToAccountId { get; private set; }

    public decimal Amount { get; private set; }

    public string Type { get; private set; }
    public string Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsFraud { get; set; }

    public Transaction(
        Guid from,
        Guid to,
        decimal amount,
        string type,
        string status)
    {
        FromAccountId = from;
        ToAccountId = to;
        Amount = amount;
        Type = type;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }
}
