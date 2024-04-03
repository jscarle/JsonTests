# System.Text.Json Custom Enum Serialization

Comparing different methods (using the default `JsonSerializer`, using a reflection based `JsonConverter`, and using an
AOT [source generation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation)
compatible `JsonStringEnumConverter` as explained by [Eirik Tsarpalis](https://github.com/eiriktsarpalis)in this
[GitHub comment](https://github.com/dotnet/runtime/issues/74385#issuecomment-1705083109)) of serializing and deserializing
custom enum values using System.Text.Json.

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/jscarle/SystemTextJsonCustomEnumSerialization/main.yml?logo=github&label=tests)

