using bid.Features.CreateBid;
using bid.Features.GetBids;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bid.Controllers;

[ApiController]
[Route("[controller]")]
public class BidController : ControllerBase
{

    private readonly ILogger<BidController> _logger;

    private readonly IMediator _mediator;

    public BidController(ILogger<BidController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetBids")]
    public async Task<IEnumerable<BidDto>> GetBidsAsync()
    {
        return await _mediator.Send(new GetBidsQuery());
    }

    [HttpPost(Name = "CreateBid")]
    public async Task<BidDto> CreateBidAsync(double amount, Guid unicornId, Guid userId)
    {
        return await _mediator.Send(new CreateBidCommand(amount, unicornId, userId));
    }
}