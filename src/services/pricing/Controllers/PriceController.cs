using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pricing.Context;
using pricing.Models;

namespace pricing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly ILogger<PriceController> _logger;
        private readonly PricingContext _pricingContext;

        public PriceController(ILogger<PriceController> logger, PricingContext pricingContext)
        {
            _logger = logger;
            _pricingContext = pricingContext;
        }

        [HttpGet]
        public IEnumerable<Pricing> Get()
        {
            return _pricingContext.Pricings;
        }
    }
}
