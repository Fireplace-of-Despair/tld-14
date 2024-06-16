using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.IndexIntroduction;

public sealed class IndexIntroduction(IStorage storage) : ViewComponent
{
    #region DataModel

    [MigrateKey("introduction")]
    public sealed class IndexIntroductionModel : BaseDataModel<IndexIntroductionModel.Translation>
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

    public IndexIntroductionModel? Model { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<IndexIntroductionModel, IndexIntroductionModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        return View(GetType().Name, Model);
    }
}
