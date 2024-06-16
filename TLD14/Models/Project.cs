namespace TLD14.Models;

public sealed class Project
{
    public required string Key { get; set; }
    
    public required string Name { get; set; }
    public required string DivisionKey { get; set; }
    public required string DivisionFull { get; set; }

    public required string Image { get; set; }

    public required string DescriptionShort { get; set; }
    public required string DescriptionFull { get; set; }

    public required string LinksAlt { get; set; }
    public required Dictionary<string, string> Links { get; set; }

    public required DateTime? ReleaseDate { get; set; }
}
