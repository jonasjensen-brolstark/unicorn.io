namespace EventService;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;

    void AddSubscription<T, TH>()
        where T : Event
        where TH : IEventHandler<T>;

    void RemoveSubscription<T, TH>()
            where TH : IEventHandler<T>
            where T : Event;

    bool HasSubscriptionsForEvent<T>() where T : Event;
    void Clear();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : Event;
    string GetEventKey<T>();
}