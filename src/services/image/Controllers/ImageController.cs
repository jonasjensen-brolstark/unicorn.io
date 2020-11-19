using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace image.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ImageController : ControllerBase
  {
    private readonly IWebHostEnvironment _env;

    private readonly ILogger<ImageController> _logger;

    public ImageController(ILogger<ImageController> logger, IWebHostEnvironment env)
    {
      _logger = logger;
      _env = env;
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
      var image = GetPhysicalPathFromRelativeUrl($"/Images/{id}.jpg");
      
      if (!System.IO.File.Exists(image))
      {
         image = GetPhysicalPathFromRelativeUrl($"/Images/default.jpg");
        // return NotFound();
      };

      return PhysicalFile(image, "image/jpeg");
    }

    private string GetPhysicalPathFromRelativeUrl(string url)
    {
      var path = Path.Combine(_env.ContentRootPath, url.TrimStart('/'));
      return path;
    }
  }
}
