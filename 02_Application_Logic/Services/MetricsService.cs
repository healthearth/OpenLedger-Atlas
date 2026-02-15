using Microsoft.EntityFrameworkCore;
using OpenLedgerAtlas.Infrastructure.Persistence;

namespace OpenLedgerAtlas.Application.Services
{
    public class MetricsService
    {
        private readonly OpenLedgerDbContext _context;

        public MetricsService(OpenLedgerDbContext context)
        {
            _context = context;
        }

        public double TransactionsPerSecond()
        {
            var oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);
            var count = _context.Transactions
                .Count(t => t.Timestamp >= oneMinuteAgo);

            return count / 60.0;
        }

        public int FraudCount()
        {
            return _context.Transactions.Count(t => t.IsFraud);
        }

        public int ActiveUsers()
        {
            return _context.Users.Count(u => u.IsActive);
        }

        public decimal CurrencyVolatilityPercent()
        {
            var rates = _context.ExchangeRates
                .OrderByDescending(r => r.Timestamp)
                .Take(2)
                .ToList();

            if (rates.Count < 2) return 0;

            var change = rates[0].Rate - rates[1].Rate;
            return (change / rates[1].Rate) * 100;
        }
    }
}
