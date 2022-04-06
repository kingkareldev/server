using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingKarel.Migrations
{
    public partial class Addtaskdescriptionforgame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "Game",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "Game");
        }
    }
}
