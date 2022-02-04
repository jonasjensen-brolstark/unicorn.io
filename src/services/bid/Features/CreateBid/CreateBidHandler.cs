using MediatR;

namespace bid.Features.CreateBid;

public class CreateBidHandler : IRequestHandler<CreateBidCommand, BidDto>
{

    private readonly BidContext _context;

    public CreateBidHandler(BidContext context)
    {
        _context = context;
    }

    public async Task<BidDto> Handle(CreateBidCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount > 0)
        {
            if (_context.Bids != null)
            {
                var bid = new Bid
                {
                    Id = Guid.NewGuid(),
                    Amount = request.Amount,
                    Timestamp = DateTimeOffset.Now,
                    UnicornId = request.UnicornId,
                    UserId = request.UserId
                };
                await _context.Bids.AddAsync(bid);
                await _context.SaveChangesAsync();

                return new BidDto
                {
                    Id = bid.Id,
                    Amount = bid.Amount,
                    Timestamp = bid.Timestamp,
                    UnicornId = bid.UnicornId
                };
            }
        }

        return new BidDto();
    }
}