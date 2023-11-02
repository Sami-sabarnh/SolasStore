using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class shippingx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedArrivalTime",
                table: "shippings");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "shippings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "shippings");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedArrivalTime",
                table: "shippings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
