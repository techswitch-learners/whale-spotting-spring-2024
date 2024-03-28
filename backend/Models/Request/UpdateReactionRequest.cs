using System.ComponentModel.DataAnnotations;
using WhaleSpotting.Attributes;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Request;

public class UpdateReactionRequest
{
    public required string Type { get; set; }

    [Required(ErrorMessage = "Sighting is required.")]
    [SightingExists]
    public required int SightingId { get; set; }
}
