using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bid.Features.GetBids;

public class GetBidsHandler : IRequestHandler<GetBidsQuery, IEnumerable<BidDto>>
{
    private readonly BidContext _context;

    public GetBidsHandler(BidContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<BidDto>> Handle(GetBidsQuery request, CancellationToken cancellationToken)
    {
        if (_context.Bids == null)
        {
            return new List<BidDto>();
        }
        var bids = await _context.Bids.ToListAsync();

        return bids.Select(bid => new BidDto
        {
            Id = bid.Id,
            Amount = bid.Amount,
            Timestamp = bid.Timestamp,
            UnicornId = bid.UnicornId
        });
    }
}