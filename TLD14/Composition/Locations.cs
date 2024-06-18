namespace TLD14.Composition;

public static class Locations
{
    public static string ImportPath => 
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "!import");

    public static string Keys =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"temp");
}