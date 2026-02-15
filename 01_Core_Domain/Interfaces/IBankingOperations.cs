// -- filepath fintechs-exhibitu/01_Core_Domain/Interfaces/IBankingOperations.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

namespace OpenLLedgerAtlas.Domain.Interfaces;

public interface IBankingOperations
{
    Task<bool> TransactAsync(Guid senderId, Guid receiverId, decimal amount);
    Task<bool> ReconcileWithPhysicalVaultAsync();
    Task<List<Guid>> GetAllAccountIdsAsync();

}
