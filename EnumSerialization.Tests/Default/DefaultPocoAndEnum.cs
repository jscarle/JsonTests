namespace EnumSerialization.Tests.Default;

public sealed record DefaultPoco
{
    public required DefaultEnum DefaultPropertyA { get; init; }
    public required DefaultEnum DefaultPropertyB { get; init; }
}

public enum DefaultEnum
{
    DefaultValueA = 0,
    DefaultValueB = 1,
}
