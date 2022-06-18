using EventService;

namespace bid.Events;

public record BidPlacedEvent : Event
{
    public double Amount { get; set; }
    public Guid UnicornId { get; set; }
    public Guid UserId { get; set; }
}