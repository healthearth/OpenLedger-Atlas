// -- filepath fintechs-exhibitu/01_Core_Domain/Interfaces/IBankingOperations.cs
namespace GlobalBank.Domain.Interfaces;

public interface IBankingOperations
{
    Task<bool> TransactAsync(Guid senderId, Guid receiverId, decimal amount);
    Task<bool> ReconcileWithPhysicalVaultAsync();
}
