using EventService;

namespace bid.Events;

public record BidRejectedEvent : Event
{
    public Guid BidId { get; set; }

    public BidRejectedEvent(Guid bidId) : base()
    {
        BidId = bidId;
    }
}