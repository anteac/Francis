using Microsoft.EntityFrameworkCore.Migrations;

namespace Francis.Database.Migrations
{
    public partial class RemovePlexStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OmbiId",
                table: "BotUsers");

            migrationBuilder.DropColumn(
                name: "PlexId",
                table: "BotUsers");

            migrationBuilder.DropColumn(
                name: "PlexToken",
                table: "BotUsers");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "BotUsers",
                newName: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "BotUsers",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "OmbiId",
                table: "BotUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlexId",
                table: "BotUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlexToken",
                table: "BotUsers",
                type: "TEXT",
                nullable: true);
        }
    }
}
