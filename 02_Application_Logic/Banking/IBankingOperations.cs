// -- filepath fintechs-exhibitu/02_Application_Logic/Banking/IBankingOperations.cs
using GlobalBank.Domain.Entities;
using GlobalBank.Domain.Interfaces;

namespace GlobalBank.Application.Banking
{
    public class BankingOperations : IBankingOperations
    {
        private readonly IBankingRepository _repository;

        public BankingOperations(IBankingRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> TransactAsync(Guid senderId, Guid receiverId, decimal amount)
        {
            var sender = _repository.GetAccountById(senderId);
            var receiver = _repository.GetAccountById(receiverId);

            if (sender.Balance < amount)
                return false; // AML + overdraft guard

            sender.Balance -= amount;
            receiver.Balance += amount;

            var tx = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = senderId,
                Amount = amount,
                TimestampUtc = DateTime.UtcNow
            };

            await _repository.SaveTransactionAsync(tx);
            await _repository.UpdateAccountAsync(sender);
            await _repository.UpdateAccountAsync(receiver);

            return true;
        }

        public Task<bool> ReconcileWithPhysicalVaultAsync()
        {
            // later: compare ledger totals vs physical vault totals
            return Task.FromResult(true);
        }
    }
}
