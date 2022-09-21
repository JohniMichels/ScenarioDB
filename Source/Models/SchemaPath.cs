
using Newtonsoft.Json.Schema;

namespace ScenarioDB;

public class SchemaPath
{
    public string Path { get; set; }
    public JsonSchema Schema { get; set; }
    
}