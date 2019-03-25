using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class PRJ4v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bar",
                columns: table => new
                {
                    BarName = table.Column<string>(maxLength: 150, nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    AgeLimit = table.Column<int>(nullable: false),
                    Educations = table.Column<string>(maxLength: 255, nullable: true),
                    ShortDescription = table.Column<string>(maxLength: 500, nullable: true),
                    LongDescription = table.Column<string>(maxLength: 2500, nullable: true),
                    CVR = table.Column<int>(maxLength: 8, nullable: false),
                    PhoneNumber = table.Column<int>(maxLength: 10, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    AvgRating = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bar", x => x.BarName);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    FavoriteBar = table.Column<string>(maxLength: 150, nullable: true),
                    FavoriteDrink = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "BarEvents",
                columns: table => new
                {
                    BarName = table.Column<string>(maxLength: 150, nullable: false),
                    EventName = table.Column<string>(maxLength: 75, nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarEvents", x => new { x.BarName, x.EventName });
                    table.ForeignKey(
                        name: "FK_BarEvents_Bar_BarName",
                        column: x => x.BarName,
                        principalTable: "Bar",
                        principalColumn: "BarName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Barrepresentatives",
                columns: table => new
                {
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    BarName = table.Column<string>(maxLength: 150, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barrepresentatives", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Barrepresentatives_Bar_BarName",
                        column: x => x.BarName,
                        principalTable: "Bar",
                        principalColumn: "BarName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    BarName = table.Column<string>(maxLength: 150, nullable: false),
                    CouponID = table.Column<string>(maxLength: 50, nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => new { x.CouponID, x.BarName });
                    table.ForeignKey(
                        name: "FK_Coupons_Bar_BarName",
                        column: x => x.BarName,
                        principalTable: "Bar",
                        principalColumn: "BarName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    BarName = table.Column<string>(maxLength: 150, nullable: false),
                    DrinksName = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => new { x.BarName, x.DrinksName });
                    table.ForeignKey(
                        name: "FK_Drinks_Bar_BarName",
                        column: x => x.BarName,
                        principalTable: "Bar",
                        principalColumn: "BarName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    BarName = table.Column<string>(maxLength: 150, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    BarPressure = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.BarName, x.Username });
                    table.ForeignKey(
                        name: "FK_Reviews_Bar_BarName",
                        column: x => x.BarName,
                        principalTable: "Bar",
                        principalColumn: "BarName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_Username",
                        column: x => x.Username,
                        principalTable: "Customers",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bar_CVR",
                table: "Bar",
                column: "CVR",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bar_Email",
                table: "Bar",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Barrepresentatives_BarName",
                table: "Barrepresentatives",
                column: "BarName");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_BarName",
                table: "Coupons",
                column: "BarName");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Username",
                table: "Reviews",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarEvents");

            migrationBuilder.DropTable(
                name: "Barrepresentatives");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Bar");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
