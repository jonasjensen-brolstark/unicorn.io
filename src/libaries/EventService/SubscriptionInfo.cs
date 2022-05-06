namespace EventService;

public class SubscriptionInfo
{
    public bool IsDynamic { get; }
    public Type HandlerType { get; }

    private SubscriptionInfo(Type handlerType)
    {
        HandlerType = handlerType;
    }

    public static SubscriptionInfo Typed(Type handlerType) =>
        new SubscriptionInfo(handlerType);
}