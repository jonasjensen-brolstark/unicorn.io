public class BidService : IBidService
{
    private readonly IBidRepository _bidRepository;

    public BidService(IBidRepository bidRepository)
    {
        _bidRepository = bidRepository;
    }

    public async Task<IEnumerable<BidDto>> GetBidsAsync()
    {
        var bids = await _bidRepository.GetBidsAsync();
        return bids.Select(bid => new BidDto
        {
            Id = bid.Id, 
            Amount = bid.Amount,
            Timestamp = bid.Timestamp
        });
    }

    public async Task<BidDto> CreateBidAsync(double amount)
    {
        if (amount > 0)
        {
            var bid = await _bidRepository.CreateBidAsync(amount);
            return new BidDto
            {
                Id = bid.Id, 
                Amount = bid.Amount,
                Timestamp = bid.Timestamp
            };
        }

        return new BidDto();
    }
}