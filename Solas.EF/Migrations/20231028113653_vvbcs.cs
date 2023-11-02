using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solas.EF.Migrations
{
    /// <inheritdoc />
    public partial class vvbcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "storeInfos",
                columns: table => new
                {
                    StoreInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupportCenterNumber = table.Column<int>(type: "int", nullable: false),
                    SupportCenterAvailable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storeInfos", x => x.StoreInfoId);
                });

            migrationBuilder.CreateTable(
                name: "socialMedias",
                columns: table => new
                {
                    SocialMediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialMediaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_socialMedias", x => x.SocialMediaId);
                    table.ForeignKey(
                        name: "FK_socialMedias_storeInfos_StoreInfoId",
                        column: x => x.StoreInfoId,
                        principalTable: "storeInfos",
                        principalColumn: "StoreInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_socialMedias_StoreInfoId",
                table: "socialMedias",
                column: "StoreInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "socialMedias");

            migrationBuilder.DropTable(
                name: "storeInfos");
        }
    }
}
