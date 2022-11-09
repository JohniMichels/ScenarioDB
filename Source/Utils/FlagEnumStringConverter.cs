

using Newtonsoft.Json;

namespace ScenarioDB.Utils;

public class FlagEnumStringArrayConverter<T> : JsonConverter
where T : struct, Enum
{
    public FlagEnumStringArrayConverter()
    {
        var type = typeof(T);
        if (!type.IsDefined(typeof(FlagsAttribute), false))
            throw new ArgumentException("Type must be a flag enum");
        EnumType = type;
    }

    public Type EnumType { get; }

    public override bool CanConvert(Type objectType)
    {
        return objectType == EnumType;
    }

    private T ReadSingleValue(string value)
    {
        return (T)Enum.Parse(EnumType, value.ToPascalCase());
    }

    private T ReadManyValues(string[] values)
    {
        var result = values
            .Select(ReadSingleValue)
            .Select(x => (int)(object)x)
            .Aggregate((a, b) => a | b);
        return (T)Enum.ToObject(EnumType, result);
    }

    private IEnumerable<string> ReadArray(JsonReader reader)
    {
        while (reader.Read() && reader.TokenType != JsonToken.EndArray)
        {
            yield return reader.TokenType != JsonToken.String
                ? throw new JsonSerializationException("Unexpected token type")
                : reader.Value?.ToString() ?? throw new JsonSerializationException("Unexpected null value");
        }
    }


    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        reader.Read();
        return reader.TokenType switch
        {
            JsonToken.StartArray => ReadManyValues(ReadArray(reader).ToArray()),
            JsonToken.String => ReadSingleValue(reader.Value!.ToString()!),
            JsonToken.Null => default,
            _ => throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing enum.")
        };
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var enumValue = (T)value!;
        writer.WriteStartArray();
        Enum.GetValues<T>()
            .Where(x => enumValue.HasFlag(x))
            .Select(x => x.ToString().ToLower())
            .ToList()
            .ForEach(writer.WriteValue);
        writer.WriteEndArray();
    }
}