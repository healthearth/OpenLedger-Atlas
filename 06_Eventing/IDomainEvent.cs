//
// 06_Eventing/IDomainEvent.cs
namespace OpenLedgerAtlas.Eventing;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}
