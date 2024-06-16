namespace TLD14.Composition;

public static class Locations
{
    public static string ImportPath => 
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "!import");
}