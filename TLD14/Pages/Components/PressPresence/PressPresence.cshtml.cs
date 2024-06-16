using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.PressPresence;

public sealed class PressPresence(IStorage storage) : ViewComponent
{
    [MigrateKey("press-presence")]
    public sealed class PressPresenceModel : BaseDataModel<PressPresenceModel.Translation>
    {
        public sealed class Translation
        {
            public required string Title { get; set; }
            public required string Description { get; set; }
            public required List<Article> Articles { get; set; } = [];
        }

        public sealed class Article
        {
            public required string Title { get; set; }
            public required string Description { get; set; }
            public required string Image { get; set; }
            public required DateTime Date { get; set; }
            public required string Url { get; set; }
        }
    }

    public PressPresenceModel Model { get; set; } = new PressPresenceModel();

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<PressPresenceModel, PressPresenceModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        return View(GetType().Name, Model);
    }
}
