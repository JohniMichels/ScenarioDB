using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace TestScenarioDB;

public class NewtonsoftJsonReader
{
    [Fact]
    [ThirdPartyTest]
    public void ArrayReading()
    {
        var reader = new JsonTextReader(new StringReader("[123, 456, 789]"));
        reader.Read();
        reader.TokenType.Should().Be(JsonToken.StartArray);
        reader.Read();
        reader.TokenType.Should().Be(JsonToken.Integer);
        reader.ReadAsInt32().Should().Be(456);
        reader.ReadAsInt32().Should().Be(789);
        reader.Value.Should().Be(789);
    }

    [Fact]
    [ThirdPartyTest]
    public void ArrayWriting()
    {
        var stringValue = new StringWriter();
        var writer = new JsonTextWriter(stringValue);
        writer.WriteStartArray();
        writer.WriteValue(123);
        writer.WriteValue(456);
        writer.WriteValue(789);
        writer.WriteEndArray();
        writer.Flush();
        writer.Close();
        stringValue.ToString().Should().Be("[123,456,789]");
    }

    [Fact]
    [ThirdPartyTest]
    public void EnumerationStringIsEnumName()
    {
        var value = Newtonsoft.Json.Schema.JSchemaType.String;
        value.ToString().Should().Be("String");
    }
}