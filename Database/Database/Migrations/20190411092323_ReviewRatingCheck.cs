using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ReviewRatingCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var check = @"ALTER TABLE Reviews
                        ADD CONSTRAINT chk_rating
                        CHECK(BarPressure>=0 AND BarPressure<=5);";

            migrationBuilder.Sql(check);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var check = @"ALTER TABLE Reviews
                        DROP CONSTRAINT chk_rating;";

            migrationBuilder.Sql(check);

        }
    }
}
