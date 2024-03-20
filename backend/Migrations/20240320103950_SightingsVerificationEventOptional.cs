using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhaleSpotting.Migrations
{
    /// <inheritdoc />
    public partial class SightingsVerificationEventOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sightings_VerificationEvents_VerificationEventId",
                table: "Sightings"
            );

            migrationBuilder.AlterColumn<int>(
                name: "VerificationEventId",
                table: "Sightings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Sightings_VerificationEvents_VerificationEventId",
                table: "Sightings",
                column: "VerificationEventId",
                principalTable: "VerificationEvents",
                principalColumn: "Id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sightings_VerificationEvents_VerificationEventId",
                table: "Sightings"
            );

            migrationBuilder.AlterColumn<int>(
                name: "VerificationEventId",
                table: "Sightings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true
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
    }
}
