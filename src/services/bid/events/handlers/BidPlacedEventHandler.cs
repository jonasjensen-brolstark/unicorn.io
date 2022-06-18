using bid.Features.CreateBid;
using EventService;
using MediatR;

namespace bid.Events.Handlers;

public class BidPlacedEventHandler : IEventHandler<BidPlacedEvent>
{

    private readonly ILogger<BidPlacedEventHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IEventService _eventService;

    public BidPlacedEventHandler(ILogger<BidPlacedEventHandler> logger, IMediator mediator, IEventService eventService)
    {
        _logger = logger;
        _mediator = mediator;
        _eventService = eventService;
    }

    public Task Handle(BidPlacedEvent @event)
    {

        _logger.LogInformation("BidPlacedEvent recieved {BidPlacedEvent}", @event);
        var createBidCommand = new CreateBidCommand(@event.Amount, @event.UnicornId, @event.UserId);
        // await _mediator.Send(createBidCommand);
        // TODO: check if bid is created successfully else publish an reject event
        var bidAcceptedEvent = new BidAcceptedEvent(@event);

        _logger.LogInformation("Publishing {BidAcceptedEvent}", bidAcceptedEvent);
        _eventService.Publish(bidAcceptedEvent);

        return Task.CompletedTask;
    }
}