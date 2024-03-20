using WhaleSpotting.Models.Data;

namespace WhaleSpotting.Models.Response;

public class SightingResponse
{
    public int Id { get; set; }

    public required decimal Latitude { get; set; }

    public required decimal Longitude { get; set; }

    public required string UserName { get; set; }

    public Species Species { get; set; } = null!;

    public required string Description { get; set; }

    public required string ImageUrl { get; set; }

    public string BodyOfWaterName { get; set; } = null!;

    public VerificationEvent? VerificationEvent { get; set; }

    public required DateTime SightingTimestamp { get; set; }

    public DateTime CreationTimestamp { get; init; } = DateTime.Now;

    public List<Reaction> Reactions { get; set; } = [];
}
