using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class vvbcscsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminJustShow",
                table: "menus",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminJustShow",
                table: "menus");
        }
    }
}
