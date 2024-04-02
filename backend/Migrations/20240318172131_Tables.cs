using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WhaleSpotting.Migrations
{
    /// <inheritdoc />
    public partial class Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<decimal>(
                name: "FavoriteLocationLatitude",
                table: "AspNetUsers",
                type: "numeric",
                nullable: true
            );

            migrationBuilder.AddColumn<decimal>(
                name: "FavoriteLocationLongitude",
                table: "AspNetUsers",
                type: "numeric",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    BadgeImageUrl = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id"
                    );
                }
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

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ExampleImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SightingId = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Sightings",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SpeciesId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    BodyOfWaterId = table.Column<int>(type: "integer", nullable: false),
                    VerificationEventId = table.Column<int>(type: "integer", nullable: false),
                    SightingTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sightings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sightings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Sightings_BodiesOfWater_BodyOfWaterId",
                        column: x => x.BodyOfWaterId,
                        principalTable: "BodiesOfWater",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Sightings_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "VerificationEvents",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    SightingId = table.Column<int>(type: "integer", nullable: false),
                    AdminId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationEvents_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_VerificationEvents_Sightings_SightingId",
                        column: x => x.SightingId,
                        principalTable: "Sightings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(name: "IX_Achievements_UserId", table: "Achievements", column: "UserId");

            migrationBuilder.CreateIndex(name: "IX_Reactions_SightingId", table: "Reactions", column: "SightingId");

            migrationBuilder.CreateIndex(name: "IX_Reactions_UserId", table: "Reactions", column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sightings_BodyOfWaterId",
                table: "Sightings",
                column: "BodyOfWaterId"
            );

            migrationBuilder.CreateIndex(name: "IX_Sightings_SpeciesId", table: "Sightings", column: "SpeciesId");

            migrationBuilder.CreateIndex(name: "IX_Sightings_UserId", table: "Sightings", column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sightings_VerificationEventId",
                table: "Sightings",
                column: "VerificationEventId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_VerificationEvents_AdminId",
                table: "VerificationEvents",
                column: "AdminId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_VerificationEvents_SightingId",
                table: "VerificationEvents",
                column: "SightingId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Sightings_SightingId",
                table: "Reactions",
                column: "SightingId",
                principalTable: "Sightings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Sightings_VerificationEvents_VerificationEventId",
                table: "Sightings",
                column: "VerificationEventId",
                principalTable: "VerificationEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationEvents_Sightings_SightingId",
                table: "VerificationEvents"
            );

            migrationBuilder.DropTable(name: "Achievements");

            migrationBuilder.DropTable(name: "Reactions");

            migrationBuilder.DropTable(name: "Sightings");

            migrationBuilder.DropTable(name: "BodiesOfWater");

            migrationBuilder.DropTable(name: "Species");

            migrationBuilder.DropTable(name: "VerificationEvents");

            migrationBuilder.DropColumn(name: "Experience", table: "AspNetUsers");

            migrationBuilder.DropColumn(name: "FavoriteLocationLatitude", table: "AspNetUsers");

            migrationBuilder.DropColumn(name: "FavoriteLocationLongitude", table: "AspNetUsers");

            migrationBuilder.DropColumn(name: "ProfileImageUrl", table: "AspNetUsers");
        }
    }
}
