// Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlLedgerRepository.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

using System.Data;
using Dapper;
using OpenLedgerAtlas.Domain.Entities;

namespace OpenLedgerAtlas.Infrastructure.Persistence;

public class SqlLedgerRepository
{
    private readonly IDbConnection _db;

    public SqlLedgerRepository(IDbConnection db)
    {
        _db = db;
    }

    public async Task<bool> SaveTransactionAsync(LedgerEntry entry)
    {
        const string sql = @"
            INSERT INTO Ledger (Debit, Credit, AssetId, Description, TimestampUtc)
            VALUES (@Debit, @Credit, @PhysicalAssetRef, @Description, @TimestampUtc)";

        return await _db.ExecuteAsync(sql, entry) > 0;
    }
}
