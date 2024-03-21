using System.ComponentModel.DataAnnotations;
using WhaleSpotting.Attributes;

namespace WhaleSpotting.Models.Request;

public class AddSightingRequest
{
    [Required(ErrorMessage = "Latitude is required.")]
    [Range(-90, 90)]
    public required decimal? Latitude { get; set; }

    [Required(ErrorMessage = "Longitude is required.")]
    [Range(-180, 180)]
    public required decimal? Longitude { get; set; }

    [Required(ErrorMessage = "Token is required.")]
    public required string Token { get; set; }

    [Required(ErrorMessage = "Species is required.")]
    [SpeciesExists]
    public required int? SpeciesId { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Image is required.")]
    public required string ImageUrl { get; set; }

    [Required(ErrorMessage = "Body of water is required.")]
    [BodyOfWaterExists]
    public required int? BodyOfWaterId { get; set; }

    [Required(ErrorMessage = "Timestamp is required.")]
    [DateTimeNotInFuture(ErrorMessage = "The timestamp must not be in the future.")]
    public required DateTime? SightingTimestamp { get; set; }
}
