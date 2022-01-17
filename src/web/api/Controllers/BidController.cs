using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;

[ApiController]
[Route("[controller]")]
public class BidsController : ControllerBase
{
    private readonly ILogger<BidsController> _logger;

    public BidsController(ILogger<BidsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Bid>> Get()
    {
        var bidClient = new RestClient("http://bid");
        var BidsRequest = new RestRequest("bid", DataFormat.Json);
        return await bidClient.GetAsync<List<Bid>>(BidsRequest);
    }

    [HttpPost]
    public async Task<Bid> Post(double amount)
    {
        var bidClient = new RestClient("http://bid");
        var BidsRequest = new RestRequest($"bid", DataFormat.Json);
        BidsRequest.AddQueryParameter("amount", amount.ToString());
        return await bidClient.PostAsync<Bid>(BidsRequest);
    }
}