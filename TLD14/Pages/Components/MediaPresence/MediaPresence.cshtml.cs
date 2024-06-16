using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.MediaPresence;

public sealed class MediaPresence(IStorage storage) : ViewComponent
{
    #region DataModel

    [MigrateKey("media-presence")]
    public sealed class MediaPresenceModel : BaseDataModel<MediaPresenceModel.Translation>
    {
        public sealed class Translation
        {
            public required string Title { get; set; }
            public required string Description { get; set; }
            public required List<Link> Links { get; set; } = [];
        }

        public sealed class Link
        {
            public required string Code { get; set; }
            public required string Url { get; set; }
            public required string UrlAlt { get; set; }
        }
    }

    #endregion

    public MediaPresenceModel? Model { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<MediaPresenceModel, MediaPresenceModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        return View(GetType().Name, Model);
    }
}
