using Common.Attributes;
using System.Text.Json.Serialization;
using TLD14.Infrastructure;

namespace TLD14.Models;

public abstract class BaseDataModel<T>
{
    [JsonIgnore]
    public T? Properties { get; set; } = default;

    public Dictionary<string, T> Translations { get; set; } = [];

    public virtual string Key
    {
        get
        {
            var key = GetType()
                .GetAttributeValue((MigrateKeyAttribute attribute) => attribute.Key);
            return key;
        }
    }
}