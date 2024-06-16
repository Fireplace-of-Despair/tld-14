using TLD14.Models;

namespace TLD14.Infrastructure;

public interface IStorage
{
    Task<T> LoadAsyncEx<T, TR>(string language) where T : BaseDataModel<TR>;
    Task<bool> MigrateAsyncEx<T>();
}
