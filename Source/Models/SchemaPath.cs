
using Newtonsoft.Json.Schema;

namespace ScenarioDB;

public record SchemaPath(string Path, JSchema Schema);
