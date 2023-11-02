using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class gggb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "OrderProductOptions",
                columns: table => new
                {
                    OptionsOptionId = table.Column<int>(type: "int", nullable: false),
                    orderProductsOrderProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductOptions", x => new { x.OptionsOptionId, x.orderProductsOrderProductId });
                    table.ForeignKey(
                        name: "FK_OrderProductOptions_optionss_OptionsOptionId",
                        column: x => x.OptionsOptionId,
                        principalTable: "optionss",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductOptions_orderProducts_orderProductsOrderProductId",
                        column: x => x.orderProductsOrderProductId,
                        principalTable: "orderProducts",
                        principalColumn: "OrderProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductOptions_orderProductsOrderProductId",
                table: "OrderProductOptions",
                column: "orderProductsOrderProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProductOptions");

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
    }
}
