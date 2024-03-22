namespace WhaleSpotting.Models.Data;

public class Species
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string ExampleImageUrl { get; set; }
    public required string WikiLink { get; set; }
}
