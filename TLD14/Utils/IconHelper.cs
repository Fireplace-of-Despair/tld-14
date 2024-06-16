using TLD14.Composition;

namespace TLD14.Utils;

public static class IconHelper
{
    public static string GetLanguage(string key)
    {
        var defaultLanguage = LocalizationInjection.Cultures[0].Name;

        var language = key.Split("_").LastOrDefault();
        if (string.IsNullOrEmpty(language))
        { 
            return defaultLanguage.ToUpper();
        }

        return language.ToUpper();
    }

    public static string GetIcon(string key)
    {
        var value = key.Split("_").FirstOrDefault();

        return value switch
        {
            "pirate" => "/icons/pirate.svg",

            "amazon" => "/icons/amazon.svg",
            "github" => "/icons/github.svg",
            "itch" => "/icons/itch.svg",            
            "pixiv" => "/icons/pixiv.svg",
            "royalroad" => "/icons/royalroad.svg",
            "steam" => "/icons/steam.svg",
            "telegram" => "/icons/telegram.svg",
            "youtube" => "/icons/youtube.svg",
            "mail" => "/icons/email.svg",
            "linkedin" => "/icons/linkedin.svg",
            _ => "/icons/default.svg",
        };
    }
}
