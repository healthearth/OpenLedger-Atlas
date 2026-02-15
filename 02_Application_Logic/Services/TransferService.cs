//
using OpenLedgerAtlas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace OpenLedgerAtlas.Application.Services
{
    public class TransferService
    {
        private readonly OpenLedgerDbContext _db;

        public TransferService(OpenLedgerDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Transfer(Guid fromUserId, Guid toUserId, decimal amount)
        {
            var from = await _db.Accounts.FirstAsync(a => a.UserId == fromUserId);
            var to = await _db.Accounts.FirstAsync(a => a.UserId == toUserId);

            if (!from.Debit(amount))
                return false;

            to.Credit(amount);

            _db.Transactions.Add(new Transaction
            {
                FromAccountId = from.Id,
                ToAccountId = to.Id,
                Amount = amount
            });

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
