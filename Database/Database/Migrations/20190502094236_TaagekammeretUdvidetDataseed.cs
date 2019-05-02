using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class TaagekammeretUdvidetDataseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BarEvents",
                columns: new[] { "BarName", "EventName", "Date", "Image" },
                values: new object[,]
                {
                    { "Katrines Kælder", "Bingo Bar", new DateTime(2019, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://letsbingo.dk/wp-content/uploads/2016/01/Bingo-graphic21.jpg" },
                    { "Medicinsk Fredagsbar - Umbilicus", "Ipod Battle Bar", new DateTime(2019, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://ecdn.evensi.com/e301908712?cph=gAAAAABcvXMS1wYy7ZyTjIpjScMOwT9aERAm7bQrt8iS-5I7umkCbr2B_3zz_OTxoGxamKgI6Su1nCvudV9KA74kF3CljhF-AJEPUhQt7K-NfWz1IbgakBxjcqUGS98Pw-gQ8RLV39CMKlB13oChr1i6Ai8xjvDMOfQ-aJr-3abxG37hRxzubXfSky6WWGnYlQDbioz7SjJXMWE3eT1mPcBhMZox_B0gQLJw7cR802diHabhG3iV17hgvr0-zOUE-DlWTjFEuK_TuZ1yFd6kuC3BWwBswtOM58r_prd6HgX2ETPXJ9iFge65DKMKUsXhVCrwB5GjRBXU0NXP9MccSFwpN2rgrXAXykspY9N_FFUMbaJOZGlhllsH89N4QKdv4oba32VDCPa9" },
                    { "Tågekammeret", "Bodycrashing", new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://taagekammeret.dk/media/__sized__/2016/bodycrashing/15271383_10210607041708452_1374044913_o-crop-c0-5__0-5-253x253-70.jpg" }
                });

            migrationBuilder.InsertData(
                table: "BarRepresentatives",
                columns: new[] { "Username", "BarName", "Name" },
                values: new object[] { "Humleridderen", "Tågekammeret", "Kenny Kernel Space" });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponID", "BarName", "ExpirationDate" },
                values: new object[] { "20MemLeak", "Tågekammeret", new DateTime(2019, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Username", "DateOfBirth", "Email", "FavoriteBar", "FavoriteDrink", "Name" },
                values: new object[] { "Koffein Karsten", new DateTime(1990, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "KaffeTrolden@gmail.com", "Tågekammeret", "Kaffe", "Karsten" });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "BarName", "DrinksName", "Image", "Price" },
                values: new object[,]
                {
                    { "Tågekammeret", "Radioaktivt Affald", "https://videnskab.dk/sites/default/files/styles/columns_12_12_desktop/public/article_media/atomaffald.jpg?itok=LXcUsHe-&timestamp=1464219173", 20.0 },
                    { "Tågekammeret", "Fadøl", "https://r2brewery.dk/wp-content/uploads/2017/11/pilsner.png", 10.0 },
                    { "Tågekammeret", "Stroh Rom", "https://www.fotoagent.dk/single_picture/10620/138/mega/stroh_80(2).jpg", 30.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarEvents",
                keyColumns: new[] { "BarName", "EventName" },
                keyValues: new object[] { "Katrines Kælder", "Bingo Bar" });

            migrationBuilder.DeleteData(
                table: "BarEvents",
                keyColumns: new[] { "BarName", "EventName" },
                keyValues: new object[] { "Medicinsk Fredagsbar - Umbilicus", "Ipod Battle Bar" });

            migrationBuilder.DeleteData(
                table: "BarEvents",
                keyColumns: new[] { "BarName", "EventName" },
                keyValues: new object[] { "Tågekammeret", "Bodycrashing" });

            migrationBuilder.DeleteData(
                table: "BarRepresentatives",
                keyColumn: "Username",
                keyValue: "Humleridderen");

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumns: new[] { "CouponID", "BarName" },
                keyValues: new object[] { "20MemLeak", "Tågekammeret" });

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Username",
                keyValue: "Koffein Karsten");

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Tågekammeret", "Fadøl" });

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Tågekammeret", "Radioaktivt Affald" });

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumns: new[] { "BarName", "DrinksName" },
                keyValues: new object[] { "Tågekammeret", "Stroh Rom" });
        }
    }
}
