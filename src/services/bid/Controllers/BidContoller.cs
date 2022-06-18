using bid.Events;
using bid.Features.CreateBid;
using bid.Features.GetBids;
using EventService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bid.Controllers;

[ApiController]
[Route("[controller]")]
public class BidController : ControllerBase
{

    private readonly ILogger<BidController> _logger;

    private readonly IMediator _mediator;
    private readonly IEventService _eventService;

    public BidController(ILogger<BidController> logger, IMediator mediator, IEventService eventService)
    {
        _logger = logger;
        _mediator = mediator;
        _eventService = eventService;
    }

    [HttpGet(Name = "GetBids")]
    public async Task<IEnumerable<BidDto>> GetBidsAsync()
    {
        return await _mediator.Send(new GetBidsQuery());
    }

    [HttpPost(Name = "CreateBid")]
    public async Task<BidDto> CreateBidAsync(double amount, Guid unicornId, Guid userId)
    {
        var bidPlacedEvent = new BidPlacedEvent{
            Amount = amount,
            UnicornId = unicornId,
            UserId = userId
        };
        _logger.LogInformation("Publishing {BidPlacedEvent}", bidPlacedEvent);
        _eventService.Publish(bidPlacedEvent);
        return await _mediator.Send(new CreateBidCommand(amount, unicornId, userId));
    }
}