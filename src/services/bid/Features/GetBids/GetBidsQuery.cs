using MediatR;

namespace bid.Features.GetBids;

public class GetBidsQuery : IRequest<IEnumerable<BidDto>> {}