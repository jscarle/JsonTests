using System.Text.Json;
using FluentAssertions;

namespace EnumSerialization.Tests.Aot;

public class AotSerializationTests
{
    [Fact]
    public void ShouldSerialize()
    {
        // Arrange
        var poco = new AotPoco { AotPropertyA = AotEnum.AotValueA, AotPropertyB = AotEnum.AotValueB };

        // Act
        var json = JsonSerializer.Serialize(poco, AotContext.Default.AotPoco);

        // Assert
        const string expectedJson = "{\"AotPropertyA\":\"AotValueA\",\"AotPropertyB\":\"aot value b\"}";
        json.Should().Be(expectedJson);
    }

    [Fact]
    public void ShouldSerializeWithCustomPropertyNamingPolicy()
    {
        // Arrange
        var poco = new AotPoco { AotPropertyA = AotEnum.AotValueA, AotPropertyB = AotEnum.AotValueB };

        // Act
        var json = JsonSerializer.Serialize(poco, AotContextWithPropertyNamingPolicy.Default.AotPoco);

        // Assert
        const string expectedJson = "{\"aotPropertyA\":\"aotValueA\",\"aotPropertyB\":\"aot value b\"}";
        json.Should().Be(expectedJson);
    }

    [Fact]
    public void ShouldDeserializeWithIntegerValues()
    {
        // Arrange
        const string json = "{\"AotPropertyA\":0,\"AotPropertyB\":1}";

        // Act
        var poco = JsonSerializer.Deserialize(json, AotContext.Default.AotPoco);

        // Assert
        var expectedPoco = new AotPoco { AotPropertyA = AotEnum.AotValueA, AotPropertyB = AotEnum.AotValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldDeserializeWithStringValues()
    {
        // Arrange
        const string json = "{\"AotPropertyA\":\"AotValueA\",\"AotPropertyB\":\"AotValueB\"}";

        // Act
        var poco = JsonSerializer.Deserialize(json, AotContext.Default.AotPoco);

        // Assert
        var expectedPoco = new AotPoco { AotPropertyA = AotEnum.AotValueA, AotPropertyB = AotEnum.AotValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldDeserializeWithStringValuesCaseInsensitive()
    {
        // Arrange
        const string json = "{\"AotPropertyA\":\"aotValueA\",\"AotPropertyB\":\"aotValueB\"}";

        // Act
        var poco = JsonSerializer.Deserialize(json, AotContext.Default.AotPoco);

        // Assert
        var expectedPoco = new AotPoco { AotPropertyA = AotEnum.AotValueA, AotPropertyB = AotEnum.AotValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldDeserializeWithCustomStringValues()
    {
        // Arrange
        const string json = "{\"AotPropertyA\":\"AotValueA\",\"AotPropertyB\":\"aot value b\"}";

        // Act
        var poco = JsonSerializer.Deserialize(json, AotContext.Default.AotPoco);

        // Assert
        var expectedPoco = new AotPoco { AotPropertyA = AotEnum.AotValueA, AotPropertyB = AotEnum.AotValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldDeserializeWithCustomStringValuesCaseInsensitive()
    {
        // Arrange
        const string json = "{\"AotPropertyA\":\"AotValueA\",\"AotPropertyB\":\"AOT VALUE B\"}";

        // Act
        var poco = JsonSerializer.Deserialize(json, AotContext.Default.AotPoco);

        // Assert
        var expectedPoco = new AotPoco { AotPropertyA = AotEnum.AotValueA, AotPropertyB = AotEnum.AotValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldThrowWithUnknownIntegerValues()
    {
        // Arrange
        const string json = "{\"AotPropertyA\":0,\"AotPropertyB\":2}";

        // Act
        var act = () => _ = JsonSerializer.Deserialize(json, AotContext.Default.AotPoco);

        // Assert
        act.Should().Throw<JsonException>();
    }
}
