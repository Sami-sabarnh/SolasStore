using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class orderprooptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderProductOption",
                columns: table => new
                {
                    OrderProductOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderProductId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductOption", x => x.OrderProductOptionId);
                    table.ForeignKey(
                        name: "FK_OrderProductOption_optionss_OptionId",
                        column: x => x.OptionId,
                        principalTable: "optionss",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductOption_orderProducts_OrderProductId",
                        column: x => x.OrderProductId,
                        principalTable: "orderProducts",
                        principalColumn: "OrderProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductOption_OptionId",
                table: "OrderProductOption",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductOption_OrderProductId",
                table: "OrderProductOption",
                column: "OrderProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProductOption");
        }
    }
}
