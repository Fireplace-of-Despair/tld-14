namespace Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MigrateKeyAttribute(string key) : Attribute
{
    public string Key { get; set; } = key;
}