using TLD14.Composition;

namespace TLD14;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        await builder.ConfigureServices();

        builder.ConfigureLocalization();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            //TODO: add error handling
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }


        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseLocalization();

        app.UseAuthorization();

        app.MapRazorPages();
        app.UseStatusCodePagesWithReExecute("/!Errors/{0}");
        app.Run();
    }
}
