// Filepath: OpenLedger-Atlas/01_Core_Domain/Entities/Account.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

namespace OpenLedgerAtlas.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Balance { get; private set; }

    public Account(Guid userId)
    {
        UserId = userId;
        Balance = 0m;
    }

    public void Credit(decimal amount)
    {
        Balance += amount;
    }

    public bool Debit(decimal amount)
    {
        if (Balance < amount)
            return false;

        Balance -= amount;
        return true;
    }

    public void ApplyDelta(decimal amount)
    {
        Balance += amount;
    }
}
