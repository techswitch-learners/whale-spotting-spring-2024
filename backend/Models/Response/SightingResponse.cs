using WhaleSpotting.Models.Data;

namespace WhaleSpotting.Models.Response;

public class SightingResponse
{
    public required int Id { get; set; }

    public required decimal Latitude { get; set; }

    public required decimal Longitude { get; set; }

    public required string UserName { get; set; }

    public required Species Species { get; set; }

    public required string Description { get; set; }

    public required string ImageUrl { get; set; }

    public required string BodyOfWaterName { get; set; }

    public VerificationEvent? VerificationEvent { get; set; }

    public required DateTime SightingTimestamp { get; set; }

    public required DateTime CreationTimestamp { get; init; }

    public required List<Reaction> Reactions { get; set; }
}
