using System.ComponentModel.DataAnnotations;
using WhaleSpotting.Attributes;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Request;

public class AddReactionRequest
{
    [Required(ErrorMessage = "Reaction type is required.")]
    public required string Type { get; set; }

    [Required(ErrorMessage = "Sighting ID is required.")]
    [SightingExists]
    public required int? SightingId { get; set; }
}
