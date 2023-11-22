using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Data.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "CoffeeId", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "https://example.com/image1.jpg", "Coffee 1", 5.99m },
                    { 2, "https://example.com/image2.jpg", "Coffee 2", 6.99m },
                    { 3, "https://example.com/image3.jpg", "Coffee 3", 7.99m },
                    { 4, "https://example.com/image4.jpg", "Coffee 4", 8.99m },
                    { 5, "https://example.com/image5.jpg", "Coffee 5", 9.99m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coffees",
                keyColumn: "CoffeeId",
                keyValue: 5);
        }
    }
}
