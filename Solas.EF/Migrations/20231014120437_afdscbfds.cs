using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class afdscbfds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalOrginal",
                table: "orderProducts");

            migrationBuilder.DropColumn(
                name: "Totaldiscoun",
                table: "orderProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TotalOrginal",
                table: "orderProducts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Totaldiscoun",
                table: "orderProducts",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
