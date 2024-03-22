using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Enums;
using WhaleSpotting.Models.Data;

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
    }
}
