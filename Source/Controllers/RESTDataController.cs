using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net;

namespace ScenarioDB;

[ApiController]
[Route("api")]
public class RESTDataController : ControllerBase
{
    public ILogger<RESTDataController> Logger { get; private set; }

    public RESTDataController(
        ILogger<RESTDataController> logger)
    {
        Logger = logger;
    }

    [HttpGet("{*path}")]
    public ActionResult<IEnumerable<string>> Get(string path)
    {
        return Ok(new List<string>() {WebUtility.UrlDecode(path), path});
    }

}