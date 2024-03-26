using System.ComponentModel.DataAnnotations.Schema;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Response;

public class ReactionResponse
{
    public ReactionType Type { get; set; }

    public required String Name { get; set; }
    public required int Count { get; set; }
}
