using Microsoft.EntityFrameworkCore.Migrations;

namespace ferrilata_devilline.Migrations
{
    public partial class EmptyDb6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PitchTable",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    BadgeName = table.Column<string>(nullable: true),
                    OldLVL = table.Column<long>(nullable: false),
                    PitchedLVL = table.Column<long>(nullable: false),
                    PitchMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PitchTable", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PitchTable");
        }
    }
}
