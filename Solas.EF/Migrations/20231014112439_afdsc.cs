using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class afdsc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "orderProducts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_orderProducts_UserId",
                table: "orderProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderProducts_AspNetUsers_UserId",
                table: "orderProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderProducts_AspNetUsers_UserId",
                table: "orderProducts");

            migrationBuilder.DropIndex(
                name: "IX_orderProducts_UserId",
                table: "orderProducts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "orderProducts");
        }
    }
}
