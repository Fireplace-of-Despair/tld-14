using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using TLD14.Composition;

namespace TLD14;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        await builder.ConfigureServices();

        builder.ConfigureLocalization();
        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(Locations.Keys))
            .UseCryptographicAlgorithms(
                new AuthenticatedEncryptorConfiguration 
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

        var app = builder.Build();
        await app.ConfigureStorage();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            //TODO: add error handling
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }


        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseLocalization();

        app.UseAuthorization();

        app.MapRazorPages();
        app.UseStatusCodePagesWithReExecute("/!Errors/{0}");
        app.Run();
    }
}
