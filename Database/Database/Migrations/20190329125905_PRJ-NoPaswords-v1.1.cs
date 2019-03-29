using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class PRJNoPaswordsv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Barrepresentatives");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Barrepresentatives",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
