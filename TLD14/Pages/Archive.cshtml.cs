using Common.Attributes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages;

public sealed class ArchiveModel(IStorage storage) : PageModel
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
    }
}
