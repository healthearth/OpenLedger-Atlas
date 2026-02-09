// Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlBankingRepository.cs
using Dapper;
using Microsoft.Data.SqlClient;

public class SqlBankingRepository : IBankingRepository {
    private readonly string _connectionString = "Your_Secure_Connection_String";

    public async Task RegisterPhysicalAssetAsync(PhysicalAssetDeposit asset, Guid targetAccountId) {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var transaction = conn.BeginTransaction();

        try {
            // 1. Insert the physical bill into the Vault
            var vaultSql = @"INSERT INTO PhysicalVault (SerialNumber, CurrencyCode, FaceValue) 
                             VALUES (@SerialNumber, @CurrencyCode, @FaceValue)";
            await conn.ExecuteAsync(vaultSql, asset, transaction);

            // 2. Mint the digital AI$ in the Ledger
            // Logic: $10 USD = 10 AI$ (Initial peg)
            var ledgerSql = @"INSERT INTO DigitalLedger (AccountId, Credit, PhysicalAssetRef, Description) 
                              VALUES (@AccountId, @Credit, @AssetRef, 'Startup Capital Deposit')";
            
            await conn.ExecuteAsync(ledgerSql, new { 
                AccountId = targetAccountId, 
                Credit = asset.FaceValue, 
                AssetRef = asset.SerialNumber 
            }, transaction);

            transaction.Commit();
        } catch {
            transaction.Rollback();
            throw; // Preserve the "All-or-Nothing" banking rule
        }
    }
}
