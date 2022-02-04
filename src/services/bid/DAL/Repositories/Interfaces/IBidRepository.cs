public interface IBidRepository
{
    Task<IEnumerable<Bid>> GetBidsAsync();
    Task<Bid> CreateBidAsync(double amount, Guid unicornId, Guid userId);
}