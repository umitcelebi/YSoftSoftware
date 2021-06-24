using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YSoftSoftware.WebUI.Migrations
{
    public partial class AddCompensationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DismissalDate",
                table: "Personels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Personels",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Personels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Personels",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Compensations",
                columns: table => new
                {
                    compensationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compensations", x => x.compensationId);
                    table.ForeignKey(
                        name: "FK_Compensations_Personels_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personels",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compensations_PersonelId",
                table: "Compensations",
                column: "PersonelId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compensations");

            migrationBuilder.DropColumn(
                name: "DismissalDate",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Personels");
        }
    }
}
