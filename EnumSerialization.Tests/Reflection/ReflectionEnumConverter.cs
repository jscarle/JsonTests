using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnumSerialization.Tests.Reflection;

public class ReflectionEnumConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var type = typeof(ReflectionEnumConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(type, options)!;
    }
}

public class ReflectionEnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    private readonly Dictionary<TEnum, string> _enumToString = new();
    private readonly Dictionary<int, TEnum> _numberToEnum = new();
    private readonly Dictionary<string, TEnum> _stringToEnum = new(StringComparer.InvariantCultureIgnoreCase);

    public ReflectionEnumConverter(JsonSerializerOptions options)
    {
        var type = typeof(TEnum);
        var names = Enum.GetNames<TEnum>();
        var values = Enum.GetValues<TEnum>();
        var underlying = Enum.GetValuesAsUnderlyingType<TEnum>().Cast<int>().ToArray();
        for (var index = 0; index < names.Length; index++)
        {
            var name = names[index];
            var value = values[index];
            var underlyingValue = underlying[index];

            var attribute = type.GetMember(name)[0]
                .GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .Cast<JsonPropertyNameAttribute>()
                .FirstOrDefault();

            var defaultStringValue = FormatName(name, options);
            var customStringValue = attribute?.Name;

            _enumToString.Add(value, customStringValue ?? defaultStringValue);
            _stringToEnum.Add(defaultStringValue, value);
            if (customStringValue is not null)
                _stringToEnum.Add(customStringValue, value);
            _numberToEnum.Add(underlyingValue, value);
        }
    }

    private static string FormatName(string name, JsonSerializerOptions options)
    {
        return options.PropertyNamingPolicy?.ConvertName(name) ?? name;
    }

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            {
                var stringValue = reader.GetString();

                if (stringValue is not null && _stringToEnum.TryGetValue(stringValue, out var enumValue))
                    return enumValue;
                break;
            }
            case JsonTokenType.Number:
            {
                if (reader.TryGetInt32(out var numValue) && _numberToEnum.TryGetValue(numValue, out var enumValue))
                    return enumValue;
                break;
            }
        }

        throw new JsonException(
            $"The JSON value '{
                Encoding.UTF8.GetString(reader.ValueSpan)
            }' could not be converted to {typeof(TEnum).FullName}. BytePosition: {reader.BytesConsumed}."
        );
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(_enumToString[value]);
    }
}
