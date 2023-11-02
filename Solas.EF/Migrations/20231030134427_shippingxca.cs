using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class shippingxca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippingId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_ShippingId",
                table: "orders",
                column: "ShippingId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_shippings_ShippingId",
                table: "orders",
                column: "ShippingId",
                principalTable: "shippings",
                principalColumn: "ShippingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_shippings_ShippingId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ShippingId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShippingId",
                table: "orders");
        }
    }
}
