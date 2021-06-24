using Microsoft.EntityFrameworkCore.Migrations;

namespace YSoftSoftware.WebUI.Migrations
{
    public partial class changeModelValidator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personels_AccountingProgram_AccountingProgramId",
                table: "Personels");

            migrationBuilder.AlterColumn<int>(
                name: "AccountingProgramId",
                table: "Personels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Personels_AccountingProgram_AccountingProgramId",
                table: "Personels",
                column: "AccountingProgramId",
                principalTable: "AccountingProgram",
                principalColumn: "AccountingProgramId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personels_AccountingProgram_AccountingProgramId",
                table: "Personels");

            migrationBuilder.AlterColumn<int>(
                name: "AccountingProgramId",
                table: "Personels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Personels_AccountingProgram_AccountingProgramId",
                table: "Personels",
                column: "AccountingProgramId",
                principalTable: "AccountingProgram",
                principalColumn: "AccountingProgramId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
