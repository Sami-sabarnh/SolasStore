using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class gggbcssafnnlkjhfgmgkjhcdgnhx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponCodedisscount",
                table: "couponCodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CouponCodepercent",
                table: "couponCodes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCodedisscount",
                table: "couponCodes");

            migrationBuilder.DropColumn(
                name: "CouponCodepercent",
                table: "couponCodes");
        }
    }
}
