using Newtonsoft.Json.Schema;

namespace ScenarioDB.Repositories;
public interface ISchemaPathRepository
{
    Task<JSchema> GetSchemaAsync(string path);
    Task<SchemaPath?> SetAsync(SchemaPath schemaPath);
    Task<IEnumerable<string>> GetPathsAsync();
    Task<JSchema?> DeleteAsync(string path);
    Task<SchemaPath?> CreateAsync(SchemaPath schemaPath);
}

public interface IDataRepository
{
    Task<IEnumerable<string>> GetAsync(string path);
    Task<string> SetAsync(string path, object data);
    Task<IEnumerable<string>> DeleteAsync(string path);
}