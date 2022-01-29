public class BidDto {
    public Guid Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public Guid UnicornId { get; set; }
    public double Amount { get; set; }
}