using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatabaseObject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseObject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_DatabaseObject_Id",
                        column: x => x.Id,
                        principalTable: "DatabaseObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobTitle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTitle_DatabaseObject_Id",
                        column: x => x.Id,
                        principalTable: "DatabaseObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Software_DatabaseObject_Id",
                        column: x => x.Id,
                        principalTable: "DatabaseObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cabinet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cabinet_DatabaseObject_Id",
                        column: x => x.Id,
                        principalTable: "DatabaseObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cabinet_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    CabinetId = table.Column<int>(type: "integer", nullable: true),
                    JobTitleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Cabinet_CabinetId",
                        column: x => x.CabinetId,
                        principalTable: "Cabinet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_DatabaseObject_Id",
                        column: x => x.Id,
                        principalTable: "DatabaseObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_JobTitle_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitle",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComplexHardware",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    InventoryNumber = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexHardware", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplexHardware_DatabaseObject_Id",
                        column: x => x.Id,
                        principalTable: "DatabaseObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplexHardware_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hardware",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    Article = table.Column<string>(type: "text", nullable: true),
                    DateCreate = table.Column<DateOnly>(type: "date", nullable: false),
                    ComplexHardwareId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardware", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hardware_ComplexHardware_ComplexHardwareId",
                        column: x => x.ComplexHardwareId,
                        principalTable: "ComplexHardware",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hardware_DatabaseObject_Id",
                        column: x => x.Id,
                        principalTable: "DatabaseObject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Cabinet_DepartmentId",
                table: "Cabinet",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplexHardware_UserId",
                table: "ComplexHardware",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardware_ComplexHardwareId",
                table: "Hardware",
                column: "ComplexHardwareId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareSoftware_SoftwaresId",
                table: "HardwareSoftware",
                column: "SoftwaresId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CabinetId",
                table: "User",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_User_JobTitleId",
                table: "User",
                column: "JobTitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareSoftware");

            migrationBuilder.DropTable(
                name: "Hardware");

            migrationBuilder.DropTable(
                name: "Software");

            migrationBuilder.DropTable(
                name: "ComplexHardware");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Cabinet");

            migrationBuilder.DropTable(
                name: "JobTitle");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "DatabaseObject");
        }
    }
}
