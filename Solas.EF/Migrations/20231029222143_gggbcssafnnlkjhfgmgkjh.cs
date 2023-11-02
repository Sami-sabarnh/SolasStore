using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class gggbcssafnnlkjhfgmgkjh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_couponCodes_CouponCodeId",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "CouponCodeId",
                table: "orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_couponCodes_CouponCodeId",
                table: "orders",
                column: "CouponCodeId",
                principalTable: "couponCodes",
                principalColumn: "CouponCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_couponCodes_CouponCodeId",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "CouponCodeId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_couponCodes_CouponCodeId",
                table: "orders",
                column: "CouponCodeId",
                principalTable: "couponCodes",
                principalColumn: "CouponCodeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
