//
// 06_Eventing/SimpleEventBus.cs
namespace OpenLedgerAtlas.Eventing;

public class SimpleEventBus
{
    private readonly Dictionary<Type, List<Func<IDomainEvent, Task>>> _handlers = new();

    public void Register<T>(Func<T, Task> handler) where T : IDomainEvent
    {
        var type = typeof(T);
        if (!_handlers.ContainsKey(type))
            _handlers[type] = new();

        _handlers[type].Add(e => handler((T)e));
    }

    public async Task PublishAsync(IDomainEvent domainEvent)
    {
        var type = domainEvent.GetType();
        if (!_handlers.ContainsKey(type)) return;

        foreach (var handler in _handlers[type])
            await handler(domainEvent);
    }
}
