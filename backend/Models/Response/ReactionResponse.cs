using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Response;

public class ReactionResponse
{
    // public required string Type { get; set; }

    // public required int UserId { get; set; }

    public required int SightingId { get; set; }
    public required Dictionary<string, int> Reactions { get; set; }
    public string? CurrentUserReaction { get; set; }

    // public required DateTime Timestamp { get; set; }
}
