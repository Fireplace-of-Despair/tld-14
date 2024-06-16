using Common.Attributes;
using Common.Exceptions;
using Common.Json;
using StackExchange.Redis;
using System.Text.Json;
using TLD14.Composition;
using TLD14.Models;

namespace TLD14.Infrastructure;

public sealed class StorageRedis(IConnectionMultiplexer multiplexer) : IStorage
{
    public async Task<T> LoadAsyncEx<T, TR>(string language) where T : BaseDataModel<TR>
    {
        if (!multiplexer.IsConnected) 
        {
            throw new IncidentException(ExceptionCode.Timeout);
        }

        var key = typeof(T).GetAttributeValue((MigrateKeyAttribute attribute) => attribute.Key);

        var db = multiplexer.GetDatabase();
        var result = await db.StringGetAsync(key);
        if (result.IsNull)
        {
            throw new IncidentException(ExceptionCode.NotFound);
        }

        var tmp = JsonSerializer.Deserialize<T>(result!)!;
        tmp.Properties = tmp.Translations.FirstOrDefault(x => x.Key == language).Value
            ?? tmp.Translations.First().Value;
        return tmp;
    }

    public async Task<bool> MigrateAsyncEx<T>()
    {
        var key = typeof(T).GetAttributeValue((MigrateKeyAttribute attribute) => attribute.Key);

        var json = File.ReadAllText(Path.Combine(Locations.ImportPath, $"{key}.json"));
        var item = JsonSerializer.Deserialize<T>(json, JsonOptions.Default)!;

        if (!multiplexer.IsConnected) 
        {
            throw new IncidentException(ExceptionCode.Timeout);
        }

        var db = multiplexer.GetDatabase();

        var jsonString = JsonSerializer.Serialize(item, JsonOptions.Default);

        return await db.StringSetAsync(key, jsonString);
    }
}
