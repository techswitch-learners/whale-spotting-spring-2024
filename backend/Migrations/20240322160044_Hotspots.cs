using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhaleSpotting.Migrations
{
    /// <inheritdoc />
    public partial class Hotspots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotSpots",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotSpots", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "ViewingSuggestions",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    HotSpotId = table.Column<int>(type: "integer", nullable: false),
                    SpeciesId = table.Column<int>(type: "integer", nullable: false),
                    Platforms = table.Column<string>(type: "text", nullable: false),
                    PlatformBoxes = table.Column<int[]>(type: "integer[]", nullable: false),
                    TimeOfYear = table.Column<string>(type: "text", nullable: false),
                    Months = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewingSuggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewingSuggestions_HotSpots_HotSpotId",
                        column: x => x.HotSpotId,
                        principalTable: "HotSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_ViewingSuggestions_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.InsertData(
                table: "HotSpots",
                columns: new[] { "Id", "Country", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { -3, "Costa Rica", 8.583333m, -83.266667m, "Dulce Gulf" },
                    { -2, "Canada", 48.428333m, -123.364722m, "Victoria" },
                    { -1, "Argentina", -42.566667m, -64.283333m, "Puerto Pirámides" }
                }
            );

            migrationBuilder.InsertData(
                table: "ViewingSuggestions",
                columns: new[] { "Id", "HotSpotId", "Months", "PlatformBoxes", "Platforms", "SpeciesId", "TimeOfYear" },
                values: new object[,]
                {
                    {
                        -4,
                        -3,
                        new[] { 11, 0, 1, 2, 6, 7, 8, 9, 10 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        9,
                        "December-March/July-mid-November"
                    },
                    {
                        -3,
                        -3,
                        new[] { 11, 0, 1, 2, 6, 7, 8, 9, 10 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        -2,
                        -2,
                        new[] { 3, 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        9,
                        "April – October"
                    },
                    {
                        -1,
                        -1,
                        new[] { 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 2 },
                        "Motorized boat and land-based",
                        13,
                        "June – December"
                    }
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_ViewingSuggestions_HotSpotId",
                table: "ViewingSuggestions",
                column: "HotSpotId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ViewingSuggestions_SpeciesId",
                table: "ViewingSuggestions",
                column: "SpeciesId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ViewingSuggestions");

            migrationBuilder.DropTable(name: "HotSpots");
        }
    }
}
