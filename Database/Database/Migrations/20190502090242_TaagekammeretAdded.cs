using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class TaagekammeretAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bar",
                columns: new[] { "BarName", "Address", "AgeLimit", "AvgRating", "CVR", "Educations", "Email", "Image", "LongDescription", "PhoneNumber", "ShortDescription" },
                values: new object[] { "Tågekammeret", "Ny Munkegade 118, 8000 Aarhus", 18, 4.0, 34126399, "Fysik, Datalogi, IT Bachelor, Matematik-Økonomi, Nanoteknologi", "BEST@TAAGEKAMMERET.dk", "https://taagekammeret.dk/static/TKlogo.jpg", "Du ender i et sort hul, hvis du bliver ved med at drikke her", 87154052, "Husk Tågelygter!" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bar",
                keyColumn: "BarName",
                keyValue: "Tågekammeret");
        }
    }
}
