// fintechs-exhibitu/02_Application_Logic/Banking/BankingOperations.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

using OpenLedgerAtlas.Domain.Interfaces;
using OpenLedgerAtlas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace OpenLedgerAtlas.Application.Banking;

public class BankingOperations : IBankingOperations
{
    private readonly IBankingRepository _repo;
    private readonly OpenLedgerDbContext _db;

    public BankingOperations(IBankingRepository repo, OpenLedgerDbContext db)
    {
        _repo = repo;
        _db = db;
    }

    public async Task<bool> TransactAsync(Guid from, Guid to, decimal amount)
    {
        if (amount <= 0)
            return false;

        var total = await _repo.GetLedgerTotalAsync();
        if (total < amount)
            return false;

        await _repo.InsertLedgerEntryAsync(from, 0, amount, "Transfer Out");
        await _repo.InsertLedgerEntryAsync(to, amount, 0, "Transfer In");

        return true;
    }

    public async Task<bool> ReconcileWithPhysicalVaultAsync()
    {
        var ledger = await _repo.GetLedgerTotalAsync();
        var vault  = await _repo.GetPhysicalVaultTotalAsync();

        return ledger == vault;
    }

    public async Task<List<Guid>> GetAllAccountIdsAsync()
    {
        return await _db.Accounts
            .Select(a => a.Id)
            .ToListAsync();
    }
}
