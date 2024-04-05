using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhaleSpotting.Migrations
{
    /// <inheritdoc />
    public partial class AchievementsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Achievements_AspNetUsers_UserId", table: "Achievements");

            migrationBuilder.DropIndex(name: "IX_Achievements_UserId", table: "Achievements");

            migrationBuilder.DropColumn(name: "UserId", table: "Achievements");

            migrationBuilder.AddColumn<int>(
                name: "MinExperience",
                table: "Achievements",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "BadgeImageUrl", "Description", "MinExperience", "Name" },
                values: new object[,]
                {
                    {
                        1,
                        "plankton.png",
                        "As a Plankton, you're embarking on an exciting journey. Stay curious and alert – every discovery counts!",
                        0,
                        "Plankton"
                    },
                    {
                        2,
                        "calf.png",
                        "With a keen eye for the majestic giants of the sea, you, the Calf, have begun to make waves in the whale spotting world. Keep sailing forth!",
                        50,
                        "Calf"
                    },
                    {
                        3,
                        "pod.png",
                        "As a member of the Pod, your consistent sightings and growing expertise are making ripples. Dive deeper and share in the collective knowledge!",
                        180,
                        "Pod"
                    },
                    {
                        4,
                        "narwhal.png",
                        "With the wisdom of the Narwhal, you navigate the sea's secrets with ease. Your sightings are as rare and valuable as the tusked creatures themselves.",
                        500,
                        "Narwhal"
                    },
                    {
                        5,
                        "orca.png",
                        "As an Orca, your legendary sightings are as powerful as the ocean's apex predator. Lead the pack with your insights and inspire the community!",
                        1000,
                        "Orca"
                    }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Achievements", keyColumn: "Id", keyValue: 1);

            migrationBuilder.DeleteData(table: "Achievements", keyColumn: "Id", keyValue: 2);

            migrationBuilder.DeleteData(table: "Achievements", keyColumn: "Id", keyValue: 3);

            migrationBuilder.DeleteData(table: "Achievements", keyColumn: "Id", keyValue: 4);

            migrationBuilder.DeleteData(table: "Achievements", keyColumn: "Id", keyValue: 5);

            migrationBuilder.DropColumn(name: "MinExperience", table: "Achievements");

            migrationBuilder.AddColumn<int>(name: "UserId", table: "Achievements", type: "integer", nullable: true);

            migrationBuilder.CreateIndex(name: "IX_Achievements_UserId", table: "Achievements", column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_AspNetUsers_UserId",
                table: "Achievements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id"
            );
        }
    }
}
