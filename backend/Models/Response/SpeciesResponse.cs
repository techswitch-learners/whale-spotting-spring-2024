namespace WhaleSpotting.Models.Response;

public class SpeciesResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string ExampleImageUrl { get; set; }
    public required string WikiLink { get; set; }
}
