using Microsoft.AspNetCore.Mvc;
using TLD14.Models;

namespace TLD14.Pages.Components.ProjectCard;

public sealed class ProjectCard : ViewComponent
{
    public IViewComponentResult Invoke(Project project)
    {
        return View(GetType().Name, project);
    }
}
