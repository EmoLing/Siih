using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareSoftwares");

            migrationBuilder.CreateTable(
                name: "HardwareSoftware",
                columns: table => new
                {
                    HardwaresId = table.Column<int>(type: "integer", nullable: false),
                    SoftwaresId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareSoftware", x => new { x.HardwaresId, x.SoftwaresId });
                    table.ForeignKey(
                        name: "FK_HardwareSoftware_Hardware_HardwaresId",
                        column: x => x.HardwaresId,
                        principalTable: "Hardware",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HardwareSoftware_Software_SoftwaresId",
                        column: x => x.SoftwaresId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardwareSoftware_SoftwaresId",
                table: "HardwareSoftware",
                column: "SoftwaresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareSoftware");

            migrationBuilder.CreateTable(
                name: "HardwareSoftwares",
                columns: table => new
                {
                    HardwareId = table.Column<int>(type: "integer", nullable: false),
                    SoftwareId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareSoftwares", x => new { x.HardwareId, x.SoftwareId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardwareSoftwares_SoftwareId",
                table: "HardwareSoftwares",
                column: "SoftwareId");
        }
    }
}
