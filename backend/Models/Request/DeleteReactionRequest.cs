using System.ComponentModel.DataAnnotations;
using WhaleSpotting.Attributes;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Request;

public class DeleteReactionRequest
{
    [Required(ErrorMessage = "Sighting ID is required.")]
    [SightingExists]
    public required int? SightingId { get; set; }
}
