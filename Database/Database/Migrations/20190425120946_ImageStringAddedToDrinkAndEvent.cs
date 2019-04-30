using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ImageStringAddedToDrinkAndEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Drinks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "BarEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "BarEvents");
        }
    }
}
