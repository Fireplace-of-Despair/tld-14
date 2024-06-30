using Microsoft.AspNetCore.Mvc.RazorPages;
using TLD14.Composition;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Utils;
using static TLD14.Pages.Components.PressPresence.PressPresence;

namespace TLD14.Pages;

public sealed class PressModel(IStorage storage) : PageModel
{
    public async Task OnGet()
    {
        var headerInfo = await storage.LoadAsyncEx<PressPresenceModel, PressPresenceModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );
        ViewData[Constants.OpenGraph.Description] = headerInfo.Properties!.Description.Minify();
    }
}
