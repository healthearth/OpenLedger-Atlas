using Microsoft.EntityFrameworkCore;

namespace OpenSourceBank.Infrastructure.Data
{
    public class OpenLedgerDbContext : DbContext
    {
        public OpenLedgerDbContext(DbContextOptions<OpenLedgerDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
    }
}
