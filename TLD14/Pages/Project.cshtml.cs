using Common.Attributes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages;
public sealed class ProjectModel(IStorage storage) : PageModel
{
    [MigrateKey("projects")]
    public sealed class IndexProjectsModel : BaseDataModel<IndexProjectsModel.Translation>
    {
        public sealed class Translation
        {
            public required string Name { get; set; }
            public required string Description { get; set; }
            public required string Archive { get; set; }
            public required string ArchiveDescription { get; set; }

            public required List<Project> Projects { get; set; }
        }
    }

    public Project? Project {  get; set; }

    public async Task OnGet(string code)
    {
        var model = await storage.LoadAsyncEx<IndexProjectsModel, IndexProjectsModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        Project = model.Properties!.Projects.First(x => x.Key == code);
    }
}
