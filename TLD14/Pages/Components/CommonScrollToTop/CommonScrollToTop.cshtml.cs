using Microsoft.AspNetCore.Mvc;

namespace TLD14.Pages.Components.CommonScrollToTop;

[ViewComponent]
public sealed class CommonScrollToTop() : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View(GetType().Name);
    }
}