using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Enums;
using WhaleSpotting.Models.Data;
using WhaleSpotting.Models.Transfer;

namespace WhaleSpotting;

public class WhaleSpottingContext(DbContextOptions<WhaleSpottingContext> options)
    : IdentityDbContext<User, Role, int>(options)
{
    public DbSet<Achievement> Achievements { get; set; } = null!;
    public DbSet<BodyOfWater> BodiesOfWater { get; set; } = null!;
    public DbSet<Reaction> Reactions { get; set; } = null!;
    public DbSet<Sighting> Sightings { get; set; } = null!;
    public DbSet<Species> Species { get; set; } = null!;
    public DbSet<VerificationEvent> VerificationEvents { get; set; } = null!;
    public DbSet<HotSpot> HotSpots { get; set; } = null!;
    public DbSet<ViewingSuggestion> ViewingSuggestions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var userRole = new Role
        {
            Id = (int)RoleType.User,
            Name = RoleType.User.ToString(),
            NormalizedName = RoleType.User.ToString().ToUpper(),
        };
        var adminRole = new Role
        {
            Id = (int)RoleType.Admin,
            Name = RoleType.Admin.ToString(),
            NormalizedName = RoleType.Admin.ToString().ToUpper(),
        };
        builder.Entity<Role>().HasData(userRole, adminRole);

        using var speciesStreamReader = new StreamReader("Data/species.csv");
        using var speciesCsvReader = new CsvReader(speciesStreamReader, CultureInfo.InvariantCulture);
        var speciesList = speciesCsvReader.GetRecords<Species>().ToList();
        builder.Entity<Species>().HasData(speciesList);

        // Inject HotSpotRowTest from csv file

        using var streamReader = new StreamReader("Data/hotSpotRowTest.csv");
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        var hotSpotRows = csvReader.GetRecords<HotSpotRow>().ToList();

        // Select columns for HotSpot model
        var hotSpots = hotSpotRows
            .Select(hotSpotRow => new
            {
                hotSpotRow.TownOrHarbour,
                hotSpotRow.Latitude,
                hotSpotRow.Longitude,
                hotSpotRow.Country,
            })
            .Distinct()
            .ToList();

        for (var i = 0; i < hotSpots.Count; i++)
        {
            var hotSpot = new HotSpot
            {
                Id = -1 - i,
                Name = hotSpots[i].TownOrHarbour,
                Latitude = hotSpots[i].Latitude,
                Longitude = hotSpots[i].Longitude,
                Country = hotSpots[i].Country,
                ViewingSuggestions = [],
            };
            builder.Entity<HotSpot>().HasData(hotSpot);

            // Viewing suggestions at this spot
            var ViewingSuggestions = hotSpotRows.Where(hotSpotRow => hotSpot.Name == hotSpotRow.TownOrHarbour).ToList();
            foreach (var suggestion in ViewingSuggestions)
            {
                var viewingSuggestion = new ViewingSuggestion
                {
                    Id = suggestion.Id,
                    HotSpotId = hotSpot.Id,
                    SpeciesId = speciesList.First(spec => spec.Name == suggestion.Species).Id,
                    Platforms = suggestion.Platform,
                    PlatformBoxes = suggestion
                        .PlatformBoxes.Split(',')
                        .Where(platform => Enum.TryParse<Platform>(platform, out _))
                        .Select(platform => Enum.Parse<Platform>(platform))
                        .ToList(),
                    TimeOfYear = suggestion.TimeOfYear,
                    Months = suggestion
                        .Months.Split(',')
                        .Where(month => Enum.TryParse<Month>(month, out _))
                        .Select(month => Enum.Parse<Month>(month))
                        .ToList(),
                };
                builder.Entity<ViewingSuggestion>().HasData(viewingSuggestion);
            }
        }
    }
}
