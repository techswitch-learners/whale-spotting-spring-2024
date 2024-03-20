using System.ComponentModel.DataAnnotations;
using WhaleSpotting.Attributes;

namespace WhaleSpotting.Models.Request;

public class SightingRequest
{
    [Range(-90, 90)]
    public required decimal Latitude { get; set; }

    [Range(-180, 180)]
    public required decimal Longitude { get; set; }
    public required string Token { get; set; }
    public required int SpeciesId { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required int BodyOfWaterId { get; set; }

    [DateTimeNotInFuture(ErrorMessage = "The date must not be later than today.")]
    public required DateTime SightingTimestamp { get; set; }
}
