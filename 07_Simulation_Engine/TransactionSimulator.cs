//
namespace OpenLedgerAtlas.Simulation_Engine;

public class TransactionSimulator
{
    private readonly IBankingOperations _ops;
    private readonly Random _rng = new();

    public async Task GenerateAsync(
        List<Guid> accounts,
        int volume = 10000)
    {
        for (int i = 0; i < volume; i++)
        {
            var from = accounts[_rng.Next(accounts.Count)];
            var to = accounts[_rng.Next(accounts.Count)];

            if (from == to) continue;

            decimal amount = Math.Round(
                (decimal)_rng.NextDouble() * 5000m, 2);

            await _ops.TransactAsync(from, to, amount);
        }
    }
}
