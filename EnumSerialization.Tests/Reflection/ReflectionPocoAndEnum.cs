using System.Text.Json.Serialization;

namespace EnumSerialization.Tests.Reflection;

public sealed record ReflectionPoco
{
    public required ReflectionEnum ReflectionPropertyA { get; init; }
    public required ReflectionEnum ReflectionPropertyB { get; init; }
}

public enum ReflectionEnum
{
    ReflectionValueA = 0,

    [JsonPropertyName("reflection value b")]
    ReflectionValueB = 1,
}
