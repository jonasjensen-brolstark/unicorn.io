using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using profile.Models;

namespace profile.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProfileController : ControllerBase
  {
    private static IEnumerable<Profile> _profiles = new Profile[] {
          new Profile{
              Id =  Guid.Parse("2bfb422d-7b50-41e7-86df-ecfc1ebde843"),
              Name = "Rainbow",
              Age= 19,
              Weight = 100,
            },
            new Profile{
              Id = Guid.Parse("0fd000a6-2cbf-4545-b7dd-099ebb170abf"),
              Name = "Happy",
              Age= 25,
              Weight = 125,
            },
            new Profile{
              Id = Guid.Parse("586adccb-d597-4eb0-b992-f43280c8ff5d"),
              Name = "Spotty",
              Age= 29,
              Weight = 115,
            },
            new Profile{
              Id = Guid.Parse("ed4ef265-d893-4fec-8ba9-743906c066e1"),
              Name = "John",
              Age= 52,
              Weight = 259,
            }
        };
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Profile> Get()
    {
      return _profiles;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(Guid id)
    {
      var profile = _profiles.FirstOrDefault(p => p.Id == id);

      if(profile == null) {
        return NotFound();
      }
      return Ok(profile);
    }
  }
}
