using System.ComponentModel.DataAnnotations.Schema;
using WhaleSpotting.Enums;

namespace WhaleSpotting.Models.Data;

public class ViewingSuggestion
{
    public int Id { get; set; }

    public int HotspotId { get; set; }

    [ForeignKey(nameof(HotspotId))]
    public Hotspot Hotspot { get; set; } = null!;

    public int SpeciesId { get; set; }

    [ForeignKey(nameof(SpeciesId))]
    public Species Species { get; set; } = null!;

    public required string Platforms { get; set; }
    public required List<Platform> PlatformBoxes { get; set; }

    public required string TimeOfYear { get; set; }

    public required List<Month> Months { get; set; }
}
