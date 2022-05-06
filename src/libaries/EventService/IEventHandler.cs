namespace EventService;

public interface IEventHandler<in TIntegrationEvent>  where TIntegrationEvent : Event
{
    Task Handle(TIntegrationEvent @event);
}
