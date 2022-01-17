using Microsoft.EntityFrameworkCore;

public class BidRepository : IBidRepository
{
    private readonly BidContext _context;

    public BidRepository(BidContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Bid>> GetBidsAsync()
    {
        if(_context.Bids == null)
        {
            return new List<Bid>();
        }
        return await _context.Bids.ToListAsync();
    }

    public async Task<Bid> CreateBidAsync(double amount)
    {
        if (_context.Bids != null && amount > 0)
        {
            var bid = new Bid
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                Timestamp = DateTimeOffset.Now
            };
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();
            return bid;
        }
        return new Bid();
    }
}