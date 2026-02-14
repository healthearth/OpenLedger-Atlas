//
// 06_Eventing/IDomainEvent.cs
namespace GlobalBank.Eventing;

public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}
