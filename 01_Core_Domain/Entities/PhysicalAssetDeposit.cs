// -- fintechs-exhibitu/01_Core_Domain/Entities/PhysicalAssetDeposit.cs
namespace GlobalBank.Domain.Entities;

public class PhysicalAssetDeposit
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string AssetType { get; set; } = "USD";
    public DateTime DepositedAtUtc { get; set; }
}
