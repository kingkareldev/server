using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingKarel.Migrations
{
    public partial class Setupstorymissionrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameProgress_Game_GameId",
                table: "GameProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_GameProgress_Users_UserId",
                table: "GameProgress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameProgress",
                table: "GameProgress");

            migrationBuilder.RenameTable(
                name: "GameProgress",
                newName: "GameProgresses");

            migrationBuilder.RenameIndex(
                name: "IX_GameProgress_UserId",
                table: "GameProgresses",
                newName: "IX_GameProgresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameProgress_GameId",
                table: "GameProgresses",
                newName: "IX_GameProgresses_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameProgresses",
                table: "GameProgresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameProgresses_Game_GameId",
                table: "GameProgresses",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameProgresses_Users_UserId",
                table: "GameProgresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameProgresses_Game_GameId",
                table: "GameProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_GameProgresses_Users_UserId",
                table: "GameProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameProgresses",
                table: "GameProgresses");

            migrationBuilder.RenameTable(
                name: "GameProgresses",
                newName: "GameProgress");

            migrationBuilder.RenameIndex(
                name: "IX_GameProgresses_UserId",
                table: "GameProgress",
                newName: "IX_GameProgress_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameProgresses_GameId",
                table: "GameProgress",
                newName: "IX_GameProgress_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameProgress",
                table: "GameProgress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameProgress_Game_GameId",
                table: "GameProgress",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameProgress_Users_UserId",
                table: "GameProgress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
