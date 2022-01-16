using Microsoft.AspNetCore.Mvc;

namespace bid.Controllers;

[ApiController]
[Route("[controller]")]
public class BidController : ControllerBase
{

    private readonly ILogger<BidController> _logger;
    private readonly IBidService _bidService;

    public BidController(ILogger<BidController> logger, IBidService bidService)
    {
        _logger = logger;
        _bidService = bidService;
    }

    [HttpGet(Name = "GetBids")]
    public async Task<IEnumerable<BidDto>> GetBidsAsync()
    {
        return await _bidService.GetBidsAsync();
    }

    [HttpPost(Name = "CreateBid")]
    public async Task<BidDto> CreateBidAsync(double amount)
    {
        return await _bidService.CreateBidAsync(amount);
    }
}