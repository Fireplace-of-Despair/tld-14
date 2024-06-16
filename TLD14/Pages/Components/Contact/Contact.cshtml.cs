using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using TLD14.Infrastructure;
using TLD14.Localizations;
using TLD14.Models;

namespace TLD14.Pages.Components.Contact;

public sealed class Contact(IStorage storage) : ViewComponent
{
    #region DataModel
    
    [MigrateKey("contact")]
    public sealed class ContactModel : BaseDataModel<ContactModel.Translation>
    {
        public sealed class Translation
        {
            public required string Title { get; set; }
            public required string Description { get; set; }
            public Dictionary<string, string> Contacts { get; set; } = [];
        }
    }

    #endregion

    public ContactModel? Model { get; set; }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Model = await storage.LoadAsyncEx<ContactModel, ContactModel.Translation>
        (
            HttpContext.GetRequestLanguage()
        );

        return View(GetType().Name, Model);
    }
}