public class Bid {
    public Guid Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public Guid UnicornId { get; set; }
    public Guid UserId { get; set; }
    public double Amount { get; set; }
}