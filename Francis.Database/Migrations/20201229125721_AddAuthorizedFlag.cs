using Microsoft.EntityFrameworkCore.Migrations;

namespace Francis.Database.Migrations
{
    public partial class AddAuthorizedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "BotUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Authorized",
                table: "BotUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authorized",
                table: "BotUsers");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "BotUsers",
                type: "TEXT",
                nullable: true);
        }
    }
}
