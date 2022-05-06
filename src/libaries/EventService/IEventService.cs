namespace EventService;

public interface IEventService
{
    void Publish(Event @event);

    void Subscribe<T, TH>()
        where T : Event
        where TH : IEventHandler<T>;

    void Unsubscribe<T, TH>()
        where TH : IEventHandler<T>
        where T : Event;
}
