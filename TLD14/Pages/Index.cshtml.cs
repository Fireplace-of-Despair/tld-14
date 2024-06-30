using Common.Attributes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TLD14.Composition;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;
using TLD14.Pages.Components.IndexIntroduction;
using TLD14.Utils;
using static TLD14.Pages.Components.IndexIntroduction.IndexIntroduction;

namespace TLD14.Pages;

public sealed class IndexModel(IStorage storage) : PageModel
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

    public async Task OnGet()
    {
        Model = await storage.LoadAsyncEx<BrandHeaderModel, BrandHeaderModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        var headerInfo = await storage.LoadAsyncEx<IndexIntroductionModel, IndexIntroductionModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );
        ViewData[Constants.OpenGraph.Description] = headerInfo.Properties!.Content.Minify();
        ViewData[Constants.OpenGraph.Image] = 
            $"https://{HttpContext.Request.Host}" + 
            headerInfo.Properties!.Image;
    }
}
