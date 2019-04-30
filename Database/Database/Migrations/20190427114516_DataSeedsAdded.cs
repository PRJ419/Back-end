using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class DataSeedsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bar",
                columns: new[] { "BarName", "Address", "AgeLimit", "AvgRating", "CVR", "Educations", "Email", "Image", "LongDescription", "PhoneNumber", "ShortDescription" },
                values: new object[,]
                {
                    { "Katrines Kælder", "5125 Edison, Finlandsgade 22, 8200 Aarhus", 18, 5.0, 33985703, "IKT,EE,E,ST", "katrineskaelder@outlook.dk", null, "Der er mange øl", 12345678, "Der er øl" },
                    { "Medicinsk Fredagsbar - Umbilicus", "Medicinerhuset, Bygning 1161, Ole Worms Allé 4, 8000 Aarhus", 18, 3.0, 29129932, "Medicin", "bestyrelsen@umbi.dk", null, "Der er alt for mange mennesker og alt for få øl", 51927090, "Der er varme øl" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Username", "DateOfBirth", "Email", "FavoriteBar", "FavoriteDrink", "Name" },
                values: new object[,]
                {
                    { "Bodega Bent", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JegElskerØl@Yahoo.com", "Katrines Kælder", "Fadøl", "Bent" },
                    { "Dehydrerede Dennis", new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "JegErTørstig@gmail.com", "Medicinsk Fredagsbar - Umbilicus", "Vodka Redbull", "Dennis" }
                });

            migrationBuilder.InsertData(
                table: "BarEvents",
                columns: new[] { "BarName", "EventName", "Date", "Image" },
                values: new object[,]
                {
                    { "Katrines Kælder", "Tobias tager Level Up", new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { "Medicinsk Fredagsbar - Umbilicus", "Andreas på tur!", new DateTime(2019, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "BarRepresentatives",
                columns: new[] { "Username", "BarName", "Name" },
                values: new object[,]
                {
                    { "Legend27", "Katrines Kælder", "Ole Ølmave" },
                    { "Kratluskeren", "Medicinsk Fredagsbar - Umbilicus", "Tørstige Torsten" }
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponID", "BarName", "ExpirationDate" },
                values: new object[,]
                {
                    { "123ØL", "Katrines Kælder", new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "VarmØlNuTak", "Medicinsk Fredagsbar - Umbilicus", new DateTime(2019, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "BarName", "DrinksName", "Image", "Price" },
                values: new object[,]
                {
                    { "Katrines Kælder", "Spejlæg", null, 50.0 },
                    { "Katrines Kælder", "Flaskeøl", null, 10.0 },
                    { "Katrines Kælder", "Fadøl", null, 20.0 },
                    { "Medicinsk Fredagsbar - Umbilicus", "Ceres Top", null, 10.0 },
                    { "Medicinsk Fredagsbar - Umbilicus", "Vodka Redbull", null, 20.0 },
                    { "Medicinsk Fredagsbar - Umbilicus", "Hospitalssprit", null, 10.0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "BarName", "Username", "BarPressure" },
                values: new object[,]
                {
                    { "Katrines Kælder", "Bodega Bent", 5 },
                    { "Medicinsk Fredagsbar - Umbilicus", "Bodega Bent", 3 },
                    { "Katrines Kælder", "Dehydrerede Dennis", 5 },
                    { "Medicinsk Fredagsbar - Umbilicus", "Dehydrerede Dennis", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarEvents",
                keyColumns: new[] { "BarName", "EventName" },
                keyValues: new object[] { "Katrines Kælder", "Tobias tager Level Up" });

            migrationBuilder.DeleteData(
                table: "BarEvents",
                keyColumns: new[] { "BarName", "EventName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Andreas på tur!" });

            migrationBuilder.DeleteData(
                table: "BarRepresentatives",
                keyColumn: "Username",
                keyValue: "Kratluskeren");

            migrationBuilder.DeleteData(
                table: "BarRepresentatives",
                keyColumn: "Username",
                keyValue: "Legend27");

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumns: new[] { "CouponID", "BarName" },
                keyValues: new object[] { "123ØL", "Katrines Kælder" });

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumns: new[] { "CouponID", "BarName" },
                keyValues: new object[] { "VarmØlNuTak", "Medicinsk Fredagsbar - Umbilicus" });

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Katrines Kælder", "Fadøl" });

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Katrines Kælder", "Flaskeøl" });

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Katrines Kælder", "Spejlæg" });

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Ceres Top" });

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Hospitalssprit" });

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Vodka Redbull" });

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumns: new[] { "BarName", "Username" },
                keyValues: new object[] { "Katrines Kælder", "Bodega Bent" });

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumns: new[] { "BarName", "Username" },
                keyValues: new object[] { "Katrines Kælder", "Dehydrerede Dennis" });

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumns: new[] { "BarName", "Username" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Bodega Bent" });

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumns: new[] { "BarName", "Username" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Dehydrerede Dennis" });

            migrationBuilder.DeleteData(
                table: "Bar",
                keyColumn: "BarName",
                keyValue: "Katrines Kælder");

            migrationBuilder.DeleteData(
                table: "Bar",
                keyColumn: "BarName",
                keyValue: "Medicinsk Fredagsbar - Umbilicus");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Username",
                keyValue: "Bodega Bent");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Username",
                keyValue: "Dehydrerede Dennis");
        }
    }
}
