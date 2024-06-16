using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.IndexLore;

public sealed class IndexLore(IStorage storage) : ViewComponent
{
    #region DataModel

    [MigrateKey("lore")]
    public sealed class IndexLoreModel : BaseDataModel<IndexLoreModel.Translation>
    {
        public sealed class Translation
        {
            public required string Name { get; set; }
            public required string Content { get; set; }
            public required string Image { get; set; }
            public required string ImageAlt { get; set; }
        }
    }

    #endregion

    public IndexLoreModel? Model { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<IndexLoreModel, IndexLoreModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        return View(GetType().Name, Model);
    }
}
