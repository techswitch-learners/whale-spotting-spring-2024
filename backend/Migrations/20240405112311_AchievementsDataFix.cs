using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhaleSpotting.Migrations
{
    /// <inheritdoc />
    public partial class AchievementsDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Narwhale"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Narwahle"
            );
        }
    }
}
