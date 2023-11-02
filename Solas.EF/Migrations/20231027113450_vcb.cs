using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class vcb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_AddressId",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "IX_orders_AddressId",
                table: "orders",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_AddressId",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "IX_orders_AddressId",
                table: "orders",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");
        }
    }
}
