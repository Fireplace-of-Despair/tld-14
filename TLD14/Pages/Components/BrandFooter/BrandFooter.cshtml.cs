using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.BrandFooter;

[ViewComponent]
public sealed class BrandFooter(IStorage storage) : ViewComponent
{
    #region DataModel
    [MigrateKey("brand-footer")]
    public sealed class BrandFooterModel : BaseDataModel<BrandFooterModel.Translation>
    {
        public sealed class Translation
        {
            public required string Brand { get; set; }
        }
    }
    #endregion

    public BrandFooterModel? Model { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<BrandFooterModel, BrandFooterModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        return View(GetType().Name, Model);
    }
}