// fintechs-exhibitu/01_Core_Domain/Entities/VaultAuditLogs.cs
namespace GlobalBank.Domain.Entities;

public class VaultAuditLog
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal DigitalTotal { get; set; }
    public decimal PhysicalTotal { get; set; }
    public decimal Discrepancy { get; set; }
    public bool IsCompliant { get; set; }
}
