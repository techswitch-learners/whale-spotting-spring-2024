namespace WhaleSpotting.Models.Request;

public class SightingRequest
{
    public required decimal Latitude { get; set; }
    public required decimal Longitude { get; set; }
    public required string Token { get; set; }
    public required int SpeciesId { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required int BodyOfWaterId { get; set; }
    public required DateTime SightingTimestamp { get; set; }
}
