// -- fintechs-exhibitu/01_Core_Domain/Entities/PhysicalAssetDeposit.cs
namespace GlobalBank.Domain.Entities;

public class PhysicalAssetDeposit
{
    public Guid Id { get; set; }
    public decimal FaceValue { get; set; }
    public string CurrencyCode { get; set; } = "USD";
    public string SerialNumber { get; set; } = string.Empty;
    public DateTime DepositedAtUtc { get; set; }
}
