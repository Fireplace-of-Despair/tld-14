using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using TLD14.Composition;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.Archive;

public sealed class Archive(IStorage storage) : ViewComponent
{
    #region DataModel 
    [MigrateKey("projects")]
    public sealed class ArchiveModel : BaseDataModel<ArchiveModel.Translation>
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
    #endregion

    public ArchiveModel? Model { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<ArchiveModel, ArchiveModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );
        Model.Properties!.Projects =
            Model.Properties.Projects
            .Where(x => x.DivisionKey.Equals(
                Constants.Divisions.AshenChronicles, StringComparison.CurrentCultureIgnoreCase))
            .ToList();

        return View(GetType().Name, Model);
    }
}
