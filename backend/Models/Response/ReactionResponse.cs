namespace WhaleSpotting.Models.Response;

public class ReactionResponse
{
    public required string Type { get; set; }

    public required int UserId { get; set; }

    public required int SightingId { get; set; }

    public required DateTime Timestamp { get; set; }
}
