using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using ScenarioDB.Repositories;

namespace ScenarioDB;

[ApiController]
[Route("admin/[controller]")]
public class SchemaController : ControllerBase
{
    public ILogger<SchemaController> Logger { get; }
    public ISchemaPathRepository SchemaPathRepository { get; }

    public SchemaController(
        ILogger<SchemaController> logger,
        ISchemaPathRepository schemaPathRepository
    )
    {
        Logger = logger;
        SchemaPathRepository = schemaPathRepository;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<SchemaPath>>> Get()
    {
        return Ok(await SchemaPathRepository.GetPathsAsync());
    }

    [HttpGet("{*path}")]
    public async Task<ActionResult<SchemaPath>> Get(string path)
    {
        return Ok(await SchemaPathRepository.GetSchemaAsync(WebUtility.UrlDecode(path)));
    }

    [HttpPost("{*path}")]
    public async Task<ActionResult<SchemaPath>> Post(string path, [FromBody] JSchema value)
    {
        return Ok(await SchemaPathRepository.CreateAsync(new SchemaPath(WebUtility.UrlDecode(path), value)));
    }

    [HttpPut("{*path}")]
    public async Task<ActionResult<SchemaPath>> Put(string path, [FromBody] JSchema value)
    {
        return Ok(await SchemaPathRepository.SetAsync(new SchemaPath(WebUtility.UrlDecode(path), value)));
    }
}
