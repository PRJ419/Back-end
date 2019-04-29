using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddedImagesToDataSeedings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bar",
                keyColumn: "BarName",
                keyValue: "Katrines Kælder",
                column: "Image",
                value: "https://scontent-dus1-1.xx.fbcdn.net/v/t1.0-9/13166_441233562611984_1450333570_n.png?_nc_cat=105&_nc_ht=scontent-dus1-1.xx&oh=3a0e9139a633dd8d9131afd229eab1da&oe=5D2B6EDD");

            migrationBuilder.UpdateData(
                table: "Bar",
                keyColumn: "BarName",
                keyValue: "Medicinsk Fredagsbar - Umbilicus",
                column: "Image",
                value: "https://scontent-dus1-1.xx.fbcdn.net/v/t1.0-9/43698279_2427965823910886_4605085834809442304_n.png?_nc_cat=111&_nc_ht=scontent-dus1-1.xx&oh=e3bc006d52005d545011cc52b1f8a7d8&oe=5D76ACBE");

            migrationBuilder.UpdateData(
                table: "BarEvents",
                keyColumns: new[] { "BarName", "EventName" },
                keyValues: new object[] { "Katrines Kælder", "Tobias tager Level Up" },
                column: "Image",
                value: "https://vignette.wikia.nocookie.net/my-hero-academia-fanon/images/0/0a/Level_Up.png/revision/latest?cb=20180722000746");

            migrationBuilder.UpdateData(
                table: "BarEvents",
                keyColumns: new[] { "BarName", "EventName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Andreas på tur!" },
                column: "Image",
                value: "https://media1.s-nbcnews.com/i/newscms/2016_48/1811466/161128-drinking-alcohol-jpo-108p_52ad934c90bc61c93c2242c4349f5e55.jpg");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Katrines Kælder", "Fadøl" },
                column: "Image",
                value: "https://r2brewery.dk/wp-content/uploads/2017/11/pilsner.png");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Katrines Kælder", "Flaskeøl" },
                column: "Image",
                value: "https://www.calle.dk/SL/PI/705/128/8c021bde-d649-4515-8c92-0effa962bafe.jpg");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Ceres Top" },
                column: "Image",
                value: "https://www.calle.dk/SL/PI/705/128/8c021bde-d649-4515-8c92-0effa962bafe.jpg");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Hospitalssprit" },
                column: "Image",
                value: "https://www.fotoagent.dk/single_picture/11981/138/mega/201096_1.jpg");

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Vodka Redbull" },
                column: "Image",
                value: "https://www.drinkdelivery.it/wp-content/uploads/2015/05/vodka-absolute-redbull.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bar",
                keyColumn: "BarName",
                keyValue: "Katrines Kælder",
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bar",
                keyColumn: "BarName",
                keyValue: "Medicinsk Fredagsbar - Umbilicus",
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "BarEvents",
                keyColumns: new[] { "BarName", "EventName" },
                keyValues: new object[] { "Katrines Kælder", "Tobias tager Level Up" },
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "BarEvents",
                keyColumns: new[] { "BarName", "EventName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Andreas på tur!" },
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Katrines Kælder", "Fadøl" },
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Katrines Kælder", "Flaskeøl" },
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Ceres Top" },
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Hospitalssprit" },
                column: "Image",
                value: null);

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Vodka Redbull" },
                column: "Image",
                value: null);
        }
    }
}
