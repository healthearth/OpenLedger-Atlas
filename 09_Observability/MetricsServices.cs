//
public class MetricsService
{
    private readonly OpenLedgerDbContext _db;

    public MetricsService(OpenLedgerDbContext db)
    {
        _db = db;
    }

    public async Task<object> GetMetrics()
    {
        var totalUsers = await _db.Users.CountAsync();
        var totalTx = await _db.Transactions.CountAsync();
        var totalVolume = await _db.Transactions.SumAsync(t => t.Amount);

        return new
        {
            Users = totalUsers,
            Transactions = totalTx,
            Volume = totalVolume
        };
    }
}
