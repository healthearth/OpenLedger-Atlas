//
namespace OpenLedgerAtlas.Simulation_Engine;

public class FraudScenarioGenerator
{
    private readonly IBankingOperations _ops;
    private readonly Random _rng = new();

    public FraudScenarioGenerator(IBankingOperations ops)
    {
        _ops = ops;
    }

    /// <summary>
    /// Triggers a burst of fraudulent transactions for stress testing.
    /// </summary>
    public async Task TriggerBurstAsync(int count)
    {
        var accounts = await _ops.GetAllAccountIdsAsync();

        for (int i = 0; i < count; i++)
        {
            var from = accounts[_rng.Next(accounts.Count)];
            var to = accounts[_rng.Next(accounts.Count)];

            if (from == to) continue;

            decimal amount = Math.Round(
                (decimal)_rng.NextDouble() * 10000m, 2);

            await _ops.TransactAsync(from, to, amount);
        }
    }
}
