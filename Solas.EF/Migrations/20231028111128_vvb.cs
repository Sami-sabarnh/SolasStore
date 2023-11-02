using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class vvb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderProductId",
                table: "optionss",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_optionss_OrderProductId",
                table: "optionss",
                column: "OrderProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_optionss_orderProducts_OrderProductId",
                table: "optionss",
                column: "OrderProductId",
                principalTable: "orderProducts",
                principalColumn: "OrderProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_optionss_orderProducts_OrderProductId",
                table: "optionss");

            migrationBuilder.DropIndex(
                name: "IX_optionss_OrderProductId",
                table: "optionss");

            migrationBuilder.DropColumn(
                name: "OrderProductId",
                table: "optionss");
        }
    }
}
