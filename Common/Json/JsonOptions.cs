using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Common.Json;
public static class JsonOptions
{
    private static readonly JsonSerializerOptions _option = new()
    {
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),        
    };

    public static JsonSerializerOptions Default
    {
        get
        {
            return _option;
        }
    }
}
