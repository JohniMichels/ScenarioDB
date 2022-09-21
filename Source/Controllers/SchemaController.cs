using Microsoft.AspNetCore.Mvc;

namespace ScenarioDB;

[ApiController]
[Route("admin/[controller]")]
public class SchemaController : ControllerBase
{
    public ILogger<SchemaController> Logger { get; private set; }
    
    public SchemaController(ILogger<SchemaController> logger)
    {
        Logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SchemaPath>> Get()
    {
        return Ok(new List<SchemaPath>() {context, context});
    }

    private static SchemaPath context = new();

    [HttpPost]
    public ActionResult<SchemaPath> Post([FromBody] SchemaPath value)
    {
        context = value;
        return value;
    }
}
