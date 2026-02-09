// Filepath: fintechs-exhibitu/01_Core_Domain/Interfaces/IBankingRepository.cs
using GlobalBank.Domain.Entities;

namespace GlobalBank.Domain.Interfaces
{
    public interface IBankingRepository
    {
        Account GetAccountById(Guid id);
        Task SaveTransactionAsync(Transaction transaction);
        Task UpdateAccountAsync(Account account);
    }
}
