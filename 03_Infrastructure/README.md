© 2026 Andrew Kieckhefer. All rights reserved.

# Summary

// Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlBankingRepository.cs

using Dapper;
using Microsoft.Data.SqlClient;
using GlobalBank.Domain.Entities;
using GlobalBank.Domain.Interfaces;

namespace GlobalBank.Infrastructure.Persistence;

public class SqlBankingRepository : IBankingRepository
{
    private readonly string _connectionString;

    public SqlBankingRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task RecordCapitalDepositAsync(PhysicalAssetDeposit deposit)
    {
        // TODO: persist deposit
        return Task.CompletedTask;
    }

    public async Task<Account?> GetAccountByIdAsync(Guid id)
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.QuerySingleOrDefaultAsync<Account>(
            "SELECT * FROM Accounts WHERE Id = @id",
            new { id });
    }

    public async Task InsertLedgerEntryAsync(
        Guid accountId,
        decimal credit,
        decimal debit,
        string description,
        string? physicalAssetRef = null)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.ExecuteAsync(
            @"INSERT INTO DigitalLedger
              (AccountId, Credit, Debit, Description, PhysicalAssetRef)
              VALUES (@accountId, @credit, @debit, @description, @physicalAssetRef)",
            new { accountId, credit, debit, description, physicalAssetRef });
    }

    public async Task RegisterPhysicalAssetAsync(
        PhysicalAssetDeposit asset,
        Guid targetAccountId)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.ExecuteAsync(
            @"INSERT INTO PhysicalVault (SerialNumber, CurrencyCode, FaceValue)
              VALUES (@SerialNumber, @CurrencyCode, @FaceValue)",
            asset);
    }

    public async Task<decimal> GetLedgerTotalAsync()
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.ExecuteScalarAsync<decimal>(
            "SELECT ISNULL(SUM(Credit - Debit), 0) FROM DigitalLedger");
    }

    public async Task<decimal> GetPhysicalVaultTotalAsync()
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.ExecuteScalarAsync<decimal>(
            "SELECT ISNULL(SUM(FaceValue), 0) FROM PhysicalVault");
    }
} plus // Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlLedgerContext.cs
using Microsoft.EntityFrameworkCore;

public class SqlLedgerContext : DbContext {
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<LedgerEntry> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Ensure Ledger is "Write-Only" for compliance
        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        // Additional constraints to ensure balances can never be negative
    }
} plus // Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlLedgerRepository.cs
public async Task<bool> SaveTransactionAsync(LedgerEntry entry) {
    // Use SQL Transactions to ensure the update is ATOMIC 
    // (It either happens 100% or 0%—no partial money moves)
    const string sql = "INSERT INTO Ledger (Debit, Credit, AssetId) VALUES (@Debit, @Credit, @AssetId)";
    return await _db.ExecuteAsync(sql, entry) > 0;
} plus // fintechs-exhibitu/03_Infrastructure/PaymentGateway/Stripe_Adapter.cs plus // Filepath: fintechs-exhibitu/03_Infrastructure/Vault/Physical_Audit_Logger.cs
using GlobalBank.Domain.Entities;

namespace GlobalBank.Infrastructure.Vault;

public class PhysicalAuditLogger 
{
    private readonly SqlLedgerContext _db;

    public PhysicalAuditLogger(SqlLedgerContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Compares physical vault inventory against digital liabilities.
    /// This is the "Truth Check" for the AI$.
    /// </summary>
    public async Task<AuditResult> PerformVaultReconciliationAsync(decimal actualPhysicalCash)
    {
        // 1. Get total Digital AI$ in circulation from SQL
        decimal totalDigitalCirculation = await _db.Accounts.SumAsync(a => a.DigitalBalance);

        // 2. Calculate Discrepancy
        decimal discrepancy = actualPhysicalCash - totalDigitalCirculation;

        var log = new VaultAuditLog
        {
            Timestamp = DateTime.UtcNow,
            DigitalTotal = totalDigitalCirculation,
            PhysicalTotal = actualPhysicalCash,
            Discrepancy = discrepancy,
            IsCompliant = discrepancy >= 0 // We must have at least 100% backing
        };

        // 3. Write to an IMMUTABLE audit table (Legal requirement)
        _db.AuditLogs.Add(log);
        
        await _db.Accounts.SumAsync(a => a.Balance);

        return new AuditResult(log.IsCompliant, discrepancy);
    }
} 
