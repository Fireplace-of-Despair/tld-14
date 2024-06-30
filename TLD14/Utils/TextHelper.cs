using System.Text.RegularExpressions;

namespace TLD14.Utils;

public static partial class TextHelper
{
    [GeneratedRegex(@"<[^>]*>")]
    private static partial Regex HtmlTags();

    public static string Minify(this string text)
    {
        var result = HtmlTags().Replace(text, " ");

        if (result.Length <= 280)
        {
            return result;
        }

        return string.Concat(result.AsSpan(0, 280), "...");
    }
}
