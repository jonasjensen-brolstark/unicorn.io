public interface IBidService
{
    public Task<IEnumerable<BidDto>> GetBidsAsync();
    public Task<BidDto> CreateBidAsync(double amount, Guid unicornId, Guid userId);
}