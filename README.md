# System.Text.Json Custom Enum Serialization

Comparing different methods of serializing and deserializing custom enum values using [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json).

- Using the default `JsonSerializer` (see [source](https://github.com/jscarle/SystemTextJsonCustomEnumSerialization/blob/main/EnumSerialization.Tests/Default)📄).
- Using a reflection based `JsonConverter` (see [source](https://github.com/jscarle/SystemTextJsonCustomEnumSerialization/tree/main/EnumSerialization.Tests/Reflection)📄).
- Using an AOT compatible `JsonStringEnumConverter` with [source generation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation) 
as explained by [Eirik Tsarpalis](https://github.com/eiriktsarpalis) in this [comment](https://github.com/dotnet/runtime/issues/74385#issuecomment-1705083109) (see [source](https://github.com/jscarle/SystemTextJsonCustomEnumSerialization/tree/main/EnumSerialization.Tests/Aot)📄).

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/jscarle/SystemTextJsonCustomEnumSerialization/main.yml?logo=github&label=tests)

