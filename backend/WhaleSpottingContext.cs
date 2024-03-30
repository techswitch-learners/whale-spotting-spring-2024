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
    public DbSet<Hotspot> Hotspots { get; set; } = null!;
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

        using var streamReader = new StreamReader("Data/Hotspots.csv");
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        var HotspotRows = csvReader.GetRecords<HotspotRow>().ToList();

        var distinctHotspots = HotspotRows
            .DistinctBy(Hotspot =>
                (Hotspot.Country, Hotspot.Region, Hotspot.TownOrHarbour, Hotspot.Latitude, Hotspot.Longitude)
            )
            .ToList();

        for (var i = 0; i < distinctHotspots.Count; i++)
        {
            var Hotspot = new Hotspot
            {
                Id = i + 1,
                Name = GetHotspotName(distinctHotspots[i]),
                Latitude = distinctHotspots[i].Latitude,
                Longitude = distinctHotspots[i].Longitude,
                Country = distinctHotspots[i].Country,
                ViewingSuggestions = [],
            };
            builder.Entity<Hotspot>().HasData(Hotspot);

            var viewingSuggestions = HotspotRows
                .Where(HotspotRow => Hotspot.Name == GetHotspotName(HotspotRow))
                .ToList();
            foreach (var suggestion in viewingSuggestions)
            {
                var viewingSuggestion = new ViewingSuggestion
                {
                    Id = suggestion.Id,
                    HotspotId = Hotspot.Id,
                    SpeciesId = speciesList.First(spec => spec.Name == suggestion.Species).Id,
                    Platforms = suggestion.Platform,
                    PlatformBoxes = suggestion
                        .PlatformBoxes.Split(
                            ',',
                            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
                        )
                        .Where(platform => Enum.TryParse<Platform>(platform, out _))
                        .Select(Enum.Parse<Platform>)
                        .ToList(),
                    TimeOfYear = suggestion.TimeOfYear,
                    Months = suggestion
                        .Months.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Where(month => Enum.TryParse<Month>(month, out _))
                        .Select(Enum.Parse<Month>)
                        .ToList(),
                };
                builder.Entity<ViewingSuggestion>().HasData(viewingSuggestion);
            }
        }
    }

    private static string GetHotspotName(HotspotRow HotspotRow)
    {
        return !string.IsNullOrEmpty(HotspotRow.Region) && !string.IsNullOrEmpty(HotspotRow.TownOrHarbour)
            ? $"{HotspotRow.TownOrHarbour}, {HotspotRow.Region}"
            : !string.IsNullOrEmpty(HotspotRow.Region)
                ? HotspotRow.Region
                : HotspotRow.TownOrHarbour;
    }
}
