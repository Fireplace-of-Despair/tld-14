using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.IndexNavigation;

public sealed class IndexNavigation(IStorage storage) : ViewComponent
{
    [MigrateKey("index-navigation")]
    public sealed class IndexNavigationModel : BaseDataModel<List<IndexNavigationModel.Translation>>
    {
        public sealed class Translation
        {
            public required string Name { get; set; }
            public required string Anchor { get; set; }
        }
    }

    public IndexNavigationModel? Model { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<IndexNavigationModel, List<IndexNavigationModel.Translation>>
        (
            HttpContext.GetRequestLanguage()
        );

        return View(GetType().Name, Model);
    }
}
