using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Destination = table.Column<string>(nullable: true),
                    ReviewDetails = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    City = table.Column<string>(maxLength: 20, nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "City", "Country", "Destination", "Rating", "ReviewDetails" },
                values: new object[,]
                {
                    { 1, "Seattle", "USA", "Pike Place Market", 3, "Eh" },
                    { 2, "Portland", "USA", "Rose Garden", 3, "Crazy people" },
                    { 6, "Portland", "USA", "Rose Garden", 2, "Crazy people" },
                    { 3, "Maui", "USA", "Sandy beaches", 5, "Fun" },
                    { 4, "Cape Town", "South Africa", "soccer stadium", 4, "Nice" },
                    { 5, "Beijing", "China", "Forbidden City", 1, "Ugh" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
