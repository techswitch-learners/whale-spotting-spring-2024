namespace WhaleSpotting.Models.Response;

public class SpeciesResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ExampleImageUrl { get; set; }
}
