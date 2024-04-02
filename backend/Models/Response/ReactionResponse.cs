using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Response;

public class ReactionResponse
{
    public required int SightingId { get; set; }
    public required Dictionary<string, int> Reactions { get; set; }
    public string? CurrentUserReaction { get; set; }
}
