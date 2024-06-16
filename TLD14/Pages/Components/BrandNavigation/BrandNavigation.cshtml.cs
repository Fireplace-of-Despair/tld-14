using Common.Attributes;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TLD14.Composition;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.BrandNavigation;

public sealed class BrandNavigation(IStorage storage, IOptions<RequestLocalizationOptions> localizationOptions) : ViewComponent
{
    #region DataModel

    [MigrateKey("brand-navigation")]
    public sealed class BrandNavigationModel : BaseDataModel<List<BrandNavigationModel.Translation>>
    {
        public sealed class Translation
        {
            public required string Name { get; set; }
            public required string Page { get; set; }
        }

        public CultureSwitcherModel? CultureSwitcherModel { get; set; }
    }

    #endregion

    public BrandNavigationModel? Model { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<BrandNavigationModel, List<BrandNavigationModel.Translation>>
        (
            HttpContext.GetRequestLanguage()
        );

        var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
        Model.CultureSwitcherModel = new CultureSwitcherModel
        {
            SupportedCultures = localizationOptions.Value.SupportedUICultures!.ToList(),
            CurrentUICulture = cultureFeature!.RequestCulture.UICulture
        };

        return View(GetType().Name, Model);
    }
}
