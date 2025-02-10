using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationS_H_1_n_To_n_n : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Hardwares_HardwareId",
                table: "Softwares");

            migrationBuilder.DropIndex(
                name: "IX_Softwares_HardwareId",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "HardwareId",
                table: "Softwares");

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
                        name: "FK_HardwareSoftware_Hardwares_HardwaresId",
                        column: x => x.HardwaresId,
                        principalTable: "Hardwares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HardwareSoftware_Softwares_SoftwaresId",
                        column: x => x.SoftwaresId,
                        principalTable: "Softwares",
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

            migrationBuilder.AddColumn<int>(
                name: "HardwareId",
                table: "Softwares",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_HardwareId",
                table: "Softwares",
                column: "HardwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Hardwares_HardwareId",
                table: "Softwares",
                column: "HardwareId",
                principalTable: "Hardwares",
                principalColumn: "Id");
        }
    }
}
