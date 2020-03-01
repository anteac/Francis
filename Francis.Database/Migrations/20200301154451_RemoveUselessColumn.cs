using Microsoft.EntityFrameworkCore.Migrations;

namespace Francis.Database.Migrations
{
    public partial class RemoveUselessColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.IsSqlite())
            {
                return;
            }

            migrationBuilder.DropColumn(
                name: "RequestProgressionId",
                table: "BotUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.IsSqlite())
            {
                return;
            }

            migrationBuilder.AddColumn<int>(
                name: "RequestProgressionId",
                table: "BotUsers",
                type: "INTEGER",
                nullable: true);
        }
    }
}
