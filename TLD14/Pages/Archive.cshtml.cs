using Microsoft.AspNetCore.Mvc.RazorPages;
using TLD14.Composition;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Utils;

namespace TLD14.Pages;

public sealed class ArchiveModel(IStorage storage) : PageModel
{
    public async Task OnGet()
    {
        var headerInfo = await storage.LoadAsyncEx<Components.Archive.Archive.ArchiveModel,
            Components.Archive.Archive.ArchiveModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        ViewData[Constants.OpenGraph.Description] = headerInfo.Properties!.ArchiveDescription.Minify();
    }
}
