using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class shippingxcaxa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_shippings_ShippingId",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingId",
                table: "orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_shippings_ShippingId",
                table: "orders",
                column: "ShippingId",
                principalTable: "shippings",
                principalColumn: "ShippingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_shippings_ShippingId",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_shippings_ShippingId",
                table: "orders",
                column: "ShippingId",
                principalTable: "shippings",
                principalColumn: "ShippingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
