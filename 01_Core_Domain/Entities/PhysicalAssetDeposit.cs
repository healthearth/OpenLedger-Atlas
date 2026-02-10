// Filepath: fintechs-exhibitu/01_Core_Domain/Entities/PhysicalAssetDeposit.cs

namespace GlobalBank.Domain.Entities;

public class PhysicalAssetDeposit
{
    public Guid Id { get; private set; }
    public decimal FaceValue { get; private set; }
    public string CurrencyCode { get; private set; }
    public string SerialNumber { get; private set; }
    public DateTime DepositedAtUtc { get; private set; }

    public PhysicalAssetDeposit(
        decimal faceValue,
        string currencyCode,
        string serialNumber)
    {
        Id = Guid.NewGuid();
        FaceValue = faceValue;
        CurrencyCode = currencyCode;
        SerialNumber = serialNumber;
        DepositedAtUtc = DateTime.UtcNow;
    }
}
