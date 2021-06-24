using Microsoft.EntityFrameworkCore.Migrations;

namespace YSoftSoftware.WebUI.Migrations
{
    public partial class ChangePersonelModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personels_Projects_ProjectId",
                table: "Personels");

            migrationBuilder.DropIndex(
                name: "IX_Personels_ProjectId",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Personels");

            migrationBuilder.CreateTable(
                name: "PersonelProject",
                columns: table => new
                {
                    PersonelsPersonelId = table.Column<int>(type: "int", nullable: false),
                    ProjectsProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelProject", x => new { x.PersonelsPersonelId, x.ProjectsProjectId });
                    table.ForeignKey(
                        name: "FK_PersonelProject_Personels_PersonelsPersonelId",
                        column: x => x.PersonelsPersonelId,
                        principalTable: "Personels",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelProject_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonelProject_ProjectsProjectId",
                table: "PersonelProject",
                column: "ProjectsProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelProject");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Personels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Personels_ProjectId",
                table: "Personels",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personels_Projects_ProjectId",
                table: "Personels",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
