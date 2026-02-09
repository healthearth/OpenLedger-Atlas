// fintechs-exhibitu/01_Core_Domain/Entities/VaultAuditLogs.cs
namespace GlobalBank.Domain.Entities;

public class VaultAuditLog
{
    public Guid Id { get; set; }
    public decimal LedgerTotal { get; set; }
    public decimal PhysicalTotal { get; set; }
    public DateTime LoggedAtUtc { get; set; }
}
