using Microsoft.EntityFrameworkCore.Migrations;

namespace ferrilata_devilline.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Badges_hello",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "hello",
                table: "Levels",
                newName: "f");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_hello",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Badges_f",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "f",
                table: "Levels",
                newName: "hello");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_f",
                table: "Levels",
                newName: "IX_Levels_hello");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Badges_hello",
                table: "Levels",
                column: "hello",
                principalTable: "Badges",
                principalColumn: "BadgeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
