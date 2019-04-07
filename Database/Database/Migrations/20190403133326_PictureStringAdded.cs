using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class PictureStringAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barrepresentatives_Bar_BarName",
                table: "Barrepresentatives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Barrepresentatives",
                table: "Barrepresentatives");

            migrationBuilder.RenameTable(
                name: "Barrepresentatives",
                newName: "BarRepresentatives");

            migrationBuilder.RenameIndex(
                name: "IX_Barrepresentatives_BarName",
                table: "BarRepresentatives",
                newName: "IX_BarRepresentatives_BarName");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Bar",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarRepresentatives",
                table: "BarRepresentatives",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_BarRepresentatives_Bar_BarName",
                table: "BarRepresentatives",
                column: "BarName",
                principalTable: "Bar",
                principalColumn: "BarName",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarRepresentatives_Bar_BarName",
                table: "BarRepresentatives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarRepresentatives",
                table: "BarRepresentatives");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Bar");

            migrationBuilder.RenameTable(
                name: "BarRepresentatives",
                newName: "Barrepresentatives");

            migrationBuilder.RenameIndex(
                name: "IX_BarRepresentatives_BarName",
                table: "Barrepresentatives",
                newName: "IX_Barrepresentatives_BarName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Barrepresentatives",
                table: "Barrepresentatives",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Barrepresentatives_Bar_BarName",
                table: "Barrepresentatives",
                column: "BarName",
                principalTable: "Bar",
                principalColumn: "BarName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
