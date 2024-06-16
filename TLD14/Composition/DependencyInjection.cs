using Microsoft.Extensions.Caching.Memory;
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
    public static Task<WebApplicationBuilder> ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IStorage, InMemoryStorage>();
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return Task.FromResult(builder);
    }

    public static async Task<WebApplication> ConfigureStorage(this WebApplication app)
    {
        var cache = app.Services.GetService<IMemoryCache>()!;
        var storage = new InMemoryStorage(cache);
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

        return app;
    }
}
