using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WhaleSpotting.Migrations
{
    /// <inheritdoc />
    public partial class BodiesOfWater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Sightings_BodiesOfWater_BodyOfWaterId", table: "Sightings");

            migrationBuilder.DropTable(name: "BodiesOfWater");

            migrationBuilder.DropIndex(name: "IX_Sightings_BodyOfWaterId", table: "Sightings");

            migrationBuilder.DropColumn(name: "BodyOfWaterId", table: "Sightings");

            migrationBuilder.AddColumn<string>(
                name: "BodyOfWater",
                table: "Sightings",
                type: "text",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "BodyOfWater", table: "Sightings");

            migrationBuilder.AddColumn<int>(
                name: "BodyOfWaterId",
                table: "Sightings",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateTable(
                name: "BodiesOfWater",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodiesOfWater", x => x.Id);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Sightings_BodyOfWaterId",
                table: "Sightings",
                column: "BodyOfWaterId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Sightings_BodiesOfWater_BodyOfWaterId",
                table: "Sightings",
                column: "BodyOfWaterId",
                principalTable: "BodiesOfWater",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
