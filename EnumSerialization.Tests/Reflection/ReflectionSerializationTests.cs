using System.Text.Json;
using FluentAssertions;

namespace EnumSerialization.Tests.Reflection;

public class ReflectionSerializationTests
{
    private static readonly JsonSerializerOptions Options = new() { Converters = { new ReflectionEnumConverter() } };

    [Fact]
    public void ShouldSerialize()
    {
        // Arrange
        var poco = new ReflectionPoco { ReflectionPropertyA = ReflectionEnum.ReflectionValueA, ReflectionPropertyB = ReflectionEnum.ReflectionValueB };

        // Act
        var json = JsonSerializer.Serialize(poco, Options);

        // Assert
        const string expectedJson = "{\"ReflectionPropertyA\":\"ReflectionValueA\",\"ReflectionPropertyB\":\"reflection value b\"}";
        json.Should().Be(expectedJson);
    }

    [Fact]
    public void ShouldSerializeWithCustomPropertyNamingPolicy()
    {
        // Arrange
        var poco = new ReflectionPoco { ReflectionPropertyA = ReflectionEnum.ReflectionValueA, ReflectionPropertyB = ReflectionEnum.ReflectionValueB };
        var options = new JsonSerializerOptions(Options) { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        // Act
        var json = JsonSerializer.Serialize(poco, options);

        // Assert
        const string expectedJson = "{\"reflectionPropertyA\":\"reflectionValueA\",\"reflectionPropertyB\":\"reflection value b\"}";
        json.Should().Be(expectedJson);
    }

    [Fact]
    public void ShouldDeserializeWithIntegerValues()
    {
        // Arrange
        const string json = "{\"ReflectionPropertyA\":0,\"ReflectionPropertyB\":1}";

        // Act
        var poco = JsonSerializer.Deserialize<ReflectionPoco>(json, Options);

        // Assert
        var expectedPoco = new ReflectionPoco { ReflectionPropertyA = ReflectionEnum.ReflectionValueA, ReflectionPropertyB = ReflectionEnum.ReflectionValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldDeserializeWithStringValues()
    {
        // Arrange
        const string json = "{\"ReflectionPropertyA\":\"ReflectionValueA\",\"ReflectionPropertyB\":\"ReflectionValueB\"}";

        // Act
        var poco = JsonSerializer.Deserialize<ReflectionPoco>(json, Options);

        // Assert
        var expectedPoco = new ReflectionPoco { ReflectionPropertyA = ReflectionEnum.ReflectionValueA, ReflectionPropertyB = ReflectionEnum.ReflectionValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldDeserializeWithStringValuesCaseInsensitive()
    {
        // Arrange
        const string json = "{\"ReflectionPropertyA\":\"reflectionValueA\",\"ReflectionPropertyB\":\"reflectionValueB\"}";

        // Act
        var poco = JsonSerializer.Deserialize<ReflectionPoco>(json, Options);

        // Assert
        var expectedPoco = new ReflectionPoco { ReflectionPropertyA = ReflectionEnum.ReflectionValueA, ReflectionPropertyB = ReflectionEnum.ReflectionValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldDeserializeWithCustomStringValues()
    {
        // Arrange
        const string json = "{\"ReflectionPropertyA\":\"ReflectionValueA\",\"ReflectionPropertyB\":\"reflection value b\"}";

        // Act
        var poco = JsonSerializer.Deserialize<ReflectionPoco>(json, Options);

        // Assert
        var expectedPoco = new ReflectionPoco { ReflectionPropertyA = ReflectionEnum.ReflectionValueA, ReflectionPropertyB = ReflectionEnum.ReflectionValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldDeserializeWithCustomStringValuesCaseInsensitive()
    {
        // Arrange
        const string json = "{\"ReflectionPropertyA\":\"ReflectionValueA\",\"ReflectionPropertyB\":\"REFLECTION VALUE B\"}";

        // Act
        var poco = JsonSerializer.Deserialize<ReflectionPoco>(json, Options);

        // Assert
        var expectedPoco = new ReflectionPoco { ReflectionPropertyA = ReflectionEnum.ReflectionValueA, ReflectionPropertyB = ReflectionEnum.ReflectionValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldThrowWithUnknownIntegerValues()
    {
        // Arrange
        const string json = "{\"ReflectionPropertyA\":0,\"ReflectionPropertyB\":3}";

        // Act
        var act = () => _ = JsonSerializer.Deserialize<ReflectionPoco>(json, Options);

        // Assert
        act.Should().Throw<JsonException>();
    }
}
