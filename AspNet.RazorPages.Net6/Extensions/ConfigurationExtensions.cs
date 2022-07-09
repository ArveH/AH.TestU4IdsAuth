namespace AspNet.RazorPages.Net6.Extensions;

public static class ConfigurationExtensions
{
    public static string SafeGet(this ConfigurationManager config, string key)
    {
        try
        {
            return config[key] ?? "";
        }
        catch
        {
            return "";
        }
    }
}