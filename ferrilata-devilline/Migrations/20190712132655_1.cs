using Microsoft.EntityFrameworkCore.Migrations;

namespace ferrilata_devilline.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    BadgeId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Version = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.BadgeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    LevelId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    LevelNumber = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true),
                    BadgeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.LevelId);
                    table.ForeignKey(
                        name: "FK_Levels_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "BadgeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pitches",
                columns: table => new
                {
                    PitchId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Status = table.Column<string>(nullable: true),
                    PitchedLevel = table.Column<string>(nullable: true),
                    PitchedMessage = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    Created = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    LevelId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pitches", x => x.PitchId);
                    table.ForeignKey(
                        name: "FK_Pitches_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pitches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLevels",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    LevelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLevels", x => new { x.UserId, x.LevelId });
                    table.ForeignKey(
                        name: "FK_UserLevels_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLevels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Message = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    PitchId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Pitches_PitchId",
                        column: x => x.PitchId,
                        principalTable: "Pitches",
                        principalColumn: "PitchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_BadgeId",
                table: "Levels",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pitches_LevelId",
                table: "Pitches",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Pitches_UserId",
                table: "Pitches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PitchId",
                table: "Reviews",
                column: "PitchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLevels_LevelId",
                table: "UserLevels",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserLevels");

            migrationBuilder.DropTable(
                name: "Pitches");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Badges");
        }
    }
}
