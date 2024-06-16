using StackExchange.Redis;
using TLD14.Infrastructure;
using TLD14.Pages.Components.BrandFooter;
using TLD14.Pages.Components.BrandHeader;
using TLD14.Pages.Components.BrandNavigation;
using TLD14.Pages.Components.Contact;
using TLD14.Pages.Components.IndexIntroduction;
using TLD14.Pages.Components.IndexLore;
using TLD14.Pages.Components.IndexNavigation;
using TLD14.Pages.Components.IndexProjects;
using TLD14.Pages.Components.MediaPresence;
using TLD14.Pages.Components.PressPresence;

namespace TLD14.Composition;

public static class DependencyInjection
{
    private const string Redis = "Redis";

    public static async Task<WebApplicationBuilder> ConfigureServices(this WebApplicationBuilder builder)
    {
        var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString(Redis)!);
        
        builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options));
        builder.Services.AddSingleton<IStorage, StorageRedis>();

        //------------------
        var storage = new StorageRedis(ConnectionMultiplexer.Connect(options));
        await storage.MigrateAsyncEx<BrandHeader.BrandHeaderModel>();
        await storage.MigrateAsyncEx<BrandFooter.BrandFooterModel>();
        await storage.MigrateAsyncEx<BrandNavigation.BrandNavigationModel>();
        
        await storage.MigrateAsyncEx<IndexNavigation.IndexNavigationModel>();
        await storage.MigrateAsyncEx<IndexIntroduction.IndexIntroductionModel>();
        await storage.MigrateAsyncEx<IndexLore.IndexLoreModel>();
        await storage.MigrateAsyncEx<IndexProjects.IndexProjectsModel>();

        await storage.MigrateAsyncEx<MediaPresence.MediaPresenceModel>();
        await storage.MigrateAsyncEx<Contact.ContactModel>();
        await storage.MigrateAsyncEx<PressPresence.PressPresenceModel>();
        //------------------

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return builder;
    }
}
