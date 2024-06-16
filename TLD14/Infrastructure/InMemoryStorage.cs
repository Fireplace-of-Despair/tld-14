using Common.Attributes;
using Common.Exceptions;
using Common.Json;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using TLD14.Composition;
using TLD14.Models;

namespace TLD14.Infrastructure;

public class InMemoryStorage(IMemoryCache memoryCache) : IStorage
{
    public Task<T> LoadAsyncEx<T, TR>(string language) where T : BaseDataModel<TR>
    {
        var key = typeof(T).GetAttributeValue((MigrateKeyAttribute attribute) => attribute.Key);

        var result = memoryCache.Get<string>(key);
        var data = JsonSerializer.Deserialize<T>(result!)!;

        data.Properties = data.Translations.FirstOrDefault(x => x.Key == language).Value
            ?? data.Translations.First().Value;

        return Task.FromResult(data);
    }    
    
    public Task<bool> MigrateAsyncEx<T>()
    {
        var key = typeof(T).GetAttributeValue((MigrateKeyAttribute attribute) => attribute.Key);

        var json = File.ReadAllText(Path.Combine(Locations.ImportPath, $"{key}.json"));
        var item = JsonSerializer.Deserialize<T>(json, JsonOptions.Default)!;

        var jsonString = JsonSerializer.Serialize(item, JsonOptions.Default);

        memoryCache.Set(key, jsonString);

        return Task.FromResult(true);
    }
}
