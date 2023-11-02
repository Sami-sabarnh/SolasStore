using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class gggbcssaf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponCodeId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_CouponCodeId",
                table: "orders",
                column: "CouponCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_couponCodes_CouponCodeId",
                table: "orders",
                column: "CouponCodeId",
                principalTable: "couponCodes",
                principalColumn: "CouponCodeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_couponCodes_CouponCodeId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_CouponCodeId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CouponCodeId",
                table: "orders");
        }
    }
}
