using Newtonsoft.Json.Schema;

namespace ScenarioDB.Repositories;


public class MockRepository : ISchemaPathRepository
{
    public IDictionary<string, JSchema> InternalDictionary { get; } = new Dictionary<string, JSchema>();
    public Task<SchemaPath?> CreateAsync(SchemaPath schemaPath)
    {
        InternalDictionary.Add(schemaPath.Path, schemaPath.Schema);
        return Task.FromResult<SchemaPath?>(schemaPath);
    }

    public Task<JSchema?> DeleteAsync(string path)
    {
        return InternalDictionary.Remove(path) ?
            Task.FromResult<JSchema?>(InternalDictionary[path]) :
            Task.FromResult<JSchema?>(null);
    }

    public Task<IEnumerable<string>> GetPathsAsync()
    {
        return Task.FromResult<IEnumerable<string>>(InternalDictionary.Keys);
    }

    public Task<JSchema> GetSchemaAsync(string path)
    {
        return Task.FromResult<JSchema>(InternalDictionary[path]);
    }

    public Task<SchemaPath?> SetAsync(SchemaPath schemaPath)
    {
        InternalDictionary[schemaPath.Path] = schemaPath.Schema;
        return Task.FromResult<SchemaPath?>(schemaPath);
    }
}