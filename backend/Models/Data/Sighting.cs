using System.ComponentModel.DataAnnotations.Schema;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Data;

public class Sighting
{
    public int Id { get; set; }

    public required decimal Latitude { get; set; }

    public required decimal Longitude { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    public int SpeciesId { get; set; }

    [ForeignKey(nameof(SpeciesId))]
    public Species Species { get; set; } = null!;

    public required string Description { get; set; }

    public required string ImageUrl { get; set; }

    public int BodyOfWaterId { get; set; }

    [ForeignKey(nameof(BodyOfWaterId))]
    public BodyOfWater BodyOfWater { get; set; } = null!;

    public int? VerificationEventId { get; set; }

    [ForeignKey(nameof(VerificationEventId))]
    public VerificationEvent? VerificationEvent { get; set; }

    public required DateTime SightingTimestamp { get; set; }

    public DateTime CreationTimestamp { get; init; } = DateTime.Now;

    public List<Reaction> Reactions { get; set; } = [];
}
