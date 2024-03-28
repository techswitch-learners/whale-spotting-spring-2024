using System.ComponentModel.DataAnnotations.Schema;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Data;

public class VerificationEvent
{
    public int Id { get; set; }

    public int SightingId { get; set; }

    [ForeignKey(nameof(SightingId))]
    public Sighting Sighting { get; set; } = null!;

    public int AdminId { get; set; }

    [ForeignKey(nameof(AdminId))]
    public User Admin { get; set; } = null!;

    public required ApprovalStatus ApprovalStatus { get; set; }

    public string? Comment { get; set; }

    public DateTime Timestamp { get; init; } = DateTime.Now;
}
