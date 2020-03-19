﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Models;
using api.Dtos;
using RestSharp;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnicornController : ControllerBase
    {
        private readonly ILogger<UnicornController> _logger;

        public UnicornController(ILogger<UnicornController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Unicorn> Get()
        {
            var profileClient = new RestClient("http://profile");
            var profileRequest = new RestRequest("profile", DataFormat.Json);
            var profiles = profileClient.Get<List<Profile>>(profileRequest).Data;

            var pricingClient = new RestClient("http://pricing");
            var pricingRequest = new RestRequest("price", DataFormat.Json);
            var pricings = pricingClient.Get<List<Pricing>>(pricingRequest).Data;

            var unicorns = new List<Unicorn>();
            foreach(var profile in profiles) {
              unicorns.Add(new Unicorn{
                Age = profile.Age,
                Name = profile.Name,
                Weight = profile.Weight,
                Price = pricings.FirstOrDefault(p => p.UnicornId == profile.Id).Price,
                Id = profile.Id
              });
            }

            return unicorns;
        }
    }
}
