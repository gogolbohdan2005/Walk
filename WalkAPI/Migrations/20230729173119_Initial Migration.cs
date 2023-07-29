using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WalkAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowToGetHere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Places = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Walks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonMoutains = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxHeight = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    Elevation = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonImageURLs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walks_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "HowToGetHere", "Name", "Places" },
                values: new object[,]
                {
                    { 1, "Using train from Lviv or Ivano-Frankivsk is very conveniet and preferable", "Lazeshchyna (Yasinna)", "You can get to mountais Kukul, Petros, Drahobrat, it`s waterfall, lake Ivor and waterfall Trufanets, " },
                    { 2, "Using regular (12:00, 15:00 am) bus from Verhovyna, Verhovyna can be reached from Ivano-Frankivsk using bus", "Dzembronya", "Able to reach awesome mountain Pip Ivan, Dzembronya and it`s waterfall and even second biggest mountain -- Brebenescyl" },
                    { 3, "Using train or bus to Vorohta, then catching taxi or unregular bus to base Zaroslak", "Zaroslak", "Visit Hoverla, polonuna Pozhyzhevska, lake Nesamovute, mountains Rebra, Hytun-Tomnatyk, Shputzi" },
                    { 4, "Using bus from IvanoFrankivsk or train from Frankivsk, Lviv, other cities", "Yaremche", "You can visit various waterfalls, old aqueducs, polonynas and small mountain" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Walks");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
