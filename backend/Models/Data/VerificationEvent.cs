using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpotting.Models.Data;

public class VerificationEvent
{
    public required int Id { get; set; }

    public int SightingId { get; set; }

    [ForeignKey(nameof(SightingId))]
    public Sighting Sighting { get; set; } = null!;

    public int AdminId { get; set; }

    [ForeignKey(nameof(AdminId))]
    public User Admin { get; set; } = null!;

    public string? Comment { get; set; }

    public DateTime Timestamp { get; init; } = DateTime.Now;
}
