using MediatR;

namespace bid.Features.CreateBid;

public class CreateBidCommand : IRequest<BidDto>
{
    public CreateBidCommand(double amount, Guid unicornId, Guid userId)
    {
        Amount = amount;
        UnicornId = unicornId;
        UserId = userId;
    }

    public double Amount { get; set; }
    public Guid UnicornId { get; set; }
    public Guid UserId { get; set; }
}