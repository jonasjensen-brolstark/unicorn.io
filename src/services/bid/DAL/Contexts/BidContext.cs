using Microsoft.EntityFrameworkCore;

public class BidContext: DbContext {
    public BidContext(DbContextOptions<BidContext> options) : base(options) {
    }

    public DbSet<Bid>? Bids { get; set; }
}