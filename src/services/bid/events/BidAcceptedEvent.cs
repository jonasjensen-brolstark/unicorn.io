using EventService;

namespace bid.Events;

public record BidAcceptedEvent : Event
{
    public double Amount { get; set; }
    public Guid UnicornId { get; set; }
    public Guid UserId { get; set; }
    public Guid BidId { get; set; }

    public BidAcceptedEvent(BidPlacedEvent bidPlaced) : base()
    {
        Amount = bidPlaced.Amount;
        UnicornId = bidPlaced.UnicornId;
        UserId = bidPlaced.UserId;
        BidId = bidPlaced.Id;
    }
}