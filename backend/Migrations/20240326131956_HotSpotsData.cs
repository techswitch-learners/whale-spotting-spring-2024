using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhaleSpotting.Migrations
{
    /// <inheritdoc />
    public partial class HotSpotsData : Migration
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
                    { 1, "Argentina", -42.5701796m, -64.2787391m, "Puerto Pirámides, Chubut/ Península Valdés" },
                    {
                        2,
                        "Argentina",
                        -42.6559866m,
                        -64.9863695m,
                        "Playa El Doradillo and Puerto Madryn, Chubut/ Península Valdés"
                    },
                    { 3, "Argentina", -40.7333763m, -64.9535634m, "San Antonio Oeste, Río Negro/ Bahía San Antonio" },
                    { 4, "Argentina", -54.8019121m, -68.3029511m, "Ushuaia, Tierra del Fuego/Beagle cannel." },
                    { 5, "Argentina", -42.5701796m, -64.2787391m, "Puerto Pirámides, Chubut/Península Valdés" },
                    { 6, "Argentina", -42.7636217m, -65.0348311m, "Puerto Madryn, Chubut/Península Valdés" },
                    { 7, "Argentina", -43.2493016m, -65.3076351m, "Trelew, Chubut/Península Valdés" },
                    { 8, "Australia", -28.6334699m, 153.6382501m, "Cape Byron" },
                    { 9, "Australia", -30.2962013m, 153.1138924m, "Coffs Harbour" },
                    { 10, "Australia", -35.0480805m, 150.7446771m, "Jervis Bay" },
                    { 11, "Australia", -37.0843679m, 149.9276991m, "Twofold Bay" },
                    { 12, "Australia", -25.2881539m, 152.7676633m, "Hervey Bay" },
                    { 13, "Australia", -27.0946084m, 152.9205918m, "Moreton Bay" },
                    { 14, "Australia", -18.2870668m, 147.6991918m, "The Great Barrier Reef" },
                    {
                        15,
                        "Australia",
                        -31.4936111m,
                        131.1586111m,
                        "Head of the Bight (Great Australian Bight Marine Park areas)"
                    },
                    { 16, "Australia", -35.5502959m, 138.620909m, "Encounter Coast (Victor Harbour)" },
                    { 17, "Australia", -38.3420842m, 141.6012039m, "Portland" },
                    { 18, "Australia", -16.4m, 122.9333333m, "Waters off the NW coast north of Cape Leveque" },
                    { 19, "Australia", -32.2038096m, 128.4849923m, "Great Australian Bight" },
                    { 20, "Australia", -31.9513993m, 115.8616783m, "Perth waters" },
                    { 21, "Australia", -25.274398m, 133.775136m, "Geographe Bay Area" },
                    { 22, "Australia", -38.4033652m, 142.5108558m, "Logan’s Beach" },
                    { 23, "Australia", -42.19m, 148.15m, "Great Oyster Bay" },
                    { 24, "Australia", -43.3566056m, 147.325247m, "Adventure Bay" },
                    { 25, "Brazil", -12.5745995m, -38.0049816m, "Praia do Forte" },
                    { 26, "Brazil", -12.9777334m, -38.501648m, "Salvador" },
                    { 27, "Brazil", -13.3800917m, -38.9124904m, "Morro de São Paulo" },
                    { 28, "Brazil", -13.8939046m, -38.9504994m, "Barra Grande" },
                    { 29, "Brazil", -14.2801238m, -38.9946356m, "Itacaré" },
                    { 30, "Brazil", -16.4443537m, -39.0653656m, "Porto Seguro" },
                    { 31, "Brazil", -16.4910066m, -39.0740031m, "Arraial d’Ajuda" },
                    { 32, "Brazil", -14.235004m, -51.92528m, "Prado" },
                    { 33, "Brazil", -17.1067377m, -39.1813492m, "Cumuruxatiba" },
                    { 34, "Brazil", -17.7336523m, -39.265271m, "Caravelas" },
                    { 35, "Brazil", -20.3196644m, -40.3384748m, "Vitória" },
                    { 36, "Brazil", -28.028072m, -48.6216255m, "Garopaba" },
                    { 37, "Brazil", -28.2408045m, -48.6686568m, "Imbituba" },
                    { 38, "Brazil", -28.6042345m, -48.8140499m, "Farol de Santa Marta/Laguna" },
                    { 39, "Canada", 49.6506376m, -125.4493906m, "Vancouver Island, British Columbia" },
                    { 40, "Canada", 48.4284207m, -123.3656444m, "Victoria, British Columbia" },
                    { 41, "Canada", 43.5933961m, -79.5382607m, "Long Beach, British Columbia" },
                    { 42, "Canada", 50.5492459m, -126.832251m, "Telegraph Cove, British Columbia" },
                    { 43, "Canada", 50.0331226m, -125.2733354m, "Campbell River, British Columbia" },
                    { 44, "Canada", 49.1529842m, -125.9066184m, "Tofino, British Columbia" },
                    {
                        45,
                        "Canada",
                        48.1459776m,
                        -69.7128395m,
                        "St. Lawrence River Estuary,Saguenay Fjord (near Tadoussac), Mingan region, Québec"
                    },
                    { 46, "Canada", 56.130366m, -106.346771m, "Bay of Fundy, Nova Scotia and New Brunswick" },
                    { 47, "Canada", 44.6475811m, -63.5727683m, "Halifax, Nova Scotia and New Brunswick" },
                    { 48, "Canada", 46.2486851m, -60.851817m, "Cape Breton, Nova Scotia and New Brunswick" },
                    {
                        49,
                        "Canada",
                        47.5556097m,
                        -52.7452511m,
                        "St. John’s, Avalon Peninsula, Newfoundland and Labrador"
                    },
                    { 50, "Canada", 58.7679415m, -94.1695807m, "Manitoba (including Churchill), Canadian Arctic" },
                    { 51, "Canada", 72.7001169m, -77.9585316m, "Nanavut (Pond Inlet), Canadian Arctic" },
                    { 52, "Canada", 65.4215054m, -70.9654211m, "Baffin Island, Canadian Arctic" },
                    { 53, "Chile", -29.2680556m, -71.5416667m, "Isla Choros" },
                    { 54, "Chile", -29.2344444m, -71.5263889m, "Isla Damas" },
                    { 55, "Chile", -29.0333333m, -71.5777778m, "Isla Chañaral" },
                    { 56, "Chile", -41.9291711m, -74.0360814m, "Puñihuil ( Chiloé)/ Melinka," },
                    { 57, "Chile", -43.366667m, -73.353611m, "Gulf of Corcovado" },
                    {
                        58,
                        "Chile",
                        -41.8101472m,
                        -68.9062689m,
                        "Moraleda Channel Refugio Island (Patagonian channels)"
                    },
                    { 59, "Chile", -43.8983m, -73.745m, "Melinka" },
                    { 60, "Chile", -53.1633845m, -70.9078263m, "Strait of Magellan/ Punta Arenas" },
                    { 61, "Chile", -18.4782534m, -70.3125988m, "Arica" },
                    { 62, "Chile", -42.6239686m, -73.9265732m, "Chiloé/Patagonian channels" },
                    { 63, "Colombia", 5.82m, -77.42m, "Nuquí/ Gulf of Tribugá, Pacific coast, Caribbean Coast" },
                    { 64, "Colombia", 3.97m, -77.32m, "Bahía de Málaga, Pacific coast, Caribbean Coast" },
                    { 65, "Colombia", 2.9683333m, -78.1844444m, "Gorgona Island, Pacific coast, Caribbean Coast" },
                    { 66, "Costa Rica", 9.1674405m, -83.7365518m, "Bahía Ballena, Southern Pacific" },
                    { 67, "Costa Rica", 9.1635009m, -83.7358514m, "Uvita, Southern Pacific" },
                    { 68, "Costa Rica", 8.8689482m, -83.4710234m, "Sierpe, Southern Pacific" },
                    { 69, "Costa Rica", 8.711884m, -83.66144m, "Drake Bay, Southern Pacific" },
                    { 70, "Costa Rica", 9.1674405m, -83.7365518m, "Bahía Ballena, Pacific coastlines" },
                    { 71, "Costa Rica", 8.711884m, -83.66144m, "Drake Bay, Pacific coastlines" },
                    { 72, "Costa Rica", 9.9887553m, -84.2199753m, "El Coco, Pacific coastlines" },
                    { 73, "Costa Rica", 8.6040618m, -83.1133792m, "Golfito, Pacific coastlines" },
                    { 74, "Costa Rica", 9.8209521m, -83.6986568m, "Port Jiménez, Pacific coastlines" },
                    { 75, "Costa Rica", 9.728666m, -84.8150781m, "Gulf of Nicoya, Pacific coastlines" },
                    { 76, "Costa Rica", 9.4318681m, -84.1619076m, "Quepos, Pacific coastlines" },
                    { 77, "Costa Rica", 8.8689482m, -83.4710234m, "Sámara and Sierpe, Pacific coastlines" },
                    { 78, "Costa Rica", 8.4691779m, -83.2077645m, "Dulce Gulf, Pacific coastlines" },
                    { 79, "Costa Rica", 8.8689482m, -83.4710234m, "Sierpe, Pacific coastlines" },
                    { 80, "Costa Rica", 9.9778439m, -84.8294211m, "Puntarenas, Pacific coastlines" },
                    { 81, "Costa Rica", 10.0365029m, -84.2436849m, "Tambor, Pacific coastlines" },
                    { 82, "Costa Rica", 10.9432945m, -85.6858616m, "Cuajiniquil, Pacific coastlines" },
                    { 83, "Costa Rica", 10.7267764m, -85.80766m, "Gulf of Papagayo, Pacific coastlines" },
                    { 84, "Costa Rica", 8.707268m, -83.8817571m, "Caño Island, Pacific coastlines" },
                    { 85, "Costa Rica", 9.5832283m, -82.6105228m, "Gandoca, Caribbean coastline" },
                    { 86, "Costa Rica", 9.629838m, -82.6577963m, "Manzanillo, Caribbean coastline" },
                    { 87, "Denmark", 56.26392m, 9.501785m, "Nuuk, Nuup Kangerlua" },
                    { 88, "Denmark", 65.5680901m, -37.1873002m, "Kulusuk, Tasiilaq area" },
                    { 89, "Denmark", 65.6134561m, -37.6335696m, "Tasiilaq, Tasiilaq area" },
                    { 90, "Denmark", 56.26392m, 9.501785m, "Disko Bay and southwards, West Greenland" },
                    { 91, "Denmark", 65.6134561m, -37.6335696m, "Tasiilaq, East Greenland" },
                    { 92, "Denmark", 69.2437661m, -53.5413188m, "Disko Bay" },
                    { 93, "Denmark", 56.26392m, 9.501785m, "South Greenland" },
                    { 94, "Denmark", 65.6134561m, -37.6335696m, "Tasiilaq area" },
                    { 95, "Denmark", 69.2437661m, -53.5413188m, "Qeqertarsuaq, Disko Bay" },
                    { 96, "Denmark", 69.2198118m, -51.0986032m, "Ilulissat, Disko Bay" },
                    { 97, "Dominica", 15.3091676m, -61.3793554m, "Roseau" },
                    { 98, "Dominica", 15.5561791m, -61.45814m, "Portsmouth" },
                    { 99, "Dominican Republic", 19.1435577m, -69.1447081m, "Samaná Bay, North/Northeast" },
                    { 100, "Dominican Republic", 18.735693m, -70.162651m, "Silver bank, North/Northeast" },
                    { 101, "Dominican Republic", 18.735693m, -70.162651m, "Navidad bank, North/Northeast" },
                    { 102, "Ecuador", -0.9676533m, -80.7089101m, "Manta, Manabí Province" },
                    { 103, "Ecuador", 0.0731181m, -80.0513928m, "Pedernales, Manabí Province" },
                    { 104, "Ecuador", -1.5539322m, -80.8045568m, "Puerto López, Manabí Province" },
                    {
                        105,
                        "Ecuador",
                        -1.4886678m,
                        -80.7738641m,
                        "Isla de la Plata (Parque Nacional Machalilla), Manabí Province"
                    },
                    { 106, "Ecuador", -1.5961038m, -80.8668369m, "Isla Salango, Manabí Province" },
                    { 107, "Ecuador", 0.6105615m, -80.0190796m, "Muisne, Esmeraldas Province" },
                    { 108, "Ecuador", 0.8680071m, -79.8464804m, "Atacames, Esmeraldas Province" },
                    { 109, "Ecuador", -1.831239m, -78.183406m, "Súa (Reserva Marino Galeras), Esmeraldas Province" },
                    {
                        110,
                        "Ecuador",
                        -2.1887133m,
                        -81.0110023m,
                        "Santa Elena (Reserva Marina Puntilla de Santa Elena), Santa Elena Province"
                    },
                    { 111, "Ecuador", -1.9804241m, -80.7477708m, "Ayangue, Santa Elena Province" },
                    { 112, "Ecuador", -3.2334344m, -79.9696277m, "Jambelí, El Oro Province" },
                    {
                        113,
                        "Ecuador",
                        -3.1716525m,
                        -80.4361291m,
                        "Isla Santa Clara (Refugio de Vida Silvestre Isla Santa Clara)., Guayas Province"
                    },
                    { 114, "Ecuador", -0.6393592m, -90.3371889m, "Isla Santa Cruz, Galapagos" },
                    { 115, "Ecuador", -0.8674715m, -89.436391m, "Isla San Cristobal, Galapagos" },
                    { 116, "Ecuador", -1.3083314m, -90.4313729m, "Isla Floreana, Galapagos" },
                    { 117, "Ecuador", -0.8292374m, -91.135302m, "Isla Isabela, Galapagos" },
                    { 118, "France", 46.227638m, 2.213749m, "Atlantic Coast" },
                    { 119, "France", 44.0144936m, 6.2116438m, "Provence coast, Mediterranean Coast" },
                    {
                        120,
                        "France",
                        -20.904305m,
                        165.618042m,
                        "South lagoon of New Caledonia and Tahiti, South Pacific (French Polynesia, New-Caledonia)"
                    },
                    {
                        121,
                        "France",
                        -17.5388435m,
                        -149.8295234m,
                        "Moorea, South Pacific (French Polynesia, New-Caledonia)"
                    },
                    {
                        122,
                        "France",
                        -16.5004126m,
                        -151.7414904m,
                        "Bora Bora, South Pacific (French Polynesia, New-Caledonia)"
                    },
                    {
                        123,
                        "France",
                        -22.480133m,
                        -151.3385191m,
                        "Rurutu French Polynesia, South Pacific (French Polynesia, New-Caledonia)"
                    },
                    { 124, "France", 46.227638m, 2.213749m, "Carribean (Agoa Sanctuary)" },
                    { 125, "France", 43.677079m, 4.433703m, "Saint Gilles (west coast),, Indian Ocean - Réunion" },
                    { 126, "France", 42.867945m, 1.372902m, "Le Port, Indian Ocean - Réunion" },
                    { 127, "France", 49.019157m, 2.247531m, "Saint-Leu, Indian Ocean - Réunion" },
                    { 128, "France", 48.383707m, 7.47253m, "Saint-Pierre, Indian Ocean - Réunion" },
                    { 129, "France", -12.78064m, 45.2326964m, "Mamoudzou, Indian Ocean - Mayotte" },
                    { 130, "France", -12.7819879m, 45.2564226m, "Dzaoudzi, Indian Ocean - Mayotte" },
                    { 131, "France", 46.227638m, 2.213749m, "plage N’Gouja, Indian Ocean - Mayotte" },
                    { 132, "France", 46.227638m, 2.213749m, "plage Gouela, Indian Ocean - Mayotte" },
                    { 133, "Gabon", 0.4077972m, 9.4402833m, "Librevillle, The entire coastline" },
                    { 134, "Gabon", -0.7149503m, 8.7843278m, "Port Gentil,, The entire coastline" },
                    { 135, "Gabon", -2.1538342m, 9.5896043m, "Loango National Park, The entire coastline" },
                    { 136, "Gabon", -3.8194511m, 11.0202729m, "Mayumba National Park, The entire coastline" },
                    { 137, "Ireland", 51.4843274m, -9.3661093m, "Baltimore, South and Southeast" },
                    { 138, "Ireland", 52.1527666m, -6.994804m, "Dunmore East, South and Southeast" },
                    { 139, "Ireland", 51.7058853m, -8.5222327m, "Kinsale, South and Southeast" },
                    { 140, "Ireland", 52.1757194m, -6.5863919m, "Kilmore Quay, South and Southeast" },
                    { 141, "Ireland", 51.558803m, -9.1434791m, "Unionhall, South and Southeast" },
                    { 142, "Ireland", 51.6514954m, -9.9103302m, "Castletownbere, South and Southeast" },
                    { 143, "Ireland", 52.1408534m, -10.2671142m, "Dingle, South and Southeast" },
                    { 144, "Ireland", 52.1333113m, -10.3617104m, "Ventry, South and Southeast" },
                    { 145, "Ireland", 51.4843274m, -9.3661093m, "Baltimore, Southwest, South, Southeast" },
                    { 146, "Ireland", 51.6514954m, -9.9103302m, "Castletownbere, Southwest, South, Southeast" },
                    { 147, "Ireland", 52.1408534m, -10.2671142m, "Dingle, Southwest, South, Southeast" },
                    { 148, "Ireland", 52.1527666m, -6.994804m, "Dunmore East, Southwest, South, Southeast" },
                    { 149, "Ireland", 52.1757194m, -6.5863919m, "Kilmore Quay, Southwest, South, Southeast" },
                    { 150, "Ireland", 51.7058853m, -8.5222327m, "Kinsale, Southwest, South, Southeast" },
                    { 151, "Ireland", 51.558803m, -9.1434791m, "Unionhall, Southwest, South, Southeast" },
                    { 152, "Ireland", 52.1333113m, -10.3617104m, "Ventry, Southwest, South, Southeast" },
                    { 153, "Italy", 43.7806979m, 7.6722799m, "Bordighera, Pelagos Sanctuary, Sardinia" },
                    { 154, "Italy", 43.8159671m, 7.7760567m, "Sanremo, Pelagos Sanctuary, Sardinia" },
                    { 155, "Italy", 43.8897316m, 8.0393482m, "Imperia, Pelagos Sanctuary, Sardinia" },
                    { 156, "Italy", 43.9848226m, 8.1305992m, "Andora, Pelagos Sanctuary, Sardinia" },
                    { 157, "Italy", 43.9783918m, 8.1579376m, "Laigueglia, Pelagos Sanctuary, Sardinia" },
                    { 158, "Italy", 44.1261565m, 8.2558714m, "Loano, Pelagos Sanctuary, Sardinia" },
                    { 159, "Italy", 44.014336m, 8.1811741m, "Alassio, Pelagos Sanctuary, Sardinia" },
                    { 160, "Italy", 44.2975603m, 8.4645m, "Savona, Pelagos Sanctuary, Sardinia" },
                    { 161, "Italy", 44.3890436m, 8.5611142m, "Varazze, Pelagos Sanctuary, Sardinia" },
                    { 162, "Italy", 44.4058612m, 8.6860167m, "Arenzano, Pelagos Sanctuary, Sardinia" },
                    { 163, "Italy", 44.4071448m, 8.9347381m, "Genova, Pelagos Sanctuary, Sardinia" },
                    { 164, "Italy", 44.3837051m, 9.0391431m, "Nervi, Pelagos Sanctuary, Sardinia" },
                    { 165, "Italy", 44.3614219m, 9.1437445m, "Recco, Pelagos Sanctuary, Sardinia" },
                    { 166, "Italy", 44.3542792m, 9.1498178m, "Camogli, Pelagos Sanctuary, Sardinia" },
                    { 167, "Italy", 41.1357338m, 9.4967926m, "Poltu Quatu, Pelagos Sanctuary, Sardinia" },
                    {
                        168,
                        "Italy",
                        43.7806979m,
                        7.6722799m,
                        "Bordighera, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        169,
                        "Italy",
                        43.8159671m,
                        7.7760567m,
                        "Sanremo, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        170,
                        "Italy",
                        43.8897316m,
                        8.0393482m,
                        "Imperia, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        171,
                        "Italy",
                        43.9848226m,
                        8.1305992m,
                        "Andora, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        172,
                        "Italy",
                        43.9783918m,
                        8.1579376m,
                        "Laigueglia, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        173,
                        "Italy",
                        44.1261565m,
                        8.2558714m,
                        "Loano, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        174,
                        "Italy",
                        44.014336m,
                        8.1811741m,
                        "Alassio, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        175,
                        "Italy",
                        44.2975603m,
                        8.4645m,
                        "Savona, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        176,
                        "Italy",
                        44.3890436m,
                        8.5611142m,
                        "Varazze, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        177,
                        "Italy",
                        44.4058612m,
                        8.6860167m,
                        "Arenzano, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        178,
                        "Italy",
                        44.4071448m,
                        8.9347381m,
                        "Genova, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        179,
                        "Italy",
                        44.3837051m,
                        9.0391431m,
                        "Nervi, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        180,
                        "Italy",
                        44.3614219m,
                        9.1437445m,
                        "Recco, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        181,
                        "Italy",
                        44.3542792m,
                        9.1498178m,
                        "Camogli, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        182,
                        "Italy",
                        41.1357338m,
                        9.4967926m,
                        "Poltu Quatu, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        183,
                        "Italy",
                        40.7271553m,
                        13.9434035m,
                        "Ischia, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        184,
                        "Italy",
                        40.8517983m,
                        14.26812m,
                        "Naples, Pelagos Sanctuary, Sardinia, Campania Archipelago, Pontino Archipelago"
                    },
                    {
                        185,
                        "Kenya",
                        -3.3425465m,
                        40.0274159m,
                        "Watamu, Malindi Watamu National Marine Park and Reserve (MWMPA)"
                    },
                    {
                        186,
                        "Kenya",
                        -0.6462266m,
                        38.4060986m,
                        "Ngomeni (just north of park, Malindi Watamu National Marine Park and Reserve (MWMPA)"
                    },
                    { 187, "Maldives", 3.202778m, 73.22068m, "Maldives" },
                    { 188, "Mexico", 31.8667427m, -116.5963713m, "Ensenada, Baja California" },
                    {
                        189,
                        "Mexico",
                        27.9672737m,
                        -114.0210061m,
                        "Guerrero Negro/ Laguna Ojo de Liebre, Baja California Sur"
                    },
                    { 190, "Mexico", 26.753045m, -113.2473278m, "San Ignacio/Laguna San Ignacio, Baja California Sur" },
                    { 191, "Mexico", 25.0189888m, -111.6532253m, "Puerto Adolfo Mateo, Baja California Sur" },
                    {
                        192,
                        "Mexico",
                        27.9617875m,
                        -111.0370989m,
                        "San Carlos/Bahía Magdalena Puerto Chale/Bahía Almejas, Baja California Sur"
                    },
                    { 193, "Mexico", 26.0117564m, -111.3477531m, "Loreto, Baja California Sur" },
                    { 194, "Mexico", 23.0636562m, -109.7024376m, "San José del Cabo, Baja California Sur" },
                    { 195, "Mexico", 22.8948129m, -109.9152149m, "Cabo San Lucas, Baja California Sur" },
                    { 196, "Mexico", 23.4363627m, -109.4296296m, "Cabo Pulmo, Baja California Sur" },
                    { 197, "Mexico", 23.2494148m, -106.4111425m, "Mazatlán, Sinaloa" },
                    { 198, "Mexico", 21.0261111m, -105.265m, "San Blas,Compostela (Rincón de Guayabitos), Nayarit" },
                    { 199, "Mexico", 20.8690862m, -105.4410109m, "Sayulita, Nayarit" },
                    { 200, "Mexico", 20.7811243m, -105.5288272m, "Punta de Mita,, Nayarit" },
                    { 201, "Mexico", 20.7544076m, -105.3760604m, "La Cruz de Huanacaxtle, Nayarit" },
                    { 202, "Mexico", 20.6986205m, -105.2964898m, "Nuevo Vallarta, Nayarit" },
                    { 203, "Mexico", 20.6870668m, -105.2284329m, "Puerto Vallarta, Bahía de Tenacatita, Jalisco" },
                    { 204, "Mexico", 19.0536292m, -104.3170724m, "Manzanillo, Colima" },
                    { 205, "Mexico", 17.5390397m, -101.2701934m, "Petatlan, Guerrero" },
                    { 206, "Mexico", 15.6677291m, -96.5545185m, "Puerto Ángel-Mazunte, Oaxaca" },
                    { 207, "New Zealand", -42.3994483m, 173.6799111m, "Kaikoura, South Island" },
                    { 208, "New Zealand", -36.8508827m, 174.7644881m, "Auckland, Hauraki Gulf" },
                    { 209, "New Zealand", -45.8795455m, 170.5005957m, "Dunedin, South Island" },
                    {
                        210,
                        "New Zealand",
                        -42.3994483m,
                        173.6799111m,
                        "Kaikoura, North Island and northern South Island"
                    },
                    {
                        211,
                        "New Zealand",
                        -41.0815077m,
                        174.3331694m,
                        "Marlborough Sounds, North Island and northern South Island"
                    },
                    {
                        212,
                        "New Zealand",
                        -37.6869653m,
                        176.1654272m,
                        "Tauranga, North Island and northern South Island"
                    },
                    {
                        213,
                        "New Zealand",
                        -36.8508827m,
                        174.7644881m,
                        "Auckland, North Island and northern South Island"
                    },
                    {
                        214,
                        "New Zealand",
                        -35.2167252m,
                        174.1540659m,
                        "Bay of Islands, North Island and northern South Island"
                    },
                    { 215, "Norway", 69.3160799m, 16.1202284m, "Andenes, Nordland/Vesterålen" },
                    { 216, "Norway", 69.01893m, 15.1234474m, "Stø, Nordland/Vesterålen" },
                    { 217, "Norway", 69.3160799m, 16.1202284m, "Andenes, Nordland, Troms, Finnmark" },
                    { 218, "Norway", 69.01893m, 15.1234474m, "Stø, Nordland, Troms, Finnmark" },
                    { 219, "Norway", 70.0339919m, 20.9737453m, "Skjervøy, Nordland, Troms, Finnmark" },
                    { 220, "Norway", 69.6492047m, 18.9553238m, "Tromsø, Nordland, Troms, Finnmark" },
                    { 221, "Norway", 60.472024m, 8.468946m, "Nordland, Troms, Finnmark" },
                    {
                        222,
                        "Norway",
                        69.3160799m,
                        16.1202284m,
                        "Andenes, Nordland/Vesterålen,Troms, Finnmark, Svalbard"
                    },
                    { 223, "Norway", 69.01893m, 15.1234474m, "Stø, Nordland/Vesterålen,Troms, Finnmark, Svalbard" },
                    {
                        224,
                        "Norway",
                        70.0339919m,
                        20.9737453m,
                        "Skjervøy, Nordland/Vesterålen,Troms, Finnmark, Svalbard"
                    },
                    {
                        225,
                        "Norway",
                        69.6492047m,
                        18.9553238m,
                        "Tromsø, Nordland/Vesterålen,Troms, Finnmark, Svalbard"
                    },
                    { 226, "Norway", 60.472024m, 8.468946m, "Nordland/Vesterålen,Troms, Finnmark, Svalbard" },
                    { 227, "Norway", 78.2231722m, 15.626723m, "Longyearbyen, Svalbard" },
                    { 228, "Panama", 8.0733624m, -82.3560389m, "Boca Chica Golfo de Chiriqui, Pacific Coast" },
                    { 229, "Panama", 8.9823792m, -79.5198696m, "Panama City, Pacific Coast" },
                    { 230, "Panama", 7.6321187m, -79.9989895m, "Isla Iguana (Tono and Pedasi), Pacific Coast" },
                    { 231, "Panama", 8.431881238m, -78.95123703m, "Las Perlas Archipelago, Pacific Coast" },
                    { 232, "Panama", 8.537981m, -80.782127m, " Bahia Piña, Pacific Coast" },
                    { 233, "Panama", 8.7946029m, -79.5554268m, "Taboga, Pacific Coast" },
                    { 234, "Panama", 7.4693076m, -81.7568253m, "Coiba, Pacific Coast" },
                    { 235, "Peru", -4.1764859m, -81.1237446m, "Los Organos, Piura, Tumbes" },
                    { 236, "Peru", -4.1034782m, -81.0451037m, "Máncora, Piura, Tumbes" },
                    { 237, "Peru", -4.2165081m, -81.1699102m, "El Ñuro, Piura, Tumbes" },
                    { 238, "Peru", -4.2506038m, -81.2332956m, "Cabo Blanco, Piura, Tumbes" },
                    { 239, "Peru", -4.1764859m, -81.1237446m, "Los Organos, Piura" },
                    { 240, "Portugal", 37.7412488m, -25.6755944m, "All islands, Azores" },
                    { 241, "Portugal", 32.7607074m, -16.9594723m, "All islands, Madeira" },
                    { 242, "Portugal", 39.399872m, -8.224454m, "Entire coast, Mainland Portugal" },
                    { 243, "Portugal", 39.399872m, -8.224454m, "Algarve region, Mainland Portugal" },
                    { 244, "South Africa", -34.4063429m, 19.2686949m, "Hermanus, Western Cape Province" },
                    { 245, "South Africa", -34.5805396m, 19.3517529m, "Gansbaai, Western Cape Province" },
                    { 246, "South Africa", -34.6163984m, 19.3501046m, "Kleinbaai, Western Cape Province" },
                    { 247, "South Africa", -34.0350789m, 23.0464579m, "Knysna, Western Cape Province" },
                    { 248, "South Africa", -33.9608369m, 25.6022423m, "Port Elizabeth, Eastern Cape" },
                    { 249, "South Africa", -34.1934595m, 18.435835m, "Simonstown, Western Cape Province" },
                    { 250, "South Africa", -34.0620481m, 23.3713855m, "Plettenberg Bay, Western Cape Province" },
                    { 251, "South Africa", -30.559482m, 22.937506m, "Algoa Bay, Eastern Cape Province" },
                    { 252, "South Africa", -28.377531m, 32.4107259m, "St Lucia, Kwa-Zulu Natal Province" },
                    { 253, "South Africa", -34.1934595m, 18.435835m, "Simonstown, Western Cape" },
                    { 254, "South Africa", -34.4063429m, 19.2686949m, "Hermanus, Western Cape" },
                    { 255, "South Africa", -34.5805396m, 19.3517529m, "Gansbaai, Western Cape" },
                    { 256, "South Africa", -34.6163984m, 19.3501046m, "Kleinbaai, Western Cape" },
                    { 257, "South Africa", -34.0350789m, 23.0464579m, "Knysna, Western Cape" },
                    { 258, "South Africa", -34.1836263m, 22.1243871m, "Mosselbay, Western Cape" },
                    { 259, "South Africa", -34.0620481m, 23.3713855m, "Plettenberg Bay, Western Cape" },
                    { 260, "Spain", 28.2915637m, -16.6291304m, "Canary Isands" },
                    { 261, "Spain", 40.463667m, -3.74922m, "Mediterranean" },
                    { 262, "Sri Lanka", 5.948262m, 80.4715866m, "Mirissa in the south-west" },
                    { 263, "Sri Lanka", 8.5873638m, 81.2152121m, "Trincomalee in the north-east" },
                    { 264, "Sultanate of Oman", 17.5041962m, 56.0361469m, "Hallaniyat Islands, Dhofar" },
                    { 265, "Tonga", -18.622756m, -173.9902982m, "Vava’u" },
                    { 266, "Tonga", -21.1465968m, -175.2515482m, "Tongatapu" },
                    { 267, "Tonga", -21.178986m, -175.198242m, "‘Eua and Ha’apai." },
                    { 268, "United Kingdom", 54.9456129m, -1.9479664m, "North East England" },
                    { 269, "United Kingdom", 55.378051m, -3.435973m, "Celtic Deep" },
                    { 270, "United Kingdom", 55.378051m, -3.435973m, "UK wide" },
                    { 271, "United Kingdom", 56.4906712m, -4.2026458m, "North and West Scotland" },
                    { 272, "United Kingdom", 60.1529871m, -1.1492932m, "Shetland" },
                    { 273, "United States of America", 37.09024m, -95.712891m, "Southwest(Pacific)" },
                    { 274, "United States of America", 39.5333379m, -74.6868815m, "Northeast(Atlantic)" },
                    { 275, "United States of America", 25.304304m, -90.065918m, "(Gulf of Mexico)" },
                    { 276, "United States of America", 61.1892616m, -149.8065058m, "Alaska(Pacific)" },
                    { 277, "United States of America", 34.30856m, -78.1469792m, "Northwest" },
                    { 278, "United States of America", 37.09024m, -95.712891m, "Southwest" },
                    { 279, "United States of America", 37.09024m, -95.712891m, "Southeast(Pacific)" },
                    { 280, "United States of America", 63.588753m, -154.4930619m, "Alaska" },
                    { 281, "United States of America", 19.8986819m, -155.6658568m, "Hawaii" },
                    { 282, "United States of America", 37.09024m, -95.712891m, "Southeast" },
                    { 283, "United States of America", 25.304304m, -90.065918m, "Northwest(Gulf of Mexico)" },
                    { 284, "United States of America", 37.09024m, -95.712891m, "Southeast(Gulf of Mexico)" },
                    { 285, "United States of America", 63.588753m, -154.4930619m, "Alaska(Gulf of Mexico)" },
                    { 286, "United States of America", 52.0767026m, -123.8302432m, "Northwest(Pacific)" },
                    { 287, "United States of America", 21.3072314m, -157.8619256m, "Hawaii(Pacific)" }
                }
            );

            migrationBuilder.InsertData(
                table: "ViewingSuggestions",
                columns: new[] { "Id", "HotSpotId", "Months", "PlatformBoxes", "Platforms", "SpeciesId", "TimeOfYear" },
                values: new object[,]
                {
                    {
                        1,
                        1,
                        new[] { 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 2 },
                        "Motorized boat and land-based",
                        13,
                        "June – December"
                    },
                    {
                        2,
                        2,
                        new[] { 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 2 },
                        "Motorized boat and land-based",
                        13,
                        "June – December"
                    },
                    { 3, 3, new[] { 8, 9, 10 }, new[] { 0 }, "Motorized boat", 13, "September-November" },
                    { 4, 4, new[] { 0, 1, 2, 3 }, new[] { 0 }, "Motorized boat", 8, "January - April" },
                    {
                        5,
                        5,
                        new[] { 1, 2, 3, 4, 9, 10 },
                        new[] { 2 },
                        "Land-based",
                        9,
                        "End of February-May & Oct-Nov"
                    },
                    {
                        6,
                        6,
                        new[] { 1, 2, 3, 4, 9, 10 },
                        new[] { 2 },
                        "Land-based",
                        9,
                        "End of February-May & Oct-Nov"
                    },
                    {
                        7,
                        7,
                        new[] { 1, 2, 3, 4, 9, 10 },
                        new[] { 2 },
                        "Land-based",
                        9,
                        "End of February-May & Oct-Nov"
                    },
                    { 8, 8, new[] { 5, 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June – November" },
                    { 9, 9, new[] { 5, 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June – November" },
                    { 10, 10, new[] { 5, 6, 7, 8, 9, 10, 11 }, new[] { 2 }, "Land-based", 8, "June – December" },
                    { 11, 11, new[] { 5, 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June – November" },
                    { 12, 11, new[] { 5, 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 13, "June – November" },
                    { 13, 11, new[] { 5, 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 3, "June – November" },
                    { 14, 12, new[] { 7, 8, 9 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "August – October" },
                    { 15, 13, new[] { 4, 5, 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "May – November" },
                    { 16, 14, new[] { 4, 5, 6, 7, 8, 9, 10 }, new[] { 0 }, "Unknown", 8, "May – November" },
                    { 17, 15, new[] { 5, 6, 7, 8 }, new[] { 2 }, "Land-based", 13, "June – September" },
                    { 18, 16, new[] { 5, 6, 7, 8 }, new[] { 2 }, "Land-based", 13, "June – September" },
                    { 19, 17, new[] { 0, 1, 2, 3, 4, 10, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 3, "November – May" },
                    { 20, 18, new[] { 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "mid-August – mid-September" },
                    { 21, 19, new[] { 7, 8, 9, 10, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 13, "August – November" },
                    { 22, 19, new[] { 7, 8, 9, 10, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "August – November" },
                    { 23, 19, new[] { 7, 8, 9, 10, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 15, "August – November" },
                    { 24, 20, new[] { 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "September – November" },
                    { 25, 20, new[] { 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 13, "September – November" },
                    { 26, 21, new[] { 2, 3, 4 }, new[] { 0, 2 }, "Unknown", 3, "March - May" },
                    { 27, 22, new[] { 4, 5, 6, 7, 8, 9 }, new[] { 0, 1, 2, 3 }, "Unknown", 13, "May – October" },
                    { 28, 22, new[] { 4, 5, 6, 7, 8, 9 }, new[] { 0, 1, 2, 3 }, "Unknown", 3, "May – October" },
                    { 29, 22, new[] { 4, 5, 6, 7, 8, 9 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "May – October" },
                    {
                        30,
                        23,
                        new[] { 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "May – July, September – December"
                    },
                    {
                        31,
                        23,
                        new[] { 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        13,
                        "May – July, September – December"
                    },
                    {
                        32,
                        24,
                        new[] { 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        13,
                        "May – July, September – December"
                    },
                    { 33, 25, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 34, 26, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 35, 27, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 36, 28, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 37, 29, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 38, 30, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 39, 31, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 40, 32, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 41, 33, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 42, 34, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 43, 35, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July – November" },
                    { 44, 36, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 13, "July – November" },
                    { 45, 37, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 13, "July – November" },
                    { 46, 38, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 13, "July – November" },
                    {
                        47,
                        39,
                        new[] { 3, 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        9,
                        "April – October"
                    },
                    {
                        48,
                        40,
                        new[] { 3, 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        9,
                        "April – October"
                    },
                    {
                        49,
                        41,
                        new[] { 3, 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        9,
                        "April – October"
                    },
                    {
                        50,
                        42,
                        new[] { 3, 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        9,
                        "April – October"
                    },
                    {
                        51,
                        43,
                        new[] { 3, 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        9,
                        "April – October"
                    },
                    {
                        52,
                        44,
                        new[] { 3, 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        9,
                        "April – October"
                    },
                    {
                        53,
                        39,
                        new[] { 2, 3, 4 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        7,
                        "March - May"
                    },
                    {
                        54,
                        40,
                        new[] { 2, 3, 4 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        7,
                        "March – May"
                    },
                    {
                        55,
                        41,
                        new[] { 2, 3, 4 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        7,
                        "March – May"
                    },
                    {
                        56,
                        42,
                        new[] { 2, 3, 4 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        7,
                        "March – May"
                    },
                    {
                        57,
                        43,
                        new[] { 2, 3, 4 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        7,
                        "March – May"
                    },
                    {
                        58,
                        44,
                        new[] { 2, 3, 4 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        7,
                        "March – May"
                    },
                    {
                        59,
                        39,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        8,
                        "May – September"
                    },
                    {
                        60,
                        40,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        8,
                        "May – September"
                    },
                    {
                        61,
                        41,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        8,
                        "May – September"
                    },
                    {
                        62,
                        42,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        8,
                        "May – September"
                    },
                    {
                        63,
                        43,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        8,
                        "May – September"
                    },
                    {
                        64,
                        44,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        8,
                        "May – September"
                    },
                    {
                        65,
                        39,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        10,
                        "May - September"
                    },
                    {
                        66,
                        40,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        10,
                        "May - September"
                    },
                    {
                        67,
                        41,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        10,
                        "May - September"
                    },
                    {
                        68,
                        42,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        10,
                        "May - September"
                    },
                    {
                        69,
                        43,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        10,
                        "May - September"
                    },
                    {
                        70,
                        44,
                        new[] { 4, 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized boat, non-motorized boats, land-based",
                        10,
                        "May - September"
                    },
                    {
                        71,
                        45,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 2 },
                        "Motorized boat, land-based",
                        2,
                        "Year-round"
                    },
                    {
                        72,
                        45,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, land-based",
                        3,
                        "May – October"
                    },
                    {
                        73,
                        45,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, land-based",
                        6,
                        "May – October"
                    },
                    {
                        74,
                        45,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, land-based",
                        8,
                        "May – October"
                    },
                    {
                        75,
                        45,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, land-based",
                        10,
                        "May – October"
                    },
                    {
                        76,
                        45,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized boat, land-based",
                        13,
                        "May – October"
                    },
                    {
                        77,
                        46,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        12,
                        "Unknown"
                    },
                    {
                        78,
                        47,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        12,
                        "Unknown"
                    },
                    {
                        79,
                        48,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        12,
                        "Unknown"
                    },
                    {
                        80,
                        46,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        6,
                        "May – October"
                    },
                    {
                        81,
                        47,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        6,
                        "May – October"
                    },
                    {
                        82,
                        48,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        6,
                        "May – October"
                    },
                    {
                        83,
                        46,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        8,
                        "May – October"
                    },
                    {
                        84,
                        47,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        8,
                        "May – October"
                    },
                    {
                        85,
                        48,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        8,
                        "May – October"
                    },
                    {
                        86,
                        46,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        10,
                        "May – October"
                    },
                    {
                        87,
                        47,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        10,
                        "May – October"
                    },
                    {
                        88,
                        48,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        10,
                        "May – October"
                    },
                    {
                        89,
                        46,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        13,
                        "May – October"
                    },
                    {
                        90,
                        47,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        13,
                        "May – October"
                    },
                    {
                        91,
                        48,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        13,
                        "May – October"
                    },
                    {
                        92,
                        49,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        9,
                        "June – September"
                    },
                    {
                        93,
                        49,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        8,
                        "June – September"
                    },
                    {
                        94,
                        49,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 2 },
                        "Motorized vessel, non-motorized craft, land-based",
                        10,
                        "June – September"
                    },
                    {
                        95,
                        50,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 3 },
                        "Motorized vessel, non-motorized craft, helicopter",
                        2,
                        "June – September"
                    },
                    {
                        96,
                        51,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 3 },
                        "Motorized vessel, non-motorized craft, helicopter",
                        2,
                        "June – September"
                    },
                    {
                        97,
                        52,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 3 },
                        "Motorized vessel, non-motorized craft, helicopter",
                        2,
                        "June – September"
                    },
                    {
                        98,
                        50,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 3 },
                        "Motorized vessel, non-motorized craft, helicopter",
                        11,
                        "June – September"
                    },
                    {
                        99,
                        51,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 3 },
                        "Motorized vessel, non-motorized craft, helicopter",
                        11,
                        "June – September"
                    },
                    {
                        100,
                        52,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 3 },
                        "Motorized vessel, non-motorized craft, helicopter",
                        11,
                        "June – September"
                    },
                    {
                        101,
                        50,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 3 },
                        "Motorized vessel, non-motorized craft, helicopter",
                        4,
                        "June – September"
                    },
                    {
                        102,
                        51,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 3 },
                        "Motorized vessel, non-motorized craft, helicopter",
                        4,
                        "June – September"
                    },
                    {
                        103,
                        52,
                        new[] { 5, 6, 7, 8 },
                        new[] { 0, 3 },
                        "Motorized vessel, non-motorized craft, helicopter",
                        4,
                        "June – September"
                    },
                    {
                        104,
                        53,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        3,
                        "December – April"
                    },
                    {
                        105,
                        54,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        3,
                        "December – April"
                    },
                    {
                        106,
                        55,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        3,
                        "December – April"
                    },
                    {
                        107,
                        56,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        3,
                        "December – April"
                    },
                    {
                        108,
                        57,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        3,
                        "December – April"
                    },
                    {
                        109,
                        58,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        3,
                        "December – April"
                    },
                    { 110, 53, new[] { 0, 1, 2, 3, 11 }, new[] { 0 }, "Motorised boat", 6, "December – April" },
                    { 111, 54, new[] { 0, 1, 2, 3, 11 }, new[] { 0 }, "Motorised boat", 6, "December – April" },
                    { 112, 55, new[] { 0, 1, 2, 3, 11 }, new[] { 0 }, "Motorised boat", 6, "December – April" },
                    {
                        113,
                        53,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        8,
                        "December – April"
                    },
                    {
                        114,
                        54,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        8,
                        "December – April"
                    },
                    {
                        115,
                        55,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        8,
                        "December – April"
                    },
                    {
                        116,
                        59,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        8,
                        "December – April"
                    },
                    {
                        117,
                        57,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        8,
                        "December – April"
                    },
                    {
                        118,
                        58,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        8,
                        "December – April"
                    },
                    {
                        119,
                        60,
                        new[] { 0, 1, 2, 3, 11 },
                        new[] { 0, 2 },
                        "Motorised boat, land-based",
                        8,
                        "December – April"
                    },
                    { 120, 60, new[] { 0, 1, 2, 3, 11 }, new[] { 2 }, "Land-based", 13, "December – April" },
                    {
                        121,
                        61,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        15,
                        "All year round"
                    },
                    {
                        122,
                        62,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat, kayak",
                        9,
                        "All year round"
                    },
                    {
                        123,
                        60,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat, kayak",
                        9,
                        "All year round"
                    },
                    {
                        124,
                        61,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        12,
                        "All year round"
                    },
                    { 125, 63, new[] { 5, 6, 7, 8, 9, 10 }, new[] { 0 }, "Boat-based tours", 8, "June – November" },
                    { 126, 64, new[] { 5, 6, 7, 8, 9, 10 }, new[] { 0 }, "Boat-based tours", 8, "June – November" },
                    { 127, 65, new[] { 5, 6, 7, 8, 9, 10 }, new[] { 0 }, "Boat-based tours", 8, "June – November" },
                    {
                        128,
                        66,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        5,
                        "Variable"
                    },
                    {
                        129,
                        67,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        5,
                        "Variable"
                    },
                    {
                        130,
                        68,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        5,
                        "Variable"
                    },
                    {
                        131,
                        69,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        5,
                        "Variable"
                    },
                    {
                        132,
                        70,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        133,
                        71,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        134,
                        72,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        135,
                        73,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        136,
                        74,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        137,
                        75,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        138,
                        76,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        139,
                        77,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        140,
                        78,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        141,
                        79,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        142,
                        80,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        143,
                        81,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        144,
                        82,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        145,
                        83,
                        new[] { 0, 1, 2, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Catamaran, Motorized boat",
                        8,
                        "December-March/July-mid-November"
                    },
                    {
                        146,
                        84,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        9,
                        "Uncommon and variable"
                    },
                    {
                        147,
                        70,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        9,
                        "Uncommon and variable"
                    },
                    {
                        148,
                        70,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        9,
                        "Uncommon and variable"
                    },
                    {
                        149,
                        78,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        9,
                        "Uncommon and variable"
                    },
                    {
                        150,
                        85,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        9,
                        "Uncommon and variable"
                    },
                    {
                        151,
                        86,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        9,
                        "Uncommon and variable"
                    },
                    { 152, 87, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 15, "Summer, but not every year" },
                    { 153, 88, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 15, "Summer, but not every year" },
                    { 154, 89, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 15, "Summer, but not every year" },
                    { 155, 90, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 8, "Summer" },
                    { 156, 91, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 8, "Summer" },
                    { 157, 92, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 6, "Summer" },
                    { 158, 93, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 6, "Summer" },
                    { 159, 94, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 6, "Summer" },
                    { 160, 92, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 10, "Summer" },
                    { 161, 93, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 10, "Summer" },
                    { 162, 94, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 10, "Summer" },
                    { 163, 95, new[] { 1, 2, 3, 4 }, new[] { 0 }, "Boat", 4, "February – May" },
                    { 164, 96, new[] { 1, 2, 3, 4 }, new[] { 0 }, "Boat", 4, "February – May" },
                    { 165, 97, new[] { 0, 1, 2, 3, 10, 11 }, new[] { 0 }, "Motorized boat", 8, "November – April" },
                    { 166, 98, new[] { 0, 1, 2, 3, 10, 11 }, new[] { 0 }, "Motorized boat", 8, "November – April" },
                    {
                        167,
                        97,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Motorized boat, in water encounters",
                        15,
                        "All year round"
                    },
                    {
                        168,
                        98,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Motorized boat, in water encounters",
                        15,
                        "All year round"
                    },
                    { 169, 99, new[] { 0, 1, 2 }, new[] { 0 }, "Boat", 8, "January – March" },
                    { 170, 100, new[] { 0, 1, 2 }, new[] { 0 }, "Boat", 8, "January – March" },
                    { 171, 101, new[] { 0, 1, 2 }, new[] { 0 }, "Boat", 8, "January – March" },
                    { 172, 102, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 173, 103, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 174, 104, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 175, 105, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 176, 106, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 177, 107, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 178, 108, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 179, 109, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 180, 110, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 181, 111, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 182, 112, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    { 183, 113, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Motorized boat", 8, "June – October" },
                    {
                        184,
                        114,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat",
                        5,
                        "All year round"
                    },
                    {
                        185,
                        115,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Motorized boats, Cruceros",
                        5,
                        "All year round"
                    },
                    {
                        186,
                        116,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Motorized boats, Cruceros",
                        5,
                        "All year round"
                    },
                    {
                        187,
                        117,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Motorized boats, Cruceros",
                        5,
                        "All year round"
                    },
                    {
                        188,
                        114,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Motorized boats, Cruceros",
                        9,
                        "All year round"
                    },
                    {
                        189,
                        115,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Motorized boats, Cruceros",
                        9,
                        "All year round"
                    },
                    {
                        190,
                        116,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Motorized boats, Cruceros",
                        9,
                        "All year round"
                    },
                    {
                        191,
                        117,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Motorized boats, Cruceros",
                        9,
                        "All year round"
                    },
                    {
                        192,
                        114,
                        new[] { 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Motorized boats, Cruceros",
                        8,
                        "June – October"
                    },
                    {
                        193,
                        115,
                        new[] { 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Motorized boats, Cruceros",
                        8,
                        "June – October"
                    },
                    {
                        194,
                        116,
                        new[] { 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Motorized boats, Cruceros",
                        8,
                        "June – October"
                    },
                    {
                        195,
                        117,
                        new[] { 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Motorized boats, Cruceros",
                        8,
                        "June – October"
                    },
                    { 196, 118, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 6, "June-September" },
                    { 197, 118, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 15, "June-September" },
                    { 198, 119, new[] { 3, 4, 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 6, "April-September" },
                    { 199, 119, new[] { 3, 4, 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 15, "April-September" },
                    { 200, 119, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 12, "June- September" },
                    { 201, 120, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July - November" },
                    { 202, 121, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July - November" },
                    { 203, 122, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July - November" },
                    { 204, 123, new[] { 6, 7, 8, 9, 10 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "July - November" },
                    { 205, 124, new[] { 0, 1, 2, 3, 4, 5, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "December – June" },
                    { 206, 125, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June-September" },
                    { 207, 126, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June-September" },
                    { 208, 127, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June-September" },
                    { 209, 128, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June-September" },
                    { 210, 129, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June-September" },
                    { 211, 130, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June-September" },
                    { 212, 131, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June-September" },
                    { 213, 132, new[] { 5, 6, 7, 8 }, new[] { 0, 1, 2, 3 }, "Unknown", 8, "June-September" },
                    { 214, 133, new[] { 6, 7, 8 }, new[] { 0 }, "Boat-based", 8, "July – September" },
                    { 215, 134, new[] { 6, 7, 8 }, new[] { 0 }, "Boat-based", 8, "July – September" },
                    { 216, 135, new[] { 6, 7, 8 }, new[] { 0 }, "Boat-based", 8, "July – September" },
                    { 217, 136, new[] { 6, 7, 8 }, new[] { 0 }, "Boat-based", 8, "July – September" },
                    { 218, 137, new[] { 0, 1, 8, 9, 10, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 6, "Autumn-Winter" },
                    { 219, 138, new[] { 0, 1, 8, 9, 10, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 6, "Autumn-Winter" },
                    { 220, 139, new[] { 0, 1, 8, 9, 10, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 6, "Autumn-Winter" },
                    { 221, 140, new[] { 0, 1, 8, 9, 10, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 6, "Autumn-Winter" },
                    { 222, 141, new[] { 0, 1, 8, 9, 10, 11 }, new[] { 0, 1, 2, 3 }, "Unknown", 6, "Autumn-Winter" },
                    {
                        223,
                        137,
                        new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        10,
                        "Spring-Summer-Autumn"
                    },
                    {
                        224,
                        142,
                        new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        10,
                        "Spring-Summer-Autumn"
                    },
                    {
                        225,
                        143,
                        new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        10,
                        "Spring-Summer-Autumn"
                    },
                    {
                        226,
                        139,
                        new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        10,
                        "Spring-Summer-Autumn"
                    },
                    {
                        227,
                        141,
                        new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        10,
                        "Spring-Summer-Autumn"
                    },
                    {
                        228,
                        144,
                        new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        10,
                        "Spring-Summer-Autumn"
                    },
                    {
                        229,
                        145,
                        new[] { 0, 1, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "Summer-Autumn-Winter"
                    },
                    {
                        230,
                        146,
                        new[] { 0, 1, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "Summer-Autumn-Winter"
                    },
                    {
                        231,
                        147,
                        new[] { 0, 1, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "Summer-Autumn-Winter"
                    },
                    {
                        232,
                        148,
                        new[] { 0, 1, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "Summer-Autumn-Winter"
                    },
                    {
                        233,
                        149,
                        new[] { 0, 1, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "Summer-Autumn-Winter"
                    },
                    {
                        234,
                        150,
                        new[] { 0, 1, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "Summer-Autumn-Winter"
                    },
                    {
                        235,
                        151,
                        new[] { 0, 1, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "Summer-Autumn-Winter"
                    },
                    {
                        236,
                        152,
                        new[] { 0, 1, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "Summer-Autumn-Winter"
                    },
                    {
                        237,
                        153,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        238,
                        154,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        239,
                        155,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        240,
                        156,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        241,
                        157,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        242,
                        158,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        243,
                        159,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        244,
                        160,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        245,
                        161,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        246,
                        162,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        247,
                        163,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        248,
                        164,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        249,
                        165,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        250,
                        166,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        251,
                        167,
                        new[] { 4, 5, 6, 7 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        6,
                        "May - August"
                    },
                    {
                        252,
                        168,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        253,
                        169,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        254,
                        170,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        255,
                        171,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        256,
                        172,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        257,
                        173,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        258,
                        174,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        259,
                        175,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        260,
                        176,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        261,
                        177,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        262,
                        178,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        263,
                        179,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        264,
                        180,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        265,
                        181,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        266,
                        182,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        267,
                        183,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        268,
                        184,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        15,
                        "May - October"
                    },
                    {
                        269,
                        168,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        270,
                        169,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        271,
                        170,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        272,
                        171,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        273,
                        172,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        274,
                        173,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        275,
                        174,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        276,
                        175,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        277,
                        176,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        278,
                        177,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        279,
                        178,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        280,
                        179,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        281,
                        180,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        282,
                        181,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        283,
                        182,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        284,
                        183,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        285,
                        184,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        12,
                        "May - October"
                    },
                    {
                        286,
                        153,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        287,
                        154,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        288,
                        155,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        289,
                        156,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        290,
                        157,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        291,
                        158,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        292,
                        159,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        293,
                        160,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        294,
                        161,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        295,
                        162,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        296,
                        163,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        297,
                        164,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        298,
                        165,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        299,
                        166,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    {
                        300,
                        167,
                        new[] { 4, 5, 6, 7, 8, 9 },
                        new[] { 0 },
                        "Big motorized boat, sailing boat",
                        1,
                        "May - October"
                    },
                    { 301, 185, new[] { 7, 8, 9 }, new[] { 0 }, "Small boats", 8, "August - October" },
                    { 302, 186, new[] { 7, 8, 9 }, new[] { 0 }, "Small boats", 8, "August - October" },
                    { 303, 187, new[] { 0, 1, 2, 3, 4, 10, 11 }, new[] { 0 }, "Boat", 3, "November - May" },
                    {
                        304,
                        187,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        5,
                        "During El Nino years"
                    },
                    { 305, 187, new[] { 5, 6, 7, 8, 9 }, new[] { 0 }, "Boat", 8, "June - October" },
                    {
                        306,
                        187,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        15,
                        "All year round"
                    },
                    {
                        307,
                        187,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        15,
                        "All year round"
                    },
                    {
                        308,
                        187,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        9,
                        "All year round"
                    },
                    {
                        309,
                        187,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        12,
                        "All year round"
                    },
                    {
                        310,
                        187,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        1,
                        "All year round"
                    },
                    {
                        311,
                        187,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        1,
                        "All year round"
                    },
                    {
                        312,
                        187,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        1,
                        "All year round"
                    },
                    {
                        313,
                        187,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        1,
                        "All year round"
                    },
                    {
                        314,
                        188,
                        new[] { 0, 1, 2, 3, 4, 11 },
                        new[] { 0 },
                        "Medium and senior vessels",
                        7,
                        "December - May"
                    },
                    { 315, 189, new[] { 1, 2 }, new[] { 0 }, "Motorized small boats", 7, "February-March" },
                    { 316, 190, new[] { 1, 2 }, new[] { 0 }, "Motorized small boats", 7, "February-March" },
                    { 317, 191, new[] { 1, 2 }, new[] { 0 }, "Motorized small boats", 7, "February-March" },
                    { 318, 192, new[] { 1, 2 }, new[] { 0 }, "Motorized small boats", 7, "February-March" },
                    { 319, 193, new[] { 1, 2 }, new[] { 0 }, "Motorized small boats", 3, "February-March" },
                    { 320, 194, new[] { 1, 2 }, new[] { 0 }, "Motorized boats different sizes.", 8, "February-March" },
                    { 321, 195, new[] { 1, 2 }, new[] { 0 }, "Motorized boats different sizes.", 8, "February-March" },
                    { 322, 196, new[] { 1, 2 }, new[] { 0 }, "Motorized boats different sizes.", 8, "February-March" },
                    { 323, 197, new[] { 1 }, new[] { 0 }, "Motorized boats different sizes.", 8, "February" },
                    { 324, 198, new[] { 1 }, new[] { 0 }, "Motorized boats different sizes.", 8, "February" },
                    { 325, 199, new[] { 1 }, new[] { 0 }, "Motorized boats different sizes.", 8, "February" },
                    { 326, 200, new[] { 1 }, new[] { 0 }, "Motorized boats different sizes.", 8, "February" },
                    { 327, 201, new[] { 1 }, new[] { 0 }, "Motorized boats different sizes.", 8, "February" },
                    { 328, 202, new[] { 1 }, new[] { 0 }, "Motorized boats different sizes.", 8, "February" },
                    { 329, 203, new[] { 1 }, new[] { 0 }, "Motorized small boats", 8, "February" },
                    { 330, 204, new[] { 1 }, new[] { 0 }, "Motorized small boats", 8, "February" },
                    { 331, 205, new[] { 0, 1 }, new[] { 0 }, "Motorized small boats", 8, "January-February" },
                    { 332, 206, new[] { 0, 1 }, new[] { 0 }, "Motorized small boats", 8, "January-February" },
                    { 333, 193, new[] { 1, 2 }, new[] { 0 }, "Motorized small boats", 6, "February - March" },
                    {
                        334,
                        207,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 3 },
                        "Boat, Aerial",
                        15,
                        "All year round"
                    },
                    {
                        335,
                        208,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        5,
                        "All year round"
                    },
                    { 336, 207, new[] { 5, 6 }, new[] { 0, 3 }, "Boat, Aerial", 8, "June-July" },
                    { 337, 207, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 13, "June-August" },
                    { 338, 209, new[] { 5, 6, 7 }, new[] { 0 }, "Boat", 13, "June-August" },
                    {
                        339,
                        210,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        9,
                        "All year round"
                    },
                    {
                        340,
                        211,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        9,
                        "All year round"
                    },
                    {
                        341,
                        212,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        9,
                        "All year round"
                    },
                    {
                        342,
                        213,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        9,
                        "All year round"
                    },
                    {
                        343,
                        214,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Boat",
                        9,
                        "All year round"
                    },
                    {
                        344,
                        215,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        15,
                        "All year round"
                    },
                    {
                        345,
                        216,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        15,
                        "All year round"
                    },
                    {
                        346,
                        217,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        9,
                        "All year round"
                    },
                    { 347, 218, new[] { 0, 10, 11 }, new[] { 0 }, "Motorised boat", 9, "Nov-Jan" },
                    {
                        348,
                        219,
                        new[] { 0, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        9,
                        "Nov-Jan, depending on if/where the herring will be overwintering in fjords"
                    },
                    {
                        349,
                        220,
                        new[] { 0, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        9,
                        "Nov-Jan, depending on if/where the herring will be overwintering in fjords"
                    },
                    {
                        350,
                        215,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        12,
                        "Andenes: Year around (infrequent)"
                    },
                    {
                        351,
                        216,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        12,
                        "Unknown"
                    },
                    {
                        352,
                        221,
                        new[] { 0, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        9,
                        "Nov-Jan, depending on if/where the herring will be overwintering in fjords"
                    },
                    {
                        353,
                        222,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        8,
                        "All year round"
                    },
                    { 354, 223, new[] { 5, 6, 7 }, new[] { 0 }, "Motorised boat", 8, "Summer" },
                    {
                        355,
                        224,
                        new[] { 0, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        8,
                        "Nov-Jan, depending on if/where the herring will be overwintering in fjords"
                    },
                    {
                        356,
                        225,
                        new[] { 0, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        8,
                        "Nov-Jan, depending on if/where the herring will be overwintering in fjords"
                    },
                    {
                        357,
                        226,
                        new[] { 0, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        8,
                        "Nov-Jan, depending on if/where the herring will be overwintering in fjords"
                    },
                    {
                        358,
                        227,
                        new[] { 0, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        8,
                        "Nov-Jan, depending on if/where the herring will be overwintering in fjords"
                    },
                    {
                        359,
                        215,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorised boat",
                        10,
                        "All year round"
                    },
                    { 360, 216, new[] { 5, 6, 7 }, new[] { 0 }, "Motorised boat", 10, "Summer" },
                    { 361, 227, new[] { 5, 6, 7 }, new[] { 0 }, "Motorised boat", 6, "Summer (might be observed)" },
                    { 362, 227, new[] { 5, 6, 7 }, new[] { 0 }, "Motorised boat", 3, "Summer (might be observed)" },
                    {
                        363,
                        227,
                        new[] { 5, 6, 7 },
                        new[] { 0 },
                        "Motorised boat",
                        2,
                        "Summer (might be observed) (expeditions)"
                    },
                    {
                        364,
                        227,
                        new[] { 5, 6, 7 },
                        new[] { 0 },
                        "Motorised boat",
                        11,
                        "Summer (might be observed) (expeditions)"
                    },
                    {
                        365,
                        227,
                        new[] { 5, 6, 7 },
                        new[] { 0 },
                        "Motorised boat",
                        4,
                        "Summer (might be observed) (expeditions)"
                    },
                    {
                        366,
                        228,
                        new[] { 0, 1, 2, 3, 6, 7, 8, 9, 11 },
                        new[] { 0 },
                        "Boat-based and live-aboard",
                        8,
                        "July-October or December –April (Golfo de Chriqui only)"
                    },
                    {
                        367,
                        229,
                        new[] { 0, 1, 2, 3, 6, 7, 8, 9, 11 },
                        new[] { 0 },
                        "Boat-based and live-aboard",
                        8,
                        "July-October or December –April (Golfo de Chriqui only)"
                    },
                    {
                        368,
                        230,
                        new[] { 0, 1, 2, 3, 6, 7, 8, 9, 11 },
                        new[] { 0 },
                        "Boat-based and live-aboard",
                        8,
                        "July-October or December –April (Golfo de Chriqui only)"
                    },
                    {
                        369,
                        231,
                        new[] { 0, 1, 2, 3, 6, 7, 8, 9, 11 },
                        new[] { 0 },
                        "Boat-based and live-aboard",
                        8,
                        "July-October or December –April (Golfo de Chriqui only)"
                    },
                    {
                        370,
                        232,
                        new[] { 0, 1, 2, 3, 6, 7, 8, 9, 11 },
                        new[] { 0 },
                        "Boat-based and live-aboard",
                        8,
                        "July-October or December –April (Golfo de Chriqui only)"
                    },
                    {
                        371,
                        233,
                        new[] { 0, 1, 2, 3, 6, 7, 8, 9, 11 },
                        new[] { 0 },
                        "Boat-based and live-aboard",
                        8,
                        "July-October or December –April (Golfo de Chriqui only)"
                    },
                    {
                        372,
                        234,
                        new[] { 0, 1, 2, 3, 6, 7, 8, 9, 11 },
                        new[] { 0 },
                        "Boat-based and live-aboard",
                        8,
                        "July-October or December –April (Golfo de Chriqui only)"
                    },
                    {
                        373,
                        235,
                        new[] { 6, 7, 8, 9 },
                        new[] { 0 },
                        "Motorized boat (yachts, artisanal fishing boats)",
                        8,
                        "July - October"
                    },
                    {
                        374,
                        236,
                        new[] { 6, 7, 8, 9 },
                        new[] { 0 },
                        "Motorized boat (yachts, artisanal fishing boats)",
                        8,
                        "July - October"
                    },
                    {
                        375,
                        237,
                        new[] { 6, 7, 8, 9 },
                        new[] { 0 },
                        "Motorized boat (yachts, artisanal fishing boats)",
                        8,
                        "July - October"
                    },
                    {
                        376,
                        238,
                        new[] { 6, 7, 8, 9 },
                        new[] { 0 },
                        "Motorized boat (yachts, artisanal fishing boats)",
                        8,
                        "July - October"
                    },
                    {
                        377,
                        239,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat (yachts, artisanal fishing boats)",
                        5,
                        "Rarely observed"
                    },
                    {
                        378,
                        239,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "Motorized boat (yachts, artisanal fishing boats)",
                        9,
                        "Rarely observed"
                    },
                    {
                        379,
                        240,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        15,
                        "All year (more abundant during summer)"
                    },
                    {
                        380,
                        241,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "motorized boat",
                        15,
                        "All year"
                    },
                    { 381, 241, new[] { 3, 4, 5, 6, 7, 8, 9 }, new[] { 0 }, "motorized boat", 5, "April-October" },
                    {
                        382,
                        240,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        8,
                        "Not very common"
                    },
                    {
                        383,
                        241,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        8,
                        "Not very common"
                    },
                    {
                        384,
                        242,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        8,
                        "Not very common"
                    },
                    { 385, 240, new[] { 3, 4, 5 }, new[] { 0 }, "motorized boat", 14, "April to June" },
                    { 386, 241, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 14, "June-August" },
                    { 387, 240, new[] { 3, 4, 5 }, new[] { 0 }, "motorized boat", 6, "April to June" },
                    { 388, 241, new[] { 0, 1, 2, 3, 4, 11 }, new[] { 0 }, "motorized boat", 6, "winter and spring" },
                    {
                        389,
                        242,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        6,
                        "More common in offshore zones"
                    },
                    { 390, 240, new[] { 3, 4, 5 }, new[] { 0 }, "motorized boat", 3, "April to June" },
                    {
                        391,
                        241,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        3,
                        "Not very common"
                    },
                    {
                        392,
                        240,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        10,
                        "Not very common"
                    },
                    {
                        393,
                        241,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        10,
                        "Not very common"
                    },
                    {
                        394,
                        242,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        10,
                        "All year (coastal areas)"
                    },
                    { 395, 243, new[] { 5, 6, 7, 8 }, new[] { 0 }, "motorized boat", 9, "June to September" },
                    {
                        396,
                        241,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        9,
                        "Not very common"
                    },
                    {
                        397,
                        241,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        9,
                        "Not very common"
                    },
                    {
                        398,
                        241,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        12,
                        "All year"
                    },
                    {
                        399,
                        241,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        1,
                        "All year (hard to spot due to shy behavior)"
                    },
                    {
                        400,
                        241,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        1,
                        "All year (hard to spot due to shy behavior)"
                    },
                    { 401, 244, new[] { 6, 7, 8, 9, 10 }, new[] { 0 }, "Motorized boat", 5, "Jul-Nov" },
                    { 402, 245, new[] { 6, 7, 8, 9, 10 }, new[] { 0 }, "Motorized boat", 5, "Jul-Nov" },
                    { 403, 246, new[] { 6, 7, 8, 9, 10 }, new[] { 0 }, "Motorized boat", 5, "Jul-Nov" },
                    { 404, 247, new[] { 0, 1, 2, 3, 6, 7, 8, 9, 10, 11 }, new[] { 0 }, "Vessel based", 5, "Jul-Apr" },
                    { 405, 248, new[] { 8, 9, 10 }, new[] { 0 }, "Vessel based", 5, "Sept-Nov" },
                    { 406, 249, new[] { 5, 6, 7 }, new[] { 0 }, "Vessel based", 8, "Jun-Aug" },
                    { 407, 244, new[] { 5, 6, 7 }, new[] { 0 }, "Vessel based", 8, "Jun-Aug" },
                    { 408, 245, new[] { 5, 6, 7 }, new[] { 0 }, "Vessel based", 8, "Jun-Aug" },
                    { 409, 246, new[] { 5, 6, 7 }, new[] { 0 }, "Vessel based", 8, "Jun-Aug" },
                    {
                        410,
                        247,
                        new[] { 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 2 },
                        "Vessel Based; Land based",
                        8,
                        "Jun-Apr"
                    },
                    {
                        411,
                        250,
                        new[] { 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 2 },
                        "Vessel Based; Land based",
                        8,
                        "Jun-Apr"
                    },
                    { 412, 251, new[] { 5, 6, 7, 8, 9, 10 }, new[] { 0 }, "Vessel Based", 8, "Jun-Nov" },
                    { 413, 252, new[] { 5, 6, 7, 8, 9, 10, 11 }, new[] { 0 }, "Vessel based", 8, "Jun-Sept" },
                    {
                        414,
                        253,
                        new[] { 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 2 },
                        "Vessel based; land based",
                        13,
                        "Jun-Nov"
                    },
                    {
                        415,
                        254,
                        new[] { 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 2 },
                        "Vessel based; land based",
                        13,
                        "Jun-Nov"
                    },
                    {
                        416,
                        255,
                        new[] { 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 2 },
                        "Vessel based; land based",
                        13,
                        "Jun-Nov"
                    },
                    {
                        417,
                        256,
                        new[] { 5, 6, 7, 8, 9, 10 },
                        new[] { 0, 2 },
                        "Vessel based; land based",
                        13,
                        "Jun-Nov"
                    },
                    { 418, 257, new[] { 6, 7, 8, 9 }, new[] { 0 }, "Vessel based", 13, "Jul-Oct" },
                    { 419, 258, new[] { 5, 6, 7, 8, 9 }, new[] { 0, 2 }, "Vessel Based; Land based", 13, "Jun-Oct" },
                    { 420, 259, new[] { 5, 6, 7, 8, 9 }, new[] { 0, 2 }, "Vessel based; Land based", 13, "Jun-Oct" },
                    { 421, 248, new[] { 5, 6, 7, 8, 9, 10, 11 }, new[] { 0 }, "Vessel based", 13, "Jun-Sept" },
                    { 422, 244, new[] { 9 }, new[] { 0 }, "Vessel based", 9, "October" },
                    { 423, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 14, "May and June" },
                    { 424, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 3, "May and June" },
                    { 425, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 6, "May and June" },
                    { 426, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 10, "May and June" },
                    { 427, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 5, "May and June" },
                    { 428, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 13, "May and June" },
                    { 429, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 9, "May and June" },
                    {
                        430,
                        260,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        12,
                        "All year"
                    },
                    {
                        431,
                        260,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        12,
                        "All year"
                    },
                    { 432, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 8, "May and June" },
                    { 433, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 9, "May and June" },
                    { 434, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 15, "May and June" },
                    { 435, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 9, "May and June" },
                    { 436, 260, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 1, "May and June" },
                    { 437, 261, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 14, "May and June" },
                    { 438, 261, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 3, "May and June" },
                    { 439, 261, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 6, "May and June" },
                    { 440, 261, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 10, "May and June" },
                    {
                        441,
                        261,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        12,
                        "All year"
                    },
                    { 442, 261, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 9, "May and June" },
                    { 443, 261, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 15, "May and June" },
                    { 444, 261, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 9, "May and June" },
                    { 445, 261, new[] { 4, 5 }, new[] { 0 }, "motorized boat", 1, "May and June" },
                    {
                        446,
                        262,
                        new[] { 0, 1, 2, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        3,
                        "between December and March"
                    },
                    { 447, 263, new[] { 2, 3, 4, 5, 6 }, new[] { 0, 1, 2, 3 }, "Unknown", 3, "between March and July" },
                    {
                        448,
                        262,
                        new[] { 0, 1, 2, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        5,
                        "between December and March"
                    },
                    { 449, 263, new[] { 2, 3, 4, 5, 6 }, new[] { 0, 1, 2, 3 }, "Unknown", 5, "between March and July" },
                    {
                        450,
                        262,
                        new[] { 0, 1, 2, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        15,
                        "between December and March"
                    },
                    {
                        451,
                        263,
                        new[] { 2, 3, 4, 5, 6 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        15,
                        "between March and July"
                    },
                    { 452, 264, new[] { 0, 1, 2, 11 }, new[] { 0 }, "Motorized boat", 8, "Sporadic, Dec-March" },
                    { 453, 265, new[] { 6, 7, 8, 9 }, new[] { 0, 1 }, "Boat, swim with", 8, "July-October" },
                    { 454, 266, new[] { 6, 7, 8, 9 }, new[] { 0, 1 }, "Boat, swim with", 8, "July-October" },
                    { 455, 267, new[] { 6, 7, 8, 9 }, new[] { 0, 1 }, "Boat, swim with", 8, "July-October" },
                    { 456, 268, new[] { 5, 6, 7 }, new[] { 0, 1, 2, 3 }, "Unknown", 6, "Summer (offshore)" },
                    { 457, 269, new[] { 5, 6, 7 }, new[] { 0, 1, 2, 3 }, "Unknown", 6, "Summer (offshore)" },
                    {
                        458,
                        270,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        10,
                        "All year"
                    },
                    {
                        459,
                        270,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        8,
                        "Sporadic sightings throughout the year"
                    },
                    {
                        460,
                        271,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        9,
                        "All year"
                    },
                    {
                        461,
                        272,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0, 1, 2, 3 },
                        "Unknown",
                        9,
                        "All year"
                    },
                    { 462, 273, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 3, "Summer" },
                    { 463, 274, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 5, "Summer" },
                    { 464, 273, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 5, "Summer" },
                    { 465, 275, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 5, "Summer" },
                    { 466, 274, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 6, "Summer" },
                    { 467, 276, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 6, "Summer" },
                    { 468, 277, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 6, "Summer" },
                    { 469, 278, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 6, "Summer" },
                    { 470, 276, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 7, "Summer (Alaska)" },
                    { 471, 277, new[] { 2, 3, 4 }, new[] { 2 }, "shore-based", 7, "Spring (Northwest, Southwest)" },
                    { 472, 278, new[] { 2, 3, 4 }, new[] { 2 }, "shore-based", 7, "Spring (Northwest, Southwest)" },
                    {
                        473,
                        274,
                        new[] { 5, 6, 7 },
                        new[] { 0 },
                        "motorized boat",
                        8,
                        "Summer (Northeast, Alaska, Northwest, Southwest)"
                    },
                    {
                        474,
                        279,
                        new[] { 0, 1, 8, 9, 10, 11 },
                        new[] { 2 },
                        "shore-based",
                        8,
                        "Winter & Spring (Southeast, Hawaii)"
                    },
                    {
                        475,
                        280,
                        new[] { 5, 6, 7 },
                        new[] { 2 },
                        "shore-based",
                        8,
                        "Summer (Northeast, Alaska, Northwest, Southwest)"
                    },
                    {
                        476,
                        277,
                        new[] { 5, 6, 7 },
                        new[] { 2 },
                        "shore-based",
                        8,
                        "Summer (Northeast, Alaska, Northwest, Southwest)"
                    },
                    {
                        477,
                        278,
                        new[] { 5, 6, 7 },
                        new[] { 2 },
                        "shore-based",
                        8,
                        "Summer (Northeast, Alaska, Northwest, Southwest)"
                    },
                    {
                        478,
                        281,
                        new[] { 0, 1, 2, 3, 4, 11 },
                        new[] { 2 },
                        "shore-based",
                        8,
                        "Winter & Spring (Southeast, Hawaii)"
                    },
                    { 479, 274, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 10, "Summer" },
                    { 480, 273, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 10, "Summer" },
                    { 481, 277, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 10, "Summer" },
                    { 482, 274, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 13, "Summer (Northeast)" },
                    { 483, 282, new[] { 0, 1, 11 }, new[] { 2 }, "shore-based", 13, "Winter (Southeast)" },
                    { 484, 274, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 14, "Summer" },
                    { 485, 279, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 14, "Summer" },
                    { 486, 274, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 15, "Summer" },
                    { 487, 276, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 15, "Summer" },
                    { 488, 283, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 15, "Summer" },
                    { 489, 278, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 15, "Summer" },
                    {
                        490,
                        274,
                        new[] { 5, 6, 7 },
                        new[] { 0, 1, 2, 3 },
                        "motorized boat",
                        9,
                        "Summer (Northeast, Southwest)"
                    },
                    {
                        491,
                        284,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        9,
                        "Year round (Southeast, Hawaii)"
                    },
                    {
                        492,
                        273,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        9,
                        "Year round (Southeast, Hawaii)"
                    },
                    {
                        493,
                        281,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        9,
                        "Year round (Southeast, Hawaii)"
                    },
                    { 494, 274, new[] { 5, 6, 7 }, new[] { 0 }, "motorized boat", 9, "Summer" },
                    {
                        495,
                        285,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 2 },
                        "shore-based",
                        9,
                        "Rare (Hawaii)"
                    },
                    {
                        496,
                        286,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 2 },
                        "shore-based",
                        9,
                        "Year-Round (Northwest)"
                    },
                    {
                        497,
                        278,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 2 },
                        "shore-based",
                        9,
                        "Year-Round (Northwest)"
                    },
                    {
                        498,
                        281,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 2 },
                        "shore-based",
                        9,
                        "Year-Round (Northwest)"
                    },
                    {
                        499,
                        274,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        12,
                        "Year-round"
                    },
                    {
                        500,
                        284,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        12,
                        "Year-round"
                    },
                    {
                        501,
                        287,
                        new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                        new[] { 0 },
                        "motorized boat",
                        12,
                        "Year-round"
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
