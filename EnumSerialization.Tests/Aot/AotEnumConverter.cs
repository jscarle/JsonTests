using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnumSerialization.Tests.Aot;

public sealed class AotEnumConverter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] TEnum>()
    : JsonStringEnumConverter<TEnum>(ResolveNamingPolicy())
    where TEnum : struct, Enum
{
    private static JsonPropertyNamingPolicy? ResolveNamingPolicy()
    {
        var map = typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => (f.Name, AttributeName: f.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name))
            .Where(pair => pair.AttributeName != null)
            .ToDictionary();

        return map.Count > 0 ? new JsonPropertyNamingPolicy(map!) : null;
    }

    private sealed class JsonPropertyNamingPolicy(IReadOnlyDictionary<string, string> map) : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return map.GetValueOrDefault(name, name);
        }
    }
}
