namespace WhaleSpotting.Models.Data;

public class Achievement
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string BadgeImageUrl { get; set; }
    public required int MinExperience { get; set; }
}
