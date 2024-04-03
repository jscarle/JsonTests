using System.Text.Json.Serialization;

namespace EnumSerialization.Tests.Aot;

[JsonSourceGenerationOptions(Converters = [typeof(AotEnumConverter<AotEnum>)])]
[JsonSerializable(typeof(AotPoco))]
public partial class AotContext : JsonSerializerContext;

[JsonSourceGenerationOptions(Converters = [typeof(AotEnumConverter<AotEnum>)], PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(AotPoco))]
public partial class AotContextWithPropertyNamingPolicy : JsonSerializerContext;

public sealed record AotPoco
{
    public required AotEnum AotPropertyA { get; init; }
    public required AotEnum AotPropertyB { get; init; }
}

public enum AotEnum
{
    AotValueA = 0,

    [JsonPropertyName("aot value b")]
    AotValueB = 1,
}
