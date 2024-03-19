using System.ComponentModel.DataAnnotations.Schema;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Data;

public class Reaction
{
    public int Id { get; set; }

    public ReactionType Type { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    public int SightingId { get; set; }

    [ForeignKey(nameof(SightingId))]
    public Sighting Sighting { get; set; } = null!;

    public required DateTime Timestamp { get; set; }
}
