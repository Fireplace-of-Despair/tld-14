using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Globalization;

namespace TLD14.Composition;

//https://www.mikesdotnetting.com/article/348/razor-pages-localisation-seo-friendly-urls
public static class LocalizationInjection
{
    public static CultureInfo[] Cultures
    {
        get
        {
            return
            [
                new CultureInfo("en"),
                new CultureInfo("ja")
            ];
        }
    }

    public static WebApplicationBuilder ConfigureLocalization(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(Cultures[0].Name);
            options.SupportedCultures = Cultures;
            options.SupportedUICultures = Cultures;
            options.RequestCultureProviders.Insert(0, 
                new RouteDataRequestCultureProvider { Options = options }
                );
        });
        
        builder.Services.AddRazorPages(options => {
            options.Conventions.Add(new CultureTemplatePageRouteModelConvention());
        });

        return builder;
    }

    public static WebApplication UseLocalization(this WebApplication application)
    {
        var localizationOptions = application.Services.GetService<IOptions<RequestLocalizationOptions>>()!.Value;
        application.UseRequestLocalization(localizationOptions);

        var rewriter = new RewriteOptions();
        rewriter.Add(
            new RedirectUnsupportedCultures
            (
                application.Services.GetService<IOptions<RequestLocalizationOptions>>()!,
                application.Services.GetService<LinkGenerator>()!

                ));
        application.UseRewriter(rewriter);

        return application;
    }
}

public sealed class CultureSwitcherModel
{
    public CultureInfo? CurrentUICulture { get; set; }
    public List<CultureInfo> SupportedCultures { get; set; } = [];
}

public sealed class CultureTemplatePageRouteModelConvention : IPageRouteModelConvention
{
    public void Apply(PageRouteModel model)
    {
        var selectorCount = model.Selectors.Count;

        for (var i = 0; i < selectorCount; i++)
        {
            var selector = model.Selectors[i];

            model.Selectors.Add(new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel
                {
                    Order = -1,
                    Template = AttributeRouteModel.CombineTemplates("{culture?}", selector.AttributeRouteModel!.Template),
                }
            });
        }
    }
}

public class RedirectUnsupportedCultures : IRule
{
    private readonly IList<CultureInfo> _cultureItems;
    private readonly string _cultureRouteKey;
    private readonly LinkGenerator _linkGenerator;

    public RedirectUnsupportedCultures(IOptions<RequestLocalizationOptions> options,
        LinkGenerator linkGenerator)
    {
        RouteDataRequestCultureProvider provider = options.Value.RequestCultureProviders
            .OfType<RouteDataRequestCultureProvider>()
            .First();

        _cultureItems = options.Value.SupportedUICultures ?? [];

        _cultureRouteKey = provider.RouteDataStringKey;
        _linkGenerator = linkGenerator;
    }

    public void ApplyRule(RewriteContext context)
    {
        if (context == null) { throw new ArgumentException(null, nameof(context)); }
        if (context.HttpContext?.Request?.Path.Value == null)
        {
            return;
        }

        // do not redirect static assets and do not redirect from a controller that is meant to set the locale
        // similar to how you would not restrict a guest user from login form on public site.
        if (context.HttpContext.Request.Path.Value.EndsWith(".ico") ||
            context.HttpContext.Request.Path.Value.Contains("change-culture"))
        {
            return;
        }

        IRequestCultureFeature cultureFeature = context.HttpContext.Features.Get<IRequestCultureFeature>()!;
        string actualCulture = cultureFeature.RequestCulture.Culture.Name!;
        string requestedCulture = context.HttpContext.GetRouteValue(_cultureRouteKey)?.ToString() ?? string.Empty;

        // Here you can add more rules to redirect based on maybe cookie setting, or even language options saved in database user profile
        if (string.IsNullOrEmpty(requestedCulture) || _cultureItems.All(x => x.Name != requestedCulture)
            && !string.Equals(requestedCulture, actualCulture, StringComparison.OrdinalIgnoreCase))
        {
            context.HttpContext.GetRouteData().Values[_cultureRouteKey] = actualCulture;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = StatusCodes.Status301MovedPermanently;
            context.Result = RuleResult.EndResponse;

            // preserve query part parameters of the URL (?parameters) if there were any
            response.Headers[HeaderNames.Location] =
                _linkGenerator.GetPathByAction(
                    context.HttpContext,
                    values: context.HttpContext.GetRouteData().Values
                )
              + context.HttpContext.Request.QueryString;
        }
    }
}