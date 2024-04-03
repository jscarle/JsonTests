using System.Text.Json;
using FluentAssertions;

namespace EnumSerialization.Tests.Default;

public class DefaultSerializationTests
{
    [Fact]
    public void ShouldSerialize()
    {
        // Arrange
        var poco = new DefaultPoco { DefaultPropertyA = DefaultEnum.DefaultValueA, DefaultPropertyB = DefaultEnum.DefaultValueB };

        // Act
        var json = JsonSerializer.Serialize(poco);

        // Assert
        const string expectedJson = "{\"DefaultPropertyA\":0,\"DefaultPropertyB\":1}";
        json.Should().Be(expectedJson);
    }

    [Fact]
    public void ShouldDeserialize()
    {
        // Arrange
        const string json = "{\"DefaultPropertyA\":0,\"DefaultPropertyB\":1}";

        // Act
        var poco = JsonSerializer.Deserialize<DefaultPoco>(json);

        // Assert
        var expectedPoco = new DefaultPoco { DefaultPropertyA = DefaultEnum.DefaultValueA, DefaultPropertyB = DefaultEnum.DefaultValueB };
        poco.Should().Be(expectedPoco);
    }

    [Fact]
    public void ShouldDeserializeIncorrectlyWithUnknownIntegerValue()
    {
        // Arrange
        const string json = "{\"DefaultPropertyA\":0,\"DefaultPropertyB\":2}";

        // Act
        var poco = JsonSerializer.Deserialize<DefaultPoco>(json)!;

        // Assert
        var intValues = Enum.GetValuesAsUnderlyingType<DefaultEnum>();
        foreach (int intValue in intValues)
            ((int)poco.DefaultPropertyB).Should().NotBe(intValue);
    }

    [Fact]
    public void ShouldThrowWithStringValues()
    {
        // Arrange
        const string json = "{\"DefaultPropertyA\":\"DefaultValueA\",\"DefaultPropertyB\":\"DefaultValueB\"}";

        // Act
        var act = () => _ = JsonSerializer.Deserialize<DefaultPoco>(json);

        // Assert
        act.Should().Throw<JsonException>();
    }
}
