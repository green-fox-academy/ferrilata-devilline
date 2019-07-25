using Microsoft.EntityFrameworkCore.Migrations;

namespace ferrilata_devilline.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Badges_f",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "f",
                table: "Levels",
                newName: "BadgeId");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_f",
                table: "Levels",
                newName: "IX_Levels_BadgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Badges_BadgeId",
                table: "Levels",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "BadgeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Badges_BadgeId",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "BadgeId",
                table: "Levels",
                newName: "f");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_BadgeId",
                table: "Levels",
                newName: "IX_Levels_f");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Badges_f",
                table: "Levels",
                column: "f",
                principalTable: "Badges",
                principalColumn: "BadgeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
