using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhaleSpotting.Migrations
{
    /// <inheritdoc />
    public partial class SpeciesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Description", table: "Species", newName: "WikiLink");

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "ExampleImageUrl", "Name", "WikiLink" },
                values: new object[,]
                {
                    {
                        1,
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/6/62/Berardius_bairdii.jpg/1599px-Berardius_bairdii.jpg?20151221184110",
                        "Beaked whale",
                        "https://en.wikipedia.org/wiki/Beaked_whale"
                    },
                    {
                        2,
                        "https://upload.wikimedia.org/wikipedia/commons/e/e8/Oceanogr%C3%A0fic_29102004.jpg",
                        "Beluga whale",
                        "https://en.wikipedia.org/wiki/Beluga_whale"
                    },
                    {
                        3,
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1c/Anim1754_-_Flickr_-_NOAA_Photo_Library.jpg/2560px-Anim1754_-_Flickr_-_NOAA_Photo_Library.jpg",
                        "Blue whale",
                        "https://en.wikipedia.org/wiki/Blue_whale"
                    },
                    {
                        4,
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Bowhead_Whale_NOAA.jpg/2560px-Bowhead_Whale_NOAA.jpg",
                        "Bowhead whale",
                        "https://en.wikipedia.org/wiki/Bowhead_whale"
                    },
                    {
                        5,
                        "https://upload.wikimedia.org/wikipedia/commons/3/35/Balaenoptera_brydei.jpg",
                        "Bryde's whale",
                        "https://en.wikipedia.org/wiki/Bryde%27s_whale"
                    },
                    {
                        6,
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/c/ce/Finhval_%281%29.jpg/2560px-Finhval_%281%29.jpg",
                        "Fin whale",
                        "https://en.wikipedia.org/wiki/Fin_whale"
                    },
                    {
                        7,
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/0/00/Ballena_gris_adulta_con_su_ballenato.jpg/2560px-Ballena_gris_adulta_con_su_ballenato.jpg",
                        "Gray whale",
                        "https://en.wikipedia.org/wiki/Gray_whale"
                    },
                    {
                        8,
                        "https://upload.wikimedia.org/wikipedia/commons/6/61/Humpback_Whale_underwater_shot.jpg",
                        "Humpback whale",
                        "https://en.wikipedia.org/wiki/Humpback_whale"
                    },
                    {
                        9,
                        "https://upload.wikimedia.org/wikipedia/commons/3/37/Killerwhales_jumping.jpg",
                        "Killer whale",
                        "https://en.wikipedia.org/wiki/Orca"
                    },
                    {
                        10,
                        "https://upload.wikimedia.org/wikipedia/commons/d/d9/Minke_Whale_%28NOAA%29.jpg",
                        "Minke whale",
                        "https://en.wikipedia.org/wiki/Minke_whale"
                    },
                    {
                        11,
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/%D0%9D%D0%B0%D1%80%D0%B2%D0%B0%D0%BB_%D0%B2_%D1%80%D0%BE%D1%81%D1%81%D0%B8%D0%B9%D1%81%D0%BA%D0%BE%D0%B9_%D0%90%D1%80%D0%BA%D1%82%D0%B8%D0%BA%D0%B5.jpg/1280px-%D0%9D%D0%B0%D1%80%D0%B2%D0%B0%D0%BB_%D0%B2_%D1%80%D0%BE%D1%81%D1%81%D0%B8%D0%B9%D1%81%D0%BA%D0%BE%D0%B9_%D0%90%D1%80%D0%BA%D1%82%D0%B8%D0%BA%D0%B5.jpg",
                        "Narwhal",
                        "https://en.wikipedia.org/wiki/Narwhal"
                    },
                    {
                        12,
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e2/Pilot_whale.jpg/1920px-Pilot_whale.jpg",
                        "Pilot whale",
                        "https://en.wikipedia.org/wiki/Pilot_whale"
                    },
                    {
                        13,
                        "https://upload.wikimedia.org/wikipedia/commons/e/e2/Southern_right_whale.jpg",
                        "Right whale",
                        "https://en.wikipedia.org/wiki/Right_whale"
                    },
                    {
                        14,
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e3/Sei_whale_mother_and_calf_Christin_Khan_NOAA.jpg/1280px-Sei_whale_mother_and_calf_Christin_Khan_NOAA.jpg",
                        "Sei whale",
                        "https://en.wikipedia.org/wiki/Sei_whale"
                    },
                    {
                        15,
                        "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b1/Mother_and_baby_sperm_whale.jpg/1920px-Mother_and_baby_sperm_whale.jpg",
                        "Sperm whale",
                        "https://en.wikipedia.org/wiki/Sperm_whale"
                    },
                    {
                        16,
                        "https://cdn1.vectorstock.com/i/thumb-large/32/75/cartoon-curious-whale-and-speech-bubble-sticker-vector-26423275.jpg",
                        "Other/unknown",
                        "https://en.wikipedia.org/wiki/Whale"
                    }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 1);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 2);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 3);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 4);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 5);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 6);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 7);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 8);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 9);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 10);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 11);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 12);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 13);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 14);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 15);

            migrationBuilder.DeleteData(table: "Species", keyColumn: "Id", keyValue: 16);

            migrationBuilder.RenameColumn(name: "WikiLink", table: "Species", newName: "Description");
        }
    }
}
