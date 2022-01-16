using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;

[ApiController]
[Route("[controller]")]
public class BidController : ControllerBase
{
    private readonly ILogger<BidController> _logger;

    public BidController(ILogger<BidController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Bid>> Get()
    {
        var bidClient = new RestClient("http://profile");
        var BidsRequest = new RestRequest("bid", DataFormat.Json);
        return await bidClient.GetAsync<List<Bid>>(BidsRequest);
    }
}