using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.BrandHeader;

public sealed class BrandHeader(IStorage storage) : ViewComponent
{
    #region DataModel
    
    [MigrateKey("brand-header")]
    public sealed class BrandHeaderModel : BaseDataModel<BrandHeaderModel.Translation>
    {
        public sealed class Translation
        {
            public required string Name { get; set; }
            public required string Slogan { get; set; }
        }
    }

    #endregion

    public BrandHeaderModel? Model { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<BrandHeaderModel, BrandHeaderModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        return View(GetType().Name, Model);
    }
}